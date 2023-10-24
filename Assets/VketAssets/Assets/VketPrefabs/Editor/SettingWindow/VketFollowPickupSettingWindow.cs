
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Vket.VketPrefabs.Language;

namespace Vket.VketPrefabs
{
    public class VketFollowPickupSettingWindow : VketPrefabSettingWindow
    {
        #region 設定用変数
        private VketFollowPickup _vketFollowPickup;
        private Transform _interactObjectsRoot;
        private GameObject _interactObject;
        
        private float _attachRange;
        private int _targetBone;

        private CapsuleCollider _collider;
        #endregion
        
        #region const定義
        private const string InteractObjectsRootName = "ModeController";
        #endregion

        #region readonly定義

        private readonly string[] _boneNames = new string[]
        {
            "Head", "Neck", "Chest", "Spine", "Hips", "Shoulder", "UpperArm", "LowerArm", "Hand", "UpperLeg",
            "LowerLeg", "Foot"
        };

        #endregion

        protected override void InitWindow()
        {
            // ウィンドウ最小サイズの設定
            minSize = new Vector2(350f, 500f);

            if (_vketPrefabInstance)
            {
                _vketFollowPickup = _vketPrefabInstance.GetComponent<VketFollowPickup>();
                _collider = _vketPrefabInstance.GetComponent<CapsuleCollider>();
                _interactObjectsRoot = _vketPrefabInstance.Find(InteractObjectsRootName);
            }
            
            if (_vketFollowPickup)
            {
                _attachRange = (float)_vketFollowPickup.GetProgramVariable("attachRange");
                _targetBone = (int)_vketFollowPickup.GetProgramVariable("targetBone");
            }
            
            // 見た目のオブジェクトを取得
            if (_interactObjectsRoot && _interactObjectsRoot.childCount != 0)
            {
                _interactObject = _interactObjectsRoot.GetChild(0).gameObject;
            }
        }

        private void OnGUI()
        {
            InitStyle();
            
            if(!BaseHeader("VketFollowPickup"))
                return;
            
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUI.skin.box);
            
            /* "1.追従するボーンの設定" */
            EditorGUILayout.LabelField(LocalizedMessage.Get("VketFollowPickupSettingWindow.FollowBoneSetting"), _settingItemStyle);
            
            EditorGUI.BeginChangeCheck();
            _targetBone = EditorGUILayout.Popup(_targetBone, _boneNames);
            if (EditorGUI.EndChangeCheck())
            {
                _vketFollowPickup.SetProgramVariable("targetBone", _targetBone);
                PrefabUtility.RecordPrefabInstancePropertyModifications(_vketFollowPickup);
            }
            
            GUILayout.Space(3);
            
            /* "2.アタッチ範囲の設定" */
            EditorGUILayout.LabelField(LocalizedMessage.Get("VketFollowPickupSettingWindow.AttachRangeSetting"), _settingItemStyle);
                
            EditorGUI.BeginChangeCheck();
            _attachRange = EditorGUILayout.FloatField(_attachRange);
            if (EditorGUI.EndChangeCheck())
            {
                if (_attachRange < 0.01f)
                    _attachRange = 0.01f;
                    
                _vketFollowPickup.SetProgramVariable("attachRange", _attachRange);
                PrefabUtility.RecordPrefabInstancePropertyModifications(_vketFollowPickup);
            }
            
            /* "Useした時、アタッチ範囲内に指定のBoneがあれば追従を開始します。" */
            EditorGUILayout.HelpBox(LocalizedMessage.Get("VketFollowPickupSettingWindow.AttachRangeSetting.Help"), MessageType.Info);

            GUILayout.Space(3);
            
            /* "3.見た目にする3Dモデルの設定" */
            EditorGUILayout.LabelField(LocalizedMessage.Get("VketFollowPickupSettingWindow.VisualSetting"), _settingItemStyle);
            EditorGUI.BeginChangeCheck();
            var interactObject = EditorGUILayout.ObjectField(_interactObject, typeof(GameObject), true) as GameObject;
            if (EditorGUI.EndChangeCheck())
            {
                if (_interactObject)
                    _interactObject.transform.parent = null;

                _interactObject = interactObject;
                
                if (_interactObject && _vketPrefabInstance)
                {
                    // シーン上に存在しない場合は複製
                    if (_interactObject.scene.name == null)
                    {
                        var copy = Instantiate(_interactObject);
                        copy.name = _interactObject.name;
                        _interactObject = copy;
                        Undo.RegisterCreatedObjectUndo(_interactObject, "Create Prefab");
                    }
                    
                    // Rootの子として設定
                    _interactObject.transform.parent = _interactObjectsRoot;
                    _interactObject.transform.localPosition = Vector3.zero;
                    _interactObject.transform.localRotation = Quaternion.identity;
                }
            }

