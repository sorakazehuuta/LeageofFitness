using System;
using UnityEditor;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using VitDeck.Language;

namespace VitDeck.Validator
{
	/// <summary>
	/// 配布物以外のすべてのオブジェクトが入稿フォルダ内にあるか検出するルール
	/// <summary>
    public class TextureAndMaterialInSubmitFolderRule : BaseRule
    {
        // 入稿フォルダに含めることを許可されていないGUID
        private readonly HashSet<string> unauthorizedIDSet;
        private readonly VketTargetFinder targetFinder;
        public TextureAndMaterialInSubmitFolderRule(string name, string[] guids, VketTargetFinder targetFinder) : base(name)
        {
            unauthorizedIDSet = new HashSet<string>(guids);
            this.targetFinder = targetFinder;
        }

        protected override void Logic(ValidationTarget target)
        {
            var referenceDictionary = targetFinder.ReferenceDictionary;
            
            // 除外する配布物アセットのパス
            var excludePaths = unauthorizedIDSet
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid));

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

            foreach (var assetPath in allAssetPaths)
            {
                if (assetPath.Contains("Lightmap"))
                    continue;
                if (assetPath.Contains("ReflectionProbe"))
                    continue;

                // 運営からの配布物を検証対象から除外
                if (excludePaths.Contains(assetPath))
                    continue;

                var tex = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Texture2D));
                var mat = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Material));

                if (tex == null && mat == null)
                    continue;

                if (tex != null && assetPath.StartsWith(submitDirectory + "/Textures"))
                    continue;

                if (mat != null && assetPath.StartsWith(submitDirectory + "/Materials"))
                    continue;

                // エラー対象である事が確定したためIssue発行
                var targetAsset = AssetDatabase.LoadMainAssetAtPath(assetPath);
                
                var solution = LocalizedMessage.Get("TextureAndMaterialInSubmitFolderRule.Solution");

                AddIssue(new Issue(targetAsset, IssueLevel.Error, solution));
            }
        }
    }
}