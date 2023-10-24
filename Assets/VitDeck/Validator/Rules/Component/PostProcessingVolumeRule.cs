using UnityEngine;
using VitDeck.Language;
using UnityEngine.Rendering.PostProcessing;

namespace VitDeck.Validator
{
    internal class PostProcessingVolumeRule : ComponentBaseRule<PostProcessVolume>
    {

        public PostProcessingVolumeRule(string name) : base(name)
        {
        }

        protected override void ComponentLogic(PostProcessVolume component)
        {
            if (component.isGlobal)
            {
                // IsGlobalはオフにすること
                AddIssue(new Issue(component, IssueLevel.Error, LocalizedMessage.Get("PostProcessingRule.IsGlobal")));
            }

            PostProcessProfile profile = component.sharedProfile;

            if (profile != null)
            {
                if (profile.HasSettings<AmbientOcclusion>())
                {
                    // AmbientOcclusionはオフにすること
                    AddIssue(new Issue(component, IssueLevel.Error, LocalizedMessage.Get("PostProcessingRule.AmbientOcclusion")));
                }

                if (profile.HasSettings<ScreenSpaceReflections>())
                {
                    // ScreenSpaceReflectionsはオフにすること
                    AddIssue(new Issue(component, IssueLevel.Error, LocalizedMessage.Get("PostProcessingRule.ScreenSpaceReflections")));
                }

                if (profile.HasSettings<DepthOfField>())
                {
                    // DepthOfFieldはオフにすること
                    AddIssue(new Issue(component, IssueLevel.Error, LocalizedMessage.Get("PostProcessingRule.DepthOfField")));
                }

                if (profile.HasSettings<MotionBlur>())
                {
                    // MotionBlurはオフにすること
                    AddIssue(new Issue(component, IssueLevel.Error, LocalizedMessage.Get("PostProcessingRule.MotionBlur")));
                }

                if (profile.HasSettings<LensDistortion>())
                {
                    // LensDistortionはオフにすること
                    AddIssue(new Issue(component, IssueLevel.Error, LocalizedMessage.Get("PostProcessingRule.LensDistortion")));
                }
            }
        }

        protected override void HasComponentObjectLogic(GameObject hasComponentObject)
        {
        }
    }
}