using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using System.Text;
using UnityEngine.UIElements;

namespace Vket.VketPrefabs.Language
{
    [CustomEditor(typeof(LanguageDictionary))]
    public class LanguageDictionaryEditor : Editor
    {
        [SerializeField]
        private TreeViewState treeViewState;

        private LanguageDictionary dictionary;

        private LanguageDictionaryTreeView treeView;
        private SearchField searchField;

        private GUIStyle wordWrapTextAreaStyle = default;

        private void OnEnable()
        {
            dictionary = (LanguageDictionary)target;
            treeView = LanguageDictionaryTreeView.CreateInstance(
                dictionary,
                ref treeViewState);

            searchField = new SearchField();
            searchField.downOrUpArrowKeyPressed += treeView.SetFocusAndEnsureSelectedItem;
        }

        public override void OnInspectorGUI()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                GUILayout.Label("Clipboard(tsv):");

                if (GUILayout.Button("Copy", EditorStyles.miniButton))
                {
                    var tsv = ToTSV();
                    GUIUtility.systemCopyBuffer = tsv;
                }

                if (GUILayout.Button("Paste", EditorStyles.miniButton))
                {
                    var tsv = GUIUtility.systemCopyBuffer;
                    ReadFromTSV(tsv);
                }
            }

            treeView.searchString = searchField.OnGUI(treeView.searchString);

            var rect = EditorGUILayout.GetControlRect(false, 200);
            treeView.OnGUI(rect);

            GUILayout.Space(10);

            SelectedMessageView();
        }

        private void SelectedMessageView()
        {
            if (wordWrapTextAreaStyle == null)
            {
                wordWrapTextAreaStyle = new GUIStyle(EditorStyles.textArea);
                wordWrapTextAreaStyle.wordWrap = true;
            }

            EditorGUI.BeginChangeCheck();
            var selections = treeView.GetSelection();
            var translations = dictionary.GetTranslations();
            foreach (var selection in selections)
            {
                GUILayout.Space(10);
                var selected = translations[selection];
                GUILayout.Label(selected.key, EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                translations[selection].value = EditorGUILayout.TextArea(selected.value, wordWrapTextAreaStyle);
                EditorGUI.indentLevel--;
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(dictionary);
            }
        }

        private void ReadFromTSV(string tsvText)
        {
            
            var translations = new List<LanguageDictionary.Pair>();
            var tsv = CSVParser.LoadFromString(tsvText, CSVParser.Delimiter.Tab);
            
            int messageIDColumnIndex = 0;
            int messageColumnIndex = 1;
            
            foreach (var row in tsv)
            {
                var messageID = row[messageIDColumnIndex];
                var message = row[messageColumnIndex];
                
                translations.Add(new LanguageDictionary.Pair() { key = messageID, value = message });
            }

            dictionary.SetTranslations(translations.ToArray());
            EditorUtility.SetDirty(target);
            treeView.Reload();
        }

        private string ToTSV()
        {
            var translations = dictionary.GetTranslations();
            var builder = new StringBuilder();

            foreach (var translation in translations)
            {
                var value = translation.value;

                if (IsMustQuote(value))
                {
                    value = $"\"{value.Replace("\"","\\\"")}\"";
                }

                builder.AppendFormat("{0}\t{1}\n", translation.key, value);
            }

            return builder.ToString();
        }

        private bool IsMustQuote(string value)
        {
            return value.Contains("\t") || value.Contains("\n");
        }
    }
}