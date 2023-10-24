using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace VketTools.Utilities
{
    public static class QuestTextureConverter
    {
        public static void ConvertAstc(string submitDirectory)
        {
            // 入稿フォルダ内に配置されているファイルのパス
            // GetFilesで取得したパス名にはバックスラッシュが混在するためスラッシュに変換
            var filePathsInSubmitDirectory = Directory
                .GetFiles(submitDirectory, "*", SearchOption.AllDirectories)
                .Select(x => x.Replace("\\", "/"));
            
            // 入稿ID
            var exhibitorID = Path.GetFileName(submitDirectory);
            // LightingがBakeされるフォルダ
            var sceneLightingBakeFolderPath = string.Format("Assets/{0}/{0}", exhibitorID);
            
            // Textureチェックの除外対象
            var lightMapFilePaths = Directory.GetFiles(sceneLightingBakeFolderPath, "Lightmap*.exr", SearchOption.AllDirectories).Select(x => x.Replace("\\", "/")).ToList();;
            var reflectionProbeFilePaths = Directory.GetFiles(sceneLightingBakeFolderPath, "ReflectionProbe*.exr", SearchOption.AllDirectories).Select(x => x.Replace("\\", "/")).ToList();
            // Textureチェックの除外リストを作成
            List<string> exclusionPathList = new List<string>(lightMapFilePaths.Count() + reflectionProbeFilePaths.Count());
            exclusionPathList.AddRange(lightMapFilePaths);
            exclusionPathList.AddRange(reflectionProbeFilePaths);
            
            // 入稿フォルダ内のテクスチャ確認
            foreach (var assetPath in filePathsInSubmitDirectory)
            {
                // 除外リストに含まれている場合はチェックしない
                if (exclusionPathList.Contains(assetPath))
                {
                    continue;
                }

                // テクスチャの変換
                var textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                if (textureImporter != null)
                {
                    ConvertAstc(textureImporter);
                }
            }
        }
        
        // テスト用
        //[MenuItem("Assets/ConvertASTC", false)]
        static void ConvertAstc()
        {
            var texture = Selection.activeObject as Texture;
            if (texture)
            {
                var textureImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(texture)) as TextureImporter;
                if (textureImporter != null)
                {
                    ConvertAstc(textureImporter);
                }
            }
        }

        /// <summary>
        /// ASTC_6x6に変換
        /// </summary>
        /// <param name="importer">対象のTextureのTextureImporterを指定</param>
        static void ConvertAstc(TextureImporter importer)
        {
            var androidSettings = importer.GetPlatformTextureSettings("Android");
            if (!androidSettings.overridden || androidSettings.format != TextureImporterFormat.ASTC_6x6)
            {
                androidSettings.overridden = true;
                androidSettings.format = TextureImporterFormat.ASTC_6x6;
                importer.SetPlatformTextureSettings(androidSettings);
            }
        }
    }
}
