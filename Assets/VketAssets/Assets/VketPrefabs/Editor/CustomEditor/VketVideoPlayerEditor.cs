
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Video.Components.Base;

namespace Vket.VketPrefabs
{
    [CustomEditor(typeof(VketVideoPlayer))]
    public class VketVideoPlayerEditor : Editor
    {
        private Vector2 scrollPosition = Vector2.zero;
        private VketVideoPlayer vketVideoPlayer;
        private BaseVRCVideoPlayer videoPlayer;

        private void OnEnable()
        {
            if (vketVideoPlayer == null)
                vketVideoPlayer = (VketVideoPlayer)target;

            videoPlayer = (BaseVRCVideoPlayer)serializedObject.FindProperty("videoPlayer").objectReferenceValue;
        }

        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            // Draw Title and Summary
            var style = new GUIStyle();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("  Vket Video Player", new GUIStyle(EditorStyles.boldLabel));
            EditorGUILayout.EndVertical();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(152));
            EditorGUILayout.TextArea(
                "** ReadMe **\n" +
                "- \"Video Url\"に指定した動画を再生できます\n" +
                "- 操作パネルが不要の場合は\"VketVideoPlayer/Interface\"を非Activeにしてください\n" +
                "  \"VketVideoPlayer/Screen\"に動画が再生されます。アスペクト比に応じてScaleを調整してください\n" +
                "  \"VketVideoPlayer/AudioSource\"で動画の音声の大きさや範囲を調整できます\n" +
                "- \"World Bgm Fade\"を有効にすると動画再生時にワールドBGMがフェードアウトします\n" +
                "- \"OnBoothPlay\"を有効にするとプレイヤーがブースに接近した時に自動的に動画が再生されます\n" +
                "- 動画が再生されていない時は\"Disabled Image\"に指定した画像が表示されます\n" +
                "　動画のロード中は\"Loading Image\"に指定した画像が表示されます\n" +
                "- It plays the video specified in \"Video Url\".\n" +
                "- If the control panel is not necessary, deactivate \"VketVideoPlayer/Interface\".\n" +
                "  The video will be displayed on \"VketVideoPlayer/Screen\". Adjust the Scale according to the aspect ratio.\n" +
                "  The volume and range of video audio can be adjusted with \"VketVideoPlayer/AudioSource\".\n" +
                "- If \"World Bgm Fade\" is active the world BGM will fade out upon video play.\n" +
                "- If \"OnBoothPlay\" is active the video will automatically start playing when a player approaches the booth.\n" +
                "- When the video is not played, the image set on \"Disabled Image\" will be displayed.\n" +
                "　While the video is loaded, the image on \"Loading Image\" will be displayed.");
            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space();

            serializedObject.Update();

            // Draw Field of Video Url
            EditorGUILayout.PropertyField(serializedObject.FindProperty("videoUrl"));

            // Draw Field of Loop
            if (videoPlayer != null)
            {
                var so = new SerializedObject(videoPlayer);
                so.Update();
                var loopProp = so.FindProperty("loop");
                EditorGUI.BeginChangeCheck();
                bool loop = EditorGUILayout.Toggle("Loop", loopProp.boolValue);
                if (EditorGUI.EndChangeCheck())
                {
                    loopProp.boolValue = loop;
                    so.ApplyModifiedProperties();
                }
            }

            EditorGUILayout.Space();

            // Draw Field of World Bgm Fade
            EditorGUILayout.PropertyField(serializedObject.FindProperty("worldBgmFade"));

            // Draw Field of On Booth Play
            var onBoothPlay = EditorGUILayout.PropertyField(serializedObject.FindProperty("onBoothPlay"));

            // Draw Fiedl of Disabled Image
            var disabledImageProperty = serializedObject.FindProperty("disabledImage");
            EditorGUI.BeginChangeCheck();
            disabledImageProperty.objectReferenceValue = (Texture2D)EditorGUILayout.ObjectField("Disabled Image", disabledImageProperty.objectReferenceValue, typeof(Texture2D), false);

            // Draw Fiedl of Loading Image
            var loadingImageProperty = serializedObject.FindProperty("loadingImage");
            EditorGUI.BeginChangeCheck();
            loadingImageProperty.objectReferenceValue = (Texture2D)EditorGUILayout.ObjectField("Loading Image", loadingImageProperty.objectReferenceValue, typeof(Texture2D), false);

            serializedObject.ApplyModifiedProperties();
            
            EditorGUI.BeginDisabledGroup(vketVideoPlayer.gameObject.scene.name == null);
            if (GUILayout.Button("Open Setting Window"))
            {
                VketPrefabSettingWindow.OpenSettingWindow<VketVideoPlayerSettingWindow>(vketVideoPlayer.transform);
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
