using UnityEngine;
using VitDeck.Language;
using System.Collections.Generic;
using UnityEditor;
#if VRC_SDK_VRCSDK3
using System.IO;
using System.Text.RegularExpressions;

namespace VitDeck.Validator
{
    /// <summary>
    /// 禁止された #if UNITY_EDITOR が使用されていないか検出するルール
    /// </summary>
    public class DisabledDirectiveRule : BaseRule
    {
        private readonly HashSet<string> excludedAssetGUIDs;
        private readonly Dictionary<string, string> disabledDirective;
        public DisabledDirectiveRule(string name, Dictionary<string, string> disabledDirective, string[] excludedAssetGUIDs) : base(name)
        {
            this.excludedAssetGUIDs = new HashSet<string>(excludedAssetGUIDs);
            this.disabledDirective = disabledDirective;
        }
        protected override void Logic(ValidationTarget target)
        {
            var assets = target.GetAllAssets();

            foreach (var asset in assets)
            {
                if (asset.GetType() == typeof(MonoScript) && !excludedAssetGUIDs.Contains(GetGUID(asset)))
                {
                    using (var fs = new StreamReader(AssetDatabase.GetAssetPath(asset), System.Text.Encoding.GetEncoding("UTF-8")))
                    {
                        string code = fs.ReadToEnd();
                        foreach (var disabled in disabledDirective)
                        {
                            var directivePattern = disabled.Key;
                            var directiveInMessage = disabled.Value;
                            if (Regex.IsMatch(code, directivePattern))
                            {
                                // {0}は使用しないこと
                                var message = LocalizedMessage.Get("DisabledDirectiveRule.Use", Regex.Match(code, directiveInMessage));
                                // 使用禁止されたディレクティブを使用しないでください
                                var solution = LocalizedMessage.Get("DisabledDirectiveRule.Use.Solution");

                                AddIssue(new Issue(
                                    asset,
                                    IssueLevel.Error,
                                    message,
                                    solution));
                            }
                        }
                    }
                }
            }
        }
        private static string GetGUID(Object asset)
        {
            return AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(asset));
        }
    }
}
#endif