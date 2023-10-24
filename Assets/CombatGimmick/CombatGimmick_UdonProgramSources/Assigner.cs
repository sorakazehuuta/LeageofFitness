
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace CombatGimmick
{
    public enum TeamName
    {
        A, B, C, D, None    　
    }

    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class Assigner : UdonSharpBehaviour
    {
        //---------------------------------------
        [Header("必須項目.無いとエラー")]
        [Tooltip("このAssignerで登録するヒットボックス")] public PlayerHitBox playerHitBox;
        [Tooltip("所属するチーム(None以外を選択すること)")] public TeamName teamName;
        //---------------------------------------
        [Header("任意項目.無しでも動作可能")]
        [Tooltip("登録成功時の効果音")] [SerializeField] AudioClip SuccessAudioClip;
        [Tooltip("登録失敗時の効果音")] [SerializeField] AudioClip FailAudioClip;
        [Tooltip("登録したAssignerを示すマーカー")] [SerializeField] GameObject AssignedMarkerObject;
        [Tooltip("他プレイヤー登録済みを示すマーカー")] [SerializeField] GameObject OtherPlayerAssignedMarkerObject;
        [Tooltip("登録済みプレイヤー名を表示するテキスト")] [SerializeField] Text PlayerNameText;
        //---------------------------------------
        [Header("NoResetモードインベントリ用の設定")]
        [Tooltip("SingleInventory用のPickup")] public VRC_Pickup SingleInventoryPickup;
        [Tooltip("DoubleInventory用のPickup")] public VRC_Pickup DoubleInventoryPickup;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("Assigner[]")] public Assigner[] Assigners;
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        //---------------------------------------
        AudioSource audioSource;
        int leftPlayerID;
        const int defaultLayer = 0;
        const int InvisibleToAssignedPlayerLayer = 18;   //MirrorReflectionはローカルプレイヤーの目に見えない(カメラやミラー越しでのみ見える)
        //---------------------------------------
        void Start()
        {
            audioSource = this.GetComponent<AudioSource>();

            DisableNoResetInventoryPickup();

            if (AssignedMarkerObject) { AssignedMarkerObject.SetActive(false); }

            SendCustomEventDelayedSeconds("DelayedSecond_StartSync", 1.0f);
            SendCustomEventDelayedSeconds("DelayedSecond_InitializeOtherPlayerAssignedMarker", 10.0f);
        }
        //---------------------------------------
        public void DelayedSecond_StartSync()
        {
            playerHitBox.OnDeserialization();
        }
        //---------------------------------------
        public void DelayedSecond_InitializeOtherPlayerAssignedMarker()
        {
            if (!OtherPlayerAssignedMarkerObject) { return; }

            else if (playerHitBox.SyncedPlayerID <= 0)
            {
                OtherPlayerAssignedMarkerObject.SetActive(false);
                return;
            }
                       
            else if (!Utilities.IsValid(VRCPlayerApi.GetPlayerById(playerHitBox.SyncedPlayerID)))
            {
                OtherPlayerAssignedMarkerObject.SetActive(false);
                return;
            }

            else
            {
                OtherPlayerAssignedMarkerObject.SetActive(true);
                return;
            }
        }
        //---------------------------------------
        public override void Interact()
        {
            TryAssign();
        }
        //---------------------------------------
        public void TryAssign()
        {
            //ゲーム中は登録を実行できない
            if (gameManager.SyncedInGame || gameManager.SyncedInBattle)
            {
                if (FailAudioClip) { audioSource.PlayOneShot(FailAudioClip, 1.0f); }
                return;
            }

            //自分がこのAssignerに登録済みだった場合、登録を解除する
            if (CheckIsOccupied() && CheckIsOccupiedByMe())
            {
                Release();
                if (SuccessAudioClip) { audioSource.PlayOneShot(SuccessAudioClip, 1.0f); }
                gameManager.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_HideStartChecker");
                return;
            }

            //自分以外がこのAssignerに登録済みだった場合、エラー音をならす
            if (CheckIsOccupied() && !CheckIsOccupiedByMe())
            {
                if (FailAudioClip) { audioSource.PlayOneShot(FailAudioClip, 1.0f); }
                return;
            }

            //このAssignerに登録済みプレイヤーがいない場合、ローカルプレイヤーが登録済みのAssignerを解除し、改めてこのAssignerに登録する
            for (int i = 0; i < Assigners.Length; ++i) { if (Assigners[i].CheckIsOccupiedByMe()) { Assigners[i].Release(); } }
            Assign();
            if (SuccessAudioClip) { audioSource.PlayOneShot(SuccessAudioClip, 1.0f); }
            return;
        }
        //---------------------------------------
        public bool CheckIsOccupied()
        {
            if (playerHitBox.SyncedPlayerID != 0) { return true; }
            return false;
        }
        //---------------------------------------
        public bool CheckIsOccupiedByMe()
        {
            if (Utilities.IsValid(Networking.LocalPlayer) && playerHitBox.SyncedPlayerID == Networking.LocalPlayer.playerId) { return true; }
            return false;
        }
        //---------------------------------------
        public void Release()
        {
            playerManager.playerHitBox = null;

            TrySetOwner(playerHitBox.gameObject);
            playerHitBox.SyncedReviveTicket = 0;
            playerHitBox.SyncedPlayerID = 0;
            playerHitBox.SyncedScore = 0;
            playerHitBox.Sync();

            DisableNoResetInventoryPickup();

            if (AssignedMarkerObject) { AssignedMarkerObject.SetActive(false); }
            if (playerHitBox.InvisibleToAssignedPlayerObject) { playerHitBox.InvisibleToAssignedPlayerObject.layer = defaultLayer; }

            playerHitBox.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_UpdatePlayerHitBoxRenderer");
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_HideOtherPlayerAssignedMarker");
        }
        //---------------------------------------
        public void Assign()
        {
            playerManager.playerHitBox = playerHitBox;
            playerManager.ResetAmmo();

            int assignID;
            if (Utilities.IsValid(Networking.LocalPlayer)) { assignID = Networking.LocalPlayer.playerId; }
            else { assignID = 0; }

            TrySetOwner(playerHitBox.gameObject);
            playerHitBox.FullHitPoint();
            playerHitBox.ResetShield();
            playerHitBox.SyncedReviveTicket = gameManager.SyncedMaxReviveTicket;
            playerHitBox.SyncedPlayerID = assignID;
            playerHitBox.SyncedScore = 0;
            playerHitBox.Sync();

            EnableNoResetInventoryPickup();

            if (AssignedMarkerObject) { AssignedMarkerObject.SetActive(true); }
            if (playerHitBox.InvisibleToAssignedPlayerObject) { playerHitBox.InvisibleToAssignedPlayerObject.layer = InvisibleToAssignedPlayerLayer; }

            playerHitBox.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_UpdatePlayerHitBoxRenderer");
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_ShowOtherPlayerAssignedMarker");
        }
        //---------------------------------------
        public void EnableNoResetInventoryPickup()
        {
            if (SingleInventoryPickup)
            {
                if (SingleInventoryPickup.GetComponent<RangedWeapon_MainModule>()) { SingleInventoryPickup.GetComponent<RangedWeapon_MainModule>().EnableWeapon(); }
                if (SingleInventoryPickup.GetComponent<RangedWeapon_SubModule>()) { SingleInventoryPickup.GetComponent<RangedWeapon_SubModule>().EnableWeapon(); }
                if (SingleInventoryPickup.GetComponent<ExWeapon>()) { SingleInventoryPickup.GetComponent<ExWeapon>().EnableWeapon(); }
                if (SingleInventoryPickup.GetComponent<MeleeWeapon>()) { SingleInventoryPickup.GetComponent<MeleeWeapon>().EnableWeapon(); }
            }
            if (DoubleInventoryPickup)
            {
                if (DoubleInventoryPickup.GetComponent<RangedWeapon_MainModule>()) { DoubleInventoryPickup.GetComponent<RangedWeapon_MainModule>().EnableWeapon(); }
                if (DoubleInventoryPickup.GetComponent<RangedWeapon_SubModule>()) { DoubleInventoryPickup.GetComponent<RangedWeapon_SubModule>().EnableWeapon(); }
                if (DoubleInventoryPickup.GetComponent<ExWeapon>()) { DoubleInventoryPickup.GetComponent<ExWeapon>().EnableWeapon(); }
                if (DoubleInventoryPickup.GetComponent<MeleeWeapon>()) { DoubleInventoryPickup.GetComponent<MeleeWeapon>().EnableWeapon(); }
            }
        }
        //---------------------------------------
        public void DisableNoResetInventoryPickup()
        {
            if (SingleInventoryPickup)
            {
                if (SingleInventoryPickup.GetComponent<RangedWeapon_MainModule>()) { SingleInventoryPickup.GetComponent<RangedWeapon_MainModule>().DisableWeapon(); }
                if (SingleInventoryPickup.GetComponent<RangedWeapon_SubModule>()) { SingleInventoryPickup.GetComponent<RangedWeapon_SubModule>().DisableWeapon(); }
                if (SingleInventoryPickup.GetComponent<ExWeapon>()) { SingleInventoryPickup.GetComponent<ExWeapon>().DisableWeapon(); }
                if (SingleInventoryPickup.GetComponent<MeleeWeapon>()) { SingleInventoryPickup.GetComponent<MeleeWeapon>().DisableWeapon(); }
            }
            if (DoubleInventoryPickup)
            {
                if (DoubleInventoryPickup.GetComponent<RangedWeapon_MainModule>()) { DoubleInventoryPickup.GetComponent<RangedWeapon_MainModule>().DisableWeapon(); }
                if (DoubleInventoryPickup.GetComponent<RangedWeapon_SubModule>()) { DoubleInventoryPickup.GetComponent<RangedWeapon_SubModule>().DisableWeapon(); }
                if (DoubleInventoryPickup.GetComponent<ExWeapon>()) { DoubleInventoryPickup.GetComponent<ExWeapon>().DisableWeapon(); }
                if (DoubleInventoryPickup.GetComponent<MeleeWeapon>()) { DoubleInventoryPickup.GetComponent<MeleeWeapon>().DisableWeapon(); }
            }
        }
        //---------------------------------------
        public void ShowPlayerName()
        {
            if (!PlayerNameText) { return; }

            string PlayerName;
            if (Utilities.IsValid(VRCPlayerApi.GetPlayerById(playerHitBox.SyncedPlayerID))) { PlayerName = VRCPlayerApi.GetPlayerById(playerHitBox.SyncedPlayerID).displayName; }
            else{ PlayerName = "-"; }

            PlayerNameText.text = PlayerName + "\nID:" + playerHitBox.SyncedPlayerID;
        }
        //---------------------------------------
        public override void OnPlayerLeft(VRCPlayerApi player)
        {  
            leftPlayerID = player.playerId;    //退室したプレイヤーのデータを保存

            //Owner移譲を考慮し、少しディレイを挟んで、プレイヤーがインスタンスに残っているか確認する
            SendCustomEventDelayedSeconds("DelayedSecond_CheckAssignedPlayerIsInInstance", 3.0f);   
        }
        //---------------------------------------
        public void DelayedSecond_CheckAssignedPlayerIsInInstance()
        {
            if(leftPlayerID == playerHitBox.SyncedPlayerID && playerHitBox.SyncedPlayerID != 0 && CheckLocalPlayerIsOwner(this.gameObject))
            {
                playerHitBox.SyncedPlayerID = 0;               
                playerHitBox.Sync();
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_HideOtherPlayerAssignedMarker");
            }
        }
        //---------------------------------------
        public void Network_ShowOtherPlayerAssignedMarker()
        {
            if (!OtherPlayerAssignedMarkerObject) { return; }

            else if (playerHitBox.SyncedPlayerID <= 0)
            {
                OtherPlayerAssignedMarkerObject.SetActive(false);
                return;
            }

            else if (!Utilities.IsValid(VRCPlayerApi.GetPlayerById(playerHitBox.SyncedPlayerID)))
            {
                OtherPlayerAssignedMarkerObject.SetActive(false);
                return;
            }

            if (!Utilities.IsValid(Networking.LocalPlayer)) { return; }

            else if (VRCPlayerApi.GetPlayerById(playerHitBox.SyncedPlayerID) == Networking.LocalPlayer)
            {
                OtherPlayerAssignedMarkerObject.SetActive(false);
                return;
            }

            else
            {
                OtherPlayerAssignedMarkerObject.SetActive(true);
                return;
            }
        }
        //---------------------------------------
        public void Network_HideOtherPlayerAssignedMarker()
        {
            if (OtherPlayerAssignedMarkerObject) { OtherPlayerAssignedMarkerObject.SetActive(false); }
        }
        //---------------------------------------
        public bool CheckLocalPlayerIsOwner(GameObject obj)
        {
            if (!Utilities.IsValid(Networking.LocalPlayer))
            {
                return true;    //ローカルプレイヤーが存在しない場合(CliantSimを使用しない場合)
            }
            else if (Networking.GetOwner(obj) == Networking.LocalPlayer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //---------------------------------------
        public void TrySetOwner(GameObject obj)
        {
            if (Utilities.IsValid(Networking.LocalPlayer) && Networking.GetOwner(obj) != Networking.LocalPlayer)
            {
                Networking.SetOwner(Networking.LocalPlayer, obj);
            }
        }
        //---------------------------------------
        public bool AutoBuild(GameManager _gameManager, PlayerManager _playerManager, Assigner[] _assigners, int HitBoxLayerNum)
        {
            gameManager = _gameManager;
            playerManager = _playerManager;
            Assigners = _assigners;

            if (!playerHitBox)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " に対応するヒットボックスが設定されていません.", this.gameObject);

                return false;
            }

            for(int i = 0; i < Assigners.Length; ++i)
            {
                if (Assigners[i] == this.GetComponent<Assigner>())
                {
                    playerHitBox.PlayerHitBoxIndex = i;
                }
            }

            playerHitBox.SetAssigner(this.GetComponent<Assigner>());
            playerHitBox.HidePlayerHitBoxPos = _gameManager.HidePlayerHitBoxPos;
            playerHitBox.playerManager = _playerManager;
            playerHitBox.teamName = teamName;
            playerHitBox.gameManager = _gameManager;
            playerHitBox.GlobalJumpAndGroundedEffect = playerManager.GlobalJumpAndGroundedEffect;
            playerHitBox.gameObject.layer = HitBoxLayerNum;

            if (gameManager.SetMaxHitPointManually)
            {
                if (playerHitBox.MaxPlayerHitPoint <= 0)
                {
                    Debug.Log("<color=#ffa500>Warning:</color> " + playerHitBox.gameObject.name + " の最大HPが0以下です.手動で設定して下さい.", playerHitBox.gameObject);
                }
            }
            else
            {
                playerHitBox.MaxPlayerHitPoint = gameManager.MaxPlayerHitPoint; //MaxPlayerHitPoint > 0はGameManager.AutoBuildで確認済み
            }

            return true;
        }
        //---------------------------------------
    }
}

