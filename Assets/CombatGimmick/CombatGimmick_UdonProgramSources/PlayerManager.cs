
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;
using UnityEngine.UI;

namespace CombatGimmick
{
    public enum ReloadType
    {
        NoReload, AutoReload, Bbutton, Abutton, PointUpward, PointDownward, PointUpDown
    }
    public enum DualWieldingType
    {
        Enable, Disable,
    }
    public enum InventoryType
    {
        None, Retrieve, ForcedRetrieve, Single, SingleNoReset, Double, DoubleNoReset
    }

    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class PlayerManager : UdonSharpBehaviour
    {
        //---------------------------------------
        [Tooltip("trueならジャンプ・着地時のエフェクトを同期する")] public bool GlobalJumpAndGroundedEffect;
        //---------------------------------------
        [Header("----リロード設定----")]
        [Tooltip("NoReloadならリロード不要")] public ReloadType reloadType;
        const KeyCode ReloadKey = KeyCode.E;  //リロード発動用のキー(デスクトップ用)
        const KeyCode SecondaryFuncKey = KeyCode.Q;  //セカンダリ機能発動用のキー(デスクトップ用)
        [Tooltip("上下に向けてリロードする際の許容角度")] [Range(0, 90)] public float PointReloadAngle = 20.0f;
        //---------------------------------------
        [Header("----ジャンプ設定----")]
        [Tooltip("trueならダブルジャンプ可能")] public bool EnableDoubleJump;
        [Tooltip("ダブルジャンプの強さ")] public float DoubleJumpImpulse;
        bool DoubleJumped;
        float lastSingleJumpTime;
        const float DoubleJumpDelay = 0.3f; //設定した秒数の間は、ジャンプ直後にダブルジャンプが暴発しないようにする
        bool lastGrounded;
        //---------------------------------------
        [Header("----スタミナ設定----")]
        [Tooltip("trueなら徐々にスタミナ回復")] public bool RegenStamina;
        [Tooltip("1秒あたりのスタミナ回復量")] [SerializeField] float StaminaRegenRate;
        [Tooltip("最大スタミナ量")] [SerializeField] float MaxStamina;
        public float stamina;
        //---------------------------------------
        [Header("----武器の制限----")]
        [Tooltip("Disableなら武器を2つ持てない(両手持ち武器は例外)")] public DualWieldingType dualWieldingType = DualWieldingType.Enable;
        [Tooltip("弾薬の初期携帯数")] public int[] DefaultAmmo;
        [Tooltip("弾薬の最大携帯数")] public int[] MaxAmmo;
        //---------------------------------------
        [Header("----インベントリ設定----")]
        [Tooltip("None以外ならインベントリを利用可能")] public InventoryType inventoryType;
        [HideInInspector] [Tooltip("左インベントリに格納したPickup")] public VRC_Pickup LeftInventoryPickup;
        [HideInInspector] [Tooltip("右インベントリに格納したPickup")] public VRC_Pickup RightInventoryPickup;
        [Tooltip("インベントリに格納したPickupを隠す座標")] public Vector3 HideInventoryPickupPos;
        [HideInInspector] public VRC_Pickup LastDroppedPickup;   //最後に落としたPickupオブジェクト(InventoryTypeがRetrieve・RetrieveAutoResetの時に使用)
        const KeyCode LeftInventoryKey = KeyCode.Alpha1;   //左インベントリを呼び出すキー(デスクトップ用)
        const KeyCode RightInventoryKey = KeyCode.Alpha2;  //右インベントリを呼び出すキー(デスクトップ用)
        //---------------------------------------
        [Header("----その他エフェクト.無しでも動作可能----")]
        [Tooltip("被弾時のエフェクト")] public GameObject OnDamageEffectObject;
        [Tooltip("ダメージ時の効果音")] [SerializeField] AudioClip DamageAudioClip;
        [Tooltip("回復時の効果音")] [SerializeField] AudioClip HealAudioClip;
        //---------------------------------------
        [Header("----データ表示.無しでも動作可能----")]
        [Tooltip("HP表記")] [SerializeField] Text HitPointText;
        [Tooltip("シールド表記")] [SerializeField] Text ShieldText;
        [Tooltip("スタミナ表記")] [SerializeField] Text StaminaText;
        [Tooltip("HPバー")] [SerializeField] Transform HitPointBarTransform;
        [Tooltip("シールドバー")] [SerializeField] Transform ShieldBarTransform;
        [Tooltip("スタミナバー")] [SerializeField] Transform StaminaBarTransform;
        [Tooltip("Ammoの残残表記")] [SerializeField] Text[] AmmoText;
        //---------------------------------------
        //[Header("手動で変更しないこと")]
        [HideInInspector] public Flag myFlag;
        [HideInInspector] public int[] Ammo;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        [HideInInspector] [Tooltip("DamageArea")] public DamageArea[] damageAreas;
        [HideInInspector] [Tooltip("ConquestZone")] public ConquestZone[] conquestZones;
        //---------------------------------------
        bool VRmode;
        [HideInInspector] public TeamName Team;
        [HideInInspector] public PlayerHitBox playerHitBox;   //ローカルプレイヤーに追従させるヒットボックス
        float oldTime;
        [HideInInspector] public bool isAlive = true;
        AudioSource audioSource;
        float lastJumpEffectTime;
        const float jumpEffectTimeInterval = 0.25f;   //坂道にいるとジャンプ・着地エフェクトが暴発するので、ある程度時間をおかないと連続発動しないようにする
        [HideInInspector] public float defaultJumpImpulse;
        [HideInInspector] public float defaultRunSpeed;
        [HideInInspector] public float defaultWalkSpeed;
        [HideInInspector] public float defaultStrafeSpeed;
        bool Initialized = false;
        public Assigner[] Assigners;
        float lastLeftButtonReloadInputTime;
        float lastRightButtonReloadInputTime;
        const float ButtonReloadInputInterval = 0.2f;    //0だとボタン押しっぱなしで連続判定が出る.最悪効果音と干渉してBGMが止まるので、最低限のインターバルを設定.
        //---------------------------------------
        void Start()
        {
            Initialize();
        }
        //---------------------------------------
        public void DelayedSecond_Initialized()
        {
            if (!Utilities.IsValid(Networking.LocalPlayer)) { SendCustomEventDelayedSeconds("DelayedSecond_Initialized", 1.0f); return; }

            defaultRunSpeed = Networking.LocalPlayer.GetRunSpeed();
            defaultWalkSpeed = Networking.LocalPlayer.GetWalkSpeed();
            defaultStrafeSpeed = Networking.LocalPlayer.GetStrafeSpeed();
            defaultJumpImpulse = Networking.LocalPlayer.GetJumpImpulse();

            Initialized = true;
        }
        //---------------------------------------
        private void Update()
        {
            if (!Initialized) { return; }

            if (!VRmode)
            {
                CheckReloadInputInDesktopMode();//デスクトップモードのリロード判定
                CheckSubModuleUseInputInDesktopMode();//デスクトップモードのサブモジュールUse判定
                CheckDesktopInventoryUseInput();
            }
            else
            {
                CheckReloadInputInVRMode(); //VRモードかつ、ボタンリロードor上下リロードモードのリロード判定
            }

            CheckSyncedJumpEffect();

            RegenerateStamina();

            ShowPlayerStatus();
        }
        //---------------------------------------
        public void CheckReloadInputInDesktopMode()
        {
            if (reloadType == ReloadType.Abutton || reloadType == ReloadType.Bbutton || reloadType == ReloadType.PointUpward || reloadType == ReloadType.PointDownward || reloadType == ReloadType.PointUpDown)
            {
                //リロード判定
                if (Input.GetKeyDown(ReloadKey)) { TryStartReload(VRC_Pickup.PickupHand.Right); }
            }
        }
        //---------------------------------------
        public void CheckSubModuleUseInputInDesktopMode()
        {
            if (!Utilities.IsValid(Networking.LocalPlayer)) { return; }
            if (!Utilities.IsValid(Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right))) { return; }

            VRC_Pickup PickupInRightHand = Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right);

