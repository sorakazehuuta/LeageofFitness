using UnityEditor;
using VitDeck.Exporter.Addons.VRCSDK3;

namespace VketTools.Main
{
    public static class MoveSerializeUdonPrograms
    {
#if VRC_SDK_VRCSDK3
        [MenuItem("VketTools/MoveSerializedUdonPrograms")]
        public static void MoveSerializedUdonPrograms()
        {
            LinkedUdonManager.MoveSerializedUdonPrograms();
        }
#endif
    }
}