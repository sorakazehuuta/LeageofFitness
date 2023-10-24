using UnityEditor;
using UnityEngine;
using VitDeck.Language;

namespace VitDeck.Validator
{
    internal class MeshRendererRule : ComponentBaseRule<MeshRenderer>
    {
        private bool _isItem;
        
        public MeshRendererRule(string name, bool isItem = false) : base(name)
        {
            _isItem = isItem;
        }

        protected override void ComponentLogic(MeshRenderer component)
        {
            // Material0のMesh Rendererは禁止
            if (component.sharedMaterials.Length == 0)
            {
                AddIssue(new Issue(
                    component,
                    IssueLevel.Error,
                    LocalizedMessage.Get("SkinnedMeshRendererRule.MustAttachMaterial")));
            }

            if (!_isItem)
            {
                // Lightmap Parametersの変更は禁止
                var staticEditorFlagsProp = new SerializedObject(component.gameObject).FindProperty("m_StaticEditorFlags");
                bool contributeGI = (staticEditorFlagsProp.intValue & (int)StaticEditorFlags.ContributeGI) != 0;
                var lightmapParametersProp = new SerializedObject(component).FindProperty("m_LightmapParameters");
                var lightmapParameters = lightmapParametersProp.objectReferenceValue as LightmapParameters;
                if (contributeGI && lightmapParameters != null)
                {
                    AddIssue(new Issue(
                        component,
                        IssueLevel.Error,
                        LocalizedMessage.Get("MeshRendererRule.ChangeLightmapParameters")));
                }
            }
        }

        protected override void HasComponentObjectLogic(GameObject hasComponentObject)
        {
            var flags = GameObjectUtility.GetStaticEditorFlags(hasComponentObject);

            if (IsLightmapStatic(flags) && IsNotInEnvironmentLayer(hasComponentObject))
            {
                AddIssue(new Issue(
                    hasComponentObject,
                    IssueLevel.Error,
                    LocalizedMessage.Get("MeshRendererRule.StaticMeshMustPutInEnvironmentLayer")));
            }
        }

        private static bool IsNotInEnvironmentLayer(GameObject hasComponentObject)
        {
            return hasComponentObject.layer != LayerMask.NameToLayer("Environment");
        }

        private static bool IsLightmapStatic(StaticEditorFlags flags)
        {
            return (flags & StaticEditorFlags.ContributeGI) == StaticEditorFlags.ContributeGI;
        }
    }
}