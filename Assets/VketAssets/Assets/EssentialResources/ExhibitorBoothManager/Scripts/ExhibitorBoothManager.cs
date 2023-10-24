
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;
using VRC.SDK3.Video.Components;

namespace Vket.UdonManager
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class ExhibitorBoothManager : UdonSharpBehaviour
    {
        public GameObject UIRootObject;
        public Transform[] userInterfaces;
        [Space]
        public Component[] startComponents;
        public Component[] soundFadeComponents;
        public Component[] languageSwitcherComponents;
        public Component[] isQuestComponents;
        [Space]
        public Component[] udonComponents;
        public int[] callbackMasks;
        public VRCPickup[] pickups;
        public Vector3[] initPositions;
        public Quaternion[] initRotations;
        public AudioSource[] audioSources;
        public Camera[] cameras;
        public GameObject[] projectorObjects;
        public VRC.SDK3.Video.Components.Base.BaseVRCVideoPlayer[] videoPlayers;

        private readonly float executeStartTime = 1.0f;

        private VRCPlayerApi localPlayer;
        private Transform playerTriggerTransform;
        private float defaultJumpImpulse;
        private float defaultWalkSpeed;
        private float defaultRunSpeed;
        private float defaultStrafeSpeed;
        private bool isInBooth;
        private bool isSwitchedEnglish;

        private void Start()
        {
            localPlayer = Networking.LocalPlayer;
            playerTriggerTransform = transform.Find("PlayerTrigger");

#if UNITY_ANDROID
            // Set Variable "Vket_IsQuest"
            foreach (var component in isQuestComponents)
            {
                UdonBehaviour ub = (UdonBehaviour)component;
                ub.SetProgramVariable("Vket_IsQuest", true);
            }
#endif

            SendCustomEventDelayedSeconds(nameof(_DelayExecuteStart), executeStartTime, VRC.Udon.Common.Enums.EventTiming.Update);
        }

        public void _DelayExecuteStart()
        {
            // Send Callback "_VketStart"
            foreach (var component in startComponents)
            {
                UdonBehaviour ub = (UdonBehaviour)component;
                ub.SendCustomEvent("_VketStart");
            }

            if (localPlayer != null && localPlayer.IsValid())
            {
                defaultJumpImpulse = localPlayer.GetJumpImpulse();
                defaultWalkSpeed = localPlayer.GetWalkSpeed();
                defaultRunSpeed = localPlayer.GetRunSpeed();
                defaultStrafeSpeed = localPlayer.GetStrafeSpeed();
            }
        }

        private void Update()
        {
            if (!isInBooth)
                return;

            // Send Callback "_VketUpdate"
            for (int i = 0; i < udonComponents.Length; i++)
            {
                int hasBit = callbackMasks[i] & 1;
                if (hasBit != 0)
                {
                    if (udonComponents[i].gameObject.activeInHierarchy)
                    {
                        UdonBehaviour ub = (UdonBehaviour)udonComponents[i];
                        ub.SendCustomEvent("_VketUpdate");
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (!isInBooth)
                return;

            // Send Callback "_VketFixedUpdate"
            for (int i = 0; i < udonComponents.Length; i++)
            {
                int hasBit = callbackMasks[i] & 2;
                if (hasBit != 0)
                {
                    if (udonComponents[i].gameObject.activeInHierarchy)
                    {
                        UdonBehaviour ub = (UdonBehaviour)udonComponents[i];
                        ub.SendCustomEvent("_VketFixedUpdate");
                    }
                }
            }
        }

        private void LateUpdate()
        {
            if(localPlayer != null && localPlayer.IsValid())
                playerTriggerTransform.position = localPlayer.GetPosition();

            if (!isInBooth)
                return;

            // Send Callback "_VketLateUpdate"
            for (int i = 0; i < udonComponents.Length; i++)
            {
                int hasBit = callbackMasks[i] & 4;
                if (hasBit != 0)
                {
                    if (udonComponents[i].gameObject.activeInHierarchy)
                    {
                        UdonBehaviour ub = (UdonBehaviour)udonComponents[i];
                        ub.SendCustomEvent("_VketLateUpdate");
                    }
                }
            }
        }

        public override void PostLateUpdate()
        {
            if (!isInBooth)
                return;

            // Send Callback "_VketPostLateUpdate"
            for (int i = 0; i < udonComponents.Length; i++)
            {
                int hasBit = callbackMasks[i] & 8;
                if (hasBit != 0)
                {
                    if (udonComponents[i].gameObject.activeInHierarchy)
                    {
                        UdonBehaviour ub = (UdonBehaviour)udonComponents[i];
                        ub.SendCustomEvent("_VketPostLateUpdate");
                    }
                }
            }
        }

        public void _ActivateBooth()
        {
            // Send Callback "_VketOnBoothEnter"
            for (int i = 0; i < udonComponents.Length; i++)
            {
                int hasBit = callbackMasks[i] & 16;
                if (hasBit != 0)
                {
                    UdonBehaviour ub = (UdonBehaviour)udonComponents[i];
                    ub.SendCustomEvent("_VketOnBoothEnter");
                }
            }

            isInBooth = true;
        }

        public void _DeactivateBooth()
        {
            // Send Callback "_VketOnBoothExit"
            for (int i = 0; i < udonComponents.Length; i++)
            {
                int hasBit = callbackMasks[i] & 32;
                if (hasBit != 0)
                {
                    UdonBehaviour ub = (UdonBehaviour)udonComponents[i];
                    ub.SendCustomEvent("_VketOnBoothExit");
                }
            }

            // Exit Processing
            for (int i = 0; i < pickups.Length; i++)
            {
                pickups[i].Drop();
                pickups[i].transform.SetPositionAndRotation(initPositions[i], initRotations[i]);
            }
            foreach (var audioSource in audioSources)
                audioSource.enabled = false;
            foreach (var camera in cameras)
                camera.enabled = false;
            foreach (var projectorObject in projectorObjects)
                projectorObject.SetActive(false);
            foreach (var videoPlayer in videoPlayers)
                if (videoPlayer.gameObject.activeInHierarchy)
                    videoPlayer.Stop();

            if (localPlayer != null && localPlayer.IsValid())
            {
                localPlayer.SetJumpImpulse(defaultJumpImpulse);
                localPlayer.SetWalkSpeed(defaultWalkSpeed);
                localPlayer.SetRunSpeed(defaultRunSpeed);
                localPlayer.SetStrafeSpeed(defaultStrafeSpeed);
            }

            isInBooth = false;
        }

        public void _StopAllSoundFade()
        {
            foreach (var component in soundFadeComponents)
            {
                UdonBehaviour ub = (UdonBehaviour)component;
                ub.SendCustomEvent("_StopSoundFade");
            }
        }

        public void _SwitchLanguage()
        {
            isSwitchedEnglish = !isSwitchedEnglish;

            foreach (var component in languageSwitcherComponents)
            {
                UdonBehaviour ub = (UdonBehaviour)component;
                if (isSwitchedEnglish)
                    ub.SendCustomEvent("_SwitchToEn");
                else
                    ub.SendCustomEvent("_SwitchToJp");
            }
        }

        public void _EnableFollowPickupUI()
        {
            userInterfaces[0].gameObject.SetActive(true);
            UIRootObject.SetActive(true);
        }

        public void _DisableFollowPickupUI()
        {
            userInterfaces[0].gameObject.SetActive(false);
            foreach (var ui in userInterfaces)
            {
                if (ui.gameObject.activeSelf)
                    return;
            }
            UIRootObject.SetActive(false);
        }
    }
}