using System.Collections.Generic;

namespace VitDeck.Validator.RuleSets
{
    public interface IOfficialAssetData
    {
        string[] GUIDs { get; }
        string[] MaterialGUIDs { get; }
        string[] PickupObjectSyncPrefabGUIDs { get; }
        string[] AvatarPedestalPrefabGUIDs { get; }
        string[] VideoPlayerPrefabGUIDs { get; }
        string[] ImageDownloaderPrefabGUIDs { get; }
        string[] StringDownloaderPrefabGUIDs { get; }
        string[] StarshipTreasurePrefabGUIDs { get; }
        string[] UdonBehaviourPrefabGUIDs { get; }
        string[] UdonBehaviourGlobalLinkGUIDs { get; }
        string[] SizeIgnorePrefabGUIDs { get; }
        string[] AudioSourcePrefabGUIDs { get; }
        string[] CanvasPrefabGUIDs { get; }
        string[] PointLightProbeGUIDs { get; }
        string[] VRCSDKForbiddenPrefabGUIDs { get; }
        string[] DisabledCallback { get; }
        Dictionary<string, string> DisabledDirectives { get; }
        
        /// <summary>
        /// 使用可能なシェーダー群
        /// FIXME: 2023/08/18現在 使用されてない
        /// </summary>
        Dictionary<string,string> AllowedShaders { get; }
        
        /// <summary>
        /// 使用不可なシェーダー群
        /// </summary>
        string[] DeniedShaderNames { get; }
    }
}