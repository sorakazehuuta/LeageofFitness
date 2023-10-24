
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Vket.UdonManager
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class ExhibitorBoothActivator : UdonSharpBehaviour
    {
        [SerializeField]
        private ExhibitorBoothManager boothManager;
        [SerializeField]
        private CapsuleCollider playerTriggerCollider;

        private void OnTriggerEnter(Collider other)
        {
            if (other != playerTriggerCollider)
                return;

            boothManager._ActivateBooth();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other != playerTriggerCollider)
                return;

            boothManager._DeactivateBooth();
        }
    }
}