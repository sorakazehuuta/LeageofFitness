
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

//両手にブースター持っている場合の解除処理修正

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
    public class ExWeapon : Weapon
    {
        //---------------------------------------
        [Tooltip("Triggerしたときの機能")] [SerializeField] SubModuleFunctionType subWeaponFunctionType;
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
        //---------------------------------------
        [Header("任意項目(その他)")]
        [Tooltip("ピックアップ時の効果音")] [SerializeField] AudioClip PickupSound;
        [Tooltip("チェックを入れるとドロップ後に自動でリセット")] [SerializeField] bool AutoResetAfterDrop = true;
        [Tooltip("ドロップしてから自動でリセットするまでの時間")] public float AutoResetDuration = 30.0f;
        //---------------------------------------
        [Header("----VRコントローラ設定----")]
        [Tooltip("trueなら射撃時などにコントローラを振動させる")] [SerializeField] bool EnableHaptics;
        [Tooltip("コントローラの振動時間")] [SerializeField] float HapticsDuration = 0.3f;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("VRC_Pickup")] public VRC_Pickup Pickup;
        [HideInInspector] [Tooltip("AudioSource")] [SerializeField] AudioSource audioSource;
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        //---------------------------------------
        private VRCPlayerApi localPlayer;
        bool VRmode;
        bool boosting;
        float lastDroppedTime;  //最後に武器をDropした時刻
        //---------------------------------------
        private void Start()
        {
            localPlayer = Networking.LocalPlayer;

            if (subWeaponFunctionType == SubModuleFunctionType.Shield && Utilities.IsValid(Networking.LocalPlayer))
            {
                CloseShield();
            }

            if (Utilities.IsValid(Networking.LocalPlayer) && Networking.LocalPlayer.IsUserInVR()) { VRmode = true; }
            else{ VRmode = false; }
        }
        //---------------------------------------
        private void FixedUpdate()
        {
            //このオブジェクトを持ってトリガー中のみ、ブースト発動
            if (Utilities.IsValid(localPlayer) && boosting)
            {
                float DeltaTime = Time.fixedDeltaTime;

                float Thrust;
                if (Pickup.currentHand == VRC_Pickup.PickupHand.Left) { Thrust = Mathf.Max(Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryIndexTrigger"), Input.GetKey(KeyCode.Space) ? 1 : 0); }
                else { Thrust = Mathf.Max(Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryIndexTrigger"), Input.GetKey(KeyCode.Space) ? 1 : 0); }

                playerManager.stamina -= DeltaTime * StaminaConsumptionRate;
                if (playerManager.stamina <= 0)
                {
                    playerManager.stamina = 0;
                    StopBoosting();
                }

                Vector3 PlayerVel = localPlayer.GetVelocity();

                Vector3 NewForwardVec;
                NewForwardVec = Thrust * this.transform.forward;

                float BackThrustAmount = -((Vector3.Dot(PlayerVel, NewForwardVec)) * _backthruststrength * DeltaTime);
                NewForwardVec = NewForwardVec * _thruststrength * Thrust * DeltaTime * Mathf.Max(1, (BackThrustAmount * Thrust));

                localPlayer.SetVelocity(PlayerVel + NewForwardVec);
            }
        }
        //---------------------------------------
        public override void OnPickup()
        {
            if (!CheckDualWielding()) { Pickup.Drop(); return; }
            
            if (PickupSound) { audioSource.PlayOneShot(PickupSound, 1.0f); }
            
            PlayHaptics();
        }
        //---------------------------------------
        public override void OnDrop()
        {
            //自動リセット用のフラグ更新
            if (CheckAutoResetPickup())
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_UpdateLastDroppedTime");
            }

            if (subWeaponFunctionType == SubModuleFunctionType.Shield && Utilities.IsValid(Networking.LocalPlayer))
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_CloseShield");
                return;
            }

            if (subWeaponFunctionType == SubModuleFunctionType.Booster && Utilities.IsValid(Networking.LocalPlayer))
            {
                StopBoosting();
                return;
            }

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
            }
        }
        //---------------------------------------
        public override void OnPickupUseDown()
        {
            if (subWeaponFunctionType == SubModuleFunctionType.DashPlayerForward && Utilities.IsValid(Networking.LocalPlayer))
            {
                Dash(Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation * Vector3.forward);    //プレイヤーの視線方向にダッシュ
                return;
            }

            if (subWeaponFunctionType == SubModuleFunctionType.DashModelForward && Utilities.IsValid(Networking.LocalPlayer))
            {
                Dash(this.transform.forward);    //このオブジェクトのZ方向にダッシュ
                return;
            }

            if (subWeaponFunctionType == SubModuleFunctionType.Shield && Utilities.IsValid(Networking.LocalPlayer))
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_OpenShield"); //シールド展開
                return;
            }

            if (subWeaponFunctionType == SubModuleFunctionType.Booster && Utilities.IsValid(Networking.LocalPlayer))
            {
                TryStartBoosting(); //ブースト開始
                return;
            }
        }
        //---------------------------------------
        public override void OnPickupUseUp()
        {
            if (subWeaponFunctionType == SubModuleFunctionType.Shield && Utilities.IsValid(Networking.LocalPlayer))
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_CloseShield");
                return;
            }

            if (subWeaponFunctionType == SubModuleFunctionType.Booster && Utilities.IsValid(Networking.LocalPlayer))
            {
                StopBoosting();
                return;
            }
        }
        //---------------------------------------
        public void Dash(Vector3 direction)
        {
            Vector3 currentSpeed = Networking.LocalPlayer.GetVelocity();
            Vector3 Impulse = direction * DashImpulse;
            Networking.LocalPlayer.SetVelocity(currentSpeed + Impulse);
        }
        //---------------------------------------
        public void TryStartBoosting()
        {
            if (playerManager.stamina >= 0)
            {
                boosting = true;
                playerManager.RegenStamina = false;
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_PlayBoostEffect");
            }
        }
        //---------------------------------------
        public void StopBoosting()
        {
            boosting = false;
            if (!IsBoostingInOtherHand()) { playerManager.RegenStamina = true; }
            
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Network_StopBoostEffect");
        }
        //---------------------------------------
        public bool IsBoostingInOtherHand()
        {
            if (!Utilities.IsValid(Networking.LocalPlayer)) { return false; }

            VRC_Pickup.PickupHand otherHand;
            if (Pickup.currentHand == VRC_Pickup.PickupHand.Left) { otherHand = VRC_Pickup.PickupHand.Right; }
            else { otherHand = VRC_Pickup.PickupHand.Left; }

            if (!Networking.LocalPlayer.GetPickupInHand(otherHand)) { return false; }

            VRC_Pickup otherPickup = Networking.LocalPlayer.GetPickupInHand(otherHand);

            if (otherPickup.GetComponent<ExWeapon>() && otherPickup.GetComponent<ExWeapon>().boosting) { return true; }

            return false;
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
        public void Network_OpenShield()
        {
            ShieldObject.SetActive(true);
            if (OpenShieldClip) { this.GetComponent<AudioSource>().PlayOneShot(OpenShieldClip, 1.0f); }
        }
        //---------------------------------------
        public void Network_CloseShield()
        {
            CloseShield();
        }
        //---------------------------------------
        public void CloseShield()
        {
            ShieldObject.SetActive(false);
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
        public void ResetTransform()
        {
            if (Pickup.IsHeld) { Pickup.Drop(); }

            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.identity;
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
        public override bool AutoBuild(PlayerManager pm)
        {
            playerManager = pm;
            audioSource = this.GetComponent<AudioSource>();

            if(subWeaponFunctionType == SubModuleFunctionType.Bipod)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " SubWeaponFunctionTypeはBipod以外を選択してください.", this.gameObject);
                return false;
            }

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

