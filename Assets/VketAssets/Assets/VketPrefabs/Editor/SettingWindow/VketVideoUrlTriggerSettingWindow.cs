using UnityEditor;
using UnityEngine;
using Vket.VketPrefabs.Language;
using VRC.SDKBase;

namespace Vket.VketPrefabs
{
    public class VketVideoUrlTriggerSettingWindow : VketPrefabSettingWindow
    {
        #region 設定用変数
        private VketVideoUrlTrigger _vketVideoUrlTrigger;
        
        private string _videoURL;
        private VketVideoPlayer _vketVideoPlayer;
        private bool _isInteract = true;
        private Transform _interactObjectsRoot;
        private GameObject _interactObject;
        private Collider _collider;
        
        #endregion

        #region const定義
        private const string InteractObjectsRootName = "IntractObjectsRoot";
        #endregion

        protected override void InitWindow()
        {
            // ウィンドウ最小サイズの設定
            minSize = new Vector2(350f, 500f);

            if (_vketPrefabInstance)
            {
                _vketVideoUrlTrigger = _vketPrefabInstance.GetComponent<VketVideoUrlTrigger>();
                _collider = _vketPrefabInstance.GetComponent<Collider>();
            }
            
            if (_vketVideoUrlTrigger)
            {
                var url = _vketVideoUrlTrigger.GetProgramVariable("videoUrl") as VRCUrl;
                _videoURL = url.ToString();
                _vketVideoPlayer = _vketVideoUrlTrigger.GetProgramVariable("vketVideoPlayer") as VketVideoPlayer;
                _isInteract = (bool)_vketVideoUrlTrigger.GetProgramVariable("isInteract");
            }

            if (_isInteract)
            {
                CreateAndLoadInteractObjectsRoot();
            }
        }

        /// <summary>
        /// ObjectsRootの生成
        /// 初期化時にモードがInterectの場合は読み込みをする
        /// </summary>
        private void CreateAndLoadInteractObjectsRoot()
        {
            if (_vketPrefabInstance)
            {
                if (_vketPrefabInstance.childCount == 0)
                {
                    _interactObjectsRoot = new GameObject(InteractObjectsRootName).transform;
                    _interactObjectsRoot.localPosition = Vector3.zero;
                    _interactObjectsRoot.localRotation = Quaternion.identity;
                    _interactObjectsRoot.localScale = Vector3.one;
                    _interactObjectsRoot.parent = _vketPrefabInstance;
                }
                else
                {
                    _interactObjectsRoot = _vketPrefabInstance.Find(InteractObjectsRootName);
                }
            }
            
            if (_interactObjectsRoot && _interactObjectsRoot.childCount != 0)
            {
                _interactObject = _interactObjectsRoot.GetChild(0).gameObject;
            }
        }

        private void OnGUI()
        {
            InitStyle();

            if(!BaseHeader("VketVideoUrlTrigger"))
                return;

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUI.skin.box);
            
            /* "1.ビデオのURLを指定" */
            EditorGUILayout.LabelField(LocalizedMessage.Get("VketVideoUrlTriggerSettingWindow.VideoUrlSetting"), _settingItemStyle);
            EditorGUI.BeginChangeCheck();
            _videoURL = EditorGUILayout.TextField(_videoURL);
            if (EditorGUI.EndChangeCheck() && _vketVideoUrlTrigger)
            {
                _vketVideoUrlTrigger.SetProgramVariable("videoUrl", new VRCUrl(_videoURL));
                PrefabUtility.RecordPrefabInstancePropertyModifications(_vketVideoUrlTrigger);
            }
            
            /* "URLが空の場合はそのままVideoPlayerを再生します。" */
            EditorGUILayout.HelpBox(LocalizedMessage.Get("VketVideoUrlTriggerSettingWindow.VideoUrlSetting.Help"), MessageType.Info);

            GUILayout.Space(3);
            
            /* "2.対象のビデオプレイヤーを指定" */
            EditorGUILayout.LabelField(LocalizedMessage.Get("VketVideoUrlTriggerSettingWindow.VideoPlayerSetting"), _settingItemStyle);
            EditorGUI.BeginChangeCheck();
            _vketVideoPlayer = EditorGUILayout.ObjectField(_vketVideoPlayer, typeof(VketVideoPlayer), true) as VketVideoPlayer;
            if (EditorGUI.EndChangeCheck() && _vketVideoUrlTrigger)
            {
                _vketVideoUrlTrigger.SetProgramVariable("vketVideoPlayer", _vketVideoPlayer);
                PrefabUtility.RecordPrefabInstancePropertyModifications(_vketVideoUrlTrigger);
            }
            
