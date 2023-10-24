using UnityEngine;
using VitDeck.Language;

namespace VitDeck.Validator
{
    internal class ParticleSystemForceFieldRule : ComponentBaseRule<ParticleSystemForceField>
    {
        public ParticleSystemForceFieldRule(string name) : base(name)
        {
        }

        protected override void ComponentLogic(ParticleSystemForceField component)
        {
            switch (component.shape)
            {
                case ParticleSystemForceFieldShape.Box:
                case ParticleSystemForceFieldShape.Sphere:
                    break;
                default:
                {
                    AddIssue(new Issue(
                        component,
                        IssueLevel.Error,
                        /* "ParticleSystemForceFieldのShapeはBoxまたはSphereを使用してください。" */ LocalizedMessage.Get("ParticleSystemForceFieldRule.ShapeError")));
                }
                    break;
            }
        }

        protected override void HasComponentObjectLogic(GameObject hasComponentObject)
        {
        }
    }
}