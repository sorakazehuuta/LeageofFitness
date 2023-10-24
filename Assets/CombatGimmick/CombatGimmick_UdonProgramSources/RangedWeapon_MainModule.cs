
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using System.Collections;

//---------------------------------------
//遠距離攻撃武器のクラス
//RangedWeapon_SubModuleを併用すると両手持ち武器になる
//---------------------------------------

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [DefaultExecutionOrder(10)]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
    public class RangedWeapon_MainModule : Weapon
    {
        //---------------------------------------
        [Header("必須項目.無いとエラー")]
        [Tooltip("武器モデルの親オブジェクト")] [SerializeField] Transform ModelTransform;                                       
        //---------------------------------------
        [Header("任意項目(連射設定)")]
        [Tooltip("オート連射ならtrue")] [SerializeField] bool automatic;
        [Tooltip("最大連射速度(round/s)")] [SerializeField] float FireInterval = 0.15f;
        [Tooltip("最大弾数")] [SerializeField] int MaxMagazine = 30;
        [Tooltip("残り弾数表示")] [SerializeField] Text MagazineText;
        [Tooltip("使用する弾薬の種類(-1なら無効)")] public int AmmoType = -1;
        //---------------------------------------
        [Header("任意項目(リロード設定)")]
        [Tooltip("リロード時間")] [SerializeField] float reloadTime = 3.0f;
        [Tooltip("リロード時に動作するアニメーター")] [SerializeField] Animator ReloadAnimator;
        [Tooltip("リロード開始時の効果音")] [SerializeField] AudioClip StartReloadSound;
        [Tooltip("リロード終了時の効果音")] [SerializeField] AudioClip CompleteReloadSound;
        //---------------------------------------
        [Header("任意項目(その他)")]
        [Tooltip("射撃時の効果音")] [SerializeField] AudioClip FireSound;
        [Tooltip("ピックアップ時の効果音")] [SerializeField] AudioClip PickupSound;
        [Tooltip("リロード中などで射撃できなかった場合の効果音")] [SerializeField] AudioClip ClickSound;
        [Tooltip("チェックを入れるとドロップ後に自動でリセット")] [SerializeField] bool AutoResetAfterDrop = true;
        [Tooltip("ドロップしてから自動でリセットするまでの時間")] public float AutoResetDuration = 30.0f;
        //---------------------------------------
        [Header("----VRコントローラ設定----")]
        [Tooltip("trueなら射撃時などにコントローラを振動させる")] public bool EnableHaptics;
        [Tooltip("コントローラの振動時間")] public float HapticsDuration = 0.3f;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("発射する通常弾")] [SerializeField] Bullet[] bullet;
        [HideInInspector] [Tooltip("発射する誘導ミサイル")] [SerializeField] Missile[] missile;
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        [HideInInspector] [Tooltip("VRC_Pickup")] public VRC_Pickup Pickup;
        [HideInInspector] [Tooltip("AudioSource")] [SerializeField] AudioSource audioSource;
        [HideInInspector] [Tooltip("PointerModule")] [SerializeField] PointerModule pointerModule;
        [HideInInspector] [Tooltip("CollisionCheckerModule")] public CollisionCheckerModule collisionCheckerModule;
        [HideInInspector] [Tooltip("RangedWeapon_SubModule")] public RangedWeapon_SubModule SubModule;
        //---------------------------------------
        float lastFireTime; //最後に射撃した時刻
        bool isTriggered;   //オート連射の場合のみ使用.トリガーダウン時にtrue
        bool reloading; //リロード中はtrue
        int Magazine;   //現在のマガジン残弾
        float SubModuleLerp;
        bool VRmode;
        float lastDroppedTime;  //最後に武器をDropした時刻
        Vector3 ModelLookAtPosition;    //SubModuleがある場合のみ有効.通常はSubModuleの座標.Bipodを使った場合は,使った時のSubModuleの座標.
        [HideInInspector] public TeamName Team;    //武器を持っている人のチーム番号.
        ParticleSystem[] BulletParticle;      //射撃したプレイヤーに見えるパーティクル.命中判定あり
        ParticleSystem[] DummyBulletParticle; //射撃したプレイヤー以外に見える演出用パーティクル.bullet.gameObjectをクローンし、命中判定を自動でなくす    
        //---------------------------------------
        void Start()
        {
            Initialize();
        }
        //---------------------------------------
        private void Update()
        {
            //モデルをPickupに追従させる
            
            //両手武器の場合
            if (SubModule) { MoveDualweaponModel(); return; }

            //片手武器の場合
            ModelTransform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
            return;
        }
        //---------------------------------------
        public void MoveDualweaponModel()
        {
            //デスクトップモードはSubModuleの位置を自動で合わせる
            if (Utilities.IsValid(Networking.LocalPlayer) && Pickup.currentPlayer == Networking.LocalPlayer && !VRmode)
            {
                SubModule.transform.position = this.transform.position + this.transform.rotation * SubModule.InitialRelativePosition;
                SubModule.transform.rotation = this.transform.rotation;
            }

            //Bipodが有効の場合
            if (SubModule.bipodEnabled)
            {
                ModelTransform.position = this.transform.position;
                ModelTransform.LookAt(SubModule.bipodPosition, this.transform.up);
                return;
            }

            //Bipodが無効の場合
            ModelTransform.position = this.transform.position;
            ModelTransform.LookAt(SubModule.transform.position, this.transform.up);
            return;
        }
        //---------------------------------------
        public void Fire()
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_PlayFireEffect");

            PlayHaptics();
            lastFireTime = Time.time;

            if (playerManager.reloadType == ReloadType.NoReload) { return; }

            --Magazine;
            if (MagazineText) { MagazineText.text = Magazine.ToString(); }
            if (playerManager.reloadType == ReloadType.AutoReload && Magazine <= 0) { StartReload(); }
        }
        //---------------------------------------
        public void Network_PlayFireEffect()
        {
            //通常弾の場合
            if (BulletParticle.Length > 0)
            {
                if (Utilities.IsValid(Networking.LocalPlayer) && Pickup.currentPlayer == Networking.LocalPlayer)
                {
                    for (int i = 0; i < bullet.Length; ++i) { BulletParticle[i].Play(); }
                }
                else
                {
                    for (int i = 0; i < bullet.Length; ++i) { DummyBulletParticle[i].Play(); }                    
                }
                for (int i = 0; i < bullet.Length; ++i) { if (FireSound) { bullet[i].PlayFireSound(FireSound); } }

                return;
            }            
            
            //誘導ミサイルの場合
            if (missile.Length > 0)
            {
                for (int i = 0; i < missile.Length; ++i)
                {
                    if (missile[i].CheckTargetLocked())
                    {
                        missile[i].Launch();
                        if (missile[i].FireAudioClip) { audioSource.PlayOneShot(missile[i].FireAudioClip); }
                    }
                }

                return;
            }            
        }
        //---------------------------------------
        public void PlayHaptics()
        {
            if (EnableHaptics && Utilities.IsValid(Networking.LocalPlayer))
            {
                if (Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left) == Pickup)
                {
                    Networking.LocalPlayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Left, HapticsDuration, 1.0f, 300.0f);
                }
                if (Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right) == Pickup)
                {
                    Networking.LocalPlayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Right, HapticsDuration, 1.0f, 300.0f);
                }

                if (SubModule) { SubModule.PlaySubModuleHaptics(HapticsDuration); }
            }
        }
        //---------------------------------------
        public void EnableWeapon()
        {
            Pickup.pickupable = true;

            if (SubModule) { SubModule.EnableWeapon(); }
        }
        //---------------------------------------
        public void DisableWeapon()
        {
            if(Utilities.IsValid(Networking.LocalPlayer) && Pickup.currentPlayer == Networking.LocalPlayer) { Pickup.Drop(); }
            Pickup.pickupable = false;

            if (SubModule) { SubModule.DisableWeapon(); }
        }
        //---------------------------------------
        public override void OnPickup()
        {
            if (!CheckDualWielding()) { Pickup.Drop(); return; }

            if (pointerModule) { pointerModule.UnlockTarget(); }
            for (int i = 0; i < bullet.Length; ++i) { if (bullet[i]) { bullet[i].SetTeam(playerManager.Team); } }   
            
            if (pointerModule) { pointerModule.OnPickup(); }
            if (PickupSound) { audioSource.PlayOneShot(PickupSound, 1.0f); }

            for (int i = 0; i < bullet.Length; ++i)
            {
                if (playerManager.playerHitBox && bullet[i].subBullet)
                {
                    bullet[i].subBullet.UpdateCarrierPlayerHitBoxIndex(playerManager.playerHitBox.PlayerHitBoxIndex);
                }
            }

            PlayHaptics();

            StartReloadOnPickup();
            if(!VRmode && SubModule) { SubModule.OnPickup(); TrySetOwner(SubModule.gameObject); SubModule.StartReloadOnPickup(); }
        }
        //---------------------------------------
        public void StartReloadOnPickup()
        {
            if (reloading) { return; }
            if (playerManager.reloadType != ReloadType.AutoReload) { return; }
            if (Magazine > 0) { return; }
            if (AmmoType < 0) { return; }
            if (AmmoType >= playerManager.Ammo.Length) { return; }
            if (playerManager.Ammo[AmmoType] <= 0) { return; }

            if (playerManager.Ammo[AmmoType] >= MaxMagazine) { Magazine = MaxMagazine; playerManager.Ammo[AmmoType] -= MaxMagazine; }
            else { Magazine = playerManager.Ammo[AmmoType]; playerManager.Ammo[AmmoType] = 0; }
            reloading = true;
            if (MagazineText) { MagazineText.text = Magazine.ToString(); }
            if (StartReloadSound) { audioSource.PlayOneShot(StartReloadSound, 1.0f); }
            if (ReloadAnimator) { ReloadAnimator.SetTrigger("StartReload"); }
            PlayHaptics();
            playerManager.ShowAmmo();

            SendCustomEventDelayedSeconds("DelayedSecond_CompleteReload", reloadTime);
        }
        //---------------------------------------
        public bool CheckDualWielding()
        {
            if(playerManager.dualWieldingType == DualWieldingType.Enable) { return true; }
            if (!Utilities.IsValid(Networking.LocalPlayer)) { return false; }

            VRC_Pickup.PickupHand otherHand;
            if (Pickup.currentHand == VRC_Pickup.PickupHand.Left) { otherHand = VRC_Pickup.PickupHand.Right; }
            else { otherHand = VRC_Pickup.PickupHand.Left; }

            if (!Networking.LocalPlayer.GetPickupInHand(otherHand)) { return true; }

            VRC_Pickup otherPickup = Networking.LocalPlayer.GetPickupInHand(otherHand);

            if (otherPickup.GetComponent<RangedWeapon_MainModule>()) { return false; }
            if (otherPickup.GetComponent<MeleeWeapon>()) { return false; }
            if (otherPickup.GetComponent<ExWeapon>()) { return false; }

            return true;
        }
        //---------------------------------------
        public override void OnDrop()
        {
            if (automatic) { isTriggered = false; }
            if (pointerModule) { pointerModule.OnDrop(); }

            if (SubModule && SubModule.GetSubModuleFunctionType() == SubModuleFunctionType.Shield)
            {
                SubModule.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_CloseShield");
            }

            if (SubModule && SubModule.GetSubModuleFunctionType() == SubModuleFunctionType.Bipod)
            {
                SubModule.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_DisableBipod");
            }

            if (SubModule) { SubModule.ResetSubModuleTransform(); }

            //インベントリ機能用のフラグ更新
            if (playerManager.inventoryType == InventoryType.Retrieve || playerManager.inventoryType == InventoryType.ForcedRetrieve)
            {
                playerManager.LastDroppedPickup = Pickup;
            }

            if (VRmode)
            {
                Vector3 DropPos = this.transform.position;
                Vector3 HeadPos = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
                Quaternion HeadRot = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;

                //Dropしたオブジェクトの位置が、頭の左後方・右後方にあるかチェックする
                //PickupVector.zがマイナスなら後方、PickupVector.xがマイナスなら左
                Vector3 DropVector = Quaternion.Inverse(HeadRot) * (DropPos - HeadPos);

                //頭の後方でDropする
                if (playerManager.inventoryType == InventoryType.Single && DropVector.z < 0)
                {
                    playerManager.StorePickupInLeftInventory(Pickup);
                }
                if (playerManager.inventoryType == InventoryType.Double && DropVector.z < 0)
                {
                    //頭の左後方でDropする
                    if (DropVector.x < 0)
                    {
                        playerManager.StorePickupInLeftInventory(Pickup);
                    }
                    //頭の右後方でDropする
                    else
                    {
                        playerManager.StorePickupInRightInventory(Pickup);
                    }
                }
            }

            //自動リセット用のフラグ更新
            if (CheckAutoResetPickup())
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_UpdateLastDroppedTime");
            }
        }
        //---------------------------------------
        public bool CheckAutoResetPickup()
        {
            if (!AutoResetAfterDrop) { return false; }

            if (playerManager.inventoryType == InventoryType.SingleNoReset || playerManager.inventoryType == InventoryType.DoubleNoReset)
            {
                for (int i = 0; i < gameManager.Assigners.Length; ++i) { if (gameManager.Assigners[i].SingleInventoryPickup == Pickup || gameManager.Assigners[i].DoubleInventoryPickup == Pickup) { return false; } }
                return true;
            }
            if (playerManager.inventoryType == InventoryType.None) { return true; }

            if (playerManager.inventoryType == InventoryType.Single || playerManager.inventoryType == InventoryType.Double) { return false; }

            if (playerManager.inventoryType == InventoryType.Retrieve || playerManager.inventoryType == InventoryType.ForcedRetrieve) { return false; }

            else return true;
        }
        //---------------------------------------
        public override void OnPickupUseDown()
        {
            if (playerManager.playerHitBox && playerManager.playerHitBox.SyncedReviveTicket <= 0)
            {
                PlayClickSound();
                return;
            }

            if (reloading)
            {
                PlayClickSound();
                return;
            }

            if (Magazine <= 0)
            {
                PlayClickSound();
                return;
            }

            if (Time.time - lastFireTime < FireInterval * 0.90f)
            {
                PlayClickSound();
                return;
            }

            bool isReady;
            
            if (missile.Length > 0)
            {
                if (collisionCheckerModule) { isReady = missile[0].CheckTargetLocked() & !collisionCheckerModule.CollisionCheck(); }
                else { isReady = missile[0].CheckTargetLocked(); }
            }
            else if (collisionCheckerModule) { isReady = !collisionCheckerModule.CollisionCheck(); }
            else { isReady = true; }
            
            if (!isReady)
            {
                PlayClickSound();
                return;
            }

            Fire();
            if (automatic)
            {
                isTriggered = true;
                SendCustomEventDelayedSeconds("DelayedSecond_TryNextFire", FireInterval);
            }
            return;
        }
        //---------------------------------------
        public override void OnPickupUseUp()
        {
            if (automatic) { isTriggered = false; }
        }
        //---------------------------------------
        public void DelayedSecond_TryNextFire()
        {
            if (isTriggered)
            {
                OnPickupUseDown();
            }
        }
        //---------------------------------------
        public void PlayClickSound()
        {
            if (ClickSound) { audioSource.PlayOneShot(ClickSound, 1.0f); }
        }
        //---------------------------------------
        public bool GetReloading()
        {
            return reloading;
        }
        //---------------------------------------
        public bool SubtractAmmo()
        {
            //ゲーム開始前は無条件でリロード
            if (!gameManager.SyncedInGame)
            {
                Magazine = MaxMagazine;
                if (MagazineText) { MagazineText.text = Magazine.ToString(); }
                return true;
            }

            //弾薬指定なしの場合は無条件でリロード
            if (AmmoType < 0)
            {
                Magazine = MaxMagazine;
                if (MagazineText) { MagazineText.text = Magazine.ToString(); }
                return true;
            }

            //指定弾薬の予備なしの場合はリロード中止
            if (playerManager.Ammo[AmmoType] <= 0) { return false; }

            //指定弾薬を消費してリロード
            int subtractValue = MaxMagazine - Magazine;
            if (playerManager.Ammo[AmmoType] < subtractValue) { subtractValue = playerManager.Ammo[AmmoType]; }
            playerManager.Ammo[AmmoType] -= subtractValue;
            Magazine += subtractValue;
            if (MagazineText) { MagazineText.text = Magazine.ToString(); }
            playerManager.ShowAmmo();
            return true;
        }
        //---------------------------------------
        public void StartReload()
        {
            //弾薬指定なしの場合
            if (!SubtractAmmo()) { PlayClickSound(); return; }

            reloading = true;
            if (StartReloadSound) { audioSource.PlayOneShot(StartReloadSound, 1.0f); }
            if (ReloadAnimator) { ReloadAnimator.SetTrigger("StartReload"); }

            PlayHaptics();

            SendCustomEventDelayedSeconds("DelayedSecond_CompleteReload", reloadTime);
        }
        //---------------------------------------
        public void ReloadButtonPressed()
        {
            if (!reloading) { StartReload(); }
        }
        //---------------------------------------
        public int TryReloadOnTakeDropItem(int tookAmmoIndex, int tookAmmoValue)
        {
            //AutoReloadモードでマガジンが空の時に、対応する弾薬を拾ったら強制リロードする
            //未処理の弾数を返す
            if (reloading) { return tookAmmoValue; }
            if (playerManager.reloadType != ReloadType.AutoReload) { return tookAmmoValue; }
            if (AmmoType == -1) { return tookAmmoValue; }
            if (AmmoType != tookAmmoIndex) { return tookAmmoValue; }
            if (Magazine > 0) { return tookAmmoValue; }
            if (tookAmmoValue <= 0) { return tookAmmoValue; }

            if (tookAmmoValue >= MaxMagazine) { Magazine = MaxMagazine; tookAmmoValue -= MaxMagazine; }
            else { Magazine = tookAmmoValue; tookAmmoValue = 0; }
            reloading = true;
            if (MagazineText) { MagazineText.text = Magazine.ToString(); }
            if (StartReloadSound) { audioSource.PlayOneShot(StartReloadSound, 1.0f); }
            if (ReloadAnimator) { ReloadAnimator.SetTrigger("StartReload"); }
            PlayHaptics();
            SendCustomEventDelayedSeconds("DelayedSecond_CompleteReload", reloadTime);
            return tookAmmoValue;
        }
        //---------------------------------------
        public void DelayedSecond_CompleteReload()
        {
            reloading = false;
            if (CompleteReloadSound) { audioSource.PlayOneShot(CompleteReloadSound, 1.0f); }

            PlayHaptics();
        }
        //---------------------------------------
        public override void DealDamage(float dmg, GameObject target)
        {
            //ダウン状態・または戦闘中でない場合、ダメージを与えない
            if (!playerManager.isAlive || !gameManager.SyncedInBattle || !gameManager.SyncedInGame) { return; }

            if (target.GetComponent<BotHitBox>()) { target.GetComponent<BotHitBox>().TakeDamage(dmg,false); }
            if (target.GetComponent<PlayerHitBox>()) { target.GetComponent<PlayerHitBox>().TakeDamage(dmg, false); }
        }
        //---------------------------------------
        public Transform GetModelTransform()
        {
            return ModelTransform;
        }
        //---------------------------------------
        public void ResetTransform()
        {
            if (Pickup.IsHeld) { Pickup.Drop(); }

            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.identity;

            if (SubModule) { SubModule.ResetSubModuleTransform(); }
        }
        //---------------------------------------
        public void Network_UpdateLastDroppedTime()
        {
            if (AutoResetDuration < 0) { return; }

            lastDroppedTime = Time.time;
            SendCustomEventDelayedSeconds("DelayedSecond_TryReset", AutoResetDuration);
        }
        //---------------------------------------
        public void DelayedSecond_TryReset()
        {
            //フレーム単位で処理が前後することを考慮して、AutoResetDurationは0.9倍
            if (Pickup.IsHeld) { return; }
            if (lastDroppedTime + AutoResetDuration * 0.9f > Time.time) { return; }

            Magazine = MaxMagazine;
            if (CheckLocalPlayerIsOwner(this.gameObject)) { ResetTransform(); }            
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
        public void ResetMagazine()
        {
            Magazine = MaxMagazine;
            if (MagazineText) { MagazineText.text = Magazine.ToString(); }

            if (SubModule && SubModule.GetSubModuleFunctionType() == SubModuleFunctionType.FireSubWeapon) { SubModule.ResetMagazine(); }
        }
        //---------------------------------------
        public override void Initialize()
        {
            if (ReloadAnimator) { ReloadAnimator.speed = 1.0f / reloadTime; }

            if (SubModule)
            {
                SubModule.SetMainModule(this.GetComponent<RangedWeapon_MainModule>());
                ModelLookAtPosition = SubModule.transform.position;
            }

            if (pointerModule)
            {
                pointerModule.SetMainModule(this.GetComponent<RangedWeapon_MainModule>());

                for (int i = 0; i < missile.Length; ++i)
                {
                    if (missile[0]) { pointerModule.SetMissile(missile); }
                }                    
            }
            if (collisionCheckerModule)
            {
                collisionCheckerModule.SetMainModule(this.GetComponent<RangedWeapon_MainModule>());
            }

            BulletParticle = new ParticleSystem[bullet.Length];
            DummyBulletParticle = new ParticleSystem[bullet.Length];
            for (int i = 0; i < bullet.Length; ++i)
            {
                if (bullet[i])
                {
                    BulletParticle[i] = bullet[i].GetComponent<ParticleSystem>();
                    bullet[i].SetMainModule(this.GetComponent<RangedWeapon_MainModule>());
                    
                    //ダミーの銃弾パーティクルを自動で作成
                    GameObject DummyBulletObject = VRCInstantiate(bullet[i].gameObject);
                    DummyBulletObject.transform.SetParent(bullet[i].transform.parent);
                    DummyBulletObject.transform.localPosition = bullet[i].transform.localPosition;
                    DummyBulletObject.transform.localRotation = bullet[i].transform.localRotation;
                    DummyBulletParticle[i] = DummyBulletObject.GetComponent<ParticleSystem>();
                    var collisionModule = DummyBulletParticle[i].collision;
                    collisionModule.sendCollisionMessages = false;

                    if (bullet[i].subBullet) { bullet[i].subBullet.ClonedSubBullet = DummyBulletObject.GetComponent<Bullet>().subBullet; }
                }
            }

            Magazine = MaxMagazine;
            if (MagazineText) { MagazineText.text = Magazine.ToString(); }

            if (Utilities.IsValid(Networking.LocalPlayer) && Networking.LocalPlayer.IsUserInVR()) { VRmode = true; }
            else { VRmode = false; }
        }
        //---------------------------------------
        public bool IsSubModuleBullet(Bullet b, Bullet[] _subModuleBullets)
        {
            if(_subModuleBullets.Length <= 0) { return false; }

            for(int i = 0; i < _subModuleBullets.Length; ++i)
            {
                if(b == _subModuleBullets[i]) { return true; }
            }

            return false;
        }
        //---------------------------------------
        public override bool AutoBuild(PlayerManager pm, GameManager gm)
        {
            Bullet[] subModuleBullets = new Bullet[0];   //SubModule用に指定されているBulletがある場合は、MainModuleのAutoBuildからは除外する

            gameManager = gm;
            playerManager = pm;
            audioSource = this.GetComponent<AudioSource>();

            int subModuleNum = 0;
            Transform parentTransform = this.transform.parent;
            foreach (Transform child in parentTransform) { if (child.GetComponent<RangedWeapon_SubModule>()) { ++subModuleNum; } }
            if (subModuleNum > 1)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + "にSubModuleが2つ以上あります.", this.gameObject);
                return false;
            }
            foreach (Transform child in parentTransform) { if (child.GetComponent<RangedWeapon_SubModule>()) { SubModule = child.GetComponent<RangedWeapon_SubModule>(); } }
            if (SubModule)
            {
                SubModule.SetMainModule(this.GetComponent<RangedWeapon_MainModule>());
                SubModule.SetInitialRelativePosition();
                subModuleBullets = SubModule.GetSubModuleBullets();
            }

            foreach (Transform child in ModelTransform) { if (child.GetComponent<RangedWeapon_MeleeModule>()) { child.GetComponent<RangedWeapon_MeleeModule>().SetMainModule(this.GetComponent<RangedWeapon_MainModule>()); } }

            int bulletNum = 0;
            foreach (Transform child in ModelTransform) { if (child.GetComponent<Bullet>() && !IsSubModuleBullet(child.GetComponent<Bullet>(), subModuleBullets)) { ++bulletNum; } }
            bullet = new Bullet[bulletNum];
            int n = 0;
            foreach (Transform child in ModelTransform) { if (child.GetComponent<Bullet>() && !IsSubModuleBullet(child.GetComponent<Bullet>(), subModuleBullets)) { bullet[n] = child.GetComponent<Bullet>();++n; } }

            int missileNum = 0;
            foreach (Transform child in ModelTransform) { if (child.GetComponent<Missile>()) { ++missileNum; } }
            missile = new Missile[missileNum];
            n = 0;
            foreach (Transform child in ModelTransform) { if (child.GetComponent<Missile>()) { missile[n] = child.GetComponent<Missile>(); ++n; } }

            int pointerModuleNum = 0;
            foreach (Transform child in ModelTransform) { if (child.GetComponent<PointerModule>()) { ++pointerModuleNum; } }
            if (pointerModuleNum > 1)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + "にPointerModuleが2つ以上あります.", this.gameObject);
                return false;
            }
            foreach (Transform child in ModelTransform) { if (child.GetComponent<PointerModule>()) { pointerModule = child.GetComponent<PointerModule>(); } }

            int collisionCheckerModuleNum = 0;
            foreach (Transform child in ModelTransform) { if (child.GetComponent<CollisionCheckerModule>()) { ++collisionCheckerModuleNum; } }
            if (collisionCheckerModuleNum > 1)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + "にCollisionCheckerModuleが2つ以上あります.", this.gameObject);
                return false;
            }
            foreach (Transform child in ModelTransform) { if (child.GetComponent<CollisionCheckerModule>()) { collisionCheckerModule = child.GetComponent<CollisionCheckerModule>(); } }

            if (!this.GetComponent<VRC_Pickup>())
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " にVRC_Pickupコンポーネントがありません.", this.gameObject);
                return false;
            }
            Pickup = this.GetComponent<VRC_Pickup>();            

            if (bullet.Length <= 0 && missile.Length <= 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " にBulletまたはMissileが設定されていません.", this.gameObject);
                return false;
            }

            if(missile.Length > 0 && pointerModule)
            {
                pointerModule.missile = missile;
                pointerModule.SetMainModule(this.GetComponent<RangedWeapon_MainModule>());
                for (int i = 0; i < missile.Length; ++i) { missile[i].mainModule = this.GetComponent<RangedWeapon_MainModule>(); }                    
            }

            if (missile.Length > 0 && !pointerModule)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + "にPointerModuleがありません.ロックオンができません.", this.gameObject);
                return false;
            }

            for (int i = 0; i < bullet.Length; ++i)
            {
                if (bullet[i] && !bullet[i].GetComponent<ParticleSystem>())
                {
                    Debug.Log("<color=#ff0000>Warning:</color> " + bullet[i].gameObject.name + "にParticleSystemコンポーネントがありません.", bullet[i].gameObject);
                    return false;
                }
            }

            if (pm.DefaultAmmo.Length <= AmmoType)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " AmmoTypeの値は,PlayerManagerのDefaultAmmoの要素数より小さくしてください.", this.gameObject);
                return false;
            }

            if (playerManager.reloadType != ReloadType.NoReload && MaxMagazine <= 0)
            {
                MaxMagazine = 30;
            }
            if (playerManager.reloadType != ReloadType.NoReload && reloadTime <= 0)
            {
                reloadTime = 3.0f;
            }

            return true;
        }
        //---------------------------------------
    }
}


