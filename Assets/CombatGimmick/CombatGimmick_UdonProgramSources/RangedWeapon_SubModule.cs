
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

//---------------------------------------
//遠距離攻撃武器のクラス
//RangedWeapon_MainModuleと組み合わせて両手持ち武器を作る   
//---------------------------------------

namespace CombatGimmick
{
    public enum SubModuleFunctionType
    {
        None, DashPlayerForward, DashModelForward, Shield, Booster, Bipod, FireSubWeapon
    }

    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
    public class RangedWeapon_SubModule : Weapon
    {
        //---------------------------------------
        [Header("----サブモジュールの機能設定----")]
        [Tooltip("Triggerしたときの機能")] [SerializeField] SubModuleFunctionType subModuleFunctionType;
        [Tooltip("サブモジュール使用に必要なスタミナ")] [SerializeField] float CostStamina;
        //---------------------------------------
        [Header("----DashPlayerForward/DashModelForwardモードの設定----")]
        [Tooltip("ダッシュの速度")] [SerializeField] float DashImpulse;
        //---------------------------------------
        [Header("----Shieldモードの設定----")]
        [Tooltip("DeployShieldモードで展開するシールド")] [SerializeField] GameObject ShieldObject;
        [Tooltip("シールド展開時の効果音")] [SerializeField] AudioClip OpenShieldClip;
        //---------------------------------------
        [Header("----Boosterモードの設定----")]
        [Tooltip("ブーストの加速力")] [SerializeField] float _thruststrength;
        [Tooltip("ブーストの減速力")] [SerializeField] float _backthruststrength;
        [Tooltip("ブースト時のスタミナ消費速度")] [SerializeField] float StaminaConsumptionRate;
        [Tooltip("ブースト時のエフェクト")] [SerializeField] ParticleSystem BoostParticle;
        [Tooltip("ブースト時の効果音")] [SerializeField] AudioClip BoostClip;
        bool boosting;
        //---------------------------------------
        [Header("----FireSubWeaponモードの設定----")]
        [Tooltip("発射する通常弾")] [SerializeField] Bullet[] bullet;
        [Tooltip("オート連射ならtrue")] [SerializeField] bool automatic;
        [Tooltip("最大連射速度(round/s)")] [SerializeField] float FireInterval = 0.15f;
        [Tooltip("最大弾数")] [SerializeField] int MaxMagazine = 30;
        [Tooltip("残り弾数表示")] [SerializeField] Text MagazineText;
        [Tooltip("使用する弾薬の種類(-1なら無効)")] public int AmmoType = -1;
        [Tooltip("リロード時間")] [SerializeField] float reloadTime = 3.0f;
        [Tooltip("リロード時に動作するアニメーター")] [SerializeField] Animator ReloadAnimator;
        [Tooltip("リロード開始時の効果音")] [SerializeField] AudioClip StartReloadSound;
        [Tooltip("リロード終了時の効果音")] [SerializeField] AudioClip CompleteReloadSound;
        [Tooltip("射撃時の効果音")] [SerializeField] AudioClip FireSound;
        [Tooltip("リロード中などで射撃できなかった場合の効果音")] [SerializeField] AudioClip ClickSound;
        //---------------------------------------
        //[HideInInspector][Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("このオブジェクトのVRC_Pickup")] [SerializeField] VRC_Pickup Pickup;
        [HideInInspector] [Tooltip("AudioSource")] [SerializeField] AudioSource audioSource;
        [HideInInspector] [Tooltip("RangedWeapon_MainModule")] [SerializeField] RangedWeapon_MainModule mainModule;
        [HideInInspector] [Tooltip("playerManager")] [SerializeField] PlayerManager playerManager;
        [HideInInspector] [Tooltip("GameManager")] [SerializeField] GameManager gameManager;
        [HideInInspector] [Tooltip("Vector3")] public Vector3 InitialRelativePosition;
        //---------------------------------------
        private VRCPlayerApi localPlayer;
        bool VRmode;
        Transform ModelTransform;
        [HideInInspector] public bool bipodEnabled;
        [HideInInspector] public Vector3 bipodPosition;
        bool isTriggered;   //オート連射の場合のみ使用.トリガーダウン時にtrue
        bool reloading; //リロード中はtrue
        int Magazine;   //現在のマガジン残弾
        float lastFireTime; //最後に射撃した時刻
        [HideInInspector] public ParticleSystem[] BulletParticle;      //射撃したプレイヤーに見えるパーティクル.命中判定あり
        [HideInInspector] public ParticleSystem[] DummyBulletParticle; //射撃したプレイヤー以外に見える演出用パーティクル.bullet.gameObjectをクローンし、命中判定を自動でなくす    
        //---------------------------------------
        void Start()
        {
            Initialize();
        }
        //---------------------------------------
        private void FixedUpdate()
        {
            //このオブジェクトを持ってトリガー中のみ、ブースト発動
            if (Utilities.IsValid(localPlayer) && boosting)  
            {
                float DeltaTime = Time.fixedDeltaTime;

                float Thrust;
                if(Pickup.currentHand == VRC_Pickup.PickupHand.Left) { Thrust = Mathf.Max(Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryIndexTrigger"), Input.GetKey(KeyCode.Space) ? 1 : 0); }
                else { Thrust = Mathf.Max(Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryIndexTrigger"), Input.GetKey(KeyCode.Space) ? 1 : 0); }

                playerManager.stamina -= DeltaTime * StaminaConsumptionRate;
                if(playerManager.stamina <= 0)
                {
                    playerManager.stamina = 0;
                    StopBoosting();
                }

                Vector3 PlayerVel = localPlayer.GetVelocity();  
                
                Vector3 NewForwardVec;
                NewForwardVec = Thrust * ModelTransform.forward;
                
                float BackThrustAmount = -((Vector3.Dot(PlayerVel, NewForwardVec)) * _backthruststrength * DeltaTime);
                NewForwardVec = NewForwardVec * _thruststrength * Thrust * DeltaTime * Mathf.Max(1, (BackThrustAmount * Thrust));

                localPlayer.SetVelocity(PlayerVel + NewForwardVec);
            }
        }
        //---------------------------------------
        public void TryStartBoosting()
        {
            if (playerManager.stamina >= 0 && mainModule.GetComponent<VRC_Pickup>().currentPlayer == localPlayer)
            {
                boosting = true;
                playerManager.RegenStamina = false;
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_PlayBoostEffect");
            }
        }
        //---------------------------------------
        public void Network_PlayBoostEffect()
        {
            if (BoostParticle) { BoostParticle.Play(); }
            if (BoostClip) { this.GetComponent<AudioSource>().PlayOneShot(BoostClip, 1.0f); }
        }
        //---------------------------------------
        public void Network_StopBoostEffect()
        {
            if (BoostParticle) { BoostParticle.Stop(); }
        }
        //---------------------------------------
        public void StopBoosting()
        {
            boosting = false;
            playerManager.RegenStamina = true;
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_StopBoostEffect");
        }
        //---------------------------------------
        public override void OnPlayerRespawn(VRCPlayerApi player)
        {
            if (player.isLocal)
            { player.SetVelocity(Vector3.zero); }
        }
        //---------------------------------------
        public SubModuleFunctionType GetSubModuleFunctionType()
        {
            return subModuleFunctionType;
        }
        //---------------------------------------
        public override void OnPickupUseDown()
        {
            if (playerManager.stamina < CostStamina) { return; }
            playerManager.stamina -= CostStamina;

            if (subModuleFunctionType == SubModuleFunctionType.DashPlayerForward && Utilities.IsValid(Networking.LocalPlayer))
            {
                Dash(Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation * Vector3.forward);    //プレイヤーの視線方向にダッシュ
                return;
            }

            if (subModuleFunctionType == SubModuleFunctionType.DashModelForward && Utilities.IsValid(Networking.LocalPlayer))
            {
                Dash(mainModule.GetModelTransform().forward);    //銃身の方向にダッシュ
                return;
            }

            if (subModuleFunctionType == SubModuleFunctionType.Shield && Utilities.IsValid(Networking.LocalPlayer))
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All,"Network_OpenShield"); //シールド展開
                return;
            }

            if (subModuleFunctionType == SubModuleFunctionType.Booster && Utilities.IsValid(Networking.LocalPlayer))
            {
                TryStartBoosting(); //ブースト開始
                return;
            }

            if (subModuleFunctionType == SubModuleFunctionType.Bipod)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_EnableBipod");
                return;
            }

            if (subModuleFunctionType == SubModuleFunctionType.FireSubWeapon)
            {
                TryShootSubWeapon();
                return;
            }
        }
        //---------------------------------------
        public override void OnPickupUseUp()
        {   
            if (subModuleFunctionType == SubModuleFunctionType.Shield && Utilities.IsValid(Networking.LocalPlayer))
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_CloseShield");
                return;
            }

            if (subModuleFunctionType == SubModuleFunctionType.Booster && Utilities.IsValid(Networking.LocalPlayer))
            {
                StopBoosting();
                return;
            }

            if (subModuleFunctionType == SubModuleFunctionType.Bipod)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_DisableBipod");
                return;
            }

