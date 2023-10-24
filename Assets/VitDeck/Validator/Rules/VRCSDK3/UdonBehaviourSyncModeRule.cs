#if VRC_SDK_VRCSDK3
using System;
using System.Linq;
using UnityEditor;
using VitDeck.Language;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace VitDeck.Validator
{
    /// <summary>
    /// UdonBehaviourSyncMode.Continuous をチェックするルール
    /// </summary>
    internal class UdonBehaviourSyncModeRule : BaseUdonBehaviourRule
    {
        public UdonBehaviourSyncModeRule(string name) : base(name)
        {
        }

        protected override void ComponentLogic(UdonBehaviour udonBehaviour)
        {
            // プログラムアセットを取得する
            var program = GetUdonProgram(udonBehaviour);
            // SyncMetadataTableが無ければスキップ
            if (program?.SyncMetadataTable == null) return;
            // UdonSyncMetadata取得
            var syncs = program.SyncMetadataTable.GetAllSyncMetadata();
            // 同期変数がないならスキップ
            if (!syncs.Any()) return;

            // SyncType.Continuous (≒ UdonBehaviourSyncMode.Continuous)
            if (udonBehaviour.SyncMethod == Networking.SyncType.Continuous)
            {
                AddIssue(new Issue(
                    udonBehaviour,
                    IssueLevel.Error,
                    LocalizedMessage.Get("UdonBehaviourSyncModeRule.InvalidType"),
                    LocalizedMessage.Get("UdonBehaviourSyncModeRule.InvalidType.Solution")));
            }
        }
    }
}
#endif