            /* "見た目となる3Dオブジェクトを設定します。" */
            EditorGUILayout.HelpBox(LocalizedMessage.Get("VketFollowPickupSettingWindow.VisualSetting.Help"), MessageType.Info);

            GUILayout.Space(3);

            /* "4.コライダーのサイズ調整" */
            EditorGUILayout.LabelField(LocalizedMessage.Get("VketFollowPickupSettingWindow.ColliderScaleSetting"), _settingItemStyle);
            if (GUILayout.Button("Setup Collider"))
            {
                AdjustCapsuleCollider();
            }

            /* "見た目となる3Dオブジェクトの大きさに自動で設定します。
             少し小さめに設定されるため、細かい調整はInspectorのCapcelColliderのサイズ変更を手動で行ってください。" */
            EditorGUILayout.HelpBox(LocalizedMessage.Get("VketFollowPickupSettingWindow.ColliderScaleSetting.Help"), MessageType.Info);

            GUILayout.Space(3);

            EditorGUILayout.EndScrollView();
            
            BaseFooter("VketFollowPickup", _interactObject);
        }
        
        private void AdjustCapsuleCollider()
        {
            if (_collider == null)
            {
                Debug.LogWarning("CapsuleCollider not found.");
                return;
            }

            if (!_interactObjectsRoot)
            {
                Debug.Log($"[{InteractObjectsRootName}] Object not found.");
                return;
            }
            
            var renderers = _interactObjectsRoot.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0)
            {
                Debug.Log("Renderer not found.");
                return;
            }

            Bounds totalBounds = CalculateBounds(renderers);

            Vector3 pos = _vketFollowPickup.transform.position;
            Vector3 scale = _vketFollowPickup.transform.lossyScale;

            Vector3 localCenter = new Vector3(
                (totalBounds.center.x - pos.x) / scale.x,
                (totalBounds.center.y - pos.y) / scale.y,
                (totalBounds.center.z - pos.z) / scale.z
            );
            Vector3 localSize = new Vector3(
                totalBounds.size.x / scale.x,
                totalBounds.size.y / scale.y,
                totalBounds.size.z / scale.z
            );

            Undo.RecordObject(_collider, "AdjustCollider");
            _collider.center = localCenter;
            _collider.height = localSize.y;
            _collider.radius = localSize.x <= localSize.y ? localSize.x * 0.5f : localSize.y * 0.5f;

            Debug.Log("Setup Completed.");
        }

        private Bounds CalculateBounds(Renderer[] renderers)
        {
            Bounds bounds = new Bounds();

            foreach (var renderer in renderers)
            {
                Vector3 min = renderer.bounds.center - (renderer.bounds.size * 0.5f);
                Vector3 max = renderer.bounds.center + (renderer.bounds.size * 0.5f);

                if (bounds.size == Vector3.zero)
                    bounds = new Bounds(renderer.bounds.center, Vector3.zero);

                bounds.Encapsulate(min);
                bounds.Encapsulate(max);
            }

            return bounds;
        }
        
        private AnimatorOverrideController CreateOverrideController(AnimatorOverrideController overrideControllerBase)
        {
            var path = EditorUtility.SaveFilePanelInProject("Save new override controller", "NewOverrideController.overrideController", "overrideController", "");
            if (!string.IsNullOrEmpty(path))
            {
                if (overrideControllerBase == null)
                {
                    Debug.Log("Error! Not found override controller empty");
                    return null;
                }

                var newController = new AnimatorOverrideController(overrideControllerBase);
                AssetDatabase.CreateAsset(newController, path);
                AssetDatabase.Refresh();
                Undo.RegisterCreatedObjectUndo(newController, "Create override controller");

                return newController;
            }

            return null;
        }
    }
}
