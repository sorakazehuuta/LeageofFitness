using UnityEditor;
using VitDeck.Main;
using VketTools.Language;

namespace VketTools.Utilities
{
    public static class UpdateUtility
    {
        private static string releaseUrl = $"https://vket-tools-api.an.r.appspot.com/api/get/version";

        // 初回インポート時(VKET_TOOLS未定義時)にはチェックしないようにする
#if VKET_TOOLS
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            if (!Hiding.HidingUtil.DebugMode)
            {
                UpdateCheck();

                // VRCSDK3はPostProcessing 3.0.3 以降が既定で入っているのでチェックしない（既存のインストールが壊れる）
#if VRC_SDK_VRCSDK2
                    AssetUtility.CheckPPSV2();
#endif
            }
        }
#endif

        public static bool UpdateCheck()
        {
            VersionInfo versionInfo = AssetUtility.VersionInfoData;
            if (versionInfo == null)
            {
                return false;
            }

            JsonReleaseInfo.FetchInfo($"{releaseUrl}?event_version={AssetUtility.VersionInfoData.event_version}&version_type={AssetUtility.VersionInfoData.package_type}");
            string latestVersion = JsonReleaseInfo.GetVersion();

            if (latestVersion != versionInfo.version)
            {
                if (!EditorUtility.DisplayDialog("Update", LocalizedMessage.Get("UpdateUtility.UpdateCheck.Message", latestVersion), LocalizedMessage.Get("Yes"), LocalizedMessage.Get("No")))
                {
                    return false;
                }

                VitDeck.Main.UpdateCheck.UpdatePackage(latestVersion);
                // UpdatePackage() の完了時点ではまだアップデートし終えていないので、後続の処理を止めたいので false を返す
                return false;
            }

            return true;
        }
    }
}