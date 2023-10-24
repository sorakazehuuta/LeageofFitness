
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class Flag : UdonSharpBehaviour
    {
        //---------------------------------------
        [Tooltip("FlagCarrierモード以外の場合, オブジェクトをこの座標に隠す")] [SerializeField] Vector3 HideFlagPosition;
        //---------------------------------------
        [Header("その他(任意項目.無しでも動作可能)")]
        [Tooltip("フラッグ所持時に表示されるマーカー")] [SerializeField] GameObject CarryingFlagMarker;
        [Tooltip("フラッグを拾った時に発動する効果音")] [SerializeField] AudioClip TakeFlagClip;
        [Tooltip("フラッグを持った時の速度倍率")] [SerializeField, Range(0.1f, 1.0f)] float FlagCarrierSpeedMultiplier = 0.7f;
        //---------------------------------------
        [UdonSynced] int SyncedSpawnPointID;
        Collider colider;
        const float FlagSpawnDelay = 3;
        AudioSource audioSource;
        //---------------------------------------
        //[Header("手動で変更しないこと")]
        [HideInInspector] [Tooltip("フラッグを持っているプレイヤーのPlayerHitBox")] public PlayerHitBox FlagCarrierPlayerHitBox;
        [HideInInspector] [Tooltip("フラッグを持っているプレイヤーのPlayerID")] [UdonSynced] public int SyncedFlagCarrierPlayerID;      //独自定義の型(PlayerHitBox)は同期できないので、Intで表現
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("Transform[]")] public Transform[] FlagSpawnPoints;
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        [HideInInspector] [Tooltip("ScoreManager")] public ScoreManager scoreManager;
        [HideInInspector] [Tooltip("Assigner[]")] public Assigner[] Assigners;
        //---------------------------------------
        void Start()
        {
            audioSource = this.GetComponent<AudioSource>();
            colider = this.GetComponent<Collider>();
            colider.isTrigger = true;

            HideCarryingFlagMarker();
        }
        //---------------------------------------
        private void Update()
        {
            if (!gameManager.SyncedInBattle || !gameManager.SyncedInGame) { this.transform.position = HideFlagPosition; }

            //フラッグを持っているプレイヤーのPlayerHitBoxに追従させる(持っているプレイヤーがいない場合はその場に置いたままにする)
            else if (gameManager.gameEndCondition == GameEndCondition.FlagCarrier)
            {
                Vector3 offset = new Vector3(0, 0, -0.75f);
                if (FlagCarrierPlayerHitBox) { this.transform.position = FlagCarrierPlayerHitBox.transform.position + Quaternion.Euler(0, FlagCarrierPlayerHitBox.transform.eulerAngles.y, 0) * offset; }
            }

            else
            {
                this.transform.position = HideFlagPosition;
            }
        }
        //---------------------------------------
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (!playerManager.playerHitBox) { return; }//自分がゲームに登録していない
            if (!gameManager.SyncedInBattle) { return; }//戦闘開始前
            if (!playerManager.isAlive) { return; }//ダウン状態
            if (playerManager.myFlag) { return; }//既にフラッグを持っている
            if (!Utilities.IsValid(Networking.LocalPlayer)) { return; }//プレイヤーデータが利用できない
            if (!player.isLocal) { return; }//既にフラッグを持っている
            if (SyncedFlagCarrierPlayerID > 0) { return; }  //他の人がフラッグを持っている

            TakeFlag(); 
        }
        //---------------------------------------
        public void TakeFlag()
        {
            if (!playerManager.playerHitBox) { return; }

            TrySetOwner(this.gameObject);
            SyncedFlagCarrierPlayerID = playerManager.playerHitBox.SyncedPlayerID;
            Sync();

            if (Utilities.IsValid(Networking.LocalPlayer))
            {
                Networking.LocalPlayer.SetRunSpeed(playerManager.defaultRunSpeed * FlagCarrierSpeedMultiplier);
                Networking.LocalPlayer.SetWalkSpeed(playerManager.defaultWalkSpeed * FlagCarrierSpeedMultiplier);
                Networking.LocalPlayer.SetStrafeSpeed(playerManager.defaultStrafeSpeed * FlagCarrierSpeedMultiplier);
                Networking.LocalPlayer.SetJumpImpulse(playerManager.defaultJumpImpulse * FlagCarrierSpeedMultiplier);
            }
            ShowCarryingFlagMarker();
            if (TakeFlagClip) { audioSource.PlayOneShot(TakeFlagClip); }
        }
        //---------------------------------------
        public void ShowCarryingFlagMarker()
        {
            if (CarryingFlagMarker) { CarryingFlagMarker.SetActive(true); }
        }
        //---------------------------------------
        public void HideCarryingFlagMarker()
        {
            if (CarryingFlagMarker) { CarryingFlagMarker.SetActive(false); }
        }
        //---------------------------------------
        public void DropFlag()
        {
            playerManager.myFlag = null;

            TrySetOwner(this.gameObject);
            SyncedFlagCarrierPlayerID = 0;
            Sync();

            HideCarryingFlagMarker();
        }
        //---------------------------------------
        public void SetRandomSpawnPointNumber()
        {
            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                SyncedSpawnPointID = Random.Range(0, FlagSpawnPoints.Length);
                SyncedFlagCarrierPlayerID = 0;
                Sync();

                SendCustomEventDelayedSeconds("DelayedSecond_TryRespawnFlag", FlagSpawnDelay);  //同期変数で次のスポーン位置を更新してから、一定時間後にフラッグをリスポーン             
            }
        }
        //---------------------------------------
        public void DelayedSecond_TryRespawnFlag()
        {
            if (gameManager.SyncedInBattle)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_RespawnFlag");
            }
            else
            {
                //戦闘終了していた場合
                SyncedFlagCarrierPlayerID = 0;
                Sync();
            }
        }
        //---------------------------------------
        public void Network_RespawnFlag()
        {
            RespawnFlag();
        }
        //---------------------------------------
        public void RespawnFlag()
        {
            colider.enabled = true;
            this.transform.SetPositionAndRotation(FlagSpawnPoints[SyncedSpawnPointID].position, FlagSpawnPoints[SyncedSpawnPointID].rotation);
        }
        //---------------------------------------
        public void OnTriggerEnter(Collider other)
        {
            if (!Utilities.IsValid(other)) { return; }
            if (!other.GetComponent<FlagGoal>()) { return; }
            if (!playerManager.playerHitBox) { return; }

            if (playerManager.playerHitBox.teamName == other.GetComponent<FlagGoal>().teamName) { ReachedGoal(playerManager.playerHitBox.teamName); }
        }
        //---------------------------------------
        public void ReachedGoal(TeamName _team)
        {
            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (_team == TeamName.A) { scoreManager.AddFlagScore(TeamName.A); }
                if (_team == TeamName.B) { scoreManager.AddFlagScore(TeamName.B); }
                if (_team == TeamName.C) { scoreManager.AddFlagScore(TeamName.C); }
                if (_team == TeamName.D) { scoreManager.AddFlagScore(TeamName.D); }

                SetRandomSpawnPointNumber();    //次のスポーンポイントを設定する
            }

            if (Utilities.IsValid(Networking.LocalPlayer))
            {
                Networking.LocalPlayer.SetRunSpeed(playerManager.defaultRunSpeed);
                Networking.LocalPlayer.SetWalkSpeed(playerManager.defaultWalkSpeed);
                Networking.LocalPlayer.SetStrafeSpeed(playerManager.defaultStrafeSpeed);
                Networking.LocalPlayer.SetJumpImpulse(playerManager.defaultJumpImpulse);
            }

            HideCarryingFlagMarker();

            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_PlayReachedGoalEffect");
        }
        //---------------------------------------
        public void Network_PlayReachedGoalEffect()
        {
            colider.enabled = false;
            if(playerManager.myFlag == this.GetComponent<Flag>()) { playerManager.myFlag = null; }
        }
        //---------------------------------------
        public void Sync()
        {
            RequestSerialization();
            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                OnDeserialization();
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
        public override void OnDeserialization()
        {
            //誰も拾っていない場合
            if (SyncedFlagCarrierPlayerID <= 0)
            {
                colider.enabled = true;
                FlagCarrierPlayerHitBox = null;
                return;
            }

            bool isCarried = false;
            for (int i = 0; i < Assigners.Length; ++i)
            {
                if (Assigners[i].playerHitBox.SyncedPlayerID == SyncedFlagCarrierPlayerID)
                {
                    FlagCarrierPlayerHitBox = Assigners[i].playerHitBox;
                    isCarried = true;
                }
            }

            //誰も拾っていない場合(同期タイミングによるエラー回避)
            if (!isCarried)
            {
                colider.enabled = true;
                FlagCarrierPlayerHitBox = null;
                return;
            }

            if (Utilities.IsValid(Networking.LocalPlayer))
            {
                if (Networking.LocalPlayer.playerId == SyncedFlagCarrierPlayerID)
                {
                    playerManager.myFlag = this.GetComponent<Flag>();   //自分がフラッグを拾った場合
                    colider.enabled = true;
                }
                else if (Networking.LocalPlayer.playerId != SyncedFlagCarrierPlayerID && playerManager.myFlag == this.GetComponent<Flag>())
                {
                    playerManager.myFlag = null;    //自分が既に持っているフラッグを他の人が拾った場合(同期ラグ対策)
                    colider.enabled = false;
                }
            }
        }
        //---------------------------------------
        public bool AutoBuild(GameManager _gameManager, PlayerManager _playerManager, Assigner[] _assigners, ScoreManager _scoreManager)
        {
            gameManager = _gameManager;
            playerManager = _playerManager;
            Assigners = _assigners;
            FlagSpawnPoints = _gameManager.FlagSpawnPoints;
            scoreManager = _scoreManager;

            return true;
        }
        //---------------------------------------
    }
}

