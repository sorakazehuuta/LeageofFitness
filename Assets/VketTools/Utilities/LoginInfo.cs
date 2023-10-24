using System.Linq;
using UnityEngine;
using VketTools.Utilities.Networking;

namespace VketTools.Utilities
{
    public class LoginInfo : ScriptableObject
    {
        [SerializeField]
        public Networking.NetworkUtility.AccessTokenProvider.Result authentication;
        [SerializeField]
        public Networking.NetworkUtility.Profile.Result profile;
        [SerializeField]
        public Networking.NetworkUtility.UserData data;
        [SerializeField]
        public Networking.NetworkUtility.MallData mall_data;
        [SerializeField]
        public string vketId;
        [SerializeField]
        public string accessToken;
        [SerializeField]
        public bool vrcLaunchFlag = true;
        [SerializeField]
        public Networking.NetworkUtility.World[] world;
        [SerializeField]
        public Networking.NetworkUtility.Shop[] shop;
        [SerializeField]
        public Networking.NetworkUtility.Genre[] genre;
        [SerializeField]
        public Networking.NetworkUtility.DefaultCubeInfoData dcInfoData;
        [SerializeField]
        public Networking.NetworkUtility.SubmissionDateInfo submissionTerm;
        [SerializeField]
        public int selectedItemNum = 9999;
        [SerializeField]
        public int selectedCircleNum = 9999;
        [SerializeField]
        public int selectedWorldId = 9999;
        [SerializeField]
        public bool errorFlag = false;
        [SerializeField]
        public Texture2D circleIcon;
        [SerializeField]
        public Texture2D[] worldIcon;

        private enum WorldType : ushort
        {
            PC = 0,
            Quest = 1,
        }

        private enum BoothType : ushort
        {
            Booth = 0,
            Item = 1,
        }

        private WorldType GetWorldType(NetworkUtility.World world)
        {
            switch (world.world_type)
            {
                case "pc":
                    return WorldType.PC;
                case "quest":
                    return WorldType.Quest;
                default:
                    return WorldType.PC;
            }
        }
        
        private BoothType GetBoothType(NetworkUtility.EntryWorld world)
        {
            switch (world.booth_type)
            {
                case "booth":
                    return BoothType.Booth;
                case "item":
                    return BoothType.Item;
                default:
                    return BoothType.Booth;
            }
        }
        
        public bool IsQuest(int? worldId = null)
        {
            if (!worldId.HasValue)
                worldId = selectedWorldId;

            var validWorld = GetWorld(worldId.Value);
            if (validWorld == null)
                return false;

            var type = GetWorldType(validWorld);
            return type == WorldType.Quest;
        }

        public bool IsItem(int? worldId = null)
        {
            if (!worldId.HasValue)
                worldId = selectedWorldId;
            
            var validWorld = GetEntryWorld(worldId.Value);
            if (validWorld == null)
                return false;

            var type = GetBoothType(validWorld);
            return type == BoothType.Item;
        }

        public string GetWorldName(int? worldId = null)
        {
            if (!worldId.HasValue)
                worldId = selectedWorldId;

            if (Application.systemLanguage == SystemLanguage.Japanese)
                return GetWorldNameJpn(worldId.Value);
            else
                return GetWorldNameEng(worldId.Value);
        }

        public string GetWorldNameJpn(int worldId)
        {
            var validWorld = GetWorld(worldId);
            if (validWorld == null)
                return "";

            if (validWorld.concept.name_ja == validWorld.name)
                return validWorld.name;
            
            return $"{validWorld.concept.name_ja} - {validWorld.name}";
        }

        public string GetWorldNameEng(int worldId)
        {
            var validWorld = GetWorld(worldId);
            if (validWorld == null)
                return "";

            if (validWorld.concept.name_en == validWorld.name_en)
                return validWorld.name_en;
            
            return $"{validWorld.concept.name_en} - {validWorld.name_en}";
        }

        Networking.NetworkUtility.World GetWorld(int worldId)
        {
            if (world == null)
                return null;
            
            var validWorld = world.FirstOrDefault(w => w.id == worldId);
            if (validWorld == null)
                return null;

            return validWorld;
        }
        
        NetworkUtility.EntryWorld GetEntryWorld(int worldId)
        {
            if (!IsCircleAvailable())
                return null;
            
            var validWorld = GetCircle().worlds.FirstOrDefault(w => w.id == worldId);
            if (validWorld == null)
                return null;

            return validWorld;
        }
        
        /// <summary>
        /// サークル情報の取得
        /// </summary>
        /// <returns>サークル情報の配列</returns>
        public NetworkUtility.Circle[] GetCirclesOrCompanyCircles()
        {
            if (data.circle != null && data.circle.Length > 0) return data.circle;
            if (data.company != null && data.company.Length > 0) return data.company;
            return null;
        }
        
        /// <summary>
        /// サークル情報が使用可能か
        /// </summary>
        /// <returns>使用可能な場合trueを返す</returns>
        public bool IsCircleAvailable()
        {
            return GetCirclesOrCompanyCircles() != null;
        }

        /// <summary>
        /// 選択中のサークル情報を取得する
        /// </summary>
        /// <returns>選択中のサークル情報</returns>
        public NetworkUtility.Circle GetCircle()
        {
            return GetCirclesOrCompanyCircles()[selectedCircleNum];
        }
    }
}