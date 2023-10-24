
using System.Diagnostics;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace VketTools.PostProcessor
{
    public class VketToolsPostProcessor : AssetPostprocessor
    {
        private static readonly string VketToolsRootFolderGuid = "8680a0fe8e1365f4698937ac2ef31522";
        private const string PrefixName = "VketTools-";
        private const string DefineName = "VKET_TOOLS";

        public override int GetPostprocessOrder()
        {
            return int.MaxValue;
        }
        
        /// <summary>
        /// Unity起動時orこのファイルのインポート時に初期化
        /// </summary>
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            // 未インポート時のみ実行する
#if !VKET_TOOLS
            AssetDatabase.importPackageCompleted += OnImportPackageCompleted;
#endif
        }
        
        /// <summary>
        /// // パッケージのインポートが完了した時に呼び出される
        /// </summary>
        /// <param name="packageName">パッケージ名</param>
        private static void OnImportPackageCompleted(string packageName)
        {
            if (packageName.Contains(PrefixName))
            {
                AddSymbol(DefineName);
                
                // このタイミングでは言語ファイルが正しく読み込めないので直書き。
                var title = Application.systemLanguage == SystemLanguage.Japanese
                    ? "VketToolsがインポートされました。Unityを再起動しますか？"
                    : "VketTools has been imported. Restart Unity?";
                
                var message = Application.systemLanguage == SystemLanguage.Japanese
                    ? "※初回インポート時にUnityの再起動が必要です。"
                    : "※Unity needs to be restarted when importing for the first time.";

                if (EditorUtility.DisplayDialog(title, message, "OK", "Cancel"))
                {
                    RestartUnity();
                }
            }
        }

        /// <summary>
        /// Unityの再起動
        /// </summary>
        private static void RestartUnity()
        {
            var projectPath = FileUtil.GetProjectRelativePath("");
            Process.Start(EditorApplication.applicationPath, $"-projectPath {projectPath}");
            EditorApplication.Exit(0);
        }
        
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            var vketToolsRootFolderPath = AssetDatabase.GUIDToAssetPath(VketToolsRootFolderGuid);
            foreach (var deletedAssetPath in deletedAssets)
            {
                if (deletedAssetPath == vketToolsRootFolderPath)
                {
                    DeleteSymbol(DefineName);
                    break;
                }
            }
        }

        /// <summary>
        /// シンボルの追加
        /// </summary>
        /// <param name="defineName">シンボル名</param>
        private static void AddSymbol(string defineName)
        {
            var currentDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
            var split = currentDefines.Split(';').ToList();
            if(!split.Contains(defineName))
                split.Add(defineName);
            
            var resultDefines = string.Join(";", split);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, resultDefines);
        }

        /// <summary>
        /// シンボルの削除
        /// </summary>
        /// <param name="defineName">シンボル名</param>
        private static void DeleteSymbol(string defineName)
        {
            var currentDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
            var split = currentDefines.Split(';').ToList();
            if (split.Contains(defineName))
                split.Remove(defineName);
            
            var resultDefines = string.Join(";", split);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, resultDefines);
        }
    }
}