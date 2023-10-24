
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using System.Linq;

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class ScoreManager : UdonSharpBehaviour
    {
        //---------------------------------------
        [Header("必須項目.無いとエラー")]
        [Tooltip("ゲーム開始時に自動で非表示になる")] [SerializeField] Text DummyPlayerNameText;
        [Tooltip("ゲーム開始時に自動で非表示になる")] [SerializeField] Text DummyScoreText;
        [Tooltip("テキスト同士の間隔(ローカル座標)")] [SerializeField] Vector2 TextSpace;
        //---------------------------------------
        [Header("任意項目.無しでも動作可能")]
        [Tooltip("プレイヤー撃破時のスコア")] public float PlayerKillScore;
        [Tooltip("自滅した際のスコアペナルティ")] public float SelfKillPenaltyScore;
        //---------------------------------------
        [Header("スコア")]
        [Tooltip("TeamAのスコア")] public float TeamA_ScoreSum;
        [Tooltip("TeamBのスコア")] public float TeamB_ScoreSum;
        [Tooltip("TeamCのスコア")] public float TeamC_ScoreSum;
        [Tooltip("TeamDのスコア")] public float TeamD_ScoreSum;
        //---------------------------------------
        [Header("占領時間(コンクエストモード用)")]
        [Tooltip("TeamAのスコア")] public float TeamA_ConquestScoreSum;
        [Tooltip("TeamBのスコア")] public float TeamB_ConquestScoreSum;
        [Tooltip("TeamCのスコア")] public float TeamC_ConquestScoreSum;
        [Tooltip("TeamDのスコア")] public float TeamD_ConquestScoreSum;
        //---------------------------------------
        [Header("AutoBuild時に自動で設定")]
        [Tooltip("TeamAのヒットボックス")] public PlayerHitBox[] PlayerHitBox_Team_A;
        [Tooltip("TeamBのヒットボックス")] public PlayerHitBox[] PlayerHitBox_Team_B;
        [Tooltip("TeamCのヒットボックス")] public PlayerHitBox[] PlayerHitBox_Team_C;
        [Tooltip("TeamDのヒットボックス")] public PlayerHitBox[] PlayerHitBox_Team_D;
        //---------------------------------------
        [Tooltip("TeamAのヘッダー")] public Text TeamA_HeaderText;
        [Tooltip("TeamBのヘッダー")] public Text TeamB_HeaderText;
        [Tooltip("TeamCのヘッダー")] public Text TeamC_HeaderText;
        [Tooltip("TeamDのヘッダー")] public Text TeamD_HeaderText;
        //---------------------------------------
        [Tooltip("TeamAの合計スコア")] public Text TeamA_ScoreSumText;
        [Tooltip("TeamBの合計スコア")] public Text TeamB_ScoreSumText;
        [Tooltip("TeamCの合計スコア")] public Text TeamC_ScoreSumText;
        [Tooltip("TeamDの合計スコア")] public Text TeamD_ScoreSumText;
        //---------------------------------------
        [Tooltip("TeamAの個人スコア")] public Text[] TeamA_ScoreText;
        [Tooltip("TeamBの個人スコア")] public Text[] TeamB_ScoreText;
        [Tooltip("TeamCの個人スコア")] public Text[] TeamC_ScoreText;
        [Tooltip("TeamDの個人スコア")] public Text[] TeamD_ScoreText;
        //---------------------------------------
        [Tooltip("TeamAのプレイヤー名")] public Text[] TeamA_PlayerNameText;
        [Tooltip("TeamBのプレイヤー名")] public Text[] TeamB_PlayerNameText;
        [Tooltip("TeamCのプレイヤー名")] public Text[] TeamC_PlayerNameText;
        [Tooltip("TeamDのプレイヤー名")] public Text[] TeamD_PlayerNameText;
        //---------------------------------------
        [Tooltip("TeamAのフラッグ納品数")] [UdonSynced] public int TeamA_FlagScore;
        [Tooltip("TeamBのフラッグ納品数")] [UdonSynced] public int TeamB_FlagScore;
        [Tooltip("TeamCのフラッグ納品数")] [UdonSynced] public int TeamC_FlagScore;
        [Tooltip("TeamDのフラッグ納品数")] [UdonSynced] public int TeamD_FlagScore;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        [HideInInspector] [Tooltip("すべてのAssigner")] public Assigner[] Assigners;
        //---------------------------------------
        void Start()
        {
            DummyPlayerNameText.gameObject.SetActive(false);
            DummyScoreText.gameObject.SetActive(false);
        }
        //---------------------------------------
        public void ResetScore()
        {
            //PlayerHitBoxが持っているスコアをリセット
            for (int i = 0; i < Assigners.Length; ++i)
            {
                if (Assigners[i]) { Assigners[i].playerHitBox.ResetScore(); }
            }
        }
        //---------------------------------------
        public void CalculateScore()
        {
            if (PlayerHitBox_Team_A.Length > 0)
            {
                TeamA_ScoreSum = 0;
                for (int i = 0; i < PlayerHitBox_Team_A.Length; ++i)
                {
                    if (Utilities.IsValid(VRCPlayerApi.GetPlayerById(PlayerHitBox_Team_A[i].SyncedPlayerID))) { TeamA_PlayerNameText[i].text = VRCPlayerApi.GetPlayerById(PlayerHitBox_Team_A[i].SyncedPlayerID).displayName; }
                    else { TeamA_PlayerNameText[i].text = "N/A"; }
                    TeamA_ScoreText[i].text = PlayerHitBox_Team_A[i].SyncedScore.ToString("F1");
                    TeamA_ScoreSum += PlayerHitBox_Team_A[i].SyncedScore;
                }
                if (TeamA_ScoreSumText) { TeamA_ScoreSumText.text = TeamA_ScoreSum.ToString("F1"); }
            }

            if (PlayerHitBox_Team_B.Length > 0)
            {
                TeamB_ScoreSum = 0;
                for (int i = 0; i < PlayerHitBox_Team_B.Length; ++i)
                {
                    if (Utilities.IsValid(VRCPlayerApi.GetPlayerById(PlayerHitBox_Team_B[i].SyncedPlayerID))) { TeamB_PlayerNameText[i].text = VRCPlayerApi.GetPlayerById(PlayerHitBox_Team_B[i].SyncedPlayerID).displayName; }
                    else { TeamB_PlayerNameText[i].text = "N/A"; }
                    TeamB_ScoreText[i].text = PlayerHitBox_Team_B[i].SyncedScore.ToString("F1");
                    TeamB_ScoreSum += PlayerHitBox_Team_B[i].SyncedScore;
                }
                if (TeamB_ScoreSumText) { TeamB_ScoreSumText.text = TeamB_ScoreSum.ToString("F1"); }
            }

            if (PlayerHitBox_Team_C.Length > 0)
            {
                TeamC_ScoreSum = 0;
                for (int i = 0; i < PlayerHitBox_Team_C.Length; ++i)
                {
                    if (Utilities.IsValid(VRCPlayerApi.GetPlayerById(PlayerHitBox_Team_C[i].SyncedPlayerID))) { TeamC_PlayerNameText[i].text = VRCPlayerApi.GetPlayerById(PlayerHitBox_Team_C[i].SyncedPlayerID).displayName; }
                    else { TeamC_PlayerNameText[i].text = "N/A"; }
                    TeamC_ScoreText[i].text = PlayerHitBox_Team_C[i].SyncedScore.ToString("F1");
                    TeamC_ScoreSum += PlayerHitBox_Team_C[i].SyncedScore;
                }
                if (TeamC_ScoreSumText) { TeamC_ScoreSumText.text = TeamC_ScoreSum.ToString("F1"); }
            }

            if (PlayerHitBox_Team_D.Length > 0)
            {
                TeamD_ScoreSum = 0;
                for (int i = 0; i < PlayerHitBox_Team_D.Length; ++i)
                {
                    if (Utilities.IsValid(VRCPlayerApi.GetPlayerById(PlayerHitBox_Team_D[i].SyncedPlayerID))) { TeamD_PlayerNameText[i].text = VRCPlayerApi.GetPlayerById(PlayerHitBox_Team_D[i].SyncedPlayerID).displayName; }
                    else { TeamD_PlayerNameText[i].text = "N/A"; }
                    TeamD_ScoreText[i].text = PlayerHitBox_Team_D[i].SyncedScore.ToString("F1");
                    TeamD_ScoreSum += PlayerHitBox_Team_D[i].SyncedScore;
                }
                if (TeamD_ScoreSumText) { TeamD_ScoreSumText.text = TeamD_ScoreSum.ToString("F1"); }
            }
        }
        //---------------------------------------
        public bool AutoBuild(Assigner[] _assigners, GameManager _gameManager)
        {
            gameManager = _gameManager;
            _gameManager.scoreManager = this.GetComponent<ScoreManager>();

            Assigners = _assigners;

            if (!DummyPlayerNameText)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " DummyPlayerNameTextがありません.", this.gameObject);
            }
            if (!DummyScoreText)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " DummyScoreTextがありません.", this.gameObject);
            }

            int AteamNumber = 0;
            int BteamNumber = 0;
            int CteamNumber = 0;
            int DteamNumber = 0;

            for (int i = 0; i < _assigners.Length; ++i)
            {
                if (_assigners[i] && _assigners[i].teamName == TeamName.A) { ++AteamNumber; }
                else if (_assigners[i] && _assigners[i].teamName == TeamName.B) { ++BteamNumber; }
                else if (_assigners[i] && _assigners[i].teamName == TeamName.C) { ++CteamNumber; }
                else if (_assigners[i] && _assigners[i].teamName == TeamName.D) { ++DteamNumber; }

                if (_assigners[i]) { _assigners[i].playerHitBox.scoreManager = this.GetComponent<ScoreManager>(); }
            }

            PlayerHitBox_Team_A = new PlayerHitBox[AteamNumber];
            PlayerHitBox_Team_B = new PlayerHitBox[BteamNumber];
            PlayerHitBox_Team_C = new PlayerHitBox[CteamNumber];
            PlayerHitBox_Team_D = new PlayerHitBox[DteamNumber];

            int num;

            num = 0;    //Aチーム初期化
            for (int i = 0; i < _assigners.Length; ++i)
            {
                if (_assigners[i] && _assigners[i].teamName == TeamName.A)
                {
                    PlayerHitBox_Team_A[num] = _assigners[i].playerHitBox;
                    ++num;
                }
            }

            num = 0;    //Bチーム初期化
            for (int i = 0; i < _assigners.Length; ++i)
            {
                if (_assigners[i] && _assigners[i].teamName == TeamName.B)
                {
                    PlayerHitBox_Team_B[num] = _assigners[i].playerHitBox;
                    ++num;
                }
            }

            num = 0;    //Cチーム初期化
            for (int i = 0; i < _assigners.Length; ++i)
            {
                if (_assigners[i] && _assigners[i].teamName == TeamName.C)
                {
                    PlayerHitBox_Team_C[num] = _assigners[i].playerHitBox;
                    ++num;
                }
            }

            num = 0;    //Dチーム初期化
            for (int i = 0; i < _assigners.Length; ++i)
            {
                if (_assigners[i] && _assigners[i].teamName == TeamName.D)
                {
                    PlayerHitBox_Team_D[num] = _assigners[i].playerHitBox;
                    ++num;
                }
            }

            //各PlayerHitBoxにScoreManagerを設定
            for (int i = 0; i < _assigners.Length; ++i)
            {
                if (!_assigners[i]) { _assigners[i].playerHitBox.scoreManager = this.GetComponent<ScoreManager>(); }
            }

            //スコア表示用のテキストをまだ作成していない場合は、シーン内に設置されたオブジェクト数をもとにTextを生成
            Transform canvasTransform = DummyPlayerNameText.transform.parent;
            bool initialized = false;

            if (canvasTransform.childCount > 2)
            {
                initialized = true; //生成していない時は、Child(0)とChild(1)しかない
            }

            //スコア表示用のテキストを新しく作成
            if (!initialized)
            {
                TeamA_ScoreText = new Text[AteamNumber];
                TeamB_ScoreText = new Text[BteamNumber];
                TeamC_ScoreText = new Text[CteamNumber];
                TeamD_ScoreText = new Text[DteamNumber];

                TeamA_PlayerNameText = new Text[AteamNumber];
                TeamB_PlayerNameText = new Text[BteamNumber];
                TeamC_PlayerNameText = new Text[CteamNumber];
                TeamD_PlayerNameText = new Text[DteamNumber];

                Vector3 DummyPlayerNameTextPosition = DummyPlayerNameText.transform.localPosition;
                Vector3 DummyScoreTextPosition = DummyScoreText.transform.localPosition;

                //TeamA
                if (AteamNumber > 0)
                {
                    GameObject TeamAHeaderTextObj = Instantiate(DummyPlayerNameText.gameObject);
                    TeamAHeaderTextObj.SetActive(true);
                    TeamAHeaderTextObj.transform.parent = canvasTransform;
                    TeamAHeaderTextObj.transform.localPosition = DummyPlayerNameTextPosition + new Vector3(0, 0, 0);
                    TeamAHeaderTextObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                    TeamAHeaderTextObj.name = "TeamAHeader";
                    TeamA_HeaderText = TeamAHeaderTextObj.GetComponent<Text>();
                    TeamA_HeaderText.text = "TeamA";

                    GameObject TeamAScoreTextObj = Instantiate(DummyScoreText.gameObject);
                    TeamAScoreTextObj.SetActive(true);
                    TeamAScoreTextObj.transform.parent = canvasTransform;
                    TeamAScoreTextObj.transform.localPosition = DummyScoreTextPosition + new Vector3(0, 0, 0);
                    TeamAScoreTextObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                    TeamAScoreTextObj.name = "TeamAScore";
                    TeamA_ScoreSumText = TeamAScoreTextObj.GetComponent<Text>();
                    TeamA_ScoreSumText.text = "0";

                    for (int i = 0; i < AteamNumber; ++i)
                    {
                        GameObject newPlayerNameObj = Instantiate(DummyPlayerNameText.gameObject);
                        newPlayerNameObj.SetActive(true);
                        newPlayerNameObj.transform.parent = canvasTransform;
                        newPlayerNameObj.transform.localPosition = DummyPlayerNameTextPosition + new Vector3(0, -TextSpace.x * (i + 1), 0);
                        newPlayerNameObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                        newPlayerNameObj.name = "AteamPlayer (" + i + ")";
                        TeamA_PlayerNameText[i] = newPlayerNameObj.GetComponent<Text>();
                        TeamA_PlayerNameText[i].text = "N/A";

                        GameObject newScoreObj = Instantiate(DummyScoreText.gameObject);
                        newScoreObj.SetActive(true);
                        newScoreObj.transform.parent = canvasTransform;
                        newScoreObj.transform.localPosition = DummyScoreTextPosition + new Vector3(0, -TextSpace.x * (i + 1), 0);
                        newScoreObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                        newScoreObj.name = "AteamScore (" + i + ")";
                        TeamA_ScoreText[i] = newScoreObj.GetComponent<Text>();
                        TeamA_ScoreText[i].text = "0";
                    }
                }

                //TeamB
                if (BteamNumber > 0)
                {
                    GameObject TeamBHeaderTextObj = Instantiate(DummyPlayerNameText.gameObject);
                    TeamBHeaderTextObj.SetActive(true);
                    TeamBHeaderTextObj.transform.parent = canvasTransform;
                    TeamBHeaderTextObj.transform.localPosition = DummyPlayerNameTextPosition + new Vector3(-TextSpace.y, 0, 0);
                    TeamBHeaderTextObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                    TeamBHeaderTextObj.name = "TeamBHeader";
                    TeamB_HeaderText = TeamBHeaderTextObj.GetComponent<Text>();
                    TeamB_HeaderText.text = "TeamB";

                    GameObject TeamBScoreTextObj = Instantiate(DummyScoreText.gameObject);
                    TeamBScoreTextObj.SetActive(true);
                    TeamBScoreTextObj.transform.parent = canvasTransform;
                    TeamBScoreTextObj.transform.localPosition = DummyScoreTextPosition + new Vector3(-TextSpace.y, 0, 0);
                    TeamBScoreTextObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                    TeamBScoreTextObj.name = "TeamBScore";
                    TeamB_ScoreSumText = TeamBScoreTextObj.GetComponent<Text>();
                    TeamB_ScoreSumText.text = "0";

                    for (int i = 0; i < BteamNumber; ++i)
                    {
                        GameObject newPlayerNameObj = Instantiate(DummyPlayerNameText.gameObject);
                        newPlayerNameObj.SetActive(true);
                        newPlayerNameObj.transform.parent = canvasTransform;
                        newPlayerNameObj.transform.localPosition = DummyPlayerNameTextPosition + new Vector3(-TextSpace.y, -TextSpace.x * (i + 1), 0);
                        newPlayerNameObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                        newPlayerNameObj.name = "BteamPlayer (" + i + ")";
                        TeamB_PlayerNameText[i] = newPlayerNameObj.GetComponent<Text>();
                        TeamB_PlayerNameText[i].text = "N/A";

                        GameObject newScoreObj = Instantiate(DummyScoreText.gameObject);
                        newScoreObj.SetActive(true);
                        newScoreObj.transform.parent = canvasTransform;
                        newScoreObj.transform.localPosition = DummyScoreTextPosition + new Vector3(-TextSpace.y, -TextSpace.x * (i + 1), 0);
                        newScoreObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                        newScoreObj.name = "BteamScore (" + i + ")";
                        TeamB_ScoreText[i] = newScoreObj.GetComponent<Text>();
                        TeamB_ScoreText[i].text = "0";
                    }
                }

                //TeamC
                if (CteamNumber > 0)
                {
                    GameObject TeamCHeaderTextObj = Instantiate(DummyPlayerNameText.gameObject);
                    TeamCHeaderTextObj.SetActive(true);
                    TeamCHeaderTextObj.transform.parent = canvasTransform;
                    TeamCHeaderTextObj.transform.localPosition = DummyPlayerNameTextPosition + new Vector3(-TextSpace.y * 2, 0, 0);
                    TeamCHeaderTextObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                    TeamCHeaderTextObj.name = "TeamCHeader";
                    TeamC_HeaderText = TeamCHeaderTextObj.GetComponent<Text>();
                    TeamA_HeaderText.text = "TeamC";

                    GameObject TeamCScoreTextObj = Instantiate(DummyScoreText.gameObject);
                    TeamCScoreTextObj.SetActive(true);
                    TeamCScoreTextObj.transform.parent = canvasTransform;
                    TeamCScoreTextObj.transform.localPosition = DummyScoreTextPosition + new Vector3(-TextSpace.y * 2, 0, 0);
                    TeamCScoreTextObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                    TeamCScoreTextObj.name = "TeamCScore";
                    TeamC_ScoreSumText = TeamCScoreTextObj.GetComponent<Text>();
                    TeamC_ScoreSumText.text = "0";

                    for (int i = 0; i < CteamNumber; ++i)
                    {
                        GameObject newPlayerNameObj = Instantiate(DummyPlayerNameText.gameObject);
                        newPlayerNameObj.SetActive(true);
                        newPlayerNameObj.transform.parent = canvasTransform;
                        newPlayerNameObj.transform.localPosition = DummyPlayerNameTextPosition + new Vector3(-TextSpace.y * 2, -TextSpace.x * (i + 1), 0);
                        newPlayerNameObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                        newPlayerNameObj.name = "CteamPlayer (" + i + ")";
                        TeamC_PlayerNameText[i] = newPlayerNameObj.GetComponent<Text>();
                        TeamC_PlayerNameText[i].text = "N/A";

                        GameObject newScoreObj = Instantiate(DummyScoreText.gameObject);
                        newScoreObj.SetActive(true);
                        newScoreObj.transform.parent = canvasTransform;
                        newScoreObj.transform.localPosition = DummyScoreTextPosition + new Vector3(-TextSpace.y * 2, -TextSpace.x * (i + 1), 0);
                        newScoreObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                        newScoreObj.name = "CteamScore (" + i + ")";
                        TeamC_ScoreText[i] = newScoreObj.GetComponent<Text>();
                        TeamC_ScoreText[i].text = "0";
                    }
                }

                //TeamD
                if (DteamNumber > 0)
                {
                    GameObject TeamDHeaderTextObj = Instantiate(DummyPlayerNameText.gameObject);
                    TeamDHeaderTextObj.SetActive(true);
                    TeamDHeaderTextObj.transform.parent = canvasTransform;
                    TeamDHeaderTextObj.transform.localPosition = DummyPlayerNameTextPosition + new Vector3(-TextSpace.y * 3, 0, 0);
                    TeamDHeaderTextObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                    TeamDHeaderTextObj.name = "TeamDHeader";
                    TeamD_HeaderText = TeamDHeaderTextObj.GetComponent<Text>();
                    TeamD_HeaderText.text = "TeamD";

                    GameObject TeamDScoreTextObj = Instantiate(DummyScoreText.gameObject);
                    TeamDScoreTextObj.SetActive(true);
                    TeamDScoreTextObj.transform.parent = canvasTransform;
                    TeamDScoreTextObj.transform.localPosition = DummyScoreTextPosition + new Vector3(-TextSpace.y * 3, 0, 0);
                    TeamDScoreTextObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                    TeamDScoreTextObj.name = "TeamDScore";
                    TeamD_ScoreSumText = TeamDScoreTextObj.GetComponent<Text>();
                    TeamD_ScoreSumText.text = "0";

                    for (int i = 0; i < DteamNumber; ++i)
                    {
                        GameObject newPlayerNameObj = Instantiate(DummyPlayerNameText.gameObject);
                        newPlayerNameObj.SetActive(true);
                        newPlayerNameObj.transform.parent = canvasTransform;
                        newPlayerNameObj.transform.localPosition = DummyPlayerNameTextPosition + new Vector3(-TextSpace.y * 3, -TextSpace.x * (i + 1), 0);
                        newPlayerNameObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                        newPlayerNameObj.name = "DteamPlayer (" + i + ")";
                        TeamD_PlayerNameText[i] = newPlayerNameObj.GetComponent<Text>();
                        TeamD_PlayerNameText[i].text = "N/A";

                        GameObject newScoreObj = Instantiate(DummyScoreText.gameObject);
                        newScoreObj.SetActive(true);
                        newScoreObj.transform.parent = canvasTransform;
                        newScoreObj.transform.localPosition = DummyScoreTextPosition + new Vector3(-TextSpace.y * 3, -TextSpace.x * (i + 1), 0);
                        newScoreObj.transform.rotation = DummyPlayerNameText.transform.rotation;
                        newScoreObj.name = "DteamScore (" + i + ")";
                        TeamD_ScoreText[i] = newScoreObj.GetComponent<Text>();
                        TeamD_ScoreText[i].text = "0";
                    }
                }
            }

            return true;
        }
        //---------------------------------------
        public TeamName CheckWinnerTeam_TimerMode()
        {
            //Timerモード・チーム戦での勝者を判定する

            if (gameManager.teamMode == TeamMode.FreeForAll) { return TeamName.None; } //チーム戦でない場合は動作保証外(勝利チームは関係ない.形式上Noneを返す)

            //チーム戦で、gameEndConditionがTimerの場合は最もスコアが高いチームを返す
            if (gameManager.gameEndCondition == GameEndCondition.Timer)
            {
                float topScore = -1;
                TeamName WinnerTeamName = TeamName.None;
                bool isDraw = false;

                if (PlayerHitBox_Team_A.Length > 0)
                {
                    if (TeamA_ScoreSum > topScore)
                    {
                        WinnerTeamName = TeamName.A;
                        topScore = TeamA_ScoreSum;
                        isDraw = false;
                    }
                    else if (TeamA_ScoreSum == topScore)
                    {
                        isDraw = true;
                    }
                }
                if (PlayerHitBox_Team_B.Length > 0)
                {
                    if (TeamB_ScoreSum > topScore)
                    {
                        WinnerTeamName = TeamName.B;
                        topScore = TeamB_ScoreSum;
                        isDraw = false;
                    }
                    else if (TeamB_ScoreSum == topScore)
                    {
                        isDraw = true;
                    }
                }
                if (PlayerHitBox_Team_C.Length > 0)
                {
                    if (TeamC_ScoreSum > topScore)
                    {
                        WinnerTeamName = TeamName.C;
                        topScore = TeamC_ScoreSum;
                        isDraw = false;
                    }
                    else if (TeamC_ScoreSum == topScore)
                    {
                        isDraw = true;
                    }
                }
                if (PlayerHitBox_Team_D.Length > 0)
                {
                    if (TeamD_ScoreSum > topScore)
                    {
                        WinnerTeamName = TeamName.D;
                        topScore = TeamD_ScoreSum;
                        isDraw = false;
                    }
                    else if (TeamD_ScoreSum == topScore)
                    {
                        isDraw = true;
                    }
                }

                if (isDraw) { return TeamName.None; }
                else { return WinnerTeamName; }
            }

            //下記のモードは動作保証外.
            //Scoreモードでは別の関数で定期的に勝者判定を行う.
            //Destroyモードはチーム戦を想定する.FFA戦ではこの関数を使わない.
            //Ticketモードでは別の関数で勝者判定を行う.
            else
            {
                return TeamName.None;
            }
        }
        //---------------------------------------
        public PlayerHitBox CheckWinnerPlayer_TimerMode()
        {
            //FFA戦での勝者を判定する
            //ドローの場合はnullを返す

            //FFA戦でない場合は動作保証外(勝利チームは関係ない.形式上Noneを返す)
            if (gameManager.teamMode == TeamMode.TeamBattle) { return null; }

            //Timerモードでは、最もスコアが高いプレイヤーのPlayerHitBoxを返す
            if (gameManager.gameEndCondition == GameEndCondition.Timer)
            {
                float topScore = -1;
                PlayerHitBox WinnerPlayerHitBox = null;
                bool isDraw = false;
                for (int i = 0; i < PlayerHitBox_Team_A.Length; ++i)
                {
                    if (PlayerHitBox_Team_A[i].SyncedScore > topScore)
                    {
                        WinnerPlayerHitBox = PlayerHitBox_Team_A[i];
                        topScore = PlayerHitBox_Team_A[i].SyncedScore;
                        isDraw = false;
                    }
                    else if (PlayerHitBox_Team_A[i].SyncedScore == topScore)
                    {
                        isDraw = true;
                    }
                }
                for (int i = 0; i < PlayerHitBox_Team_B.Length; ++i)
                {
                    if (PlayerHitBox_Team_B[i].SyncedScore > topScore)
                    {
                        WinnerPlayerHitBox = PlayerHitBox_Team_B[i];
                        topScore = PlayerHitBox_Team_B[i].SyncedScore;
                        isDraw = false;
                    }
                    else if (PlayerHitBox_Team_B[i].SyncedScore == topScore)
                    {
                        isDraw = true;
                    }
                }
                for (int i = 0; i < PlayerHitBox_Team_C.Length; ++i)
                {
                    if (PlayerHitBox_Team_C[i].SyncedScore > topScore)
                    {
                        WinnerPlayerHitBox = PlayerHitBox_Team_C[i];
                        topScore = PlayerHitBox_Team_C[i].SyncedScore;
                        isDraw = false;
                    }
                    else if (PlayerHitBox_Team_C[i].SyncedScore == topScore)
                    {
                        isDraw = true;
                    }
                }
                for (int i = 0; i < PlayerHitBox_Team_D.Length; ++i)
                {
                    if (PlayerHitBox_Team_D[i].SyncedScore > topScore)
                    {
                        WinnerPlayerHitBox = PlayerHitBox_Team_D[i];
                        topScore = PlayerHitBox_Team_D[i].SyncedScore;
                        isDraw = false;
                    }
                    else if (PlayerHitBox_Team_D[i].SyncedScore == topScore)
                    {
                        isDraw = true;
                    }
                }

                if (isDraw) { return null; }
                else { return WinnerPlayerHitBox; }
            }

            //下記のモードは動作保証外.
            //Scoreモードでは別の関数で定期的に勝者判定を行う.
            //Destroyモードはチーム戦を想定する.FFA戦ではこの関数を使わない.
            //Ticketモードでは別の関数で勝者判定を行う.
            else
            {
                return null;
            }
        }
        //---------------------------------------
        public TeamName CheckWinnerTeam_ScoreMode()
        {
            //Scoreモード・チーム戦での勝者を判定する
            float targetScore = gameManager.SyncedMaxScore;
            if (PlayerHitBox_Team_A.Length > 0 && TeamA_ScoreSum >= targetScore) { return TeamName.A; }
            else if (PlayerHitBox_Team_B.Length > 0 && TeamB_ScoreSum >= targetScore) { return TeamName.B; }
            else if (PlayerHitBox_Team_C.Length > 0 && TeamC_ScoreSum >= targetScore) { return TeamName.C; }
            else if (PlayerHitBox_Team_D.Length > 0 && TeamD_ScoreSum >= targetScore) { return TeamName.D; }
            else { return TeamName.None; }
        }
        //---------------------------------------
        public PlayerHitBox CheckWinnerPlayer_ScoreMode()
        {
            //Scoreモード・FFA戦での勝者を判定する

            //規定スコアに達したプレイヤーのPlayerHitBoxを返す
            //どのプレイヤーも規定スコアに達していない場合はnullを返す
            float targetScore = gameManager.SyncedMaxScore;

            for (int i = 0; i < PlayerHitBox_Team_A.Length; ++i)
            {
                if (PlayerHitBox_Team_A[i].SyncedScore >= targetScore)
                {
                    return PlayerHitBox_Team_A[i];
                }
            }
            for (int i = 0; i < PlayerHitBox_Team_B.Length; ++i)
            {
                if (PlayerHitBox_Team_B[i].SyncedScore >= targetScore)
                {
                    return PlayerHitBox_Team_B[i];
                }
            }
            for (int i = 0; i < PlayerHitBox_Team_C.Length; ++i)
            {
                if (PlayerHitBox_Team_C[i].SyncedScore >= targetScore)
                {
                    return PlayerHitBox_Team_C[i];
                }
            }
            for (int i = 0; i < PlayerHitBox_Team_D.Length; ++i)
            {
                if (PlayerHitBox_Team_D[i].SyncedScore >= targetScore)
                {
                    return PlayerHitBox_Team_D[i];
                }
            }

            return null;
        }
        //---------------------------------------
        public TeamName CheckWinnerTeam_TicketMode()
        {
            //Ticketモード・チーム戦での勝者を判定する

            int remainingTeamNum = 0;

            bool TeamA_Alive = false;
            if (PlayerHitBox_Team_A.Length > 0)
            {
                for (int i = 0; i < PlayerHitBox_Team_A.Length; ++i)
                {
                    if (PlayerHitBox_Team_A[i].SyncedReviveTicket > 0) { TeamA_Alive = true; }
                }
            }

            bool TeamB_Alive = false;
            if (PlayerHitBox_Team_B.Length > 0)
            {
                for (int i = 0; i < PlayerHitBox_Team_B.Length; ++i)
                {
                    if (PlayerHitBox_Team_B[i].SyncedReviveTicket > 0) { TeamB_Alive = true; }
                }
            }

            bool TeamC_Alive = false;
            if (PlayerHitBox_Team_C.Length > 0)
            {
                for (int i = 0; i < PlayerHitBox_Team_C.Length; ++i)
                {
                    if (PlayerHitBox_Team_C[i].SyncedReviveTicket > 0) { TeamC_Alive = true; }
                }
            }

            bool TeamD_Alive = false;
            if (PlayerHitBox_Team_D.Length > 0)
            {
                for (int i = 0; i < PlayerHitBox_Team_D.Length; ++i)
                {
                    if (PlayerHitBox_Team_D[i].SyncedReviveTicket > 0) { TeamD_Alive = true; }
                }
            }

            if (TeamA_Alive) { ++remainingTeamNum; }
            if (TeamB_Alive) { ++remainingTeamNum; }
            if (TeamC_Alive) { ++remainingTeamNum; }
            if (TeamD_Alive) { ++remainingTeamNum; }

            if (remainingTeamNum == 1)
            {
                if (TeamA_Alive) { return TeamName.A; }
                else if (TeamB_Alive) { return TeamName.B; }
                else if (TeamC_Alive) { return TeamName.C; }
                else { return TeamName.D; }
            }   //1チームだけ残っている場合
            else if (remainingTeamNum == 0) { return TeamName.A; }   //同時に全滅した場合はAチームの勝ちにする
            else { return TeamName.None; }  //2チーム以上残っている場合
        }
        //---------------------------------------
        public int CountAlivePlayer_TicketMode()
        {
            int remainingPlayerNum = 0;
            for (int i = 0; i < Assigners.Length; ++i)
            {
                if (Assigners[i].playerHitBox.SyncedReviveTicket > 0 && Assigners[i].playerHitBox.SyncedPlayerID > 0 && Utilities.IsValid(VRCPlayerApi.GetPlayerById(Assigners[i].playerHitBox.SyncedPlayerID)))
                {
                    ++remainingPlayerNum;
                }
            }
            return remainingPlayerNum;
        }
        //---------------------------------------
        public PlayerHitBox CheckWinnerPlayer_TicketMode()
        {
            //Ticketモード・FFA戦での勝者を判定する

            int remainingPlayerNum = 0;
            PlayerHitBox remainingPlayerHitBox = null;
            if (PlayerHitBox_Team_A.Length > 0)
            {
                for (int i = 0; i < Assigners.Length; ++i)
                {
                    if (Assigners[i].playerHitBox.SyncedReviveTicket > 0 && Assigners[i].playerHitBox.SyncedPlayerID > 0 && Utilities.IsValid(VRCPlayerApi.GetPlayerById(Assigners[i].playerHitBox.SyncedPlayerID)))
                    {
                        ++remainingPlayerNum;
                        remainingPlayerHitBox = Assigners[i].playerHitBox;
                    }
                }
            }

            if (remainingPlayerNum == 1) { return remainingPlayerHitBox; }   //1人だけ残っている場合(勝者確定)
            else if (remainingPlayerNum == 0) { return null; }//同時に全滅した場合はスルー(DelayedSecond_CheckWinner_TicketMode()で引き分け)
            else { return null; }//2人以上残っている場合
        }
        //---------------------------------------
        public TeamName CheckWinnerTeam_DestoryMode()
        {
            //Conquestモード・チーム戦(2チームのみ)での勝者を判定する
            //FFA戦や3チーム以上の場合は想定しない

            Bot[] TeamA_BaseBot = gameManager.TeamA_BaseBot;
            Bot[] TeamB_BaseBot = gameManager.TeamB_BaseBot;
            bool TeamAWins = true;
            bool TeamBWins = true;

            for (int i = 0; i < TeamA_BaseBot.Length; ++i)
            {
                if (TeamA_BaseBot[i].isAlive)
                {
                    TeamBWins = false;
                }
            }
            for (int i = 0; i < TeamB_BaseBot.Length; ++i)
            {
                if (TeamB_BaseBot[i].isAlive)
                {
                    TeamAWins = false;
                }
            }

            if (TeamAWins) { return TeamName.A; }
            else if (TeamBWins) { return TeamName.B; }
            else { return TeamName.None; }
        }
        //---------------------------------------
        public void CalculateConquestScore()
        {
            float targetScore = gameManager.SyncedMaxConquestTime;

            float ATeamScore = 0;
            if (PlayerHitBox_Team_A.Length > 0)
            {
                for (int i = 0; i < PlayerHitBox_Team_A.Length; ++i)
                {
                    ATeamScore += PlayerHitBox_Team_A[i].SyncedConquestScore;
                    TeamA_ScoreText[i].text = PlayerHitBox_Team_A[i].SyncedConquestScore.ToString("F1");
                    Debug.Log("TeamA[" + i + "] ConquestScore=" + PlayerHitBox_Team_A[i].SyncedConquestScore);
                }
                if (ATeamScore > targetScore) { ATeamScore = targetScore; }
                TeamA_ConquestScoreSum = ATeamScore;

                if (TeamA_ScoreSumText) { TeamA_ScoreSumText.text = TeamA_ConquestScoreSum.ToString("F1"); }
            }

            float BTeamScore = 0;
            if (PlayerHitBox_Team_B.Length > 0)
            {
                for (int i = 0; i < PlayerHitBox_Team_B.Length; ++i)
                {
                    BTeamScore += PlayerHitBox_Team_B[i].SyncedConquestScore;
                    TeamB_ScoreText[i].text = PlayerHitBox_Team_B[i].SyncedConquestScore.ToString("F1");
                    Debug.Log("TeamB[" + i + "] ConquestScore=" + PlayerHitBox_Team_B[i].SyncedConquestScore);
                }
                if (BTeamScore > targetScore) { BTeamScore = targetScore; }
                TeamB_ConquestScoreSum = BTeamScore;

                if (TeamB_ScoreSumText) { TeamB_ScoreSumText.text = TeamB_ConquestScoreSum.ToString("F1"); }
            }

            float CTeamScore = 0;
            if (PlayerHitBox_Team_C.Length > 0)
            {
                for (int i = 0; i < PlayerHitBox_Team_C.Length; ++i)
                {
                    CTeamScore += PlayerHitBox_Team_C[i].SyncedConquestScore;
                    TeamC_ScoreText[i].text = PlayerHitBox_Team_C[i].SyncedConquestScore.ToString("F1");
                }
                if (CTeamScore > targetScore) { CTeamScore = targetScore; }
                TeamC_ConquestScoreSum = CTeamScore;

                if (TeamC_ScoreSumText) { TeamC_ScoreSumText.text = TeamC_ConquestScoreSum.ToString("F1"); }
            }

            float DTeamScore = 0;
            if (PlayerHitBox_Team_D.Length > 0)
            {
                for (int i = 0; i < PlayerHitBox_Team_D.Length; ++i)
                {
                    DTeamScore += PlayerHitBox_Team_D[i].SyncedConquestScore;
                    TeamD_ScoreText[i].text = PlayerHitBox_Team_D[i].SyncedConquestScore.ToString("F1");
                }
                if (DTeamScore > targetScore) { DTeamScore = targetScore; }
                TeamD_ConquestScoreSum = DTeamScore;

                if (TeamD_ScoreSumText) { TeamD_ScoreSumText.text = TeamD_ConquestScoreSum.ToString("F1"); }
            }
        }
        //---------------------------------------
        public TeamName CheckWinnerTeam_ConquestMode()
        {
            //Conquestモード・チーム戦での勝者を判定する
            float targetScore = gameManager.SyncedMaxConquestTime;

            float ATeamScore = 0;
            if (PlayerHitBox_Team_A.Length > 0)
            {
                for (int i = 0; i < PlayerHitBox_Team_A.Length; ++i) { ATeamScore += PlayerHitBox_Team_A[i].SyncedConquestScore; }
                TeamA_ConquestScoreSum = ATeamScore;
                if (TeamA_ConquestScoreSum >= targetScore) { return TeamName.A; }
            }

            float BTeamScore = 0;
            if (PlayerHitBox_Team_B.Length > 0)
            {
                for (int i = 0; i < PlayerHitBox_Team_B.Length; ++i) { BTeamScore += PlayerHitBox_Team_B[i].SyncedConquestScore; }
                TeamB_ConquestScoreSum = BTeamScore;
                if (TeamB_ConquestScoreSum >= targetScore) { return TeamName.B; }
            }

            float CTeamScore = 0;
            if (PlayerHitBox_Team_C.Length > 0)
            {
                for (int i = 0; i < PlayerHitBox_Team_C.Length; ++i) { CTeamScore += PlayerHitBox_Team_C[i].SyncedConquestScore; }
                TeamC_ConquestScoreSum = CTeamScore;
                if (TeamC_ConquestScoreSum >= targetScore) { return TeamName.C; }
            }

            float DTeamScore = 0;
            if (PlayerHitBox_Team_D.Length > 0)
            {
                for (int i = 0; i < PlayerHitBox_Team_D.Length; ++i) { DTeamScore += PlayerHitBox_Team_D[i].SyncedConquestScore; }
                TeamD_ConquestScoreSum = DTeamScore;
                if (TeamD_ConquestScoreSum >= targetScore) { return TeamName.D; }
            }

            return TeamName.None;
        }
        //---------------------------------------
        public PlayerHitBox CheckWinnerPlayer_ConquestMode()
        {
            //Conquestモード・FFA戦での勝者を判定する

            //規定スコアに達したプレイヤーのPlayerHitBoxを返す
            //どのプレイヤーも規定スコアに達していない場合はnullを返す
            float targetScore = gameManager.SyncedMaxConquestTime;

            for (int i = 0; i < PlayerHitBox_Team_A.Length; ++i)
            {
                if (PlayerHitBox_Team_A[i].SyncedConquestScore >= targetScore)
                {
                    return PlayerHitBox_Team_A[i];
                }
            }
            for (int i = 0; i < PlayerHitBox_Team_B.Length; ++i)
            {
                if (PlayerHitBox_Team_B[i].SyncedConquestScore >= targetScore)
                {
                    return PlayerHitBox_Team_B[i];
                }
            }
            for (int i = 0; i < PlayerHitBox_Team_C.Length; ++i)
            {
                if (PlayerHitBox_Team_C[i].SyncedConquestScore >= targetScore)
                {
                    return PlayerHitBox_Team_C[i];
                }
            }
            for (int i = 0; i < PlayerHitBox_Team_D.Length; ++i)
            {
                if (PlayerHitBox_Team_D[i].SyncedConquestScore >= targetScore)
                {
                    return PlayerHitBox_Team_D[i];
                }
            }

            return null;
        }
        //---------------------------------------
        public void AddFlagScore(TeamName team)
        {
            TrySetOwner(this.gameObject);
            if (team == TeamName.A) { ++TeamA_FlagScore; }
            if (team == TeamName.B) { ++TeamB_FlagScore; }
            if (team == TeamName.C) { ++TeamC_FlagScore; }
            if (team == TeamName.D) { ++TeamD_FlagScore; }
            Sync();
        }
        //---------------------------------------
        public void ResetFlagScore()
        {
            if (!CheckLocalPlayerIsOwner(this.gameObject)) { return; }

            TeamA_FlagScore = 0;
            TeamB_FlagScore = 0;
            TeamC_FlagScore = 0;
            TeamD_FlagScore = 0;

            Sync();
        }
        //---------------------------------------
        public TeamName CheckWinnerTeam_FlagCarrierMode()
        {
            //FlagCarrierモード・チーム戦での勝者を判定する

            gameManager.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_ShowFlagScore");

            float targetFlagScore = gameManager.SyncedMaxFlagScore;
            if (PlayerHitBox_Team_A.Length > 0 && TeamA_FlagScore >= targetFlagScore) { return TeamName.A; }
            else if (PlayerHitBox_Team_B.Length > 0 && TeamB_FlagScore >= targetFlagScore) { return TeamName.B; }
            else if (PlayerHitBox_Team_C.Length > 0 && TeamC_FlagScore >= targetFlagScore) { return TeamName.C; }
            else if (PlayerHitBox_Team_D.Length > 0 && TeamD_FlagScore >= targetFlagScore) { return TeamName.D; }
            else { return TeamName.None; }
        }
        //---------------------------------------
        public int CastPlayerHitBoxToInt(PlayerHitBox _p)
        {
            //TeamNameを同期するためのキャスト
            //_pがnullであれば-1
            //それ以外であれば1から順に整数を割り当てる
            //割り当てる整数はgameManager.Assigners[]のインデックス

            if (!_p) { return -1; }
            else
            {
                for (int i = 0; i < gameManager.Assigners.Length; ++i)
                {
                    if (_p == gameManager.Assigners[i].playerHitBox) { return i; }
                }
                return -1;  //該当するものがない場合のエラー回避
            }
        }
        //---------------------------------------
        public PlayerHitBox CastIntToPlayerHitBox(int _i)
        {
            //PlayerHitBoxを同期するためのキャスト
            //_iが-1であればnull
            //それ以外であれば1から順に整数を割り当てる
            //割り当てる整数はgameManager.Assigners[]のインデックス

            if (_i < 0 || gameManager.Assigners.Length <= _i) { return null; }
            else { return gameManager.Assigners[_i].playerHitBox; }
        }
        //---------------------------------------
        public int CastTeamNameToInt(TeamName _t)
        {
            //TeamNameを同期するためのキャスト
            //_tが未定義であれば-1
            //Noneであれば0
            //それ以外であれば1から順に整数を割り当てる

            if (_t == TeamName.None) { return 0; }
            if (_t == TeamName.A) { return 1; }
            if (_t == TeamName.B) { return 2; }
            if (_t == TeamName.C) { return 3; }
            if (_t == TeamName.D) { return 4; }
            else { return -1; }
        }
        //---------------------------------------
        public TeamName CastIntToTeamName(int _i)
        {
            //TeamNameを同期するためのキャスト
            //_iが1から4であれば順にTeamNameを割り当てる
            //それ以外であればTeamName.Noneを返す

            if (_i == 1) { return TeamName.A; }
            else if (_i == 2) { return TeamName.B; }
            else if (_i == 3) { return TeamName.C; }
            else if (_i == 4) { return TeamName.D; }
            else { return TeamName.None; }
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
        public void Sync()
        {
            RequestSerialization();
            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                OnDeserialization();
            }
        }
        //---------------------------------------
        public override void OnDeserialization()
        {
            //Flagモードの場合、GameManagerのOwnerは終了判定を行う
            if (gameManager.gameEndCondition == GameEndCondition.FlagCarrier)
            {
                if (CheckLocalPlayerIsOwner(gameManager.gameObject))
                {
                    gameManager.CheckWinner_FlagCarrierMode();
                    gameManager.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_ShowFlagScore");
                }
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
    }
}

