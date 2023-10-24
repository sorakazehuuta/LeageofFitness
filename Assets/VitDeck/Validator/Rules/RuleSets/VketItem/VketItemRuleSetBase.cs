#if VRC_SDK_VRCSDK3
using UnityEngine;
using VitDeck.Language;
using VRC.SDK3.Components;

namespace VitDeck.Validator.RuleSets
{

    /// <summary>
    /// Vket Itemの基本ルールセット。
    /// </summary>
    /// <remarks>
    /// 変数をabstractまたはvirtualプロパティで宣言し、継承先で上書きすることでワールドによる制限の違いを表現する。
    /// </remarks>
    public abstract class VketItemRuleSetBase : IRuleSet
    {
        public abstract string RuleSetName
        {
            get;
        }

        protected IOfficialAssetData _officialAssetData;

        protected readonly long MegaByte = 1048576;

        protected readonly VketTargetFinder targetFinder = new VketTargetFinder();
        public IValidationTargetFinder TargetFinder => targetFinder;

        public VketItemRuleSetBase(IOfficialAssetData officialAssetData) : base()
        {
            _officialAssetData = officialAssetData;
        }

        /// <summary>
        /// 入稿フォルダの最大容量
        /// </summary>
        protected abstract long FolderSizeLimit { get; }

        /// <summary>
        /// ブースのサイズ
        /// </summary>
        protected abstract Vector3 BoothSizeLimit { get; }
        
        /// <summary>
        /// Materialの最大数
        /// 0の場合はルール検証をスキップ
        /// </summary>
        protected abstract int MaterialUsesLimit { get; }

        private ComponentReference[] GetComponentReferences()
        {
            return new ComponentReference[] {
                new ComponentReference("Skinned Mesh Renderer", new[]{"UnityEngine.SkinnedMeshRenderer"}, ValidationLevel.ALLOW),
                new ComponentReference("Mesh Renderer ", new[]{"UnityEngine.MeshRenderer"}, ValidationLevel.ALLOW),
                new ComponentReference("Mesh Filter", new[]{"UnityEngine.MeshFilter"}, ValidationLevel.ALLOW),
                
                // ルート以外で使用していたら弾くのはVketItemRuleで行う
                new ComponentReference("Collider", new[]{"UnityEngine.BoxCollider"}, ValidationLevel.ALLOW),
                new ComponentReference("Rigidbody", new[]{"UnityEngine.Rigidbody"}, ValidationLevel.ALLOW),
                new ComponentReference("VRC Pickup", new[]{"VRC.SDKBase.VRC_Pickup", "VRC.SDK3.Components.VRCPickup"}, ValidationLevel.ALLOW),
            };
        }
        
