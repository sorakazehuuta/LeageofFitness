
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;

namespace Vket.VketPrefabs
{
    [CustomEditor(typeof(VketSoundFade))]
    public class VketSoundFadeEditor : Editor
    {
        private VketSoundFade _vketSoundFade;
        private Vector2 scrollPosition = Vector2.zero;

        private void OnEnable()
        {
            if (!_vketSoundFade)
                _vketSoundFade = target as VketSoundFade;
        }
        
        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            // Draw Title and Summary
            var style = new GUIStyle();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("  Vket Sound Fade", new GUIStyle(EditorStyles.boldLabel));
            EditorGUILayout.EndVertical();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(95));
            EditorGUILayout.TextArea(
                "** ReadMe **\n" +
                "- Interactするたび、ワールドBGMをフェードアウト、インします\n" +
                "　子のAudioSourceに音源を設定しておくとワールドBGMと入れ替わる形で音源をフェードします\n" +
                "　音源のフェードインにかける時間を\"Fade In Time\"で設定できます\n" +
                "- \"On Booth Fading\"を有効にするとプレイヤーがブースに接近した時に自動的にフェードします\n" +
                "- Every time a player Interact with this object. the world BGM will be fade out or in\n" +
                "　When an audio file is set to AudioSource, the child object, the set audio will fade as if replacing the world BGM.\n" +
                "　You can set the length of time for sound fade in with \"Fade In Time\".\n" +
                "- If \"On Booth Fading\" is active, the fade will automatically kick in when a player is near the booth.");
            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();

            serializedObject.Update();

            // Draw Field of On Booth Fading
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fadeInTime"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onBoothFading"));

            serializedObject.ApplyModifiedProperties();

            EditorGUI.BeginDisabledGroup(_vketSoundFade.gameObject.scene.name == null);
            if (GUILayout.Button("Open Setting Window"))
            {
                VketPrefabSettingWindow.OpenSettingWindow<VketSoundFadeSettingWindow>(_vketSoundFade.transform);
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
