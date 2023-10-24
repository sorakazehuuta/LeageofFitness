
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class HUD : UdonSharpBehaviour
    {
        //---------------------------------------
        [Tooltip("プレイヤーへの追従速度(0～1)")] [SerializeField, Range(0, 1)] float lerpValue = 0.15f;
        [Tooltip("デスクトップモード時のローカル座標")] [SerializeField] Vector3 DeskTopModePosition;
        [Tooltip("デスクトップモード時のローカルスケール")] [SerializeField] Vector3 DeskTopModeScale;
        [Tooltip("HUDのオブジェクト")] [SerializeField] Transform HUDObjectTransform;
        //---------------------------------------
        bool VRmode;
        //---------------------------------------
        private void Start()
        {
            if (Utilities.IsValid(Networking.LocalPlayer) && Networking.LocalPlayer.IsUserInVR())
            {
                VRmode = true;
            }
            else
            {
                VRmode = false;
            }

            if (!VRmode)
            {
                HUDObjectTransform.localPosition = DeskTopModePosition;
                HUDObjectTransform.localScale = DeskTopModeScale;
            }

            HUDObjectTransform.gameObject.SetActive(false);
        }
        //---------------------------------------
        private void Update()
        {
            if (Utilities.IsValid(Networking.LocalPlayer))
            {
                if(VRmode)
                {
                    this.transform.position = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation, lerpValue);
                }
                else
                {
                    this.transform.position = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
                    this.transform.rotation = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;
                }
            }
        }
        //---------------------------------------
        public void EnableHUD()
        {
            HUDObjectTransform.gameObject.SetActive(true);
        }
        //---------------------------------------
        public void DisableHUD()
        {
            HUDObjectTransform.gameObject.SetActive(false);
        }
        //---------------------------------------
    }
}