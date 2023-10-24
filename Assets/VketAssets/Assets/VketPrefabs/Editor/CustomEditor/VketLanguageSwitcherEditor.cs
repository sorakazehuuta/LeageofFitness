
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;

namespace Vket.VketPrefabs
{
    [CustomEditor(typeof(VketLanguageSwitcher))]
    public class VketLanguageSwitcherEditor : Editor
    {
        private Vector2 scrollPosition = Vector2.zero;
        private VketLanguageSwitcher _vketLanguageSwitcher;

        private void OnEnable()
        {
            _vketLanguageSwitcher = target as VketLanguageSwitcher;
        }
        
        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            // Draw Title and Summary
            var style = new GUIStyle();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("  Vket Language Switcher", new GUIStyle(EditorStyles.boldLabel));
            EditorGUILayout.EndVertical();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(94));
            EditorGUILayout.TextArea(
                "** ReadMe **\n" +
                "- Interactするとワールドの言語設定が切り替わります(ローカル)\n" +
                "- 日本語設定の時は\"Jp Switch Objects\"に登録したオブジェクトが表示され、\n" +
                "　\"En Switch Objects\"に登録したオブジェクトが非表示になります\n" +
                "　英語設定の時はその反対になります\n" +
                "- \"Switch To English/Japanese Sprite\"にSpriteを登録すると表示画像を変更できます\n" +
                "- Players can change the world language setting locally by Interact.\n" +
                "- When it is set to Japanese objects registered to \"Jp Switch Objects\" will be displayed\n" +
                "  while objects registered to \"En Switch Objects\" will disappear.\n" +
                "  and vice versa when set to English.\n" +
                "  You can change the appearance image by setting a Sprite on \"Switch To English/Japanese Sprite\".");
            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space();

            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("jpSwitchObjects"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("enSwitchObjects"));

            // Draw Field of Switch To English Sprite
            var enSpriteProperty = serializedObject.FindProperty("switchToEnglishSprite");
            EditorGUI.BeginChangeCheck();
            var toEnSprite = (Sprite)EditorGUILayout.ObjectField("Switch To English Sprite", enSpriteProperty.objectReferenceValue, typeof(Sprite), false);
            if (EditorGUI.EndChangeCheck())
            {
                enSpriteProperty.objectReferenceValue = toEnSprite;
                var spriteRenderer = (SpriteRenderer)serializedObject.FindProperty("spriteRenderer").objectReferenceValue;
                Undo.RecordObject(spriteRenderer, "Modify switch to english sprite");
                spriteRenderer.sprite = toEnSprite;
            }

            // Draw Field of Switch To Japanese Sprite
            var jpSpriteProperty = serializedObject.FindProperty("switchToJapaneseSprite");
            EditorGUI.BeginChangeCheck();
            var toJpSprite = (Sprite)EditorGUILayout.ObjectField("Switch To Japanese Sprite", jpSpriteProperty.objectReferenceValue, typeof(Sprite), false);
            if (EditorGUI.EndChangeCheck())
                jpSpriteProperty.objectReferenceValue = toJpSprite;

            serializedObject.ApplyModifiedProperties();

            EditorGUI.BeginDisabledGroup(_vketLanguageSwitcher.gameObject.scene.name == null);
            if (GUILayout.Button("Open Setting Window"))
            {
                VketPrefabSettingWindow.OpenSettingWindow<VketLanguageSwitcherSettingWindow>(_vketLanguageSwitcher.transform);
            }
            EditorGUI.EndDisabledGroup();
            
            //base.OnInspectorGUI();
        }
    }
}