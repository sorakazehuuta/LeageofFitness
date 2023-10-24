
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;

namespace Vket.VketPrefabs
{
    [CustomEditor(typeof(VketFollowPickup))]
    public class VketFollowPickupEditor : Editor
    {
        private Vector2 scrollPosition = Vector2.zero;
        private bool isFoldoutOpen;
        private VketFollowPickup _vketFollowPickup;

        private void OnEnable()
        {
            _vketFollowPickup = target as VketFollowPickup;
        }
        
        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(108));
            EditorGUILayout.TextArea(
                "** ReadMe **\n" +
                "- Useすると体に追従するPickupです\n" +
                "　位置同期し、誰も持っていない状態で30秒経過すると位置がリセットされます\n" +
                "- \"Target Bone\"で指定したBoneに追従します\n" +
                "- Useした時、\"Attach Range\"の範囲内にBoneがあれば追従を開始します\n" +
                "- オブジェクトの子に見た目に設定したいオブジェクトを配置して、\n" +
                "　CapsuleColliderを適度な大きさに調整してください\n" +
                "- This Pickup will follow the player when Used.\n" +
                "  It's location is synced and reset when left for 30 seconds without being held.\n" +
                "- When Used, the object will start following a Bone within the range of \"Attach Range\".\n" +
                "- Set the object for appearance as a child of \"ModeController\" object\n" +
                "　and adjust the size of CapsuleCollider as you see fit.");
            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space();

            serializedObject.Update();

            // Draw Field of Attach Range
            EditorGUILayout.PropertyField(serializedObject.FindProperty("attachRange"));

            // Draw Field of Target Bone
            SerializedProperty targetBoneProperty = serializedObject.FindProperty("targetBone");
            EditorGUI.BeginChangeCheck();
            int bone = EditorGUILayout.Popup("Target Bone", targetBoneProperty.intValue,
                new string[] { "Head", "Neck", "Chest", "Spine", "Hips", "Shoulder", "UpperArm", "LowerArm", "Hand", "UpperLeg", "LowerLeg", "Foot" });
            if (EditorGUI.EndChangeCheck())
            {
                targetBoneProperty.intValue = bone;
            }
            /*
            EditorGUILayout.Space();
            
            // References
            isFoldoutOpen = EditorGUILayout.Foldout(isFoldoutOpen, "References(Do not change)");
            if (isFoldoutOpen)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("pickup"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("udonManager"));
                EditorGUI.indentLevel--;
            }
            */
            serializedObject.ApplyModifiedProperties();

            EditorGUI.BeginDisabledGroup(_vketFollowPickup.gameObject.scene.name == null);
            if (GUILayout.Button("Open Setting Window"))
            {
                VketPrefabSettingWindow.OpenSettingWindow<VketFollowPickupSettingWindow>(_vketFollowPickup.transform);
            }
            EditorGUI.EndDisabledGroup();
            //base.OnInspectorGUI();
        }
    }
}
