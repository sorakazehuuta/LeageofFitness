
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class MovingPlatformManager : UdonSharpBehaviour
    {
        //---------------------------------------
        //[Header("手動で変更しないこと")]
        [HideInInspector] public Transform AnchorTransform;
        //---------------------------------------
        Vector3 lastAnchorPos;
        //---------------------------------------
        private void Update()
        {
            if (!AnchorTransform) { return; }
            if (!Utilities.IsValid(Networking.LocalPlayer)) { return; }

            Vector3 AnchorPos = AnchorTransform.position;
            Vector3 anchorPosDelta = AnchorPos - lastAnchorPos;
            
            Networking.LocalPlayer.TeleportTo(Networking.LocalPlayer.GetPosition() + anchorPosDelta, Networking.LocalPlayer.GetRotation());

            lastAnchorPos = AnchorPos;
        }
        //---------------------------------------
        public void SetAnchorTransform(Transform t)
        {
            AnchorTransform = t;
            lastAnchorPos = AnchorTransform.position;
        }
        //---------------------------------------
        public void ResetAnchorTransform()
        {
            AnchorTransform = null;
        }
        //---------------------------------------
        public override void OnPlayerRespawn(VRCPlayerApi player)
        {
            if (Utilities.IsValid(Networking.LocalPlayer) || player != Networking.LocalPlayer) { return; }

            ResetAnchorTransform();
        }
        //---------------------------------------
    }
}
