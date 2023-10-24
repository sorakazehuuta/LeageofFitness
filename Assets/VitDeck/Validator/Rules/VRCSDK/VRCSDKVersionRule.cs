using System;
using System.IO;
using UnityEngine;
using VitDeck.Language;

namespace VitDeck.Validator
{
    /// <summary>
    /// VRCSDKのバージョンを検出するルール
    /// </summary>
    /// <remarks>
    /// GUIDは可変である可能性があるのでファイルパスをチェックする。
    /// </remarks>
    public class VRCSDKVersionRule : BaseRule
    {
        /* VRCSDKのフォルダGUIDが変わったみたい？
           ファイルパスが変わる可能性とGUIDが変わる可能性ならファイルパスが変わる可能性の方が低い気がするのでパスを直接指定。*/
        // VCC化に伴い仕組みごとファイルパスが変わったので変更
        //const string VRCSDKDependenciesFolderGUID = "23868bd667cf64b479fbd8d1039e2cd2";
        // const string versionFilePath = "Assets/VRCSDK/version.txt";
        const string manifestFilePath = "Packages/com.vrchat.worlds/package.json";

        private VRCSDKVersion targetVersion;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">ルール名</param>
        /// <param name="version">VRCSDKのバージョン</param>
        public VRCSDKVersionRule(string name, VRCSDKVersion version) : base(name)
        {
            targetVersion = version;
        }

        protected override void Logic(ValidationTarget target)
        {
            if (!File.Exists(manifestFilePath))
            {
                AddIssue(new Issue(null,
                    IssueLevel.Error,
                    LocalizedMessage.Get("VRCSDKVersionRule.NotInstalled"),
                    LocalizedMessage.Get("VRCSDKVersionRule.NotInstalled.Solution")
                    ));
                return;
            }

            var manifest = JsonUtility.FromJson<Manifest>(File.ReadAllText(manifestFilePath));
            var currentVersion = new VRCSDKVersion(manifest.version);

            if (currentVersion < targetVersion)
            {
                AddIssue(new Issue(null,
                    IssueLevel.Error,
                    LocalizedMessage.Get("VRCSDKVersionRule.PreviousVersion"),
                    LocalizedMessage.Get("VRCSDKVersionRule.PreviousVersion.Solution", targetVersion.ToReadableString())
                    ));
            }
        }

        [Serializable]
        private class Manifest
        {
            [SerializeField] public string version;
        }
    }
}