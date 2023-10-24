using System.Collections.Generic;
using System;
using UdonSharp;
using UnityEngine;
using VRC.Udon;
using VRC.SDK3.Components;
using VRC.SDK3.Video.Components;
using VRC.Udon.Common;
using VRC.Udon.Common.Interfaces;
using VRC.Udon.Serialization.OdinSerializer;
#if UNITY_EDITOR
using UnityEditor;
using UdonSharpEditor;
#endif

namespace Vket.UdonManager
{
#if UNITY_EDITOR
    [IgnoreBuild]
    public class ExhibitorBoothManagerSetup : MonoBehaviour, IPreSaveExecute
    {
        [SerializeField]
        private Transform boothRoot;
        [SerializeField]
        private Vector3 boothActivatorAreaSize = new Vector3(6.0f, 6.0f, 8.0f);
        [SerializeField]
        private UdonBehaviour udonChipsBehaviour;

        private ExhibitorBoothManager exhibitorBoothManager;

        public void PreSaveExecute()
        {
            if (exhibitorBoothManager == null)
            {
#if !UDONSHARP
                exhibitorBoothManager = gameObject.GetUdonSharpComponent<ExhibitorBoothManager>();
#else
                exhibitorBoothManager = gameObject.GetComponent<ExhibitorBoothManager>();
#endif
                if (exhibitorBoothManager == null)
                {
                    Debug.LogError("ExhibitorBoothManager is not found.");
                    return;
                }
            }
#if !UDONSHARP
            exhibitorBoothManager.UpdateProxy();
#endif
            // Find Booth Root Transform
            if (boothRoot == null) {
                foreach (var rootObject in gameObject.scene.GetRootGameObjects())
                {
                    if (int.TryParse(rootObject.name, out _))
                    {
                        boothRoot = rootObject.transform;
                        EditorUtility.SetDirty(this);
                    }
                }
                if (boothRoot == null)
                {
                    Debug.LogWarning("BoothRoot is not found.");
                    return;
                }
            }

            // Modify Booth Activator Collider
            var boothActivatorCollider = transform.Find("ExhibitorBoothActivator")?.GetComponent<BoxCollider>();
            if (boothActivatorCollider != null)
            {
                boothActivatorCollider.center = new Vector3(0, boothActivatorAreaSize.y * 0.5f, 0);
                boothActivatorCollider.size = boothActivatorAreaSize;
                EditorUtility.SetDirty(boothActivatorCollider);
            }

            // Modify Sound Fade Components and Language Switcher Components
#if !UDONSHARP
            var udonSharpComponents = boothRoot.GetUdonSharpComponentsInChildren<UdonSharpBehaviour>(true);
#else
            var udonSharpComponents = boothRoot.GetComponentsInChildren<UdonSharpBehaviour>(true);
#endif
            List<Component> soundFadeComponentsList = new List<Component>();
            List<AudioSource> soundFadeAudioSourcesList = new List<AudioSource>();
            List<Component> languageSwitcherComponentsList = new List<Component>();
            foreach (var usb in udonSharpComponents)
            {
                switch(usb.GetUdonTypeName())
                {
                    case "VketSoundFade":
                        soundFadeComponentsList.Add(usb.GetComponent<UdonBehaviour>());
                        var audioSource = usb.GetComponentInChildren<AudioSource>();
                        if (audioSource != null)
                            soundFadeAudioSourcesList.Add(audioSource);
                        break;
                    case "VketLanguageSwitcher":
                        languageSwitcherComponentsList.Add(usb.GetComponent<UdonBehaviour>());
                        break;
                }
            }
            exhibitorBoothManager.soundFadeComponents = soundFadeComponentsList.ToArray();
#if !UDONSHARP
            exhibitorBoothManager.ApplyProxyModifications();
#endif
            exhibitorBoothManager.languageSwitcherComponents = languageSwitcherComponentsList.ToArray();
#if !UDONSHARP
            exhibitorBoothManager.ApplyProxyModifications();
#endif

            // Modify Udon Components
            List<Component> startComponentsList = new List<Component>();
            List<Component> udonComponentsList = new List<Component>();
            List<Component> isQuestComponentsList = new List<Component>();
            List<int> callbackMasksList = new List<int>();
            foreach(var udonBehaviour in boothRoot.GetComponentsInChildren<UdonBehaviour>(true))
            {
                var serializedUdonProgramAsset = new SerializedObject(udonBehaviour)
                    .FindProperty("serializedProgramAsset").objectReferenceValue as AbstractSerializedUdonProgramAsset;
                var program = serializedUdonProgramAsset.RetrieveProgram();

                int callbackMask = GetCallbackMask(program.EntryPoints.GetExportedSymbols(), out bool hasVketStart);

                if (hasVketStart)
                    startComponentsList.Add(udonBehaviour);

                if (callbackMask > 0)
                {
                    udonComponentsList.Add(udonBehaviour);
                    callbackMasksList.Add(callbackMask);
                }

                if (HasIsQuestVariable(program.SymbolTable.GetExportedSymbols()))
                {
                    isQuestComponentsList.Add(udonBehaviour);
                }

                // Attach UdonChips Reference
                if (udonChipsBehaviour != null && HasUdonChipsVariable(program.SymbolTable.GetExportedSymbols()))
                {
#if UDONSHARP
                    var usb = udonBehaviour.GetComponent<UdonSharpBehaviour>();
                    if (usb != null) UdonSharpEditorUtility.CopyProxyToUdon(usb);
#endif
                    // UdonBehaviour Variables
                    if (!udonBehaviour.publicVariables.TrySetVariableValue("VketUdonChips", udonChipsBehaviour))
                    {
                        if (!udonBehaviour.publicVariables.TryAddVariable(CreateUdonVariable("VketUdonChips", udonChipsBehaviour, typeof(UdonBehaviour))))
                            Debug.LogError($"Failed to set public variable value.");
                    }
                    EditorUtility.SetDirty(udonBehaviour);
#if UDONSHARP
                    
                    if (usb != null)
                    {
                        UdonSharpEditorUtility.CopyUdonToProxy(usb);
                        EditorUtility.SetDirty(usb);
                    }
#endif

                    // CyanTrigger Variables
                    Component cyanTriggerComponent = udonBehaviour.GetComponent("CyanTrigger");
                    if (cyanTriggerComponent != null)
                    {

                        SerializedObject so = new SerializedObject(cyanTriggerComponent);
                        so.Update();
                        var variablesProperty = so.FindProperty("triggerInstance").FindPropertyRelative("triggerDataInstance").FindPropertyRelative("variables");
                        for (int i = 0; i < variablesProperty.arraySize; i++)
                        {
                            if (variablesProperty.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue == "VketUdonChips")
                            {
                                var dataProperty = variablesProperty.GetArrayElementAtIndex(i).FindPropertyRelative("data");

                                dataProperty.FindPropertyRelative("objEncoded").stringValue =
                                    EncodeObject(udonChipsBehaviour, out List<UnityEngine.Object> unityObjects);

                                var unityObjectsProperty = dataProperty.FindPropertyRelative("unityObjects");
                                unityObjectsProperty.arraySize = 1;
                                unityObjectsProperty.GetArrayElementAtIndex(0).objectReferenceValue = unityObjects[0];
                            }
                        }
                        so.ApplyModifiedProperties();
                    }
                }
            }
            exhibitorBoothManager.udonComponents = udonComponentsList.ToArray();
            exhibitorBoothManager.callbackMasks = callbackMasksList.ToArray();
            exhibitorBoothManager.startComponents = startComponentsList.ToArray();
            exhibitorBoothManager.isQuestComponents = isQuestComponentsList.ToArray();
#if !UDONSHARP
            exhibitorBoothManager.ApplyProxyModifications();
#endif
            // Modify Pickups
            List<VRCPickup> pickupsList = new List<VRCPickup>();
            List<Vector3> initPositionsList = new List<Vector3>();
            List<Quaternion> initRotationsList = new List<Quaternion>();
            var pickups = boothRoot.GetComponentsInChildren<VRCPickup>(true);
            for(int i=0; i < pickups.Length; i++)
            {
                bool isPickupPrefab = false;
#if !UDONSHARP
                var usb = pickups[i].GetUdonSharpComponent<UdonSharpBehaviour>();
#else
                var usb = pickups[i].GetComponent<UdonSharpBehaviour>();
#endif
                if (usb != null)
                {
                    string udonTypeName = usb.GetUdonTypeName();
                    if (udonTypeName == "VketPickup" || udonTypeName == "VketFollowPickup")
                        isPickupPrefab = true;
                }
                if (!isPickupPrefab)
                {
                    pickupsList.Add(pickups[i]);
                    initPositionsList.Add(pickups[i].transform.position);
                    initRotationsList.Add(pickups[i].transform.rotation);
                }

            }
            exhibitorBoothManager.pickups = pickupsList.ToArray();
            exhibitorBoothManager.initPositions = initPositionsList.ToArray();
            exhibitorBoothManager.initRotations = initRotationsList.ToArray();
#if !UDONSHARP
            exhibitorBoothManager.ApplyProxyModifications();
#endif
            // Modify Audio Sources
            List<AudioSource> audioSourcesList = new List<AudioSource>(boothRoot.GetComponentsInChildren<AudioSource>(true));
            foreach (var soundFadeAudio in soundFadeAudioSourcesList)
                audioSourcesList.Remove(soundFadeAudio);
            exhibitorBoothManager.audioSources = audioSourcesList.ToArray();
#if !UDONSHARP
            exhibitorBoothManager.ApplyProxyModifications();
#endif

            // Modify Cameras
            exhibitorBoothManager.cameras = boothRoot.GetComponentsInChildren<Camera>(true);
#if !UDONSHARP
            exhibitorBoothManager.ApplyProxyModifications();
#endif

            // Modify Projector Objects
            List<GameObject> projectorObjectsList = new List<GameObject>();
            foreach (var projector in boothRoot.GetComponentsInChildren<Projector>(true))
                projectorObjectsList.Add(projector.gameObject);
            exhibitorBoothManager.projectorObjects = projectorObjectsList.ToArray();
#if !UDONSHARP
            exhibitorBoothManager.ApplyProxyModifications();
#endif

            // Modify Video Players
            exhibitorBoothManager.videoPlayers = boothRoot.GetComponentsInChildren<VRC.SDK3.Video.Components.Base.BaseVRCVideoPlayer>(true);
#if !UDONSHARP
            exhibitorBoothManager.ApplyProxyModifications();
#endif

#if !UDONSHARP
            EditorUtility.SetDirty(UdonSharpEditorUtility.GetBackingUdonBehaviour(exhibitorBoothManager));
#else
            EditorUtility.SetDirty(exhibitorBoothManager);
#endif
        }

