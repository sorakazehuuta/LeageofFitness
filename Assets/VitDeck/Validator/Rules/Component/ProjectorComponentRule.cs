using UnityEngine;
using VitDeck.Language;

namespace VitDeck.Validator
{
    public class ProjectorComponentRule : ComponentBaseRule<Projector>
    {
        public ProjectorComponentRule(string name) : base(name)
        {
        }

        protected override void ComponentLogic(Projector component)
        {
            DefaultDisabledLogic(component);
        }

        private void DefaultDisabledLogic(Projector component)
        {

        }

        protected override void HasComponentObjectLogic(GameObject hasComponentObject)
        {
            if (hasComponentObject.activeSelf)
            {

                var message = LocalizedMessage.Get("ProjectorComponentRule.DefaultDisabled");
                var solution = LocalizedMessage.Get("ProjectorComponentRule.DefaultDisabled.Solution");

                AddIssue(new Issue(
                    hasComponentObject,
                    IssueLevel.Error,
                    message,
                    solution));
            }
        }
    }
}