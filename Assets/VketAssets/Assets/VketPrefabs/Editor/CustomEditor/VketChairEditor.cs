
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;


namespace Vket.VketPrefabs
{
    [CustomEditor(typeof(VketChair))]
    public class VketChairEditor : Editor
    {
        private Vector2 scrollPosition = Vector2.zero;
        private VketChair _vketChair;

        private void OnEnable()
        {
            _vketChair = target as VketChair;
        }

        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            // Draw Title and Summary
            var style = new GUIStyle();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("  Vket Chair", new GUIStyle(EditorStyles.boldLabel));
            EditorGUILayout.EndVertical();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(94));
            EditorGUILayout.TextArea(
                "** ReadMe **\n" +
                "- 普通の椅子です\n" +
                "- \"Visual\"オブジェクトの子に見た目に使用したいオブジェクトを追加してください\n" +
                "　追加したオブジェクトのコライダーは削除してください\n" +
                "- その後、\"EnterLocation\"オブジェクトを動かして座る位置を調整してください\n" +
                "- A normal chair.\n" +
                "- Add the object you want to use for the look and feel to the \"Visual\" object's children.\n" +
                "　Delete the collider of the added object.\n" +
                "- Then move the \"EnterLocation\" object to adjust the sitting position.");
            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space();

            EditorGUI.BeginDisabledGroup(_vketChair.gameObject.scene.name == null);
            if (GUILayout.Button("Open Setting Window"))
            {
                VketPrefabSettingWindow.OpenSettingWindow<VketChairSettingWindow>(_vketChair.transform);
            }

            EditorGUI.EndDisabledGroup();
        }
    }
}