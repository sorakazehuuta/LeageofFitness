using System.Linq;
using UnityEngine;
using VitDeck.Language;

namespace VitDeck.Validator
{
    public class DisallowEditorOnlyTagRule : BaseRule
    {
        private bool _isItem = false;
        public DisallowEditorOnlyTagRule(string name, bool isItem = false) : base(name)
        {
            _isItem = isItem;
        }

        protected override void Logic(ValidationTarget target)
        {
            var rootObjects = target.GetRootObjects();

            if (_isItem)
            {
                var objs = rootObjects[0].GetComponentsInChildren<Transform>(true).Distinct().Select(t => t.gameObject);
                foreach (var obj in objs)
                {
                    if (obj.CompareTag("EditorOnly"))
                    {
                        var message = LocalizedMessage.Get("DisallowEditorOnlyTagRule.Disallow");
                        var solution = LocalizedMessage.Get("DisallowEditorOnlyTagRule.Disallow.Solution");
                        AddIssue(new Issue(
                            obj,
                            IssueLevel.Error,
                            message,
                            solution));
                    }
                }
            }
            else
            {
                // DynamicおよびStaticオブジェクトを取得
                var dynamics = rootObjects.Select(o => o.transform.Find("Dynamic")).Distinct();
                var statics = rootObjects.Select(o => o.transform.Find("Static")).Distinct();

                // DynamicおよびStaticオブジェクトの子オブジェクトをすべて取得
                var objs = dynamics.Union(statics).SelectMany(t => t.GetComponentsInChildren<Transform>(true))
                    .Distinct().Select(t => t.gameObject);
                foreach (var obj in objs)
                {
                    if (obj.CompareTag("EditorOnly"))
                    {
                        // ItemPageOpenerおよびCirclePageOpenerは仕様上EditorOnlyタグが付与されるため除外
                        // ※VketAssetsをインポートしていなくても動作するようにするため、クラス名を直接文字列で指定している
                        var itemPageOpener = obj.GetComponent("ItemPageOpener");
                        var circlePageOpener = obj.GetComponent("CirclePageOpener");
                        if (itemPageOpener != null || circlePageOpener != null)
                            continue;

                        var message = LocalizedMessage.Get("DisallowEditorOnlyTagRule.Disallow");
                        var solution = LocalizedMessage.Get("DisallowEditorOnlyTagRule.Disallow.Solution");
                        AddIssue(new Issue(
                            obj,
                            IssueLevel.Error,
                            message,
                            solution));
                    }
                }
            }
        }
    }
}