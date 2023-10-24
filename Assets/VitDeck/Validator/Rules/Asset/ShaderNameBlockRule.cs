﻿using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VitDeck.Language;

namespace VitDeck.Validator
{
    /// <summary>
    /// Unityのビルトインシェーダーと同名のシェーダーの入稿を禁止するルール
    /// <summary>
    public class ShaderNameBlockRule : BaseRule
    {
        private HashSet<string> _blockList;

        public ShaderNameBlockRule(string name, string[] blockList) : base(name)
        {
            _blockList = new HashSet<string>(blockList);
        }

        protected override void Logic(ValidationTarget target)
        {
            var shaders = AssetDatabase.FindAssets("t:Shader", new[] {target.GetBaseFolderPath()})
                .Select(guid => AssetDatabase.LoadAssetAtPath<Shader>(AssetDatabase.GUIDToAssetPath(guid)));

            foreach (var shader in shaders)
            {
                if (_blockList.Contains(shader.name))
                {
                    AddIssue(new Issue(shader, IssueLevel.Error, LocalizedMessage.Get("ShaderNameBlockRule"), LocalizedMessage.Get("ShaderNameBlockRule.Solution")));
                }
            }
        }
    }
}