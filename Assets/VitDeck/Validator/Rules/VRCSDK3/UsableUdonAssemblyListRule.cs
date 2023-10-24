#if VRC_SDK_VRCSDK3
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VitDeck.Language;
using VRC.Udon;

namespace VitDeck.Validator
{
    /// <summary>
    /// UdonAssemblyの中で使用禁止処理の有無を検証する。
    /// </summary>
    /// <remarks>
    /// 複数のアセンブリリストを持ち、リストの設定に応じて許可/許否/要申請を決定します。
    /// また、プレハブをGUIDで与えることで、そのプレハブに元から追加してあるコンポーネントを許可されているものとして無視します。
    /// UsableComponentListRule と同じ使い方です。
    /// </remarks>
    internal class UsableUdonAssemblyListRule : BaseUdonBehaviourRule
    {
        private readonly UdonAssemblyReference[] _references;

        private readonly ValidationLevel _unregisteredAssemblyValidationLevel;

        private readonly HashSet<string> _ignoreUdonPrograms;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="name">ルール名</param>
        /// <param name="references">アセンブリリスト</param>
        /// <param name="ignorePrefabGUIDs">例外Prefabのリスト</param>
        /// <param name="unregisteredAssembly">リストにないアセンブリの扱い</param>
        public UsableUdonAssemblyListRule(string name,
            UdonAssemblyReference[] references,
            string[] ignoreUdonProgramGUIDs = null,
            ValidationLevel unregisteredAssembly = ValidationLevel.ALLOW)
            : base(name)
        {
            this._references = references ?? new UdonAssemblyReference[] { };
            if (ignoreUdonProgramGUIDs == null)
            {
                ignoreUdonProgramGUIDs = new string[0];
            }
            _ignoreUdonPrograms = new HashSet<string>(ignoreUdonProgramGUIDs);
            _unregisteredAssemblyValidationLevel = unregisteredAssembly;
        }

        protected override void ComponentLogic(UdonBehaviour component)
        {
            bool isIgnorePrefabInstance = IsIgnoredUdonBehaviour(component);
            var isPrefabComponent = !PrefabUtility.IsAddedComponentOverride(component);
            if (isIgnorePrefabInstance && isPrefabComponent) return;
            // ProgramSource が null の場合はスルー
            if (component.programSource == null) return;
            
            // UdonProgramName
            var programName = component.programSource.name;

            // コード
            var code = GetDisassembleCode(component);

            // 探索
            bool found = false;
            foreach (var reference in _references)
            {
                if (reference != null && reference.Exists(code))
                {
                    found = true;
                    AddAssemblyIssue(reference.name, component.gameObject, programName, reference.level);
                }
            }

            if (!found)
            {
                AddAssemblyIssue(LocalizedMessage.Get("UsableUdonAssemblyListRule.DefaultComponentGroupName"), component.gameObject, programName, _unregisteredAssemblyValidationLevel);
            }
        }

        private bool IsIgnoredUdonBehaviour(UdonBehaviour udonBehaviour)
        {
            ScriptableObject program = udonBehaviour.programSource;
            if (program == null) {
                program = new SerializedObject(udonBehaviour)
                    .FindProperty("serializedProgramAsset")
                    .objectReferenceValue as AbstractSerializedUdonProgramAsset;
            }
            var path = AssetDatabase.GetAssetPath(program);
            var guid = AssetDatabase.AssetPathToGUID(path);
            
            return _ignoreUdonPrograms.Contains(guid);
        }

        private void AddAssemblyIssue(string objectName, GameObject obj, string assemblyName, ValidationLevel level)
        {
            switch (level)
            {
                case ValidationLevel.ALLOW:
                    break;
                case ValidationLevel.DISALLOW:
                    var message = LocalizedMessage.Get("UsableUdonAssemblyListRule.Disallow", assemblyName, objectName);
                    AddIssue(new Issue(obj, IssueLevel.Error, message, string.Empty, string.Empty));
                    break;
            }
        }
    }
}
#endif
