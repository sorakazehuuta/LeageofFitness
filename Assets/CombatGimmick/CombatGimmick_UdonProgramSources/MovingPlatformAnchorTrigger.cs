
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class MovingPlatformAnchorTrigger : UdonSharpBehaviour
    {
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("MovingPlatformManager")] [SerializeField] MovingPlatformManager movingPlatformManager;
        //---------------------------------------
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (!Utilities.IsValid(Networking.LocalPlayer) || player != Networking.LocalPlayer) { return; }

            movingPlatformManager.SetAnchorTransform(this.transform);
        }
        //---------------------------------------
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (!Utilities.IsValid(Networking.LocalPlayer) || player != Networking.LocalPlayer) { return; }

            movingPlatformManager.ResetAnchorTransform();
        }
        //---------------------------------------
        public bool AutoBuild(MovingPlatformManager _movingPlatformManager)
        {
            movingPlatformManager = _movingPlatformManager;

            return true;
        }
        //---------------------------------------
    }
}


