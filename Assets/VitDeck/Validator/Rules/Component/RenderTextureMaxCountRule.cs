using System.Linq;
using UnityEngine;
using VitDeck.Language;

namespace VitDeck.Validator
{
    public class RenderTextureMaxCountRule : BaseRule
    {
        int _limit;

        public RenderTextureMaxCountRule(string name, int limit) : base(name)
        {
            this._limit = limit;
        }

        protected override void Logic(ValidationTarget target)
        {
            // カメラに設定されているすべてのRenderTextureを取得する
            var cameraObjects = target.GetAllObjects().Select(o => o.GetComponent<Camera>()).Where(c => c != null);
            var renderTextures = cameraObjects.Select(c => c.targetTexture as RenderTexture).Where(r => r != null).Distinct();

            // レンダーテクスチャの数が上限を超えていたらエラーを出す
            var renderTexturesCount = renderTextures.Count();
            if (renderTexturesCount > _limit)
            {
                var message = LocalizedMessage.Get("RenderTextureMaxCountRule.Exceeded", _limit, renderTexturesCount);
                var solution = LocalizedMessage.Get("RenderTextureMaxCountRule.Exceeded.Solution");

                foreach (var renderTexture in renderTextures)
                {
                    AddIssue(new Issue(
                        renderTexture,
                        IssueLevel.Error,
                        message,
                        solution));
                }
            }
        }
    }
}