
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public enum RespawnType
{
    AutoRespawn, ManualRespawn,
}

public enum BotHitPointType
{
    Sync, NoSync, Sync_EnableAvatarParticle
}

namespace CombatGimmick
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class Bot : HitBox
    {
        //--------------------------------------- 
        [Tooltip("所属するチーム")] public TeamName teamName;
        [Header("----リスポーン設定----")]
        [Tooltip("trueなら破壊不可能")] public bool Invincible;
        [Tooltip("AutoRespawnなら自動で復活")] public RespawnType respawnType;
        [Tooltip("指定秒数後に自動で復活")] public float respawnTime;
        //---------------------------------------
        [Header("----HP設定----")]
        [Tooltip("体力の同期方法")] [SerializeField] BotHitPointType botHitPointType;
        [Tooltip("最大HP")] [SerializeField] float MaxHitPoint;
        [Tooltip("アバターのパーティクルによるダメージ")] [SerializeField] float AvatarParticleDamage;
        float HitPoint;    //同期しない場合の体力              
        [UdonSynced] float SyncedHitPoint;
        //---------------------------------------
        [Header("----無しでも動作可能----")]
        [Tooltip("撃破時のドロップアイテム")] public Transform LootItem;
        [Tooltip("撃破時に無効にするタレット")] [SerializeField] Turret[] turret;
        [Tooltip("撃破時に発動するパーティクル")] [SerializeField] ParticleSystem KillParticle;
        [Tooltip("撃破時に発動する効果音")] [SerializeField] AudioClip KillAudioClip;
        [Tooltip("撃破時に非表示にするメッシュ")] [SerializeField] Renderer[] Renderers;
        [Tooltip("撃破時に無効化するヒットボックス")] public BotHitBox[] botHitBox;
        [Tooltip("撃破時に無効化するゲームオブジェクト")] [SerializeField] GameObject[] DisableOnKillObjects;
        [Tooltip("撃破時のスコア")] [SerializeField] float Score;
        [Tooltip("体力を表示するテキスト")] public Text HitPointText;
        //---------------------------------------
        //[Header("手動で変更しないこと")]
        [HideInInspector] [Tooltip("撃破されていなければtrue")] public bool isAlive = true;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        //---------------------------------------
        AudioSource audioSource;
        //---------------------------------------
        void Start()
        {
            audioSource = this.GetComponent<AudioSource>();

            if (botHitPointType == BotHitPointType.Sync || botHitPointType == BotHitPointType.Sync_EnableAvatarParticle)
            {
                if (CheckLocalPlayerIsOwner(this.gameObject))
                {
                    SyncedHitPoint = MaxHitPoint;
                }
                Sync();
            }
            else
            {
                HitPoint = MaxHitPoint;
            }

            if (turret.Length > 0 && turret[0]) { SetTeam(turret[0].defaultTeamName); }
            else { SetTeam(TeamName.None); }
            for(int i = 0; i < turret.Length; ++i) { if (turret[i]) { turret[i].SetBot(this.GetComponent<Bot>()); } }

            foreach (BotHitBox hb in botHitBox)
            {
                hb.SetBot(this.GetComponent<Bot>());
                hb.AvatarParticleDamage = AvatarParticleDamage;
                hb.botHitPointType = botHitPointType;
            }
        }
        //---------------------------------------
        public void TakeDamage(float dmg, bool isAvatarParticle)
        {
            if (!isAlive) { return; }
            if (!playerManager.playerHitBox || !gameManager.SyncedInBattle) { return; }
            if (gameManager.teamMode == TeamMode.TeamBattle && playerManager.playerHitBox.teamName == teamName) { return; }
            if (gameManager.teamMode == TeamMode.TeamBattle && playerManager.playerHitBox.teamName == TeamName.None) { return; }
            if (botHitPointType == BotHitPointType.Sync || botHitPointType == BotHitPointType.Sync_EnableAvatarParticle)
            {
                //アバターパーティクルはインスタンス内の人数分判定が発生するので、このオブジェクトのオーナーのみダメージ処理に入る
                if(botHitPointType == BotHitPointType.Sync_EnableAvatarParticle && !CheckLocalPlayerIsOwner(this.gameObject) && isAvatarParticle) { return; }

                TrySetOwner(this.gameObject);

                float oldSyncedHitPoint = SyncedHitPoint;
                SyncedHitPoint -= dmg;
                if (SyncedHitPoint < 0) { SyncedHitPoint = 0; }
                
                Sync();

                if (oldSyncedHitPoint > 0 && SyncedHitPoint <= 0)
                {
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_PlayKillEffect");

                    //とどめを刺したプレイヤーのローカル処理はここに記述(同期ありの場合)
                    if (playerManager.playerHitBox)
                    {
                        playerManager.playerHitBox.AddScore(Score);
                        SpawnLootItem();
                    }
                }
            }
            else if(botHitPointType == BotHitPointType.NoSync)
            {
                float oldHitPoint = HitPoint;
                HitPoint -= dmg;
                
                if (HitPoint < 0) { HitPoint = 0; }
                
                if (HitPointText) { HitPointText.text = "HitPoint=" + HitPoint; }

                if (oldHitPoint > 0 && HitPoint <= 0)
                {
                    PlayKillEffect();

                    //撃破したプレイヤーのローカル処理はここに記述(同期なしの場合)
                    if (playerManager.playerHitBox)
                    {
                        playerManager.playerHitBox.AddScore(Score);
                        SpawnLootItem();
                    }
                }
            }
        }
        //---------------------------------------
        public void SpawnLootItem()
        {
            if (!LootItem) { return; }

            TrySetOwner(LootItem.gameObject);
            LootItem.position = this.transform.position;
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
            Debug.Log("Reset hit point of " + this.gameObject.name + ". HP = " + SyncedHitPoint.ToString(), this.gameObject);

            if (HitPointText)
            {
                if (botHitPointType == BotHitPointType.Sync_EnableAvatarParticle || botHitPointType == BotHitPointType.Sync) { HitPointText.text = SyncedHitPoint.ToString(); }
                else { HitPointText.text = HitPoint.ToString(); }                
            }
        }
        //---------------------------------------
        public void Network_PlayKillEffect()
        {
            PlayKillEffect();
        }
        //---------------------------------------
        public void PlayKillEffect()
        {
            //撃破時の処理
            //その他に無効化したいオブジェクトがあれば、こことDelayedSecond_PlayRespawnEffect()で指定
            if (KillParticle) { KillParticle.Play(); }
            if (KillAudioClip) { audioSource.PlayOneShot(KillAudioClip, 1.0f); }

            for (int i = 0; i < Renderers.Length; ++i) { if (Renderers[i]) { Renderers[i].enabled = false; } }
            for (int i = 0; i < DisableOnKillObjects.Length; ++i) { if (DisableOnKillObjects[i]) { DisableOnKillObjects[i].SetActive(false); } }
            for (int i = 0; i < botHitBox.Length; ++i) { if (botHitBox[i]) { botHitBox[i].DisableColliders(); } }
            for (int i = 0; i < turret.Length; ++i) { if (turret[i]) { turret[i].Kill(); } }

            if (respawnType == RespawnType.AutoRespawn) { SendCustomEventDelayedSeconds("DelayedSecond_Revive", respawnTime); }

            isAlive = false;
            gameManager.CheckWinner_DestroyMode();
        }
        //---------------------------------------
        public void ResetOnStartGame()
        {
            //表示とコライダーを有効化
            //その他に有効化したいオブジェクトがあれば、こことPlayKillEffect()で指定
            for (int i = 0; i < DisableOnKillObjects.Length; ++i) { if (DisableOnKillObjects[i]) { DisableOnKillObjects[i].SetActive(true); } }
            for (int i = 0; i < Renderers.Length; ++i) { if (Renderers[i]) { Renderers[i].enabled = true; } }
            for (int i = 0; i < botHitBox.Length; ++i) { if (botHitBox[i]) { botHitBox[i].EnableColliders(); } }

            //HPを回復
            if (botHitPointType == BotHitPointType.Sync || botHitPointType == BotHitPointType.Sync_EnableAvatarParticle)
            {
                if (CheckLocalPlayerIsOwner(this.gameObject)) { SyncedHitPoint = MaxHitPoint; }
                Sync();
            }
            else
            {
                HitPoint = MaxHitPoint;
            }

            isAlive = true;
        }
        //---------------------------------------
        public void Revive()
        {
            //表示を有効化
            //その他に有効化したいオブジェクトがあれば、こことPlayKillEffect()で指定
            for (int i = 0; i < Renderers.Length; ++i) { if (Renderers[i]) { Renderers[i].enabled = true; } }
            for (int i = 0; i < DisableOnKillObjects.Length; ++i) { if (DisableOnKillObjects[i]) { DisableOnKillObjects[i].SetActive(true); } }

            //HPを回復
            if (botHitPointType == BotHitPointType.Sync || botHitPointType == BotHitPointType.Sync_EnableAvatarParticle)
            {
                if (CheckLocalPlayerIsOwner(this.gameObject)) { SyncedHitPoint = MaxHitPoint; }
                Sync();
            }
            else
            {
                HitPoint = MaxHitPoint;
                if (HitPointText) { HitPointText.text = SyncedHitPoint.ToString(); }
            }

            //タレットが付属しない場合
            if(turret.Length <= 0 || !turret[0])
            {
                for (int i = 0; i < botHitBox.Length; ++i) { botHitBox[i].EnableColliders(); }
                isAlive = true;
                return;
            }

            //ActivateOnRespawnモードのタレットが付属している場合            
            if (turret[0].IsActivateOnRespawn())
            {
                for (int i = 0; i < turret.Length; ++i) { turret[i].Revive(); }
                for (int i = 0; i < botHitBox.Length; ++i) { botHitBox[i].EnableColliders(); }
                isAlive = true;
                return;
            }

            //DeactivateOnRespawnモードのタレットが付属している場合   
            for (int i = 0; i < turret.Length; ++i) { turret[i].isAlive = true; }
            isAlive = false;
            return;
        }
        //---------------------------------------
        public void DelayedSecond_Revive()
        {
            Revive();
        }
        //---------------------------------------
        public void SetTeam(TeamName _team)
        {
            teamName = _team;
        }
        //---------------------------------------
        public bool AutoBuild(CombatGimmickBuilder builder, PlayerManager _playerManager, GameManager _gameManager, int HitBoxLayerNum)
        {
            playerManager = _playerManager;
            gameManager = _gameManager;

            if (MaxHitPoint <= 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " MaxHitPointは0より大きくしてください.", this.gameObject);
                return false;
            }

            if (botHitBox.Length <= 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " Botには, 最低1つBotHitBoxを設定してください.", this.gameObject);
                return false;
            }

            for (int i = 0; i < botHitBox.Length; ++i)
            {
                if (!botHitBox[i])
                {
                    Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " BotのElement " + i + " が設定されていません.", this.gameObject);
                    return false;
                }
                if (!botHitBox[i].AutoBuild(this.GetComponent<Bot>(), builder, HitBoxLayerNum))
                {
                    return false;
                }
            }
            return true;
        }
        //---------------------------------------
    }
}

