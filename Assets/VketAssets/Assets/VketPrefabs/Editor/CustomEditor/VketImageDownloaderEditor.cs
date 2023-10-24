
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;

namespace Vket.VketPrefabs
{
    [CustomEditor(typeof(VketImageDownloader))]
    public class VketImageDownloaderEditor : Editor
    {
        private Vector2 scrollPosition = Vector2.zero;

        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            // Draw Title and Summary
            var style = new GUIStyle();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("  Vket Image Downloader", new GUIStyle(EditorStyles.boldLabel));
            EditorGUILayout.EndVertical();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(94));
            EditorGUILayout.TextArea(
                "** ReadMe **\n" +
                "- プレイヤーがブースに入った時にURLから画像を読み込みます。\n" +
                "- \"Target Renderer\"と\"Target Material\"で読み込んだ画像の表示先を指定します。\n" +
                "- \"Texture Info\"でテクスチャの詳細設定や読込先のシェーダープロパティを設定出来ます。\n" +
                "- 他のブースで画像が読み込まれると、画像を表示していたRendererは読み込み前の表示に戻ります。\n" +
                "- When a player enters a booth, an image is loaded from a URL.\n" +
                "- \"Target Renderer\" and \"Target Material\" specify where the loaded image will be displayed.\n" +
                "- \"Texture Info\" allows the player to set up detailed texture settings and shader properties for the target material.\n" +
                "- When the image is loaded in another booth, the Renderer that was displaying the image will return to the pre-loaded view.");
            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space();

            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("url"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("targetRenderer"));
            var renderer = (Renderer)serializedObject.FindProperty("targetRenderer").objectReferenceValue;
            if (renderer != null)
            {
                var indexProp = serializedObject.FindProperty("materialIndex");
                EditorGUI.BeginChangeCheck();
                var index = EditorGUILayout.Popup("Target Material", indexProp.intValue, GetMaterialNames(renderer));
                if (EditorGUI.EndChangeCheck())
                {
                    indexProp.intValue = index;
                }
            }
            else
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.Popup("Target Material", 0, new string[] { "" });
                EditorGUI.EndDisabledGroup();
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("textureInfo"));

            serializedObject.ApplyModifiedProperties();
        }

        private string[] GetMaterialNames(Renderer renderer)
        {
            var materials = renderer.sharedMaterials;

            var names = new string[materials.Length];
            for(int i=0; i < materials.Length; i++)
                names[i] = materials[i].name;

            return names;
        }
    }
}
