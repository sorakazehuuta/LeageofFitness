using UnityEngine;
using VitDeck.Language;

namespace VitDeck.Validator
{
    public class CameraComponentRule : ComponentBaseRule<Camera>
    {
        public CameraComponentRule(string name) : base(name)
        {
        }

        protected override void ComponentLogic(Camera component)
        {
            DefaultDisabledLogic(component);
            NeedRenderTextureLogic(component);
        }

        private void DefaultDisabledLogic(Camera component)
        {
            if (component.enabled)
            {
                var message = LocalizedMessage.Get("CameraComponentRule.DefaultDisabled");
                var solution = LocalizedMessage.Get("CameraComponentRule.DefaultDisabled.Solution");

                // コンポーネントは初期状態でDisabledにする必要があります。
                AddIssue(new Issue(
                    component,
                    IssueLevel.Error,
                    message,
                    solution));
            }
        }

        private void NeedRenderTextureLogic(Camera component)
        {
            if (component.targetTexture == null)
            {
                var message = LocalizedMessage.Get("CameraComponentRule.NeedRenderTexture");

                // TargetTextureには必ずRenderTextureを指定してください
                AddIssue(new Issue(
                    component,
                    IssueLevel.Error,
                    message));
            }
        }

        protected override void HasComponentObjectLogic(GameObject hasComponentObject)
        {
        }
    }
}