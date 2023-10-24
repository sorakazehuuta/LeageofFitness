
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace CombatGimmick
{
    public enum TeamMode
    {
        TeamBattle, FreeForAll, None
    }
    public enum GameEndCondition
    {
        None, Score, Ticket, Timer, DestroyTarget, FlagCarrier, Conquest
        //Score：規定スコアを先に取ったプレイヤー/チームの勝ち
        //Ticket：復活時にチケット-1.最後にチケットが残ったチームの勝ち
        //Timer：タイマー終了時にスコアが最も高いプレイヤー/チームの勝ち
        //DestroyTarget：拠点オブジェクトが最後まで残っているチームの勝ち
        //FlagCarrier：フラッグを規定数先に持ち帰ったチームの勝ち
        //Domination：戦闘中に、ゾーン内に入っていた時間が長いチームの勝ち
    }
    public enum TransceiverMode
    {
        None, Friend, All
    }

    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class GameManager : UdonSharpBehaviour
    {
        //---------------------------------------
        [Header("必須項目.無いとエラー")]
        [Header("注意：このオブジェクトのOwnerのみゲーム開始可能")]
        [Tooltip("チーム戦かどうか")] public TeamMode teamMode;
        //---------------------------------------
        [Tooltip("ゲーム終了条件")] public GameEndCondition gameEndCondition;
        [Tooltip("アップロード時に設定するインスタンス定員")] [SerializeField] int MaxPlayerNum = 10;
        [Tooltip("リスポーンポイント(空のゲームオブジェクト)")] [SerializeField] Transform RespawnTransform;
        //---------------------------------------
        [Header("プレイヤーのHP設定")]
        [Tooltip("プレイヤーの最大HP")] [SerializeField] public float MaxPlayerHitPoint = 100;
        [Tooltip("trueならプレイヤーの最大HPを個別に設定可能(手動設定が必要)")] [SerializeField] public bool SetMaxHitPointManually;
        //---------------------------------------
        [Header("ゲーム開始時の設定(任意項目.無しでも動作可能)")]
        [Tooltip("設定ありなら未登録プレイヤーがいるかチェックする")] [SerializeField] GameObject StartChecker;
        [Tooltip("カウントダウンの秒数")] [SerializeField] float CountDownDuration = 5;
        [Tooltip("カウントダウンの効果音")] [SerializeField] AudioClip CountDownClip;
        [Tooltip("ゲーム開始時に動作させるAnimator")] [SerializeField] Animator[] OnStartGameAnimators;
        //---------------------------------------
        [Header("戦闘開始時の設定(任意項目.無しでも動作可能)")]
        [Tooltip("trueなら戦闘開始時に自動リスポーン")] [SerializeField] bool AutoRespawnOnBattleStart = true;
        [Tooltip("Aチームのリスポーン座標")] public Transform[] TeamA_RespawnTransform;
        [Tooltip("Bチームのリスポーン座標")] public Transform[] TeamB_RespawnTransform;
        [Tooltip("Cチームのリスポーン座標")] public Transform[] TeamC_RespawnTransform;
        [Tooltip("Dチームのリスポーン座標")] public Transform[] TeamD_RespawnTransform;
        [Tooltip("FFA用のリスポーン座標")] public Transform[] FFA_RespawnTransform;        //最大ゲーム人数よりも多い値をセットすること
        [Tooltip("戦闘開始時に動作させるAnimator")] [SerializeField] Animator[] OnStartBattleAnimators;
        //---------------------------------------
        [Header("戦闘終了時の設定(任意項目.無しでも動作可能)")]
        [Tooltip("戦闘終了時の効果音")] [SerializeField] AudioClip BattleEndClip;
        [Tooltip("戦闘終了時に動作させるAnimator")] [SerializeField] Animator[] OnEndBattleAnimators;
        [Tooltip("戦闘終了からゲーム終了までの時間")] [SerializeField] float BattleEndDuration;
        //---------------------------------------
        [Header("ゲーム終了時の設定(任意項目.無しでも動作可能)")]
        [Tooltip("trueなら2回目のゲーム開始を禁止する")] [SerializeField] bool DisableRestart;
        [Tooltip("trueならゲーム終了時に自動リスポーン")] [SerializeField] bool AutoRespawnOnGameEnd;
        [Tooltip("勝利プレイヤーのリスポーン座標")] public Transform WinnerRespawnTransform;
        [Tooltip("敗北プレイヤーのリスポーン座標")] public Transform LoserRespawnTransform;
        [Tooltip("勝利プレイヤーまたはチームの名前を出すテキスト")] [SerializeField] Text WinnerNameText;
        [Tooltip("ゲーム終了時に動作させるAnimator")] [SerializeField] Animator[] OnEndGameAnimators;
        [Tooltip("ゲーム終了時に発動するエフェクト")] [SerializeField] ParticleSystem OnGameEndParticle;
        [Tooltip("ゲーム終了時に出現させるオブジェクト")] [SerializeField] GameObject[] OnGameEndSpawnObjects;
        [Tooltip("勝利プレイヤー/チーム名の前につける文字")] [SerializeField] string WinnerPrefix = "";
        [Tooltip("勝利プレイヤー/チーム名の後ろにつける文字")] [SerializeField] string WinnerSuffix = "の勝ち！";
        [Tooltip("引き分け時に表示する文字")] [SerializeField] string DrawText = "引き分け！";
        [Tooltip("Aチームの名前")] public string TeamA_CustomName = "チームA";
        [Tooltip("Bチームの名前")] public string TeamB_CustomName = "チームB";
        [Tooltip("Cチームの名前")] public string TeamC_CustomName = "チームC";
        [Tooltip("Dチームの名前")] public string TeamD_CustomName = "チームD";
        //---------------------------------------
        [Header("その他(任意項目.無しでも動作可能)")]
        [Tooltip("ゲーム開始成功時の効果音")] [SerializeField] AudioClip SuccessClip;
        [Tooltip("ゲーム開始キャンセル時の効果音")] [SerializeField] AudioClip CancelClip;
        [Tooltip("マスターの名前を表示するText")] [SerializeField] Text OwnerNameText;
        [Tooltip("未使用のプレイヤー用ヒットボックスを隠す座標")] public Vector3 HidePlayerHitBoxPos = new Vector3(0, -100, 0);
        [Tooltip("チーム設定戦/バトロワ設定の表示用テキスト")] [SerializeField] Text TeamModeText;
        [Tooltip("ゲームモード設定の表示用テキスト")] [SerializeField] Text GameEndConditionText;
        [Tooltip("ゲーム時間を表示するテキスト")] [SerializeField] Text[] SyncedGameTimetext;
        [Tooltip("チーム戦モードの名前")] [SerializeField] string TeamBattleMode_CustomName = "チーム戦";
        [Tooltip("バトルロワイヤルモードの名前")] [SerializeField] string FreeForAllMode_CustomName = "バトルロワイヤル";
        [Tooltip("スコアモードの名前")] [SerializeField] string ScoreMode_CustomName = "スコアモード";
        [Tooltip("残機制モードの名前")] [SerializeField] string TicketMode_CustomName = "残機制モード";
        [Tooltip("タイム制モードの名前")] [SerializeField] string TimerMode_CustomName = "タイム制モード";
        [Tooltip("拠点防衛モードの名前")] [SerializeField] string DestroyTargetMode_CustomName = "拠点防衛モード";
        [Tooltip("フラッグモードの名前")] [SerializeField] string FlagCarrierMode_CustomName = "フラッグモード";
        [Tooltip("ゾーン制圧モードの名前")] [SerializeField] string ConquestMode_CustomName = "ゾーン制圧モード";
        [Tooltip("全員のトランシーバー有効時に表示するオブジェクト")] [SerializeField] GameObject AllTransceiverMarker;
        [Tooltip("味方のみのトランシーバー有効時に表示するオブジェクト")] [SerializeField] GameObject TeamTransceiverMarker;
        [Tooltip("トランシーバー無効時に表示するオブジェクト")] [SerializeField] GameObject NoTransceiverMarker;
        //---------------------------------------
        [Tooltip("トランシーバー使用時のボイス音量")] [SerializeField, Range(0, 10)] float TransceiverGain = 10.0f;
        [HideInInspector] [Tooltip("For Debugging")] public Text debugTransceiverGainText;
        //---------------------------------------
        [Header("スコアモードの必須設定")]
        [Tooltip("目標スコア")] [UdonSynced] public float SyncedMaxScore = 200;
        //---------------------------------------
        [Header("スコアモードの任意項目")]
        [Tooltip("スコアモードでのみ表示するオブジェクト")] [SerializeField] GameObject[] EnableOnScoreModeObjects;
        [Tooltip("スコアモードの目標スコアを表示するText")] [SerializeField] Text[] MaxScoreText;
        [Tooltip("スコアモードの現在のスコアを表示するText")] [SerializeField] Text[] MyScoreText;
        //---------------------------------------
        [Header("残機制モードの必須設定")]
        [Tooltip("各チームの残機数")] [UdonSynced] public int SyncedMaxReviveTicket = 10;
        //---------------------------------------
        [Header("残機制モードの任意項目")]
        [Tooltip("残機制モードでのみ表示するオブジェクト")] [SerializeField] GameObject[] EnableOnTicketModeObjects;
        [Tooltip("残機制モードの最大残機数を表示するText")] [SerializeField] Text[] MaxReviveTicketText;
        [Tooltip("残機制モードの残り残機数を表示するText")] [SerializeField] Text[] MyReviveTicketText;
        //---------------------------------------
        [Header("タイム制モードの必須設定")]
        [Tooltip("戦闘時間(秒)")] [UdonSynced] public float SyncedBattleDuration = 60;
        //---------------------------------------
        [Header("タイム制モードの任意項目")]
        [Tooltip("タイム制モードでのみ表示するオブジェクト")] [SerializeField] GameObject[] EnableOnTimerModeObjects;
        [Tooltip("タイム制モードの制限時間を表示するText")] [SerializeField] Text[] BattleDurationText;
        [Tooltip("タイム制モードの残り時間を表示するText")] [SerializeField] Text[] RemainingBattleDurationText;
        //---------------------------------------
        [Header("拠点防衛モードの必須設定")]
        [Tooltip("撃破されるとAチーム敗北")] public Bot[] TeamA_BaseBot;
        [Tooltip("撃破されるとBチーム敗北")] public Bot[] TeamB_BaseBot;
        //---------------------------------------
        [Header("拠点防衛モードの任意項目")]
        [Tooltip("拠点防衛モードでのみ表示するオブジェクト")] [SerializeField] GameObject[] EnableOnDestroyModeObjects;
        //---------------------------------------
        [Header("フラッグモードの必須設定")]
        [Tooltip("フラッグの出現位置")] public Transform[] FlagSpawnPoints;
        [Tooltip("目標納品数")] [UdonSynced] public float SyncedMaxFlagScore = 5;
        //---------------------------------------
        [Header("フラッグモードの任意項目")]
        [Tooltip("フラッグモードでのみ表示するオブジェクト")] [SerializeField] GameObject[] EnableOnFlagModeObjects;
        [Tooltip("フラッグモードの目標納品数を表示するText")] [SerializeField] Text[] MaxFlagScoreText;
        [Tooltip("フラッグモードの現在の納品数を表示するText")] [SerializeField] Text[] MyFlagScoreText;
        //---------------------------------------
        [Header("ゾーン制圧モードの必須設定")]
        [Tooltip("ゲーム終了に必要な占領時間")] [UdonSynced] public float SyncedMaxConquestTime = 100;
        //---------------------------------------
        [Header("ゾーン制圧モードの任意項目")]
        [Tooltip("ゾーン制圧モードでのみ表示するオブジェクト")] [SerializeField] GameObject[] EnableOnConquestModeObjects;
        [Tooltip("ゾーン制圧モードの目標占領時間を表示するText")] [SerializeField] Text[] MaxConquestTimeText;
        [Tooltip("ゾーン制圧モードの現在の占領時間を表示するText")] [SerializeField] Text[] MyConquestTimeText;
        //---------------------------------------
        //ゲーム開始前              SyncedInGame = false, SyncedInBattle = false
        //カウントダウン中          SyncedInGame = true,  SyncedInBattle = false
        //戦闘中                    SyncedInGame = true,  SyncedInBattle = true
        //戦闘後～ゲーム終了        SyncedInGame = true,  SyncedInBattle = false
        //[Header("手動で変更しないこと")]
        [HideInInspector] [Tooltip("ゲーム中ならtrue")] [UdonSynced] public bool SyncedInGame;
        [HideInInspector] [Tooltip("戦闘中ならtrue")] [UdonSynced] public bool SyncedInBattle;
        [HideInInspector] [Tooltip("勝者のPlayerHitBox")] [UdonSynced] public int SyncedWinnerPlayerHitBox;      //独自定義の型(PlayerHitBox)は同期できないので、Intで表現
        [HideInInspector] [Tooltip("勝者のチーム")] [UdonSynced] public int SyncedWinnerTeamName;               //独自定義の型(TeamName)は同期できないので、Intで表現
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("RangedWeapon_MainModule[]")] public RangedWeapon_MainModule[] rangedWeapon_MainModules;
        [HideInInspector] [Tooltip("MeleeWeapon[]")] public MeleeWeapon[] meleeWeapons;
        [HideInInspector] [Tooltip("SubWeapon[]")] public ExWeapon[] exWeapons;
        [HideInInspector] [Tooltip("Assigner[]")] public Assigner[] Assigners;
        [HideInInspector] [Tooltip("MusicManager")] public MusicManager musicManager;
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        [HideInInspector] [Tooltip("ScoreManager")] public ScoreManager scoreManager;
        [HideInInspector] [Tooltip("SyncedTeamMode")] [UdonSynced] [SerializeField] int SyncedTeamMode;                   //独自定義の型(TeamMode)は同期できないので、Intで表現
        [HideInInspector] [Tooltip("SyncedGameEndCondition")] [UdonSynced] [SerializeField] int SyncedGameEndCondition;      //独自定義の型(GameEndCondition)は同期できないので、Intで表現
        [HideInInspector] [Tooltip("DropItem[]")] public DropItem[] dropItems;
        [HideInInspector] [Tooltip("Flag[]")] public Flag[] flags;
        [HideInInspector] [Tooltip("ConquestZone[]")] public ConquestZone[] conquestZone;
        [HideInInspector] [Tooltip("Bot[]")] public Bot[] bots;
        [HideInInspector] [Tooltip("Turret[]")] public Turret[] turrets;
        [HideInInspector] [Tooltip("HUD")] public HUD hud;
        //---------------------------------------
        float gameStartTime;
        float battleStartTime;
        float battleEndTime;
        bool gameEndFlagForTimerMode;
        float LastUpdateTime;
        [UdonSynced] float SyncedGameTime;
        [UdonSynced] int SyncedTransceiverMode;
        TransceiverMode transceiverMode;
        const int CheckWinnerInterval = 3;
        int MaxInstancePlayerNum;
        //---------------------------------------
        private void Start()
        {
            for (int i = 0; i < OnGameEndSpawnObjects.Length; ++i)
            {
                if (OnGameEndSpawnObjects[i]) { OnGameEndSpawnObjects[i].SetActive(false); }
            }

            if (StartChecker) { StartChecker.SetActive(false); }

            MaxInstancePlayerNum = MaxPlayerNum * 2 + 2;    //VRCの仕様で、表示上の最大人数x2に加えて、ワールド作者+インスタンス作成者は無条件で入れる

            const float delay = 1;
            SendCustomEventDelayedSeconds("DelayedSecond_StartSync", delay);
        }
        //---------------------------------------
        public void DelayedSecond_StartSync()
        {
            StartSync();
        }
        //---------------------------------------
        public void StartSync()
        {
            OnDeserialization();
        }
        //---------------------------------------
        private void Update()
        {
            if (Time.time - LastUpdateTime > 1 && gameEndCondition == GameEndCondition.Timer && SyncedInBattle)
            {
                UpdateTimer();

                LastUpdateTime = Time.time;
            }
            //時間制モードかつバトル中はゲーム終了時間に到達しているかチェック
            //実際の処理はGameManagerのマスターのみ実行
            CheckTimerEnd();
        }
        //---------------------------------------
        public void CheckTimerEnd()
        {
            if (gameEndCondition != GameEndCondition.Timer) { return; }
            if (gameEndFlagForTimerMode) { return; }
            if (!SyncedInBattle) { return; }
            if (Time.time - gameStartTime < 1) { return; }
            if (Time.time - battleStartTime < SyncedBattleDuration) { return; }
            if (!CheckLocalPlayerIsOwner(this.gameObject)) { return; }

            gameEndFlagForTimerMode = true;
            SendBattleEndMessage_TimerMode();
        }
        //---------------------------------------
        public void UpdateTimer()
        {
            float CurrentTime = Time.time;
            float remainingTime = SyncedBattleDuration - (CurrentTime - battleStartTime);

            if (remainingTime <= 0)
            {
                for (int i = 0; i < RemainingBattleDurationText.Length; ++i) { if (RemainingBattleDurationText[i]) { RemainingBattleDurationText[i].text = "00: 00: 00"; } }
                return;
            }

            int hour, second, minute;
            int remain1 = (int)remainingTime % 3600;
            hour = ((int)remainingTime - remain1) / 3600;
            second = remain1 % 60;
            minute = (remain1 - second) / 60;
            for (int i = 0; i < RemainingBattleDurationText.Length; ++i) { if (RemainingBattleDurationText[i]) { RemainingBattleDurationText[i].text = hour.ToString("D2") + ": " + minute.ToString("D2") + ": " + second.ToString("D2"); } }
        }
        //---------------------------------------
        public override void Interact()
        {
            TryStartGame();
        }
        //---------------------------------------
        public void TryStartGame()
        {
            //ゲーム中はInteract不可
            if (SyncedInBattle || SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //サポートしていないゲーム設定であればキャンセル
            if (gameEndCondition == GameEndCondition.DestroyTarget && teamMode == TeamMode.FreeForAll)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }
            if (gameEndCondition == GameEndCondition.FlagCarrier && teamMode == TeamMode.FreeForAll)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }
            
            //Owner以外が触った場合
            if (!CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //既にゲーム開始していた場合
            if (SyncedInGame || SyncedInBattle)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //ゲーム開始できる状態かチェックする
            if (!CheckPlayerExists())
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }
            if (StartChecker && teamMode == TeamMode.TeamBattle)
            {
                if (!CheckUnAssignedTeam())
                {
                    if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                    return;
                }
            }
            else if (StartChecker && teamMode == TeamMode.FreeForAll)
            {
                if (!CheckUnAssignedPlayer())
                {
                    if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                    return;
                }
            }

            //ゲーム開始
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_StartGame");

            //Ownerはゲーム状態を同期する(Ownershipは取得済み)
            SyncedInGame = true;
            SyncedInBattle = false;
            Sync();
        }
        //---------------------------------------
        public bool CheckPlayerExists()
        {
            //1人も登録済みプレイヤーがいない場合はfalse
            //それ以外であればtrue

            int assignedPlayerNum = 0;
            for (int i = 0; i < Assigners.Length; ++i) { if (Assigners[i].playerHitBox.SyncedPlayerID > 0) { ++assignedPlayerNum; } }

            if (assignedPlayerNum <= 0)
            {
                //登録していないプレイヤーがいる
                if (StartChecker) { StartChecker.SetActive(false); }
                return false;
            }

            return true;
        }
        //---------------------------------------
        public void ForceStartGame()
        {
            //ゲーム開始
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_StartGame");

            //ゲーム状態を同期する(Ownershipは取得済み)
            SyncedInGame = true;
            SyncedInBattle = false;
            Sync();

            if (StartChecker) { StartChecker.SetActive(false); }
        }
        //---------------------------------------
        public void CancelStartGame()
        {
            if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); } //ゲーム開始失敗

            StartChecker.SetActive(false);
        }
        //---------------------------------------
        public void Network_StartGame()
        {
            StartGame();
        }
        //---------------------------------------
        public void StartGame()
        {
            playerManager.isAlive = true;
            if (StartChecker) { StartChecker.SetActive(false); }

            for (int i = 0; i < OnGameEndSpawnObjects.Length; ++i) { if (OnGameEndSpawnObjects[i]) { OnGameEndSpawnObjects[i].SetActive(false); } }

            //武器の位置をリセット(SendCustomNetworkEventで全員がチェックする)
            if (CheckLocalPlayerIsOwner(this.gameObject)) { ResetAllWeapons(); }

            //PlayerHitBoxの状態をリセット
            for (int i = 0; i < Assigners.Length; ++i) { if (Assigners[i]) { Assigners[i].playerHitBox.ResetOnStartGame(); } }

            //タレットをリセット
            for (int i = 0; i < turrets.Length; ++i) { if (turrets[i]) { turrets[i].ResetOnStartGame(); } }

            //Botをリセット
            if (bots.Length > 0) { for (int i = 0; i < bots.Length; ++i) { if (bots[i]) { bots[i].ResetOnStartGame(); } } }

            //拠点防衛モードのBotをリセット
            if (TeamA_BaseBot.Length > 0) { for (int i = 0; i < TeamA_BaseBot.Length; ++i) { if (TeamA_BaseBot[i]) { TeamA_BaseBot[i].ResetOnStartGame(); } } }
            if (TeamB_BaseBot.Length > 0) { for (int i = 0; i < TeamB_BaseBot.Length; ++i) { if (TeamB_BaseBot[i]) { TeamB_BaseBot[i].ResetOnStartGame(); } } }

            if (playerManager.inventoryType == InventoryType.SingleNoReset || playerManager.inventoryType == InventoryType.DoubleNoReset) { }   //なにもしない
            else { playerManager.ResetLastPickup(); playerManager.ResetInventory(); }

            for (int i = 0; i < OnStartGameAnimators.Length; ++i) { if (OnStartGameAnimators[i]) { OnStartGameAnimators[i].SetTrigger("StartGame"); } }

            //占領ゾーンの状態を初期化
            if (gameEndCondition == GameEndCondition.Conquest) { for (int i = 0; i < conquestZone.Length; ++i) { conquestZone[i].ResetOnStartGame(); } }

            //フラッグモードの得点をリセット
            if (gameEndCondition == GameEndCondition.FlagCarrier)
            {
                scoreManager.ResetFlagScore();
            }

            gameEndFlagForTimerMode = false;
            gameStartTime = Time.time;

            musicManager.FadeOut(CountDownDuration);
            musicManager.SendCustomEventDelayedSeconds("DelayedSecond_PlayBattleMusic", CountDownDuration);
            if (CountDownClip) { this.GetComponent<AudioSource>().PlayOneShot(CountDownClip, 1.0f); }
            SendCustomEventDelayedSeconds("DelayedSecond_StartBattle", CountDownDuration);
        }
        //---------------------------------------
        public void DelayedSecond_StartBattle()
        {
            StartBattle();
        }
        //---------------------------------------
        public void StartBattle()
        {
            if (playerManager.playerHitBox && hud) { hud.EnableHUD(); }

            battleStartTime = Time.time;

            playerManager.ResetAmmo();

            //Ownerはゲーム状態を同期する
            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                SyncedInGame = true;
                SyncedInBattle = true;
                Sync();
            }

            for (int i = 0; i < OnStartBattleAnimators.Length; ++i) { if (OnStartBattleAnimators[i]) { OnStartBattleAnimators[i].SetTrigger("StartBattle"); } }

            //DropItemが存在する場合はリセット
            if (dropItems.Length > 0)
            {
                Transform dropItemParentTransform = dropItems[0].transform.parent;
                foreach (Transform child in dropItemParentTransform)
                {
                    if (child.GetComponent<DropItem>())
                    {
                        DropItem d = child.GetComponent<DropItem>();
                        d.StartBattle();
                    }
                }
            }

            //ゲーム開始時のリスポーン処理
            if (AutoRespawnOnBattleStart)
            {
                //チーム戦なら所属するチームのスポーン地点にテレポート
                if (teamMode == TeamMode.TeamBattle)
                {
                    //ゲーム開始時は、チーム全員が同じスポーンポイントに飛ぶ
                    //ゲーム中にダウン→復活した場合は、チームに対応したランダムスポーンに飛ばす
                    if (playerManager.playerHitBox && playerManager.playerHitBox.teamName == TeamName.A)
                    {
                        TeleportAndSetRespawnPoint(TeamA_RespawnTransform[0]);
                    }
                    else if (playerManager.playerHitBox && playerManager.playerHitBox.teamName == TeamName.B)
                    {
                        TeleportAndSetRespawnPoint(TeamB_RespawnTransform[0]);
                    }
                    else if (playerManager.playerHitBox && playerManager.playerHitBox.teamName == TeamName.C)
                    {
                        TeleportAndSetRespawnPoint(TeamC_RespawnTransform[0]);
                    }
                    else if (playerManager.playerHitBox && playerManager.playerHitBox.teamName == TeamName.D)
                    {
                        TeleportAndSetRespawnPoint(TeamD_RespawnTransform[0]);
                    }
                }

                //FFA戦ならランダムスポーン地点にテレポート
                else if (teamMode == TeamMode.FreeForAll)
                {
                    //ゲームに参加する最大人数以上のTransformを、配列に設定しておくこと
                    //もし10人参加できるゲームで、5箇所しかスポーンポイントを設定していなかった場合は、
                    //6人目は1人目と同じスポーンポイント、7人目は2人目と同じスポーンポイント・・に飛ばす
                    //そもそも設定値が0の場合は飛ばさない
                    if (playerManager.playerHitBox && FFA_RespawnTransform.Length > 0)
                    {
                        int myHitBoxNum = 0;
                        bool found = false;
                        for (int i = 0; i < Assigners.Length; ++i)
                        {
                            if (Assigners[i].playerHitBox == playerManager.playerHitBox && !found)
                            {
                                found = true;
                                myHitBoxNum = i;
                            }
                        }

                        int BattleStartSpawnNum = myHitBoxNum % FFA_RespawnTransform.Length;
                        //Debug.Log("BattleStartSpawnNum = " + BattleStartSpawnNum);

                        if (Utilities.IsValid(Networking.LocalPlayer)) { TeleportAndSetRespawnPoint(FFA_RespawnTransform[BattleStartSpawnNum]); }
                    }
                }
            }

            //ゲーム終了条件の設定に合わせて処理を行う
            if (gameEndCondition == GameEndCondition.Timer)
            {
                //SendCustomEventDelayedSeconds("DelayedSecond_TimerEnd", SyncedBattleDuration);    //強制終了ボタンと干渉するので廃止(void Update()でゲーム終了判定実施)
            }
            else if (gameEndCondition == GameEndCondition.Score)
            {
                SendCustomEventDelayedSeconds("DelayedSecond_CheckWinner_ScoreMode", CheckWinnerInterval);//一定間隔で終了条件を満たしているか確認する(実際の処理に入れるのはGameManagerのOwnerのみ)
            }
            else if (gameEndCondition == GameEndCondition.Ticket)
            {
                SendCustomEventDelayedSeconds("DelayedSecond_CheckWinner_TicketMode", CheckWinnerInterval);//一定間隔で終了条件を満たしているか確認する(実際の処理に入れるのはGameManagerのOwnerのみ)
            }
            else if (gameEndCondition == GameEndCondition.FlagCarrier)
            {
                for (int i = 0; i < flags.Length; ++i)
                {
                    flags[i].SetRandomSpawnPointNumber();
                    //flags[i].RespawnFlag();
                } //フラッグをスポーンさせる.実際の処理はFlagのOwnerのみ実行.
            }
            else if (gameEndCondition == GameEndCondition.Conquest)
            {
                SendCustomEventDelayedSeconds("DelayedSecond_CheckWinner_Conquest", CheckWinnerInterval);//一定間隔で終了条件を満たしているか確認する(実際の処理に入れるのはGameManagerのOwnerのみ)
            }
        }
        //---------------------------------------
        public void DelayedSecond_CheckWinner_ScoreMode()
        {
            //一定間隔でスコアモードの終了条件を満たしているか確認する
            //実際のチェックはGameManagerのOwnerのみが実施する
            //ゲーム中のOwner権限移譲があった場合を考慮して、Owner以外も関数は定期的に呼び出す

            if (!SyncedInBattle || !SyncedInGame) { return; }    //戦闘が終わっていれば戦闘終了のチェックを強制終了する

            SendCustomEventDelayedSeconds("DelayedSecond_CheckWinner_ScoreMode", CheckWinnerInterval);   //次のチェックを予約

            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                //勝利チーム・プレイヤーを判定する
                //winnerTeam == TeamName.None以外であればゲーム終了
                bool gameEnd = false;
                if (teamMode == TeamMode.TeamBattle)
                {
                    TeamName winnerTeam = scoreManager.CheckWinnerTeam_ScoreMode();
                    if (winnerTeam == TeamName.None) { return; }
                    else
                    {
                        gameEnd = true;
                        SyncedWinnerTeamName = scoreManager.CastTeamNameToInt(winnerTeam);    //チーム戦モードの勝者確定
                    }
                }
                else
                {
                    PlayerHitBox winnerPlayerHitBox = scoreManager.CheckWinnerPlayer_ScoreMode();
                    if (!winnerPlayerHitBox) { return; }
                    else
                    {
                        gameEnd = true;
                        SyncedWinnerPlayerHitBox = scoreManager.CastPlayerHitBoxToInt(winnerPlayerHitBox);
                    }
                }

                if (gameEnd)
                {
                    //勝者判定データを同期する(Ownershipは取得済み、下記の2変数が同期される)
                    //SyncedWinnerTeamName
                    //SyncedWinnerPlayerHitBox
                    Sync();

                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_EndBattle");
                }
            }
        }
        //---------------------------------------
        public void DelayedSecond_CheckWinner_TicketMode()
        {
            //一定間隔でチケットモードの終了条件を満たしているか確認する
            //実際のチェックはGameManagerのOwnerのみが実施する
            //ゲーム中のOwner権限移譲があった場合を考慮して、Owner以外も関数は定期的に呼び出す

            if (!SyncedInBattle || !SyncedInGame) { return; }    //戦闘が終わっていれば戦闘終了のチェックを強制終了する

            SendCustomEventDelayedSeconds("DelayedSecond_CheckWinner_TicketMode", CheckWinnerInterval);   //次のチェックを予約

            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                //勝利チーム・プレイヤーを判定する
                //winnerTeam == TeamName.None以外であればゲーム終了
                bool gameEnd = false;
                if (teamMode == TeamMode.TeamBattle)
                {
                    TeamName winnerTeam = scoreManager.CheckWinnerTeam_TicketMode();
                    if (winnerTeam == TeamName.None) { return; }
                    else
                    {
                        gameEnd = true;
                        SyncedWinnerTeamName = scoreManager.CastTeamNameToInt(winnerTeam);    //チーム戦モードの勝者確定
                    }
                }
                else
                {
                    if (scoreManager.CountAlivePlayer_TicketMode() <= 0)
                    {
                        gameEnd = true;
                        SyncedWinnerPlayerHitBox = -1;
                    }
                    else
                    {
                        PlayerHitBox winnerPlayerHitBox = scoreManager.CheckWinnerPlayer_TicketMode();
                        if (!winnerPlayerHitBox) { return; }
                        else
                        {
                            gameEnd = true;
                            SyncedWinnerPlayerHitBox = scoreManager.CastPlayerHitBoxToInt(winnerPlayerHitBox);
                        }
                    }
                }

                if (gameEnd)
                {
                    //勝者判定データを同期する(Ownershipは取得済み、下記の2変数が同期される)
                    //SyncedWinnerTeamName
                    //SyncedWinnerPlayerHitBox
                    Sync();

                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_EndBattle");
                }
            }
        }
        //---------------------------------------
        public void ShowMyReviveTicket()
        {
            string str;
            if (!playerManager.playerHitBox) { str = "-"; }
            else { str = playerManager.playerHitBox.SyncedReviveTicket.ToString(); }
            for (int i = 0; i < MyReviveTicketText.Length; ++i) { if (MyReviveTicketText[i]) { MyReviveTicketText[i].text = str; } }
        }
        //---------------------------------------
        public void CheckWinner_DestroyMode()
        {
            //Botが破壊された直後に、ゲーム終了条件を満たしているか確認する
            //実際のチェックはGameManagerのOwnerのみが実施する

            if (gameEndCondition != GameEndCondition.DestroyTarget) { return; }

            if (!SyncedInBattle || !SyncedInGame) { return; }    //戦闘が終わっていれば戦闘終了のチェックを強制終了する

            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                //勝利チーム・プレイヤーを判定する
                //winnerTeam == TeamName.None以外であればゲーム終了
                bool gameEnd = false;
                if (teamMode == TeamMode.TeamBattle)
                {
                    TeamName winnerTeam = scoreManager.CheckWinnerTeam_DestoryMode();
                    if (winnerTeam == TeamName.None) { return; }
                    else
                    {
                        gameEnd = true;
                        SyncedWinnerTeamName = scoreManager.CastTeamNameToInt(winnerTeam);    //チーム戦モードの勝者確定
                    }
                }
                else
                {
                    //デストロイモードでFFA戦は想定していない
                }

                if (gameEnd)
                {
                    //勝者判定データを同期する(Ownershipは取得済み、下記の2変数が同期される)
                    //SyncedWinnerTeamName
                    //SyncedWinnerPlayerHitBox
                    Sync();

                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_EndBattle");
                }
            }
        }
        //---------------------------------------
        public void CheckWinner_FlagCarrierMode()
        {
            //フラッグが納品された直後に、ゲーム終了条件を満たしているか確認する
            //実際のチェックはGameManagerのOwnerのみが実施する

            if (!SyncedInBattle || !SyncedInGame) { return; }    //戦闘が終わっていれば戦闘終了のチェックを強制終了する

            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                //勝利チーム・プレイヤーを判定する
                //winnerTeam == TeamName.None以外であればゲーム終了
                bool gameEnd = false;
                if (teamMode == TeamMode.TeamBattle)
                {
                    TeamName winnerTeam = scoreManager.CheckWinnerTeam_FlagCarrierMode();
                    if (winnerTeam == TeamName.None) { return; }
                    else
                    {
                        gameEnd = true;
                        SyncedWinnerTeamName = scoreManager.CastTeamNameToInt(winnerTeam);    //チーム戦モードの勝者確定
                    }
                }
                else
                {
                    //フラッグ制でFFA戦は想定していない
                }

                if (gameEnd)
                {
                    //勝者判定データを同期する(Ownershipは取得済み、下記の2変数が同期される)
                    //SyncedWinnerTeamName
                    //SyncedWinnerPlayerHitBox
                    Sync();

                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_EndBattle");
                }
            }
        }
        //---------------------------------------
        public void DelayedSecond_CheckWinner_Conquest()
        {
            //一定間隔でコンクエストモードの終了条件を満たしているか確認する
            //合わせてプレイヤーがゾーン内にいるか確認し、ゾーン内であれば加点する
            //実際の終了チェックはGameManagerのOwnerのみが実施する
            //ゲーム中のOwner権限移譲があった場合を考慮して、Owner以外も関数は定期的に呼び出す

            if (gameEndCondition == GameEndCondition.Conquest && playerManager.playerHitBox)
            {
                scoreManager.CalculateConquestScore();
                if (teamMode == TeamMode.TeamBattle) { ShowTeamConquestScore(); }
                else { ShowFreeForAllConquestScore(); }

                playerManager.playerHitBox.TrySetOwner(playerManager.playerHitBox.gameObject);
                for (int i = 0; i < conquestZone.Length; ++i) { playerManager.playerHitBox.SyncedConquestScore += conquestZone[i].CalculateAddScore() * CheckWinnerInterval; }
                playerManager.playerHitBox.Sync();
            }

            if (!SyncedInBattle || !SyncedInGame) { return; }    //戦闘が終わっていれば戦闘終了のチェックを強制終了する

            SendCustomEventDelayedSeconds("DelayedSecond_CheckWinner_Conquest", CheckWinnerInterval);   //次のチェックを予約

            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                //勝利チーム・プレイヤーを判定する
                //winnerTeam == TeamName.None以外であればゲーム終了
                bool gameEnd = false;
                if (teamMode == TeamMode.TeamBattle)
                {
                    TeamName winnerTeam = scoreManager.CheckWinnerTeam_ConquestMode();
                    if (winnerTeam == TeamName.None) { return; }
                    else
                    {
                        gameEnd = true;
                        SyncedWinnerTeamName = scoreManager.CastTeamNameToInt(winnerTeam);    //チーム戦モードの勝者確定
                    }
                }
                else
                {
                    PlayerHitBox winnerPlayerHitBox = scoreManager.CheckWinnerPlayer_ConquestMode();
                    if (!winnerPlayerHitBox) { return; }
                    else
                    {
                        gameEnd = true;
                        SyncedWinnerPlayerHitBox = scoreManager.CastPlayerHitBoxToInt(winnerPlayerHitBox);
                    }
                }

                if (gameEnd)
                {
                    //勝者判定データを同期する(Ownershipは取得済み、下記の2変数が同期される)
                    //SyncedWinnerTeamName
                    //SyncedWinnerPlayerHitBox
                    Sync();

                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_EndBattle");
                }
            }
        }
        //---------------------------------------
        public void ShowFreeForAllConquestScore()
        {
            if (!playerManager.playerHitBox) { return; }

            for (int i = 0; i < Assigners.Length; ++i)
            {
                if (Assigners[i].playerHitBox == playerManager.playerHitBox)
                {
                    for (int j = 0; j < MyConquestTimeText.Length; ++j) { if (MyConquestTimeText[j]) { MyConquestTimeText[j].text = Assigners[i].playerHitBox.SyncedConquestScore.ToString("F2"); } }
                }
            }
        }
        //---------------------------------------
        public void ShowTeamConquestScore()
        {
            if (!playerManager.playerHitBox) { for (int i = 0; i < MyConquestTimeText.Length; ++i) { if (MyConquestTimeText[i]) { MyConquestTimeText[i].text = "-"; } } }
            else if (playerManager.playerHitBox.teamName == TeamName.A) { for (int i = 0; i < MyConquestTimeText.Length; ++i) { if (MyConquestTimeText[i]) { MyConquestTimeText[i].text = scoreManager.TeamA_ConquestScoreSum.ToString("F2"); } } }
            else if (playerManager.playerHitBox.teamName == TeamName.B) { for (int i = 0; i < MyConquestTimeText.Length; ++i) { if (MyConquestTimeText[i]) { MyConquestTimeText[i].text = scoreManager.TeamB_ConquestScoreSum.ToString("F2"); } } }
            else if (playerManager.playerHitBox.teamName == TeamName.C) { for (int i = 0; i < MyConquestTimeText.Length; ++i) { if (MyConquestTimeText[i]) { MyConquestTimeText[i].text = scoreManager.TeamC_ConquestScoreSum.ToString("F2"); } } }
            else if (playerManager.playerHitBox.teamName == TeamName.D) { for (int i = 0; i < MyConquestTimeText.Length; ++i) { if (MyConquestTimeText[i]) { MyConquestTimeText[i].text = scoreManager.TeamD_ConquestScoreSum.ToString("F2"); } } }
            else { for (int i = 0; i < MyConquestTimeText.Length; ++i) { if (MyConquestTimeText[i]) { MyConquestTimeText[i].text = "Error"; } } }
        }
        //---------------------------------------
        public void DelayedSecond_TimerEnd()
        {
            SendBattleEndMessage_TimerMode();
        }
        //---------------------------------------
        public void SendBattleEndMessage_TimerMode()
        {
            //Timerモードのみ実行

            //Ownerのみ処理実施
            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_EndBattle");

                //ここで勝者を判定し、同期変数をセットする
                if (teamMode == TeamMode.TeamBattle)
                {
                    SyncedWinnerTeamName = scoreManager.CastTeamNameToInt(scoreManager.CheckWinnerTeam_TimerMode());
                }
                else
                {
                    SyncedWinnerPlayerHitBox = scoreManager.CastPlayerHitBoxToInt(scoreManager.CheckWinnerPlayer_TimerMode());
                }

                //勝者判定データを同期する(Ownershipは取得済み、下記の2変数が同期される)
                //SyncedWinnerTeamName
                //SyncedWinnerPlayerHitBox
                Sync();
            }
        }
        //---------------------------------------
        public void ForceEndBattle()
        {
            if(!SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_EndBattle");
        }
        //---------------------------------------
        public void Network_EndBattle()
        {
            EndBattle();
        }
        //---------------------------------------
        public void EndBattle()
        {
            battleEndTime = Time.time;

            for (int i = 0; i < OnEndBattleAnimators.Length; ++i) { if (OnEndBattleAnimators[i]) { OnEndBattleAnimators[i].SetTrigger("EndBattle"); } }

            //GameManagerのOwnerはゲーム状態を更新
            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                SyncedInBattle = false;
                Sync();
            }

            //ドロップアイテムを削除
            if (dropItems.Length > 0)
            {
                Transform dropItemParentTransform = dropItems[0].transform.parent;
                foreach (Transform child in dropItemParentTransform)
                {
                    if (child.GetComponent<DropItem>())
                    {
                        DropItem d = child.GetComponent<DropItem>();
                        d.EndBattle();
                    }
                }
            }

            musicManager.FadeOut(BattleEndDuration);    //音楽をフェードアウト
            SendCustomEventDelayedSeconds("DelayedSecond_EndGame", BattleEndDuration);//一定時間後にゲーム終了処理(SendBattleEndMessageのタイミングで、Ownerが勝者判定と同期を実施済み)
            if (BattleEndClip) { this.GetComponent<AudioSource>().PlayOneShot(BattleEndClip, 1.0f); }
        }
        //---------------------------------------
        public void DelayedSecond_EndGame()
        {
            EndGame();
        }
        //---------------------------------------
        public void EndGame()
        {
            if (playerManager.playerHitBox && hud) { hud.DisableHUD(); }

            PlayEndGameEffect();
            playerManager.EnableAllWeapons();

            for (int i = 0; i < OnGameEndSpawnObjects.Length; ++i) { if (OnGameEndSpawnObjects[i]) { OnGameEndSpawnObjects[i].SetActive(true); } }

            for (int i = 0; i < OnEndGameAnimators.Length; ++i) { if (OnEndGameAnimators[i]) { OnEndGameAnimators[i].SetTrigger("EndGame"); } }

            for (int i = 0; i < Assigners.Length; ++i) { if (Assigners[i]) { Assigners[i].playerHitBox.ResetOnEndGame(); } }

            //ゲームに登録している場合は、勝者・敗者それぞれの地点にテレポートする
            //ゲームに登録していない場合はテレポートしない
            if (playerManager.playerHitBox)
            {
                if (CheckLocalPlayerIsWinner()) { TeleportAndSetRespawnPoint(WinnerRespawnTransform); }
                else { TeleportAndSetRespawnPoint(LoserRespawnTransform); }
            }

            //Ownerのみゲーム状態同期の処理を実施
            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                SyncedGameTime = battleEndTime - battleStartTime;

                if (!DisableRestart)
                {
                    //ゲーム状態を同期する(Ownershipは取得済み)
                    SyncedInGame = false;
                    SyncedInBattle = false;
                    Sync();
                }
                else
                {
                    //2回目のゲーム開始を禁止する場合はゲーム状態をそのままにする(ダンジョン攻略系のゲームなど)
                    //SyncedInGame == true && SyncedInBattle == trueでないと次のゲーム開始処理を行えない
                    Sync();
                }
            }
        }
        //---------------------------------------
        public bool CheckLocalPlayerIsWinner()
        {
            //playerManager.playerHitBoxはnullでない前提でよい
            if (teamMode == TeamMode.TeamBattle)
            {
                if (playerManager.playerHitBox.teamName == scoreManager.CastIntToTeamName(SyncedWinnerTeamName)) { return true; }
                else { return false; }
            }
            else if (teamMode == TeamMode.FreeForAll)
            {
                if (playerManager.playerHitBox == scoreManager.CastIntToPlayerHitBox(SyncedWinnerPlayerHitBox)) { return true; }
                else { return false; }
            }
            else { return false; }
        }
        //---------------------------------------
        public void PlayEndGameEffect()
        {
            //勝者の名前を表示する
            if (WinnerNameText)
            {
                string Prefix = WinnerPrefix;
                string WinnerName;
                string Suffix = WinnerSuffix;

                if (teamMode == TeamMode.TeamBattle)
                {
                    if (scoreManager.CastIntToTeamName(SyncedWinnerTeamName) == TeamName.A) { WinnerName = TeamA_CustomName; }
                    else if (scoreManager.CastIntToTeamName(SyncedWinnerTeamName) == TeamName.B) { WinnerName = TeamB_CustomName; }
                    else if (scoreManager.CastIntToTeamName(SyncedWinnerTeamName) == TeamName.C) { WinnerName = TeamC_CustomName; }
                    else if (scoreManager.CastIntToTeamName(SyncedWinnerTeamName) == TeamName.D) { WinnerName = TeamD_CustomName; }
                    else { Prefix = ""; WinnerName = DrawText; Suffix = ""; }
                }
                else
                {
                    int WinnerPlayerID;
                    if (!scoreManager.CastIntToPlayerHitBox(SyncedWinnerPlayerHitBox)) { WinnerPlayerID = -1; }
                    else { WinnerPlayerID = scoreManager.CastIntToPlayerHitBox(SyncedWinnerPlayerHitBox).SyncedPlayerID; }
                    if (WinnerPlayerID == -1) { Prefix = ""; WinnerName = DrawText; Suffix = ""; }
                    else if (Utilities.IsValid(VRCPlayerApi.GetPlayerById(WinnerPlayerID)))
                    {
                        WinnerName = VRCPlayerApi.GetPlayerById(WinnerPlayerID).displayName;
                    }
                    else
                    {
                        WinnerName = "N/A";
                    }
                }

                WinnerNameText.text = Prefix + WinnerName + Suffix;
            }

            if (OnGameEndParticle) { OnGameEndParticle.Play(); }
            musicManager.PlayGameEndMusicOneShot();
            musicManager.SendCustomEventDelayedSeconds("DelayedSecond_PlayLobbyMusic", musicManager.GameEndMusicDuration);
        }
        //---------------------------------------
        public void TeleportAndSetRespawnPoint(Transform newRespawnPoint)
        {
            RespawnTransform.SetPositionAndRotation(newRespawnPoint.position, newRespawnPoint.rotation);
            if (Utilities.IsValid(Networking.LocalPlayer))
            {
                Networking.LocalPlayer.TeleportTo(RespawnTransform.position, RespawnTransform.rotation);
                Networking.LocalPlayer.SetVelocity(Vector3.zero);
            }
        }
        //---------------------------------------
        public bool CheckUnAssignedPlayer()
        {
            //FFA戦でインスタンス内に未登録のプレイヤーがいる場合はfalse
            //それ以外であればtrue

            int instancePlayerNum = 0;
            VRCPlayerApi[] InInstancePlayers = new VRCPlayerApi[MaxInstancePlayerNum];
            VRCPlayerApi.GetPlayers(InInstancePlayers);
            for (int i = 0; i < InInstancePlayers.Length; ++i) { if (Utilities.IsValid(InInstancePlayers[i])) { ++instancePlayerNum; } }

            int assignedPlayerNum = 0;
            for (int i = 0; i < Assigners.Length; ++i) { if (Assigners[i].playerHitBox.SyncedPlayerID > 0) { ++assignedPlayerNum; } }

            if (instancePlayerNum > assignedPlayerNum)
            {
                //登録していないプレイヤーがいる
                if (StartChecker) { StartChecker.SetActive(true); }
                return false;
            }

            return true;
        }
        //---------------------------------------
        public bool CheckUnAssignedTeam()
        {
            //チーム戦でプレイヤーがいないチームがいる場合はfalse
            //インスタンス内に未登録のプレイヤーがいる場合はfalse
            //それ以外であればtrue

            int instancePlayerNum = 0;
            VRCPlayerApi[] InInstancePlayers = new VRCPlayerApi[MaxInstancePlayerNum];
            VRCPlayerApi.GetPlayers(InInstancePlayers);
            for (int i = 0; i < InInstancePlayers.Length; ++i) { if (Utilities.IsValid(InInstancePlayers[i])) { ++instancePlayerNum; } }

            int assignedPlayerNum = 0;
            for (int i = 0; i < Assigners.Length; ++i) { if (Assigners[i].playerHitBox.SyncedPlayerID > 0) { ++assignedPlayerNum; } }

            if (instancePlayerNum > assignedPlayerNum)
            {
                //登録していないプレイヤーがいる
                if (StartChecker) { StartChecker.SetActive(true); }
                return false;
            }

            bool TeamA_Ready = false;
            PlayerHitBox[] PlayerHitBox_Team_A = scoreManager.PlayerHitBox_Team_A;
            if (PlayerHitBox_Team_A.Length <= 0) { TeamA_Ready = true; }
            else { for (int i = 0; i < PlayerHitBox_Team_A.Length; ++i) { if (PlayerHitBox_Team_A[i].SyncedPlayerID > 0) { TeamA_Ready = true; } } }

            bool TeamB_Ready = false;
            PlayerHitBox[] PlayerHitBox_Team_B = scoreManager.PlayerHitBox_Team_B;
            if (PlayerHitBox_Team_B.Length <= 0) { TeamB_Ready = true; }
            else { for (int i = 0; i < PlayerHitBox_Team_B.Length; ++i) { if (PlayerHitBox_Team_B[i].SyncedPlayerID > 0) { TeamB_Ready = true; } } }

            bool TeamC_Ready = false;
            PlayerHitBox[] PlayerHitBox_Team_C = scoreManager.PlayerHitBox_Team_C;
            if (PlayerHitBox_Team_C.Length <= 0) { TeamC_Ready = true; }
            else { for (int i = 0; i < PlayerHitBox_Team_C.Length; ++i) { if (PlayerHitBox_Team_C[i].SyncedPlayerID > 0) { TeamC_Ready = true; } } }

            bool TeamD_Ready = false;
            PlayerHitBox[] PlayerHitBox_Team_D = scoreManager.PlayerHitBox_Team_D;
            if (PlayerHitBox_Team_D.Length <= 0) { TeamD_Ready = true; }
            else { for (int i = 0; i < PlayerHitBox_Team_D.Length; ++i) { if (PlayerHitBox_Team_D[i].SyncedPlayerID > 0) { TeamA_Ready = true; } } }

            if (TeamA_Ready && TeamB_Ready && TeamC_Ready && TeamD_Ready)
            {
                //全員が登録済み/両チームに登録者がいる
                StartChecker.SetActive(false);
                return true;
            }

            //登録者がいないチームがいる
            StartChecker.SetActive(true);

            return false;
        }
        //---------------------------------------
        public void ResetAllWeapons()
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_ResetAllWeaponTransform");
        }
        //---------------------------------------
        public void Network_ResetAllWeaponTransform()
        {
            for (int i = 0; i < rangedWeapon_MainModules.Length; ++i)
            {
                if (rangedWeapon_MainModules[i])
                {
                    rangedWeapon_MainModules[i].ResetTransform();
                    rangedWeapon_MainModules[i].ResetMagazine();
                }
            }

            for (int i = 0; i < meleeWeapons.Length; ++i)
            {
                if (meleeWeapons[i]) { meleeWeapons[i].ResetTransform(); }
            }

            for (int i = 0; i < exWeapons.Length; ++i)
            {
                if (exWeapons[i]) { exWeapons[i].ResetTransform(); }
            }
        }
        //---------------------------------------
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            if (OwnerNameText)
            {
                //Owner移譲のラグを考慮し、数秒待ってからOwner名の表示を行う
                SendCustomEventDelayedSeconds("DelayedSecond_ShowOwnerPlayerName", CheckWinnerInterval);
            }
        }
        //---------------------------------------
        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            if (OwnerNameText)
            {
                //Owner移譲のラグを考慮し、数秒待ってからOwner名の表示を行う
                SendCustomEventDelayedSeconds("DelayedSecond_ShowOwnerPlayerName", CheckWinnerInterval);
            }
        }
        //---------------------------------------
        public void DelayedSecond_ShowOwnerPlayerName()
        {
            ShowOwnerPlayerName();
        }
        //---------------------------------------
        public void Network_ShowOwnerPlayerName()
        {
            ShowOwnerPlayerName();
        }
        //---------------------------------------
        public void ShowOwnerPlayerName()
        {
            OwnerNameText.text = "Master : " + Networking.GetOwner(this.gameObject).displayName;
        }
        //---------------------------------------
        public void ForceTakeOwnership()
        {
            if (SyncedInBattle || SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            if (!Utilities.IsValid(Networking.LocalPlayer))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            const float delay = 1.0f;
            TrySetOwner(this.gameObject);
            if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
            if (StartChecker) { StartChecker.SetActive(false); }
            SendCustomEventDelayedSeconds("DelayedSecond_OwnershipTaken", delay);
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_HideStartChecker");
            return;
        }
        //---------------------------------------
        public void DelayedSecond_OwnershipTaken()
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_ShowOwnerPlayerName");
        }
        //---------------------------------------
        public void Network_HideStartChecker()
        {
            HideStartChecker();
        }
        //---------------------------------------
        public void HideStartChecker()
        {
            if (StartChecker) { StartChecker.SetActive(false); }
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
            //ゲーム中かどうかのフラグと、ゲームルールを同期変数で管理する
            //実際のゲーム進行は、SendCustomNetworkEventで管理する

            teamMode = CastIntToSyncedTeamMode(SyncedTeamMode);
            gameEndCondition = CastIntToSyncedGameEndCondition(SyncedGameEndCondition);
            transceiverMode = CastIntToSyncedTransceiverMode(SyncedTransceiverMode);

            if (TeamModeText)
            {
                if (teamMode == TeamMode.TeamBattle) { TeamModeText.text = TeamBattleMode_CustomName; }
                else if (teamMode == TeamMode.FreeForAll) { TeamModeText.text = FreeForAllMode_CustomName; }
                else if (teamMode == TeamMode.None) { TeamModeText.text = "TeamMode:エラー"; }
            }
            if (GameEndConditionText)
            {
                if (gameEndCondition == GameEndCondition.Score) { GameEndConditionText.text = ScoreMode_CustomName; }
                else if (gameEndCondition == GameEndCondition.Ticket) { GameEndConditionText.text = TicketMode_CustomName; }
                else if (gameEndCondition == GameEndCondition.Timer) { GameEndConditionText.text = TimerMode_CustomName; }
                else if (gameEndCondition == GameEndCondition.DestroyTarget) { GameEndConditionText.text = DestroyTargetMode_CustomName; }
                else if (gameEndCondition == GameEndCondition.FlagCarrier) { GameEndConditionText.text = FlagCarrierMode_CustomName; }
                else if (gameEndCondition == GameEndCondition.Conquest) { GameEndConditionText.text = ConquestMode_CustomName; }
                else if (gameEndCondition == GameEndCondition.None) { GameEndConditionText.text = "GameEndCondition:エラー"; }
            }
            ShowGameTime();

            //ゲームモードに合わせたオブジェクトを表示
            if (gameEndCondition == GameEndCondition.Score)
            {
                for (int i = 0; i < EnableOnScoreModeObjects.Length; ++i)
                {
                    if (EnableOnScoreModeObjects[i]) { EnableOnScoreModeObjects[i].SetActive(true); }
                }
                for (int i = 0; i < EnableOnTicketModeObjects.Length; ++i)
                {
                    if (EnableOnTicketModeObjects[i]) { EnableOnTicketModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnTimerModeObjects.Length; ++i)
                {
                    if (EnableOnTimerModeObjects[i]) { EnableOnTimerModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnDestroyModeObjects.Length; ++i)
                {
                    if (EnableOnDestroyModeObjects[i]) { EnableOnDestroyModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnFlagModeObjects.Length; ++i)
                {
                    if (EnableOnFlagModeObjects[i]) { EnableOnFlagModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnConquestModeObjects.Length; ++i)
                {
                    if (EnableOnConquestModeObjects[i]) { EnableOnConquestModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < MaxScoreText.Length; ++i) { if (MaxScoreText[i]) { MaxScoreText[i].text = SyncedMaxScore.ToString(); } }

            }
            else if (gameEndCondition == GameEndCondition.Ticket)
            {
                for (int i = 0; i < EnableOnScoreModeObjects.Length; ++i)
                {
                    if (EnableOnScoreModeObjects[i]) { EnableOnScoreModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnTicketModeObjects.Length; ++i)
                {
                    if (EnableOnTicketModeObjects[i]) { EnableOnTicketModeObjects[i].SetActive(true); }
                }
                for (int i = 0; i < EnableOnTimerModeObjects.Length; ++i)
                {
                    if (EnableOnTimerModeObjects[i]) { EnableOnTimerModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnDestroyModeObjects.Length; ++i)
                {
                    if (EnableOnDestroyModeObjects[i]) { EnableOnDestroyModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnFlagModeObjects.Length; ++i)
                {
                    if (EnableOnFlagModeObjects[i]) { EnableOnFlagModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnConquestModeObjects.Length; ++i)
                {
                    if (EnableOnConquestModeObjects[i]) { EnableOnConquestModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < MaxReviveTicketText.Length; ++i) { if (MaxReviveTicketText[i]) { MaxReviveTicketText[i].text = SyncedMaxReviveTicket.ToString(); } }
            }
            else if (gameEndCondition == GameEndCondition.Timer)
            {
                for (int i = 0; i < EnableOnScoreModeObjects.Length; ++i)
                {
                    if (EnableOnScoreModeObjects[i]) { EnableOnScoreModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnTicketModeObjects.Length; ++i)
                {
                    if (EnableOnTicketModeObjects[i]) { EnableOnTicketModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnTimerModeObjects.Length; ++i)
                {
                    if (EnableOnTimerModeObjects[i]) { EnableOnTimerModeObjects[i].SetActive(true); }
                }
                for (int i = 0; i < EnableOnDestroyModeObjects.Length; ++i)
                {
                    if (EnableOnDestroyModeObjects[i]) { EnableOnDestroyModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnFlagModeObjects.Length; ++i)
                {
                    if (EnableOnFlagModeObjects[i]) { EnableOnFlagModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnConquestModeObjects.Length; ++i)
                {
                    if (EnableOnConquestModeObjects[i]) { EnableOnConquestModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < BattleDurationText.Length; ++i) { if (BattleDurationText[i]) { BattleDurationText[i].text = SyncedBattleDuration.ToString(); } }
            }
            else if (gameEndCondition == GameEndCondition.DestroyTarget)
            {
                for (int i = 0; i < EnableOnScoreModeObjects.Length; ++i)
                {
                    if (EnableOnScoreModeObjects[i]) { EnableOnScoreModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnTicketModeObjects.Length; ++i)
                {
                    if (EnableOnTicketModeObjects[i]) { EnableOnTicketModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnTimerModeObjects.Length; ++i)
                {
                    if (EnableOnTimerModeObjects[i]) { EnableOnTimerModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnDestroyModeObjects.Length; ++i)
                {
                    if (EnableOnDestroyModeObjects[i]) { EnableOnDestroyModeObjects[i].SetActive(true); }
                }
                for (int i = 0; i < EnableOnFlagModeObjects.Length; ++i)
                {
                    if (EnableOnFlagModeObjects[i]) { EnableOnFlagModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnConquestModeObjects.Length; ++i)
                {
                    if (EnableOnConquestModeObjects[i]) { EnableOnConquestModeObjects[i].SetActive(false); }
                }
            }
            else if (gameEndCondition == GameEndCondition.FlagCarrier)
            {
                for (int i = 0; i < EnableOnScoreModeObjects.Length; ++i)
                {
                    if (EnableOnScoreModeObjects[i]) { EnableOnScoreModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnTicketModeObjects.Length; ++i)
                {
                    if (EnableOnTicketModeObjects[i]) { EnableOnTicketModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnTimerModeObjects.Length; ++i)
                {
                    if (EnableOnTimerModeObjects[i]) { EnableOnTimerModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnDestroyModeObjects.Length; ++i)
                {
                    if (EnableOnDestroyModeObjects[i]) { EnableOnDestroyModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnFlagModeObjects.Length; ++i)
                {
                    if (EnableOnFlagModeObjects[i]) { EnableOnFlagModeObjects[i].SetActive(true); }
                }
                for (int i = 0; i < EnableOnConquestModeObjects.Length; ++i)
                {
                    if (EnableOnConquestModeObjects[i]) { EnableOnConquestModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < MaxFlagScoreText.Length; ++i) { if (MaxFlagScoreText[i]) { MaxFlagScoreText[i].text = SyncedMaxFlagScore.ToString(); } }
            }
            else if (gameEndCondition == GameEndCondition.Conquest)
            {
                for (int i = 0; i < EnableOnScoreModeObjects.Length; ++i)
                {
                    if (EnableOnScoreModeObjects[i]) { EnableOnScoreModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnTicketModeObjects.Length; ++i)
                {
                    if (EnableOnTicketModeObjects[i]) { EnableOnTicketModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnTimerModeObjects.Length; ++i)
                {
                    if (EnableOnTimerModeObjects[i]) { EnableOnTimerModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnDestroyModeObjects.Length; ++i)
                {
                    if (EnableOnDestroyModeObjects[i]) { EnableOnDestroyModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnFlagModeObjects.Length; ++i)
                {
                    if (EnableOnFlagModeObjects[i]) { EnableOnFlagModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnConquestModeObjects.Length; ++i)
                {
                    if (EnableOnConquestModeObjects[i]) { EnableOnConquestModeObjects[i].SetActive(true); }
                }
                for (int i = 0; i < MaxConquestTimeText.Length; ++i) { if (MaxConquestTimeText[i]) { MaxConquestTimeText[i].text = SyncedMaxConquestTime.ToString(); } }
            }
            else if (gameEndCondition == GameEndCondition.None)
            {
                for (int i = 0; i < EnableOnScoreModeObjects.Length; ++i)
                {
                    if (EnableOnScoreModeObjects[i]) { EnableOnScoreModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnTicketModeObjects.Length; ++i)
                {
                    if (EnableOnTicketModeObjects[i]) { EnableOnTicketModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnTimerModeObjects.Length; ++i)
                {
                    if (EnableOnTimerModeObjects[i]) { EnableOnTimerModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnDestroyModeObjects.Length; ++i)
                {
                    if (EnableOnDestroyModeObjects[i]) { EnableOnDestroyModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnFlagModeObjects.Length; ++i)
                {
                    if (EnableOnFlagModeObjects[i]) { EnableOnFlagModeObjects[i].SetActive(false); }
                }
                for (int i = 0; i < EnableOnConquestModeObjects.Length; ++i)
                {
                    if (EnableOnConquestModeObjects[i]) { EnableOnConquestModeObjects[i].SetActive(false); }
                }
            }

            if(gameEndCondition != GameEndCondition.Conquest) { for (int i = 0; i < conquestZone.Length; ++i) { if (conquestZone[i]) { conquestZone[i].HideInZoneMarker(); } } }

            if(transceiverMode == TransceiverMode.None) { Network_NoTransceiver(); }
            else if (transceiverMode == TransceiverMode.Friend) { Network_FriendTransceiver(); }
            else { Network_AllTransceiver(); }
        }
        //---------------------------------------
        public void ShowGameTime()
        {
            int hour, second, minute;
            float remain1 = SyncedGameTime % 3600;
            hour = (int)((SyncedGameTime - remain1) / 3600);
            second = (int)remain1 % 60;
            minute = (int)((remain1 - second) / 60);

            for (int i = 0; i < SyncedGameTimetext.Length; ++i) { if (SyncedGameTimetext[i]) { SyncedGameTimetext[i].text = hour.ToString("D2") + ": " + minute.ToString("D2") + ": " + second.ToString("D2"); } }
        }
        //---------------------------------------
        public void Network_ShowFlagScore()
        {
            if (!playerManager.playerHitBox) { for (int i = 0; i < MyFlagScoreText.Length; ++i) { if (MyFlagScoreText[i]) { MyFlagScoreText[i].text = "-"; } } }
            else if (playerManager.playerHitBox.teamName == TeamName.A) { for (int i = 0; i < MyFlagScoreText.Length; ++i) { if (MyFlagScoreText[i]) { MyFlagScoreText[i].text = scoreManager.TeamA_FlagScore.ToString(); } } }
            else if (playerManager.playerHitBox.teamName == TeamName.B) { for (int i = 0; i < MyFlagScoreText.Length; ++i) { if (MyFlagScoreText[i]) { MyFlagScoreText[i].text = scoreManager.TeamB_FlagScore.ToString(); } } }
            else if (playerManager.playerHitBox.teamName == TeamName.C) { for (int i = 0; i < MyFlagScoreText.Length; ++i) { if (MyFlagScoreText[i]) { MyFlagScoreText[i].text = scoreManager.TeamC_FlagScore.ToString(); } } }
            else if (playerManager.playerHitBox.teamName == TeamName.D) { for (int i = 0; i < MyFlagScoreText.Length; ++i) { if (MyFlagScoreText[i]) { MyFlagScoreText[i].text = scoreManager.TeamD_FlagScore.ToString(); } } }
            else { for (int i = 0; i < MyFlagScoreText.Length; ++i) { if (MyFlagScoreText[i]) { MyFlagScoreText[i].text = "Error"; } } }
        }
        //---------------------------------------
        public void ResetScore()
        {
            //PlayerHitBoxのスコアのみをリセット
            for (int i = 0; i < Assigners.Length; ++i)
            {
                if (Assigners[i]) { Assigners[i].playerHitBox.ResetScore(); }
            }
        }
        //---------------------------------------
        public void EnableAllTransceiver()
        {
            if (!CheckLocalPlayerIsOwner(this.gameObject)) { return; }

            SyncedTransceiverMode = CastSyncedTransceiverModeToInt(TransceiverMode.All);
            Sync();
            //SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_AllTransceiver");
        }
        //---------------------------------------
        public void Network_AllTransceiver()
        {
            VRCPlayerApi[] InstancePlayers = new VRCPlayerApi[MaxInstancePlayerNum];
            VRCPlayerApi.GetPlayers(InstancePlayers);

            for (int i = 0; i < InstancePlayers.Length; ++i)
            {
                if (Utilities.IsValid(InstancePlayers[i]))
                {
                    InstancePlayers[i].SetVoiceDistanceNear(999999);
                    InstancePlayers[i].SetVoiceDistanceFar(1000000.0f);
                    InstancePlayers[i].SetVoiceGain(TransceiverGain);
                    InstancePlayers[i].SetVoiceVolumetricRadius(1000);
                }
            }

            if (AllTransceiverMarker) { AllTransceiverMarker.SetActive(true); }
            if (TeamTransceiverMarker) { TeamTransceiverMarker.SetActive(false); }
            if (NoTransceiverMarker) { NoTransceiverMarker.SetActive(false); }

            //if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public void EnableFriendTransceiver()
        {
            if (!CheckLocalPlayerIsOwner(this.gameObject)) { return; }
            SyncedTransceiverMode = CastSyncedTransceiverModeToInt(TransceiverMode.Friend);
            Sync();
            //SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_FriendTransceiver");
        }
        //---------------------------------------
        public void Network_FriendTransceiver()
        {
            VRCPlayerApi[] InstancePlayers = new VRCPlayerApi[MaxInstancePlayerNum];
            VRCPlayerApi.GetPlayers(InstancePlayers);

            for (int i = 0; i < InstancePlayers.Length; ++i)
            {
                if (Utilities.IsValid(InstancePlayers[i]))
                {
                    bool isFriend = false;
                    bool found = false;
                    for (int j = 0; j < Assigners.Length; ++j)
                    {
                        if (!found && Assigners[j].playerHitBox.SyncedPlayerID == InstancePlayers[i].playerId)
                        {
                            found = true;
                            isFriend = IsFriend(Assigners[j].playerHitBox);
                        }
                    }

                    if (isFriend)
                    {
                        InstancePlayers[i].SetVoiceDistanceNear(999999);
                        InstancePlayers[i].SetVoiceDistanceFar(1000000.0f);
                        InstancePlayers[i].SetVoiceGain(TransceiverGain);
                        InstancePlayers[i].SetVoiceVolumetricRadius(1000);
                    }
                    else
                    {
                        InstancePlayers[i].SetVoiceDistanceNear(0.0f);
                        InstancePlayers[i].SetVoiceDistanceFar(25.0f);
                        InstancePlayers[i].SetVoiceGain(15.0f);
                        InstancePlayers[i].SetVoiceVolumetricRadius(0);
                    }
                }
            }

            if (AllTransceiverMarker) { AllTransceiverMarker.SetActive(false); }
            if (TeamTransceiverMarker) { TeamTransceiverMarker.SetActive(true); }
            if (NoTransceiverMarker) { NoTransceiverMarker.SetActive(false); }

            //if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public bool IsFriend(PlayerHitBox OtherPlayerHitBox)
        {
            //味方チームのプレイヤーならtrueを返す
            //自分がゲームに登録していない場合は常にfalse
            if (!playerManager.playerHitBox) { return false; }

            TeamName OtherTeamName = OtherPlayerHitBox.teamName;
            TeamName MyTeamName = playerManager.playerHitBox.teamName;

            if (MyTeamName == OtherTeamName) { return true; }
            else { return false; }
        }
        //---------------------------------------
        public void DisableTransceiver()
        {
            if (!CheckLocalPlayerIsOwner(this.gameObject)) { return; }
            SyncedTransceiverMode = CastSyncedTransceiverModeToInt(TransceiverMode.None);
            Sync();
            //SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_NoTransceiver");
        }
        //---------------------------------------
        public void Network_NoTransceiver()
        {
            VRCPlayerApi[] InstancePlayers = new VRCPlayerApi[MaxInstancePlayerNum];
            VRCPlayerApi.GetPlayers(InstancePlayers);

            for (int i = 0; i < InstancePlayers.Length; ++i)
            {
                if (Utilities.IsValid(InstancePlayers[i]))
                {
                    InstancePlayers[i].SetVoiceDistanceNear(0.0f);
                    InstancePlayers[i].SetVoiceDistanceFar(25.0f);
                    InstancePlayers[i].SetVoiceGain(15.0f);
                    InstancePlayers[i].SetVoiceVolumetricRadius(0);
                }
            }

            if (AllTransceiverMarker) { AllTransceiverMarker.SetActive(false); }
            if (TeamTransceiverMarker) { TeamTransceiverMarker.SetActive(false); }
            if (NoTransceiverMarker) { NoTransceiverMarker.SetActive(true); }

            //if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public void SetScoreMode()
        {
            //ゲーム中はInteract不可
            if (SyncedInBattle || SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //GameManagerのOwnerでなければInteract不可
            if (!CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            SyncedGameEndCondition = CastSyncedGameEndConditionToInt(GameEndCondition.Score);
            Sync();

            if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public void SetTicketMode()
        {
            //ゲーム中はInteract不可
            if (SyncedInBattle || SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //GameManagerのOwnerでなければInteract不可
            if (!CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            SyncedGameEndCondition = CastSyncedGameEndConditionToInt(GameEndCondition.Ticket);
            Sync();

            if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public void SetTimerMode()
        {
            //ゲーム中はInteract不可
            if (SyncedInBattle || SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //GameManagerのOwnerでなければInteract不可
            if (!CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            SyncedGameEndCondition = CastSyncedGameEndConditionToInt(GameEndCondition.Timer);
            Sync();

            if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public void SetFlagMode()
        {
            //ゲーム中はInteract不可
            if (SyncedInBattle || SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //GameManagerのOwnerでなければInteract不可
            if (!CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            SyncedGameEndCondition = CastSyncedGameEndConditionToInt(GameEndCondition.FlagCarrier);
            Sync();

            if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public void SetConquestMode()
        {
            //ゲーム中はInteract不可
            if (SyncedInBattle || SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //GameManagerのOwnerでなければInteract不可
            if (!CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            SyncedGameEndCondition = CastSyncedGameEndConditionToInt(GameEndCondition.Conquest);
            Sync();

            if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public void SetDestroyTargetMode()
        {
            //ゲーム中はInteract不可
            if (SyncedInBattle || SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //GameManagerのOwnerでなければInteract不可
            if (!CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            SyncedGameEndCondition = CastSyncedGameEndConditionToInt(GameEndCondition.DestroyTarget);

            Sync();

            if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public void SetTeamBattleMode()
        {
            //ゲーム中はInteract不可
            if (SyncedInBattle || SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //GameManagerのOwnerでなければInteract不可
            if (!CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            SyncedTeamMode = CastSyncedTeamModeToInt(TeamMode.TeamBattle);
            Sync();

            if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public void SetFreeForAllMode()
        {
            //ゲーム中はInteract不可
            if (SyncedInBattle || SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //GameManagerのOwnerでなければInteract不可
            if (!CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            SyncedTeamMode = CastSyncedTeamModeToInt(TeamMode.FreeForAll);
            Sync();

            if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public void SetSyncedMaxScore(int newMaxScore)
        {
            if (!CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //ゲーム中はInteract不可
            if (SyncedInBattle || SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            SyncedGameEndCondition = CastSyncedGameEndConditionToInt(GameEndCondition.Score);
            SyncedMaxScore = newMaxScore;
            Sync();

            if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public void SetSyncedMaxReviveTicket(int newMaxReviveTicket)
        {
            if (!CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //ゲーム中はInteract不可
            if (SyncedInBattle || SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            SyncedGameEndCondition = CastSyncedGameEndConditionToInt(GameEndCondition.Ticket);
            SyncedMaxReviveTicket = newMaxReviveTicket;
            Sync();

            if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public void SetSyncedBattleDuration(int newBattleDuration)
        {
            if (!CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //ゲーム中はInteract不可
            if (SyncedInBattle || SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            SyncedGameEndCondition = CastSyncedGameEndConditionToInt(GameEndCondition.Timer);
            SyncedBattleDuration = newBattleDuration;
            Sync();

            if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public void SetSyncedMaxFlagScore(int newMaxFlagScore)
        {
            if (!CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //ゲーム中はInteract不可
            if (SyncedInBattle || SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            SyncedGameEndCondition = CastSyncedGameEndConditionToInt(GameEndCondition.FlagCarrier);
            SyncedMaxFlagScore = newMaxFlagScore;
            Sync();

            if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public void SetSyncedMaxConquestTime(int newMaxConquestTime)
        {
            if (!CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            //ゲーム中はInteract不可
            if (SyncedInBattle || SyncedInGame)
            {
                if (CancelClip) { this.GetComponent<AudioSource>().PlayOneShot(CancelClip, 1.0f); }
                return;
            }

            SyncedGameEndCondition = CastSyncedGameEndConditionToInt(GameEndCondition.Conquest);
            SyncedMaxConquestTime = newMaxConquestTime;
            Sync();

            if (SuccessClip) { this.GetComponent<AudioSource>().PlayOneShot(SuccessClip, 1.0f); }
        }
        //---------------------------------------
        public void ShowMyScore()
        {
            if (!playerManager.playerHitBox)
            {
                for (int i = 0; i < MyScoreText.Length; ++i)
                {
                    if (MyScoreText[i]) { MyScoreText[i].text = "-"; }
                }
            }
            else if (teamMode == TeamMode.FreeForAll)
            {
                for (int i = 0; i < MyScoreText.Length; ++i)
                {
                    if (MyScoreText[i]) { MyScoreText[i].text = playerManager.playerHitBox.SyncedScore.ToString("F1"); }
                }
            }
            else
            {
                if (playerManager.playerHitBox.teamName == TeamName.A)
                {
                    for (int i = 0; i < MyScoreText.Length; ++i)
                    {
                        if (MyScoreText[i]) { MyScoreText[i].text = scoreManager.TeamA_ScoreSum.ToString("F1"); }
                    }
                }
                else if (playerManager.playerHitBox.teamName == TeamName.B)
                {
                    for (int i = 0; i < MyScoreText.Length; ++i)
                    {
                        if (MyScoreText[i]) { MyScoreText[i].text = scoreManager.TeamB_ScoreSum.ToString("F1"); }
                    }
                }
                else if (playerManager.playerHitBox.teamName == TeamName.C)
                {
                    for (int i = 0; i < MyScoreText.Length; ++i)
                    {
                        if (MyScoreText[i]) { MyScoreText[i].text = scoreManager.TeamC_ScoreSum.ToString("F1"); }
                    }
                }
                else if (playerManager.playerHitBox.teamName == TeamName.D)
                {
                    for (int i = 0; i < MyScoreText.Length; ++i)
                    {
                        if (MyScoreText[i]) { MyScoreText[i].text = scoreManager.TeamD_ScoreSum.ToString("F1"); }
                    }
                }
                else
                {
                    for (int i = 0; i < MyScoreText.Length; ++i)
                    {
                        if (MyScoreText[i]) { MyScoreText[i].text = "Error"; }
                    }
                }
            }
        }
        //---------------------------------------
        public int CastSyncedTeamModeToInt(TeamMode _t)
        {
            return (int)_t;
        }
        //---------------------------------------
        public int CastSyncedGameEndConditionToInt(GameEndCondition _cond)
        {
            return (int)_cond;
        }
        //---------------------------------------
        public TeamMode CastIntToSyncedTeamMode(int _t)
        {
            return (TeamMode)_t;
        }
        //---------------------------------------
        public GameEndCondition CastIntToSyncedGameEndCondition(int _g)
        {
            return (GameEndCondition)_g;
        }
        //---------------------------------------
        public TransceiverMode CastIntToSyncedTransceiverMode(int _i)
        {
            return (TransceiverMode)_i;
        }
        //---------------------------------------
        public int CastSyncedTransceiverModeToInt(TransceiverMode _t)
        {
            return (int)_t;
        }
        //---------------------------------------
        public bool AutoBuild(Assigner[] _assigners, RangedWeapon_MainModule[] _rangedWeapon_MainModule, MeleeWeapon[] _meleeWeapons, ExWeapon[] _exWeapons, PlayerManager _playerManager, DropItem[] _dropItems, Flag[] _flags, ConquestZone[] _conquestZones, Bot[] _bots, Turret[] _turrets, HUD _hud)
        {
            playerManager = _playerManager;
            playerManager.gameManager = this.GetComponent<GameManager>();
            meleeWeapons = _meleeWeapons;
            exWeapons = _exWeapons;
            Assigners = _assigners;
            flags = _flags;
            conquestZone = _conquestZones;
            bots = _bots;
            turrets = _turrets;
            hud = _hud;

            int TeamA_Num = 0;
            int TeamB_Num = 0;
            int TeamC_Num = 0;
            int TeamD_Num = 0;
            for (int i = 0; i < Assigners.Length; ++i)
            {
                if (Assigners[i] && Assigners[i].teamName == TeamName.A) { ++TeamA_Num; }
                else if (Assigners[i] && Assigners[i].teamName == TeamName.B) { ++TeamB_Num; }
                else if (Assigners[i] && Assigners[i].teamName == TeamName.C) { ++TeamC_Num; }
                else if (Assigners[i] && Assigners[i].teamName == TeamName.D) { ++TeamD_Num; }
            }

            rangedWeapon_MainModules = new RangedWeapon_MainModule[_rangedWeapon_MainModule.Length];
            rangedWeapon_MainModules = _rangedWeapon_MainModule;

            dropItems = new DropItem[_dropItems.Length];
            dropItems = _dropItems;

            if (TeamA_RespawnTransform.Length < 1 && TeamA_Num > 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " チームAのスポーンポイントが設定されていません.", this.gameObject);
                Debug.Log("TeamA_RespawnTransformを確認してください", this.gameObject);
                return false;
            }
            if (TeamA_RespawnTransform.Length > 0)
            {
                for(int i = 0; i < TeamA_RespawnTransform.Length; ++i)
                {
                    if (!TeamA_RespawnTransform[i])
                    {
                        Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " チームAのスポーンポイントに未設定の項目があります.", this.gameObject);
                        Debug.Log("TeamA_RespawnTransformを確認してください", this.gameObject);
                        return false;
                    }
                }
            }
            if (TeamB_RespawnTransform.Length < 1 && TeamB_Num > 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " チームBのスポーンポイントが設定されていません.", this.gameObject);
                Debug.Log("TeamB_RespawnTransformを確認してください", this.gameObject);
                return false;
            }
            if (TeamB_RespawnTransform.Length > 0)
            {
                for (int i = 0; i < TeamB_RespawnTransform.Length; ++i)
                {
                    if (!TeamB_RespawnTransform[i])
                    {
                        Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " チームBのスポーンポイントに未設定の項目があります", this.gameObject);
                        Debug.Log("TeamB_RespawnTransformを確認してください", this.gameObject);
                        return false;
                    }
                }
            }
            if (TeamC_RespawnTransform.Length < 1 && TeamC_Num > 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " チームCのスポーンポイントが設定されていません.", this.gameObject);
                Debug.Log("TeamC_RespawnTransformを確認してください", this.gameObject);
                return false;
            }
            if (TeamC_RespawnTransform.Length > 0)
            {
                for (int i = 0; i < TeamC_RespawnTransform.Length; ++i)
                {
                    if (!TeamC_RespawnTransform[i])
                    {
                        Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " チームCのスポーンポイントに未設定の項目があります.", this.gameObject);
                        Debug.Log("TeamC_RespawnTransformを確認してください", this.gameObject);
                        return false;
                    }
                }
            }
            if (TeamD_RespawnTransform.Length < 1 && TeamD_Num > 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " チームDのスポーンポイントが設定されていません.", this.gameObject);
                Debug.Log("TeamD_RespawnTransformを確認してください", this.gameObject);
                return false;
            }
            if (TeamD_RespawnTransform.Length > 0)
            {
                for (int i = 0; i < TeamD_RespawnTransform.Length; ++i)
                {
                    if (!TeamD_RespawnTransform[i])
                    {
                        Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " チームDのスポーンポイントに未設定の項目があります.", this.gameObject);
                        Debug.Log("TeamD_RespawnTransformを確認してください", this.gameObject);
                        return false;
                    }
                }
            }
            if (FFA_RespawnTransform.Length < 1)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " バトルロワイヤルモードのスポーン地点が設定されていません", this.gameObject);
                Debug.Log("暫定策として、復活時は初期リスポーン地点に移動します.", this.gameObject);
                Debug.Log("FFA_RespawnTransformを確認してください", this.gameObject);
            }
            else
            {
                for (int i = 0; i < FFA_RespawnTransform.Length; ++i)
                {
                    if (!FFA_RespawnTransform[i])
                    {
                        Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " バトルロワイヤルモードのスポーン地点に未設定の項目があります.", this.gameObject);
                        Debug.Log("FFA_RespawnTransformを確認してください", this.gameObject);
                        return false;
                    }
                }
            }

            if (MaxPlayerHitPoint <= 0 && !SetMaxHitPointManually)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " MaxPlayerHitPointは0以上にしてください.", this.gameObject);
                return false;
            }

            else if (teamMode == TeamMode.None)
            {
                Debug.Log("<color=#ff0000>Warning:</color> teamModeがNoneになっています.", this.gameObject);
                return false;
            }

            else if (gameEndCondition == GameEndCondition.None)
            {
                Debug.Log("<color=#ff0000>Warning:</color> gameEndConditionがNoneになっています.", this.gameObject);
                return false;
            }

            SyncedTeamMode = CastSyncedTeamModeToInt(teamMode);
            SyncedGameEndCondition = CastSyncedGameEndConditionToInt(gameEndCondition);
            return true;
        }
        //---------------------------------------
        //ForDebugging
        public void UpdateTransceiverGain()
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_SetTransceiverGain");
        }
        //---------------------------------------
        //ForDebugging
        public void Network_SetTransceiverGain()
        {
            ++ TransceiverGain;
            if(TransceiverGain >= 10.0f) { TransceiverGain = 1.0f; }

            if (debugTransceiverGainText) { debugTransceiverGainText.text = "Gain=" + TransceiverGain.ToString("F0"); }
            Network_AllTransceiver();
        }
        //---------------------------------------
    }
}


