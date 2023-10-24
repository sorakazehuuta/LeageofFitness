
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;

namespace Vket.VketPrefabs
{
    [CustomEditor(typeof(VketVideoUrlTrigger))]
    public class VketVideoUrlTriggerEditor : Editor
    {
        private VketVideoUrlTrigger videoUrlTrigger;
        private Vector2 scrollPosition;

        private void OnEnable()
        {
            if (videoUrlTrigger == null)
                videoUrlTrigger = (VketVideoUrlTrigger)target;
        }

        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            // Draw Title and Summary
            var style = new GUIStyle();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("  Vket Video URL Trigger", new GUIStyle(EditorStyles.boldLabel));
            EditorGUILayout.EndVertical();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(67));
            EditorGUILayout.TextArea(
                "** ReadMe **\n" +
                "- 指定したVketVideoPlayerのURLを変更して再生します\n" +
                "- 起動方法をInteractとOnPlayerEnterから選択できます\n" +
                "- URLが空の場合はそのままVideoPlayerを再生します\n" +
                "- The trigger will change the URL and play the specified VketVideoPlayer.\n" +
                "- You can choose the activation method from Interact or OnPlayerEnter.\n" +
                "- If the URL is not set, the VideoPlayer will play as it is.");
            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space();

            serializedObject.Update();

            bool isInteract = serializedObject.FindProperty("isInteract").boolValue;
            int popupIdx = isInteract ? 1 : 0;
            EditorGUI.BeginChangeCheck();
            popupIdx = EditorGUILayout.Popup("Mode", popupIdx, new string[] { "On Player Enter", "Interact" });
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.FindProperty("isInteract").boolValue = popupIdx == 1 ? true : false;
            }

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("videoUrl"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("vketVideoPlayer"));

            serializedObject.ApplyModifiedProperties();

            EditorGUI.BeginDisabledGroup(videoUrlTrigger.gameObject.scene.name == null);
            if (GUILayout.Button("Open Setting Window"))
            {
                VketPrefabSettingWindow.OpenSettingWindow<VketVideoUrlTriggerSettingWindow>(videoUrlTrigger.transform);
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
