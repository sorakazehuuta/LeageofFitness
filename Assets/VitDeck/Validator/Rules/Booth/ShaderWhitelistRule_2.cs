using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using VitDeck.Language;

namespace VitDeck.Validator
{
    /// <summary>
    /// 使用可能なシェーダーをホワイトリスト検証します。
    /// </summary>
    public class ShaderWhitelistRule_2 : BaseRule
    {
        private readonly string[] shaderName;
        private readonly string solution;
        private readonly string solutionURL;

        /// <param name="name">ルール名</param>
        /// <param name="shaderNameGUIDPairs">キーにシェーダー名、値にシェーダーのGUIDを持つ連想配列。ビルトインシェーダーは常に許可。</param>
        public ShaderWhitelistRule_2(string name,string[] shaderName) : base(name)
        {
            this.shaderName = shaderName;
        }

        protected override void Logic(ValidationTarget target)
        {
            foreach (var gameObject in target.GetAllObjects())
            {
                foreach (var shader in this.GetShaders(gameObject))
                {
                    if (shader.name == "Hidden/InternalErrorShader") {
                        continue;
                    }


                    var path = AssetDatabase.GetAssetPath(Shader.Find(shader.name));

                    bool flag = true;

                    foreach(string s in shaderName)
                    {
                        if (shader.name.StartsWith(s))
                        {
                            flag = false;
                        }
                    }

                    if (flag)
                    {
                        this.AddIssue(new Issue(
                            gameObject,
                            IssueLevel.Error,
                            LocalizedMessage.Get("Booth.ShaderWhiteListRule.DisallowedShader", shader.name),
                            solution,
                            solutionURL
                        ));
                        continue;
                    }

                    foreach (var mat in this.GetMaterial(gameObject))
                    {
                        if (shader.name == "Standard" && mat.GetFloat("_Mode") == 2f)
                        {
//                            this.AddIssue(new Issue(
//    gameObject,
//    IssueLevel.Error,
//    LocalizedMessage.Get("Booth.ShaderWhiteListRule.DisallowedShader", "Standard/Fade"),
//    solution,
//    solutionURL
//));
                            continue;
                        }
                    }



                }
            }
        }

        /// <summary>
        /// 指定されたオブジェクトが参照するシェーダーを取得します。
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        private IEnumerable<Shader> GetShaders(GameObject gameObject)
        {
            return gameObject.GetComponents<Renderer>()
                .SelectMany(renderer => renderer.sharedMaterials)
                .Where(material => material)
                .Select(material => material.shader);
        }

        private IEnumerable<Material> GetMaterial(GameObject gameObject)
        {
            return gameObject.GetComponents<Renderer>()
    .SelectMany(renderer => renderer.sharedMaterials)
    .Where(material => material)
    .Select(material => material);
        }
    }
}
