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
    public class UdonGraphLimitRule : BaseRule
    {
        private readonly VketTargetFinder targetFinder;
        public UdonGraphLimitRule(string name ,VketTargetFinder targetFinder) : base(name)
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

            foreach (var assetPath in allAssetPaths)
            {
                VRC.Udon.Editor.ProgramSources.UdonGraphProgram.UdonGraphProgramAsset graph = (VRC.Udon.Editor.ProgramSources.UdonGraphProgram.UdonGraphProgramAsset)AssetDatabase.LoadAssetAtPath(assetPath, typeof(VRC.Udon.Editor.ProgramSources.UdonGraphProgram.UdonGraphProgramAsset));

                if(graph != null)
                {
                    var solution = LocalizedMessage.Get("UdonGraphLimitRule.Solution");

                    AddIssue(new Issue(null, IssueLevel.Error, solution));
                }
            }
        }
    }
}