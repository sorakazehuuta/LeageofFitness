using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

namespace CombatGimmick
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class HierarchyHighlighter : MonoBehaviour
    {
#if UNITY_EDITOR
        static Texture2D defaultHierarchyIcon;
        private static Vector2 offset = new Vector2(0, 2);
        private const int IconSize = 15;

        static HierarchyHighlighter()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        }

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            Color fontColor = Color.blue;
            Color backgroundColor = new Color(.76f, .76f, .76f);
            
            var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (obj != null && obj.GetComponent<CombatGimmickBuilder>())
            {
                
                if (Selection.instanceIDs.Contains(instanceID))
                {
                    fontColor = Color.white;
                    backgroundColor = new Color(0.24f, 0.48f, 0.90f);
                }
                
                var texture2D = AssetPreview.GetMiniThumbnail(obj);
                GUI.DrawTexture(selectionRect, texture2D);
                selectionRect.x += IconSize;

                Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size);
                EditorGUI.DrawRect(selectionRect, backgroundColor);
                EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle()
                {
                    normal = new GUIStyleState() { textColor = fontColor },
                    fontStyle = FontStyle.Bold
                }
                );                
            }
        }
#endif
    }
}
