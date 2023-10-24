using UnityEngine;

namespace VketTools.Utilities
{
    public class EditorPlayInfo : ScriptableObject
    {
        [SerializeField]
        public bool isVketEditorPlay = false;
        [SerializeField]
        public bool isSetPassCheckOnly = false;
        [SerializeField]
        public bool buildSizeSuccessFlag = false;
        [SerializeField]
        public bool setPassSuccessFlag = false;
        [SerializeField]
        public bool setPassFailedFlag = false;
        [SerializeField]
        public bool ssSuccessFlag = false;
        
        /// <summary>
        /// SetPassの一時避難先
        /// </summary>
        [SerializeField]
        public int setPassCalls = 0;
        
        /// <summary>
        /// Batchesの一時避難先
        /// </summary>
        [SerializeField]
        public int batches = 0;
        
        /// <summary>
        /// スクショのパス避難先
        /// </summary>
        [SerializeField]
        public string ssPath;
        [SerializeField]
        public bool clientSimEnabled = true;

        /// <summary>
        /// 確認以外でPlayModeに入ったときに注意ダイアログを表示するか
        /// 「次回以降表示しない」を選択した場合にtrueになる。
        /// </summary>
        [SerializeField] public bool showPlayModeNotification = true;
    }
}