            //サブモジュールUse判定
            if (Input.GetKeyDown(SecondaryFuncKey))
            {
                if (PickupInRightHand && PickupInRightHand.GetComponent<RangedWeapon_MainModule>() && PickupInRightHand.GetComponent<RangedWeapon_MainModule>().SubModule)
                {
                    PickupInRightHand.GetComponent<RangedWeapon_MainModule>().SubModule.OnPickupUseDown();
                }
            }
            else if (Input.GetKeyUp(SecondaryFuncKey))
            {
                if (PickupInRightHand && PickupInRightHand.GetComponent<RangedWeapon_MainModule>() && PickupInRightHand.GetComponent<RangedWeapon_MainModule>().SubModule)
                {
                    PickupInRightHand.GetComponent<RangedWeapon_MainModule>().SubModule.OnPickupUseUp();
                }
            }
        }
        //---------------------------------------
        public void CheckReloadInputInVRMode()
        {
            if (reloadType == ReloadType.Abutton)
            {
                if (Input.GetButton("Jump") && Time.time - lastLeftButtonReloadInputTime > ButtonReloadInputInterval)
                {
                    TryStartReload(VRC_Pickup.PickupHand.Left);
                    lastLeftButtonReloadInputTime = Time.time;
                }
                if (Input.GetButton("Fire2") && Time.time - lastRightButtonReloadInputTime > ButtonReloadInputInterval)
                {
                    TryStartReload(VRC_Pickup.PickupHand.Right);
                    lastRightButtonReloadInputTime = Time.time;
                }
                return;
            }

            else if (reloadType == ReloadType.Bbutton)
            {
                if (Input.GetButton("Oculus_CrossPlatform_Button4") && Time.time - lastLeftButtonReloadInputTime > ButtonReloadInputInterval)
                {
                    TryStartReload(VRC_Pickup.PickupHand.Left);
                    lastLeftButtonReloadInputTime = Time.time;
                }
                if (Input.GetButton("Oculus_CrossPlatform_Button2") && Time.time - lastRightButtonReloadInputTime > ButtonReloadInputInterval)
                {
                    TryStartReload(VRC_Pickup.PickupHand.Right);
                    lastRightButtonReloadInputTime = Time.time;
                }
                return;
            }

            if (reloadType == ReloadType.PointUpward || reloadType == ReloadType.PointUpDown)
            {
                if (!Utilities.IsValid(Networking.LocalPlayer)) { return; }

                if (Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left) && Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left).GetComponent<RangedWeapon_MainModule>())
                {
                    if (CheckRangedWeaponIsPointingUpward(Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left).GetComponent<RangedWeapon_MainModule>())) { TryStartReload(VRC_Pickup.PickupHand.Left); }
                }
                if (Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right) && Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right).GetComponent<RangedWeapon_MainModule>())
                {
                    if (CheckRangedWeaponIsPointingUpward(Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right).GetComponent<RangedWeapon_MainModule>())) { TryStartReload(VRC_Pickup.PickupHand.Right); }
                }
            }

            if (reloadType == ReloadType.PointDownward || reloadType == ReloadType.PointUpDown)
            {
                if (!Utilities.IsValid(Networking.LocalPlayer)) { return; }

                if (Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left) && Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left).GetComponent<RangedWeapon_MainModule>())
                {
                    if (CheckRangedWeaponIsPointingDownward(Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left).GetComponent<RangedWeapon_MainModule>())) { TryStartReload(VRC_Pickup.PickupHand.Left); }
                }
                if (Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right) && Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right).GetComponent<RangedWeapon_MainModule>())
                {
                    if (CheckRangedWeaponIsPointingDownward(Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right).GetComponent<RangedWeapon_MainModule>())) { TryStartReload(VRC_Pickup.PickupHand.Right); }
                }
            }
        }
        //---------------------------------------
        public bool CheckRangedWeaponIsPointingUpward(RangedWeapon_MainModule mm)
        {
            if (Vector3.Angle(mm.GetModelTransform().forward, Vector3.up) < PointReloadAngle) { return true; }
            else return false;
        }
        //---------------------------------------
        public bool CheckRangedWeaponIsPointingDownward(RangedWeapon_MainModule mm)
        {
            if (Vector3.Angle(mm.GetModelTransform().forward, Vector3.down) < PointReloadAngle) { return true; }
            else return false;
        }
        //---------------------------------------
        public void CheckSyncedJumpEffect()
        {
            if (!Utilities.IsValid(Networking.LocalPlayer)) { return; }

            if (Networking.LocalPlayer.IsPlayerGrounded() && lastGrounded == false)
            {
                //着地エフェクトを出す
                if (Time.time - lastJumpEffectTime > jumpEffectTimeInterval)
                {
                    if (GlobalJumpAndGroundedEffect)
                    {
                        if (playerHitBox) { playerHitBox.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_PlayGroundedEffect"); }
                    }
                    else
                    {
                        if (playerHitBox) { playerHitBox.PlayGroundedEffect(); }
                    }

                    lastJumpEffectTime = Time.time;
                }
                lastGrounded = true;
            }
            else if (!Networking.LocalPlayer.IsPlayerGrounded())
            {
                lastGrounded = false;
            }
        }
        //---------------------------------------
        public void RegenerateStamina()
        {
            if (!RegenStamina) { return; }

            float DeltaTime = Time.fixedDeltaTime;
            if (stamina < MaxStamina)
            {
                stamina += DeltaTime * StaminaRegenRate;
                if (stamina > MaxStamina) { stamina = MaxStamina; }
            }
        }
        //---------------------------------------
        public void ShowPlayerStatus()
        {
            float HitPointRatio;
            float ShieldRatio;
            float StaminaRatio;

            //プレイヤー未登録
            if (!playerHitBox)
            {
                HitPointRatio = 1.0f;
                ShieldRatio = 0;
                StaminaRatio = 1.0f;
                if (HitPointText) { HitPointText.text = "-"; }
                if (ShieldText) { ShieldText.text = "-"; }
            }

            //プレイヤー登録済み
            else
            {
                if (playerHitBox.MaxPlayerHitPoint <= 0) { HitPointRatio = 0; }
                else { HitPointRatio = playerHitBox.SyncedHitPoint / playerHitBox.MaxPlayerHitPoint; }

                if (playerHitBox.MaxPlayerShield <= 0) { ShieldRatio = 0; }
                else { ShieldRatio = playerHitBox.SyncedShield / playerHitBox.MaxPlayerShield; }

                if (HitPointText) { HitPointText.text = playerHitBox.SyncedHitPoint.ToString("F1"); }
                if (ShieldText) { ShieldText.text = playerHitBox.SyncedShield.ToString("F1"); }
            }

            if (MaxStamina <= 0) { StaminaRatio = 0; }
            else { StaminaRatio = stamina / MaxStamina; }
            if (StaminaText) { StaminaText.text = stamina.ToString("F1"); }

            if (HitPointBarTransform)
            {
                HitPointBarTransform.localPosition = new Vector3(-0.5f, 0, 0) * (1 - HitPointRatio);
                HitPointBarTransform.localScale = new Vector3(HitPointRatio, 1, 1);
            }
            if (ShieldBarTransform)
            {
                ShieldBarTransform.localPosition = new Vector3(-0.5f, 0, 0) * (1 - ShieldRatio);
                ShieldBarTransform.localScale = new Vector3(ShieldRatio, 1, 1);
            }
            if (StaminaBarTransform)
            {
                StaminaBarTransform.localPosition = new Vector3(-0.5f, 0, 0) * (1 - StaminaRatio);
                StaminaBarTransform.localScale = new Vector3(StaminaRatio, 1, 1);
            }
        }
        //---------------------------------------
        public void Killed()
        {
            //武器を強制的に落とす
            DisableAllWeapons();

            //フラッグ制の場合は持っているフラッグを落とす
            if (myFlag) { myFlag.DropFlag(); }

            //復活チケットを減らす(チケット制以外の場合は参照しない)
            if (playerHitBox)
            {
                if (gameManager.gameEndCondition == GameEndCondition.Ticket) { --playerHitBox.SyncedReviveTicket; }
                playerHitBox.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_PlayPlayerKillEffect");
            }

            isAlive = false;
            if (Utilities.IsValid(Networking.LocalPlayer))
            {
                Networking.LocalPlayer.Immobilize(true);
                Networking.LocalPlayer.SetJumpImpulse(0);
            }
            SendCustomEventDelayedSeconds("DelayedSecond_Revive", 2.0f);
        }
        //---------------------------------------
        public void EnableAllWeapons()
        {
            RangedWeapon_MainModule[] mainModules = gameManager.rangedWeapon_MainModules;
            for (int i = 0; i < mainModules.Length; ++i) { mainModules[i].EnableWeapon(); }

            MeleeWeapon[] meleeWeapons = gameManager.meleeWeapons;
            for (int i = 0; i < meleeWeapons.Length; ++i) { meleeWeapons[i].EnableWeapon(); }

            ExWeapon[] exWeapons = gameManager.exWeapons;
            for (int i = 0; i < exWeapons.Length; ++i) { exWeapons[i].EnableWeapon(); }
        }
        //---------------------------------------
        public void DisableAllWeapons()
        {
            RangedWeapon_MainModule[] mainModules = gameManager.rangedWeapon_MainModules;
            for (int i = 0; i < mainModules.Length; ++i) { mainModules[i].DisableWeapon(); }

            MeleeWeapon[] meleeWeapons = gameManager.meleeWeapons;
            for (int i = 0; i < meleeWeapons.Length; ++i) { meleeWeapons[i].DisableWeapon(); }

            ExWeapon[] exWeapons = gameManager.exWeapons;
            for (int i = 0; i < exWeapons.Length; ++i) { exWeapons[i].DisableWeapon(); }
        }
        //--------------------------------------- 
        public void DelayedSecond_Revive()
        {
            ResetDamageAreasOnRevive();

            isAlive = true;

            //移動不可を解除
            if (Utilities.IsValid(Networking.LocalPlayer))
            {
                Networking.LocalPlayer.Immobilize(false);
                Networking.LocalPlayer.SetRunSpeed(defaultRunSpeed);
                Networking.LocalPlayer.SetWalkSpeed(defaultWalkSpeed);
                Networking.LocalPlayer.SetStrafeSpeed(defaultStrafeSpeed);
                Networking.LocalPlayer.SetJumpImpulse(defaultJumpImpulse);
            }

            ResetAmmo();

            //武器を拾えるようにする
            EnableAllWeapons();
            if (inventoryType == InventoryType.SingleNoReset || inventoryType == InventoryType.DoubleNoReset) { DisableNoResetInventoryPickupOnOtherAssigners(); }

            //HPとシールドを回復して同期
            if (playerHitBox)
            {
                TrySetOwner(playerHitBox.gameObject);
                playerHitBox.FullHitPoint();
                playerHitBox.ResetShield();
                playerHitBox.Sync();
            }

            //ローカルプレイヤーをリスポーン
            if (playerHitBox && gameManager.SyncedInBattle)
            {
                if (gameManager.gameEndCondition == GameEndCondition.Ticket && playerHitBox.SyncedReviveTicket <= 0)
                {
                    //復活チケットがなくなっている場合は、フィールド外にリスポーンする
                    isAlive = false;
                    gameManager.TeleportAndSetRespawnPoint(gameManager.LoserRespawnTransform);
                    EnableAllWeapons();
                    playerHitBox.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_HideHitBoxModelObject");
                }

                else if (gameManager.teamMode == TeamMode.TeamBattle)
                {
                    if (playerHitBox.teamName == TeamName.A && gameManager.TeamA_RespawnTransform.Length > 0)
                    {
                        int r = Random.Range(0, gameManager.TeamA_RespawnTransform.Length);
                        gameManager.TeleportAndSetRespawnPoint(gameManager.TeamA_RespawnTransform[r]);
                    }
                    else if (playerHitBox.teamName == TeamName.B && gameManager.TeamB_RespawnTransform.Length > 0)
                    {
                        int r = Random.Range(0, gameManager.TeamB_RespawnTransform.Length);
                        gameManager.TeleportAndSetRespawnPoint(gameManager.TeamB_RespawnTransform[r]);
                    }
                    else if (playerHitBox.teamName == TeamName.C && gameManager.TeamC_RespawnTransform.Length > 0)
                    {
                        int r = Random.Range(0, gameManager.TeamC_RespawnTransform.Length);
                        gameManager.TeleportAndSetRespawnPoint(gameManager.TeamC_RespawnTransform[r]);
                    }
                    else if (playerHitBox.teamName == TeamName.D && gameManager.TeamD_RespawnTransform.Length > 0)
                    {
                        int r = Random.Range(0, gameManager.TeamD_RespawnTransform.Length);
                        gameManager.TeleportAndSetRespawnPoint(gameManager.TeamD_RespawnTransform[r]);
                    }
                }
                else if (gameManager.teamMode == TeamMode.FreeForAll)
                {
                    if (gameManager.FFA_RespawnTransform.Length > 0)
                    {
                        int r = Random.Range(0, gameManager.FFA_RespawnTransform.Length);
                        gameManager.TeleportAndSetRespawnPoint(gameManager.FFA_RespawnTransform[r]);
                    }
                }
            }
        }
        //---------------------------------------
        public void ResetDamageAreasOnRevive()
        {
            for (int i = 0; i < damageAreas.Length; ++i) { damageAreas[i].inTrigger = false; }
        }
        //---------------------------------------
        public void DisableNoResetInventoryPickupOnOtherAssigners()
        {
            for (int i = 0; i < Assigners.Length; ++i)
            {
                if (!playerHitBox || playerHitBox.assigner != Assigners[i]) { Assigners[i].DisableNoResetInventoryPickup(); }
            }
        }
        //---------------------------------------
        public void TryStartReload(VRC_Pickup.PickupHand hand)
        {
            if (!Utilities.IsValid(Networking.LocalPlayer)) { return; }

            //手に持っているPickupオブジェクトに下記のコンポーネントがあり、かつリロード中でなければ、リロードを開始する
            //RangedWeapon_MainModuleコンポーネント
            //RangedWeapon_SubModuleコンポーネント

            RangedWeapon_MainModule mm;
            RangedWeapon_SubModule sm;

            //デスクトップモード
            if (!VRmode)
            {
                if (!Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right)) { return; }

                mm = Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right).GetComponent<RangedWeapon_MainModule>();
                if (mm)
                {
                    if (!mm.GetReloading()) { mm.StartReload(); }
                    sm = mm.SubModule;
                    if (sm && !sm.GetReloading()) { sm.StartReload(); }
                    return;
                }

                sm = Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right).GetComponent<RangedWeapon_SubModule>();
                if (sm && !sm.GetReloading()) { sm.StartReload(); }
                return;
            }

            //VRモード,左手リロード
            if (hand == VRC_Pickup.PickupHand.Left)
            {
                if (!Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left)) { return; }

                mm = Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left).GetComponent<RangedWeapon_MainModule>();
                if (mm && !mm.GetReloading()) { mm.StartReload(); return; }

                sm = Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left).GetComponent<RangedWeapon_SubModule>();
                if (sm && !sm.GetReloading()) { sm.StartReload(); return; }

                return;
            }

            //VRモード,右手リロード
            if (hand == VRC_Pickup.PickupHand.Right)
            {
                if (!Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right)) { return; }

                mm = Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right).GetComponent<RangedWeapon_MainModule>();
                if (mm && !mm.GetReloading()) { mm.StartReload(); return; }

                sm = Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right).GetComponent<RangedWeapon_SubModule>();
                if (sm && !sm.GetReloading()) { sm.StartReload(); return; }

                return;
            }
        }
        //---------------------------------------
        public override void InputJump(bool value, UdonInputEventArgs args)
        {
            if (!Utilities.IsValid(Networking.LocalPlayer)) { return; }

            if (EnableDoubleJump && !Networking.LocalPlayer.IsPlayerGrounded() && !DoubleJumped && Time.time - lastSingleJumpTime > DoubleJumpDelay)
            {
                //ダブルジャンプ
                DoubleJumped = true;
                Vector3 currentSpeed = Networking.LocalPlayer.GetVelocity();
                Vector3 Impulse = Vector3.up * DoubleJumpImpulse;
                Networking.LocalPlayer.SetVelocity(currentSpeed + Impulse);

                //ジャンプエフェクトを出す
                if (Time.time - lastJumpEffectTime > jumpEffectTimeInterval)
                {
                    if (GlobalJumpAndGroundedEffect)
                    {
                        if (playerHitBox) { playerHitBox.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_PlayJumpEffect"); }
                    }
                    else
                    {
                        if (playerHitBox) { playerHitBox.PlayJumpEffect(); }
                    }

                    lastJumpEffectTime = Time.time;
                }
            }
            else if (Networking.LocalPlayer.IsPlayerGrounded())
            {
                //通常ジャンプ
                lastSingleJumpTime = Time.time;
                DoubleJumped = false;

                //ジャンプエフェクトを出す
                if (Time.time - lastJumpEffectTime > jumpEffectTimeInterval)
                {
                    if (GlobalJumpAndGroundedEffect)
                    {
                        if (playerHitBox) { playerHitBox.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_PlayJumpEffect"); }
                    }
                    else
                    {
                        if (playerHitBox) { playerHitBox.PlayJumpEffect(); }
                    }

                    lastJumpEffectTime = Time.time;
                }
            }
        }
        //---------------------------------------
        public override void InputGrab(bool value, UdonInputEventArgs args)
        {
            //VRモードなら、インベントリからPickUpオブジェクトを引き出す判定を行う

            if (inventoryType == InventoryType.None || !value || !VRmode) { return; }   //Grab解除した、またはインベントリ禁止設定になっている場合はなにもしない
            if (!Utilities.IsValid(Networking.LocalPlayer)) { return; }     //偶然プレイヤー情報を取れないフレームを踏んだ場合は回避

            Vector3 HandPos;
            VRC_Pickup.PickupHand grabHand;
            if (args.handType == HandType.LEFT)
            {
                HandPos = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.LeftHand).position;
                grabHand = VRC_Pickup.PickupHand.Left;
            }
            else
            {
                HandPos = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.RightHand).position;
                grabHand = VRC_Pickup.PickupHand.Right;
            }
            Vector3 HeadPos = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
            Quaternion HeadRot = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;

            //Grabした手の位置が、頭の左後方・右後方にあるかチェックする
            //HandVector.zがマイナスなら後方、HandVector.xがマイナスなら左
            Vector3 HandVector = Quaternion.Inverse(HeadRot) * (HandPos - HeadPos);

            //Retrieveインベントリの場合
            if (inventoryType == InventoryType.Retrieve || inventoryType == InventoryType.ForcedRetrieve)
            {
                //何も持っていない状態で、頭の後ろでGrabする
                if (HandVector.z < 0 && !Networking.LocalPlayer.GetPickupInHand(grabHand))
                {
                    RetrieveLastPickup();
                }
                return;
            }

            //シングルインベントリの場合
            if (inventoryType == InventoryType.Single)
            {
                //何も持っていない状態で、頭の後ろでGrabする
                if (HandVector.z < 0 && !Networking.LocalPlayer.GetPickupInHand(grabHand))
                {
                    RetrievePickupInLeftInventory();
                }
                return;
            }

            //ダブルインベントリの場合
            if (inventoryType == InventoryType.Double)
            {
                //何も持っていない状態で、頭の左後方でGrabする
                if (HandVector.z < 0 && HandVector.x < 0 && !Networking.LocalPlayer.GetPickupInHand(grabHand))
                {
                    RetrievePickupInLeftInventory();
                }
                //何も持っていない状態で、頭の右後方でGrabする
                else if (HandVector.z < 0 && HandVector.x > 0 && !Networking.LocalPlayer.GetPickupInHand(grabHand))
                {
                    RetrievePickupInRightInventory();
                }
                return;
            }

            //SingleNoResetインベントリの場合
            if (inventoryType == InventoryType.SingleNoReset)
            {
                //何も持っていない状態で、頭の後ろでGrabする
                if (HandVector.z < 0 && !Networking.LocalPlayer.GetPickupInHand(grabHand))
                {
                    RetrieveSpecificSingleInventoryPickup();
                }
                return;
            }

            //DoubleNoResetインベントリの場合
            if (inventoryType == InventoryType.DoubleNoReset)
            {
                //何も持っていない状態で、頭の左後方でGrabする
                if (HandVector.z < 0 && HandVector.x < 0 && !Networking.LocalPlayer.GetPickupInHand(grabHand))
                {
                    RetrieveSpecificSingleInventoryPickup();
                }
                //何も持っていない状態で、頭の右後方でGrabする
                else if (HandVector.z < 0 && HandVector.x > 0 && !Networking.LocalPlayer.GetPickupInHand(grabHand))
                {
                    RetrieveSpecificDoubleInventoryPickup();
                }
                return;
            }
        }
        //---------------------------------------
        public void RetrieveSpecificSingleInventoryPickup()
        {
            if (!playerHitBox) { return; }
            if (!playerHitBox.assigner.SingleInventoryPickup) { return; }

            //インベントリ内のオブジェクトを呼び出す
            VRC_Pickup p = playerHitBox.assigner.SingleInventoryPickup;
            TrySetOwner(p.gameObject);
            Vector3 HeadPos = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
            Quaternion HeadRot = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;
            Vector3 Offset = new Vector3(0, 0, 0.5f);
            p.transform.position = HeadPos + HeadRot * Offset;
            p.transform.rotation = HeadRot;

            if (p.GetComponent<RangedWeapon_MainModule>() && p.GetComponent<RangedWeapon_MainModule>().SubModule)
            {
                p.GetComponent<RangedWeapon_MainModule>().SubModule.ResetSubModuleTransform();
            }
        }
        //---------------------------------------
        public void RetrieveSpecificDoubleInventoryPickup()
        {
            if (!playerHitBox) { return; }
            if (!playerHitBox.assigner.DoubleInventoryPickup) { return; }

            //インベントリ内のオブジェクトを呼び出す
            VRC_Pickup p = playerHitBox.assigner.DoubleInventoryPickup;
            TrySetOwner(p.gameObject);
            Vector3 HeadPos = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
            Quaternion HeadRot = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;
            Vector3 Offset = new Vector3(0, 0, 0.5f);
            p.transform.position = HeadPos + HeadRot * Offset;
            p.transform.rotation = HeadRot;

            if (p.GetComponent<RangedWeapon_MainModule>() && p.GetComponent<RangedWeapon_MainModule>().SubModule)
            {
                p.GetComponent<RangedWeapon_MainModule>().SubModule.ResetSubModuleTransform();
            }
        }
        //---------------------------------------
        public void CheckDesktopInventoryUseInput()
        {
            //Retrieveインベントリの場合
            if (inventoryType == InventoryType.Retrieve || inventoryType == InventoryType.ForcedRetrieve)
            {
                //右手に何も持っていない状態で、LeftInventoryKeyのキーを押す
                if (Input.GetKeyDown(LeftInventoryKey) && Utilities.IsValid(Networking.LocalPlayer) && !Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right))
                {
                    RetrieveLastPickup();
                }
                return;
            }

            //シングルインベントリの場合
            if (inventoryType == InventoryType.Single)
            {
                //右手にPickupを持った状態で、LeftInventoryKeyのキーを押す
                if (Input.GetKeyDown(LeftInventoryKey) && Utilities.IsValid(Networking.LocalPlayer) && Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right))
                {
                    StorePickupInLeftInventory(Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right));
                }
                //右手に何も持っていない状態で、LeftInventoryKeyのキーを押す
                else if (Input.GetKeyDown(LeftInventoryKey) && Utilities.IsValid(Networking.LocalPlayer) && !Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right))
                {
                    RetrievePickupInLeftInventory();
                }
                return;
            }

            //ダブルインベントリの場合
            if (inventoryType == InventoryType.Double)
            {
                //右手にPickupを持った状態で、LeftInventoryKeyのキーを押す
                if (Input.GetKeyDown(LeftInventoryKey) && Utilities.IsValid(Networking.LocalPlayer) && Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right))
                {
                    StorePickupInLeftInventory(Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right));
                }
                //右手に何も持っていない状態で、LeftInventoryKeyのキーを押す
                else if (Input.GetKeyDown(LeftInventoryKey) && Utilities.IsValid(Networking.LocalPlayer) && !Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right))
                {
                    RetrievePickupInLeftInventory();
                }
                //右手にPickupを持った状態で、RightInventoryKeyのキーを押す
                else if (Input.GetKeyDown(RightInventoryKey) && Utilities.IsValid(Networking.LocalPlayer) && Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right))
                {
                    StorePickupInRightInventory(Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right));
                }
                //右手に何も持っていない状態で、RightInventoryKeyのキーを押す
                else if (Input.GetKeyDown(RightInventoryKey) && Utilities.IsValid(Networking.LocalPlayer) && !Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right))
                {
                    RetrievePickupInRightInventory();
                }
                return;
            }

            //SingleNoResetインベントリの場合
            if (inventoryType == InventoryType.SingleNoReset)
            {
                //右手に何も持っていない状態で、LeftInventoryKeyのキーを押す
                if (Input.GetKeyDown(LeftInventoryKey) && Utilities.IsValid(Networking.LocalPlayer) && !Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right))
                {
                    RetrieveSpecificSingleInventoryPickup();
                }
                return;
            }

            //DoubleNoResetインベントリの場合
            if (inventoryType == InventoryType.DoubleNoReset)
            {
                //右手に何も持っていない状態で、LeftInventoryKeyのキーを押す
                if (Input.GetKeyDown(LeftInventoryKey) && Utilities.IsValid(Networking.LocalPlayer) && !Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right))
                {
                    RetrieveSpecificSingleInventoryPickup();
                }
                //右手に何も持っていない状態で、RightInventoryKeyのキーを押す
                else if (Input.GetKeyDown(RightInventoryKey) && Utilities.IsValid(Networking.LocalPlayer) && !Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right))
                {
                    RetrieveSpecificDoubleInventoryPickup();
                }
                return;
            }
        }
        //---------------------------------------
        public void StorePickupInLeftInventory(VRC_Pickup pickupObj)
        {
            //まず持っているPickupを落とし、既にインベントリに物が入っている場合は、それを持つ
            //最後に、さっきまで持っていたPickupをインベントリに入れる
            pickupObj.Drop();
            if (LeftInventoryPickup) { RetrievePickupInLeftInventory(); }
            pickupObj.transform.position = HideInventoryPickupPos;
            LeftInventoryPickup = pickupObj;

            if (LeftInventoryPickup.GetComponent<RangedWeapon_MainModule>() && LeftInventoryPickup.GetComponent<RangedWeapon_MainModule>().SubModule)
            {
                LeftInventoryPickup.GetComponent<RangedWeapon_MainModule>().SubModule.ResetSubModuleTransform();
            }
        }
        //---------------------------------------
        public void RetrievePickupInLeftInventory()
        {
            //インベントリ内のオブジェクトを呼び出す
            if (LeftInventoryPickup)
            {
                TrySetOwner(LeftInventoryPickup.gameObject);
                Vector3 HeadPos = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
                Quaternion HeadRot = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;
                Vector3 Offset = new Vector3(0, 0, 0.5f);
                LeftInventoryPickup.transform.position = HeadPos + HeadRot * Offset;
                LeftInventoryPickup.transform.rotation = HeadRot;

                if (LeftInventoryPickup.GetComponent<RangedWeapon_MainModule>() && LeftInventoryPickup.GetComponent<RangedWeapon_MainModule>().SubModule)
                {
                    LeftInventoryPickup.GetComponent<RangedWeapon_MainModule>().SubModule.ResetSubModuleTransform();
                }

                LeftInventoryPickup = null;
            }
        }
        //---------------------------------------
        public void StorePickupInRightInventory(VRC_Pickup pickupObj)
        {
            //まず持っているPickupを落とし、既にインベントリに物が入っている場合は、それを持つ
            //最後に、さっきまで持っていたPickupをインベントリに入れる
            pickupObj.Drop();
            if (RightInventoryPickup) { RetrievePickupInRightInventory(); }
            pickupObj.transform.position = HideInventoryPickupPos;
            RightInventoryPickup = pickupObj;

            if (RightInventoryPickup.GetComponent<RangedWeapon_MainModule>() && RightInventoryPickup.GetComponent<RangedWeapon_MainModule>().SubModule)
            {
                RightInventoryPickup.GetComponent<RangedWeapon_MainModule>().SubModule.ResetSubModuleTransform();
            }
        }
        //---------------------------------------
        public void RetrievePickupInRightInventory()
        {
            //インベントリ内のオブジェクトを呼び出す
            if (RightInventoryPickup)
            {
                TrySetOwner(RightInventoryPickup.gameObject);
                Vector3 HeadPos = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
                Quaternion HeadRot = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;
                Vector3 Offset = new Vector3(0, 0, 0.5f);
                RightInventoryPickup.transform.position = HeadPos + HeadRot * Offset;
                RightInventoryPickup.transform.rotation = HeadRot;

                if (RightInventoryPickup.GetComponent<RangedWeapon_MainModule>() && RightInventoryPickup.GetComponent<RangedWeapon_MainModule>().SubModule)
                {
                    RightInventoryPickup.GetComponent<RangedWeapon_MainModule>().SubModule.ResetSubModuleTransform();
                }

                RightInventoryPickup = null;
            }
        }
        //---------------------------------------
        public void RetrieveLastPickup()
        {
            //最後に持っていたPickupオブジェクトを呼び出す

            if (!LastDroppedPickup) { return; }

            //他のプレイヤーがOwnership取得済みであれば呼び出しを中止する
            if (inventoryType != InventoryType.ForcedRetrieve && !CheckLocalPlayerIsOwner(LastDroppedPickup.gameObject))
            {
                LastDroppedPickup = null;
                return;
            }

            Vector3 HeadPos = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
            Quaternion HeadRot = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;
            Vector3 Offset = new Vector3(0, 0, 0.5f);
            LastDroppedPickup.transform.position = HeadPos + HeadRot * Offset;
            LastDroppedPickup.transform.rotation = HeadRot;

            if (LastDroppedPickup.GetComponent<RangedWeapon_MainModule>() && LastDroppedPickup.GetComponent<RangedWeapon_MainModule>().SubModule)
            {
                LastDroppedPickup.GetComponent<RangedWeapon_MainModule>().SubModule.ResetSubModuleTransform();
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
        public void ResetLastPickup()
        {
            //最後に持っていたPickupオブジェクトのデータを消去する
            LastDroppedPickup = null;
        }
        //---------------------------------------
        public void ResetInventory()
        {
            LeftInventoryPickup = null;
            RightInventoryPickup = null;
        }
        //---------------------------------------
        public void GetAmmo(int AmmoIndex, int addValue)
        {
            if (reloadType == ReloadType.AutoReload)
            {
                if (!Utilities.IsValid(Networking.LocalPlayer)) { return; }
                VRC_Pickup LeftPickup = Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left);
                VRC_Pickup RightPickup = Networking.LocalPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right);
                if (VRmode)
                {
                    if (LeftPickup && LeftPickup.GetComponent<RangedWeapon_MainModule>()) { addValue = LeftPickup.GetComponent<RangedWeapon_MainModule>().TryReloadOnTakeDropItem(AmmoIndex, addValue); }
                    if (RightPickup && RightPickup.GetComponent<RangedWeapon_MainModule>()) { addValue = RightPickup.GetComponent<RangedWeapon_MainModule>().TryReloadOnTakeDropItem(AmmoIndex, addValue); }
                    if (LeftPickup && LeftPickup.GetComponent<RangedWeapon_SubModule>()) { addValue = LeftPickup.GetComponent<RangedWeapon_SubModule>().TryReloadOnTakeDropItem(AmmoIndex, addValue); }
                    if (RightPickup && RightPickup.GetComponent<RangedWeapon_SubModule>()) { addValue = RightPickup.GetComponent<RangedWeapon_SubModule>().TryReloadOnTakeDropItem(AmmoIndex, addValue); }
                }
                else
                {
                    if (RightPickup && RightPickup.GetComponent<RangedWeapon_MainModule>()) { addValue = RightPickup.GetComponent<RangedWeapon_MainModule>().TryReloadOnTakeDropItem(AmmoIndex, addValue); }
                    if (RightPickup && RightPickup.GetComponent<RangedWeapon_MainModule>() && RightPickup.GetComponent<RangedWeapon_MainModule>().SubModule) { addValue = RightPickup.GetComponent<RangedWeapon_MainModule>().SubModule.TryReloadOnTakeDropItem(AmmoIndex, addValue); }
                }

                Ammo[AmmoIndex] += addValue;
                if (Ammo[AmmoIndex] > MaxAmmo[AmmoIndex]) { Ammo[AmmoIndex] = MaxAmmo[AmmoIndex]; }
                ShowAmmo();
                return;
            }

            Ammo[AmmoIndex] += addValue;
            if (Ammo[AmmoIndex] > MaxAmmo[AmmoIndex]) { Ammo[AmmoIndex] = MaxAmmo[AmmoIndex]; }
            ShowAmmo();
        }
        //---------------------------------------
        public void ResetAmmo()
        {
            for (int i = 0; i < MaxAmmo.Length; ++i) { Ammo[i] = DefaultAmmo[i]; }
            ShowAmmo();
        }
        //--------------------------------------- 
        public void ShowAmmo()
        {
            for (int i = 0; i < Ammo.Length; ++i) { if (AmmoText[i]) { AmmoText[i].text = Ammo[i].ToString(); } }
        }
        //--------------------------------------- 
        public void PlayDamageEffect()
        {
            if (OnDamageEffectObject)
            {
                float DamageEffectDuration = 0.1f;
                OnDamageEffectObject.SetActive(true);
                SendCustomEventDelayedSeconds("DelayedSecond_DisableOnDamageEffect", DamageEffectDuration);
            }

            if (DamageAudioClip) { audioSource.PlayOneShot(DamageAudioClip, 1.0f); }
        }
        //---------------------------------------
        public void PlayHealEffect()
        {
            if (HealAudioClip) { audioSource.PlayOneShot(HealAudioClip, 1.0f); }
        }
        //---------------------------------------
        public void DelayedSecond_DisableOnDamageEffect()
        {
            if (OnDamageEffectObject) { OnDamageEffectObject.SetActive(false); }
        }
        //---------------------------------------
        public void Initialize()
        {
            if (Utilities.IsValid(Networking.LocalPlayer) && Networking.LocalPlayer.IsUserInVR()) { VRmode = true; }
            else { VRmode = false; }

            if (OnDamageEffectObject) { OnDamageEffectObject.SetActive(false); }

            audioSource = this.GetComponent<AudioSource>();
            stamina = MaxStamina;
            isAlive = true;
            Assigners = gameManager.Assigners;
            Ammo = new int[DefaultAmmo.Length];
            for (int i = 0; i < DefaultAmmo.Length; ++i) { Ammo[i] = DefaultAmmo[i]; }

            oldTime = Time.time;

            SendCustomEventDelayedSeconds("DelayedSecond_Initialized", 1.0f);
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
        public override void OnPlayerRespawn(VRCPlayerApi player)
        {
            if (Utilities.IsValid(Networking.LocalPlayer) && Networking.LocalPlayer == player && playerHitBox && gameManager.SyncedInBattle) { playerHitBox.TakeDamage(playerHitBox.MaxPlayerHitPoint, true); }
        }
        //---------------------------------------
        public void SetAbuttonReloadMode()
        {
            reloadType = ReloadType.Abutton;
        }
        //---------------------------------------
        public void SetBbuttonReloadMode()
        {
            reloadType = ReloadType.Bbutton;
        }
        //---------------------------------------
        public bool AutoBuild(DamageArea[] _damageAreas, ConquestZone[] _conquestZones)
        {
            damageAreas = _damageAreas;
            conquestZones = _conquestZones;

            if (DefaultAmmo.Length != MaxAmmo.Length)
            {
                MaxAmmo = new int[DefaultAmmo.Length];
            }

            if (DefaultAmmo.Length != AmmoText.Length)
            {
                AmmoText = new Text[DefaultAmmo.Length];
            }

            return true;
        }
        //---------------------------------------
    }
}



