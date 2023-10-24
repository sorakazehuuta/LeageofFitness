
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace CombatGimmick
{
    public enum FitType
    {
        Head, Origin, Mean, Resize,
    }

    [ExecuteInEditMode]
    [DefaultExecutionOrder(30)]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class PlayerHitBox : HitBox
    {
        //---------------------------------------
        [Header("必須項目.無いとエラー")]
        [Tooltip("trueだとダメージを受けない")] [SerializeField] public bool Invincible;
        [Tooltip("プレイヤーの最大HP")] [SerializeField] public float MaxPlayerHitPoint;
        [Tooltip("プレイヤーの最大シールド")] [SerializeField] public float MaxPlayerShield;
        [Tooltip("プレイヤーの初期シールド")] [SerializeField] public float defaultPlayerShield;
        //---------------------------------------
        [Header("任意項目.無しでも動作可能")]
        [Tooltip("表示されるヒットボックスのモデル")] public GameObject HitBoxModelObject;
        [Tooltip("ジャンプ時の効果音")] [SerializeField] AudioClip JumpAudioClip;
        [Tooltip("着地時の効果音")] [SerializeField] AudioClip GroundedAudioClip;
        [Tooltip("ジャンプ時のエフェクト")] [SerializeField] ParticleSystem JumpParticle;
        [Tooltip("着地時のエフェクト")] [SerializeField] ParticleSystem GroundedParticle;
        [Tooltip("被弾時のエフェクト")] [SerializeField] ParticleSystem OnHitParticle;
        [Tooltip("プレイヤー撃破時のエフェクト")] [SerializeField] ParticleSystem OnKillParticle;
        //[Tooltip("ヒットボックスのレンダラー")] [SerializeField] MeshRenderer[] HitBoxRenderer;
        [Tooltip("登録したプレイヤーの主観視点で見えないオブジェクト")] public GameObject InvisibleToAssignedPlayerObject;
        //---------------------------------------
        [Header("----サイズ設定----")]
        [Tooltip("ヒットボックスを可変にするかどうか")] public FitType fitType;
        [Tooltip("最小のヒットボックス高さ.Resizeの場合のみ適用")] public float minHitBoxHeight;
        [Tooltip("ヒットボックスの[幅/高さ]の比率.Resizeの場合のみ適用")] public float hitBoxAspect;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("TeamName")] public TeamName teamName;
        [HideInInspector] [Tooltip("Assigner")] public Assigner assigner;
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        [HideInInspector] [Tooltip("ScoreManager")] public ScoreManager scoreManager;
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        [HideInInspector] [Tooltip("trueならジャンプ・着地時のエフェクトを同期する")] public bool GlobalJumpAndGroundedEffect;
        //---------------------------------------
        //[Header("手動で変更しないこと")]
        [HideInInspector] [Tooltip("現在のHP")] [UdonSynced] public float SyncedHitPoint;
        [HideInInspector] [Tooltip("現在のシールド")] [UdonSynced] public float SyncedShield;
        [HideInInspector] [Tooltip("対応するプレイヤーのスコア")] [UdonSynced] public float SyncedScore;
        [HideInInspector] [Tooltip("追従対象プレイヤーのID")] [UdonSynced] public int SyncedPlayerID;
        [HideInInspector] [Tooltip("残機数")] [UdonSynced] public int SyncedReviveTicket;
        [HideInInspector] [Tooltip("コンクエストモードの占領時間")] [UdonSynced] public float SyncedConquestScore;
        [HideInInspector] public Vector3 HidePlayerHitBoxPos;    //プレイヤー用の未使用ヒットボックスを隠す座標.AutoBuild時にGameManagerからコピーする   
        public int PlayerHitBoxIndex;
        AudioSource audioSource;
        VRCPlayerApi player;                        //追従対象プレイヤー
        float oldSyncedHitPoint;
        VRCPlayerApi leftPlayer;    //インスタンスを抜けたプレイヤー
        int leftPlayerID;
        const float invincibleAfterReviveDuration = 5.0f;
        float lastReviveTime;
        //---------------------------------------
        void Start()
        {
            Initialize();
        }
        //---------------------------------------
        public void FullHitPoint()
        {
            //このメソッド呼び出しの前にTrySetOwnerを実行すること
            SyncedHitPoint = MaxPlayerHitPoint;
            //このメソッド呼び出しの後にSyncを実行すること
        }
        //---------------------------------------
        public void ResetShield()
        {
            //このメソッド呼び出しの前にTrySetOwnerを実行すること
            SyncedShield = defaultPlayerShield;
            //このメソッド呼び出しの後にSyncを実行すること
        }
        //---------------------------------------
        public void TakeDamage(float dmg, bool selfDamage)
        {
            //復活直後は無敵
            if(Time.time - lastReviveTime < invincibleAfterReviveDuration) { return; }

            //ダメージ無効設定の場合はなにもしない
            if (Invincible) { return; }

            //自分がゲームに登録していない、または戦闘開始前であれば、なにもしない
            if (!playerManager.playerHitBox || !gameManager.SyncedInBattle) { return; }

            //プレイヤーが撃った弾が、チーム戦で味方にヒットした場合はなにもしない
            if (!selfDamage && gameManager.teamMode == TeamMode.TeamBattle && playerManager.playerHitBox.teamName == teamName) { return; }

            //チケット制で残機が0の場合はなにもしない
            if (gameManager.gameEndCondition == GameEndCondition.Ticket && SyncedReviveTicket <= 0) { return; }

            float oldHitPoint = SyncedHitPoint;
            float newHitPoint;

            //シールドがある場合は、シールドのみ減算処理を行う
            if (SyncedShield > 0 && dmg > 0)
            {
                SyncedShield -= dmg;
                
                if (SyncedShield < 0) { SyncedShield = 0; }
                newHitPoint = SyncedHitPoint;
            }
            else
            {
                newHitPoint = SyncedHitPoint - dmg;
                
                if (newHitPoint > MaxPlayerHitPoint) { newHitPoint = MaxPlayerHitPoint; }
                else if (newHitPoint < 0) { newHitPoint = 0; }

                if (oldHitPoint == newHitPoint) { return; }     //HP変動がない場合は何もしない
            }

            if (oldHitPoint > newHitPoint && OnHitParticle) { OnHitParticle.Play(); }

            TrySetOwner(this.gameObject);
            SyncedHitPoint = newHitPoint;
            Sync();

            //とどめを刺したプレイヤーのローカル処理はここに記述(同期ありの場合)
            if (newHitPoint <= 0 && oldHitPoint > 0 && playerManager.playerHitBox && playerManager.playerHitBox != this.GetComponent<PlayerHitBox>())
            {
                playerManager.playerHitBox.AddScore(scoreManager.PlayerKillScore);
            }

            //自爆でダウンした場合は減点
            if (newHitPoint <= 0 && oldHitPoint > 0 && playerManager.playerHitBox && playerManager.playerHitBox == this.GetComponent<PlayerHitBox>() && selfDamage)
            {
                playerManager.playerHitBox.AddScore(scoreManager.SelfKillPenaltyScore);
            }
        }
        //---------------------------------------
        public void TakeExplosiveDamage(float dmg, bool selfDamage, int carrierPlayerHitBoxIndex)
        {
            //ローカルで被弾処理する武器のダメージ処理

            //復活直後は無敵
            if (Time.time - lastReviveTime < invincibleAfterReviveDuration) { return; }

            //ダメージ無効設定の場合はなにもしない
            if (Invincible) { return; }

            //自分がゲームに登録していない、または戦闘開始前であれば、なにもしない
            if (!playerManager.playerHitBox || !gameManager.SyncedInBattle) { return; }

            //プレイヤーが撃った弾が、チーム戦で味方にヒットした場合はなにもしない
            if (!selfDamage && gameManager.teamMode == TeamMode.TeamBattle && playerManager.playerHitBox.teamName == teamName) { return; }

            //チケット制で残機が0の場合はなにもしない
            if (gameManager.gameEndCondition == GameEndCondition.Ticket && SyncedReviveTicket <= 0) { return; }

            float oldHitPoint = SyncedHitPoint;
            float newHitPoint;

            //シールドがある場合は、シールドのみ減算処理を行う
            if (SyncedShield > 0 && dmg > 0)
            {
                SyncedShield -= dmg;
                if (SyncedShield < 0) { SyncedShield = 0; }
                newHitPoint = SyncedHitPoint;
            }
            else
            {
                newHitPoint = SyncedHitPoint - dmg;

                if (newHitPoint > MaxPlayerHitPoint) { newHitPoint = MaxPlayerHitPoint; }
                else if (newHitPoint < 0) { newHitPoint = 0; }

                if (oldHitPoint == newHitPoint) { return; }     //HP変動がない場合は何もしない
            }

            if (oldHitPoint > newHitPoint && OnHitParticle) { OnHitParticle.Play(); }

            TrySetOwner(this.gameObject);
            SyncedHitPoint = newHitPoint;
            Sync();

            //とどめを刺した時の処理はここに記述
            //プレイヤーのミサイル被弾はローカルで処理するので、carrierPlayerHitBoxIndexからとどめを刺したプレイヤーを検索する
            if (newHitPoint <= 0 && oldHitPoint > 0 && playerManager.playerHitBox && playerManager.playerHitBox == this.GetComponent<PlayerHitBox>())
            {
                if (carrierPlayerHitBoxIndex < 0 || assigner.Assigners.Length <= carrierPlayerHitBoxIndex) { return; }

                //自爆でダウンした場合は減点
                if (playerManager.playerHitBox.PlayerHitBoxIndex == carrierPlayerHitBoxIndex)
                {
                    playerManager.playerHitBox.AddScore(scoreManager.SelfKillPenaltyScore);
                    return;
                }

                //撃破したプレイヤーに加点
                assigner.Assigners[carrierPlayerHitBoxIndex].playerHitBox.AddScore(scoreManager.PlayerKillScore);
            }
        }
        //---------------------------------------
        public void GrantReviveTicket(float plusTicket)
        {
            TrySetOwner(this.gameObject);

            SyncedReviveTicket += (int)plusTicket;
            if(SyncedReviveTicket > gameManager.SyncedMaxReviveTicket) { SyncedReviveTicket = gameManager.SyncedMaxReviveTicket; }

            Sync();
        }
        //---------------------------------------
        public void GrantShield(float plusShield)
        {
            TrySetOwner(this.gameObject);

            SyncedShield += (int)plusShield;
            if (SyncedShield > MaxPlayerShield) { SyncedShield = MaxPlayerShield; }

            Sync();
        }
        //---------------------------------------
        private void Update()
        {
            if(SyncedPlayerID <= 0)
            {
                this.transform.position = HidePlayerHitBoxPos;
                this.transform.localScale = Vector3.one;
                return;
            }

            player = VRCPlayerApi.GetPlayerById(SyncedPlayerID);
            if (!Utilities.IsValid(player))
            {
                this.transform.position = HidePlayerHitBoxPos;
                this.transform.localScale = Vector3.one;
                return;
            }
            
            Vector3 Head = player.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
            Vector3 Origin = player.GetTrackingData(VRCPlayerApi.TrackingDataType.Origin).position;
            Quaternion HeadRot = player.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;

            //設定に応じてヒットボックスの位置とサイズを調整する
            //Head, Originモードは、ヒットボックスの中心座標が頭orプレイスペースの床の高さにくる
            //そのため、子オブジェクトを1つはさんで、実際のヒットボックスの位置を上下にずらすと良い
            //PlaySpaceMoverでプレイヤーが空中に浮いた時、Originモードだとヒットボックスが地面に残ってしまうことに注意
            if (fitType == FitType.Head)
            {
                this.transform.position = new Vector3(Head.x, Head.y, Head.z);
            }
            else if (fitType == FitType.Origin)
            {
                this.transform.position = new Vector3(Head.x, Origin.y, Head.z);
            }
            else if (fitType == FitType.Mean)
            {
                this.transform.position = new Vector3(Head.x, (Head.y + Origin.y) / 2, Head.z);
            }
            else if (fitType == FitType.Resize)
            {
                const float Scaler = 1.2f;
                float height = (Head.y - Origin.y) * Scaler;   //そのままだとのアバターの頭がヒットボックス外になるので、少し大きくする
                if (height < minHitBoxHeight) { height = minHitBoxHeight; }
                float width = height / 3.0f;
                if (width < minHitBoxHeight) { width = minHitBoxHeight; }

                this.transform.position = new Vector3(Head.x, (Head.y + Origin.y) / 2, Head.z);
                this.transform.localScale = new Vector3(width, height, width);
            }

            this.transform.eulerAngles = new Vector3(0, HeadRot.eulerAngles.y, 0);
        }
        //---------------------------------------
        public void ShowHitBoxModelObject()
        {
            if (!HitBoxModelObject) { return; }

            if(playerManager.playerHitBox == this) { HitBoxModelObject.SetActive(false); }
            else { HitBoxModelObject.SetActive(true); }            
        }
        //---------------------------------------
        public void Network_HideHitBoxModelObject()
        {
            HideHitBoxModelObject();
        }
        //---------------------------------------
        public void HideHitBoxModelObject()
        {
            if (HitBoxModelObject) { HitBoxModelObject.SetActive(false); }
        }
        //---------------------------------------
        public void ResetOnStartGame()
        {
            ShowHitBoxModelObject();

            if (!CheckLocalPlayerIsOwner(this.gameObject)) { return; }

            if (SyncedPlayerID > 0)
            {
                SyncedHitPoint = MaxPlayerHitPoint;
                SyncedShield = defaultPlayerShield;
                SyncedReviveTicket = gameManager.SyncedMaxReviveTicket;;
            }
            else
            {
                SyncedHitPoint = 0;
                SyncedReviveTicket = 0;
            }
            SyncedScore = 0;
            SyncedConquestScore = 0;

            Sync();
        }
        //---------------------------------------
        public void ResetOnEndGame()
        {
            HideHitBoxModelObject();

            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                if (SyncedPlayerID > 0)
                {
                    SyncedReviveTicket = gameManager.SyncedMaxReviveTicket; ;
                }
                else
                {
                    SyncedReviveTicket = 0;
                }

                return;
            }

            Sync();
        }
        //---------------------------------------
        public void ResetScore()
        {
            //ScoreManager.ResetScoreから発動
            //スコアのみ0で初期化(HPなどは変更しない)
            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                SyncedScore = 0;
                Sync();
            }
        }
        //---------------------------------------
        public void AddScore(float _add)
        {
            if (_add == 0) { return; }

            //スコアを同期する
            TrySetOwner(this.gameObject);
            SyncedScore += _add;
            Sync();
        }
        //---------------------------------------
        public void PlayJumpEffect()
        {
            if (JumpAudioClip) { audioSource.PlayOneShot(JumpAudioClip, 1.0f); }
            if (JumpParticle) { JumpParticle.Play(); }
        }
        //---------------------------------------
        public void PlayGroundedEffect()
        {
            if (GroundedAudioClip) { audioSource.PlayOneShot(GroundedAudioClip, 1.0f); }
            if (GroundedParticle) { GroundedParticle.Play(); }
        }
        //---------------------------------------
        public void Network_PlayJumpEffect()
        {
            PlayJumpEffect();
        }
        //---------------------------------------
        public void Network_PlayGroundedEffect()
        {
            PlayGroundedEffect();
        }
        //---------------------------------------
        public override void OnPlayerLeft(VRCPlayerApi _player)
        {
            if (!Utilities.IsValid(_player)) { return; }

            leftPlayer = _player;
            leftPlayerID = leftPlayer.playerId;
            const float RemovePlayerDelay = 3.0f;   //念のためOwner移譲のラグを考慮して、一定時間のラグを入れる
            SendCustomEventDelayedSeconds("DelayedSecond_RemoveLeftPlayer", RemovePlayerDelay);
        }
        //---------------------------------------
        public void DelayedSecond_RemoveLeftPlayer()
        {
            if(SyncedPlayerID == leftPlayerID && CheckLocalPlayerIsOwner(this.gameObject))
            {
                SyncedPlayerID = 0;
                SyncedConquestScore = 0;
                SyncedScore = 0;
                SyncedHitPoint = 0;
                SyncedReviveTicket = 0;
                Sync();
            }
        }
        //---------------------------------------
        public void Network_UpdatePlayerHitBoxRenderer()
        {
            if (!playerManager.playerHitBox)
            {
                if (HitBoxModelObject) { HitBoxModelObject.SetActive(true); }
                //for (int i = 0; i < HitBoxRenderer.Length; ++i) { if (HitBoxRenderer[i]) { HitBoxRenderer[i].enabled = true; } }
                return;
            }
            if (playerManager.playerHitBox == this.GetComponent<PlayerHitBox>())
            {
                if (HitBoxModelObject) { HitBoxModelObject.SetActive(false); }
                //for (int i = 0; i < HitBoxRenderer.Length; ++i) { if (HitBoxRenderer[i]) { HitBoxRenderer[i].enabled = false; } }
                return;
            }
            if (playerManager.playerHitBox != this.GetComponent<PlayerHitBox>())
            {
                if (HitBoxModelObject) { HitBoxModelObject.SetActive(true); }
                //for (int i = 0; i < HitBoxRenderer.Length; ++i) { if (HitBoxRenderer[i]) { HitBoxRenderer[i].enabled = true; } }
                return;
            }

        }
        //---------------------------------------
        public void Network_PlayPlayerKillEffect()
        {
            if (OnKillParticle) { OnKillParticle.Play(); }
        }
        //---------------------------------------
        public Assigner GetAssigner()
        {
            return assigner;
        }
        //---------------------------------------
        public void SetAssigner(Assigner a)
        {
            assigner = a;
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
            //ローカルプレイヤーの体力が増減した場合
            if (playerManager.playerHitBox == this.GetComponent<PlayerHitBox>() && gameManager.SyncedInBattle)
            {
                if(oldSyncedHitPoint > SyncedHitPoint) { playerManager.PlayDamageEffect(); }    //体力減少
                else if(oldSyncedHitPoint < SyncedHitPoint) { playerManager.PlayHealEffect(); }   //体力回復
                if (oldSyncedHitPoint > 0 && SyncedHitPoint <= 0 && playerManager.isAlive) { playerManager.Killed(); }//体力が0になっていればダウン判定を入れる
            }

            if (gameManager.gameEndCondition == GameEndCondition.Score || gameManager.gameEndCondition == GameEndCondition.Timer)
            {
                if(gameManager.teamMode == TeamMode.TeamBattle)
                {
                    scoreManager.CalculateScore();
                }
                gameManager.ShowMyScore();
            }

            if(gameManager.gameEndCondition == GameEndCondition.Ticket)
            {
                scoreManager.CalculateScore();
                gameManager.ShowMyReviveTicket();
            }

            assigner.ShowPlayerName();

            scoreManager.CalculateScore();

            oldSyncedHitPoint = SyncedHitPoint;
        }
        //---------------------------------------
        public override void Initialize()
        {
            audioSource = this.GetComponent<AudioSource>();
        }
        //---------------------------------------
    }
}

