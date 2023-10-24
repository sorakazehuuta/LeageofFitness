using System.Collections.Generic;
using System.Linq;

namespace VitDeck.Validator.RuleSets
{
    public class Vket2023WinterOfficialAssetData : VketUdonOfficialAssetData
    {
        public override string[] GetGUIDs()
        {
            var guids = base.GetGUIDs().ToList();
            var addGuids = new string[]
            {
            };
            guids.AddRange(addGuids);
            return guids.ToArray();
        }

        public override Dictionary<string, string> GetAllowedShaders()
        {
            return new Dictionary<string, string>()
            {
            };
        }

        public override string[] GetUdonBehaviourPrefabGUIDs()
        {
            var guids = base.GetUdonBehaviourPrefabGUIDs().ToList();
            var addGuids = new string[]
            {
            };
            guids.AddRange(addGuids);
            return guids.ToArray();
        }

        public override string[] GetSizeIgnorePrefabGUIDs()
        {
            var guids = base.GetSizeIgnorePrefabGUIDs().ToList();
            var addGuids = new string[]
            {
            };
            guids.AddRange(addGuids);
            return guids.ToArray();
        }

        public override string[] GetUdonBehaviourGlobalLinkGUIDs()
        {
            var guids = base.GetUdonBehaviourGlobalLinkGUIDs().ToList();
            var addGuids = new string[]
            {
            };
            guids.AddRange(addGuids);
            return guids.ToArray();
        }
    }
}