        private int GetCallbackMask(System.Collections.Immutable.ImmutableArray<string> exportedSymbols, out bool hasVketStart)
        {
            hasVketStart = false;
            int callbackMask = 0;

            foreach (var symbol in exportedSymbols)
            {
                if (!hasVketStart && symbol == "_VketStart")
                    hasVketStart = true;

                if (symbol == "_VketUpdate")
                    callbackMask += 1;
                else if (symbol == "_VketFixedUpdate")
                    callbackMask += 2;
                else if (symbol == "_VketLateUpdate")
                    callbackMask += 4;
                else if (symbol == "_VketPostLateUpdate")
                    callbackMask += 8;
                else if (symbol == "_VketOnBoothEnter")
                    callbackMask += 16;
                else if (symbol == "_VketOnBoothExit")
                    callbackMask += 32;
            }

            return callbackMask;
        }

        private bool HasUdonChipsVariable(System.Collections.Immutable.ImmutableArray<string> exportedSymbols)
        {
            foreach (var symbol in exportedSymbols)
            {
                if (symbol == "VketUdonChips")
                    return true;
            }

            return false;
        }

        private bool HasIsQuestVariable(System.Collections.Immutable.ImmutableArray<string> exportedSymbols)
        {
            foreach (var symbol in exportedSymbols)
            {
                if (symbol == "Vket_IsQuest")
                    return true;
            }

            return false;
        }

        private PreSaveMarker FindPreSaveMarkerComponent(string tag)
        {
            var preSaveComponents = Resources.FindObjectsOfTypeAll<PreSaveMarker>();
            foreach (var component in preSaveComponents)
            {
                if (component.MarkerTag == tag)
                {
                    return component;
                }
            }
            return null;
        }

        private IUdonVariable CreateUdonVariable(string symbolName, object value, Type declaredType)
        {
            Type udonVariableType = typeof(UdonVariable<>).MakeGenericType(declaredType);
            return (IUdonVariable)Activator.CreateInstance(udonVariableType, symbolName, value);
        }

        public string EncodeObject(object obj, out List<UnityEngine.Object> unityObjects)
        {
            byte[] serializedBytes = SerializationUtility.SerializeValue(obj, DataFormat.Binary, out unityObjects);
            return Convert.ToBase64String(serializedBytes);
        }
    }
#endif
        }