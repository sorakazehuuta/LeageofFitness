
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Vket.VketPrefabs
{
    [CustomEditor(typeof(VketWebPageOpener))]
    public class VketWebPageOpenerEditor : Editor
    {
        private Vector2 scrollPosition = Vector2.zero;
        private VketWebPageOpener vketWebPageOpener;
        private Image image;
        private string[] webPageOpenerType =
        {
            "  Vket Circle Page Opener 2D",
            "  Vket Circle Page Opener 3D",
            "  Vket Item Page Opener 2D",
            "  Vket Item Page Opener 3D"
        };
        private string[] summarys =
        {
            "** ReadMe **\n" +
            "- InteractするとVketStore上のサークルページをブラウザで開きます\n" +
            "  当Prefabはバーチャルマーケット開催期間中の会場内でのみ機能します\n" +
            "- 見た目に画像を使用できます\n" +
            "  \"Sprite\"にSpriteを登録すると表示画像を変更できます\n" +
            "- 大きさを変えたいときは\"VketCirclePageOpener_2D\"オブジェクトのScaleを調整してください\n" +
            "- Upon Interact the exhibitor page at VketStore will be opened on player's browser.\n" +
            "  This Prefab will function only in the venue worlds during the event period of Virtual Market.\n" +
            "- You may use an image for its appearance.\n" +
            "  Register a Sprite on \"Sprite\" to change the appearance.\n" +
            "- If you want to change the size, adjust Scale of \"VketCirclePageOpener_2D\" object.",
            "** ReadMe **\n" +
            "- InteractするとVketStore上のサークルページをブラウザで開きます\n" +
            "  当Prefabはバーチャルマーケット開催期間中の会場内でのみ機能します\n" +
            "- 見た目に3Dモデルを使用できます\n" +
            "- \"Visual\"オブジェクトの子に見た目に使用したいオブジェクトを追加してください\n" +
            "  その後見た目に合うようにCapsuleColliderを調整します\n" +
            "　\"Setup Collider\"ボタンを押すとオブジェクトに合わせてCapsuleColliderが自動調整されます\n" +
            "  また、追加したオブジェクトのコライダーは削除してください\n" +
            "- Upon Interact the exhibitor page at VketStore will be opened on player's browser.\n" +
            "  This Prefab will function only in the venue worlds during the event period of Virtual Market.\n" +
            "- You can use a 3D model as its appearance.\n" +
            "- Add the object for object appearance as a child of \"Visual\" object.\n" +
            "  Then, adjust CapsuleCollider to match the appearance.\n" +
            "  You may press \"Setup Collider\" button to automatically adjust CapsuleCollider to fit the object.\n" +
            "  Also, please delete the collider of the object added for pedestal appearance.",
            "** ReadMe **\n" +
            "- InteractするとVketStore上の商品ページをブラウザで開きます\n" +
            "  当Prefabはバーチャルマーケット開催期間中の会場内でのみ機能します\n" +
            "- \"Item Id\"に商品ページのItemIDを設定してください\n" +
            "- 見た目に画像を使用できます\n" +
            "  \"Sprite\"にSpriteを登録すると表示画像を変更できます\n" +
            "- 大きさを変えたいときは\"VketCirclePageOpener_2D\"オブジェクトのScaleを調整してください\n" +
            "- Upon Interact the item page at VketStore will be opened on player's browser.\n" +
            "  This Prefab will function only in the venue worlds during the event period of Virtual Market.\n" +
            "- Set the ItemID of your product in Vket Store on \"Item Id\".\n" +
            "- You may use an image for its appearance.\n" +
            "  Register a Sprite on \"Sprite\" to change the appearance.\n" +
            "- If you want to change the size, adjust Scale of \"VketCirclePageOpener_2D\" object.",
            "** ReadMe **\n" +
            "- InteractするとVketStore上の商品ページをブラウザで開きます\n" +
            "  当Prefabはバーチャルマーケット開催期間中の会場内でのみ機能します\n" +
            "- \"Item Id\"に商品ページのItemIDを設定してください\n" +
            "- 見た目に3Dモデルを使用できます\n" +
            "  \"Visual\"オブジェクトの子に見た目に使用したいオブジェクトを追加してください\n" +
            "  その後見た目に合うようにCapsuleColliderを調整します\n" +
            "　\"Setup Collider\"ボタンを押すとオブジェクトに合わせてCapsuleColliderが自動調整されます\n" +
            "  また、追加したオブジェクトのコライダーは削除してください\n" +
            "- Upon Interact the item page at VketStore will be opened on player's browser.\n" +
            "  This Prefab will function only in the venue worlds during the event period of Virtual Market.\n" +
            "- Set the ItemID of your product in Vket Store on \"Item Id\".\n" +
            "- You can use a 3D model as its appearance.\n" +
            "- Add the object for object appearance as a child of \"Visual\" object.\n" +
            "  Then, adjust CapsuleCollider to match the appearance.\n" +
            "  You may press \"Setup Collider\" button to automatically adjust CapsuleCollider to fit the object.\n" +
            "  Also, please delete the collider of the object added for pedestal appearance."
        };
        private int[] summaryHeights = { 108, 140, 124, 152 };

        private void OnEnable()
        {
            if (vketWebPageOpener == null)
                vketWebPageOpener = (VketWebPageOpener)target;
        }

        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            serializedObject.Update();

            var type = serializedObject.FindProperty("type").intValue;

            // Draw Title and Summary
            var style = new GUIStyle();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField(webPageOpenerType[type], new GUIStyle(EditorStyles.boldLabel));
            EditorGUILayout.EndVertical();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(summaryHeights[type]));
            EditorGUILayout.TextArea(summarys[type]);
            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();

            if (!IsCircle(type))
            {
                var itemPageOpener = vketWebPageOpener.GetComponentInChildren<VioRama.ItemPageOpener>(true);
                if (itemPageOpener != null)
                {
                    EditorGUI.BeginChangeCheck();
                    var itemId = EditorGUILayout.IntField("Item Id", itemPageOpener.ItemId);
                    if (EditorGUI.EndChangeCheck())
                    {
                        var so = new SerializedObject(itemPageOpener);
                        so.Update();
                        so.FindProperty("_itemId").intValue = itemId;
                        so.ApplyModifiedProperties();
                    }
                }
            }

            if (Is2D(type))
            {
                Transform canvas = vketWebPageOpener.transform.Find("Canvas");
                if (canvas != null)
                {
                    if (image == null)
                        image = canvas.Find("Image")?.GetComponent<Image>();
                    if (image != null)
                    {
                        EditorGUI.BeginChangeCheck();
                        var sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", image.sprite, typeof(Sprite), false);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(image, "Modify image sprite");
                            image.sprite = sprite;
                            image.enabled = false;
                            image.enabled = true;
                        }

                        EditorGUILayout.Space();
                    }
                    else
                    {
                        Debug.Log("Not Found \"VketWebPageOpener/Canvas/Image\"");
                    }
                }
            }
            else
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
                    AdjustCapsuleCollider();
                }
            }
            
            EditorGUILayout.Space(10);
            
            EditorGUI.BeginDisabledGroup(vketWebPageOpener.gameObject.scene.name == null);
            if (GUILayout.Button("Open Setting Window"))
            {
                VketPrefabSettingWindow.OpenSettingWindow<VketWebPageOpenerSettingWindow>(vketWebPageOpener.transform);
            }
            EditorGUI.EndDisabledGroup();

            //base.OnInspectorGUI();
        }

        private bool Is2D(int type)
        {
            return type == 0 || type == 2;
        }

        private bool IsCircle(int type)
        {
            return type == 0 || type == 1;
        }

        private void AdjustCapsuleCollider()
        {
            CapsuleCollider capsuleCollider = vketWebPageOpener.GetComponent<CapsuleCollider>();
            if (capsuleCollider == null)
            {
                Debug.LogWarning("CapsuleCollider not found.");
                return;
            }

            var renderers = vketWebPageOpener.transform.Find("Visual").GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0)
            {
                Debug.Log("Renderer not found.");
                return;
            }

            Bounds totalBounds = CalculateBounds(renderers);

            Vector3 pos = vketWebPageOpener.transform.position;
            Vector3 scale = vketWebPageOpener.transform.lossyScale;

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
    }
}
