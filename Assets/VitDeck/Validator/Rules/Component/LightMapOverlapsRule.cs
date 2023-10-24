
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using VitDeck.Language;

namespace VitDeck.Validator
{
    /// <summary>
    /// ContributeGIにチェックが入っているオブジェクト(ScalesInLightmapの値が0のものは除く)の中で、ライトマップがオーバーラップ状態になっているものは入稿できない
    /// </summary>
    public class LightMapOverlapsRule : BaseRule
    {

        public LightMapOverlapsRule(string name) : base(name)
        {
        }

        protected override void Logic(ValidationTarget target)
        {
            var rootObjects = target.GetRootObjects();

            foreach(var rootObject in rootObjects)
            {
                var staticRoot = rootObject.transform.Find("Static");
                if (staticRoot == null)
                {
                    continue;
                }

                LogicForStaticRoot(staticRoot);
            }
        }

        private void LogicForStaticRoot(Transform staticRoot)
        {
            var children = staticRoot.GetComponentsInChildren<Transform>(true);
            
            foreach(var child in children)
            {
                var gameObject = child.gameObject;
                var flag = GameObjectUtility.GetStaticEditorFlags(child.gameObject);
                
                if ((flag & StaticEditorFlags.ContributeGI) != 0)
                {
                    foreach (var renderer in gameObject.GetComponents<Renderer>())
                    {
                        if (renderer == null) 
                            continue;
                        
                        var lightmapScaleProp = new SerializedObject(renderer).FindProperty("m_ScaleInLightmap");
                        var lightmapScale = lightmapScaleProp.floatValue;
                        
                        var layout = Type.GetType("UnityEditor.LightmapEditorSettings,UnityEditor");
                        var method = layout.GetMethod("HasUVOverlaps", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static, null, new[] {typeof(Renderer)}, null);
                        
                        if (lightmapScale != 0 && (bool)method.Invoke(null, new object[] {renderer}))
                        {
                            var message = LocalizedMessage.Get("LightMapOverlapsRule.Message");
                            var solution = LocalizedMessage.Get("LightMapOverlapsRule.LightmapStaticMeshAssetShouldGenerateLightmap.Solution");

                            AddIssue(new Issue(gameObject, IssueLevel.Error, message, solution));
                        }
                    }
                }
            }
        }
    }
}