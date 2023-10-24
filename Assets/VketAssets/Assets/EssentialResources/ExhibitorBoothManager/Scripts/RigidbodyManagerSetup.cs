using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRC.SDK3.Components;
#if UNITY_EDITOR
using UnityEditor;
using UdonSharpEditor;
#endif

namespace Vket.UdonManager
{
#if UNITY_EDITOR
    [IgnoreBuild]
    public class RigidbodyManagerSetup : MonoBehaviour, IPreSaveExecute
    {
        [SerializeField]
        private Transform boothRoot;

        private RigidbodyManager rigidbodyManager;

        public void PreSaveExecute()
        {
            if (rigidbodyManager == null)
            {
#if !UDONSHARP
                rigidbodyManager = gameObject.GetUdonSharpComponent<RigidbodyManager>();
#else
                rigidbodyManager = gameObject.GetComponent<RigidbodyManager>();
#endif
                if (rigidbodyManager == null)
                {
                    Debug.LogError("RigidbodyManager is not found.");
                    return;
                }
            }
#if !UDONSHARP
            rigidbodyManager.UpdateProxy();
#endif

            // Find Booth Root Transform
            if (boothRoot == null)
            {
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
                    Debug.LogError("RigidbodyManager: BoothRoot is not found.");
                    return;
                }
            }

            // Modify Rigidbodies and ObjectSyncs
            List<Rigidbody> rigidbodiesList = new List<Rigidbody>();
            List<VRCObjectSync> objectsyncsList = new List<VRCObjectSync>();
            foreach(var rb in boothRoot.GetComponentsInChildren<Rigidbody>())
            {
                if (rb.isKinematic)
                    continue;

                var objectSync = rb.GetComponent<VRCObjectSync>();
                if (objectSync == null)
                    rigidbodiesList.Add(rb);
                else
                    objectsyncsList.Add(objectSync);
            }
            rigidbodyManager.rigidbodies = rigidbodiesList.ToArray();
            rigidbodyManager.objectSyncs = objectsyncsList.ToArray();
#if !UDONSHARP
            rigidbodyManager.ApplyProxyModifications();
#endif

#if !UDONSHARP
            EditorUtility.SetDirty(UdonSharpEditorUtility.GetBackingUdonBehaviour(rigidbodyManager));
#else
            EditorUtility.SetDirty(rigidbodyManager);
#endif
        }
    }
#endif
        }