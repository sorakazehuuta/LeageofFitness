using UnityEngine;
using UnityEditor;
using VitDeck.Language;

namespace VitDeck.Validator
{
    public class StaticFlagRule : BaseRule
    {

        //private const StaticEditorFlags mustFlag = StaticEditorFlags.OccludeeStatic | StaticEditorFlags.ReflectionProbeStatic;

        private bool _isItem;
        
        public StaticFlagRule(string name, bool isItem = false) : base(name)
        {
            _isItem = isItem;
        }

        protected override void Logic(ValidationTarget target)
        {
            var rootObjects = target.GetRootObjects();

            if (_isItem)
            {
                foreach(var rootObject in rootObjects)
                {
                    LogicForStaticRootItem(rootObject.transform);
                }
            }
            else
            {
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
        }
        
        private void LogicForStaticRootItem(Transform staticRoot)
        {
            var children = staticRoot.GetComponentsInChildren<Transform>(true);

            foreach(var child in children)
            {
                var gameObject = child.gameObject;
                // Static設定がされている場合
                if (gameObject.isStatic)
                {
                    AddIssue(new Issue(
                        gameObject,
                        IssueLevel.Error,
                        LocalizedMessage.Get("StaticFlagRule.ItemStaticSet"),
                        LocalizedMessage.Get("StaticFlagRule.ItemStaticSet.Solution")));
                }
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
                    foreach (var filter in gameObject.GetComponents<MeshFilter>())
                    {
                        if (filter == null) 
                            continue;
                            
                        var mesh = filter.sharedMesh;
                        if (mesh == null) // メッシュが設定されていない場合はチェック対象外
                            continue;
                            
                        if (mesh.uv2.Length != 0) // uv2があればLightmapとして利用できる為問題なし
                            continue;

                        var assetPath = AssetDatabase.GetAssetPath(mesh);
                        if (string.IsNullOrWhiteSpace(assetPath)) // 対象のメッシュがアセットでない
                        {
                            AddIssueForIndependentMeshWithoutUV2(filter);
                            continue;
                        }
                            
                        var importer = AssetImporter.GetAtPath(assetPath) as ModelImporter;
                        if (importer == null) // 対象のメッシュのimporterがない（モデルインポートでないメッシュアセット）
                        {
                            AddIssueForIndependentMeshWithoutUV2(filter);
                            continue;
                        }

                        if (!importer.generateSecondaryUV) // 対象のメッシュアセットのgenerateSecondaryUVが無効になっている
                        {
                            var message = LocalizedMessage.Get("StaticFlagRule.LightmapStaticMeshAssetShouldGenerateLightmap");
                            var solution = LocalizedMessage.Get("StaticFlagRule.LightmapStaticMeshAssetShouldGenerateLightmap.Solution");
                            var solutionURL = LocalizedMessage.Get("StaticFlagRule.LightmapStaticMeshAssetShouldGenerateLightmap.SolutionURL");
                        
                            AddIssue(new Issue(filter, IssueLevel.Warning, message, solution, solutionURL));
                        }
                    }
                }
                
                if ((flag & StaticEditorFlags.OccluderStatic) != 0)
                {
                    AddIssue(new Issue(gameObject, IssueLevel.Error, /* "OccluderStaticを無効にして下さい。" */ LocalizedMessage.Get("StaticFlagRule.OccluderStaticNotAllowed")));
                }
                
                if((flag & StaticEditorFlags.OccludeeStatic) == 0)
                {
                    AddIssue(new Issue(
                        gameObject,
                        IssueLevel.Error,
                        LocalizedMessage.Get("StaticFlagRule.OccludeeStaticNotSet"),
                        LocalizedMessage.Get("StaticFlagRule.OccludeeStaticNotSet.Solution")));
                }

                if ((flag & StaticEditorFlags.NavigationStatic) != 0)
                {
                    AddIssue(new Issue(gameObject, IssueLevel.Error, /* "NavigationStaticを無効にして下さい。" */ LocalizedMessage.Get("StaticFlagRule.NavigationStaticNotAllowed")));
                }
                
                if ((flag & StaticEditorFlags.OffMeshLinkGeneration) != 0)
                {
                    AddIssue(new Issue(gameObject, IssueLevel.Error, /* "OffMeshLinkGenerationを無効にして下さい。" */ LocalizedMessage.Get("StaticFlagRule.OffMeshLinkGenerationNotAllowed")));
                }
            }
        }

        private void AddIssueForIndependentMeshWithoutUV2(MeshFilter filter)
        {
            var message = LocalizedMessage.Get("StaticFlagRule.LightmapStaticMeshShouldHaveUV2");
            var solution = LocalizedMessage.Get("StaticFlagRule.LightmapStaticMeshShouldHaveUV2.Solution");
            var solutionURL = LocalizedMessage.Get("StaticFlagRule.LightmapStaticMeshShouldHaveUV2.SolutionURL");

            AddIssue(new Issue(filter, IssueLevel.Warning, message, solution, solutionURL));
        }
    }
}