            if (subModuleFunctionType == SubModuleFunctionType.FireSubWeapon)
            {
                if (automatic) { isTriggered = false; }
                return;
            }
        }
        //---------------------------------------
        public void TryShootSubWeapon()
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

            if (mainModule.collisionCheckerModule) { isReady = !mainModule.collisionCheckerModule.CollisionCheck(); }
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
        public void PlayClickSound()
        {
            if (ClickSound) { audioSource.PlayOneShot(ClickSound, 1.0f); }
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
        }
        //---------------------------------------
        public void StartReload()
        {
            if (!SubtractAmmo()) { PlayClickSound(); return; }

            reloading = true;
            if (StartReloadSound) { audioSource.PlayOneShot(StartReloadSound, 1.0f); }
            if (ReloadAnimator) { ReloadAnimator.SetTrigger("StartReload"); }

            PlayHaptics();

            SendCustomEventDelayedSeconds("DelayedSecond_CompleteReload", reloadTime);
        }
        //---------------------------------------
        public void ResetMagazine()
        {
            Magazine = MaxMagazine;
            if (MagazineText) { MagazineText.text = Magazine.ToString(); }
        }
        //---------------------------------------
        public int TryReloadOnTakeDropItem(int tookAmmoIndex, int tookAmmoValue)
        {
            //AutoReloadモードでマガジンが空の時に、対応する弾薬を拾ったら強制リロードする
            //拾った弾薬のうち、リロード処理で余った数を返す

            if (subModuleFunctionType != SubModuleFunctionType.FireSubWeapon) { return tookAmmoValue; }
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
        public bool GetReloading()
        {
            return reloading;
        }
        //---------------------------------------
        public void DelayedSecond_CompleteReload()
        {
            reloading = false;
            if (CompleteReloadSound) { audioSource.PlayOneShot(CompleteReloadSound, 1.0f); }

            PlayHaptics();
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
        public void PlayHaptics()
        {
            if (mainModule.EnableHaptics && Utilities.IsValid(Networking.LocalPlayer))
            {
                if (Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left) == Pickup)
                {
                    Networking.LocalPlayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Left, mainModule.HapticsDuration, 1.0f, 300.0f);
                }
                if (Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right) == Pickup)
                {
                    Networking.LocalPlayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Right, mainModule.HapticsDuration, 1.0f, 300.0f);
                }                
            }
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
        public void Network_EnableBipod()
        {
            EnableBipod();
        }
        //---------------------------------------
        public void EnableBipod()
        {
            bipodEnabled = true;
            bipodPosition = this.transform.position;
        }
        //---------------------------------------
        public void Network_DisableBipod()
        {
            DisableBipod();
        }
        //---------------------------------------
        public void DisableBipod()
        {
            bipodEnabled = false;
        }
        //---------------------------------------
        public void Network_OpenShield()
        {
            ShieldObject.SetActive(true);
            if (OpenShieldClip) { this.GetComponent<AudioSource>().PlayOneShot(OpenShieldClip, 1.0f); }
        }
        //---------------------------------------
        public void CloseShield()
        {
            ShieldObject.SetActive(false);
        }
        //---------------------------------------
        public void Network_CloseShield()
        {
            CloseShield();
        }
        //---------------------------------------
        public void Dash(Vector3 direction)
        {
            Vector3 currentSpeed = Networking.LocalPlayer.GetVelocity();
            Vector3 Impulse = direction * DashImpulse;
            Networking.LocalPlayer.SetVelocity(currentSpeed + Impulse);
        }
        //---------------------------------------
        public override void OnPickup()
        {
            if (subModuleFunctionType == SubModuleFunctionType.Shield && Utilities.IsValid(Networking.LocalPlayer))
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_CloseShield");
            }

            for (int i = 0; i < bullet.Length; ++i)
            {
                if (playerManager.playerHitBox && bullet[i] && bullet[i].subBullet)
                {
                    bullet[i].subBullet.UpdateCarrierPlayerHitBoxIndex(playerManager.playerHitBox.PlayerHitBoxIndex);
                }
            }

            StartReloadOnPickup();
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
        public override void OnDrop()
        {
            ResetSubModuleTransform();

            if (subModuleFunctionType == SubModuleFunctionType.Shield && Utilities.IsValid(Networking.LocalPlayer))
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_CloseShield");
                return;
            }

            if (subModuleFunctionType == SubModuleFunctionType.Booster && Utilities.IsValid(Networking.LocalPlayer))
            {
                boosting = false;
                return;
            }

            if (subModuleFunctionType == SubModuleFunctionType.Bipod)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_DisableBipod");
                return;
            }
        }
        //---------------------------------------
        public void EnableWeapon()
        {
            Pickup.pickupable = true;
        }
        //---------------------------------------
        public void DisableWeapon()
        {
            if (Utilities.IsValid(Networking.LocalPlayer) && Pickup.currentPlayer == Networking.LocalPlayer)
            {
                Pickup.Drop();
            }
            Pickup.pickupable = false;
        }
        //---------------------------------------
        public void ResetSubModuleTransform()
        {
            if (Pickup.IsHeld) { return; }

            this.transform.position = mainModule.transform.position + mainModule.transform.rotation * InitialRelativePosition;
            this.transform.rotation = mainModule.transform.rotation;
        }
        //---------------------------------------
        public void PlaySubModuleHaptics(float hapticsDuration)
        {
            //Utilities.IsValid(Networking.LocalPlayer) == trueはMainModule側で判別済み
            if (Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left) == Pickup)
            {
                Networking.LocalPlayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Left, hapticsDuration, 1.0f, 300.0f);
            }
            if (Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right) == Pickup)
            {
                Networking.LocalPlayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Right, hapticsDuration, 1.0f, 300.0f);
            }
        }
        //---------------------------------------
        public void SetPlayerManager(PlayerManager pm)
        {
            playerManager = pm;
        }
        //---------------------------------------
        public void SetModelTransform(Transform _modelTransform)
        {
            ModelTransform = _modelTransform;
        }
        //---------------------------------------
        public void SetMainModule(RangedWeapon_MainModule mm)
        {
            mainModule = mm;
        }
        //---------------------------------------
        public RangedWeapon_MainModule GetMainModule()
        {
            return mainModule;
        }
        //---------------------------------------
        public void SetInitialRelativePosition()
        {
            InitialRelativePosition = Quaternion.Inverse(mainModule.transform.rotation) * (this.transform.position - mainModule.transform.position);
        }
        //---------------------------------------
        public override void Initialize()
        {
            ModelTransform = mainModule.GetModelTransform();

            if (!this.GetComponent<VRC_Pickup>())
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " has no VRC_Pickup component");
            }
            else
            {
                Pickup = this.GetComponent<VRC_Pickup>();
            }
            if(subModuleFunctionType == SubModuleFunctionType.DashPlayerForward || subModuleFunctionType == SubModuleFunctionType.DashModelForward)
            {
                if(DashImpulse <= 0)
                {
                    Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " DashImpulse must be greater than 0");
                }
            }

            if (subModuleFunctionType == SubModuleFunctionType.Shield && Utilities.IsValid(Networking.LocalPlayer))
            {
                CloseShield();
            }

            if (subModuleFunctionType == SubModuleFunctionType.FireSubWeapon)
            {
                if (ReloadAnimator) { ReloadAnimator.speed = 1.0f / reloadTime; }
                Magazine = MaxMagazine;
                if (MagazineText) { MagazineText.text = Magazine.ToString(); }
            }

            localPlayer = Networking.LocalPlayer;
            if (!Utilities.IsValid(localPlayer)) { gameObject.SetActive(false); }

            if (Utilities.IsValid(Networking.LocalPlayer) && Networking.LocalPlayer.IsUserInVR()) { VRmode = true; }
            else { VRmode = false; }

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
        }
        //---------------------------------------
        public Bullet[] GetSubModuleBullets()
        {
            return bullet;
        }
        //---------------------------------------
        public bool AutoBuild(GameManager gm, PlayerManager pm)
        {
            gameManager = gm;
            playerManager = pm;
            audioSource = this.GetComponent<AudioSource>();

            if (!this.GetComponent<VRC_Pickup>())
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " にVRC_Pickupコンポーネントがありません.", this.gameObject);
                return false;
            }
            Pickup = this.GetComponent<VRC_Pickup>();

            return true;
        }
        //---------------------------------------
    }
}