        public virtual IRule[] GetRules()
        {
            // デフォルトで使っていたAttribute式は宣言時にconst以外のメンバーが利用できない。
            // 継承したプロパティを参照して挙動を変えることが出来ない為、直接リストを返す方式。
            return new IRule[]
            {
                // Unityバージョンルール
                new UnityVersionRule(LocalizedMessage.Get("VketRuleSetBase.UnityVersionRule.Title", "2019.4.31f1"), "2019.4.31f1"),

                // SDKバージョンルール
                new VRCSDKVersionRule(LocalizedMessage.Get("VketRuleSetBase.VRCSDKVersionRule.Title"), new VRCSDKVersion("3.4.0")),

                // 入稿不可のシェーダールール
                // Unityのビルトインシェーダーと同名のシェーダーは使用できません。
                new ShaderNameBlockRule(LocalizedMessage.Get("VketRuleSetBase.ShaderNameBlockRule.Title"), _officialAssetData.DeniedShaderNames),

                // 配布物以外のすべてのオブジェクトが入稿フォルダ内にあるか検出するルール
                new ExistInSubmitFolderRule(LocalizedMessage.Get("VketRuleSetBase.ExistInSubmitFolderRule.Title"), _officialAssetData.GUIDs, targetFinder),

                // 配布アセットが入稿フォルダ内に入っていないか確認
                new AssetGuidBlacklistRule(LocalizedMessage.Get("VketRuleSetBase.OfficialAssetDontContainRule.Title"), _officialAssetData.GUIDs),

                // ファイル名とフォルダ名の使用禁止文字ルール
                new AssetNamingRule(LocalizedMessage.Get("VketRuleSetBase.NameOfFileAndFolderRule.Title"), @"[a-zA-Z0-9 _\.\-\(\)]+"),

                // ファイルパスの長さが規定値を超えていないか検出するルール
                new AssetPathLengthRule(LocalizedMessage.Get("VketRuleSetBase.FilePathLengthLimitRule.Title", 184), 184),

                // 特定の拡張子を持つアセットを検出するルール(メッシュアセットのファイル形式で特定のものが含まれていないこと)
                new AssetExtensionBlacklistRule(LocalizedMessage.Get("VketRuleSetBase.MeshFileTypeBlacklistRule.Title"),
                                                new string[]{".ma", ".mb", "max", "c4d", ".blend"}
                ),

                // アセットに埋め込まれたMaterialもしくはTextureを使っていないこと
                new ContainMatOrTexInAssetRule(LocalizedMessage.Get("VketRuleSetBase.ContainMatOrTexInAssetRule.Title")),

                // フォルダサイズが設定値を超えていないか検出
                new FolderSizeRule(LocalizedMessage.Get("VketRuleSetBase.FolderSizeRule.Title"), FolderSizeLimit),
                
                // VketItem入稿用のルール
                // Pickup動作確認のため追加
                // VRCPickup, BoxCollider, RigidBodyはRootのオブジェクト以外では使用できない
                new VketItemRule(LocalizedMessage.Get("VketItemRuleSetBase.VketItemRule.Title")),
                
                // 入稿シーンの構造が正しいか
                new SceneStructureRule(LocalizedMessage.Get("VketRuleSetBase.SceneStructureRule.Title")),
                
                // Staticフラグが正しいか、特定のStatic設定によるルールの適用
                // アイテムの場合、Static禁止
                new StaticFlagRule(LocalizedMessage.Get("VketItemRuleSetBase.StaticFlagsRule.Title"), true),
                
                // EditorOnlyタグは使用不可
                new DisallowEditorOnlyTagRule(LocalizedMessage.Get("VketRuleSetBase.DisallowEditorOnlyTagRule.Title"), true),

                // ブースサイズのルール
                new VRCBoothBoundsRule(LocalizedMessage.Get("VketRuleSetBase.BoothBoundsRule.Title"),
                    size: BoothSizeLimit,
                    margin: 0.01f,
                    Vector3.zero,
                    _officialAssetData.SizeIgnorePrefabGUIDs),

                // Material使用数が最大値を超えていないか
                new AssetTypeLimitRule(
                    LocalizedMessage.Get("VketRuleSetBase.MaterialLimitRule.Title", MaterialUsesLimit),
                    typeof(Material),
                    MaterialUsesLimit,
                    _officialAssetData.MaterialGUIDs),
                
                // シェーダーエラーの検出
                // マテリアルが設定されていない、シェーダーエラー、シェーダーがnullであることを検出
                new ErrorShaderRule(LocalizedMessage.Get("Booth.ErrorShaderRule.Title")), 
                // // シェーダーホワイトリストのチェック
                // new ShaderWhitelistRule(LocalizedMessage.Get("Booth.ShaderWhiteListRule.Title"),
                //     _officialAssetData.AllowedShaders, LocalizedMessage.Get("Booth.ShaderWhiteListRule.Solution")), 

                // 使用不可なコンポーネントの検出
                // ~の使用は許可されていません。
                new UsableComponentListRule(LocalizedMessage.Get("VketRuleSetBase.UsableComponentListRule.Title"),
                    GetComponentReferences(), ignorePrefabGUIDs: _officialAssetData.GUIDs, true, ValidationLevel.DISALLOW),

                // SkinnedMeshRendererのルール
                // Update When Offscreenを無効
                // Materials 0のSkinned Mesh Rendererは禁止
                new SkinnedMeshRendererRule(LocalizedMessage.Get("VketRuleSetBase.SkinnedMeshRendererRule.Title")),

                // MeshRendererのルール
                // ContributeGI をチェックしたオブジェクトは、Layer割り当てをEnvironmentとしてください
                // Materials 0のMesh Rendererは禁止
                new MeshRendererRule(LocalizedMessage.Get("VketRuleSetBase.MeshRendererRule.Title")),
                
                // ExistInSubmitFolderRule ではVket・VRC公式扱いのPrefabは制限されないので、禁止するPrefabはここで止める
                // （PickupやVideoPlayerのようなPrefab個数制限ルールを転用）
                // #302
                new PrefabLimitRule(
                    LocalizedMessage.Get("VketRuleSetBase.UnusabePrefabRule.Title"),
                    _officialAssetData.VRCSDKForbiddenPrefabGUIDs,
                    0),
            };
        }
    }
}
#endif
