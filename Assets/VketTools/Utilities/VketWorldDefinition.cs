using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace VketTools.Utilities
{
    [CreateAssetMenu]
    public class VketWorldDefinition : ScriptableObject
    {
        public bool IsItem { get; set; }
        
        [Header("ワールド名")]
        [SerializeField]
        private string _worldName;
        
        [Header("ブース入稿設定")]
        [SerializeField]
        private string _validatorRuleSet;
        
        [SerializeField]
        private string _exportSetting;
        
        [SerializeField]
        private int _templateIndex;
        
        [SerializeField]
        private int _setPassMaxSize;
        
        [SerializeField]
        private int _batchesMaxSize;
        
        [SerializeField]
        private float _buildMaxSize;
        
        [SerializeField]
        private string _ruleUrlJa;
        
        [SerializeField]
        private string _ruleUrlEn;

        [Header("アイテム入稿設定")]
        
        [SerializeField] 
        private string _itemValidatorRuleSet;
        
        [SerializeField]
        private string _itemExportSetting;
        
        [SerializeField] 
        private int _itemTemplateIndex;
        
        [SerializeField] 
        private int _itemSetPassMaxSize;
        
        [SerializeField] 
        private int _itemBatchesMaxSize;
        
        [SerializeField] 
        private float _itemBuildMaxSize;
        
        [SerializeField]
        private string _itemRuleUrlJa;
        
        [SerializeField]
        private string _itemRuleUrlEn;

        public string ValidatorRuleSet => IsItem ? _itemValidatorRuleSet : _validatorRuleSet;
        public string ExportSetting => IsItem ? _itemExportSetting : _exportSetting;
        public int TemplateIndex => IsItem ? _itemTemplateIndex : _templateIndex;
        public int SetPassMaxSize => IsItem ? _itemSetPassMaxSize : _setPassMaxSize;
        public int BatchesMaxSize => IsItem? _itemBatchesMaxSize : _batchesMaxSize;
        public float BuildMaxSize => IsItem ? _itemBuildMaxSize : _buildMaxSize;
        public string RuleUrlJa => IsItem ? _itemRuleUrlJa : _ruleUrlJa;
        public string RuleUrlEn => IsItem ? _itemRuleUrlEn : _ruleUrlEn;
        public string WorldName => _worldName;
    }

    /// <summary>
    /// Inspectorから変更時になぜか反映されないので保存ボタンを設置
    /// </summary>
    [CustomEditor(typeof(VketWorldDefinition))]
    public class VketWorldDefinitionInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Save"))
            {
                EditorUtility.SetDirty(target);
                AssetDatabase.Refresh();
                AssetDatabase.SaveAssets();
            }
        }
    }
}
