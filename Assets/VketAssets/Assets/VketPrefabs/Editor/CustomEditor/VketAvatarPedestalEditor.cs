
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Components;

namespace Vket.VketPrefabs
{
    [CustomEditor(typeof(VketAvatarPedestal))]
    public class VketAvatarPedestalEditor : Editor
    {
        VketAvatarPedestal vketAvatarPedestal;
        private Vector2 scrollPosition;
        private UnityEngine.UI.Image image;

        private string[] pedestalType =
        {
            "  Vket Avatar Pedestal Default",
            "  Vket Avatar Pedestal 2D",
            "  Vket Avatar Pedestal 3D"
        };

        private string[] summarys =
        {
            "** ReadMe **\n" +
            "- 確認ウィンドウを表示するアバターペデスタルです\n" +
            "- \"Blueprint Id\"にアバターのblueprintIdを設定してください\n" +
            "- 大きさを変えたいときは\"VketAvatarPedestal_Default\"オブジェクトのScaleを調整してください\n" +
            "- This avatar pedestal is equipped with a confirmation window.\n" +
            "- Set the blueprintId of the avatar on \"Blueprint Id\"\n" +
            "- If you want to change the size, adjust the Scale of \"VketAvatarPedestal_Default\" object.",
            "** ReadMe **\n" +
            "- 確認ウィンドウを表示するアバターペデスタルです。見た目に画像を使用できます\n" +
            "- \"Blueprint Id\"にアバターのblueprintIdを設定してください\n" +
            "- \"Source Image\"を変更することで使用画像を変更できます\n" +
            "- 大きさを変えたいときは\"VketAvatarPedestal_2D\"オブジェクトのScaleを調整してください\n" +
            "- This avatar pedestal is equipped a confirmation window where you can use an image as appearance.\n" +
            "- Set the blueprintId of the avatar on \"Blueprint Id\"\n" +
            "- Change \"Source Image\" to choose the image file.",
            "** ReadMe **\n" +
            "- 確認ウィンドウを表示するアバターペデスタルです。見た目に3Dモデルを使用できます\n" +
            "- \"Blueprint Id\"にアバターのblueprintIdを設定してください\n" +
            "- \"Visual\"オブジェクトの子に見た目に使用したいオブジェクトを追加してください\n" +
            "  その後見た目に合うようにCapsuleColliderを調整します\n" +
            "　\"Setup Collider\"ボタンを押すとオブジェクトに合わせてCapsuleColliderが自動調整されます\n" +
            "  また、追加したオブジェクトのコライダーは削除してください\n" +
            "- This avatar pedestal is equipped with a confirmation window where you can use a 3D model as appearance.\n" +
            "- Set the blueprintId of the avatar on \"Blueprint Id\"\n" +
            "- Add the object for pedestal appearance as a child of \"Visual\" object.\n" +
            "  Then, adjust CapsuleCollider to match the appearance.\n" +
            "  You may press \"Setup Collider\" button to automatically adjust CapsuleCollider to fit the object.\n" +
            "  Also, please delete the collider of the object added for pedestal appearance."
        };

        private int[] summaryHeights = { 67, 95, 108 };

        private void OnEnable()
        {
            if (vketAvatarPedestal == null)
                vketAvatarPedestal = target as VketAvatarPedestal;
        }

        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            serializedObject.Update();
            int type = serializedObject.FindProperty("type").intValue;
            VRCAvatarPedestal avatarPedestal =
                (VRCAvatarPedestal)serializedObject.FindProperty("avatarPedestal").objectReferenceValue;

            // Draw Title and Summary
            var style = new GUIStyle();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField(pedestalType[type], new GUIStyle(EditorStyles.boldLabel));
            EditorGUILayout.EndVertical();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(summaryHeights[type]));
            EditorGUILayout.TextArea(summarys[type]);
            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();

            // Draw Field of Blueprint Id
            EditorGUI.BeginChangeCheck();
            var blueprintId = EditorGUILayout.TextField("Blueprint Id", avatarPedestal.blueprintId);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(avatarPedestal, "Modify blueprint id");
                avatarPedestal.blueprintId = blueprintId;
            }

            // Draw Field of Source Image
            if (type == 1)
            {
                if (image == null)
                    image = vketAvatarPedestal.transform?.Find("Canvas")?.Find("Image")
                        .GetComponent<UnityEngine.UI.Image>();

                if (image != null)
                {
                    EditorGUI.BeginChangeCheck();
                    var sourceImage =
                        (Sprite)EditorGUILayout.ObjectField("Source Image", image.sprite, typeof(Sprite), false);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(image, "Modify source image");
                        image.sprite = sourceImage;
                        image.enabled = false;
                        image.enabled = true;
                    }
                }
                else
                {
                    Debug.Log("Not Found \"VketAvatarPedestal2D/Canvas/Image\"");
                }
            }
            else if (type == 2)
            {
                var property = serializedObject.FindProperty("autoAdjustPosition");
                EditorGUI.BeginChangeCheck();
                bool autoAdjust = EditorGUILayout.Toggle(new GUIContent("Auto-adjust", "Auto-adjust position of the pop-up dialog (Recommended)"), property.boolValue);
                if (EditorGUI.EndChangeCheck())
                {
                    property.boolValue = autoAdjust;
                    serializedObject.ApplyModifiedProperties();
                }

                EditorGUILayout.Space();
                if (GUILayout.Button("Setup Collider"))
                {
                    AdjustCapsuleCollider(vketAvatarPedestal, vketAvatarPedestal.transform.Find("Visual"));
                }
            }

            EditorGUI.BeginDisabledGroup(vketAvatarPedestal.gameObject.scene.name == null);
            if (GUILayout.Button("Open Setting Window"))
            {
                VketPrefabSettingWindow
                    .OpenSettingWindow<VketAvatarPedestalSettingWindow>(vketAvatarPedestal.transform);
            }

            EditorGUI.EndDisabledGroup();
        }

        public static void AdjustCapsuleCollider(VketAvatarPedestal vketAvatarPedestal, Transform visualRoot)
        {
            CapsuleCollider capsuleCollider = vketAvatarPedestal.GetComponent<CapsuleCollider>();
            if (capsuleCollider == null)
            {
                Debug.LogWarning("CapsuleCollider not found.");
                return;
            }

            var renderers = visualRoot.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0)
            {
                Debug.Log("Renderer not found.");
                return;
            }

            Bounds totalBounds = CalculateBounds(renderers);

            Vector3 pos = vketAvatarPedestal.transform.position;
            Vector3 scale = vketAvatarPedestal.transform.lossyScale;

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

            Undo.RecordObject(capsuleCollider, "AdjustCollider");
            capsuleCollider.center = localCenter;
            capsuleCollider.height = localSize.y;
            capsuleCollider.radius = localSize.x <= localSize.y ? localSize.x * 0.5f : localSize.y * 0.5f;

            Debug.Log("Setup Completed.");
        }
        
        private static Bounds CalculateBounds(Renderer[] renderers)
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
    }
}
