
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SpectatorCamera : UdonSharpBehaviour
    {        
        //---------------------------------------
        [Header("必須項目.無いとエラー")]
        [Tooltip("追従対象がいない場合の座標")] public Camera camera;
        [Tooltip("追従対象がいない場合の座標")] public Vector3 defaultCameraPos;
        [Tooltip("追従速度")] [SerializeField] float lerpValue;
        [Tooltip("表示先のオブジェクト")] public GameObject ScreenObject;
        //---------------------------------------
        [Header("任意項目(その他)")]
        [Tooltip("プレイヤー名を表示するテキスト")] public Text PlayerNameText;
        [Tooltip("チーム名を表示するテキスト")] public Text PlayerTeamText;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("PlayerHitBox[]")] public PlayerHitBox[] playerHitBoxes;
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        //---------------------------------------
        int CurrentTargetPlayerHitBoxID;
        int playerID;
        VRCPlayerApi player;
        //---------------------------------------
        void Start()
        {
            CurrentTargetPlayerHitBoxID = -1;
            ScreenObject.SetActive(false);
            camera.gameObject.SetActive(false);
        }
        //---------------------------------------
        public void UseSpectatorCamera()
        {
            if (!camera.gameObject.activeSelf)
            {
                SelectNextPlayer();
                camera.gameObject.SetActive(true);
                ScreenObject.SetActive(true);
                return;
            }

            camera.gameObject.SetActive(false);
            ScreenObject.SetActive(false);
            return;
        }
        //---------------------------------------
        private void Update()
        {
            if (0 <= CurrentTargetPlayerHitBoxID && CurrentTargetPlayerHitBoxID < playerHitBoxes.Length)
            {
                if (playerHitBoxes[CurrentTargetPlayerHitBoxID].SyncedReviveTicket > 0)
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, playerHitBoxes[CurrentTargetPlayerHitBoxID].transform.position, lerpValue);
                    camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, Quaternion.LookRotation(playerHitBoxes[CurrentTargetPlayerHitBoxID].transform.position - camera.transform.position, Vector3.up),lerpValue);
                    return;
                }

                SelectNextPlayer();
                return;
            }

            this.transform.position = defaultCameraPos;
            camera.transform.localRotation = Quaternion.identity;
            this.transform.localEulerAngles = Vector3.zero;
            return;
        }
        //---------------------------------------
        public void GetPlayerByPlayerHitBoxID(int hitBoxID)
        {
            if (CurrentTargetPlayerHitBoxID < 0 || playerHitBoxes.Length <= CurrentTargetPlayerHitBoxID) { player = null; return; }

            int playerID = playerHitBoxes[CurrentTargetPlayerHitBoxID].SyncedPlayerID;
            player = VRCPlayerApi.GetPlayerById(playerID);
            
            if (!Utilities.IsValid(player)) { player = null; return; }
        }
        //---------------------------------------
        public void ShowPlayerName(VRCPlayerApi p)
        {
            if (!PlayerTeamText) { return; }
            if (!Utilities.IsValid(p)) { return; }

            PlayerNameText.text = p.displayName;
        }
        //---------------------------------------
        public void ShowPlayerTeam(int hitBoxID)
        {
            if (!PlayerTeamText) { return; }
            if (gameManager.teamMode != TeamMode.TeamBattle) { PlayerTeamText.text = ""; return; }
            if (hitBoxID < 0 || playerHitBoxes.Length <= hitBoxID) { return; }
            
            TeamName _teamName = playerHitBoxes[hitBoxID].teamName;
            if (_teamName == TeamName.A) { PlayerTeamText.text = gameManager.TeamA_CustomName; return; }
            if (_teamName == TeamName.B) { PlayerTeamText.text = gameManager.TeamB_CustomName; return; }
            if (_teamName == TeamName.C) { PlayerTeamText.text = gameManager.TeamC_CustomName; return; }
            if (_teamName == TeamName.D) { PlayerTeamText.text = gameManager.TeamD_CustomName; return; }          
        }
        //---------------------------------------
        public void SelectNextPlayer()
        {
            CurrentTargetPlayerHitBoxID = GetNextPlayerHitBoxID(CurrentTargetPlayerHitBoxID);
            GetPlayerByPlayerHitBoxID(CurrentTargetPlayerHitBoxID);
            ShowPlayerName(player);
            ShowPlayerTeam(CurrentTargetPlayerHitBoxID);
        }
        //---------------------------------------
        public void SelectPreviousPlayer()
        {
            CurrentTargetPlayerHitBoxID = GetPreviousPlayerHitBoxID(CurrentTargetPlayerHitBoxID);
            GetPlayerByPlayerHitBoxID(CurrentTargetPlayerHitBoxID);
            ShowPlayerName(player);
            ShowPlayerTeam(CurrentTargetPlayerHitBoxID);
        }
        //---------------------------------------
        public int GetNextPlayerHitBoxID(int startPlayerHitBoxID)
        {
            for(int i = startPlayerHitBoxID + 1; i < playerHitBoxes.Length; ++i)
            {
                if(playerHitBoxes[i].SyncedPlayerID > 0 && playerHitBoxes[i].SyncedReviveTicket > 0) { return i; }
            }

            for (int i = 0; i <= startPlayerHitBoxID; ++i)
            {
                if (i >= 0 && playerHitBoxes[i].SyncedPlayerID > 0 && playerHitBoxes[i].SyncedReviveTicket > 0) { return i; }
            }

            return -1;
        }
        //---------------------------------------
        public int GetPreviousPlayerHitBoxID(int startPlayerHitBoxID)
        {
            for (int i = startPlayerHitBoxID - 1; i >= 0; --i)
            {
                if (playerHitBoxes[i].SyncedPlayerID > 0 && playerHitBoxes[i].SyncedReviveTicket > 0) { return i; }
            }

            for (int i = playerHitBoxes.Length - 1; i >= startPlayerHitBoxID; --i)
            {
                if (i >= 0 && playerHitBoxes[i].SyncedPlayerID > 0 && playerHitBoxes[i].SyncedReviveTicket > 0) { return i; }
            }

            return -1;
        }
        //---------------------------------------
        public bool AutoBuild(GameManager _gameManager, Assigner[] _assigners)
        {
            gameManager = _gameManager;

            playerHitBoxes = new PlayerHitBox[_assigners.Length];
            for (int i = 0; i < playerHitBoxes.Length; ++i) { playerHitBoxes[i] = _assigners[i].playerHitBox; }

            return true;
        }
        //---------------------------------------
    }
}

