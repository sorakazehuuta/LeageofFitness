
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
    public class ArmDevice : UdonSharpBehaviour
    {
        //---------------------------------------
        [Header("必須項目.無いとエラー")]
        [Tooltip("プレイヤーに追従するオブジェクト")] [SerializeField] Transform ArmDeviceObjectTransform;
        [Tooltip("座標計算用の空オブジェクト")] [SerializeField] Transform DummyObjectTransform;
        //---------------------------------------
        [Header("その他の設定")]
        [Tooltip("falseなら追従させない")] public bool enable;
        //---------------------------------------
        VRCPlayerApi.TrackingDataType FollowHand;
        Vector3 HandPosOffset;
        Quaternion HandRotOffset;
        Quaternion ResetHandRot;
        Quaternion InverseResetHandRot;
        //---------------------------------------
        void Start()
        {
            DummyObjectTransform.localPosition = Vector3.zero;
            DummyObjectTransform.localRotation = Quaternion.identity;
        }
        //---------------------------------------
        private void LateUpdate()
        {
            if (enable && ArmDeviceObjectTransform && DummyObjectTransform && Utilities.IsValid(Networking.LocalPlayer))
            {
                ArmDeviceObjectTransform.position = Networking.LocalPlayer.GetTrackingData(FollowHand).position + Networking.LocalPlayer.GetTrackingData(FollowHand).rotation * HandPosOffset;
                ArmDeviceObjectTransform.rotation = Networking.LocalPlayer.GetTrackingData(FollowHand).rotation * InverseResetHandRot * DummyObjectTransform.rotation;
            }
        }
        //---------------------------------------
        public override void Interact()
        {
            if (this.GetComponent<AudioSource>()) { this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip); }
            ResetOffset();
        }
        //---------------------------------------
        public void ResetOffset()
        {
            float distanceL = (Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.LeftHand).position - this.transform.position).magnitude;
            float distanceR = (Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.RightHand).position - this.transform.position).magnitude;

            if (!enable)
            {
                enable = true;
                ArmDeviceObjectTransform.gameObject.SetActive(true);
            }
            else if(distanceL < distanceR && FollowHand == VRCPlayerApi.TrackingDataType.RightHand)
            {
                enable = false;
                ArmDeviceObjectTransform.gameObject.SetActive(false);
                return;
            }
            else if (distanceL > distanceR && FollowHand == VRCPlayerApi.TrackingDataType.LeftHand)
            {
                enable = false;
                ArmDeviceObjectTransform.gameObject.SetActive(false);
                return;
            }

            if(distanceL < distanceR)
            {
                FollowHand = VRCPlayerApi.TrackingDataType.LeftHand;
            }
            else
            {
                FollowHand = VRCPlayerApi.TrackingDataType.RightHand;
            }

            ResetHandRot = Networking.LocalPlayer.GetTrackingData(FollowHand).rotation;
            InverseResetHandRot = Quaternion.Inverse(ResetHandRot);

            HandRotOffset = Quaternion.RotateTowards(ResetHandRot, DummyObjectTransform.rotation, 180);
            HandPosOffset = InverseResetHandRot * (DummyObjectTransform.position - Networking.LocalPlayer.GetTrackingData(FollowHand).position);
        }
        //---------------------------------------
    }
}


