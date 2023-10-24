
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System;

namespace CombatGimmick
{
    public enum LaserType
    {
        Local, Friend, Global,
    }

    public enum RayCastType
    {
        Default, Hybrid,
    }

    [ExecuteInEditMode]
    [DefaultExecutionOrder(20)] //MainModuleよりも後に実行
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class PointerModule : UdonSharpBehaviour
    {
        //---------------------------------------
        [Header("----描画設定----")]
        [Tooltip("レーザーが見えるプレイヤー")] [SerializeField] LaserType laserType;
        [Tooltip("レーザーの最大距離")] [SerializeField] float range = 100;
        [Tooltip("trueならワールド読み込み時に自動で描画Off")] [SerializeField] bool DisableOnInitialize;
        //---------------------------------------
        [Header("----ミサイルがある場合のみ適用----")]
        [Tooltip("ロックオン判定の種類")] [SerializeField] RayCastType rayCastType;
        [Tooltip("Capsure/Hybridモードのロックオン判定の半径")] [SerializeField] float CapsureRayRadius = 1;
        [Tooltip("ロックオン判定の頻度")] [SerializeField] float LockInterval = 1;
        [Tooltip("ロックオンの継続時間")] [SerializeField] float LockDuration = 3;
        [Tooltip("ロックオン時の効果音")] [SerializeField] AudioClip LockAudioClip;
        [Tooltip("ロックオン時のレーザーのマテリアル")] [SerializeField] Material LockedLaserMaterial;
        //---------------------------------------
        //[Header("手動で変更しないこと")]
        [HideInInspector] [UdonSynced] public int SyncedTargetIndex = -1;
        [HideInInspector] [UdonSynced] public int SyncedCarrierPlayerHitBoxIndex;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("HitBoxLayer")] public int hitBoxLayer;
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        [HideInInspector] [Tooltip("RangedWeapon_MainModule")] [SerializeField] RangedWeapon_MainModule mainModule;
        [HideInInspector] [Tooltip("Missle(ない場合はnull)")] public Missile[] missile;
        [HideInInspector] [Tooltip("PlayerHitBox[]")] public PlayerHitBox[] playerHitBoxes;
        [HideInInspector] [Tooltip("BotHitBox[]")] public BotHitBox[] botHitBoxes;
        //---------------------------------------
        bool enabledLaser;
        float lastLockTime;
        LayerMask rayMask;
        LayerMask capsuleRayMask;
        LineRenderer lineRenderer;
        Vector3 Direction;
        RaycastHit _hitInfo;
        RaycastHit _capsuleHitInfo;
        Vector3 To;
        Vector3[] LaserPositions = new Vector3[2];
        AudioSource audioSource;
        Material UnlockedLaserMaterial;
        //---------------------------------------
        void Start()
        {
            Initialize();
        }
        //---------------------------------------
        private void Update()
        {
            if (!enabledLaser) { return; }

            Direction = this.transform.forward;

            if (rayCastType == RayCastType.Default) { DefaultRayCast(); }
            if (rayCastType == RayCastType.Hybrid) { HybridRayCast(); }

            lineRenderer.SetPositions(LaserPositions);
        }
        //---------------------------------------
        public void DefaultRayCast()
        {
            if (Physics.Raycast(this.transform.position, Direction, out _hitInfo, range, rayMask, QueryTriggerInteraction.Ignore))
            {
                To = _hitInfo.point;
                if (missile[0] && Time.time - lastLockTime > LockInterval && enabledLaser) { LockTarget(_hitInfo.collider); }
                LaserPositions = new Vector3[] { this.transform.position, To };
                return;
            }

            To = this.transform.position + this.transform.forward * range;
            LaserPositions = new Vector3[] { this.transform.position, To };
        }
        //---------------------------------------
        public void HybridRayCast()
        {               
            if (!Physics.CapsuleCast(this.transform.position, this.transform.position, CapsureRayRadius, Direction, out _capsuleHitInfo, range, capsuleRayMask, QueryTriggerInteraction.Ignore))
            {     
                //CapsuleCastが命中していない場合
                To = this.transform.position + this.transform.forward * range;
                LaserPositions = new Vector3[] { this.transform.position, To };
                return;
            }

            if (!_capsuleHitInfo.collider.GetComponent<BotHitBox>() && !_capsuleHitInfo.collider.GetComponent<PlayerHitBox>())
            {
                //CapsuleCastがヒットボックスなしのオブジェクトに命中した場合
                if (!Physics.Raycast(this.transform.position, Direction, out _hitInfo, range, rayMask, QueryTriggerInteraction.Ignore))
                {
                    //RayCastが命中していない場合
                    To = this.transform.position + this.transform.forward * range;
                    LaserPositions = new Vector3[] { this.transform.position, To };
                    return;
                }
                To = _hitInfo.point;
                LaserPositions = new Vector3[] { this.transform.position, To };
                return;
            }
                
            //CapsuleCastがヒットボックスに命中したら、RayCastで実際の射線が通っているかチェックする
            Vector3 newDirection = (_capsuleHitInfo.point - this.transform.position).normalized;

            if (!Physics.Raycast(this.transform.position, newDirection, out _hitInfo, range, rayMask, QueryTriggerInteraction.Ignore))
            {
                //RayCastが命中していない場合
                To = this.transform.position + this.transform.forward * range;
                LaserPositions = new Vector3[] { this.transform.position, To };
                return;
            }

            if (!_hitInfo.collider.GetComponent<BotHitBox>() && !_hitInfo.collider.GetComponent<PlayerHitBox>())
            {
                //RayCastがヒットボックスなしのオブジェクトに命中した場合
                if (!Physics.Raycast(this.transform.position, Direction, out _hitInfo, range, rayMask, QueryTriggerInteraction.Ignore))
                {
                    To = this.transform.position + this.transform.forward * range;
                    LaserPositions = new Vector3[] { this.transform.position, To };
                    return;
                }

                To = _hitInfo.point;
                LaserPositions = new Vector3[] { this.transform.position, To };
                return;
            }

            //RayCastがヒットボックスに命中した場合
            if (missile[0] && Time.time - lastLockTime > LockInterval && enabledLaser) { LockTarget(_hitInfo.collider); }
            To = _hitInfo.point;
            LaserPositions = new Vector3[] { this.transform.position, To };
            return;
        }
        //---------------------------------------
        public void LockTarget(Collider col)
        {
            if (!playerManager.playerHitBox) { return; }
            //if (!gameManager.SyncedInBattle) { return; }
            if (!Utilities.IsValid(col)) { return; }

            if (col.GetComponent<PlayerHitBox>())
            {
                if(playerManager.playerHitBox == col.GetComponent<PlayerHitBox>()) { return; }  //自分はロックしない
                if(gameManager.teamMode == TeamMode.TeamBattle && playerManager.playerHitBox.teamName == col.GetComponent<PlayerHitBox>().teamName) { return; }    //チームメイトはロックしない          

                int newTargetIndex = LockPlayerHitBox(col.GetComponent<PlayerHitBox>());
                if (newTargetIndex < 0) { return; }

                TrySetOwner(this.gameObject);
                SyncedTargetIndex = newTargetIndex;
                if (playerManager.playerHitBox) { SyncedCarrierPlayerHitBoxIndex = playerManager.playerHitBox.PlayerHitBoxIndex; }
                else { SyncedCarrierPlayerHitBoxIndex = -1; }
                Sync();
                
                if (LockAudioClip) { audioSource.PlayOneShot(LockAudioClip, 1.0f); }
                if (LockedLaserMaterial) { lineRenderer.sharedMaterial = LockedLaserMaterial; }
                SendCustomEventDelayedSeconds("DelayedSecond_TryUnlockTarget", LockDuration);
                lastLockTime = Time.time;
                return;
            }

            if (col.GetComponent<BotHitBox>())
            {                
                if(col.GetComponent<BotHitBox>().GetBot().teamName == playerManager.playerHitBox.teamName) { return; }//味方のBotはロックしない

                int newTargetIndex = LockBotHitBox(col.GetComponent<BotHitBox>());
                if (newTargetIndex < 0) { return; }

                TrySetOwner(this.gameObject);
                SyncedTargetIndex = newTargetIndex;
                if (playerManager.playerHitBox) { SyncedCarrierPlayerHitBoxIndex = playerManager.playerHitBox.PlayerHitBoxIndex; }
                else { SyncedCarrierPlayerHitBoxIndex = -1; }
                Sync();
                
                if (LockAudioClip) { audioSource.PlayOneShot(LockAudioClip, 1.0f); }
                if (LockedLaserMaterial) { lineRenderer.sharedMaterial = LockedLaserMaterial; }
                SendCustomEventDelayedSeconds("DelayedSecond_TryUnlockTarget", LockDuration);
                lastLockTime = Time.time;
                return;
            }

            return;
        }
        //---------------------------------------
        public int LockPlayerHitBox(PlayerHitBox _hitBox)
        {
            for (int i = 0; i < playerHitBoxes.Length; ++i)
            {
                if (playerHitBoxes[i] == _hitBox)
                {
                    return i;
                }
            }
            return -1;
        }
        //---------------------------------------
        public int LockBotHitBox(BotHitBox _hitBox)
        {
            for (int i = 0; i < botHitBoxes.Length; ++i)
            {
                if (botHitBoxes[i] == _hitBox)
                {
                    return i + playerHitBoxes.Length;
                }
            }
            return -1;
        }
        //---------------------------------------
        public void DelayedSecond_TryUnlockTarget()
        {
            const float mult = 0.9f;    //フレーム処理時間を考慮し、閾値を調整する
            if (Time.time - lastLockTime > LockDuration * mult)
            {
                UnlockTarget();
                if (UnlockedLaserMaterial) { lineRenderer.sharedMaterial = UnlockedLaserMaterial; }
            }
        }
        //---------------------------------------
        public void UnlockTarget()
        {
            TrySetOwner(this.gameObject);
            SyncedTargetIndex = -1;
            Sync();         
        }
        //---------------------------------------
        public override void OnPickup() 
        {
            //MainModuleをPickupした時に、このPointerModuleが関連づけされていれば発動する
            //このオブジェクト単体にPickupをつけた場合でも動作可能

            if (laserType == LaserType.Global)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_EnableGlobalLaser");
            }
            else if(laserType == LaserType.Friend)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_EnableFriendLaser");
            }
            else
            {
                lineRenderer.enabled = true;
            }

            enabledLaser = true;
        }
        //---------------------------------------
        public override void OnDrop()
        {
            if (laserType == LaserType.Global || laserType == LaserType.Friend)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_DisableLaser");
            }
            else
            {
                lineRenderer.enabled = false;
            }

            enabledLaser = false;
        }
        //---------------------------------------
        public void Network_EnableFriendLaser()
        {
            if(playerManager.Team == mainModule.Team)
            {
                lineRenderer.enabled = true;
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
        //---------------------------------------
        public void Network_EnableGlobalLaser()
        {
            lineRenderer.enabled = true;
        }
        //---------------------------------------
        public void Network_DisableLaser()
        {
            lineRenderer.enabled = false;
        }
        //---------------------------------------
        public void SetMainModule(RangedWeapon_MainModule mm)
        {
            mainModule = mm;
        }
        //---------------------------------------
        public void SetMissile(Missile[] _missile)
        {
            missile = _missile;
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
            if(missile.Length <= 0) { return; }

            for (int i = 0; i < missile.Length; ++i)
            {
                if (missile[i])
                {
                    missile[i].MasterTargetIndex = SyncedTargetIndex;
                    missile[i].MasterCarrierPlayerHitBoxIndex = SyncedCarrierPlayerHitBoxIndex;
                }
            }
        }
        //---------------------------------------
        public void Initialize()
        {
            if (!this.GetComponent<LineRenderer>())
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " LineRendererコンポーネントがありません");
            }
            else
            {
                lineRenderer = this.GetComponent<LineRenderer>();
            }

            if (LockedLaserMaterial) { UnlockedLaserMaterial = lineRenderer.material; }

            audioSource = this.GetComponent<AudioSource>();
            
            if (DisableOnInitialize) { lineRenderer.enabled = false; }

            if (CapsureRayRadius <= 0) { CapsureRayRadius = 1; }

            if (range <= 0) { range = 0.1f; }
            //if (MinHybridCastRange <= 0) { MinHybridCastRange = 0.05f; }
            //if (rayCastType == RayCastType.Hybrid && MinHybridCastRange >= range) { MinHybridCastRange = range / 2; }

            //ヒットするレイヤーの設定
            rayMask = 1 << 0 | 1 << hitBoxLayer;    //default(0)とヒットボックスのレイヤーにのみ命中
            capsuleRayMask = 1 << hitBoxLayer;      //ヒットボックスのレイヤーにのみ命中
            /*
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
                */
        }
        //---------------------------------------
        public bool AutoBuild(PlayerManager pm, GameManager gm, int HitBoxLayerNum, Assigner[] _assigners, BotHitBox[] _botHitBoxes)
        {
            playerManager = pm;
            gameManager = gm;

            hitBoxLayer = HitBoxLayerNum;

            botHitBoxes = _botHitBoxes;
            playerHitBoxes = new PlayerHitBox[_assigners.Length];
            for (int i = 0; i < playerHitBoxes.Length; ++i) { playerHitBoxes[i] = _assigners[i].playerHitBox; }

            return true; ;
        }
        //---------------------------------------
    }
}

