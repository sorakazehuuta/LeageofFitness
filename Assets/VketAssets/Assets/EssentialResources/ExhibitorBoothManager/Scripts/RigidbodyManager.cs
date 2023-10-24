
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;

namespace Vket.UdonManager
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class RigidbodyManager : UdonSharpBehaviour
    {
        public Rigidbody[] rigidbodies;
        public VRCObjectSync[] objectSyncs;

        [SerializeField]
        private BoxCollider boxCollider;

        VRCPlayerApi localPlayer;
        private int playerCount;

        private void Start()
        {
            SetKinematic(true);
            SendCustomEventDelayedSeconds(nameof(_DelayEnabled), 1.0f, VRC.Udon.Common.Enums.EventTiming.Update);
        }

        public void _DelayEnabled()
        {
            boxCollider.enabled = true;
        }

        private void SetKinematic(bool kinematic)
        {
            if (localPlayer == null)
                localPlayer = Networking.LocalPlayer;

            foreach (var rb in rigidbodies)
                rb.isKinematic = kinematic;

            foreach(var objectSync in objectSyncs)
            {
                if (Networking.IsOwner(localPlayer, objectSync.gameObject))
                    objectSync.SetKinematic(kinematic);
            }
        }

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            playerCount++;
            SetKinematic(playerCount == 0);
        }

        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            playerCount--;
            SetKinematic(playerCount == 0);
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            playerCount = 0;

            boxCollider.enabled = false;
            boxCollider.enabled = true;

            SendCustomEventDelayedFrames("_CountCheck", 2, VRC.Udon.Common.Enums.EventTiming.Update);
        }

        public void _CountCheck()
        {
            if (playerCount == 0)
                SetKinematic(true);
        }
    }
}