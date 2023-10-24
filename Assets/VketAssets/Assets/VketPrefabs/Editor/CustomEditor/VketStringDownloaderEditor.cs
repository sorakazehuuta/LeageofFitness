
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Vket.VketPrefabs
{
    [CustomEditor(typeof(VketStringDownloader))]
    public class VketStringDownloaderEditor : Editor
    {
        private Vector2 scrollPosition = Vector2.zero;

        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            // Draw Title and Summary
            var style = new GUIStyle();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("  Vket String Downloader", new GUIStyle(EditorStyles.boldLabel));
            EditorGUILayout.EndVertical();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(64));
            EditorGUILayout.TextArea(
                "** ReadMe **\n" +
                "- プレイヤーがブースに入った時にURLからテキストを読み込みます。\n" +
                "- 読み込んだテキストは\"Target Text\"に指定したComponentに書き込まれます。\n" +
                "  ComponentにはText, TextMeshPro, TextMeshProUGUIが指定出来ます。\n" +
                "- When a player enters a booth, text is loaded from a URL.\n" +
                "- The loaded text is written to the component specified in the \"Target Text\" field.\n" +
                "　Text, TextMeshPro, and TextMeshProUGUI can be specified.");
            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space();

            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("url"));

            var textProp = serializedObject.FindProperty("targetText");
            EditorGUI.BeginChangeCheck();
            var component = (Component)EditorGUILayout.ObjectField("Target Text", textProp.objectReferenceValue, typeof(Component), true);
            if (EditorGUI.EndChangeCheck())
            {
                if (component != null)
                {
                    var type = component.GetType();
                    if (type == typeof(Text) || type == typeof(TextMeshPro) || type == typeof(TextMeshProUGUI))
                    {
                        textProp.objectReferenceValue = component;
                    }
                    else
                    {
                        var textComponent = GetTextComponent(component);
                        if (textComponent != null)
                            textProp.objectReferenceValue = textComponent;
                    }
                }
                else
                {
                    textProp.objectReferenceValue = null;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private Component GetTextComponent(Component component)
        {
            Component textComponent;

            textComponent = component.GetComponent<Text>();
            if (textComponent != null)
                return textComponent;

            textComponent = component.GetComponent<TextMeshPro>();
            if (textComponent != null)
                return textComponent;

            textComponent = component.GetComponent<TextMeshProUGUI>();
            return textComponent;
        }
    }
}
