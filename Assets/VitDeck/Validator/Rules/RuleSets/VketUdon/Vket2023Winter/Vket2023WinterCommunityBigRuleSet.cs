#if VRC_SDK_VRCSDK3
using UnityEngine;

namespace VitDeck.Validator.RuleSets
{
    /// <summary>
    /// コミュニティコラボブース 大規模ブースプラン
    /// </summary>
    public class Vket2023WinterCommunityBigRuleSet : VketUdonRuleSetBase
    {
        public Vket2023WinterCommunityBigRuleSet() : base(new Vket2023WinterOfficialAssetData())
        {
        }
        public override string RuleSetName => "Vket2023Winter - Community (Big)";
        
        protected override long FolderSizeLimit => 100 * MegaByte;

        protected override Vector3 BoothSizeLimit => new Vector3(15, 8, 10);

        protected override int UdonBehaviourCountLimit => 20;

        protected override int VRCObjectSyncCountLimit => 10;

        protected override int VRCObjectPoolCountLimit => 0;

        protected override int VRCObjectPoolPoolLimit => 0;

        protected override int VRCPickupCountLimit => 10;

        protected override int UdonBehaviourSynchronizePositionCountLimit => 10;

        protected override int UdonScriptSyncedVariablesLimit => 10;

        protected override int MaterialUsesLimit => 0;

        protected override int LightmapCountLimit => 2;

        protected override int LightmapSizeLimit => 512;

        protected override int VRCStationCountLimit => 4;

        protected override int ClothCountLimit => 1;

        protected override int AnimatorCountLimit => 50;

        protected override int AudioSourceCountLimit => 20;
        
        protected override float AudioSourceMaxDistance => 10;

        protected override int VketImageDownloaderUsesLimit => 1;

        protected override int VketStringDownloaderUsesLimit => 1;
        
        protected override int VketStarshipTreasureUsesLimit => 0;

        protected override int VketVideoPlayerUsesLimit => 1;

        protected override int CameraCountLimit => 2;

        protected override int RenderTextureCountLimit => 2;

        protected override Vector2 RenderTextureSizeLimit => new Vector2(1024, 1024);

        protected override float RayCastLength => 10.0f;

        protected override bool AllowIsKinematic => true;

        protected override LightConfigRule.LightConfig ApprovedPointLightConfig
            => new LightConfigRule.LightConfig(
                new[] { LightmapBakeType.Baked },
                minRange: 0, maxRange: 12,
                minIntensity: 0, maxIntensity: 10,
                minBounceIntensity: 0, maxBounceIntensity: 15,
                approvedShadowTypes: new LightShadows[] { LightShadows.Soft, LightShadows.Hard });

        protected override LightConfigRule.LightConfig ApprovedSpotLightConfig
            => new LightConfigRule.LightConfig(
                new[] { LightmapBakeType.Baked },
                minRange: 0, maxRange: 12,
                minIntensity: 0, maxIntensity: 10,
                minBounceIntensity: 0, maxBounceIntensity: 15,
                approvedShadowTypes: new LightShadows[] { LightShadows.Soft, LightShadows.Hard });

        protected override LightConfigRule.LightConfig ApprovedAreaLightConfig
            => new LightConfigRule.LightConfig(
                new[] { LightmapBakeType.Baked },
                minRange: 0, maxRange: 12,
                minIntensity: 0, maxIntensity: 10,
                minBounceIntensity: 0, maxBounceIntensity: 15,
                approvedShadowTypes:null,
                castShadows:true,
                new LightConfigRule.AreaLightConfig(
                    minAreaWidth: 0, maxAreaWidth: 10,
                    minAreaHeight: 0, maxAreaHeight: 10,
                    minAreaRadius: 0, maxAreaRadius: 5));

        protected override int AreaLightUsesLimit => 3;

        protected override ValidationLevel MoreAdvancedObjectValidationLevel => ValidationLevel.ALLOW;

        protected override int PickupObjectSyncUsesLimit => 10;

        protected override bool UdonInactiveRuleIsEnabled => false;

        protected override bool ExhibitStructureRuleIsEnabled => true;
        protected override bool ExhibitStructureRuleOnlyDynamic => false;

        protected override bool UdonDynamicObjectParentRuleIsEnabled => true;

        /*public override IRule[] GetRules()
        {
            var rules = base.GetRules().ToList();

            var vrcBoothBoundsRule = rules.Find(rule => rule is VRCBoothBoundsRule);
            rules.Remove(vrcBoothBoundsRule);

            return rules.ToArray();
        }*/
    }
}
#endif