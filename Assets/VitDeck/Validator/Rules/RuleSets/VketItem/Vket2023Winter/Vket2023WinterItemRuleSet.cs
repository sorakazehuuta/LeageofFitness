#if VRC_SDK_VRCSDK3
using UnityEngine;

namespace VitDeck.Validator.RuleSets
{
    public class Vket2023WinterItemRuleSet : VketItemRuleSetBase
    {
        public Vket2023WinterItemRuleSet() : base(new VketItemOfficialAssetData())
        {
        }
        public override string RuleSetName => "Vket2023Winter - Item";

        protected override long FolderSizeLimit => 20 * MegaByte;

        protected override Vector3 BoothSizeLimit => new Vector3(1, 2, 1);

        protected override int MaterialUsesLimit => 0;

        // public override IRule[] GetRules()
        // {
        //     var rules = base.GetRules().ToList();
        //     // rules.Add() 等行うならここで操作する。今回このルールセットでは追加がない
        //     return rules.ToArray();
        // }
    }
}
#endif