using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VitDeck.Language;

namespace VitDeck.Validator
{
    /// <summary>
    /// Quest会場のテクスチャの圧縮設定に関するルール
    /// <summary>
    public class QuestTextureCompressionRule : BaseRule
    {
        private readonly VketTargetFinder targetFinder;
        public QuestTextureCompressionRule(string name, VketTargetFinder targetFinder) : base(name)
        {
            this.targetFinder = targetFinder;
        }

        protected override void Logic(ValidationTarget target)
        {
            var referenceDictionary = targetFinder.ReferenceDictionary;

            // 入稿フォルダ内および出展物から参照されるすべてのアセットのパス
            var allAssetPaths = target.GetAllAssetPaths();
            var submitDirectory = target.GetBaseFolderPath();

            // 入稿フォルダ内に配置されているファイルのパス
            // GetFilesで取得したパス名にはバックスラッシュが混在するためスラッシュに変換
            var filePathsInSubmitDirectory = Directory
                .GetFiles(submitDirectory, "*", SearchOption.AllDirectories)
                .Select(x => x.Replace("\\", "/"));

            // 入稿フォルダ内のディレクトリのパス
            var folderPathInSubmitDirectory = Directory
                .GetDirectories(submitDirectory, "*", SearchOption.AllDirectories)
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
                // 除外リストに含まれているか確認
                bool isExclusion = false;
                foreach (var exclusionPath in exclusionPathList)
                {
                    if (assetPath.Contains(exclusionPath))
                    {
                        isExclusion = true;
                        break;
                    }
                }
                
                // 除外リストに含まれている場合はチェックしない
                if(isExclusion)
                    continue;
                
                // テクスチャのチェック
                var textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                if (textureImporter != null)
                {
                    // Androidで使用される設定を参照
                    var defaultSettings = textureImporter.GetDefaultPlatformTextureSettings();
                    var androidSettings = textureImporter.GetPlatformTextureSettings("Android");
                    var finalSettings = androidSettings.overridden ? androidSettings : defaultSettings;

                    // テクスチャの圧縮設定が「ASTC 6x6」以外になっていればエラー
                    if (finalSettings.format != TextureImporterFormat.ASTC_6x6)
                    {
                        var message = LocalizedMessage.Get("QuestTextureCompressionRule.ASTC6x6");
                        var solution = LocalizedMessage.Get("QuestTextureCompressionRule.ASTC6x6.Solution");
                        AddIssue(new Issue(textureImporter, IssueLevel.Error, message, solution));
                    }

                    // 2048以上のテクスチャの場合エラー
                    if (finalSettings.maxTextureSize > 2048)
                    {
                        var message = LocalizedMessage.Get("QuestTextureCompressionRule.OverSizeTexture");
                        var solution = LocalizedMessage.Get("QuestTextureCompressionRule.OverSizeTexture.Solution");
                        AddIssue(new Issue(textureImporter, IssueLevel.Error, message, solution));
                    }

                    // テクスチャの縦横解像度が4の倍数でなければエラー
                    var tex = AssetDatabase.LoadAssetAtPath<Texture>(assetPath);
                    if (tex != null)
                    {
                        // TODO: 圧縮後のサイズを基準に計算するようになっているか？
                        var invalidWidth = tex.width % 4 != 0;
                        var invalidHeight = tex.height % 4 != 0;

                        if (invalidWidth || invalidHeight)
                        {
                            var message = LocalizedMessage.Get("QuestTextureCompressionRule.MultipleOf4");
                            var solution = LocalizedMessage.Get("QuestTextureCompressionRule.MultipleOf4.Solution");
                            AddIssue(new Issue(tex, IssueLevel.Error, message, solution));
                        }
                    }
                }
            }
        }
    }
}