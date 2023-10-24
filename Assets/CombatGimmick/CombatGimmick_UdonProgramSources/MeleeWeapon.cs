
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public enum SkillType
{
    OnUse, Charge, None
}

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
    public class MeleeWeapon : Weapon
    {
        //---------------------------------------
        [Header("必須項目.無いとエラー")]
        [Tooltip("攻撃力")] public float Damage;
        [Tooltip("攻撃スピード(秒)")] public float AttackInterval;
        //---------------------------------------
        [Header("スキル設定")]
        [Tooltip("スキルの攻撃力")] public float SkillDamage;
        [Tooltip("必要スタミナ")] public float CostStamina;
        [Tooltip("スキルの発動条件")] public SkillType skillType = SkillType.None;
        [Tooltip("スキルのチャージ時間")] public float skillChargeTime;
        [Tooltip("スキルの有効時間")] public float skillActiveDuration;
        [Tooltip("使用するスキル弾薬の種類(-1なら無効)")] public int AmmoType = -1;
        //---------------------------------------
        [Header("投擲時の設定")]
        [Tooltip("オンにすると投擲攻撃できる")] public bool Throwable;
        [Tooltip("投げてからダメージが発生する時間")] public float ThrowAttackDuration = 1.5f;
        [Tooltip("スキルの発動条件")] public float ThrowAttackDamage;
        //---------------------------------------
        [Header("任意項目.無しでも動作可能")]
        [Tooltip("trueならスキルチャージのエフェクトを同期する")] public bool GlobalSkillChargeEffect = true;
        [Tooltip("スキルのチャージ開始時のエフェクト")] [SerializeField] ParticleSystem SkillChargeStartEffect;
        [Tooltip("スキルの発動時の効果音")] [SerializeField] AudioClip SkillChargeStartClip;
        [Tooltip("スキルのチャージ完了時のエフェクト")] [SerializeField] ParticleSystem SkillChargeEndEffect;
        [Tooltip("スキルの発動時の効果音")] [SerializeField] AudioClip SkillChargeEndClip;
        [Tooltip("スキルの発動時のエフェクト")] [SerializeField] ParticleSystem SkillActivateEffect;
        [Tooltip("スキルの発動時の効果音")] [SerializeField] AudioClip SkillActivateClip;
        [Tooltip("チェックを入れるとドロップ後に自動でリセット")] [SerializeField] bool AutoResetAfterDrop = true;
        [Tooltip("ドロップしてから自動でリセットするまでの時間")] public float AutoResetDuration = 30.0f;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        [HideInInspector] [Tooltip("VRC_Pickup")] public VRC_Pickup Pickup;   //通常のヒットボックスであればこのオブジェクトのVRC_Pickup, スキル用のヒットボックスであればスキル発動用VRC_Pickupが設定される
        //---------------------------------------
        Collider collider;  //この武器のヒットボックス.
        float LastHitTime;  //最後にヒットした時間
        float LastSkillActivateTime;  //最後にスキル発動した時間
        int Magazine;   //現在のスキル残弾
        bool Charged;
        bool Charging;
        float StartChargeTime;
        float DropWeaponTime;
        AudioSource audioSource;
        bool VRmode;
        float lastDroppedTime;  //最後に武器をDropした時刻
        //---------------------------------------
        void Start()
        {
            audioSource = this.GetComponent<AudioSource>();
            collider = this.GetComponent<Collider>();

            if (Utilities.IsValid(Networking.LocalPlayer) && Networking.LocalPlayer.IsUserInVR()) { VRmode = true; }
            else { VRmode = false; }
        }
        //---------------------------------------
        public override void OnPickup()
        {
            if (!CheckDualWielding()) { Pickup.Drop(); return; }
            if (Throwable){ collider.isTrigger = true; }

            Charged = false;
            Charging = false;
        }
        //---------------------------------------
        public bool CheckDualWielding()
        {
            if (playerManager.dualWieldingType == DualWieldingType.Enable) { return true; }
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
            if (Throwable) { ThrowWeapon(); }

            StartChargeTime = Time.time;
            Charged = false;
            Charging = false;

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
                for (int i = 0; i < playerManager.gameManager.Assigners.Length; ++i) { if (playerManager.gameManager.Assigners[i].SingleInventoryPickup == Pickup || playerManager.gameManager.Assigners[i].DoubleInventoryPickup == Pickup) { return false; } }
                return true;
            }
            if (playerManager.inventoryType == InventoryType.None) { return true; }

            if (playerManager.inventoryType == InventoryType.Single || playerManager.inventoryType == InventoryType.Double) { return false; }

            if (playerManager.inventoryType == InventoryType.Retrieve || playerManager.inventoryType == InventoryType.ForcedRetrieve) { return false; }

            else return true;
        }
        //---------------------------------------
        public void ThrowWeapon()
        {
            DropWeaponTime = Time.time;
            collider.isTrigger = false;
        }
        //---------------------------------------
        public override void OnPickupUseDown()
        {
            //OnUseモードであれば即時発動
            if (skillType == SkillType.OnUse)
            {
                if (Time.time - LastSkillActivateTime <= skillChargeTime) { return; }
                if (playerManager.stamina < CostStamina) { return; }
                if (!SubtractAmmo()) { return; }

                ActivateSkill();
                playerManager.stamina -= CostStamina;
                return;
            }

            //Chargeモードであればチャージ開始
            if (skillType == SkillType.Charge)
            {
                StartChargeSkill();
                return;
            }
        }
        //---------------------------------------
        public override void OnPickupUseUp()
        {
            if (ChargeSkillReady())
            {
                ActivateSkill();
                playerManager.stamina -= CostStamina;
            }

            Charged = false;
            Charging = false;
        }
        //---------------------------------------
        public bool ChargeSkillReady()
        {
            if (!Charged) { return false; }
            if (!Charging) { return false; }
            if (skillType != SkillType.Charge) { return false; }
            if (playerManager.stamina < CostStamina) { return false; }
            if (!SubtractAmmo()) { return false; }

            return true;
        }
        //---------------------------------------
        public bool SubtractAmmo()
        {
            if (AmmoType < 0)
            {
                return true;
            }

            if (playerManager.Ammo[AmmoType] <= 0) { return false; }
            
            --playerManager.Ammo[AmmoType];
            playerManager.ShowAmmo();
            return true;
        }
        //---------------------------------------
        public void StartChargeSkill()
        {
            StartChargeTime = Time.time;
            Charging = true;

            if (GlobalSkillChargeEffect) { SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_PlayStartChargeSkillEffect"); }
            else { PlayStartChargeSkillEffect(); }

            if (SkillChargeStartEffect) { SkillChargeStartEffect.Play(); }
            if (SkillChargeStartClip) { audioSource.PlayOneShot(SkillChargeStartClip, 1.0f); }

            SendCustomEventDelayedSeconds("DelayedSecond_CheckEndChargeSkill", skillChargeTime);
        }
        //---------------------------------------
        public void DelayedSecond_CheckEndChargeSkill()
        {
            if(Utilities.IsValid(Networking.LocalPlayer) && Pickup.currentPlayer == Networking.LocalPlayer && Charging)
            {
                Charged = true;
                if (GlobalSkillChargeEffect) {SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_PlayEndChargeSkillEffect"); }
                else {PlayEndChargeSkillEffect(); }
            }
        }
        //---------------------------------------
        public bool IsPickedupByMe()
        {
            if (!Utilities.IsValid(Networking.LocalPlayer)){ return false; }
            if (Pickup.currentPlayer != Networking.LocalPlayer) { return false; }
            return true;
        }
        //---------------------------------------
        public void OnTriggerEnter(Collider other)
        {
            //ゲームから脱落していればダメージを与えない
            if (playerManager.playerHitBox && playerManager.playerHitBox.SyncedReviveTicket <= 0) { return; }

            //ダウン状態・または戦闘中でない場合、ダメージを与えない
            if (!playerManager.isAlive || !gameManager.SyncedInBattle || !gameManager.SyncedInGame) { return; }
            
            //最後のヒットから一定時間経過していない場合、ダメージを与えない
            if (Time.time - LastHitTime < AttackInterval) { return; }
            
            //自分が持っていない武器はダメージを与えない
            if (!IsPickedupByMe()) { return; }
                        
            //自分以外のヒットボックスに命中した場合にダメージ処理を実行
            if (other.GetComponent<HitBox>() && other.GetComponent<HitBox>() != playerManager.playerHitBox)
            {
                float dmg;
                if(Time.time - LastSkillActivateTime < skillActiveDuration) { dmg = SkillDamage; }
                else { dmg = Damage; }
                
                if (other.GetComponent<BotHitBox>())
                {
                    other.GetComponent<BotHitBox>().TakeDamage(dmg, false);
                    LastHitTime = Time.time;
                }
                else if (other.GetComponent<PlayerHitBox>())
                {
                    other.GetComponent<PlayerHitBox>().TakeDamage(dmg, false);
                    LastHitTime = Time.time;
                }                
            }
        }
        //---------------------------------------
        public void OnCollisionEnter(Collision collision)
        {
            if (!Utilities.IsValid(collision)) { return; }

            if (!Throwable) { return; }
            if (IsPickedupByMe()) { return; }
            if (DropWeaponTime - Time.time > ThrowAttackDuration) { return; }
            if (Time.time - LastHitTime < AttackInterval) { return; }

            //自分以外のヒットボックスに命中した場合にダメージ処理を実行
            if (collision.gameObject.GetComponent<HitBox>() && collision.gameObject.GetComponent<HitBox>() != playerManager.playerHitBox)
            {
                float dmg = ThrowAttackDamage;

                if (collision.gameObject.GetComponent<BotHitBox>())
                {
                    collision.gameObject.GetComponent<BotHitBox>().TakeDamage(dmg, false);
                    LastHitTime = Time.time;
                }
                else if (collision.gameObject.GetComponent<PlayerHitBox>())
                {
                    collision.gameObject.GetComponent<PlayerHitBox>().TakeDamage(dmg, false);
                    LastHitTime = Time.time;
                }
            }
        }
        //---------------------------------------
        public void ActivateSkill()
        {
            Charged = false;
            LastSkillActivateTime = Time.time;
            LastHitTime = 0;

            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_PlayActivateSkillEffect");
        }
        //---------------------------------------
        public void EnableHitBox()
        {
            collider.enabled = true;
            SendCustomEventDelayedSeconds("DelayedSecond_DisableHitBox", skillActiveDuration);
        }
        //---------------------------------------
        public void DelayedSecond_DisableHitBox()
        {
            DisableHitBox();
        }
        //---------------------------------------
        public void DisableHitBox()
        {
            collider.enabled = false;
        }
        //---------------------------------------
        public void Network_PlayStartChargeSkillEffect()
        {
            PlayStartChargeSkillEffect();
        }
        //---------------------------------------
        public void PlayStartChargeSkillEffect()
        {
            if (SkillChargeStartEffect) { SkillChargeStartEffect.Play(); }
            if (SkillChargeStartClip) { audioSource.PlayOneShot(SkillChargeStartClip, 1.0f); }
        }
        //---------------------------------------
        public void Network_PlayEndChargeSkillEffect()
        {
            PlayEndChargeSkillEffect();
        }
        //---------------------------------------
        public void PlayEndChargeSkillEffect()
        {
            if (SkillChargeEndEffect) { SkillChargeEndEffect.Play(); }
            if (SkillChargeEndClip) { audioSource.PlayOneShot(SkillChargeEndClip, 1.0f); }
        }
        //---------------------------------------
        public void Network_PlayActivateSkillEffect()
        {
            if (SkillActivateEffect) { SkillActivateEffect.Play(); }
            if (SkillActivateClip) { audioSource.PlayOneShot(SkillActivateClip, 1.0f); }
        }
        //---------------------------------------
        public void EnableWeapon()
        {
            Pickup.pickupable = true;
        }
        //---------------------------------------
        public void DisableWeapon()
        {
            if (Utilities.IsValid(Networking.LocalPlayer) && Pickup.currentPlayer == Networking.LocalPlayer) { Pickup.Drop(); }
            Pickup.pickupable = false;
        }
        //---------------------------------------
        public void SetPickup(VRC_Pickup _p)
        {
            Pickup = _p;
        }
        //---------------------------------------
        public void ResetTransform()
        {
            //スキルのヒットボックスとして使うオブジェクトにはPickupコンポーネントをつけない
            if (this.GetComponent<VRC_Pickup>())
            {
                if (Pickup.IsHeld) { Pickup.Drop(); }

                this.transform.localPosition = Vector3.zero;
                this.transform.localRotation = Quaternion.identity;
            }
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
        public override bool AutoBuild(PlayerManager pm, GameManager gm)
        {
            playerManager = pm;
            gameManager = gm;

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