            GUILayout.Space(3);
            
            /* "3.起動方法を選択" */
            EditorGUILayout.LabelField(LocalizedMessage.Get("VketVideoUrlTriggerSettingWindow.StartingMethodSetting"), _settingItemStyle);
            int popupIdx = _isInteract ? 1 : 0;
            EditorGUI.BeginChangeCheck();
            popupIdx = EditorGUILayout.Popup("Mode", popupIdx, new string[] { "On Player Enter", "Interact" });
            if (EditorGUI.EndChangeCheck())
            {
                _isInteract = popupIdx == 1;
                _vketVideoUrlTrigger.SetProgramVariable("isInteract", _isInteract);
                PrefabUtility.RecordPrefabInstancePropertyModifications(_vketVideoUrlTrigger);

                if (_isInteract)
                {
                    // Rootが存在しない場合は生成
                    if(!_interactObjectsRoot)
                        CreateAndLoadInteractObjectsRoot();
                }
                else
                {
                    DestroyImmediate(_interactObjectsRoot.gameObject);
                    _interactObjectsRoot = null;
                    _interactObject = null;
                }
            }
            
            /* "[Interact] -> Useした場合
             [On Player Enter] -> プレイヤーが指定範囲に入った場合" */
            EditorGUILayout.HelpBox(LocalizedMessage.Get("VketVideoUrlTriggerSettingWindow.StartingMethodSetting.Help"), MessageType.Info);
            
            GUILayout.Space(3);
            
            if (_isInteract)
            {
                /* "4.Useの見た目設定" */
                EditorGUILayout.LabelField(LocalizedMessage.Get("VketVideoUrlTriggerSettingWindow.VisualSetting"), _settingItemStyle);
                EditorGUI.BeginChangeCheck();
                _interactObject = EditorGUILayout.ObjectField(_interactObject, typeof(GameObject), false) as GameObject;
                if (EditorGUI.EndChangeCheck())
                {
                    if (_interactObject && _vketPrefabInstance)
                    {
                        // シーン上に存在しない場合は複製
                        if (_interactObject.scene.name == null)
                        {
                            var copy = Instantiate(_interactObject);
                            copy.name = _interactObject.name;
                            _interactObject = copy;
                        }
                        
                        // Rootの子として設定
                        _interactObject.transform.parent = _interactObjectsRoot;
                        _interactObject.transform.localPosition = Vector3.zero;
                        _interactObject.transform.localRotation = Quaternion.identity;
                    }
                }
                
                /* "見た目となるオブジェクトを設定します。
                 スイッチなどのプレハブを指定してください。" */
                EditorGUILayout.HelpBox(LocalizedMessage.Get("VketVideoUrlTriggerSettingWindow.VisualSetting.Help"), MessageType.Info);
                
                GUILayout.Space(3);
                
                /* "5.Use可能な範囲の設定" */
                EditorGUILayout.LabelField(LocalizedMessage.Get("VketVideoUrlTriggerSettingWindow.UseRangeSetting"), _settingItemStyle);
                /* "Colliderを選択" */
                if (GUILayout.Button(LocalizedMessage.Get("VketVideoUrlTriggerSettingWindow.UseRangeSetting.SelectColliderButton")))
                {
                    Selection.activeTransform = _collider.transform;
                    EditorGUIUtility.PingObject(_collider);
                }
                /* "BoxColliderの範囲で指定します。
                 Inspectorから調整してください。" */
                EditorGUILayout.HelpBox(LocalizedMessage.Get("VketVideoUrlTriggerSettingWindow.UseRangeSetting.Help"), MessageType.Info);
            }
            else
            {
                /* "4.プレイヤーが入ったときに再生する範囲の設定" */
                EditorGUILayout.LabelField(LocalizedMessage.Get("VketVideoUrlTriggerSettingWindow.PlayerEnterSetting"), _settingItemStyle);
                
                /* "Colliderを選択" */
                if (GUILayout.Button(LocalizedMessage.Get("VketVideoUrlTriggerSettingWindow.PlayerEnterSetting.SelectColliderButton")))
                {
                    Selection.activeTransform = _collider.transform;
                    EditorGUIUtility.PingObject(_collider);
                }
                
                /* "BoxColliderの範囲に入ったときに再生されます。
                 Inspectorから調整してください。" */
                EditorGUILayout.HelpBox(LocalizedMessage.Get("VketVideoUrlTriggerSettingWindow.PlayerEnterSetting.Help"), MessageType.Info);
            }

            EditorGUILayout.EndScrollView();
            
            BaseFooter("VketVideoUrlTrigger");
        }
    }
}
