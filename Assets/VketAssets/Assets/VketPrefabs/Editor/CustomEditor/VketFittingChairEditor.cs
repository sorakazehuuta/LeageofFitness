
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;

namespace Vket.VketPrefabs
{
    [CustomEditor(typeof(VketFittingChair))]
    public class VketFittingChairEditor : Editor
    {
        private Vector2 scrollPosition = Vector2.zero;
        private VketFittingChair _vketFittingChair;

        private void OnEnable()
        {
            _vketFittingChair = target as VketFittingChair;
        }

        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            // Draw Title and Summary
            var style = new GUIStyle();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("  Vket Fitting Chair", new GUIStyle(EditorStyles.boldLabel));
            EditorGUILayout.EndVertical();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(108));
            EditorGUILayout.TextArea(
                "** ReadMe **\n" +
                "- Hipボーンの位置基準で座れる椅子です。座ると座面の高さをHipの高さに自動調節します\n" +
                "- \"Visual\"オブジェクトの子に見た目に使用したいオブジェクトを追加してください\n" +
                "　追加したオブジェクトのコライダーは削除してください\n" +
                "- その後、\"EnterLocation\"オブジェクトを動かして座る位置を調整してください\n" +
                "  \"EnterLocation\"オブジェクトの高さにHipが来るよう調整されます\n" +
                "- This chair is designed to sit on the Hip Bone position standard.\n" +
                "  Automatically adjusts seat height to Hip height when seated.\n" +
                "- Add the object you want to use for the look and feel to the children of the \"Visual\" object.\n" +
                "  Delete the collider of the added object\n" +
                "- Then adjust the seating position by moving the \"EnterLocation\" object.\n" +
                "　The Hip is adjusted to be at the height of the \"EnterLocation\" object.");
            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space();
            
            EditorGUI.BeginDisabledGroup(_vketFittingChair.gameObject.scene.name == null);
            if (GUILayout.Button("Open Setting Window"))
            {
                VketPrefabSettingWindow.OpenSettingWindow<VketFittingChairSettingWindow>(_vketFittingChair.transform);
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}