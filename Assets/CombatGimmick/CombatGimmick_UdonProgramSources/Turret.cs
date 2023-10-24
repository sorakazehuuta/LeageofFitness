
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System;
using UnityEngine.UI;

namespace CombatGimmick
{
    public enum TurretRespawnType
    {
        DeactivateOnRespawn, ActivateOnRespawn
    }

    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class Turret : UdonSharpBehaviour
    {
        //---------------------------------------
        [Header("必須項目.無いとエラー")]  
        [Tooltip("索敵用のレイヤー")] [SerializeField] bool[] hitLayer = new bool[32];
        [Tooltip("銃身の向き(Z軸)")] [SerializeField] Transform ForwardTransform;
        //---------------------------------------
        [Header("----タレットの性能----")]
        [Tooltip("銃弾のパーティクル")] [SerializeField] Bullet bullet;
        [Tooltip("所属するチーム")] public TeamName defaultTeamName = TeamName.None;
        [Tooltip("trueならチーム変更可能")] [SerializeField] bool CanSwapTeam;
        [Tooltip("タレットの探索半径")] [SerializeField] float SearchRange = 100;
        [Tooltip("ターゲットを変更する周期")] [SerializeField] float SearchInterval = 5;
        [Tooltip("タレットの旋回速度")] [SerializeField, Range(0, 1)] float AimLerpValue = 0.05f;
        [Tooltip("射撃時に許容する誤差")] [SerializeField] float FireAngleDegreeTorelance = 5;
        [Tooltip("射撃する周期")] [SerializeField] float FireInterval = 5;
        [Tooltip("復活時に自動で再起動するかどうか")] public TurretRespawnType turretRespawnType;
        //---------------------------------------
        [Header("----無しでも動作可能----")]
        [Tooltip("コントロール取得時に発動するエフェクト")] [SerializeField] ParticleSystem TakeControlParticle;
        [Tooltip("射撃時に発動する効果音")] [SerializeField] AudioClip FireAudioClip;
        [Tooltip("タレット起動時に発動する効果音")] [SerializeField] AudioClip ActivateAudioClip;
        [Tooltip("タレット撃破時に発動する効果音")] [SerializeField] AudioClip KillAudioClip;
        //---------------------------------------
        float lastSearchTargetTime;
        float lastFireTime;
        LayerMask rayMask;
        RaycastHit _hitInfo;
        AudioSource audioSource;
        ParticleSystem BulletParticle;
        Bot bot;
        [UdonSynced] int SyncedTargetPlayerHitBoxID;  //ターゲットプレイヤーのPlayerID
        //---------------------------------------
        //[Header("手動で変更しないこと")]
        [HideInInspector] [Tooltip("撃破されていなければtrue")] public bool isAlive = true;
        [HideInInspector] [Tooltip("所属するチーム")] public TeamName teamName;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("PlayerHitBox[]")] public PlayerHitBox[] playerHitBoxes;
        [HideInInspector] [Tooltip("Assigner[]")] public Assigner[] Assigners;
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        //---------------------------------------
        public Text debugText;
        //---------------------------------------
        void Start()
        {
            Initialize();
        }
        //---------------------------------------
        private void Update()
        {
            if (debugText) { debugText.text = Time.time.ToString("F1") + ": SyncedTargetPlayerHitBoxID = " + SyncedTargetPlayerHitBoxID; }

            if (!gameManager.SyncedInBattle || !isAlive) { return; }
            
            if (gameManager.teamMode == TeamMode.TeamBattle && teamName == TeamName.None) { return; }
                       
            if (Time.time - lastSearchTargetTime > SearchInterval && gameManager.teamMode == TeamMode.TeamBattle) { SearchTeamMode(); }
            if (Time.time - lastSearchTargetTime > SearchInterval && gameManager.teamMode == TeamMode.FreeForAll) { SearchFreeForAllMode(); }

            if (SyncedTargetPlayerHitBoxID < 0 || SyncedTargetPlayerHitBoxID >= playerHitBoxes.Length) { return; }
            
            Aim();
            
            if (Time.time - lastFireTime > FireInterval && SyncedTargetPlayerHitBoxID >= 0) { Fire(); }
        }
        //---------------------------------------
        public void SearchTeamMode()
        {
            lastSearchTargetTime = Time.time;

            if (!CheckLocalPlayerIsOwner(this.gameObject)) { return; }

            bool targetIsFound = false;
            for(int i = 0; i < playerHitBoxes.Length; ++i)
            {
                if (!targetIsFound && Physics.Raycast(this.transform.position, playerHitBoxes[i].transform.position - this.transform.position, out _hitInfo, SearchRange, rayMask, QueryTriggerInteraction.Ignore))
                {
                    if(_hitInfo.transform.GetComponent<PlayerHitBox>() && _hitInfo.transform.GetComponent<PlayerHitBox>().teamName != teamName && _hitInfo.transform.GetComponent<PlayerHitBox>().SyncedReviveTicket > 0 && _hitInfo.transform.GetComponent<PlayerHitBox>().SyncedHitPoint > 0)
                    {
                        targetIsFound = true;

                        SyncedTargetPlayerHitBoxID = i;
                    }
                }
            }

            if (!targetIsFound) { SyncedTargetPlayerHitBoxID = -1; }

            Sync();
        }
        //---------------------------------------
        public void SearchFreeForAllMode()
        {
            lastSearchTargetTime = Time.time;

            if (!CheckLocalPlayerIsOwner(this.gameObject)) { return; }

            bool targetIsFound = false;
            for (int i = 0; i < playerHitBoxes.Length; ++i)
            {
                if (!targetIsFound && Physics.Raycast(this.transform.position, playerHitBoxes[i].transform.position - this.transform.position, out _hitInfo, SearchRange, rayMask, QueryTriggerInteraction.Ignore))
                {
                    if (_hitInfo.transform.GetComponent<PlayerHitBox>())
                    {
                        targetIsFound = true;

                        SyncedTargetPlayerHitBoxID = i;
                    }
                }
            }

            if (!targetIsFound) { SyncedTargetPlayerHitBoxID = -1; }

            Sync();
        }
        //---------------------------------------
        public void Aim()
        {
            if (!Utilities.IsValid(playerHitBoxes[SyncedTargetPlayerHitBoxID])){ return; }

            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(playerHitBoxes[SyncedTargetPlayerHitBoxID].transform.position - this.transform.position, Vector3.up), AimLerpValue);
        }
        //---------------------------------------
        public void Fire()
        {
            if (!Utilities.IsValid(playerHitBoxes[SyncedTargetPlayerHitBoxID])) { return; }
            if (!bullet) { return; }

            float FireAngleDegreeError = Vector3.Angle(playerHitBoxes[SyncedTargetPlayerHitBoxID].transform.position - this.transform.position, ForwardTransform.forward);
            //Debug.Log("FireAngleDegree=" + FireAngleDegreeError);
            if (FireAngleDegreeError < FireAngleDegreeTorelance)
            {
                lastFireTime = Time.time;

                if (FireAudioClip) { audioSource.PlayOneShot(FireAudioClip, 0.5f); }
                if (BulletParticle) { BulletParticle.Play(); }
            }
        }
        //---------------------------------------
        public void Kill()
        {
            isAlive = false;

            teamName = TeamName.None;
            if (KillAudioClip) { audioSource.PlayOneShot(KillAudioClip, 1.0f); }
        }
        //---------------------------------------
        public void Revive()
        {
            isAlive = true;

            if (turretRespawnType == TurretRespawnType.DeactivateOnRespawn) { return; }

            teamName = defaultTeamName;
            if (defaultTeamName == TeamName.A) { SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_TeamA_TakeControl"); }
            if (defaultTeamName == TeamName.B) { SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_TeamB_TakeControl"); }
            if (defaultTeamName == TeamName.C) { SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_TeamC_TakeControl"); }
            if (defaultTeamName == TeamName.D) { SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_TeamD_TakeControl"); }
        }
        //---------------------------------------
        public override void Interact()
        {
            if (gameManager.teamMode == TeamMode.FreeForAll) { return; }
            if (teamName != TeamName.None) { return; }
            if (!playerManager.playerHitBox) { return; }
            if (!isAlive) { return; }
            if (!CanSwapTeam && playerManager.playerHitBox.teamName != teamName) { return; }

            if (playerManager.playerHitBox.teamName == TeamName.A) { SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_TeamA_TakeControl"); }
            if (playerManager.playerHitBox.teamName == TeamName.B) { SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_TeamB_TakeControl"); }
            if (playerManager.playerHitBox.teamName == TeamName.C) { SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_TeamC_TakeControl"); }
            if (playerManager.playerHitBox.teamName == TeamName.D) { SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_TeamD_TakeControl"); }
        }
        //---------------------------------------        
        public void ResetOnStartGame()
        {
            isAlive = true;
            teamName = defaultTeamName;

            if (!CheckLocalPlayerIsOwner(this.gameObject)) { return; }

            SyncedTargetPlayerHitBoxID = -1;
            Sync();
        }        
        //---------------------------------------
        public void Network_TeamA_TakeControl()
        {
            teamName = TeamName.A;
            isAlive = true;
            if (bot)
            {
                bot.SetTeam(TeamName.A);
                bot.isAlive = true;
                for (int i = 0; i < bot.botHitBox.Length; ++i) { bot.botHitBox[i].EnableColliders(); }
            }

            if (ActivateAudioClip) { audioSource.PlayOneShot(ActivateAudioClip, 1.0f); }
            if (TakeControlParticle) { TakeControlParticle.Play(); }
        }
        //---------------------------------------
        public void Network_TeamB_TakeControl()
        {
            teamName = TeamName.B;
            isAlive = true;
            if (bot)
            {
                bot.SetTeam(TeamName.B);
                bot.isAlive = true;
                for (int i = 0; i < bot.botHitBox.Length; ++i) { bot.botHitBox[i].EnableColliders(); }
            }

            if (ActivateAudioClip) { audioSource.PlayOneShot(ActivateAudioClip, 1.0f); }
            if (TakeControlParticle) { TakeControlParticle.Play(); }
        }
        //---------------------------------------
        public void Network_TeamC_TakeControl()
        {
            teamName = TeamName.C;
            isAlive = true;
            if (bot)
            {
                bot.SetTeam(TeamName.C);
                bot.isAlive = true;
                for (int i = 0; i < bot.botHitBox.Length; ++i) { bot.botHitBox[i].EnableColliders(); }
            }

            if (ActivateAudioClip) { audioSource.PlayOneShot(ActivateAudioClip, 1.0f); }
            if (TakeControlParticle) { TakeControlParticle.Play(); }
        }
        //---------------------------------------
        public void Network_TeamD_TakeControl()
        {
            teamName = TeamName.D;
            isAlive = true;
            if (bot)
            {
                bot.SetTeam(TeamName.D);
                bot.isAlive = true;
                for (int i = 0; i < bot.botHitBox.Length; ++i) { bot.botHitBox[i].EnableColliders(); }
            }

            if (ActivateAudioClip) { audioSource.PlayOneShot(ActivateAudioClip, 1.0f); }
            if (TakeControlParticle) { TakeControlParticle.Play(); }
        }
        //---------------------------------------
        public void Network_TeamNone_TakeControl()
        {
            teamName = TeamName.None;
            isAlive = false;
            if (bot) { bot.SetTeam(TeamName.None); }
            for (int i = 0; i < bot.botHitBox.Length; ++i) { bot.botHitBox[i].EnableColliders(); }
        }
        //---------------------------------------
        public bool IsActivateOnRespawn()
        {
            if(turretRespawnType == TurretRespawnType.ActivateOnRespawn) { return true; }
            return false;
        }
        //---------------------------------------
        public void SetBot(Bot _bot)
        {
            bot = _bot;
        }
        //---------------------------------------
        public void DealDamage(float dmg, GameObject target)
        {
            //以下の条件ではダメージを与えない
            //・ローカルプレイヤーがゲームに参加登録していない
            //・味方チームに属するタレットから被弾した
            //・ローカルプレイヤーがダウン状態
            //・戦闘中でない
            if (!Utilities.IsValid(target)) { return; }
            if (!playerManager.playerHitBox) { return; }
            if (playerManager.playerHitBox.teamName == teamName && gameManager.teamMode == TeamMode.TeamBattle) { return; }
            if (!playerManager.isAlive || !gameManager.SyncedInBattle || !gameManager.SyncedInGame) { return; }
            if (target.GetComponent<PlayerHitBox>() && target.GetComponent<PlayerHitBox>() == playerManager.playerHitBox) { target.GetComponent<PlayerHitBox>().TakeDamage(dmg, true); }
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
            if (debugText) { debugText.text = "OnDeserialization(): " + Time.time + ": SyncedTargetPlayerHitBoxID = " + SyncedTargetPlayerHitBoxID; }
        }
        //---------------------------------------
        public bool AutoBuild(Assigner[] _assigners, GameManager _gameManager, PlayerManager _playerManager)
        {
            gameManager = _gameManager;
            playerManager = _playerManager;
            Assigners = _assigners;
            playerHitBoxes = new PlayerHitBox[Assigners.Length];
            for(int i = 0; i < playerHitBoxes.Length; ++i) { playerHitBoxes[i] = Assigners[i].playerHitBox; }
            
            return true;
        }        
        //---------------------------------------
        public void Initialize()
        {
            teamName = defaultTeamName;

            audioSource = this.GetComponent<AudioSource>();
            if (bullet)
            {
                BulletParticle = bullet.GetComponent<ParticleSystem>();
                bullet.SetTurret(this.GetComponent<Turret>());
            }

            //ヒットするレイヤーの設定
            rayMask = Convert.ToInt32(hitLayer[0]) << 0
                | Convert.ToInt32(hitLayer[1]) << 1
                | Convert.ToInt32(hitLayer[2]) << 2
                | Convert.ToInt32(hitLayer[3]) << 3
                | Convert.ToInt32(hitLayer[4]) << 4
                | Convert.ToInt32(hitLayer[5]) << 5
                | Convert.ToInt32(hitLayer[6]) << 6
                | Convert.ToInt32(hitLayer[7]) << 7
                | Convert.ToInt32(hitLayer[8]) << 8
                | Convert.ToInt32(hitLayer[9]) << 9
                | Convert.ToInt32(hitLayer[10]) << 10
                | Convert.ToInt32(hitLayer[11]) << 11
                | Convert.ToInt32(hitLayer[12]) << 12
                | Convert.ToInt32(hitLayer[13]) << 13
                | Convert.ToInt32(hitLayer[14]) << 14
                | Convert.ToInt32(hitLayer[15]) << 15
                | Convert.ToInt32(hitLayer[16]) << 16
                | Convert.ToInt32(hitLayer[17]) << 17
                | Convert.ToInt32(hitLayer[18]) << 18
                | Convert.ToInt32(hitLayer[19]) << 19
                | Convert.ToInt32(hitLayer[20]) << 20
                | Convert.ToInt32(hitLayer[21]) << 21
                | Convert.ToInt32(hitLayer[22]) << 22
                | Convert.ToInt32(hitLayer[23]) << 23
                | Convert.ToInt32(hitLayer[24]) << 24
                | Convert.ToInt32(hitLayer[25]) << 25
                | Convert.ToInt32(hitLayer[26]) << 26
                | Convert.ToInt32(hitLayer[27]) << 27
                | Convert.ToInt32(hitLayer[28]) << 28
                | Convert.ToInt32(hitLayer[29]) << 29
                | Convert.ToInt32(hitLayer[30]) << 30
                | Convert.ToInt32(hitLayer[31]) << 31;
        }
        //---------------------------------------
    }
}
