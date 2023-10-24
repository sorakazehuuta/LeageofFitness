using System.Collections.Generic;
using System.Linq;

namespace VitDeck.Validator.RuleSets
{
    public class VketUdonOfficialAssetData : IOfficialAssetData
    {
        string[] GetDynamicBoneGUIDs()
        {
            return new string[]
            {
                "bdbe6feeda2a62b45ad9a4e311031478", // Assets/DynamicBone/ReadMe.txt
                "ba128457d0ea5e3439dbe4a53b9d1273", // Assets/DynamicBone/Demo/c1.fbx
                "902c84bf971339c459ce4b757e333a55", // Assets/DynamicBone/Demo/Demo1.unity
                "178320cedf292cb4f8d6c0b737b35953", // Assets/DynamicBone/Demo/DynamicBoneDemo1.cs
                "19015a5957bbaa745a61cba005220542", // Assets/DynamicBone/Demo/tail.FBX
                "f9ac8d30c6a0d9642a11e5be4c440740", // Assets/DynamicBone/Scripts/DynamicBone.cs
                "baedd976e12657241bf7ff2d1c685342", // Assets/DynamicBone/Scripts/DynamicBoneCollider.cs
                "04878769c08021a41bc2d2375e23ec0b", // Assets/DynamicBone/Scripts/DynamicBoneColliderBase.cs
                "4e535bdf3689369408cc4d078260ef6a", // Assets/DynamicBone/Scripts/DynamicBonePlaneCollider.cs
            };
        }

        string[] GetTextMeshProGUIDs()
        {
            return new string[]
            {
                "1b8d251f9af63b746bf2f7ffe00ebb9b",  // Assets/TextMesh Pro/Documentation/TextMesh Pro User Guide 2016.pdf
                "6e59c59b81ab47f9b6ec5781fa725d2c",  // Assets/TextMesh Pro/Fonts/LiberationSans - OFL.txt
                "e3265ab4bf004d28a9537516768c1c75",  // Assets/TextMesh Pro/Fonts/LiberationSans.ttf
                "fade42e8bc714b018fac513c043d323b",  // Assets/TextMesh Pro/Resources/LineBreaking Following Characters.txt
                "d82c1b31c7e74239bff1220585707d2b",  // Assets/TextMesh Pro/Resources/LineBreaking Leading Characters.txt
                "3f5b5dff67a942289a9defa416b206f3",  // Assets/TextMesh Pro/Resources/TMP Settings.asset
                "e73a58f6e2794ae7b1b7e50b7fb811b0",  // Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF - Drop Shadow.mat
                "2e498d1c8094910479dc3e1b768306a4",  // Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF - Fallback.asset
                "79459efec17a4d00a321bdcc27bbc385",  // Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF - Outline.mat
                "8f586378b4e144a9851e7b34d9b748ee",  // Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF.asset
                "c41005c129ba4d66911b75229fd70b45",  // Assets/TextMesh Pro/Resources/Sprite Assets/EmojiOne.asset
                "f952c082cb03451daed3ee968ac6c63e",  // Assets/TextMesh Pro/Resources/Style Sheets/Default Style Sheet.asset
                "407bc68d299748449bbf7f48ee690f8d",  // Assets/TextMesh Pro/Shaders/TMPro.cginc
                "c334973cef89a9840b0b0c507e0377ab",  // Assets/TextMesh Pro/Shaders/TMPro_Mobile.cginc
                "3997e2241185407d80309a82f9148466",  // Assets/TextMesh Pro/Shaders/TMPro_Properties.cginc
                "d930090c0cd643c7b55f19a38538c162",  // Assets/TextMesh Pro/Shaders/TMPro_Surface.cginc
                "48bb5f55d8670e349b6e614913f9d910",  // Assets/TextMesh Pro/Shaders/TMP_Bitmap-Custom-Atlas.shader
                "1e3b057af24249748ff873be7fafee47",  // Assets/TextMesh Pro/Shaders/TMP_Bitmap-Mobile.shader
                "128e987d567d4e2c824d754223b3f3b0",  // Assets/TextMesh Pro/Shaders/TMP_Bitmap.shader
                "dd89cf5b9246416f84610a006f916af7",  // Assets/TextMesh Pro/Shaders/TMP_SDF Overlay.shader
                "14eb328de4b8eb245bb7cea29e4ac00b",  // Assets/TextMesh Pro/Shaders/TMP_SDF SSD.shader
                "bc1ede39bf3643ee8e493720e4259791",  // Assets/TextMesh Pro/Shaders/TMP_SDF-Mobile Masking.shader
                "a02a7d8c237544f1962732b55a9aebf1",  // Assets/TextMesh Pro/Shaders/TMP_SDF-Mobile Overlay.shader
                "c8d12adcee749c344b8117cf7c7eb912",  // Assets/TextMesh Pro/Shaders/TMP_SDF-Mobile SSD.shader
                "fe393ace9b354375a9cb14cdbbc28be4",  // Assets/TextMesh Pro/Shaders/TMP_SDF-Mobile.shader
                "85187c2149c549c5b33f0cdb02836b17",  // Assets/TextMesh Pro/Shaders/TMP_SDF-Surface-Mobile.shader
                "f7ada0af4f174f0694ca6a487b8f543d",  // Assets/TextMesh Pro/Shaders/TMP_SDF-Surface.shader
                "68e6db2ebdc24f95958faec2be5558d6",  // Assets/TextMesh Pro/Shaders/TMP_SDF.shader
                "cf81c85f95fe47e1a27f6ae460cf182c",  // Assets/TextMesh Pro/Shaders/TMP_Sprite.shader
                "381dcb09d5029d14897e55f98031fca5",  // Assets/TextMesh Pro/Sprites/EmojiOne Attribution.txt
                "8f05276190cf498a8153f6cbe761d4e6",  // Assets/TextMesh Pro/Sprites/EmojiOne.json
                "dffef66376be4fa480fb02b19edbe903",  // Assets/TextMesh Pro/Sprites/EmojiOne.png
            };
        }
        string[] GetVrcSdkGUIDs()
        {
            return new string[]
            {
                "c5f84ed1cfe4e034c93031e8936428ab",  // Packages/com.vrchat.base/license.txt
                "1f872e4d36d785e409479da1c5fcde4c",  // Packages/com.vrchat.base/package.json
                "182bdf489e961364e891f322dc1d31f1",  // Packages/com.vrchat.base/Editor/VRCSDK/SampleHintCreator.cs
                "6e9c6119ac4eb334284fb7b4bc6d1f05",  // Packages/com.vrchat.base/Editor/VRCSDK/VRC.SDKBase.Editor.asmdef
                "cb850b86de9091d4db4595959c56f954",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/Oculus/Spatializer/ONSPAudioSourceEditor.cs
                "10d9f721d76e07a47bc9e5f61e2fae72",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/EnvConfig.cs
                "0bc1f4d12c7c0f3468bd3469a5209dc1",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/FixAnimatorControllers.cs
                "9a4af4545014b16439e24fcba1fd1e5e",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/FixConstraintUpdateOrder.cs
                "c3399613f583f3e46b2df27ae87dd5d6",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/HDRColorFixerUtility.cs
                "2ac39b32acc741458c6f8a502807384f",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/ProtectVPMPackages.cs
                "679ba0056bf110c4db8b550082e73a5f",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/ShaderKeywordsUtility.cs
                "0d2c09d149d213846ac4bdab38be0385",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/VRCCachedWebRequest.cs
                "4a0e17705e9d89643aa66589bee0428f",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/VRCNetworkIDUtility.cs
                "58f421a645644f18aca86b7d90ded374",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/VRCPackageSettings.cs
                "2b0e3b5a958748ec8b1fdf0b4b636ce4",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/VRCSdkBuilderExceptions.cs
                "cb5d1f9882b08564cae97b2b14ad4e8f",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/VRC_EditorTools.cs
                "f4cf5dd705ab67149afaba40b4a8fa7e",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/VRC_SdkSplashScreen.cs
                "2970d5a709404efb9249205c86afaf20",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/API/IVRCContent.cs
                "cffdc642f18a45c9865fc930726518fe",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/API/VRCApi.cs
                "c2861b3597c849e08c2126c83ef3fca7",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/API/VRCApiCache.cs
                "67fd710f3e92422eba267779143a9c5f",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/API/VRCApiError.cs
                "536cf9130a7e41a19f0d7e5d7900210d",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/API/VRCApiExceptions.cs
                "93e3e8f8e94746a6b83e7431081ef622",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/API/VRCAvatar.cs
                "45cf16ea678f4615920c2d510d154633",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/API/VRCFile.cs
                "685a233b2bbf4a99a5f62f2d9727f484",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/API/VRCProgressContent.cs
                "3e259a399dbb4ef19a7a89d2b4233247",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/API/VRCTools.cs
                "f7f09500761d4a019e606df903a022c8",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/API/VRCUnityPackage.cs
                "bcf1554494894265b68fbf2e7c089782",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/API/VRCWorld.cs
                "21332e1f0d937794d916d2402ba1943a",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/BuildPipeline/VRC.SDKBase.Editor.BuildPipeline.asmdef
                "0a1d20f4241085e46bdddc71b691465b",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/BuildPipeline/Samples/VRCSDKBuildRequestedCallbackSample.cs
                "0a364ece829b6234888c59987a305a00",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/AutoAddSpatialAudioComponents.cs
                "89005ebc9543e0a4284893c09ca19b1d",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/EditorCoroutine.cs
                "3d6c2e367eaa9564ebf6267ec163cfbd",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/EditorHandling.cs
                "4810e652e8242384c834320970702290",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/EventHandlerEditor.cs
                "482185bf29f12074dada194ffef6a682",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/OldTriggerEditors.cs
                "5e83254bb97e84795ac882692ae124ba",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRCAvatarDescriptorEditor.cs
                "26a75599848adb449b7aceed5090e35c",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRCObjectSpawnEditor.cs
                "ed4aad2698d3b62408e69b57c7748791",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRCObjectSyncEditor.cs
                "8986a640e24a0754ea0aded12234b808",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRCPlayerModEditorWindow.cs
                "792e7964a56e51f4188e1221751642e9",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRCPlayerModsEditor.cs
                "5262a02c32e41e047bdfdfc3b63db8ff",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRCPlayerStationEditor.cs
                "e9cbc493bbbc443fb92898aa84d221ec",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRCSceneDescriptorEditor.cs
                "eeda995d0ceac6443a54716996eab52e",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRC_AvatarVariationsEditor.cs
                "0ac7998a36f085844847acbc046d4e27",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRC_DataStorageEditor.cs
                "3b63b118c0591b548ba1797e6be4292e",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRC_DestructibleStandardEditor.cs
                "e19a7147a2386554a8e4d6e414f190a2",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRC_ObjectSyncEditor.cs
                "4aff4e5c0d600c845b29d7b8b7965d68",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRC_PickupEditor.cs
                "5c545625e0bf93045ac1c5864141c5c1",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRC_PlayerAudioOverrideEditor.cs
                "0d2d4cba733f5eb4ba170368e67710d2",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRC_SpatialAudioSourceEditor.cs
                "ae0e74693b7899f47bd98864f94b9311",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRC_SyncVideoPlayerEditor.cs
                "3f9dccfed0b072f49a307b3f20a7e768",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRC_SyncVideoStreamEditor.cs
                "3aecd666943878944a811acb9db2ace7",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRC_TriggerEditor.cs
                "d09b36020f697be4d9a0f5a6a48cfa83",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRC_WebPanelEditor.cs
                "764e26c1ca28e2e45a30c778c1955a47",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Components/VRC_YouTubeSyncEditor.cs
                "310a760e312f2984e85eece367bab19a",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/ControlPanel/IVRCSdkControlPanelBuilder.cs
                "20b4cdbdda9655947aab6f8f2c90690f",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/ControlPanel/VRCSdkControlPanel.cs
                "5066cd5c1cc208143a1253cac821714a",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/ControlPanel/VRCSdkControlPanelAccount.cs
                "4c73e735ee0380241b186a8993fa56bf",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/ControlPanel/VRCSdkControlPanelBuilder.cs
                "c768b42ca9a2f2b48afeb1fa03d5e1bf",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/ControlPanel/VRCSdkControlPanelBuilderAttribute.cs
                "c7333cdb3df19724b84b4a1b05093fe0",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/ControlPanel/VRCSdkControlPanelContent.cs
                "f3507a74e4b8cfd469afac127fa5f4e5",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/ControlPanel/VRCSdkControlPanelHelp.cs
                "8357b9b7ef2416946ae86f465a64c0e0",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/ControlPanel/VRCSdkControlPanelSettings.cs
                "e12747ce8f144b2692d71a3fee32b2da",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/CreatorCompanion/UnityWindowWebSocketClient.cs
                "7f2e61b9b35d20342af38d897c424eaa",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/CreatorCompanion/VRCSocketClient.cs
                "c1877d18642b4765bdeba2e1cb631978",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/Checklist/Checklist.cs
                "fa4cfed5918f428db418a0c5ee52de02",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/Checklist/Resources/ChecklistLayout.uxml
                "8b3c8c1b61ee451892e761c0d1b0d60e",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/Checklist/Resources/ChecklistStyles.uss
                "9246484d22834713af8e88e23d611272",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/ContentWarningsField/ContentWarningsField.cs
                "aa0b64baef3243fa97ff4a14d0cab0b8",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/ContentWarningsField/Resources/ContentWarningsField.uxml
                "34a61e595b7045ef9dd83aaa2d0b95ed",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/ContentWarningsField/Resources/ContentWarningsFieldStyles.uss
                "e7485de206c641469dcf232a342a69c6",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/GenericBuilderNotification/GenericBuilderNotification.cs
                "6e653ccec8c748c79b9bcf3b98428441",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/GenericBuilderNotification/Resources/GenericBuilderNotification.uxml
                "98fa0a3c12e04eeaac24aa52eed81e8a",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/GenericBuilderNotification/Resources/GenericBuilderNotificationStyles.uss
                "cefc8575eca3497fbe2221e89b6ba018",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/TagsField/TagsField.cs
                "36d3f6fd312f4ef48aa0cd0b8fa0c9ef",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/TagsField/Resources/TagsField.uxml
                "cf83c9f55a1e4586b639fab90fcc48dc",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/TagsField/Resources/TagsFieldStyles.uss
                "21784b55f82d46e0aba5b8fec85c5551",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/Thumbnail/Thumbnail.cs
                "f436adf433bd48d69a078c8ec49814e0",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/Thumbnail/Resources/Thumbnail.uxml
                "f774b221f3fa4973a0931098da26177e",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/Thumbnail/Resources/ThumbnailStyles.uss
                "8625fc371c064cb39e1c550073645721",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/ThumbnailFoldout/ThumbnailFoldout.cs
                "7a0a0d51d65e4ac9a9ea3861a089855c",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/ThumbnailFoldout/Resources/ThumbnailFoldout.uxml
                "7b592ca25f524408a84f100c8580649c",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Elements/VRCTextField/VRCTextField.cs
                "358965b17df740308d14104f06d30bf8",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Public SDK API/IVRCSdkBuilderApi.cs
                "3f2c54d95b1d4bc084f3a35db2c95ade",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Public SDK API/IVRCSdkPanelApi.cs
                "94edf62c27899004b9bca09b15e239ce",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Resources/CropShader.shader
                "9ae7399f0cf902a41a20f3487af8322a",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Resources/SDK_Panel_Banner.png
                "abbe9332cbff4a143812d98775e04a72",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Resources/ThumbnailCapture.renderTexture
                "8c4423c989a97d64eabbd9375dbd3e89",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Resources/vrcSdkBottomHeader.png
                "567fb68379e44c4d970461b6294276ac",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Resources/VRCSdkBuilderLayout.uxml
                "5edd88a9009748e3a2d8e7900dcf839c",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Resources/VRCSdkBuilderStyles.uss
                "cb3df6568f1c9b74d8bbe0e192d2514d",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Resources/vrcSdkClDialogNewIcon.png
                "1fc57198d6a4e5a4e9c3175ab5c609a7",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Resources/vrcSdkHeader.png
                "a55e07f20e57463886f414c9c5393f9f",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Resources/VRCSdkPanelLayout.uxml
                "3c20193d066a421398fc7681c439ea8f",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Resources/VRCSdkPanelStyles.uss
                "2eab3e5bca2445eb92e40701323ddddc",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Resources/VRCSdkSplashScreen.uxml
                "9211071f17f5422b8148dbee973f90d5",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Resources/VRCSdkSplashScreenStyles.uss
                "a337b85d7d3b8f0408576d7f08bf609f",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Resources/vrcSdkSplashUdon1.png
                "19889d5e743a45f4da316fb1d760ffba",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Resources/vrcSdkSplashUdon2.png
                "93710d221addc0243ba90dd20369844b",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/SDK3Compatibility/VRCSdk3Analysis.cs
                "e634ad4b7e2c19c49bd1c3856efaf04b",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/ShaderStripping/InjectStereoVariants.cs
                "3f05fc74d61cc0c448411f8b55c918ca",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/ShaderStripping/StripAndroidAvatars.cs
                "06d01ef00e2795244aa8b5cbe879b16e",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/ShaderStripping/StripPostProcessing.cs
                "09158b5a87ea9554daafaef906ae927e",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/ShaderStripping/VRC.SDKBase.Editor.ShaderStripping.asmdef
                "62d40cc4e8f8494695f0102c58b3ea60",  // Packages/com.vrchat.base/Editor/VRCSDK/Dependencies/VRChat/Validation/Performance/SDKPerformanceDisplay.cs
                "c99f0f9c20c10d244b376b774f4b9de2",  // Packages/com.vrchat.base/Editor/VRCSDK/Plugins/VRC.SDK3.Dynamics.Contact.Editor.dll
                "adf6ca0c885565e46a26e53348e18e99",  // Packages/com.vrchat.base/Editor/VRCSDK/Plugins/VRC.SDK3.Dynamics.PhysBone.Editor.dll
                "40ef2e46f900131419e869398a8d3c9d",  // Packages/com.vrchat.base/Editor/VRCSDK/Plugins/UniTask/Editor/SplitterGUILayout.cs
                "4129704b5a1a13841ba16f230bf24a57",  // Packages/com.vrchat.base/Editor/VRCSDK/Plugins/UniTask/Editor/UniTask.Editor.asmdef
                "52e2d973a2156674e8c1c9433ed031f7",  // Packages/com.vrchat.base/Editor/VRCSDK/Plugins/UniTask/Editor/UniTaskTrackerTreeView.cs
                "5bee3e3860e37484aa3b861bf76d129f",  // Packages/com.vrchat.base/Editor/VRCSDK/Plugins/UniTask/Editor/UniTaskTrackerWindow.cs
                "bb15d88e30f9fae428df916379b289b2",  // Packages/com.vrchat.base/Editor/VRCSDK/Sample Assets/RealtimeEmissiveGammaGUI.cs
                "869af56c430f8e64db9912f417580e47",  // Packages/com.vrchat.base/Runtime/VRCSDK/license.txt
                "2cdbe2e71e2c46e48951c13df254e5b1",  // Packages/com.vrchat.base/Runtime/VRCSDK/version.txt
                "3456780c4fb2d324ab9c633d6f1b0ddb",  // Packages/com.vrchat.base/Runtime/VRCSDK/VRC.SDKBase.asmdef
                "a2e4b2ce02fa7914895069e5fdbf112d",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/librsync/Blake2Sharp.dll
                "912b2ac597cb1ad4c9bdc1a98ec15459",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/librsync/librsync.net.dll
                "db74213609a3d444c8bc111a3e373878",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/Managed/Collections.Pooled.dll
                "9787e75870c1dc347a0943055c585c64",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/Managed/System.Buffers.dll
                "03440596fa1da9c4f9796a20de292254",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/Managed/System.Collections.Immutable.dll
                "c30a499f804ba2e4281452f42b3ce52d",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/Managed/System.Memory.dll
                "9b6143470b1a740428cde7079e2c7555",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/Managed/System.Numerics.Vectors.dll
                "8b3b4a8bdfbaf344686d420abd25de73",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/Managed/System.Runtime.CompilerServices.Unsafe.dll
                "d4b2b271e0873ac4eb32b7dfd4cafb7e",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/Managed/System.Threading.Tasks.Extensions.dll
                "92a6b60b2fff40d46856d7d64d897dfd",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/Managed/VRC.Collections.dll
                "e503ea6418d27594caa33b93cac1b06a",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/Oculus/Spatializer/Scripts/ONSPAudioSource.cs
                "ad074644ff568a14187a3690cfbd7534",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/Oculus/Spatializer/Scripts/ONSPSettings.cs
                "d471b09e7f06a69458457ec63d3532b8",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Settings.asset
                "da07ab9b78cb0432e95e11e2cb619ea7",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Materials/BlueprintCam.mat
                "94b649c2bd1ac4cac95049605dc5333d",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Materials/BlueprintCam.renderTexture
                "2166f6bbfce69594fad494087eca58e8",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Materials/damageGrey.mat
                "e13e96301b7c8214dac6883be5b82bfa",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Models/damageSphere.fbx
                "841c3ce718e8b61408005c1cfce6b7de",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Models/Materials/lambert2.mat
                "4acdf7b3eb426480bb5acf58638bd493",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/awsconfig.xml
                "dd5614b710e774040ab715161f7dfaca",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/endpoints.customizations.json
                "37b4abef7420c4c7ea71dbe76757498a",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/endpoints.json
                "dce0dda226bd1f147a34f9b4660f5992",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/VRCProjectSettings.prefab
                "43066d8a73c579048891e3c123e252a0",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/2FAIcons/SDK_Warning_Triangle_icon.png
                "f310c3dbad3125d4e8fc2e00bdc2acb4",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/CL_Icons/CL_Lab_Icon_256.png
                "36349feed06587e479724a1a09c0b267",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/CL_Icons/Icon_New.png
                "53f9c1f34eb97ec4196ff26de5e242f7",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/EditorUI_Icons/EditorUI_Link.png
                "4109d4977ddfb6548b458318e220ac70",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/PerformanceIcons/Perf_Good_32.png
                "644caf5607820c7418cf0d248b12f33b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/PerformanceIcons/Perf_Great_32.png
                "2886eb1248200a94d9eaec82336fbbad",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/PerformanceIcons/Perf_Horrible_32.png
                "9296abd40c7c1934cb668aae07b41c69",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/PerformanceIcons/Perf_Medium_32.png
                "e561d0406779ab948b7f155498d101ee",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/PerformanceIcons/Perf_Poor_32.png
                "f0f530dea3891c04e8ab37831627e702",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/Validation/Performance/StatsLevels/Quest/AvatarPerformanceStatLevels_Quest.asset
                "e750436d0bab192489da0debe67ee879",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/Validation/Performance/StatsLevels/Quest/Excellent_Quest.asset
                "b25db21b17fba3d49a7248568fdb9870",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/Validation/Performance/StatsLevels/Quest/Good_Quest.asset
                "31feb7417182a80469408649071d10ac",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/Validation/Performance/StatsLevels/Quest/Medium_Quest.asset
                "171503e8193e15447967be1e3ca1e714",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/Validation/Performance/StatsLevels/Quest/Poor_Quest.asset
                "438f83f183e95f740877d4c22ed91af2",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/Validation/Performance/StatsLevels/Windows/AvatarPerformanceStatLevels_Windows.asset
                "88c46902276e7624e8adda9020bef28b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/Validation/Performance/StatsLevels/Windows/Excellent_Windows.asset
                "38957d57ab5a7f145b954d20fc24b1d4",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/Validation/Performance/StatsLevels/Windows/Good_Windows.asset
                "65edaefdc2f87414594559cb89383b5b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/Validation/Performance/StatsLevels/Windows/Medium_Windows.asset
                "595049d4e162571489f2437524d98a31",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/Validation/Performance/StatsLevels/Windows/Poor_Windows.asset
                "36c0d886a26373c46be857f2fc441071",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Scripts/ApiFileHelper.cs
                "acadc6659c5ab3446ad0d5de2563f95f",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Scripts/AudioManagerSettings.cs
                "8d047eaa3325d654aa62ccad6f73eb93",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Scripts/CommunityLabsConstants.cs
                "d0c461e358764cd1ab95544e34b0346c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Scripts/GameViewMethods.cs
                "a3132e0ab7e16494a9d492087a1ca447",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Scripts/RuntimeAPICreation.cs
                "1e5ebf65c5dceeb4c909aa7812bd2999",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Scripts/RuntimeBlueprintCreation.cs
                "2bd5ee5d69ee0f3449bf2f81fcb7f4e3",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Scripts/RuntimeWorldCreation.cs
                "0d49300ad532d4ae6b569b28de5b7dac",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Scripts/SceneSaver.cs
                "10121679f780956408f9a434a526f553",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Scripts/MaterialFallback/FallbackMaterialCache.cs
                "65a0b1106808685488e7287333ef73e9",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Scripts/Validation/AvatarValidation.cs
                "bef0a8d1d2c547119a62b7d7a5c512ea",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Scripts/Validation/ShaderValidation.cs
                "8a90ec11b51863c4cb2d8a8cee31c2fb",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Scripts/Validation/ValidationUtils.cs
                "9b03724cd556cb047b2da80492ea28a5",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Scripts/Validation/WorldValidation.cs
                "13d3efffb839ced4c8426a88a0c3e98c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Textures/damageGreyNoAlpha.png
                "8d95767408d35544c98f92ef7279b8db",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Textures/damageGRNoAlpha.png
                "861bc2dd35aa1534d89330ffa4434b61",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Textures/VRChatBanner.png
                "7c25bd6cb39c5764aa960267d4c8c33b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/SDKBase-Legacy.dll
                "cdfe97a8253414b4bb5dd295880489bd",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/VRC.Dynamics.dll
                "80f1b8067b0760e4bb45023bc2e9de66",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/VRC.SDK3.Dynamics.Contact.dll
                "2a2c05204084d904aa4945ccff20d8e5",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/VRC.SDK3.Dynamics.PhysBone.dll
                "36a780ff1cab41e429e89d185a72b6d4",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/VRC.Utility.dll
                "4ecd63eff847044b68db9453ce219299",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/VRCCore-Editor.dll
                "b0e1c0f72d838fe49bfe88b987a471bd",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/VRCCore-Standalone.dll
                "dc5cab6c932db3247aab9f50c5f3bd5f",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/VRCSDKBase-Editor.dll
                "db48663b319a020429e3b1265f97aff1",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/VRCSDKBase.dll
                "4a020cfbc5bfdce4b9a4208a59c84249",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/Harmony/0Harmony.dll
                "cfb05dea28888d04c892237fe888538e",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/Harmony/LICENSE.txt
                "6074c25a8e12377448689bc2b96da9eb",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Licence.txt
                "d1a9a71f68bb0d04db91ddaa3329abf9",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/package.json
                "01d1404ca421466419a7db7340ff5e77",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/AsyncLazy.cs
                "8ef320b87f537ee4fb2282e765dc6166",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/AsyncReactiveProperty.cs
                "4f95ac245430d304bb5128d13b6becc8",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/AsyncUnit.cs
                "7d739f510b125b74fa7290ac4335e46e",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/CancellationTokenEqualityComparer.cs
                "4be7209f04146bd45ac5ee775a5f7c26",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/CancellationTokenExtensions.cs
                "22d85d07f1e70ab42a7a4c25bd65e661",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/CancellationTokenSourceExtensions.cs
                "5ceb3107bbdd1f14eb39091273798360",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Channel.cs
                "ff50260d74bd54c4b92cf99895549445",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/EnumerableAsyncExtensions.cs
                "bc661232f11e4a741af54ba1c175d5ee",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/EnumeratorAsyncExtensions.cs
                "930800098504c0d46958ce23a0495202",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/ExceptionExtensions.cs
                "b20cf9f02ac585948a4372fa4ee06504",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/IUniTaskAsyncEnumerable.cs
                "3e4d023d8404ab742b5e808c98097c3c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/IUniTaskSource.cs
                "dc4c5dc2a5f246e4f8df44cab735826c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/MoveNextSource.cs
                "15fb5b85042f19640b973ce651795aca",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/PlayerLoopHelper.cs
                "57095a17fdca7ee4380450910afc7f26",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/PlayerLoopTimer.cs
                "e3377e2ae934ed54fb8fd5388e2d9eb9",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Progress.cs
                "19f4e6575150765449cc99f25f06f25f",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/TaskPool.cs
                "6347ab34d2db6d744a654e8d62d96b96",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/TimeoutController.cs
                "f68b22bb8f66f5c4885f9bd3c4fc43ed",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/TriggerEvent.cs
                "f51ebe6a0ceec4240a699833d6309b23",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTask.asmdef
                "bd6beac8e0ebd264e9ba246c39429c72",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTask.Bridge.cs
                "8947adf23181ff04db73829df217ca94",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTask.cs
                "ecff7972251de0848b2c0fa89bbd3489",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTask.Delay.cs
                "4e12b66d6b9bd7845b04a594cbe386b4",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTask.Factory.cs
                "8473162fc285a5f44bcca90f7da073e7",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTask.Run.cs
                "4132ea600454134439fa2c7eb931b5e6",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTask.Threading.cs
                "87c9c533491903a4288536b5ac173db8",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTask.WaitUntil.cs
                "355997a305ba64248822eec34998a1a0",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTask.WhenAll.cs
                "5110117231c8a6d4095fd0cbd3f4c142",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTask.WhenAll.Generated.cs
                "c32578978c37eaf41bdd90e1b034637d",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTask.WhenAny.cs
                "13d604ac281570c4eac9962429f19ca9",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTask.WhenAny.Generated.cs
                "ed03524d09e7eb24a9fb9137198feb84",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTaskCompletionSource.cs
                "05460c617dae1e440861a7438535389f",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTaskExtensions.cs
                "4b4ff020f73dc6d4b8ebd4760d61fb43",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTaskExtensions.Shorthand.cs
                "eaea262a5ad393d419c15b3b2901d664",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTaskObservableExtensions.cs
                "d6cad69921702d5488d96b5ef30df1b0",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTaskScheduler.cs
                "abf3aae9813db2849bce518f8596e920",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTaskSynchronizationContext.cs
                "e9f28cd922179634d863011548f89ae7",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UniTaskVoid.cs
                "e9147caba40da434da95b39709c13784",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UnityAsyncExtensions.AssetBundleRequestAllAssets.cs
                "98f5fedb44749ab4688674d79126b46a",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UnityAsyncExtensions.AsyncGPUReadback.cs
                "8cc7fd65dd1433e419be4764aeb51391",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UnityAsyncExtensions.cs
                "30979a768fbd4b94f8694eee8a305c99",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UnityAsyncExtensions.Jobs.cs
                "2edd588bb09eb0a4695d039d6a1f02b2",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UnityAsyncExtensions.MonoBehaviour.cs
                "6804799fba2376d4099561d176101aff",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UnityAsyncExtensions.uGUI.cs
                "090b20e3528552b4a8d751f7df525c2b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UnityBindingExtensions.cs
                "013a499e522703a42962a779b4d9850c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/UnityWebRequestException.cs
                "8507e97eb606fad4b99c6edf92e19cb8",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/_InternalVisibleTo.cs
                "02ce354d37b10454e8376062f7cbe57a",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/CompilerServices/AsyncMethodBuilderAttribute.cs
                "68d72a45afdec574ebc26e7de2c38330",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/CompilerServices/AsyncUniTaskMethodBuilder.cs
                "e891aaac17b933a47a9d7fa3b8e1226f",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/CompilerServices/AsyncUniTaskVoidMethodBuilder.cs
                "98649642833cabf44a9dc060ce4c84a1",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/CompilerServices/StateMachineRunner.cs
                "3dc6441f9094f354b931dc3c79fb99e5",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/External/Addressables/AddressablesAsyncExtensions.cs
                "593a5b492d29ac6448b1ebf7f035ef33",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/External/Addressables/UniTask.Addressables.asmdef
                "1f448d5bc5b232e4f98d89d5d1832e8e",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/External/DOTween/DOTweenAsyncExtensions.cs
                "029c1c1b674aaae47a6841a0b89ad80e",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/External/DOTween/UniTask.DOTween.asmdef
                "b6ba480edafb67d4e91bb10feb64fae5",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/External/TextMeshPro/TextMeshProAsyncExtensions.cs
                "79f4f2475e0b2c44e97ed1dee760627b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/External/TextMeshPro/TextMeshProAsyncExtensions.InputField.cs
                "dc47925d1a5fa2946bdd37746b2b5d48",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/External/TextMeshPro/UniTask.TextMeshPro.asmdef
                "f83ebad81fb89fb4882331616ca6d248",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/ArrayPool.cs
                "424cc208fb61d4e448b08fcfa0eee25e",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/ArrayPoolUtil.cs
                "23146a82ec99f2542a87971c8d3d7988",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/ArrayUtil.cs
                "f66c32454e50f2546b17deadc80a4c77",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/ContinuationQueue.cs
                "f80fb1c9ed4c99447be1b0a47a8d980b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/DiagnosticsExtensions.cs
                "5f39f495294d4604b8082202faf98554",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/Error.cs
                "7d63add489ccc99498114d79702b904d",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/MinimumQueue.cs
                "340c6d420bb4f484aa8683415ea92571",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/PlayerLoopRunner.cs
                "8932579438742fa40b010edd412dbfba",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/PooledDelegate.cs
                "94975e4d4e0c0ea4ba787d3872ce9bb4",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/RuntimeHelpersAbstraction.cs
                "60cdf0bcaea36b444a7ae7263ae7598f",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/StatePool.cs
                "a203c73eb4ccdbb44bddfd82d38fdda9",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/TaskTracker.cs
                "ebaaf14253c9cfb47b23283218ff9b67",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/UnityEqualityComparer.cs
                "111ba0e639de1d7428af6c823ead4918",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/UnityWebRequestExtensions.cs
                "f16fb466974ad034c8732c79c7fd67ea",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/ValueStopwatch.cs
                "6c78563864409714593226af59bcb6f3",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Internal/WeakDictionary.cs
                "5dc68c05a4228c643937f6ebd185bcca",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Aggregate.cs
                "7271437e0033af2448b600ee248924dd",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/All.cs
                "e2b2e65745263994fbe34f3e0ec8eb12",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Any.cs
                "3268ec424b8055f45aa2a26d17c80468",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/AppendPrepend.cs
                "69866e262589ea643bbc62a1d696077a",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/AsUniTaskAsyncEnumerable.cs
                "01ba1d3b17e13fb4c95740131c7e6e19",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/AsyncEnumeratorBase.cs
                "58499f95012fb3c47bb7bcbc5862e562",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Average.cs
                "951310243334a3148a7872977cb31c5c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Buffer.cs
                "edebeae8b61352b428abe9ce8f3fc71a",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Cast.cs
                "6cb07f6e88287e34d9b9301a572284a5",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/CombineLatest.cs
                "7cb9e19c449127a459851a135ce7d527",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Concat.cs
                "36ab06d30f3223048b4f676e05431a7f",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Contains.cs
                "e606d38eed688574bb2ba89d983cc9bb",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Count.cs
                "0202f723469f93945afa063bfb440d15",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Create.cs
                "19e437c039ad7e1478dbce1779ef8660",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/DefaultIfEmpty.cs
                "8f09903be66e5d943b243d7c19cb3811",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Distinct.cs
                "0351f6767df7e644b935d4d599968162",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/DistinctUntilChanged.cs
                "dd83c8e12dedf75409b829b93146d130",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Do.cs
                "c835bd2dd8555234c8919c7b8ef3b69a",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/ElementAt.cs
                "4fa123ad6258abb4184721b719a13810",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Empty.cs
                "38c1c4129f59dcb49a5b864eaf4ec63c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Except.cs
                "417946e97e9eed84db6f840f57037ca6",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/First.cs
                "ca8d7f8177ba16140920af405aea3fd4",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/ForEach.cs
                "a2de80df1cc8a1240ab0ee7badd334d0",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/GroupBy.cs
                "7bf7759d03bf3f64190d3ae83b182c2c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/GroupJoin.cs
                "93999a70f5d57134bbe971f3e988c4f2",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Intersect.cs
                "dc4ff8cb6d7c9a64896f2f082124d6b3",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Join.cs
                "a0ccc93be1387fa4a975f06310127c11",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Last.cs
                "198b39e58ced3ab4f97ccbe0916787d5",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/LongCount.cs
                "5c8a118a6b664c441820b8a87d7f6e28",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Max.cs
                "57ac9da21d3457849a8e45548290a508",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Min.cs
                "2d6da02d9ab970e4999daf7147d98e36",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/MinMax.cs
                "8b307c3d3be71a94da251564bcdefa3d",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Never.cs
                "111ffe87a7d700442a9ef5af554b252c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/OfType.cs
                "413883ceff8546143bdf200aafa4b8f7",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/OrderBy.cs
                "cddbf051d2a88f549986c468b23214af",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Pairwise.cs
                "93c684d1e88c09d4e89b79437d97b810",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Publish.cs
                "b7ea1bcf9dbebb042bc99c7816249e02",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Queue.cs
                "d826418a813498648b10542d0a5fb173",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Range.cs
                "3819a3925165a674d80ee848c8600379",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Repeat.cs
                "4313cd8ecf705e44f9064ce46e293c2c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Return.cs
                "b2769e65c729b4f4ca6af9826d9c7b90",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Reverse.cs
                "dc68e598ca44a134b988dfaf5e53bfba",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Select.cs
                "d81862f0eb12680479ccaaf2ac319d24",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/SelectMany.cs
                "b382772aba6128842928cdb6b2e034b0",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/SequenceEqual.cs
                "1bcd3928b90472e43a3a92c3ba708967",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Single.cs
                "9c46b6c7dce0cb049a73c81084c75154",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Skip.cs
                "df1d7f44d4fe7754f972c9e0b6fa72d5",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/SkipLast.cs
                "de932d79c8d9f3841a066d05ff29edc9",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/SkipUntil.cs
                "4b1a778aef7150d47b93a49aa1bc34ae",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/SkipUntilCanceled.cs
                "0b74b9fe361bf7148b51a29c8b2561e8",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/SkipWhile.cs
                "263479eb04c189741931fc0e2f615c2d",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Subscribe.cs
                "4149754066a21a341be58c04357061f6",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Sum.cs
                "42f02cb84e5875b488304755d0e1383d",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Take.cs
                "510aa9fd35b45fc40bcdb7e59f01fd1b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/TakeLast.cs
                "12bda324162f15349afefc2c152ac07f",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/TakeUntil.cs
                "e82f498cf3a1df04cbf646773fc11319",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/TakeUntilCanceled.cs
                "bca55adabcc4b3141b50b8b09634f764",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/TakeWhile.cs
                "9d05a7d4f4161e549b4789e1022baae8",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Throw.cs
                "debb010bbb1622e43b94fe70ec0133dd",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/ToArray.cs
                "03b109b1fe1f2df46aa56ffb26747654",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/ToDictionary.cs
                "7a3e552113af96e4986805ec3c4fc80a",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/ToHashSet.cs
                "3859c1b31e81d9b44b282e7d97e11635",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/ToList.cs
                "57da22563bcd6ca4aaf256d941de5cb0",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/ToLookup.cs
                "b4f6f48a532188e4c80b7ebe69aea3a8",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/ToObservable.cs
                "d7192de2a0581ec4db62962cc1404af5",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/ToUniTaskAsyncEnumerable.cs
                "ae57a55bdeba98b4f8ff234d98d7dd76",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Union.cs
                "5c01796d064528144a599661eaab93a6",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/UniTask.Linq.asmdef
                "d882a3238d9535e4e8ce1ad3291eb7fb",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Where.cs
                "acc1acff153e347418f0f30b1c535994",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/Zip.cs
                "00520eb52e49b5b4e8d9870d6ff1aced",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/UnityExtensions/EveryUpdate.cs
                "1ec39f1c41c305344854782c935ad354",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/UnityExtensions/EveryValueChanged.cs
                "382caacde439855418709c641e4d7b04",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Linq/UnityExtensions/Timer.cs
                "ef2840a2586894741a0ae211b8fd669b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Triggers/AsyncAwakeTrigger.cs
                "f4afdcb1cbadf954ba8b1cf465429e17",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Triggers/AsyncDestroyTrigger.cs
                "b4fd0f75e54ec3d4fbcb7fc65b11646b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Triggers/AsyncStartTrigger.cs
                "2c0c2bcee832c6641b25949c412f020f",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Triggers/AsyncTriggerBase.cs
                "59b61dbea1562a84fb7a38ae0a0a0f88",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Triggers/AsyncTriggerExtensions.cs
                "c30655636c35c3d4da44064af3d2d9a7",  // Packages/com.vrchat.base/Runtime/VRCSDK/Plugins/UniTask/Runtime/Triggers/MonoBehaviourMessagesTriggers.cs
                "68be9f0f6e5adbd44a76bf6bf69fda7b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/BrightButton.mat
                "9414e644b0d9d4c4cb1d863093f0284c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/Chair.mat
                "b6099d83d6f02e34ea589e768df4173b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/Green.mat
                "34348aa1b91e32f48bda8333f82f6335",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/GUI_Gradient_Ground.mat
                "4546b0ec54086e840800d63eb723acd2",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/GUI_Zone_Holo.mat
                "c815f7613a04b724089c206857e57c6a",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/MirrorReflection.mat
                "7a2568654af4bef4cad7a3dfa02c31b2",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/Red.mat
                "4a04f8d3981104848915e66f7a02ec72",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/Screen.mat
                "1278163a2a3ba2b4cad540a862292784",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/PanoViewer/Panosphere.shader
                "26803b57669325843a97b0ae43031082",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/PanoViewer/Sphere.mat
                "4876fc9dc009bbe4493553020a561611",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_black_grid.mat
                "eae9c11350249284e8400a100179e0b2",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_blue_grid.mat
                "1ab66d94bde8cce46bb35638099bfd31",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_grey_smooth.mat
                "76ff537c8e1a84345868e6aeee938ab3",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_navy_grid.mat
                "1032d41f900276c40a9dd24f55b7d420",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_navy_smooth.mat
                "8c19a618a0bd9844583b91dca0875a34",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_pink_grid.mat
                "fed4e78bda2b3de45954637fee164b8c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_pink_smooth.mat
                "5aa95b3fa56e28f43a84e301c3d19e08",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_white_grid.mat
                "799167b062f9e2944a302eea855166b4",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_white_smooth.mat
                "82096aab38f01cb40a1cbf8629a810ba",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_yellow_grid.mat
                "6e1d36c4bbd37d54f9ea183e4f5fd656",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_yellow_smooth.mat
                "622a87b3379022740be7e2efea3ebd33",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_block_04x04x04.fbx
                "00718395eefb6084bb25555f962f25c0",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_coin_01x01x01.fbx
                "df4796b594b970842b69211cb0078c5d",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_cube_02x02x02.fbx
                "3f79402ff4ca9c54d96a09d1a77540d5",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_cube_04x04x04.fbx
                "c09052c9b19f0ea4987bc4f4f981252f",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_cube_08x08x08.fbx
                "16fb769c0394c36469ed40a4f35c1eec",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_floor_08x01x08.fbx
                "080bc076ed19adb4091adca05de83d66",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_floor_4x1x4.FBX
                "fadddc63520db414bbc9126cbf4743ad",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_floor_64x01x64.fbx
                "ce7348d724aa0fc44aaf53391b9bae9b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_house_16x16x24.fbx
                "f45b6695d6226cd48abfc605723cc3ae",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_join_inner_01x06x01.fbx
                "40384240c1c82b94db82531689571ab0",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_join_mid_04x06x01.fbx
                "6386a10e23c45d040a22051e6ae3b70f",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_join_outer_02x06x02.fbx
                "25712b9d3dd0eb4439390fb8fea8043e",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_pillar_01x02x01.fbx
                "66a13889798137c498eae4b3acdafe19",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_pillar_02x08x02.fbx
                "38a9d3cc5c1e0aa4f92ff3445b73ed7f",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_platform_02x01x02.fbx
                "bc2ed85df3924a4458576f17e8b10057",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_platform_04x01x04.fbx
                "879dd62cbfd65314d812354e257fc5cc",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_platform_8x1x8.FBX
                "b9d7ac1a0f551404f8d32e1e02b64325",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_ramp_04x02x02.fbx
                "900e53dd850c9cc4281be6fa21bdfea5",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_steps_4x2x2.FBX
                "b5290684820a94548bedb95083785116",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/prototype_wall_8x8x1.FBX
                "4cfb7ae289eb1e546b751d287bc1ee62",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/Materials/NavyGrid.mat
                "22a917a65630c404e8ebe2c26a9c7d5e",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/Materials/PinkSmooth.mat
                "a196fd6788131ec459bfb26012466fc1",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Textures/GridEmissive.png
                "efaaea7f6a25a4d4fafa9fce85bf947f",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Textures/prototype_black_dff.png
                "3cae02495b88d2d4fbf19382b7993691",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Textures/prototype_blue_dff.png
                "33a18574a1737ab42a75137c3b83c9be",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Textures/prototype_white_dff.png
                "c3edc74ae8207fd45a93c4ed8ee27567",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Textures/SwatchMauveAlbedo.png
                "86e4aa9207c9e2740b6ace599d659c05",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Textures/SwatchNavyAlbedo.png
                "a336ccf90791f9841b7e680c010d1e88",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Textures/SwatchNavyDarkAlbedo.png
                "8b939c5b46fae7e49af7d85f731ba4ec",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Textures/SwatchOrangeAlbedo.png
                "580615edf5e29d245af58fc5fe2b06ac",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Textures/SwatchPinkDAlbedo.png
                "590546bcbd472d94e874f6e0c76cc266",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Textures/SwatchTealAlbedo.png
                "9c4d7ee42c7d4f944b2ce9d370fa265c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Textures/SwatchTurquoiseAlbedo.png
                "9d0b29cecf2678b41982d2173d3670ff",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Textures/SwatchWhiteAlbedo.png
                "b4646ae63b0bcca40b1bdde3b87e01bf",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Textures/SwatchYellowAlbedo.png
                "693137b858e4dc64c83be531351f45e6",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/Mirror.shader
                "9788d723ed7eac946a9a599e4a6ba940",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/Video-RealtimeEmissiveGamma.shader
                "5f8fef09682fab74fb7a29d783391edb",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/VRChat-Sprites-Default.shader
                "9ae8ad653e1d98940bbc79866b9170f3",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/VRChat-Sprites-Diffuse.shader
                "f8c1f8ac363df824899534a0b30eef00",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/Mobile/VRChat-Mobile-BumpedDiffuse.shader
                "528d55c4e8adab14b974ca665ed1b996",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/Mobile/VRChat-Mobile-BumpedMappedSpecular.shader
                "584dc70fbb9834e48beb29e3206e3ca0",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/Mobile/VRChat-Mobile-BumpedSpecular.shader
                "2dcd9e0568e0a6f45b92c60ba2eb16a0",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/Mobile/VRChat-Mobile-Diffuse.shader
                "b1f7ecc80417c414b9d62ce541d5bcbf",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/Mobile/VRChat-Mobile-Lightmapped.shader
                "3ad043b7f9839cb48a75a9238d433dec",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/Mobile/VRChat-Mobile-MatCapLit.shader
                "9200bec112b65ec4fbbbd33fa89c20f4",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/Mobile/VRChat-Mobile-Particle-Add.shader
                "8b39b95ac85682040beff730e0cfc77a",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/Mobile/VRChat-Mobile-Particle-Alpha.shader
                "d5b89f0c74ccf5049ba803c14a090378",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/Mobile/VRChat-Mobile-Particle-Multiply.shader
                "c0d3cb006bb294142bef136f492f2568",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/Mobile/VRChat-Mobile-Skybox.shader
                "0b7113dea2069fc4e8943843eff19f70",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/Mobile/VRChat-Mobile-StandardLite.shader
                "affc81f3d164d734d8f13053effb1c5c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Shaders/Mobile/VRChat-Mobile-ToonLit.shader

                "78d28f00a65b83742b5e85510114747e",  // Packages/com.vrchat.worlds/license.txt
                "067f9b5cc16a52649985a5947e355556",  // Packages/com.vrchat.worlds/package.json
                "84de2da7fe8ad8e439c084731189bc12",  // Packages/com.vrchat.worlds/Editor/Udon/UdonBehaviourEditor.cs
                "66ebdaa27f6d2d54cbb62abddc493674",  // Packages/com.vrchat.worlds/Editor/Udon/UdonEditorManager.cs
                "5ce0cc0dd464fa94bb634ed46d88c48b",  // Packages/com.vrchat.worlds/Editor/Udon/UdonImportPostProcessor.cs
                "627c4d5cd580ddf41bd320e784fe8b9d",  // Packages/com.vrchat.worlds/Editor/Udon/VRC.Udon.Editor.asmdef
                "fb263072af774326bec32396a5bef7ba",  // Packages/com.vrchat.worlds/Editor/Udon/Editor/DataPropertyDrawer.cs
                "8b6535096cfa29340897276abbdd015f",  // Packages/com.vrchat.worlds/Editor/Udon/External/VRC.Udon.Compiler.dll
                "585dd63e377866248b16bdba915820ed",  // Packages/com.vrchat.worlds/Editor/Udon/External/VRC.Udon.EditorBindings.dll
                "b335798a4f28bec40ba9b3d4a15acee7",  // Packages/com.vrchat.worlds/Editor/Udon/External/VRC.Udon.Graph.dll
                "21dcba1a47cc8c84381629950b692129",  // Packages/com.vrchat.worlds/Editor/Udon/External/VRC.Udon.UAssembly.dll
                "161140ecae894b84ba7bdd6e44ff4371",  // Packages/com.vrchat.worlds/Editor/Udon/External/VRC.Udon.VRCGraphModules.dll
                "19cff77330d183441a69ff6c69e07629",  // Packages/com.vrchat.worlds/Editor/Udon/External/VRC.Udon.VRCTypeResolverModules.dll
                "cac80b40f57c41d4b941dc5059271583",  // Packages/com.vrchat.worlds/Editor/Udon/GraphModules/VRCInstantiateNodeRegistry.cs
                "e1b5b45f24b268b42826fc5c5497dc15",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/SerializedUdonProgramAssetEditor.cs
                "0e5ced9511d591140b191bbd9e948e61",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/Attributes/UdonProgramSourceNewMenuAttribute.cs
                "22203902d63dec94194fefc3e155c43b",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonAssemblyProgram/UdonAssemblyProgramAsset.cs
                "3df823f3ab561fc43bcb81286e14b91d",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonAssemblyProgram/UdonAssemblyProgramAssetEditor.cs
                "3c0638314c289c24193b47d1c53c9fca",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonAssemblyProgram/UdonAssemblyProgramAssetImporter.cs
                "4f11136daadff0b44ac2278a314682ab",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UdonGraphProgramAsset.cs
                "31d6811854f59254aa1a263a8d566eb2",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UdonGraphProgramAssetEditor.cs
                "57422d3fdb0cc124189c68f87b7157cd",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/UdonGraphExtensions.cs
                "e2f2300f99ce0ea4a8d9a20b464384df",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/TypeExtension.cs
                "dfca99fb2e099d84da6c504cd521aa70",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/UdonFieldFactory.cs
                "9214873dab0ea8a4b91861cd5a04dae3",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/UdonGraph.cs
                "f166d8f1c152ef34899019ab9a4fd0f2",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/UdonGraphElementData.cs
                "54dd824c6c614b94183d92710efe4f5f",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/UdonGraphStatus.cs
                "87e2044d3bcb715499ac68cc7380a9ed",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/UdonGraphViewSettings.cs
                "c6f017dc2674fec4da54a57b2655a948",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/UdonGraphWindow.cs
                "5dcd92112af21784ba5bf6383abab768",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/UdonParameterField.cs
                "70616b8b964e3664780fc03f65f27f4f",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/UdonParameterProperty.cs
                "fddc146e8502d7b49a294b6264d66dfd",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/UdonProgramSourceView.cs
                "e5786fc577943ae45953c6f54c97116b",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/UdonWelcomeView.cs
                "aabdd863f82551d40bd3a1b0835d2fc3",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/VideoPlayerElement.cs
                "c66f97d5a5a38cb478fd4df11ece7be7",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Fields/ByteField.cs
                "602ffcf431b3e4f41a18bd868751439a",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Fields/CharField.cs
                "e79144dad56140a7bcd0d9f945153784",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Fields/DecimalField.cs
                "42d9183d7c7ce67448d1e010456e36f9",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Fields/LayerMaskField.cs
                "2a6b99e05a39452082bd89b0feec45ca",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Fields/MirrorReflectionClearFlagsField.cs
                "1c96407d2b7698c4c8a0476efa2c765d",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Fields/QuaternionField.cs
                "42747856e1884be6b1112c8838963662",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Fields/SByteField.cs
                "bcda1561abdb40c69f9eeb9211bd3e3d",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Fields/ShortField.cs
                "0b91b14ff1e24276863f173d0e9c760c",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Fields/UnsignedIntegerField.cs
                "1d1c8af690a94ef0a670e5d321733414",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Fields/UnsignedLongField.cs
                "1387a4616a8f4c87bd0d55d2ffc021c8",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Fields/UnsignedShortField.cs
                "22a713ed81ae4c7e9ad58760232ce573",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Fields/VideoErrorField.cs
                "eeb751ae1c234a04291c5039626f3470",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Fields/VRCUrlField.cs
                "469db50616185d04e8a46dcd75db12d2",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/GraphElementExtension.cs
                "7f257a6eeae213a4db991d486cace003",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonArrayEditor.cs
                "f4f0ade55ae13b6468a765826f1f2540",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonArrayInspector.cs
                "7e5916b8dd19e4445a9156a457b82ee4",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonComment.cs
                "ba3ecc4c46929404d8c2ec920743b823",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonGraphElement.cs
                "75df7c5698fa41118a907e0f229bb537",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonGraphEvents.cs
                "2adbf86181214870b117bdc6c99946d8",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonGraphGroups.cs
                "1889c18ccf7e41c7af45075ff3ad65b6",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonGraphToolbar.cs
                "346f4d7e451c44daaaacbf470d40c254",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonGraphVariables.cs
                "1b8045222a10ce04b815642b9cd5ca17",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonGroup.cs
                "dcd657bc1dcf357448d27bcfa8c5dc36",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonNode.cs
                "8f83d1d3578dd28498c71a980bca86dd",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonPort.cs
                "600349daea5c49d38adcfc76129b464c",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonSidebar.cs
                "1d5984c5be753da439b8a33ffbee8d36",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonStackNode.cs
                "5eb9a93bc4c04034bfc8cbb36063588b",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonVariableRow.cs
                "cbfa6b1c2cf44feca09853837fc740bb",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonNodes/GetOrSetProgramVariableNode.cs
                "8164fc2c5c5b43428503cf064e8b53f0",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonNodes/SendCustomEventNode.cs
                "6e65118bfe2d43f1ad2412dba47c21ee",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonNodes/SetReturnValueNode.cs
                "fd9209fd8a363ee42a49a2afaeb35805",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonNodes/SetVariableNode.cs
                "00b770580aae40ffb9f6a1d898b52269",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/GraphElements/UdonNodes/UdonNodeExtensions.cs
                "f9a3f47510be4a35893681f29632b399",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Search/UdonEventTypeWindow.cs
                "6581176c97993bb40976acff208bd0b1",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Search/UdonFocusedSearchWindow.cs
                "b721120e6c1d320448a55fe87a7de824",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Search/UdonFullSearchWindow.cs
                "e94c084f399869b42a21244fd07778c4",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Search/UdonPortSearchWindow.cs
                "6a6c453fae11b5349a33399e258d1578",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Search/UdonRegistrySearchWindow.cs
                "e5a10bb1987c27944bd08a88119b2844",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Search/UdonSearchManager.cs
                "d825ed3ba6aa7f14294e73efefc217d0",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Search/UdonSearchWindowBase.cs
                "16fc7a7a059deeb458fdcdf719b467a4",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonGraphProgram/UI/GraphView/Search/UdonVariableTypeWindow.cs
                "264ec3c8a1d423f42a144da0df6c5ebe",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonProgram/UdonProgramAsset.cs
                "41d70977fa7936441afe41442f1862b2",  // Packages/com.vrchat.worlds/Editor/Udon/ProgramSources/UdonProgram/UdonProgramAssetEditor.cs
                "9e84f8ee45862f04ca6b9f8d5c7f5897",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/CornerResize.png
                "632470b93f35ec64ab6e3efd639c986c",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/DarkButtonBG.png
                "d4ca7f47895ab36408e28f4f742fba99",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/DropdownBG.png
                "d6fa9a7a95d88a74cb6f65a1e6609d32",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/TextBoxBG.png
                "9769aa15c96ab354fac2153792524e81",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/TextBoxBGFocused.png
                "f43fd332539599c47b3bb05ea38d5d0d",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/ToolbarBG.png
                "5cbfe49b858635b44844a178cb934b68",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/ToolbarButtonBG.png
                "7dade49b2f58f734f8db0983d8e7fb60",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonChangelog.uxml
                "927841c571a405846b3442bc0aa56220",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonFlowSlot.png
                "3803fec4c7b065042891595e749524cc",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonFlowSlotFilled.png
                "7c75c00422f12124faed19bfb8dd96df",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonFlowSlotFilledLight.png
                "610088fc92e5fc64b8c7f9e9c51f2939",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonFlowSlotLight.png
                "d47fd176596dfbe4e9e78964b40c93ee",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonGraphNeonStyle.uss
                "815baa9989198624aa5fec5ecdb42bd0",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonGraphStyle.uss
                "0e2cfcbd717e75441b108d3ad9de2d29",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonLogo.png
                "8cf68553c5a4bb140a6341072891aa88",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonLogoAlpha.png
                "d0608d33a4043b2499adb1fee18f2a64",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonLogoAlphaWhite.png
                "17102758d03099542afc7a1808745eaf",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonNodeAccent.png
                "c0230adfeb2abe242b8d64c7e3bd2adc",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonNodeActiveBackground.png
                "8289cc16393cd3040a9920e71bfe10bc",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonNodeActiveBackgroundLight.png
                "f47842ead2f80fa46ab6e5bbde409193",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonNodeBackground.png
                "c9235631e37566447ae4567624755326",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonNodeBackgroundLight.png
                "2d2675a75fea1d2438859bdb320d544d",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonNodeInlay.png
                "12f29a8be9fc52640b40f6ffa59336c6",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonNodeInlayLight.png
                "1ed47570201e1854d9e455e38eecbcf7",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonSettings.uxml
                "4c886ef66add4a8baf2e1fa8821e9051",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonSidebarStyle.uss
                "91b7c8d7d899ec04e9568e9385aba34d",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonSlot.png
                "3a1ab76e09365f14cab0665b40da8843",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonSlotFilled.png
                "add07ab72e2fc3d4d81143ab77d121f5",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonSlotFilledLight.png
                "1badb339ed4f23541b6db8a9420aeea9",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonSlotLight.png
                "f1fd7e207f0a43858c07270980607232",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/UdonToolbarStyle.uss
                "37bd184e5e9b13945840f70329f2e0f6",  // Packages/com.vrchat.worlds/Editor/Udon/Resources/videoStill.png
                "c041fa712f66a5d4f8525cd447dc8b29",  // Packages/com.vrchat.worlds/Editor/Udon/TypeResolvers/UdonBehaviourTypeResolver.cs
                "f0f0e9c6659d5434399d69c33611db57",  // Packages/com.vrchat.worlds/Editor/Udon/UnityEditorTests/DataDictionaryTests.cs
                "17ace30fb4afe624387090408df4e98d",  // Packages/com.vrchat.worlds/Editor/Udon/UnityEditorTests/DataListTests.cs
                "4da3a4e08f2d1d541831ff4bb3a21a03",  // Packages/com.vrchat.worlds/Editor/Udon/UnityEditorTests/DataTokenTests.cs
                "aff2e127ee728e643af3f14dd1dd3b3e",  // Packages/com.vrchat.worlds/Editor/Udon/UnityEditorTests/JsonDictionaryTests.cs
                "31e2f9e37f4abc149ba2afe12177f907",  // Packages/com.vrchat.worlds/Editor/Udon/UnityEditorTests/JsonListTests.cs
                "0dcda41e86c546c4c978d0ef060d17bd",  // Packages/com.vrchat.worlds/Editor/Udon/UnityEditorTests/Tests.asmdef
                "b5ecdc6e48942134fb203261251a26c2",  // Packages/com.vrchat.worlds/Editor/Udon/UnityEditorTests/UdonGraphSettingsTests.cs
                "02e7e7f5f9fc2c24ab3af0b8780f3623",  // Packages/com.vrchat.worlds/Editor/Udon/UnityEditorTests/UICompilerTests.cs
                "cccff2f4f65049041acd7bc5eaa352e8",  // Packages/com.vrchat.worlds/Editor/Udon/UnityEditorTests/VRCJsonTests.cs
                "d49885815537cc443beee0124d74cab0",  // Packages/com.vrchat.worlds/Editor/Udon/UnityEditorTests/Scenes/Resources/UdonEditorTestScene.unity
                "953e2e6278cc9314f9f2913d9bc25309",  // Packages/com.vrchat.worlds/Editor/Udon/UnityEditorTests/UdonProgramSources/Resources/MaxAllPlayerVoice-TestGraph.asset
                "da113172081f2ba40b9cc46674a846d0",  // Packages/com.vrchat.worlds/Editor/Udon/UnityEditorTests/UdonProgramSources/Resources/SyncedSlider-TestGraph.asset
                "1b3abf7e59395d84c84b48d69677daaa",  // Packages/com.vrchat.worlds/Editor/VRCSDK/VRC.SDK3.Editor.asmdef
                "6ef91745551f415993180eaec9a0c74d",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/PublishConfirmationWindow.cs
                "5b56ebc0667f4a2eb02271d4b50dfb25",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/SampleImporter.cs
                "857dfa28ea5a4d00b87e04081e0d68e2",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/SDK3ImportFix.cs
                "926862297eaa5434cb1640ece9bd8510",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/VRCMidiWindow.cs
                "c9dee190dbea4fd0a14ebadf422e85e8",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/VRCPackageSettings.cs
                "f2a720a30f1043247af7742fdfd0b8e5",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/VRCSdkControlPanelWorldBuilder.cs
                "a83d9088aed84961a3e68a859a9112c6",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/WorldBuilderConstants.cs
                "46af4822c923da1479c899b6b5e2e458",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/WorldBuilderSessionState.cs
                "d57b23c04034119448f23c5fdbc57662",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Components3/VRCDestructibleUdonEditor.cs
                "5b1017097f3d46cd949892e0fce3ece9",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Components3/VRCMidiEditor.cs
                "88390b175b72faa49b553b95cbcac908",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Components3/VRCMidiEditorVisualizer.cs
                "76fb4f8f2f07c024795a6acecfeaefcc",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Components3/VRCMidiPlayerEditor.cs
                "8901d07a685ca424492a3cabff506184",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Components3/VRCPlayerStationEditor3.cs
                "4b2b9ac625bc5b04c887ff9ee9b5fdbe",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Components3/VRCSceneDescriptorEditor3.cs
                "a8cc4c1876b26174fbaeb062178a6bda",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Components3/VRC_PickupEditor3.cs
                "3f8f999a8e1ebee4588f94a8a618d7c6",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Components3/VRC_SpatialAudioSourceEditor3.cs
                "8917c827433f542428ebdaf7cc31bf34",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Elements/WorldBuildSuccessNotification/WorldBuildSuccessNotification.cs
                "236433fe0b3378242a1b83d2199b9d27",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Elements/WorldBuildSuccessNotification/Resources/WorldBuildSuccessNotification.uxml
                "a4675f35fd0b48947b0f1870568c7209",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Elements/WorldBuildSuccessNotification/Resources/WorldBuildSuccessNotificationStyles.uss
                "75cec36013175db44880a380b8f9b334",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Elements/WorldUploadErrorNotification/WorldUploadErrorNotification.cs
                "ceb22344bc36ee24cb99cf412a7534d7",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Elements/WorldUploadErrorNotification/Resources/WorldUploadErrorNotification.uxml
                "b7d711ed3e43b3b4cbdc2b976f450581",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Elements/WorldUploadErrorNotification/Resources/WorldUploadErrorNotificationStyles.uss
                "11e30a0715a549f4086eadde63e2d2de",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Elements/WorldUploadSuccessNotification/WorldUploadSuccessNotification.cs
                "e0cb08821623b5c46ae5e6a0b1f3e2b1",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Elements/WorldUploadSuccessNotification/Resources/WorldUploadSuccessNotification.uxml
                "501977f645ca1b947b49df74d5162a8b",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Elements/WorldUploadSuccessNotification/Resources/WorldUploadSuccessNotificationStyles.uss
                "dd8923a5088f4aa89bcd97d8cdab1dbe",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Public SDK API/IVRCSdkWorldBuilderApi.cs
                "019a4b561d5d4614a85046e7a1c58751",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Resources/PublishConfirmationWindow.uxml
                "da1f468c8c7846afab7d03f3f4c52139",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Resources/PublishConfirmationWindowStyles.uss
                "fddc9f028e2d45a097fc34a58821d37b",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Resources/VRCSdkWorldBuilderBuildLayout.uxml
                "3924ac38bc424700bf8f1f30557680d2",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Resources/VRCSdkWorldBuilderBuildStyles.uss
                "f7edf86396674750aa7d941446bba8fe",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Resources/VRCSdkWorldBuilderContentInfo.uxml
                "140c4a1abe4d4a5bb6ebfc5853bd8f22",  // Packages/com.vrchat.worlds/Editor/VRCSDK/SDK3/Resources/VRCSdkWorldBuilderContentInfoStyles.uss
                "267e20b7d3e9da34eaddc193c847e05e",  // Packages/com.vrchat.worlds/Integrations/ClientSim/License.md
                "3a343146949d3e342ba5abcfe6c4c808",  // Packages/com.vrchat.worlds/Integrations/ClientSim/README.md
                //"",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Documentation~/Overview.md
                "c2a0f4918cb6411b912ba5d1dd9e43fd",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Editor/ClientSimEditorRuntimeLinker.cs
                "96e35a33b2567c04bae44e25e4994158",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Editor/VRC.ClientSim.Editor.asmdef
                "656208559eae27245bd3efa317c971ec",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Editor/Editors/ClientSimObjectPoolHelperEditor.cs
                "f6279c2de63b5b040bc9d4c9c52558cf",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Editor/Editors/ClientSimObjectSyncHelperEditor.cs
                "5c1ea3d6c56287f4f96ffe530c5180bc",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Editor/Editors/ClientSimSpatialAudioHelperEditor.cs
                "dac4b19c793b4b04989e17f3979de4eb",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Editor/Editors/ClientSimSyncableEditorHelper.cs
                "c921b1cc91fd76140b3200b9f7ff0ac8",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Editor/Editors/ClientSimUdonHelperEditor.cs
                "7b749a2c3c49f074cbaac7ef4353ac9f",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Editor/ProjectSettings/ClientSimInputAxesSettings.cs
                "bac8caddcef14fa9a80bf167d612dbaf",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Editor/ProjectSettings/ClientSimProjectSettingsSetup.cs
                //"",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Editor/Resources/.ClientSimInputManagerRaw.asset
                "3c6d75275331c9e4fb6acb2ec61ef6ab",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Editor/Resources/ClientSimInputManager.asset
                "9447dfb6ba20ab1479485c89c34cf4a9",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Editor/Windows/ClientSimSettingsWindow.cs
                "bfb84e0933b14ae380634bcd70b18a70",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/AssemblyInfo.cs
                "29747eaef1c584647a4e877c3f8bc61b",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/ClientSimBehaviour.cs
                "47cdc21b10bf4c78890cfe48793dbd14",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/ClientSimRuntimeLoader.cs
                "fbe77e2f7bc98c64b9a0829b88843fc6",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/VRC.ClientSim.asmdef
                "8474ec07ef88d52458e79ca0f5240d96",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/CameraStacking/ClientSimStackedCamera.cs
                "1704c94808cb3c6479f3dc63042a956a",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/CameraStacking/ClientSimStackedVRCameraSystem.cs
                "d6ef66aee2eba604da699a98a28bba17",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/CameraStacking/Cameras/Cam_InternalUI.asset
                "039d8e7360df43b2a1b0b5f387761dce",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Events/ClientSimEventDispatcher.cs
                "5d6d64ca05845d34bb6753f2cc018bfd",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Events/EventTypes/ClientSimCombatSystemEvents.cs
                "23f6deb432924c9f898f7f83f000045b",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Events/EventTypes/ClientSimCurrentHandEvent.cs
                "4d4f50777a7b4f5ba8f64f4fe7cb04c4",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Events/EventTypes/ClientSimInteractEvent.cs
                "334e4be0c4ea3a34d86e69054d5adc66",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Events/EventTypes/ClientSimMenuEvents.cs
                "efb979a4880368e4997d8efe6882a914",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Events/EventTypes/ClientSimMouseReleasedEvent.cs
                "c994dd199309cb84c843fe843584cdbb",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Events/EventTypes/ClientSimOnPlayerEvents.cs
                "daf6e253eca84774b6643ef83c0594e7",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Events/EventTypes/ClientSimPickupEvents.cs
                "609324b47ca7465991701242bb8a9e0d",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Events/EventTypes/ClientSimRaycastHitResultsEvent.cs
                "09530bd2269622c4385b1db5178aa0a4",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Events/EventTypes/ClientSimReadyEvent.cs
                "91bfef23cc10ff640880e382bf79c390",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Events/Interfaces/IClientSimEvent.cs
                "b6db79d31b2023245b6131565300da0e",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Events/Interfaces/IClientSimEventDispatcher.cs
                "e569e5d477213484e9111d560a39cafc",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/ClientSimCombatSystemHelper.cs
                "aae00de63d785d5468f307e29acf4ab7",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/ClientSimObjectPoolHelper.cs
                "589d092631b45f945ace85beb794e76c",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/ClientSimObjectSyncHelper.cs
                "3202215373c7804408519f37ae740d55",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/ClientSimPickupHelper.cs
                "a751849f43f6c724a8ce7ad9766b132c",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/ClientSimPositionSyncedHelperBase.cs
                "c1c30c850f67fff48a0e69a3e8fa4373",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/ClientSimSpatialAudioHelper.cs
                "989fabbff35c3d845b1b0363b1709ea7",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/ClientSimStationHelper.cs
                "47cf12a423d907241aecd58f6c25b67f",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/ClientSimUdonHelper.cs
                "21636088934f3b046ae75a139062188f",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/Interfaces/IClientSimInteractable.cs
                "055ff67a9eb2dd64eb2c06f4df3626e5",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/Interfaces/IClientSimPickupable.cs
                "e92c80793952b604689f26d846684351",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/Interfaces/IClientSimPickupHandler.cs
                "4b6da4177d7e4ca78b196bbf95952162",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/Interfaces/IClientSimPositionSyncable.cs
                "c5fb7511272166a46844d934faa1c807",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/Interfaces/IClientSimRespawnHandler.cs
                "74b90df615347ae4ea37ca4ad194ae63",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/Interfaces/IClientSimStation.cs
                "c3297e7438270ea4cb73974aaad463b9",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/Interfaces/IClientSimStationHandler.cs
                "58e4590a77a47fa429b2271e49050c99",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/Interfaces/IClientSimSyncable.cs
                "d9cad5ab39e663849a092d187a23aa10",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Helpers/Interfaces/IClientSimSyncableHandler.cs
                "606ef90f0aab477f88f3df2bd3dece3b",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Input/ClientSimInputActionBased.cs
                "9f663f3afb8c43b9bde934ce5e27b320",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Input/ClientSimInputBase.cs
                "b27b7e912c8f06b47b47730996323943",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Input/ClientSimInputManager.cs
                "2a08897387d5ffd4795ccec0e8384a6a",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Input/ClientSimInputMapping.inputactions
                "1e963688a1c048ddac34d738cc8f3072",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Input/ClientSimUdonInput.cs
                "5fc129f391f3488e947eb13a9208bae4",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Input/ClientSimUdonInputBehaviour.cs
                "4025ed62957542d2b0609cfa37d16946",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Input/ClientSimUdonManagerInputEventSender.cs
                "abd396195599a944d932ae13f52f55b5",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Input/Interfaces/IClientSimInput.cs
                "64c08af8f4f24106a5c7d231af1a4ca0",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Input/Interfaces/IClientSimUdonInputEventSender.cs
                "543fc4d5c13242e499030fe4fc23b119",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Interact/ClientSimInteractManager.cs
                "be2b97a47a355084ebe65aea9bdfe938",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Interact/ClientSimRaycaster.cs
                "0d50a8c6a5f9bbd40bd24e97775aa311",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Interact/ClientSimRaycastResults.cs
                "6f25e895d01da4543afa9b7373d727da",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Interact/Interfaces/IClientSimInteractManager.cs
                "c6082ae9f1a154b46a2a8cacb70b4333",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Interact/Interfaces/IClientSimRayProvider.cs
                "873629f8aa3a283488e04aaa159e089c",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Interact/RayProviders/ClientSimCameraRayProvider.cs
                "ceb5186e2cc867941866adee1d5b2072",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Interact/RayProviders/ClientSimTransformRayProvider.cs
                "a0d02ab56371a09488276de93c16f8de",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/ClientSimPlayer.cs
                "ac1020b347eeb4e45afabd4d605aa3ae",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/ClientSimPlayerAvatarManager.cs
                "978f5eb2f40732b4a90c6107b7610ecf",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/ClientSimPlayerController.cs
                "d5604cf6b2917b144a0c023cac36e1b7",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/ClientSimPlayerHand.cs
                "07cd0208a6e39194699dcdb10e3689fc",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/ClientSimPlayerRaycaster.cs
                "661af135a574ca54298877eed2ca87a5",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/ClientSimPlayerStationManager.cs
                "d04f5e2ca86a6fe48854dbfe994387e2",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/ClientSimReticle.cs
                "69178fe96c1e3c448b9a01d50b93eada",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/Interfaces/IClientSimPlayerApiProvider.cs
                "344cfc27cd6427343b0e7b7892893413",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/Interfaces/IClientSimPlayerAudioData.cs
                "9daea73a61febf242b9a4aa963cc0dd7",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/Interfaces/IClientSimPlayerAvatarDataProvider.cs
                "374aacd37bf23bb4dbb349f92df17be0",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/Interfaces/IClientSimPlayerCameraProvider.cs
                "fdbc920da2e346a44992a6071c69023c",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/Interfaces/IClientSimPlayerLocomotionData.cs
                "865cb549269f42b8913b28f067401835",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/Interfaces/IClientSimPlayerPickupData.cs
                "1830e2049be64bfc95285fd733b6944b",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/Interfaces/IClientSimPlayerStationManager.cs
                "6a8ee738cc0b39b42b0f6fdae509f4ac",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/Interfaces/IClientSimPlayerTagsData.cs
                "736c7c603fbe7034ebf2e75b8c986a16",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/Interfaces/IClientSimTrackingProvider.cs
                "9b5b48cc61d892d4b92c2736dc813f7c",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/PlayerData/ClientSimPlayerAudioData.cs
                "ada3387971992d648ae3741d3fae31bc",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/PlayerData/ClientSimPlayerLocomotionData.cs
                "33c07b59a4c05b34fbe53c47ba5000ec",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/PlayerData/ClientSimPlayerPickupData.cs
                "b66bd713ab919834096fa0fc3b58869a",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/PlayerData/ClientSimPlayerTagsData.cs
                "5cdb19253bd15ee4296b716139111626",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/PlayerTracking/ClientSimDesktopTrackingProvider.cs
                "4416d1ef28048a94683820481afedf33",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/PlayerTracking/ClientSimDesktopTrackingRotator.cs
                "062e0aeaac1ea1e4b9c22f6b612b3163",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/PlayerTracking/ClientSimPlayerStanceEnum.cs
                "3d6fc3f30fc48c247855805690cd90cc",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Player/PlayerTracking/ClientSimTrackingProviderBase.cs
                "f950e9463c219da499826fbb7c22258c",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Avatars/VRC_Utility_FullBody/Avatar_Utility.fbx
                "e9a2e259e6f5a5743816abf66a7d5ef9",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Avatars/VRC_Utility_FullBody/Materials/Avatar_Utility_Base_MAT.mat
                "98b4c9a9be35dca4d8c5e65a03ab2706",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Avatars/VRC_Utility_FullBody/Materials/Avatar_Utility_Body_MAT.mat
                "1994f4fc451e42149869677281421a08",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Avatars/VRC_Utility_FullBody/Textures/Avatar_Utility_Base_MAT_AlbedoTransparency.png
                "afdf886ec0372bf43835e45764514cee",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Avatars/VRC_Utility_FullBody/Textures/Avatar_Utility_Base_MAT_MetallicSmoothness.png
                "92ce7c78a3437064d9b781dd692a70e5",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Avatars/VRC_Utility_FullBody/Textures/Avatar_Utility_Base_MAT_Normal.png
                "d0741124752ca474e89ba2d586484bf2",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Avatars/VRC_Utility_FullBody/Textures/Avatar_Utility_Body_MAT_AlbedoTransparency.png
                "0ec90c6e3b7802f4793df205483e45ef",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Avatars/VRC_Utility_FullBody/Textures/Avatar_Utility_Body_MAT_MetallicSmoothness.png
                "80ffcbd97b3b102428eaa5283713fea7",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Avatars/VRC_Utility_FullBody/Textures/Avatar_Utility_Body_MAT_Normal.png
                "5af4354a65e661241b77548083612a12",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimAvatar.prefab
                "1152d1ec7ec6c924e818d98cc0d0c418",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimDesktopReticle.prefab
                "6be65459cc2282a4094483092bc0dfe2",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimDestkopTrackingData.prefab
                "6f484702a206e8b47bff8ffd9d736602",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimHighlightManager.prefab
                "d7a2cfc633369c7468598f029ba01ff1",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimHighlightProxy.prefab
                "4089ec1de896aa647b54a0b3657a64a2",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimInputManager.prefab
                "50a99d7b0c3345e4bb10c8509e2821a6",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimMenu.prefab
                "ca8797bd0f7934e4e98fd29962c732cc",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimPlayerLocal.prefab
                "491f38c4a78d62d42803470bba41c5f6",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimPlayerRemote.prefab
                "d761f4ad543663d41bebbbcb3cc81c94",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimPlayerSpawner.prefab
                "06d1c79a3adb363488e1418d1e7395d5",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimProxyObjects.prefab
                "f6cffc14c9df5594e9aefe10ddcce4f0",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimSyncedObjectManager.prefab
                "6b742e94fd58cbb4fa2fa6920051cde8",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimSystem.prefab
                "148b26d454c218d418e672558fd3e7f7",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimTooltip.prefab
                "ee584a366740d2a4dbebbd4270018e45",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimTooltipManager.prefab
                "16b2ea81633f58e46b86296f827f4a5f",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Prefabs/ClientSimUnityEventSystem.prefab
                "cf547c3b264c80a41bba0a855f9bcdde",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Textures/Background.png
                "8538243211a8c784ca348e834e994dd6",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Textures/ButtonDeselected.png
                "83534af2929fd7e419eb588900853395",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Textures/ButtonSelected.png
                "c12c2cd5f477a72498394a99eae001fa",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Textures/Checkmark.png
                "3aa6c3176ff3ae84fa491b127b51a9b6",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Textures/Circle.png
                "aaeec129fbd795942b12ec48e87c68e5",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Textures/MouseCursor.png
                "f496a127ab46cbe42a3719e5d79530b6",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Resources/ClientSim/Textures/Reticle.png
                "296a1de81b60e451a9b96e4407e7c413",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/SelectionHighlight/HighlightsFX.cs
                "c2f217b24afbcbe409a887140474afa6",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/SelectionHighlight/PostEffectsBase.cs
                "4c3382d604d882e46b871743fd208392",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/SelectionHighlight/Resources/MobileHighlight.shader
                "e113d75de696e6642a3bc03a2613a64b",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Settings/ClientSimAssetPostProcessor.cs
                "2bc626fdc39b1b749bd7f4dbaeede6ff",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Settings/ClientSimSettings.cs
                "90f637f368ac148458fe56865a2462c5",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Stubs/ClientSimAVProVideoStub.cs
                "6b423402f03c4238b2fe097f146f7d01",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimBaseInput.cs
                "a2929dd4844944c799d13b91350be20d",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimBlacklistManager.cs
                "1938047de93c66141923068bf4063db5",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimHighlightManager.cs
                "cdc8e1faef31d1d479cb70d50716fc35",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimHighlightProxy.cs
                "848153f45093e01469743624ef9f9391",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimInputModule.cs
                "341c903cb5594e569aa8b82a45974fcf",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimInteractiveLayerProvider.cs
                "f8dfd7f39e425354dbab5d2c2064fe0a",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimMain.cs
                "fcf28ff06eb64c8aa55abe665a48a198",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimMenu.cs
                "8313fb7d292ed7c40a0f0e6e06a652df",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimPlayerManager.cs
                "79164cc4f26063f4db9016f803a7263d",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimPlayerSpawner.cs
                "a70a3c6d92e24dd489cdd7b4d138a17e",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimProxyObjects.cs
                "1763d0603be7460583cf4e71634eea83",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimSceneManager.cs
                "3f545fcbb2b04e7480e53fd98ae6fbed",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimSessionState.cs
                "f0bd9a5f625c4c04bd9862b62b57f431",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimSyncedObjectManager.cs
                "99cce027a08452740befb63a4966f0f9",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimTooltip.cs
                "a3368d7496423784180eac62c9b35e56",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimTooltipManager.cs
                "340d1d7e8555a8643b0ab107b14e2a78",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimUdonManager.cs
                "39cba4c4dd6f4c9686a240bd330a777c",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/ClientSimUdonManagerEventSender.cs
                "d7e78ffeb0b3def46a3a33bcb52b1ccc",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/Interfaces/IClientSimBlacklistManager.cs
                "0f56beeb09982c24c9b7164de3faccc4",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/Interfaces/IClientSimHighlightManager.cs
                "c004ca50a1e6fdf478a55c1a4e619f98",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/Interfaces/IClientSimInteractibleLayerProvider.cs
                "61b8e9d915d2b1245875c146dfa8ab96",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/Interfaces/IClientSimMousePositionProvider.cs
                "c93456661823b2a4dbab7894ba0c027e",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/Interfaces/IClientSimPlayerManager.cs
                "5c782b965c37d2f47a0f17914838fc16",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/Interfaces/IClientSimProxyObjectProvider.cs
                "7243937d85f17c34ebb98ba8023553a2",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/Interfaces/IClientSimSceneManager.cs
                "6b70f8e88d954ae397f2fd3f09c9ac49",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/Interfaces/IClientSimSessionState.cs
                "46580ba70cfd92e479b924bd1a469c07",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/Interfaces/IClientSimSyncedObjectManager.cs
                "b94475b7722141843ba4e3e8795ab65f",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/Interfaces/IClientSimTooltipManager.cs
                "7dc60a559e4c412db2226c17f35e1a66",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/Interfaces/IClientSimUdonEventSender.cs
                "60d81ad6ae86c9d4db21f724007b16cd",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/System/Interfaces/IClientSimUdonManager.cs
                "e2f5207114724364a99c23559907bd69",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Utilities/ClientSimException.cs
                "2aa1cbed33daf8245ab8d7cef3540953",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Utilities/ClientSimExtensions.cs
                "e25aff8547ec4e39afc88d6b02fd56d2",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Utilities/ClientSimObjectCollection.cs
                "ad42e5696f9d49de93f3282a9b9de082",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Runtime/Utilities/ClientSimResourceLoader.cs
                /*"",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Samples~/ClientSimSelfTests/ClientSimSelfTests.asmdef
                "",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Samples~/ClientSimSelfTests/3-Respawn/ClientSimIssue3RespawnTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Samples~/ClientSimSelfTests/3-Respawn/ClientSimIssue3RespawnTest.unity
                "",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Samples~/ClientSimSelfTests/3-Respawn/ClientSimIssue3RespawnTestObjectReferences.cs
                "",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Samples~/ClientSimSelfTests/3-Respawn/UdonProgramSources/TestRespawnCube Udon Graph Program Asset.asset
                "",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Samples~/ClientSimSelfTests/3-Respawn/UdonProgramSources/TestRespawnWithIndexCube Udon Graph Program Asset.asset
                "",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Samples~/ClientSimWorldTestExample/ClientSimExample.Tests.asmdef
                "",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Samples~/ClientSimWorldTestExample/ClientSimWorldTestExample.unity
                "",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Samples~/ClientSimWorldTestExample/ClientSimWorldTestExampleScene.cs
                "",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Samples~/ClientSimWorldTestExample/ClientSimWorldTestObjectReferences.cs
                "",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Samples~/ClientSimWorldTestExample/UdonPrograms/ClientSimTestDoorController.asset
                "",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Samples~/ClientSimWorldTestExample/UdonPrograms/ClientSimTestPickupDetector.asset
                "",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Samples~/ClientSimWorldTestExample/UdonPrograms/ClientSimTestPlayerDetector.asset
                "",  // Packages/com.vrchat.worlds/Integrations/ClientSim/Samples~/ClientSimWorldTestExample/UdonPrograms/ClientSimTestStation.asset*/
                "de16ec4337e023649b01c85475dc06f9",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/UdonSharpLocator.asset
                "84265b35cca3905448e623ef3903f0ff",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/UdonSharp.Editor.asmdef
                "5136146375e9a0a498a72a0091b40cc1",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/UdonSharpAssemblyDefinition.cs
                "12470032c7664d868b3069ec9498e568",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/UdonSharpAssemblyDefinitionPostProcessor.cs
                "b4510073e1910394e8d1a24243fdef99",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/UdonSharpAssetCompileWatcher.cs
                "71c6e4cb5bd1fbe41b48769962b0cfc5",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/UdonSharpEditorCache.cs
                "0f1d20855b7d9e64c800449a66f05b9c",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/UdonSharpEditorManager.cs
                "687437c9e3823df4592b1aa69eefad45",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/UdonSharpEditorUtility.cs
                "667b852e57f544c593e121b6400c7643",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/UdonSharpPrefabDAG.cs
                "c333ccfdd0cbdbc4ca30cef2dd6e6b9b",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/UdonSharpProgramAsset.cs
                "801304b6e3b062343a5716b0beb36788",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/UdonSharpProgramAssetPostprocessor.cs
                "1fc07ee3faa2d6b439bf3f0a7986c2db",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/UdonSharpRuntimeLogWatcher.cs
                "64c82bfb8e1aa5f499dffe7578d0c67b",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/UdonSharpUpgrader.cs
                "edba4aa7a35d687498cffd75798e0cf7",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/UdonSharpUtils.cs
                "67ef2f829963ac94db0d01f19c3a1caf",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/BuildUtilities/UdonSharpBuildChecks.cs
                "32fdba3afe6c0a74d8714d0803e14c1a",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/BuildUtilities/UdonSharpBuildCompile.cs
                "bcc0ab02ef3e9d84996c029106dfdda6",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/AbstractPhaseContext.cs
                "22d16bfb37d2e8641a205e1652f87693",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/CompilationContext.cs
                "6be5cc5b10e92984794cdb2865d806f1",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/UdonSharpClass.cs
                "6f01446373ecc38428e874f4a7075258",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/UdonSharpClassDebugInfo.cs
                "113a15c43a44e6741b1a73d3af009df3",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/UdonSharpCompilerV1.cs
                "00ab14cace2fb314ead1159028d1b7fd",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/UdonSharpHeapFactory.cs
                "be2b79ce7391df34794d97ab35d38534",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/UdonSharpLabelTable.cs
                "08d479294d790bf48b1acd11f27985c7",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Assembly/AssemblyInstruction.cs
                "cbaa361a12f19c941bdb7989992f4454",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Assembly/AssemblyModule.cs
                "c3e167753e866c64cbdd3c5c2dd4f079",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Assembly/ExportAddress.cs
                "105086e0adf1f75488bd27992509f552",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Assembly/JumpLabel.cs
                "28ac79db21e90a34bb90a4c981fcb1e4",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BindContext.cs
                "f116f846e181258419efe1e77984f5e8",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BinderSyntaxVisitor.cs
                "dbd8845883139ee49a85e893fd43912d",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/ConstantExpressionOptimizer.cs
                "ea93181486a849046a59756f23c5bed2",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/ConstantValue.cs
                "bfbbbb0cf890f664d85ed0b7485fdb29",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/ExternResolverExtensions.cs
                "e317cb7fa4d945c4484a045095a9d975",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/TypeSymbolFactory.cs
                "fe318b910c0dee1499132005ed0ebdc9",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundAccessExpression.cs
                "988f5122454f3cf46b09b98948019ff4",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundArrayAccessExpression.cs
                "d64aae3d727820c49b7d935b62c0df61",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundArrayCreationExpression.cs
                "36e46b7844beaf641b74006dac880f3a",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundAssignmentExpression.cs
                "beb46a34e718df243927180e735d4c2c",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundBitwiseNotExpression.cs
                "dd541469feb075d4eacb3446c5039db8",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundBlock.cs
                "29000ebe49b8b1542b77829cce1b5323",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundBreakStatement.cs
                "2b4a317dc7fb76a40b198a313d5a936b",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundCastExpression.cs
                "71ebbbf1dd6f1e844a417f1b6b504d7b",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundCoalesceExpression.cs
                "3d00b34c212929b46ab3530fd6f6bd15",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundConditionalExpression.cs
                "14ff91ebda983c347a578b557526495e",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundConstArrayCreationExpression.cs
                "97d2bf6e5ad2cfd4ca0c07bc65e0722e",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundContinueStatement.cs
                "ac98576983713f547ac803a0b22bc2eb",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundDoStatement.cs
                "1dbf0ab7fd74e2649b46c6685f097515",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundExpression.cs
                "2bde224ee87b1b34a96be7c62bea99d8",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundExpressionStatement.cs
                "004d86898762cfa49a0d4341af07d3be",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundExternMethodInvocation.cs
                "3bd7d982e8955b547b222c05bf89facc",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundFieldAccessExpression.cs
                "471eaf7f712b1b94b8729a6db30302a8",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundForEachCharStatement.cs
                "87631ba1def01394daff5d58136487a9",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundForEachChildTransformStatement.cs
                "7b07c77cc30beda43bcdb9851b8de6fc",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundForEachStatement.cs
                "edb01f70cd2b7bd4ebce59dfe86e28aa",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundForStatement.cs
                "7bc238b740f2a564f954f381c12538cf",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundIfStatement.cs
                "daea6d6d4a4f1f54e9b463cc39143799",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundInterpolatedStringExpression.cs
                "45b01b776c06154438ab739241a968fa",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundInvocationExpression.cs
                "99ab1957216e3a14a907ce6428284c0e",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundLocalDeclarationStatement.cs
                "81ccd6272f612a642bddbe21f975e0fe",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundNode.cs
                "2ac35632a6bec5541ba54c611c7be6e4",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundPropertyAccessExpression.cs
                "47b84e2be9d6eca4ead00c530f50485d",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundReturnStatement.cs
                "1ca2c2ebc93520e4fb348cac7cccaa5b",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundShortCircuitOperatorExpression.cs
                "4a01e89746ce9024ca0ea57d77f9113c",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundStatement.cs
                "9e900cfa7f9ba7448af747103b025bc4",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundStaticUserMethodInvocation.cs
                "1bd86ebdb185698468f73ebb4d4a57ca",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundStringAccessExpression.cs
                "093f1060b48d746498a98d436f413b29",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundSwitchStatement.cs
                "b31b1704bbe04d6478cc201357e1d414",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundUdonSharpBehaviourInvocationExpression.cs
                "8abfdc03d4bdc4943883d8165c9947d0",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundUserMethodInvocation.cs
                "4f22848a9d58a4e4387fc73b6dc122e1",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundVariableDeclarationStatement.cs
                "c0145f9ed82b8d148ab8d61814523e8a",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundVariableDeclaratorStatement.cs
                "b8b1da9db75d2ce44b62a3ac12e77199",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/BoundNodes/BoundWhileStatement.cs
                "ec2484151e7dd464b8731854d10f71bd",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/ExternBuiltinOperatorSymbol.cs
                "813fa1673d388684eb7d04bd70a62441",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/ExternFieldSymbol.cs
                "2572f84d0a3f4b943ad96d805398d61e",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/ExternMethodSymbol.cs
                "77d41e1d271c28d42be8c6837a7f0952",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/ExternPropertySymbol.cs
                "acbe4439a469fcc4b9e0c35aa8a5a51d",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/ExternSynthesizedMethodSymbol.cs
                "8655c6e1fe2c2014d92f925aa473a515",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/ExternSynthesizedOperatorSymbol.cs
                "a9e63f44abfecfd428e7b7c2330ac248",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/ExternTypeSymbol.cs
                "f615e3a1918b8f8409436f4f2303d44e",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/FieldSymbol.cs
                "050d6bf7b0ddf364a90fc59d20575831",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/IExternSymbol.cs
                "d342ad63670ec4e4b92668db9327508c",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/ImportedUdonSharpFieldSymbol.cs
                "115ce7e67c0ab9a42928e666b568433b",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/ImportedUdonSharpMethodSymbol.cs
                "de9b207d7bba87649af8eec0d2919933",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/ImportedUdonSharpTypeSymbol.cs
                "d4cab8f2cd33530428d221c4d7b80359",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/LocalSymbol.cs
                "3bfd3042b2c79dd47872314d65546cf7",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/MethodSymbol.cs
                "455996caa402d8447a332c4063524689",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/ParameterSymbol.cs
                "e5f7a5ed0e57b904d8c9dc1ccd3fff73",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/PropertySymbol.cs
                "c25be04b59679774baedfefad1ca5e9d",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/Symbol.cs
                "31bab287352494e44846ee92aee1ad70",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/SynthesizedPropertySymbol.cs
                "0ee94e25b57fc5a4e9db2c03ec002172",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/TypeParameterSymbol.cs
                "7ef66cc4827d24c4382467ccccd380dc",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/TypeSymbol.cs
                "8fc72796ffa88b546b31fc528296013c",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/UdonSharpBehaviourFieldSymbol.cs
                "b76b663ea503d004983257bda376b212",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/UdonSharpBehaviourMethodSymbol.cs
                "da1d0943004b78449a8dc1ac1dcf0de7",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/UdonSharpBehaviourPropertySymbol.cs
                "e58a568ab174e9a439cffe382fdf48b1",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Binder/Symbols/UdonSharpBehaviourTypeSymbol.cs
                "13a26fa885213324490c81913037e490",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Core/CompilerException.cs
                "325eefe64590f33428dc17a80eba340b",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Core/NotExposedException.cs
                "3e50adcf1bb218b459c93d723fd1945d",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Core/NotSupportedException.cs
                "32bb32365c0bb1943932586f682465d3",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Emit/EmitContext.cs
                "fbe73ae697581b3439df4df13ce94d25",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Emit/Value.cs
                "42fbaa867155bab46a33f30392386c1c",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Emit/ValueTable.cs
                "a666dde01117f144eb26b0b922144feb",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Compiler/Udon/CompilerUdonInterface.cs
                "26c002334a3a8544287d984c81743081",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Editors/EasyEventEditor.cs
                "160367b0eada39342a89d857487f1af5",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Editors/GrabNodeDefinitions.cs
                "447cffd08de8f5043891c135d2b85e6f",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Editors/UdonSharpBehaviourEditor.cs
                "01fc33c691e16564c8df01629b7fdb88",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Editors/UdonSharpComponentExtensions.cs
                "72fd5a305cfb0df4dbf3b03b9e1f54a2",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Editors/UdonSharpGUI.cs
                "5a086a1f2c493ff48bf8686bdf9fb578",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Editors/UdonSharpSettings.cs
                "92c1823b5737ac04eaad1127b796db7a",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Editors/UdonSharpUndo.cs
                "2759e353bd7615c438724dfba71dbe4a",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Editors/UdonTypeExposureTree.cs
                "b7a7abf6cf40e2b44bf7173c3cb2728e",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Resources/UdonSharpData_Icon.png
                "ab61bdfed53bd7a408985498cbde68cd",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Resources/UdonSharpProgramAsset icon.png
                "d166756ffd718494082c946aee6d4ab9",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Resources/UndoArrowBlack.png
                "62ddcf63cbc3b6944b9e1ca0960c150a",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Resources/UndoArrowLight.png
                "635b9b5b6fc93ad43ae31d048959d067",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Resources/Localization/en-us.resx
                "ecf0282bac3152442b9615794f214b9b",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/Formatter.cs
                "3111ba2c4ee12ab479454b252ad55820",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/ProxySerializationPolicy.cs
                "5308ff490e40ab940aef65623364eb19",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/Serializer.cs
                "d16349e2d07545c42943d628dd7ed781",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/TypeSerializationMetadata.cs
                "a79c60ee254187c4eaf21fd07e61bf6f",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/Formatters/UdonSharpBehaviourFormatterEmitter.cs
                "a796521624b70f241ba569b01c102f9b",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/Serializers/ArraySerializer.cs
                "dde4c981522b643468846305475c2f9b",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/Serializers/DefaultSerializer.cs
                "9326fc6c7a8f380488b547ea3496748f",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/Serializers/JaggedArraySerializer.cs
                "054f784b43ca05142b91708b293e0184",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/Serializers/SystemObjectSerializer.cs
                "91a3fa68f7d4ce0499baecb33e16aff5",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/Serializers/UdonSharpBaseBehaviourSerializer.cs
                "28321cb3284870840ad001503d326c0f",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/Serializers/UdonSharpBehaviourSerializer.cs
                "bccc6c4ed9117f64aaee0b63ddb70b3d",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/Serializers/UnityObjectSerializer.cs
                "4b85e23f6345466469e6df711407ff92",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/Serializers/UserEnumSerializer.cs
                "d132731e5b9d2a942857326b0edf5625",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/StorageInterfaces/StorageInterface.cs
                "0a668ec7f85a6c146ad98dd63d09aa3a",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/StorageInterfaces/UdonHeapStorageInterface.cs
                "a48cf19765aaec54187900ffa27b98ca",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Editor/Serialization/StorageInterfaces/UdonVariableStorageInterface.cs
                "99835874ee819da44948776e0df4ff1d",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/UdonSharp.Runtime.asmdef
                "ad41a74bd6fbe7346b4389a33103f5d4",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/UdonSharpAttributes.cs
                "3c6e5249679282e459858775b10f38d0",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/UdonSharpBehaviour.cs
                "6769ed80171cc314bb08688a48d812a1",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/UdonSharpDataLocator.cs
                "49f78320ed7fe0b4f8eb298ff4e9035c",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/UdonSharpLocator.cs
                "cfea4a50f25cacb44ad6ca62f61a7936",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/UdonSharpRuntimeUtils.cs
                "e1a67d4778ffb214390ddafa61c1a557",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/Libraries/UdonSharp.Lib.asmdef
                "891645f3172a08b47b8fdf0d6006b667",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/Libraries/UdonSharp.Lib.asset
                "a434385f6fdf35b4a92bbe1db28fc52a",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/Libraries/CompilerInternal/CompilerConstants.cs
                "8232339c6b1acb3469fef9fd4ed71ad6",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/Libraries/CompilerInternal/GetComponentShim.cs
                "65860a727d016b5419fa81530b1ab589",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/Libraries/CompilerInternal/InstantiationShim.cs
                "31a99fcb8a0560a468dca1ec5706eaaf",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/Libraries/CompilerInternal/UdonSharpBehaviourMethods.cs
                "b3874ffdfd4fe1d49a68990563c15819",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/Localization/LocaleManager.cs
                "bf93e699136730c4bb47d4aa04c1a922",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/Plugins/Microsoft.CodeAnalysis.CSharp.dll
                "a19a5153c23411a46923731185430b93",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/Plugins/Microsoft.CodeAnalysis.dll
                "94ef9729b9524a34ea4b868379e06f48",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/Plugins/System.Reflection.Metadata.dll
                "15eaba9fdc5c0b34a86676edbbb3afb0",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Runtime/Plugins/System.Text.Encoding.CodePages.dll
                /*"",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/CustomInspectors/CustomInspectorBehaviour.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/CustomInspectors/CustomInspectorBehaviour.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/CustomInspectors/CustomInspectorChildBehaviour.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/CustomInspectors/CustomInspectorChildBehaviour.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/ExampleTutorialTemplate.txt
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/Blue.mat
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes1.unity
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes2.unity
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes3.unity
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes4.unity
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes5.unity
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes_1.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes_1.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes_2.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes_2.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes_3.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes_3.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes_4_Signal.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes_4_Signal.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes_4_Spinner.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes_4_Spinner.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes_5_Dependent.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes_5_Dependent.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes_5_Master.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Tutorials/SpinningCubes/SpinningCubes_5_Master.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Utilities/BoneFollower.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Utilities/BoneFollower.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Utilities/ExampleUtilityTemplate.txt
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Utilities/InteractToggle.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Utilities/InteractToggle.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Utilities/PlayerModSetter.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Utilities/PlayerModSetter.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Utilities/TrackingDataFollower.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Utilities/TrackingDataFollower.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Utilities/WorldAudioSettings.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Utilities/WorldAudioSettings.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Utilities/Synced/GlobalToggleObject.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Utilities/Synced/GlobalToggleObject.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Utilities/Synced/MasterToggleObject.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Samples~/Utilities/Synced/MasterToggleObject.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/EmptyGraph.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/IntegrationTestScene.unity
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/MainPPProfile.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/MiscTests.unity
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/SerializationTestScene.unity
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/StationTestScene.unity
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/SyncTestScene.unity
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/Test01_DebugLog.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/Test02_Arithmetic.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/Test03_FlowControl.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/Test04_Constructors.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/Test05_BehaviourInteractions.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/Test06_Raycast.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/Test07_Functions.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/Test08_Instantiation.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/Test08_Instantiation_mono.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/UdonSharp.Tests.asmdef
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/UdonSharp.Tests.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/IntegrationTestScene/LightingData.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/IntegrationTestScene/ReflectionProbe-0.exr
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/MiscTests/EditorClientSetColor.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/MiscTests/EditorClientSetColor.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/Prefabs/Cube.prefab
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/Prefabs/ReferenceObject.prefab
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/SerializationTestScripts/ClassSerializer.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/SerializationTestScripts/SerializedClassTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/SerializationTestScripts/SerializedClassTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/StationTests/FollowPlayerStationTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/StationTests/FollowPlayerStationTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/StationTests/StationEventTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/StationTests/StationEventTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestLib/IntegrationTests/IntegrationTestRunner.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestLib/IntegrationTests/IntegrationTestRunner.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestLib/IntegrationTests/IntegrationTestSuite.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestLib/IntegrationTests/IntegrationTestSuite.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/TestScriptTemplate.txt
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/BugTests/AccessViaAlternateInvocee/AccessViaAlternateInvocee.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/BugTests/AccessViaAlternateInvocee/AccessViaAlternateInvocee.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/BugTests/BracesInStringInterpolation/BracesInStringInterpolation.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/BugTests/BracesInStringInterpolation/BracesInStringInterpolation.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/BugTests/MethodArgumentCorruption/ArgCorruption2.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/BugTests/MethodArgumentCorruption/ArgCorruption2.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/BugTests/MethodArgumentCorruption/ArgCorruption3.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/BugTests/MethodArgumentCorruption/ArgCorruption3.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/BugTests/MethodArgumentCorruption/MethodArgCorruptionMain.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/BugTests/MethodArgumentCorruption/MethodArgCorruptionMain.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/BugTests/TernaryCOWBugs/TestTernary.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/BugTests/TernaryCOWBugs/TestTernary.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/DefaultHeapValueTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/DefaultHeapValueTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/DisabledObjectHeapTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/DisabledObjectHeapTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/InstantiatedObjectHeapTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/InstantiatedObjectHeapTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/InstantiatedObjectTesterScript.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/InstantiatedObjectTesterScript.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/InstantiateTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/InstantiateTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/MultiOutParamsTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/MultiOutParamsTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/ObjectDestroyNullCheck.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/ObjectDestroyNullCheck.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/SByteSerialization.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/SByteSerialization.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/StructMutatorTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Canny/StructMutatorTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/ArithmeticTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/ArithmeticTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/ArrayTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/ArrayTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/GenericsTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/GenericsTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/GetComponentTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/GetComponentTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/ImplicitConversions.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/ImplicitConversions.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/LocalFunctionTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/LocalFunctionTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/MethodCallsTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/MethodCallsTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/NameOf.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/NameOf.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/OrderOfOperations.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/OrderOfOperations.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/PropertyTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/PropertyTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/PropertyTestReferenceScript.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/PropertyTestReferenceScript.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/SerializationTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/SerializationTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/Editor/HeapInspector.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/Inheritance/ClassA.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/Inheritance/ClassA.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/Inheritance/ClassB.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/Inheritance/ClassB.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/Inheritance/ClassC.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/Inheritance/ClassC.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/Inheritance/InheritanceRootTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/Inheritance/InheritanceRootTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/Core/Inheritance/TestInheritanceClassBase.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/FlowControl/ForLoopTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/FlowControl/ForLoopTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/FlowControl/RecursionTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/FlowControl/RecursionTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/FlowControl/SwitchTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/FlowControl/SwitchTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/RegressionTests/DebugCarSystemWorking.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/RegressionTests/DebugCarSystemWorking.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/RegressionTests/JaggedArrayCOWTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/RegressionTests/JaggedArrayCOWTest.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/RegressionTests/LampOrderOfOpsTests.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/RegressionTests/LampOrderOfOpsTests.cs
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/RegressionTests/UserFieldTypeConversionTest.asset
                "",  // Packages/com.vrchat.worlds/Integrations/UdonSharp/Tests~/TestScripts/RegressionTests/UserFieldTypeConversionTest.cs*/
                "c5a6a6448901a5047b542a9771565eda",  // Packages/com.vrchat.worlds/Runtime/Udon/PostLateUpdater.cs
                "45115577ef41a5b4ca741ed302693907",  // Packages/com.vrchat.worlds/Runtime/Udon/UdonBehaviour.cs
                "530bdb25a3862ff4c8be42f678c53527",  // Packages/com.vrchat.worlds/Runtime/Udon/UdonManager.cs
                "e9c25d2e2cefc694f92f74a0fb86abe4",  // Packages/com.vrchat.worlds/Runtime/Udon/UdonNetworkTypes.cs
                "3c1bc1267eab5884ebe7f232c09ee0d9",  // Packages/com.vrchat.worlds/Runtime/Udon/VRC.Udon.asmdef
                "5803ea71238fea9419dff378e1c5f5c7",  // Packages/com.vrchat.worlds/Runtime/Udon/EventProxies/AbstractUdonBehaviourEventProxy.cs
                "ebf60f9246d047e5b4eb276969598538",  // Packages/com.vrchat.worlds/Runtime/Udon/EventProxies/OnAnimatorMoveProxy.cs
                "d6977159f8fa81e44ba2d5701fbda163",  // Packages/com.vrchat.worlds/Runtime/Udon/EventProxies/OnAudioFilterReadProxy.cs
                "d85dc7e69ef80244189aaecceb6eed66",  // Packages/com.vrchat.worlds/Runtime/Udon/EventProxies/OnCollisionStayProxy.cs
                "861e3a1e3374f22478e5ab06b00b032f",  // Packages/com.vrchat.worlds/Runtime/Udon/EventProxies/OnRenderObjectProxy.cs
                "7bf26b5066b7f2f4b8c4e921e98523e5",  // Packages/com.vrchat.worlds/Runtime/Udon/EventProxies/OnTriggerStayProxy.cs
                "80229c20033661747afe59830a80c977",  // Packages/com.vrchat.worlds/Runtime/Udon/EventProxies/OnWillRenderObjectProxy.cs
                "80455fb15755bfd47b1803c8fe84e16e",  // Packages/com.vrchat.worlds/Runtime/Udon/External/VRC.Udon.ClientBindings.dll
                "a5e7b2f5005f10e44b082e7c18871cc6",  // Packages/com.vrchat.worlds/Runtime/Udon/External/VRC.Udon.Common.dll
                "9d86dc4a513809149af3856eab191a3d",  // Packages/com.vrchat.worlds/Runtime/Udon/External/VRC.Udon.Security.dll
                "ecb1eec40b5e47047891ee46e739186a",  // Packages/com.vrchat.worlds/Runtime/Udon/External/VRC.Udon.VM.dll
                "92886df353bf1f14489cf2c4578e58af",  // Packages/com.vrchat.worlds/Runtime/Udon/External/VRC.Udon.VRCWrapperModules.dll
                "a3a3dda899277cc4ea6aebe18c6b5736",  // Packages/com.vrchat.worlds/Runtime/Udon/External/VRC.Udon.Wrapper.dll
                "958b822f881cee94fa3ce9e448ce0163",  // Packages/com.vrchat.worlds/Runtime/Udon/Fonts/fontawesome-webfont.eot
                "7b0e2552919dc70488a6d4a342b928a5",  // Packages/com.vrchat.worlds/Runtime/Udon/Fonts/fontawesome-webfont.svg
                "6cb934e9a1d9ea6448040aad7dbeac81",  // Packages/com.vrchat.worlds/Runtime/Udon/Fonts/fontawesome-webfont.ttf
                "157434a595e08d248a65d0700ba86a66",  // Packages/com.vrchat.worlds/Runtime/Udon/Fonts/fontawesome-webfont.woff
                "7facddc01811f7b4aae49393880e1384",  // Packages/com.vrchat.worlds/Runtime/Udon/Fonts/fontawesome-webfont.woff2
                "452f7d8d1a7418943b69d2df35655ebe",  // Packages/com.vrchat.worlds/Runtime/Udon/Fonts/Inconsolata-Bold.ttf
                "e3027d149d2f75647b130d9ed7f7014c",  // Packages/com.vrchat.worlds/Runtime/Udon/Fonts/Inconsolata-Regular.ttf
                "d8a7948bc83b01f45ab5078c10dd8e04",  // Packages/com.vrchat.worlds/Runtime/Udon/Fonts/Lato-Bold.ttf
                "98baae691215eb546a697ff7d942a5bb",  // Packages/com.vrchat.worlds/Runtime/Udon/Fonts/Lato-Regular.ttf
                "79f72428ef5a94f44a224932dfc8bc22",  // Packages/com.vrchat.worlds/Runtime/Udon/Fonts/RobotoSlab-Bold.ttf
                "f23c08f75b40f494b9b74462d7310dfb",  // Packages/com.vrchat.worlds/Runtime/Udon/Fonts/RobotoSlab-Regular.ttf
                "bf61d954ecb803046953c666facfb904",  // Packages/com.vrchat.worlds/Runtime/Udon/ProgramSources/SerializedUdonProgramAsset.cs
                "2fad63ba312d5f44a8ab215c3f5b18f1",  // Packages/com.vrchat.worlds/Runtime/Udon/ProgramSources/Abstract/AbstractSerializedUdonProgramAsset.cs
                "7fa64b2d7df72fb4cbf7898a400e86ef",  // Packages/com.vrchat.worlds/Runtime/Udon/ProgramSources/Abstract/AbstractUdonProgramSource.cs
                "e297f55d72e99c94abb5722781971931",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/Formatters/DataTokenFormatter.cs
                "b1d0b8aa8084bcd44a572d524d7a31bb",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/Formatters/UdonGameObjectComponentReferenceFormatter.cs
                "f2626352b2a60eb41adc3580ae44c750",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/Formatters/UdonProgramFormatter.cs
                "ec4e6da38017fe7df076afceb30fa17c",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/LICENSE
                "2105a6c0e5c0955d2d4a704c5e9c9b8f",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Version.txt
                "f70a94b0bedfa8ec50ed757f72032810",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/VRC.Udon.Serialization.OdinSerializer.asmdef
                "bfaf18dca1f68cc99ebeb0b862179265",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Config/GlobalSerializationConfig.cs
                "4ac1e1612275111bd50db8a3de8ba9c4",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/BaseDataReader.cs
                "501a7e1356f5fdc8e9bbefcd61a56490",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/BaseDataReaderWriter.cs
                "9638b18c6b6b6532b3b3cd3a73fefc2a",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/BaseDataWriter.cs
                "dc1fe25e670cf981ed66b3e85c3e4249",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/IDataReader.cs
                "af6696e41807b3c3f9a1d73667f76701",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/IDataWriter.cs
                "ee0465a1838833eb878447b34339d4f4",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/Binary/BinaryDataReader.cs
                "1bc9e2503afdd0290574ebc14cf4a16d",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/Binary/BinaryDataWriter.cs
                "1361420bc2b384389a065fd2fe59fb22",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/Binary/BinaryEntryType.cs
                "7a3a6dce9e0b8317b3804b35f48f6a97",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/Json/JsonConfig.cs
                "2ecc39ef0dc55ec10f83bb7eefd4f1db",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/Json/JsonDataReader.cs
                "3e05b98a26be61fa9203d4a45bfc1e95",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/Json/JsonDataWriter.cs
                "aad0a34e801ae645b359e4800ef7f636",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/Json/JsonTextReader.cs
                "6a0f5e01b82ae0763f6f907157a2c9c8",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/SerializationNodes/SerializationNode.cs
                "eab5938e837a8de93ce64c25399edde6",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/SerializationNodes/SerializationNodeDataReader.cs
                "9321fb650525f4bed18119d187111569",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/SerializationNodes/SerializationNodeDataReaderWriterConfig.cs
                "dd54f07c359d141095a031192c5ca084",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/DataReaderWriters/SerializationNodes/SerializationNodeDataWriter.cs
                "0bdecd79f568c8a3252bb5a9f3e2acdc",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/FormatterLocators/ArrayFormatterLocator.cs
                "c4228cdbba89e2a5d52357b998c3387d",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/FormatterLocators/DelegateFormatterLocator.cs
                "cf715e98fa96d74c81b4d3f4491d2592",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/FormatterLocators/FormatterLocator.cs
                "d35d0d1eb290f5d00e273d65e5db09d7",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/FormatterLocators/GenericCollectionFormatterLocator.cs
                "f2a9beaeecdd6eb929ddb049d7846a14",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/FormatterLocators/IFormatterLocator.cs
                "cdd12b278851bfdc68ca0d9e1e4f2d28",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/FormatterLocators/ISerializableFormatterLocator.cs
                "876ae9a404abe412e663fd9bc03d3525",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/FormatterLocators/SelfFormatterLocator.cs
                "00e10f526d476731ebc596ceb66271a6",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/FormatterLocators/TypeFormatterLocator.cs
                "9aaf14140a26e04b861b027d5ddb8fb6",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/ArrayFormatter.cs
                "3f5dc00eb17e568de42119a7f0f30ee8",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/ArrayListFormatter.cs
                "9598679c29f3e3696941746c26f1ccf8",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/BaseFormatter.cs
                "dff51bfb9b4d71aa78b3e5c8fec8c924",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/DateTimeFormatter.cs
                "3480954e7eecdc9745c1d08721b2f8b3",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/DateTimeOffsetFormatter.cs
                "4f17b17e986ae6f3be6a2ea1b716fcaf",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/DelegateFormatter.cs
                "4402da708267c579874c808a813bfe62",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/DerivedDictionaryFormatter.cs
                "b80567603fe814a8b4341584f8c3b4a6",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/DictionaryFormatter.cs
                "5c21ee7e54dff531da57563e2f81fff5",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/DoubleLookupDictionaryFormatter.cs
                "54578488936f8484c97ba7c52bfb0563",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/EasyBaseFormatter.cs
                "e226537cbfa910681132da3718f41c34",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/EmittedFormatterAttribute.cs
                "149c482b2ab9c601b8bc2ecc20bbd8d9",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/EmptyTypeFormatter.cs
                "b7da6bf97199e0bb743f7639c17112ac",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/FormatterEmitter.cs
                "06ccb8250c692f2695d28bfd6bcd4273",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/GenericCollectionFormatter.cs
                "f1eaa1b43658215b6d81013928eac19e",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/HashSetFormatter.cs
                "0fcb040f1c493dc2a5224e446be8b3c9",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/IFormatter.cs
                "5cae1c5d1116a090d70b6d0289061d21",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/KeyValuePairFormatter.cs
                "ba4ee6777a44f6e9a8e2e0a222c0f7db",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/ListFormatter.cs
                "21078ce134ce87231526dee77088e7ab",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/MethodInfoFormatter.cs
                "ae604bc0ef4ef9938100804f05decb21",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/MinimalBaseFormatter.cs
                "dc1b5b3148988d0d4fc2dab60a5c146c",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/MultiDimensionalArrayFormatter.cs
                "f9ea00de8051ca957d994e11630671d9",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/NullableFormatter.cs
                "6b6a62ea2fe943a4b261f832e8a1f3dd",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/PrimitiveArrayFormatter.cs
                "8045e4edca7c27f5b16bd90d7101c935",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/QueueFormatter.cs
                "15fa864c9e3363220ceac4ec93c7f5b8",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/ReflectionFormatter.cs
                "dde0095df6bea6624dfa72a31127bc48",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/ReflectionOrEmittedBaseFormatter.cs
                "12a47dd574302b77ba3c5ac05cd04541",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/SelfFormatterFormatter.cs
                "0f59404c810d015ed87c7e1557188435",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/SerializableFormatter.cs
                "087303d0d43cf7ce5af060a0cc0b5d38",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/StackFormatter.cs
                "4b0676b49f03cc50a1e532cf23e3988e",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/TimeSpanFormatter.cs
                "c6529471b992ba4080a123aa504ef9ea",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/TypeFormatter.cs
                "4a7c8e71a3ef1124db10e72af34e1724",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Formatters/VersionFormatter.cs
                "23fa5d3fed3b4b9de502257a594b00de",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/AllowDeserializeInvalidDataAttribute.cs
                "92726834b08002d525b86fbb012e184f",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/AlwaysFormatsSelfAttribute.cs
                "72783638708ea644ba5c3e1b91f827f6",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/ArchitectureInfo.cs
                "ad4e17831e9503c1f11149997c609477",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/Buffer.cs
                "e7e73146f1e861c27c5608bff4142402",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/CachedMemoryStream.cs
                "4fd6ff4077bbbef9b366d8ffd9236173",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/CustomFormatterAttribute.cs
                "e02123fad495d06f2a89e5335f00126c",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/CustomGenericFormatterAttribute.cs
                "97e9e01eb36fd43879b166b6b3c2469b",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/CustomLogger.cs
                "95bb5531b6c1d1a5eab8400ea1bd6167",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/CustomSerializationPolicy.cs
                "c2a40a3e6a114e5a50c0af209b8ae35e",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/DataFormat.cs
                "0bd9ab6cf3bd913588b6652279b7a6ba",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/DefaultLoggers.cs
                "996e793dcc0920d2590cb61f0761d498",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/DefaultSerializationBinder.cs
                "c79df97337d89089be40beb2e272df0b",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/DeserializationContext.cs
                "ae849a3e6d277006f3b4dd58a5765955",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/EmittedAssemblyAttribute.cs
                "3b06b106636f38afbb25ddd11e0c597c",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/EntryType.cs
                "c73435dff291e72c0d9ce55b59c39145",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/ErrorHandlingPolicy.cs
                "df06475ac5299f402ca1bdee3cf7e702",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/ExcludeDataFromInspectorAttribute.cs
                "08528593c8dd764b6d928dcee6daca9f",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/FormatterLocationStep.cs
                "30194d27b77855bf09b9af809a761ca5",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/FormatterUtilities.cs
                "32f94aca65b8d09ddd7b3db72e08db3f",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/IAskIfCanFormatTypes.cs
                "7ef6b6dd5e3be66c3a66753cc7e799de",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/IExternalGuidReferenceResolver.cs
                "d1eaa1a505a876bebb9cad40d01989e9",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/IExternalIndexReferenceResolver.cs
                "9414cf6a3ea9a51afcf648fe9ea02bed",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/IExternalStringReferenceResolver.cs
                "8bab352682356b8a2b02842520a68a11",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/ILogger.cs
                "106ca47adfa52732b129015337a1c8cd",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/ISelfFormatter.cs
                "90bcbfdc0286ca48d51fc578a1e15b8f",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/ISerializationPolicy.cs
                "7de3f23805ad9d4b3d033eef45e3b59b",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/LoggingPolicy.cs
                "10eb7be2b7c363367c46bc5699a361a8",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/NodeInfo.cs
                "766bbafe64ad16f63af4b81eb430e380",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/OdinSerializeAttribute.cs
                "3db8c00661ec222984427ab12295940f",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/PrefabModification.cs
                "23ceed712f987034deb8e92c561a1d3b",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/PrefabModificationType.cs
                "96fec6c04f13e378def42ea5ad5dc940",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/PreviouslySerializedAsAttribute.cs
                "989e99cd5b8f922edc1b13bbc22f4289",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/ProperBitConverter.cs
                "82702718797409c332f9174bdad57bbc",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/RegisterFormatterAttribute.cs
                "a000ffc63858a974eb63d9ef6f91adac",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/RegisterFormatterLocatorAttribute.cs
                "dca124a461001ad1494664ed95539612",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/SerializationAbortException.cs
                "eba33c8e77e2084c660af46be1b547dd",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/SerializationConfig.cs
                "1e93880e733f9a6a084cf4061634e7fb",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/SerializationContext.cs
                "67a19021ff9e6b27d8e9257ad075055a",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/SerializationPolicies.cs
                "08607b6e9c39ec19c1b61341583c2f3e",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/SerializationUtility.cs
                "bc69d8fd9d15913a491a45d1e040faf6",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Misc/TwoWaySerializationBinder.cs
                "0e8d8c5a97fdc322a8b8471aaf02f469",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/BooleanSerializer.cs
                "8aa9f52771b0e4e6f8f0c438a4f0430b",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/ByteSerializer.cs
                "d44d1ae83013328d7b855275fa1cfad7",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/CharSerializer.cs
                "5a2a43b51cef79fd0e85028650394b55",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/ComplexTypeSerializer.cs
                "50c67937d611e4749188b838e4cff5dc",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/DecimalSerializer.cs
                "9fc4716f683bc313c24bfa537cdd97f6",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/DoubleSerializer.cs
                "7a5d23b139cd8e692702aa431b071d07",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/EnumSerializer.cs
                "19dcfa9f6a40979fc2b6c3ae0f24b67c",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/GuidSerializer.cs
                "d280b44f7c75a9a18484a84745998130",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/Int16Serializer.cs
                "eafebb70813195e03b1ba467931eb686",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/Int32Serializer.cs
                "afe45c48508431a62aba886d943d8501",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/Int64Serializer.cs
                "6ccaffe3090611c2ada67d49cf834771",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/IntPtrSerializer.cs
                "88f3ec418fdfdd7eabd6134f1de91991",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/SByteSerializer.cs
                "29261eaea99f2d34c42cdc0b04f95daa",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/Serializer.cs
                "7aa356971fd0b66eb59875b278fa7f03",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/SingleSerializer.cs
                "85996580a8691185d06ec342c5c43747",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/StringSerializer.cs
                "3936194ea64890e11a7db8474eb0bbcf",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/UInt16Serializer.cs
                "f30e426f88b471e498dd1853b7bbaee6",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/UInt32Serializer.cs
                "f55c085325e12800428d01e3535cb297",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/UInt64Serializer.cs
                "0ee9dd19c234e4b16c835b9188459e36",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Core/Serializers/UIntPtrSerializer.cs
                "94a6cc2044fcd2cb317b1cdb1e8fcdaf",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/AOTSupportScanner.cs
                "f5fe153775edbadfa2b659e0e35dc881",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/AOTSupportUtilities.cs
                "aaf2f90207414827b53b85dae0eae82e",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/OdinPrefabSerializationEditorUtility.cs
                "de5584f66ccc5e072681a310c5987b8c",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/UnityReferenceResolver.cs
                "f670c1f9aa9ab0c5988e81802c005767",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/UnitySerializationInitializer.cs
                "9eb15f2339819bb651c7872d73c89776",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/UnitySerializationUtility.cs
                "864fb1c011715f9df2998d71ac8716f6",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/DictionaryKeySupport/BaseDictionaryKeyPathProvider.cs
                "ef6f699f176c2dfdea788982526f989a",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/DictionaryKeySupport/DictionaryKeyUtility.cs
                "b513c156933d8b833ccd40d717bf7e2b",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/DictionaryKeySupport/IDictionaryKeyPathProvider.cs
                "54f653ed4a4e15c07057283c11dce4d9",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/DictionaryKeySupport/RegisterDictionaryKeyPathProviderAttribute.cs
                "0405ef103432161dff609e75f71f3f55",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/DictionaryKeySupport/Vector2DictionaryKeyPathProvider.cs
                "1d61e235c606c1c9d7269f7e68471e38",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/DictionaryKeySupport/Vector3DictionaryKeyPathProvider.cs
                "51bb2cf369b5ea90948a20e4f2ebae48",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/DictionaryKeySupport/Vector4DictionaryKeyPathProvider.cs
                "3d2976bd61cccf62b11b4d3f02762465",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/AnimationCurveFormatter.cs
                "6ff1b29d64402a15d020739becd8661e",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/BoundsFormatter.cs
                "0878ec68b6ab3c9ebc365b6d139e4840",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/Color32Formatter.cs
                "25e35581ce6d1febd9ac41864a76ecdb",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/ColorBlockFormatter.cs
                "484768ba343a6a05522c29d81a4ce61d",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/ColorFormatter.cs
                "c3968bef792c5668478ac01be7645b30",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/CoroutineFormatter.cs
                "b5b415c00da8157ac50b8f5543f0b1d9",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/GradientAlphaKeyFormatter.cs
                "8936a3e43078251682f18923139f7aee",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/GradientColorKeyFormatter.cs
                "d5b54660d5342fd45e2e43775538879d",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/GradientFormatter.cs
                "68ac0b27f571616d3ed26c23eef40c8c",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/KeyframeFormatter.cs
                "afc596cd95a1ac316024d16f6fec6536",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/LayerMaskFormatter.cs
                "558323987bf9b75943382a5faa093ee3",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/QuaternionFormatter.cs
                "196809b991e565a48e3d4ad08cb30b5e",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/RectFormatter.cs
                "c934302874ac3315ed322feefefa1f9c",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/UnityEventFormatter.cs
                "70c675a7b4c71c685ee39d745ccb058b",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/Vector2Formatter.cs
                "da2644647af1368176103aa87de1dbaf",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/Vector3Formatter.cs
                "60afa8ede3981c383782a01ddc55e943",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/Formatters/Vector4Formatter.cs
                "ff1ca109149d83b03b39644f8045275e",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/SerializedUnityObjects/IOverridesSerializationFormat.cs
                "8942002e9ac41c2bfd27c4fbedf93f09",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/SerializedUnityObjects/IOverridesSerializationPolicy.cs
                "7279ec8ad7837f13ec833193ab4282cc",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/SerializedUnityObjects/ISupportsPrefabSerialization.cs
                "ea095023abd05a7af0da4166dcefdee8",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/SerializedUnityObjects/SerializationData.cs
                "c3cecb461cebbc940ede3b5ddb72382e",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/SerializedUnityObjects/SerializedBehaviour.cs
                "56b88cfe9935184fe250bda018144f26",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/SerializedUnityObjects/SerializedComponent.cs
                "d1b9fa6342beb9fdfc2c4bc1d8e5e971",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/SerializedUnityObjects/SerializedMonoBehaviour.cs
                "6cb9325ffffee5d6ed94d71590b4049a",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/SerializedUnityObjects/SerializedScriptableObject.cs
                "eefcd68a84ee7903b08c6254c17fafe3",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/SerializedUnityObjects/SerializedStateMachineBehaviour.cs
                "d62f7ab4e5aa075b819d6c71e929686b",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Unity Integration/SerializedUnityObjects/SerializedUnityObject.cs
                "78ce67c0b3c1975c520a08d1ff9fd24e",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Extensions/FieldInfoExtensions.cs
                "068f5645a5c3f9ce36a580ec57e775d1",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Extensions/GarbageFreeIterators.cs
                "0f84614827ff91701149564447a3932b",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Extensions/LinqExtensions.cs
                "62088a9c188c943eb4035de16eb6ec32",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Extensions/MemberInfoExtensions.cs
                "63a9a0384a6fe66fb04f82f325895b30",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Extensions/MethodInfoExtensions.cs
                "1df9513f03131466eecad22d1b19c4d8",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Extensions/Operator.cs
                "da8aea12015a2df5402c9e2d4f1cec5c",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Extensions/PathUtilities.cs
                "7f13450d6fd82372ffa7ee075a8eb4c9",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Extensions/PropertyInfoExtensions.cs
                "b554cbd9469011b544a2d92ae85a3b60",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Extensions/StringExtensions.cs
                "a6a172cef14a88c7fb714df37bbecedb",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Extensions/TypeExtensions.cs
                "eb77f5278e425e91b71e186df29a5f16",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Extensions/UnityExtensions.cs
                "787c97af872124f748a4a9b366f325d3",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Misc/AssemblyImportSettingsUtilities.cs
                "146b6bd1e3b0f0926205abf839ec9e6f",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Misc/Cache.cs
                "1bd625694c606aab0cb7895da4911c6a",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Misc/DoubleLookupDictionary.cs
                "bda92ec6156282448e883bf8f6a781fd",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Misc/EmitUtilities.cs
                "570028979953bd2c60b7e89ff7cef92e",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Misc/FastTypeComparer.cs
                "42e5d977e21c7a6524213a8a7dbee24a",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Misc/Flags.cs
                "783316da32d87acfae14953e341732a3",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Misc/ICacheNotificationReceiver.cs
                "1bc635f3755c60fe69f1895dd53974e2",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Misc/ImmutableList.cs
                "000592e93b119574207ea3bf59f659e4",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Misc/MemberAliasFieldInfo.cs
                "c1e85c1ef449ccb40e05f0afd3dd717f",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Misc/MemberAliasMethodInfo.cs
                "00bf47593f2a330c1bb41552bdc1233f",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Misc/MemberAliasPropertyInfo.cs
                "5ad884ed6013d621a310ceb4c954f62a",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Misc/ReferenceEqualityComparer.cs
                "0fe3820fb4651e200f17905245ec8be2",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Misc/UnityVersion.cs
                "93b4d01199b118896c756b09a9206fc0",  // Packages/com.vrchat.worlds/Runtime/Udon/Serialization/OdinSerializer/Utilities/Misc/UnsafeUtilities.cs
                "f6cfa3d8ec4f885468d17f5b023d2529",  // Packages/com.vrchat.worlds/Runtime/Udon/WrapperModules/ExternVRCInstantiate.cs
                "6490804aa441dfe4b88eb56cccbdf081",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/VRC.SDK3.asmdef
                "215be632cb025bd429dd50a3fa942168",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/Plugins/VRCSDK3-Editor.dll
                "661092b4961be7145bfbe56e1e62337b",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/Plugins/VRCSDK3.dll
                "077557169d70ddd428ee8dc8d95ec867",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/Plugins/ImageLoader/Android/arm64-v8a/libvrc_image_loader.so
                "60c5ab40ee8670248a238994e0afa77f",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/Plugins/ImageLoader/Android/armeabi-v7a/libvrc_image_loader.so
                "cb83b9fb7acae4f90b912df4707bb939",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/Plugins/ImageLoader/iOS/arm64/libvrc_image_loader.a
                "9975c7027ce0ba6448c32a99aefa23d4",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/Plugins/ImageLoader/Licenses/vrc_image_loader-licenses.html
                "3d59aa4c4b720974fb155939d5e503bc",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/Plugins/ImageLoader/Linux/x86_64/libvrc_image_loader.so
                "be7445ecf146d3442b6a4deeef997bb9",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/Plugins/ImageLoader/MacOS/x86_64/libvrc_image_loader.dylib
                "04b467cb8547d5246babfbc309e57171",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/Plugins/ImageLoader/Windows/x86_64/vrc_image_loader.dll
                "87f13d6a98e54fb78ca47650a29029a5",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/UnityEventFilter.cs
                "fb33369bfa554472b08fc6828450a2c4",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/VRCPortMidi.cs
                "61b00e22036b5cc49a565aaec91ca8cb",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/Event.cs
                "bbb88a986ea685d41a9eac3aa3dd8c60",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/MidiDeviceInfo.cs
                "02b0549f0815d8a4982133c0a99880cf",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/MidiDeviceManager.cs
                "6b1e1637c177e0c43af16fefddc0826c",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/MidiErrorType.cs
                "b8f59aea3b1948f4ebb0d7835b69bbb7",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/MidiEvent.cs
                "a9aa39367a5a6014e8ae35c4c0fc0fd0",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/MidiException.cs
                "9ddf13cca3c55164692f303b16a5fb2d",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/MidiFilter.cs
                "324be2ea40d796c49b74c60b8c2e5c97",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/MidiInput.cs
                "c18643a5c49be3649ade32f7c9b19e99",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/MidiMessage.cs
                "a806da9b51653af47b83c3c10a6eed65",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/MidiOutput.cs
                "f858f0a52902aaf4298247159434e1ae",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/MidiStream.cs
                "63050b27ae9f6ab4cb3837f915f15067",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/MidiTimeProcDelegate.cs
                "e20f952b815e0fd4c89e244be795b605",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/PmDeviceInfo.cs
                "36b7824d7ad3b3f4fbb5ed6d2a5766ec",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/PmEvent.cs
                "5d4b9d0d9dfad1c4894df5c2a5530696",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/PortMidiMarshal.cs
                "372656c38e5c12f48b819e12fa08ac7b",  // Packages/com.vrchat.worlds/Runtime/VRCSDK/SDK3/Midi/PortMidi/Plugins/Windows/portmidi.dll
                "a1125ad687f8d9f41b0da3667d153a30",  // Packages/com.vrchat.worlds/Samples/AsyncGPUReadbackExampleScene/AsyncGPUReadback.asset
                "1c7ada571bec8444484b30bc323b9c42",  // Packages/com.vrchat.worlds/Samples/AsyncGPUReadbackExampleScene/AsyncGPUReadbackExample.unity
                "ef4a8b6bafa850446b4a5ff358a88d18",  // Packages/com.vrchat.worlds/Samples/AsyncGPUReadbackExampleScene/RenderTexture.renderTexture
                "ae776b55e18c8b9499ea29e238b86238",  // Packages/com.vrchat.worlds/Samples/AsyncGPUReadbackExampleScene/Test.mat
                "9a72295aa324fc74e98c80a22a91ad5f",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/MiniMap Graph.prefab
                "28f0e3b7b14705445956368825a8a285",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/MiniMap Pickup.prefab
                "46891e43fcc0bd24caf7a600bb4ddc26",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/Minimap Sample Scene.unity
                "8175c20f542fbaf40811f752bfbf8759",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/Graphs/MiniMap.asset
                "c10d8bf61c50ae74a8ba37e6c69900c1",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/Materials/MiniMap Blitter.mat
                "6792d124852a74b4291d60d0f2787f31",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/Materials/MiniMap.mat
                "4e872ec796d64d540aa9016e6f4528cb",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Floor.mat
                "88e9119b6e679a848b6cc4b812721fbb",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Blue Glow.mat
                "268f44155a3f69e499ecc06c8a037a6c",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Blue.mat
                "f7b973f95bf253b4aa6c2e4985c86c06",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Glow.mat
                "2249d68a0e7bf074286c24e22d34ced8",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Green Glow.mat
                "02d6f7e0ec6eaca4ea8f10743bacfc52",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Green.mat
                "f9b6ae70f8b27454da67e14997bb525a",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Purple Glow.mat
                "f5f9510b4abf7e84e86d402def103b2d",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Purple.mat
                "e913b7f0bf0007b4590919fe35053b71",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Red Glow.mat
                "a86511bcb78f07a419a662d27b8be0b2",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Red.mat
                "0388ea6ffe023754792ecc3e749e538d",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material.mat
                "a4beea845c9905b459897504b0e5698c",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sky.mat
                "c7aa539c69c86944f9426b52d22e226c",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Tower.prefab
                "0a509109a7285c840a3b067603675029",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/VRC_cube_A.prefab
                "3feed679a07b3c54e975b5932993ee76",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/VRC_cube_B.prefab
                "0523bb9e5b7e8fe429eb735a6c639beb",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Minimap Sample Scene/LightingData.asset
                "483b341d13245b9448f046da759bf89e",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Minimap Sample Scene/Lightmap-0_comp_light.exr
                "7fd1abc2be768394c8cb5ae6646715d7",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Minimap Sample Scene/ReflectionProbe-0.exr
                "f740348c768907846baed893cbdefef2",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Minimap Sample Scene_Profiles/PostProcessing Profile.asset
                "72953fddbdc14665a2b0f74187794139",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/Shaders/MiniMap Blit.shader
                "face634f37c9087409ff9539683dc34e",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/Textures/MinimapRT.renderTexture
                "d3ed2feb39967a24d84dd65010e469c0",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/Textures/SceneCaptureRT.renderTexture
                "fc706b31320c35a459f9e6138a46bf32",  // Packages/com.vrchat.worlds/Samples/MidiPlaybackExampleScene/MidiPlaybackScene.unity
                "5014acd9bece4c04a9fe7ef5b3b4d881",  // Packages/com.vrchat.worlds/Samples/MidiPlaybackExampleScene/AudioMidiAssets/MidiDemoLoop.mid
                "5b7e2419951cc034789a08ab27befee7",  // Packages/com.vrchat.worlds/Samples/MidiPlaybackExampleScene/AudioMidiAssets/MidiDemoLoop.mp3
                "b35f97b1813cb064d852c92d1c5c1751",  // Packages/com.vrchat.worlds/Samples/MidiPlaybackExampleScene/UdonProgramSources/MidiGrid.asset
                "3d024fda64377514ab33fa3baefec378",  // Packages/com.vrchat.worlds/Samples/OnControllerColliderHitExampleScene/OnCharacterControllerHitExampleGraph.asset
                "b70b5ac51c994dc4aae6a1da6138b7c1",  // Packages/com.vrchat.worlds/Samples/OnControllerColliderHitExampleScene/OnControllerColliderHitSampleScene.unity
                "4f71908aaae3aac4ebcf83e4abbaea68",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonExampleScene.unity
                "506771de2b6f16f4494d9cad34491466",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/AVProVideoScreen.mat
                "e3769e73b10dfc1498cf1136e66de63a",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/CubeMat1.mat
                "99f7ea0146bcbb64d97a8468253bb347",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/CubeMat2.mat
                "cc54f62d6419422419aacb98b2cbaa66",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/CubeMat3.mat
                "3e749d8edb4501f488bf37401bec19cf",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/Ground.mat
                "74de320d298ce3e498b0401ec1ffcb7f",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/ImageMaterial.mat
                "c706afb4295d44a48a7a860f31d36150",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/LineMaterial.mat
                "8f5d353f21dad544ebeb59af6fe64604",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/PickupCubeMat.mat
                "219b8b6950b888f40b189f45cb13f02a",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/TriggerAreaMat.mat
                "cba30de4550b90f4f8ef7bc7d94faf95",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/UnityVideoPlayerScreen.mat
                "23dafc7d7a578b44f9cb37330ca2a156",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/AvatarPedestal.prefab
                "d0f711c3b2ac5b94d945da1b732ea12c",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/ImageDownload.prefab
                "b1a39f599f0964049b1c3ba10835ddf9",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/SimplePenSystem.prefab
                "91539d9b445f8d14d89e1b1c66047592",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/StringDownloader.prefab
                "d25c8082618057240967336d52b56d3c",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/Udon Variable Sync.prefab
                "70279d83763c0d745a4e513a75053671",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VRCMirror.prefab
                "00bd1d0a2cb96d845b0767189f49508d",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VRCPlayerVisualDamage.prefab
                "77a89e097657af54c85573a268691d5f",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VRCPortalMarker.prefab
                "8894fa7e4588a5c4fab98453e558847d",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VRCWorld.prefab
                "0d3d2df115d3d5147a07bd2e971a4443",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VideoPlayers/UdonSyncPlayer (AVPro).prefab
                "f2d01e7f26c5bb04f8c22c15fbf7475b",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VideoPlayers/UdonSyncPlayer (Unity).prefab
                "4715e20276be3b141a6a216230cab4e9",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VRCChair/StationGraph.asset
                "1dacfe29d81b51c46a85f97842455123",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VRCChair/VRCChair3.prefab
                "70108d78e82c2ec488d6b504865508e0",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/SampleAssetsSet_V1.fbx
                "fc18322c8bd152b458ffc49ade697169",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/GridFloor.mat
                "fd20e45036ef323459e8286e9c23c02c",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/GridFloor_udon.mat
                "1c987494452b85f4ab4cac3322415907",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_matte.mat
                "f32dd500294c1d048bf0629cf0c69be5",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_matte_blue.mat
                "a6c1d9564b56ecd47b82dfa7a8f11cbe",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_matte_green.mat
                "3b420fd445c370647be21f178917127d",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_matte_red.mat
                "5bec13570cd015140a051a07a3c55af5",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_mixed.mat
                "278c5fc8b64c3514b98f6554ff2e1328",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_mixed_blue.mat
                "1e2cef468006db345aef0ff70a68e96f",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_mixed_green.mat
                "3fc341313acf6ac48af69958cf612904",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_mixed_red.mat
                "916688f1c2e4c63498d399d9335c9ef7",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_shine.mat
                "5461c3b904b45cb4b932e10263cb3c88",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_shine_blue.mat
                "2d24fc897d87d8d4a80a06e5684c2eb7",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_shine_green.mat
                "d419d3432b8a0a24b986e614c57c2039",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_shine_red.mat
                "21221da753878694b9b9518a540dda85",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SKY_UdonLab.mat
                "d42d6e607dd21cf44945dc953c8aa1e3",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/Floor.prefab
                "53370219a5e4a584f9c6395b208dfda3",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_billboard_matte.prefab
                "c54c44a7b317f1349b5bbf3315981f3d",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_billboard_mixed.prefab
                "05606a80052633b4c85dca01e934d390",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_billboard_shine.prefab
                "706a6b0da0fe80a4080ffc5d4e3225e0",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_button_A_matte.prefab
                "7658a1c7a33fb0f4b9a41f41dd825e3d",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_button_A_mixed.prefab
                "ebf0301a541f0dd4886bbf3682912046",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_button_A_shine.prefab
                "3486463dc6f1f3341a3708cd620f4811",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_button_B_matte.prefab
                "b3dc6a315139c4a44bd3184523b641e5",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_button_B_mixed.prefab
                "b33b62db28a33a14993177833ee91f41",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_button_B_shine.prefab
                "be555230638b05445b7a82c619f0bccf",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_chair_udon.prefab
                "67fe5764aeb1bed479337d54d189d208",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_chair_udon_mixed.prefab
                "ad5069971f2a1ea47b4db3525d965c91",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_chair_udon_shine.prefab
                "382bdcf96025b7440af9c72a7e1b6872",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_cube_A_matte.prefab
                "5cd93a74517fed64db2b6fce666760a4",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_cube_A_mixed.prefab
                "fb7e5afc37161ed4ead2fdd070c9a537",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_cube_A_shine.prefab
                "e6927a60d9835084594485b53371cdce",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_cube_B_matte.prefab
                "2a41fcd39a5fb094fbdd414730ed7d9c",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_cube_B_mixed.prefab
                "f70e27dc68a53cc4aa9513aa5e0468e8",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_cube_B_shine.prefab
                "f3b2536f1de783f4182a88b6bd9e1645",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_pedestal_udon.prefab
                "aaa719cd8b802744598805bb392fe605",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_pedestal_udon_mixed.prefab
                "f8e6c0777affc3741b0e7db6ca23a036",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_pedestal_udon_shine.prefab
                "fb2bf244d4eae16499733b84929676a1",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Scenes/SampleAssetsDemoWorld.unity
                "46b60468ef2ab9b45b3eea17cccf7733",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Scenes/Udon_BlankCanvas.unity
                "1d50805597ae3674f9267fafbce3733a",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Textures/GridFloor.png
                "b2c2c1a52cdb75943958a408093dee04",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Textures/GridFloor_udon.png
                "6d3a4716a21ee164eb4b89e692a0c95c",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Textures/textures_mixed/SampleAssetsSet_V1_phong1_AlbedoTransparency.png
                "b82ab2397d1e79e47a6f85d50ed6fa8f",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Textures/textures_mixed/SampleAssetsSet_V1_phong1_MetallicSmoothness.png
                "559d1fda9b459bc43a0b78d7fbe50b00",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Textures/textures_mixed/SampleAssetsSet_V1_phong1_Normal.png
                "95687c795811ccb4e940f7c5f0864b08",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Textures/textures_shine/SampleAssetsSet_V1_phong1_AlbedoTransparency.png
                "c9a5a786a2a781d4fae92d02de3b610f",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Textures/textures_shine/SampleAssetsSet_V1_phong1_MetallicSmoothness.png
                "252675587bd0e5841aa3c82a5098201c",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Textures/textures_shine/SampleAssetsSet_V1_phong1_Normal.png
                "ab4e1feb84159f146bd1666598bad003",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Textures/texture_matte/SampleAssetsSet_V1_phong1_AlbedoTransparency.png
                "4ba9f24739c8721499ae85110ba2157d",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Textures/texture_matte/SampleAssetsSet_V1_phong1_MetallicSmoothness.png
                "5e07d6e71c9ed3f409237935b1c7a96e",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Textures/texture_matte/SampleAssetsSet_V1_phong1_Normal.png
                "0084f06feed78df4598d31467bfb5ad8",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/0579e2f7d1c57a241a07d45f6088960b.asset
                "dae65eb5fb00e8b47a845c94a293e268",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/0e78d0c5c758aaf4f9d0ace911a2c5d8.asset
                "b020b97bb8fe7eb4cbc33cf3ea9ddf4e",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/0f3632a4c15254e4185e597a9b553015.asset
                "be32d8a368369b646ad1d60f104d7790",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/1099cbb6e22bfe74a93b71dfe7c428aa.asset
                "1f8046e5a46216043ae32287d3d8a0f5",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/18a8a73823b22934e929c67357a4e2d7.asset
                "066f3af3c5289fd468eddc9652c8960f",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/19c6455fcf036f447a988be402108b3c.asset
                "2146353cfe319a34e9b5bdc79c54ea7a",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/2a34c3726f5aaca4c9b05004c07eb5c6.asset
                "e393818b9bb38234ab7ba8cd52e84604",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/2de31a7dfc5718c47aa82772c351b8a3.asset
                "280c6d04ae0aee944ae455d58373f94a",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/3f7757f3a3e464644acd66ab61321b36.asset
                "869c42aa9d0e8aa4cbba7e1cb2f1d469",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/46df060d25eb3bc42be5fcfae616147c.asset
                "f5afe499f8835534481ce1bca8a59df1",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/4715e20276be3b141a6a216230cab4e9.asset
                "277be80e5953c544d9df339328efd1c1",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/4bfd9d9a0b7684c449d31b38065b43f4.asset
                "a1138eb26881a404eae6487507dfabd8",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/5893300e3f688004b9251878e312d460.asset
                "a535e930fcaeaa14b8bef1e98b08c502",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/6657daa4973ee1249aae293810e8bccd.asset
                "1b252f6c7bcdd8442a70e93bb939c627",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/73571ae951ee35b479181d7ee4a4be25.asset
                "8259594746b9e7840827e62e19121d83",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/853a35cf0f51df6498d68490a1f662e3.asset
                "c0be0a8c517995a458fd900c40362246",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/8803f6df285e2fd48bbd0aeeb81ed533.asset
                "38275162bcd54164781ed4d064c1bb04",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/980a7697571ae1540827c8b930f79790.asset
                "3fdaca2a1f34bfb47b549604ebb8237f",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/98cd88d0adb2e994a9e93d2efefa9eb4.asset
                "e77a96be0333ce647b04d49c7912a035",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/a7250c474046ad245ac64456f76800ca.asset
                "9de7bd535d37225438d5b812d439af19",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/aaaeaa7ebc8e35a4e9ad1275785b2636.asset
                "bc53300f7f8f86e469c869b5cbfa6814",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/aacda992b3a1dca4ea17ecbadc5cadf1.asset
                "9716f2cef49698f439f95bb7a47e05e8",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/acd8738ca64f5a9448dfb040d1f2e4d5.asset
                "d195993cb22bfbc47af469412be0b7e4",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/b2329c06350f6d24ea49bc2842c81e99.asset
                "c3b7c039b70d68e4d840fef88c5158c9",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/b5280742086799a4c8c0a14e90cd913d.asset
                "f7f4ace51225ebd4e94ff3b9367a87ec",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/c69b708d523a01b449b6ca21384d958c.asset
                "a9572073218728649a677e31cd4ed398",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/c8df303ceb45ae84f85a11591f741734.asset
                "ca01a457f355c86498fc572d133e7ad2",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/d5d0346a3148a584da4572e44316e658.asset
                "0d210e13ecd64634d8238b7bc1b34f66",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/db6380e7c98d9e94bb856b1e1b1cf56c.asset
                "6beefcecc979d6e47b6ee83ea4f319d8",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/e1b45160fe9957145826cfa2a86419a1.asset
                "b49592c48696d6c40918c057b33fc191",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SerializedUdonPrograms/f6978e5f7a08f4047b4b9cf219efba6b.asset
                "b5280742086799a4c8c0a14e90cd913d",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/AvatarPedestal Program.asset
                "566cc00e27d5822449529a3785eae366",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/AvatarScalingSettings.asset
                "0579e2f7d1c57a241a07d45f6088960b",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/ButtonSyncAnyone.asset
                "3f7757f3a3e464644acd66ab61321b36",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/ButtonSyncBecomeOwner.asset
                "5893300e3f688004b9251878e312d460",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/ButtonSyncOwner.asset
                "18a8a73823b22934e929c67357a4e2d7",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/ChangeMaterialOnEvent.asset
                "0f3632a4c15254e4185e597a9b553015",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/Chooser.asset
                "2a34c3726f5aaca4c9b05004c07eb5c6",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/CubeArraySync.asset
                "49ba99e73d9ab4349871a10f182ee457",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/DownloadString.asset
                "4bfd9d9a0b7684c449d31b38065b43f4",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/DropdownSync.asset
                "dfcb9d6121fc4084e97b5303b0054618",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/Empty.asset
                "98cd88d0adb2e994a9e93d2efefa9eb4",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/FireOnTrigger.asset
                "aacda992b3a1dca4ea17ecbadc5cadf1",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/FollowPlayer.asset
                "8732b730b248f4344a2839981e1ff9f0",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/GetPlayersText.asset
                "9d916a2228b78c646aa46fe3ba85879d",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/ImageDownload.asset
                "0e78d0c5c758aaf4f9d0ace911a2c5d8",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/InputFieldSync.asset
                "3053cc98f03a13041a10e0650d9b6e24",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/IsValid.asset
                "1099cbb6e22bfe74a93b71dfe7c428aa",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/ObjectPool.asset
                "46df060d25eb3bc42be5fcfae616147c",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/PenLine.asset
                "d5d0346a3148a584da4572e44316e658",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/PickupAndUse.asset
                "aaaeaa7ebc8e35a4e9ad1275785b2636",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/PlayerCollisionParticles.asset
                "73571ae951ee35b479181d7ee4a4be25",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/PlayerTrigger.asset
                "e1b45160fe9957145826cfa2a86419a1",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/Pooled Box.asset
                "c69b708d523a01b449b6ca21384d958c",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/Projectile.asset
                "6657daa4973ee1249aae293810e8bccd",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/SendEventOnInteract.asset
                "a7250c474046ad245ac64456f76800ca",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/SendEventOnMouseDown.asset
                "980a7697571ae1540827c8b930f79790",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/SendEventOnTimer.asset
                "f6978e5f7a08f4047b4b9cf219efba6b",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/SetActiveFromPlayerTrigger.asset
                "1f7e9fb643472ef4d83f2ad49fe34b18",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/SetAllPlayersMaxAudioDistance.asset
                "acd8738ca64f5a9448dfb040d1f2e4d5",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/SimpleForLoop.asset
                "2de31a7dfc5718c47aa82772c351b8a3",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/SimplePen.asset
                "8803f6df285e2fd48bbd0aeeb81ed533",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/SliderSync.asset
                "19c6455fcf036f447a988be402108b3c",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/SyncPickupColor.asset
                "4eb7aa2be7d95324ea25c03bf1cab34f",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/SyncValueTypes.asset
                "699261d683532df468f1ed17ff8c8cf1",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/SyncValueTypesLinear.asset
                "1acedb947e4c9dc4d8f749557d611c1e",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/SyncValueTypesSmooth.asset
                "853a35cf0f51df6498d68490a1f662e3",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/ToggleGameObject.asset
                "b2329c06350f6d24ea49bc2842c81e99",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/ToggleSync.asset
                "db6380e7c98d9e94bb856b1e1b1cf56c",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/UdonSyncPlayer.asset
                "68d999abd6627d04999b5bebe2438687",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/UseStationOnInteract.asset
                "c8df303ceb45ae84f85a11591f741734",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/UdonProgramSources/VRCWorldSettings.asset
            };
        }
        string[] GetCyanTriggerGUIDs()
        {
            // CyanTrigger V4.3.0
            return new string[]
            {
                "5e1f7f33dafe75e42b53f895b9ae7093",  // Assets/CyanTrigger/CyanTriggerLicense.txt
                "70ef95417547a714b80f77073e7f810d",  // Assets/CyanTrigger/package.json
                "a2eac6430144f5346be1ac14b692a13e",  // Assets/CyanTrigger/CyanTriggerSerialized/CyanTrigger_Empty.asset
                "e7b3346044bfeea4894892c9fb97118a",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Bool/AnimatorBoolToggle.asset
                "be62cd355a2e884438815da8785b0602",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Bool/AnimatorBoolToggleActions.asset
                "3d1ab475f3d78eb4680d5dc3d63d7291",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Float/AnimatorFloatAdd.asset
                "24030e5a7662b404181fc32aeaa10f70",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Float/AnimatorFloatAddActions.asset
                "8f36aad135d3f934d8ac403abbbb0d25",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Float/AnimatorFloatDivide.asset
                "0c5cd118a6152394e90d60a36b6d1c17",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Float/AnimatorFloatDivideActions.asset
                "59edefad4a1b55b488069a8d7df3d006",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Float/AnimatorFloatMultiply.asset
                "2205e63f786c48046958ce1e6da7c284",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Float/AnimatorFloatMultiplyActions.asset
                "82483d561eca0ab46b5f72b2aa74e7e1",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Float/AnimatorFloatRemainder.asset
                "7c33cfa6b2b337146b3da91637472cd1",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Float/AnimatorFloatRemainderActions.asset
                "01eda303a6d12ef4fadf998d5b6b5702",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Float/AnimatorFloatSubtract.asset
                "67a13cad6ead6e040baeda28ab5d9675",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Float/AnimatorFloatSubtractActions.asset
                "afa579a1c58b3d64e927dc0ba6992eca",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Int/AnimatorIntAdd.asset
                "0ca3a039f3243964c93a409cd8eab46f",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Int/AnimatorIntAddActions.asset
                "113f46450262fbe4a98c287bcac78cab",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Int/AnimatorIntDivide.asset
                "47951a4f24d444b4d9fe6518f38f866d",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Int/AnimatorIntDivideActions.asset
                "2730dd6e4b3e3894c89cf17c2dc45765",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Int/AnimatorIntMod.asset
                "6d22836772b1ac34789a9031365aaada",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Int/AnimatorIntModActions.asset
                "b834535f16a7eea44b58a5842e42a9af",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Int/AnimatorIntMultiply.asset
                "5d0364cd23f3823459efb648c40fae81",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Int/AnimatorIntMultiplyActions.asset
                "6d9a5fcf35935de4fa1125b42980429d",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Int/AnimatorIntSubtract.asset
                "d11cb79751602174cb87689ad3b371bd",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Animator/Int/AnimatorIntSubtractActions.asset
                "dc286fad3e8f01247b237a0b19ca1ed7",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Audio/AudioSourceTogglePlay.asset
                "4eb9bb9831f1d974eb64ea6efcbf2b98",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Audio/AudioSourceTogglePlayActions.asset
                "a7bcfa207ecf47544a4bb002710c4152",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Bool/DataCacheBoolGet.asset
                "c4f26e1ca1016f54ca4532adac895304",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Bool/DataCacheBoolGetActions.asset
                "740178d18fc4ec5498e543bdb9850f6c",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Bool/DataCacheBoolSet.asset
                "3aba5110c3abf4b48acca3a2c1410892",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Bool/DataCacheBoolSetActions.asset
                "c13ae53c96b040d46add4c31ea405f4c",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Component/DataCacheComponentClear.asset
                "6184e80b9bad6f54f912c0b1ccc80ab4",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Component/DataCacheComponentClearActions.asset
                "52f21a8708b47864eb60ca870b44026f",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Component/DataCacheComponentGet.asset
                "c6785c3e7da526b429b24f43190621dd",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Component/DataCacheComponentGetActions.asset
                "82997a9590e291b4eb819bd7c97924f7",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Component/DataCacheComponentSet.asset
                "2e59dd83b9988cf42bb97810d30782aa",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Component/DataCacheComponentSetActions.asset
                "b3deb6c1df71cfc44af04565e1452081",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Float/DataCacheFloatGet.asset
                "c8f3d6e301748884ba19259a4b032831",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Float/DataCacheFloatGetActions.asset
                "1edf8a57d10d84b49815c2694ccdf394",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Float/DataCacheFloatSet.asset
                "3986432d548de8443a4d6cad8110540f",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Float/DataCacheFloatSetActions.asset
                "41fe3746a41ce8444bfdfb59e2885dc0",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/GameObject/DataCacheGameObjectGet.asset
                "bfaee3d8bf545544cbf826265922a132",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/GameObject/DataCacheGameObjectGetActions.asset
                "588dd43d1879a4a44a5ab1fdc44da312",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/GameObject/DataCacheGameObjectSet.asset
                "d69150de7c08c224d9e9d9e6ca63619e",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/GameObject/DataCacheGameObjectSetActions.asset
                "dbec866a8e002d6458aefc6c796da352",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Int/DataCacheIntGet.asset
                "91e1966224a9791408e39843a68c0df7",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Int/DataCacheIntGetActions.asset
                "003b88bc34f29bf4f92cc19ea072d353",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Int/DataCacheIntSet.asset
                "61dea32523ff77a47bcf21782df666b1",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Int/DataCacheIntSetActions.asset
                "0341a4f10e06f40418cf1bb1f6f76709",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Player/DataCachePlayerGet.asset
                "186be1c3a32bf70459f87973fdf05d94",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Player/DataCachePlayerGetActions.asset
                "5872d0d04e85ef347899cfd5d38b5901",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Player/DataCachePlayerSet.asset
                "f4aab0eff7931ae4dbeb91ab099c9e1e",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Player/DataCachePlayerSetActions.asset
                "b5ed7b94c79b3f64c8bd3acc48930725",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/String/DataCacheClearActions.asset
                "4bd5f30052df54c4484180824cbb0537",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/String/DataCacheStringClear.asset
                "203863ac286fa1d4586d9ba54b4f90de",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/String/DataCacheStringGet.asset
                "94a9eef9e50a93549822305182abeb22",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/String/DataCacheStringGetActions.asset
                "adf36c1ac038b3f418cd435e025a6148",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/String/DataCacheStringSet.asset
                "d5dcf26fd95672d49a040a8439623b59",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/String/DataCacheStringSetActions.asset
                "adf33feba4ef4d2418cec826230a9a00",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Udon/DataCacheUdonClear.asset
                "3d78f99f6da9da2459e5711fdad03e5e",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Udon/DataCacheUdonClearActions.asset
                "fbc9f6f0415cf364ea62c9c5c607ce63",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Udon/DataCacheUdonGet.asset
                "0c550cac45dcd3749b0eb4a8f6de9bf0",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Udon/DataCacheUdonGetActions.asset
                "77c9d973783b38342b6394864c59e9b7",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Udon/DataCacheUdonSet.asset
                "e5f1ee07aabf7ea4fb0b620df1f0db39",  // Assets/CyanTrigger/DefaultCustomActions/Actions/DataCache/Udon/DataCacheUdonSetActions.asset
                "e25b1eae44555394882f7f9b6459f487",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Debug/DebugLog.asset
                "bb8aaf5ad8938204fb8bfcfbd4c96819",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Debug/DebugLogActions.asset
                "d2de8f5c81e0e924290679bcfd7dff8e",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Debug/DebugLogError.asset
                "8264e5e94d75a8345954b4e27eea9bc8",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Debug/DebugLogErrorActions.asset
                "2e779d1e15403b8479c2a1947d07c35b",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Debug/DebugLogWarning.asset
                "f62ea3747888c034481a71c19d1bfc51",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Debug/DebugLogWarningActions.asset
                "1f6625b01a5c6f7488e8599618e931a1",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectGetCyanTriggerComponent.asset
                "ce1fc51c8bf5ac440a6bf0ad2498a3e0",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectGetCyanTriggerComponentActions.asset
                "43e9cd80e5054af4987fcebe5fe9f500",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectGetCyanTriggerComponentInChildren.asset
                "54d4a28dd3a60ca4f8be57f279c4155c",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectGetCyanTriggerComponentInChildrenActions.asset
                "abf8d8b2d357f4c41992755bc684a985",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectGetCyanTriggerComponentInParent.asset
                "920fea1fb26f8eb48a91ea2d9c3b27bc",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectGetCyanTriggerComponentInParentActions.asset
                "9d45f4388604b6f4eb094bc7141f3dc2",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectGetCyanTriggerComponents.asset
                "96342b455359a2849afe24257bbd6efe",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectGetCyanTriggerComponentsActions.asset
                "600cd95a427222146807679619c13135",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectGetCyanTriggerComponentsInChildren.asset
                "9ae2436a54330d6479b5fe1b7042a149",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectGetCyanTriggerComponentsInChildrenActions.asset
                "f863b64cc7daea34fa37a88ac09289ea",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectGetCyanTriggerComponentsInParent.asset
                "8007c23e7b903654ba26ae95d2485a50",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectGetCyanTriggerComponentsInParentActions.asset
                "8de0281dad67ab04cbb543ec8e8271a9",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectSetCyanTriggerComponentActive.asset
                "ad0f8607ee0071c4899937c9aa976660",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectSetCyanTriggerComponentActiveActions.asset
                "8cfb1482b56fca6458dae46b043fbc7d",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectSetCyanTriggerComponentActiveToggle.asset
                "227fd71114fcbe34e975df9b1172b0c1",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectSetCyanTriggerComponentActiveToggleActions.asset
                "5dd00cea8ec61de43b0879764922c1d7",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectToggleActive.asset
                "d25e0e4844b3a86428fc5c39e87fb6eb",  // Assets/CyanTrigger/DefaultCustomActions/Actions/GameObject/GameObjectToggleActiveActions.asset
                "53b140e2aaf6def49bab5e1fa02e9e41",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Math/MathfSmoothDamp.asset
                "454d6f410d2cc654789e624f5ec71e76",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Math/MathfSmoothDampActions.asset
                "8859fd3db476cc648bb5dd02ae31d4bb",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Math/Vector3SmoothDamp.asset
                "47a6b835d73dbed48b94d5f52cb51eec",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Math/Vector3SmoothDampActions.asset
                "126287435f4f3b140954dcaacadeab8d",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Particles/ParticleSystemTogglePlay.asset
                "9cc5866af7fa71044a2ba6cd6a8ee836",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Particles/ParticleSystemTogglePlayActions.asset
                "6d92139ecede7ce44a8b2e84ca9323fb",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/DamagePlayer.asset
                "0ea729277aa7f8a4782452d1f2968c04",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/DamagePlayerActions.asset
                "2f87a1a4d0f5a2c43a45470bc9f623ab",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/DropAllPickups.asset
                "df5a728bb2a3e35429b7b3a0e46af4b3",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/DropAllPickupsActions.asset
                "cbafd7c73c1f1b44587e8528bc4c0298",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/GetPlayerBonePositionRotation.asset
                "ff969fc16126ef942a3c5898f9b0a93d",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/GetPlayerBonePositionRotationActions.asset
                "94b01fcd892846847a2aacd9cb713f24",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/GetPlayerBonePositionRotationTransform.asset
                "01bfd6dc070b965438ce98d28f19bb91",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/GetPlayerBonePositionRotationTransformActions.asset
                "600ed80f707fcdb468b139d4401e64ca",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/GetPlayerPositionRotation.asset
                "cbb8e56c1163d4247aae109c2fa7d196",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/GetPlayerPositionRotationActions.asset
                "eabaf394b7aff7542b88724d153556d9",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/GetPlayerPositionRotationTransform.asset
                "982f85f1d8d32d34595d0ead31751b09",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/GetPlayerPositionRotationTransformActions.asset
                "0a175673bb7ce8d4a9337d7b3c0a1076",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/GetPlayerTrackingPositionRotation.asset
                "ab5515dc45b7cd94abf3d00821ae5db2",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/GetPlayerTrackingPositionRotationActions.asset
                "2d8ac1ff33efcae47afd7536cfd1496e",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/GetPlayerTrackingPositionRotationTransform.asset
                "7f8c9ab46bd2a4a4882dd69059290969",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/GetPlayerTrackingPositionRotationTransformActions.asset
                "2833bbb1e88d1e94ebb780f7ba4781d5",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/HealPlayer.asset
                "81f5ec9413221a8438ed050c6183cae7",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/HealPlayerActions.asset
                "d138effb230f24e4ea03192d66e156fb",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/TeleportPlayer.asset
                "34b63703171a4f04fa6fe1c6c9b2b7fc",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/TeleportPlayerActions.asset
                "3817863451ac60d4f90ba70e5b743473",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/TeleportPlayerRandomBox.asset
                "aa9782f0833766941853bbab34e152a3",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/TeleportPlayerRandomBoxActions.asset
                "8957e8cac4a9e1a49ae146f7590b1f77",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/TeleportPlayerRandomRadius.asset
                "c97159d3c08102543b9f273be2747f61",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/TeleportPlayerRandomRadiusActions.asset
                "a662b39e93bc94d44a2ffe49b3c0b746",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/TeleportPlayerSeamless.asset
                "cc8f0638935f74b43ae8bbb4cf03bd65",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/TeleportPlayerSeamlessActions.asset
                "73246177e8a7cfc4db1e21b34e30ae12",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/TeleportPlayerWithOptions.asset
                "e666c5bc733579a42a759230f73a3f18",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Players/TeleportPlayerWithOptionsActions.asset
                "2db8f92f564b4d6499ed859c3ca928ff",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Renderer/RendererSetMaterial.asset
                "19e3be45414287248b5f1b00ec2173e7",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Renderer/RendererSetMaterialActions.asset
                "3241e57b679538e42b302bd6a9d37908",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Rigidbody/RigidbodyAddAngularVelocity.asset
                "a9897d9922a14d247bd16449ba9981b5",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Rigidbody/RigidbodyAddAngularVelocityActions.asset
                "9ef66ba08e21ec54dbf46847ffda43b0",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Rigidbody/RigidbodyAddVelocity.asset
                "95533ace588e7a445a883a40178570b8",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Rigidbody/RigidbodyAddVelocityActions.asset
                "90c53b2e65bf6f048bbaf8a7d49b956f",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Rigidbody/RigidbodyToggleGravity.asset
                "96038099f6997864eb437a47bcc53903",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Rigidbody/RigidbodyToggleGravityActions.asset
                "9d7938c722c181749a5536792e9c909e",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Rigidbody/RigidbodyToggleKinematic.asset
                "b0ecaa48660b5474b8befba95caa1c18",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Rigidbody/RigidbodyToggleKinematicActions.asset
                "1e4e03d2fd044ad4aa7aaa4473c834c4",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Transform/TransformGetLocalPositionRotation.asset
                "c613223e84fda604ea85ce6a6cd891bb",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Transform/TransformGetLocalPositionRotationActions.asset
                "4a404b4c196a3cb46952dd9ed7233f6e",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Transform/TransformGetPositionRotation.asset
                "4682b32503b27434e96ceea9be5aaf41",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Transform/TransformGetPositionRotationActions.asset
                "fb752bf59ff44984199b4de6e9896268",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Transform/TransformRespawn.asset
                "af460215e01382c4391d73e70ff4790e",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Transform/TransformRespawnActions.asset
                "d06fa41bc5d584f4f857d431250075f8",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Transform/TransformSetLocalPositionRotation.asset
                "063457468248a434bb6afd56a82b3c6b",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Transform/TransformSetLocalPositionRotationActions.asset
                "519de2f7463459a49b9ab53a23917f44",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Transform/TransformTeleport.asset
                "911754b98b6dc2545b71573e25fa7ecd",  // Assets/CyanTrigger/DefaultCustomActions/Actions/Transform/TransformTeleportActions.asset
                "a936568509df5124c82fca46dd45e4d1",  // Assets/CyanTrigger/DefaultCustomActions/Actions/UdonBehaviour/Random.asset
                "f1c665e39e428ce4dae86a4394f97ea1",  // Assets/CyanTrigger/DefaultCustomActions/Actions/UdonBehaviour/RandomActions.asset
                "c19d0e959458547459d4148e8d1d8290",  // Assets/CyanTrigger/DefaultCustomActions/Actions/VRC/VRCInstantiateTransform.asset
                "781aaf212670dd746a93ede4bda938a1",  // Assets/CyanTrigger/DefaultCustomActions/Actions/VRC/VRCInstantiateTransformActions.asset
                "cc4003f050da613469f8ae8d250962ad",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionEnterGameObject.asset
                "f7157cb47f27d9b4e926dc663fde6725",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionEnterGameObjectActions.asset
                "9a07ec15779e10c489c7eae7a9aefacd",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionEnterLayer.asset
                "b0a10dc0b74ed064094640ea06bb8495",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionEnterLayerActions.asset
                "e2f239a9a35bc04469b41b0b3f97b43a",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionEnterNameContains.asset
                "88627e84cf6e42044ba7548d94728527",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionEnterNameContainsActions.asset
                "aafdd5f3381884747ac2930f4fe0a13f",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionExitGameObject.asset
                "e00d28e7bd809ce428c31c8da71fb083",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionExitGameObjectActions.asset
                "4daafeae6d6860941842172e33affd7f",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionExitLayer.asset
                "39a236465c45e4f46ac35dbff2de4608",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionExitLayerActions.asset
                "ef87d3651796fe44ca2f3d0477325322",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionExitNameContains.asset
                "5282808685aaac546bc8af74f3cbd536",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionExitNameContainsActions.asset
                "dab040aad68ffe74fbeddf25bac825bb",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionStayGameObject.asset
                "e310920c82228694a9b8247f132a3ae1",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionStayGameObjectActions.asset
                "3201cbcc005562d4da090b6e9efcd9fc",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionStayLayer.asset
                "4eb693102417d04409365f68b82b2548",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionStayLayerActions.asset
                "0e0893faf438bfb4f8bfbe13ad4bad36",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionStayNameContains.asset
                "40ab956ceea73ac42aa45cb556bb766b",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnCollisionStayNameContainsActions.asset
                "5acfa7c18de9dc342b32830051ca0788",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnParticleCollisionGameObject.asset
                "31ae444759f927940b693b72f2cb99b9",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnParticleCollisionGameObjectActions.asset
                "2ced3d875851b1140958db628a21480f",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnParticleCollisionLayer.asset
                "bd815b405d4315c4995a6410c00ea771",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnParticleCollisionLayerActions.asset
                "ce02c19f8e634904698958ab89e8c660",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnParticleCollisionNameContains.asset
                "fdc9116a42e91864e9587f4434e59518",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnParticleCollisionNameContainsActions.asset
                "ab2597a351a67fd46bc0f57ffee27de1",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerEnterGameObject.asset
                "dcb13369c6b8aae4cab1742d34d4ff37",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerEnterGameObjectActions.asset
                "9ac6bff6480643f48a92f38663ab3a1d",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerEnterLayer.asset
                "c5c01393ca15108428c7027646e23348",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerEnterLayerActions.asset
                "e78db65c403cca4478613518bf6141b8",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerEnterNameContains.asset
                "dbdedc43e7958fe468a1142609dcf8ca",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerEnterNameContainsActions.asset
                "67798dac55bf25747b93c758b854ba6c",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerExitGameObject.asset
                "05fc6f0689ede574da94353dc5f7bd05",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerExitGameObjectActions.asset
                "bc9cf9c5d6138bd4d8c864aa438c8c85",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerExitLayer.asset
                "865b8f53b2727d54b8b200031ee37ddf",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerExitLayerActions.asset
                "f539002c888130d4d8d9d950e64a7670",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerExitNameContains.asset
                "f5a9e08dff23b9540863d77578da305f",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerExitNameContainsActions.asset
                "3ffbfb5960fbd684da73194c047782be",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerStayGameObject.asset
                "baa293c68a3781741bed2a8378f58abd",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerStayGameObjectActions.asset
                "ef93044a10349644eb4181137c5aeb49",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerStayLayer.asset
                "e55459cbf72bffc46b36ebfdab25ae6d",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerStayLayerActions.asset
                "4792e9b7f0d16f84ab2389d022922cdd",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerStayNameContains.asset
                "6abf9497b4dcab8478496d1abfbe82cb",  // Assets/CyanTrigger/DefaultCustomActions/Events/Collision/OnTriggerStayNameContainsActions.asset
                "feb7f75f8548c5e42b83bc2dee444c3c",  // Assets/CyanTrigger/DefaultCustomActions/Events/Input/OnKeyDown.asset
                "1e5a6479feb74b642bb8ac2af93ab3d8",  // Assets/CyanTrigger/DefaultCustomActions/Events/Input/OnKeyDownActions.asset
                "608ec00628979da4687a7d785fdc25da",  // Assets/CyanTrigger/DefaultCustomActions/Events/Input/OnKeyUp.asset
                "b894b90af6daebc4eab4235e22f4140b",  // Assets/CyanTrigger/DefaultCustomActions/Events/Input/OnKeyUpActions.asset
                "bd44f88d29153f045b5be2ba53fa8ed6",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerCollisionEnterLocal.asset
                "b51657aabd235554cbedcafa33511d69",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerCollisionEnterLocalActions.asset
                "66bdbcfb2b9545e4ca9033ad6e537ba7",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerCollisionEnterRemote.asset
                "fea251102f7cf7f49995e4110bd24afe",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerCollisionEnterRemoteActions.asset
                "0aa134b43711d4c47873bfe8410c4c6a",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerCollisionExitLocal.asset
                "21dbd59dd288a634faee9f892fc7afe2",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerCollisionExitLocalActions.asset
                "aad9fb690e1d48c4a97e9cba1fc433ff",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerCollisionExitRemote.asset
                "6f751d3a715e5f947afc052759061da6",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerCollisionExitRemoteActions.asset
                "0ed8af35a71b3694e8aaa094c688ca14",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerCollisionStayLocal.asset
                "8eab3734f073deb46af70b2bc23cdd59",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerCollisionStayLocalActions.asset
                "19598389a9ceb864682638815e5ef24f",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerCollisionStayRemote.asset
                "d43934b6244ba814b9c05edfd29b45aa",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerCollisionStayRemoteActions.asset
                "bfa422787973cd04c8d1021acd744edc",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerJoinLocal.asset
                "5bf427c3afc433d43baada58bcdec106",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerJoinLocalActions.asset
                "7cb75e3d94584f14fa5bd32748175ab5",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerJoinRemote.asset
                "b8dfe695c9b7eca4f8c112cd3212ded1",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerJoinRemoteActions.asset
                "d23de1380569e9549b14444d2edc66a6",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerLeftLocal.asset
                "f2f6d8bec7a67ad48abf66b0632759b6",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerLeftLocalActions.asset
                "5a6614483c67a2546bfaa32809785dc0",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerLeftRemote.asset
                "e096b033d6ed4e449af002415900f387",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerLeftRemoteActions.asset
                "29ebdabd666995c41af3687e32f97de6",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerParticleCollisionLocal.asset
                "8fa9079df90596d43b876825bab2ac90",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerParticleCollisionLocalActions.asset
                "d7e224afa2265db449a69e7e9fb8935d",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerParticleCollisionRemote.asset
                "4a6a923a38993a244a4603283941543f",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerParticleCollisionRemoteActions.asset
                "d3edc96e161426544abaa975e55bada0",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerTriggerEnterLocal.asset
                "9d28dcac079b00d449bfee0a4a2be39d",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerTriggerEnterLocalActions.asset
                "2a6784e132e233f4b94bb4e9d5621500",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerTriggerEnterRemote.asset
                "e6c2e50ab61acd44da0185ebe1335b3b",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerTriggerEnterRemoteActions.asset
                "7f6f0fa4c92c012438e094a41bb814a9",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerTriggerExitLocal.asset
                "f81da5ff77ed83140bbe17223f7354a6",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerTriggerExitLocalActions.asset
                "5b460d1033d36fc43b622be189b6143d",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerTriggerExitRemote.asset
                "2ba586b0a10bb4849a73c7adf275cd9c",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerTriggerExitRemoteActions.asset
                "9a724ea5b6d746844b759e69c248061c",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerTriggerStayLocal.asset
                "2f63d308aad2eaa46955ad89723238f5",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerTriggerStayLocalActions.asset
                "41aec62b9d26ea2459ad96e5416b4323",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerTriggerStayRemote.asset
                "b73a92b2cd2686841b1f5f2146729bd0",  // Assets/CyanTrigger/DefaultCustomActions/Events/Player/PlayerTriggerStayRemoteActions.asset
                "8d5607788dfa43149b9acf64805b2de3",  // Assets/CyanTrigger/DefaultCustomActions/Events/Stations/StationEnteredLocal.asset
                "507d84ea097c6cd448179804341ffe64",  // Assets/CyanTrigger/DefaultCustomActions/Events/Stations/StationEnteredLocalActions.asset
                "659ee7417c977c745808b260cf4d24cc",  // Assets/CyanTrigger/DefaultCustomActions/Events/Stations/StationEnteredRemote.asset
                "47e59a47139da914e94532c07b6603fc",  // Assets/CyanTrigger/DefaultCustomActions/Events/Stations/StationEnteredRemoteActions.asset
                "40178210638868d40a876bb05a940043",  // Assets/CyanTrigger/DefaultCustomActions/Events/Stations/StationExitedLocal.asset
                "9185bd3ef550ea347b0e7f09095ee2f2",  // Assets/CyanTrigger/DefaultCustomActions/Events/Stations/StationExitedLocalActions.asset
                "d6149c77ce10f2943b6f221a47b8ebc9",  // Assets/CyanTrigger/DefaultCustomActions/Events/Stations/StationExitedRemote.asset
                "e83d074a57e618d4fb861bab699ea396",  // Assets/CyanTrigger/DefaultCustomActions/Events/Stations/StationExitedRemoteActions.asset
                "e751d767d1a8186409ed251997c48ebf",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputDropValue.asset
                "37603bf62afa0514288a4f4e5159dae2",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputDropValueActions.asset
                "608730f44d5e7174e8841cb109915939",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputDropValueHand.asset
                "1738ce714a7ad5749bc76e5085f01a4f",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputDropValueHandActions.asset
                "8c27c13003474eb439892b3e6f4f9bb6",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputGrabValue.asset
                "edcba01bfbe3212449f59bbcc86f86cb",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputGrabValueActions.asset
                "21f3a0b7cfd24d141a64f1e980723398",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputGrabValueHand.asset
                "ed5139db75e38f044a5883c2fe4ef98f",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputGrabValueHandActions.asset
                "e8323f2f8c0a4e5418b4835b3dfd7bb8",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputJumpValue.asset
                "f01f7dd9a1c6e4b43a9329eb65d08271",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputJumpValueActions.asset
                "2f1b855f18ef7c841ab77f545101dd58",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputJumpValueHand.asset
                "10a3ba03ccbe89747b548b142373edb9",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputJumpValueHandActions.asset
                "5cd07d04973925448842a92e1f6445d2",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputUseValue.asset
                "87d4ed3e5b91e31478963a24ecacdcab",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputUseValueActions.asset
                "51bf4d3e192b0764e8d189effcd10728",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputUseValueHand.asset
                "52118aaab667946469223092ec621c8e",  // Assets/CyanTrigger/DefaultCustomActions/Events/VRCInput/InputUseValueHandActions.asset
                "8adc85913b3d61743ba753861097ad92",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/BoolArrayList.asset
                "599aae1226052bd45a400c429c7386db",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/BoolArrayListActions.asset
                "3320dec6860d47343b7e92beb9b76bdf",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/ComponentArrayList.asset
                "0b55ab9f64d369849939604d490e68c1",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/ComponentArrayListActions.asset
                "b87a6dc809d1b42479bc315369cac5a2",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/FloatArrayList.asset
                "7afb486ffa1d93f4181f01693a3a3a1b",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/FloatArrayListActions.asset
                "06dcbedaa1500d640a6d35cc55f05810",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/GameObjectArrayList.asset
                "566777e3fc2d5914b9e52897301521b8",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/GameObjectArrayListActions.asset
                "d4c1dbacc2071944ca05f702fe32644b",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/IntArrayList.asset
                "46a0af2a3f1815745b7c0b42a135f167",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/IntArrayListActions.asset
                "b8fc8ed5001c48849a2a7bbbeb332c2b",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/ObjectArrayList.asset
                "ab77f560c1ceef149a50342d57c582c4",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/ObjectArrayListActions.asset
                "f85657939b2bd93499ea0b15e705535a",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/PlayerArrayList.asset
                "2ed710dfc208fb740a0003b67ff4aa47",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/PlayerArrayListActions.asset
                "785ab8a83f29e1d419f25a1e93f8802a",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/StringArrayList.asset
                "30c72f7c4481dbc4680e3f6bcc1cafe9",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/StringArrayListActions.asset
                "5e009fcbd318edd4ca249fcc87070eac",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/UdonArrayList.asset
                "4c8b15c97872f064497f66da9ece4d76",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/UdonArrayListActions.asset
                "e706a91ca406eeb4b822f2bcfc0a5e28",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/Vector3ArrayList.asset
                "c2b000201592143498a54aa0a9ba2059",  // Assets/CyanTrigger/DefaultCustomActions/Utilities/ArrayLists/Vector3ArrayListActions.asset
                "f1b754899f8ab724b97b71d99b8297f3",  // Assets/CyanTrigger/Editor/Cyan.CyanTrigger.Editor.asmdef
                "7dd09b2846765e449932f014247cd376",  // Assets/CyanTrigger/Editor/CyanTriggerActionInfoHolder.cs
                "89b74ea6e96a4a8e81d6b8df73ceed4d",  // Assets/CyanTrigger/Editor/CyanTriggerBuildCallback.cs
                "361c3a4f4dc4c094e83f14fc4bda48f0",  // Assets/CyanTrigger/Editor/CyanTriggerCompiler.cs
                "3dd9a29847ef3d64c84590936f85d1da",  // Assets/CyanTrigger/Editor/CyanTriggerDataReferences.cs
                "a8fba17b1f5f41df84e8a9afb1fb5b31",  // Assets/CyanTrigger/Editor/CyanTriggerInitializer.cs
                "e1e3ad44430b99343a9767f06edd8e0b",  // Assets/CyanTrigger/Editor/CyanTriggerInstanceDataHash.cs
                "edff0f9699df5a1478efc456b87e5baa",  // Assets/CyanTrigger/Editor/CyanTriggerSerializedProgramManager.cs
                "34aed83d6fe1a924abf7907a2981d9cc",  // Assets/CyanTrigger/Editor/CyanTriggerSerializerManager.cs
                "2dc271e54dc3bc74d9f14a8315536648",  // Assets/CyanTrigger/Editor/Assembly/CyanTriggerAssemblyActionsUtils.cs
                "18104678bc6dd29499893e5110214a29",  // Assets/CyanTrigger/Editor/Assembly/CyanTriggerAssemblyCode.cs
                "23e6f78fdbbf70a4c8ccbe800fafdc6d",  // Assets/CyanTrigger/Editor/Assembly/CyanTriggerAssemblyData.cs
                "ced439970e6b6044292bbfa618b73da7",  // Assets/CyanTrigger/Editor/Assembly/CyanTriggerAssemblyDataType.cs
                "4b0009923e7776b49b87d82a0f8005d0",  // Assets/CyanTrigger/Editor/Assembly/CyanTriggerAssemblyInstruction.cs
                "d84ce4264a4e0154e86e9db32ff67fcf",  // Assets/CyanTrigger/Editor/Assembly/CyanTriggerAssemblyMethod.cs
                "d5956691d6dfd784dbeb493a85f14903",  // Assets/CyanTrigger/Editor/Assembly/CyanTriggerAssemblyProgram.cs
                "684e6756671204f44a7f480d1323f7d8",  // Assets/CyanTrigger/Editor/Assembly/CyanTriggerAssemblyProgramUtil.cs
                "4bbaed55a3064c25b0d7b880c09eaf08",  // Assets/CyanTrigger/Editor/DependencySorting/CyanTriggerDependency.cs
                "9bfc1c16bb8a49aeb119c57bba42cfb4",  // Assets/CyanTrigger/Editor/DependencySorting/CyanTriggerPrefabDependency.cs
                "ac832953683f41f29f42dae748e757dc",  // Assets/CyanTrigger/Editor/DependencySorting/CyanTriggerPrefabInstanceDependency.cs
                "f653440528004b23b8e32ec34249874f",  // Assets/CyanTrigger/Editor/DependencySorting/CyanTriggerProgramDependency.cs
                "af408040f63b4594aeda64bf700a1802",  // Assets/CyanTrigger/Editor/Migration/CyanTriggerPrefabMigrator.cs
                "8cb62a080ffd6d344a5a39df3219d441",  // Assets/CyanTrigger/Editor/Migration/CyanTriggerVersionMigrator.cs
                "a31a3a978bd0448580d4ef9f3d6d95bf",  // Assets/CyanTrigger/Editor/ProgramAssets/CyanTriggerEditableProgramAsset.cs
                "62eab678c02d50042baa35d04cd0db48",  // Assets/CyanTrigger/Editor/ProgramAssets/CyanTriggerProgramAsset.cs
                "24881127789903541916877cf013fbfa",  // Assets/CyanTrigger/Editor/ProgramAssets/CyanTriggerProgramAssetExporter.cs
                "60e2cfd5cd9948938fe7cc64cffe6bac",  // Assets/CyanTrigger/Editor/Settings/CyanTriggerResourceManager.cs
                "6315930673664780ae892acea6de119f",  // Assets/CyanTrigger/Editor/Settings/CyanTriggerResourceManagerPostProcessor.cs
                "7a8693b7ca3940c1bd0a4eb645894491",  // Assets/CyanTrigger/Editor/Settings/CyanTriggerSettings.cs
                "36905545d62649aba610bf5382000651",  // Assets/CyanTrigger/Editor/Settings/CyanTriggerSettingsColor.cs
                "8de4fe64f56193b4bb51f9edd8eb26f0",  // Assets/CyanTrigger/Editor/Settings/CyanTriggerSettingsData.cs
                "c16dd9cac1b03a34aa0d966f341cbb24",  // Assets/CyanTrigger/Editor/Settings/CyanTriggerSettingsFavoriteList.cs
                "f14f393874f035d429543e226b83f356",  // Assets/CyanTrigger/Editor/Settings/CyanTriggerSettingsFavoriteManager.cs
                "0abda5b5feec43ae8bf35fb0fa9bd1f7",  // Assets/CyanTrigger/Editor/Settings/CyanTriggerSettingsProvider.cs
                "7a580e3c791da554ea5cdafe5a4de6ec",  // Assets/CyanTrigger/Editor/UdonDefinitions/CyanTriggerDefinitionResolver.cs
                "34116b0f0e24f0743b494631b708e061",  // Assets/CyanTrigger/Editor/UdonDefinitions/CyanTriggerDefinitionStubs.cs
                "533cb005d8934a040a3974fc39ed367f",  // Assets/CyanTrigger/Editor/UdonDefinitions/CyanTriggerNodeDefinition.cs
                "2f198d4a3ae9b674194ec669ecc0f53a",  // Assets/CyanTrigger/Editor/UdonDefinitions/CyanTriggerNodeDefinitionManager.cs
                "720f5512509e885459640781382a7fd5",  // Assets/CyanTrigger/Editor/UdonDefinitions/ActionDefinition/CyanTriggerActionGroupDefinition.cs
                "c565a226c65c4b6da0b4c5b54be66052",  // Assets/CyanTrigger/Editor/UdonDefinitions/ActionDefinition/CyanTriggerActionGroupDefinitionPostprocessor.cs
                "61d0ad128b95568438c70898dc9e600c",  // Assets/CyanTrigger/Editor/UdonDefinitions/ActionDefinition/CyanTriggerActionGroupDefinitionUdonAsset.cs
                "0a319161ff3cb854abf99f75a5122c01",  // Assets/CyanTrigger/Editor/UdonDefinitions/ActionDefinition/CyanTriggerActionGroupDefinitionUtil.cs
                "a869233160ccb6e459fd83017050f1ed",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeBlock.cs
                "b2e35986ed72d794bad8cefdf7501370",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeBlockEnd.cs
                "f57da929693160a448538873c209ffbd",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeBreak.cs
                "2d140151199d4717b71a7cafb0b3460b",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeClearReplay.cs
                "0e2c6c94ddeb90a44a182e7836fbf484",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeComment.cs
                "7263f0c9ad1cc16489a157265127ef5a",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeCondition.cs
                "a73c476a822ee364b8776de1ef7010b5",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeConditionBody.cs
                "4c024c83814185446a89c1e849c025fa",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeContinue.cs
                "4243626b9d1841944867b8741e1e7e47",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeElse.cs
                "42fb9989f7bf30442add4a4b12001b4f",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeElseIf.cs
                "f8484f49d7f04bac861050589165aea2",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeEnumToBaseType.cs
                "9ebad16005756fa48b2820f8d6afce80",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeFailIfFalse.cs
                "48ef1510280744879cd1ea52c2e99523",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeGetCyanTriggerProgramName.cs
                "a0d4e10a9e65d36448e97f0ffd582309",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeIf.cs
                "69291a7219b443f288e2cb2c91371bbe",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeIsCyanTriggerProgram.cs
                "a472aa62e10cdcf4983190fd0b99e755",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeLoopFor.cs
                "78974238e67f4d8fb39614152596a457",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeLoopForEach.cs
                "23151156d4844154a3e0c899168e7d5f",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeOnTimer.cs
                "74179cc224201674d905b4432b239741",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeOnVariableChanged.cs
                "948adb087cfd8e64daa60ecc9375ac69",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodePassIfTrue.cs
                "93c3505614ad1ce46ac6e4a5bdce68a0",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeReturn.cs
                "ddb5dc7886b830f419cdf3cbd61dd9f1",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeReturnIfDisabled.cs
                "0820b663be6ea0142aadb819a2d6f92b",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeSendCustomEvent.cs
                "96cb21d81c5f48cc81a54f46ec1cb7fb",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeSendCustomEventUdon.cs
                "a0b1d2199647ca9489cc5fc6fd434dc3",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeSetComponentActive.cs
                "e0c0b1344dac41e98968ae0104db8649",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeSetComponentActiveToggle.cs
                "e971af6ffb9b8b242941c7301a131c2b",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeSetReturnValue.cs
                "c53c208ea5e86744ba506258e80be207",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeSetVariable.cs
                "93ddc0746aa946beb6ad7f243a46bd9e",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeStringNewLine.cs
                "b753855ed17857044acbdb8d9ea38f25",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeType.cs
                "3db797588eb3e4d46b2a2b809176ee80",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeVariable.cs
                "1da676ddd2a4ce9479e84113af55ea81",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeVariableProvider.cs
                "959d0f4da03362b41a8385a3df69f30d",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomNodeWhile.cs
                "294df84d1b4f43449a999901e5ac8e00",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomUdonActionNodeDefinition.cs
                "21655797e76c6ff49b1c0e7a8be7b05e",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomUdonEventNodeDefinition.cs
                "19404a46fba61e145ab14fc14cd04948",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/CyanTriggerCustomUdonNodeDefinition.cs
                "c43331e182af4214bb5db527dd24af24",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/ICyanTriggerCustomNodeCustomHash.cs
                "22e6a71c76ee492a8ae1e49b68affe71",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/ICyanTriggerCustomNodeCustomVariableInitialization.cs
                "4835a950d57849f39a03460cffd1bd79",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/ICyanTriggerCustomNodeCustomVariableInputSize.cs
                "bb9912d40c5c4dd09cdc8d42b2130ff8",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/ICyanTriggerCustomNodeCustomVariableOptions.cs
                "c0b0807ef7c040a4860a64cb96dbb3c9",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/ICyanTriggerCustomNodeCustomVariableSettings.cs
                "1d341b750cf2474a88eac921a01a22c8",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/ICyanTriggerCustomNodeDependency.cs
                "5300a0ebeab44ab99ca324e214f77047",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/ICyanTriggerCustomNodeIf.cs
                "320e83a4011dcca439772999459afbb3",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/ICyanTriggerCustomNodeLoop.cs
                "1a9825f46fda4319a0caada511167358",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/ICyanTriggerCustomNodeScope.cs
                "1fc438f0e69b4a8e9fc6cf85b8f3976f",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomNodes/ICyanTriggerCustomNodeValidator.cs
                "98b2c92392cc49f4a4b00855f32a56ff",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomTypes/CyanTriggerCustomTypeCustomAction.cs
                "fc81a4b29def4d23b155415a72565c31",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomTypes/ICyanTriggerCustomType.cs
                "ba90a074012e4c25bb1351e74e8c05ba",  // Assets/CyanTrigger/Editor/UdonDefinitions/CustomTypes/ICyanTriggerCustomTypeNoValueEditor.cs
                "da9201ff1172b1f49b8f7af57695679a",  // Assets/CyanTrigger/Editor/UI/CyanTriggerActionDefinitionEditor.cs
                "b12182510a004ebca23f9ebc165289e5",  // Assets/CyanTrigger/Editor/UI/CyanTriggerAssetEditor.cs
                "9ada94af200b14046a77e73421d2368e",  // Assets/CyanTrigger/Editor/UI/CyanTriggerEditor.cs
                "5c21c4f72abf4c16af6aabbf83470d16",  // Assets/CyanTrigger/Editor/UI/CyanTriggerProgramAssetEditor.cs
                "b6d520a7acf1c7041ab4459b0641c04b",  // Assets/CyanTrigger/Editor/UI/CyanTriggerPropertyEditor.cs
                "f608a8c9bad46bc43ae2df9c0874b966",  // Assets/CyanTrigger/Editor/UI/CyanTriggerSerializableInstanceEditor.cs
                "63b879e790764fc0b7a5c1aaf2dd842f",  // Assets/CyanTrigger/Editor/UI/CyanTriggerSettingsEditor.cs
                "e170a3d1c74543fdbac27ba6068fb5d6",  // Assets/CyanTrigger/Editor/UI/CyanTriggerSettingsFavoriteListEditor.cs
                "6c9fdf17c99e74f47bb1cc7a98177aea",  // Assets/CyanTrigger/Editor/UI/CyanTriggerSettingsWindow.cs
                "1ad76f0d80c84ed0b21c73623d536aa6",  // Assets/CyanTrigger/Editor/UI/CyanTriggerUdonBehaviourEditor.cs
                "c5503225544a4714b42c838d208a982e",  // Assets/CyanTrigger/Editor/UI/ICyanTriggerBaseEditor.cs
                "72118180967f4762bf6f036ce940db64",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/CyanTriggerCustomNodeInspectorAnimatorSetParameter.cs
                "2325cbf8f65e40d5bdb961c69d3695f9",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/CyanTriggerCustomNodeInspectorClearReplay.cs
                "7e8766984f894337b15b30748d2c95ad",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/CyanTriggerCustomNodeInspectorGetProgramVariable.cs
                "51d51b432d144a0a9b8bd54bc99b6adb",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/CyanTriggerCustomNodeInspectorGetProgramVariableType.cs
                "761973b8776545de902dbbc719366ac3",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/CyanTriggerCustomNodeInspectorManager.cs
                "982369f05c9a42fca056206052840900",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/CyanTriggerCustomNodeInspectorSendCustomEventDelayedFrames.cs
                "35d51b9cca4d40f6a8fb781e259c28c6",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/CyanTriggerCustomNodeInspectorSendCustomEventDelayedSeconds.cs
                "e16fc3a1749043c5b42cc3c8d215c964",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/CyanTriggerCustomNodeInspectorSendCustomEventUdon.cs
                "26a022370fcc4b5fbeac76661c23cadd",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/CyanTriggerCustomNodeInspectorSendCustomNetworkEvent.cs
                "27e90a4f96c44a23827f4dd827633bbe",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/CyanTriggerCustomNodeInspectorSetComponentActive.cs
                "4153744cd3bb4fc7a2240e144027c12f",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/CyanTriggerCustomNodeInspectorSetComponentActiveToggle.cs
                "c16386da4ed244829a186db08aa82e5b",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/CyanTriggerCustomNodeInspectorSetProgramVariable.cs
                "ba251b8581ab4028b11ed0c66c2cda48",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/CyanTriggerCustomNodeInspectorUtil.cs
                "c3b9bf2fbf7f40208cc3afea56b48088",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/CyanTriggerCustomNodeInspectorUtilCommonOptionCache.cs
                "ccea79fb84f144acbe7f5dde822d2836",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/ICyanTriggerCustomNodeInspector.cs
                "e73ad7cbffb4434482b81b38d523b96f",  // Assets/CyanTrigger/Editor/UI/CustomNodeInspectors/ICyanTriggerCustomNodeInspectorDisplayText.cs
                "ccc193f7bb0d460aa10c827ebcac303f",  // Assets/CyanTrigger/Editor/UI/PropertyEditors/CyanTriggerTypePropertyEditor.cs
                "cbb3b184289c4e9ca5537063aa3a4b05",  // Assets/CyanTrigger/Editor/UI/PropertyEditors/CyanTriggerTypePropertyEditorBool.cs
                "b6ceafd4cbf5477aa3e54b517d1d305d",  // Assets/CyanTrigger/Editor/UI/PropertyEditors/CyanTriggerTypePropertyEditorDynamic.cs
                "9dbaf53123b2460f8974e53d9e472585",  // Assets/CyanTrigger/Editor/UI/PropertyEditors/CyanTriggerTypePropertyEditorManager.cs
                "5a5817e6702c4a8f847a36694806a109",  // Assets/CyanTrigger/Editor/UI/PropertyEditors/CyanTriggerTypePropertyEditorMatrix4x4.cs
                "1324fc8cc48e4ea2bc1216a169ef47fa",  // Assets/CyanTrigger/Editor/UI/PropertyEditors/CyanTriggerTypePropertyEditorPlane.cs
                "b86ffdc90f9b4395952489438c7563a8",  // Assets/CyanTrigger/Editor/UI/PropertyEditors/CyanTriggerTypePropertyEditorQuaternion.cs
                "88e0da70c8804cedb49a3bf9944f35da",  // Assets/CyanTrigger/Editor/UI/PropertyEditors/CyanTriggerTypePropertyEditorRay.cs
                "2f8a1299b99d4a0ca0e5ba9befa279d9",  // Assets/CyanTrigger/Editor/UI/PropertyEditors/CyanTriggerTypePropertyEditorType.cs
                "2dcc123dc0d24e67b7256f87ab72ac02",  // Assets/CyanTrigger/Editor/UI/PropertyEditors/CyanTriggerTypePropertyEditorUtils.cs
                "ced0a5070dfc4054ae6759370417c446",  // Assets/CyanTrigger/Editor/UI/PropertyEditors/CyanTriggerTypePropertyEditorValid.cs
                "d925e60d635f49d1bef0eab75fdeff5b",  // Assets/CyanTrigger/Editor/UI/PropertyEditors/CyanTriggerTypePropertyEditorVector4.cs
                "d7bcc25968fd4b7f814222e709894d33",  // Assets/CyanTrigger/Editor/UI/PropertyEditors/CyanTriggerTypePropertyEditorVRCPlayerApi.cs
                "b29028ee6edfc164e9000a334e437bb0",  // Assets/CyanTrigger/Editor/UI/SearchWindows/CyanTriggerActionSearchWindow.cs
                "43fec38ad0e94864babb585290c3fb73",  // Assets/CyanTrigger/Editor/UI/SearchWindows/CyanTriggerCustomActionGroupSearchWindow.cs
                "e93f0e48ef6021241b64be76f7c7ad3c",  // Assets/CyanTrigger/Editor/UI/SearchWindows/CyanTriggerEventSearchWindow.cs
                "f0666e49675468c419c6a5bf2d7c8cff",  // Assets/CyanTrigger/Editor/UI/SearchWindows/CyanTriggerFavoriteSearchWindow.cs
                "126ddd38bd9fabe4b8186123dfdbc47e",  // Assets/CyanTrigger/Editor/UI/SearchWindows/CyanTriggerFocusedSearchWindow.cs
                "e2cf291e6b2daf9469e507cbceaf88d1",  // Assets/CyanTrigger/Editor/UI/SearchWindows/CyanTriggerSearchWindow.cs
                "0510ae3740de10443ba26713d9b998a4",  // Assets/CyanTrigger/Editor/UI/SearchWindows/CyanTriggerSearchWindowManager.cs
                "52897678a15448ea8f58086e80aa989e",  // Assets/CyanTrigger/Editor/UI/SearchWindows/CyanTriggerSearchWindowProvider.cs
                "2499aa2597cedcc448bdf2e7de78f067",  // Assets/CyanTrigger/Editor/UI/SearchWindows/CyanTriggerVariableSearchWindow.cs
                "73315673565d9b64db679db83d4c7638",  // Assets/CyanTrigger/Editor/UI/TreeViews/CyanTriggerActionTreeView.cs
                "50f8fcc87d6dee1438fccd6d787a3987",  // Assets/CyanTrigger/Editor/UI/TreeViews/CyanTriggerScopedDataTreeView.cs
                "2d7b6f8fc6f82e5438a1860dd934fa88",  // Assets/CyanTrigger/Editor/UI/TreeViews/CyanTriggerScopedTreeView.cs
                "2fa6fca8df8de2849a3c7fb6799135e6",  // Assets/CyanTrigger/Editor/UI/TreeViews/CyanTriggerSettingsFavoritesTreeView.cs
                "8e5ed3759cd34cf993612aee8922d17f",  // Assets/CyanTrigger/Editor/UI/TreeViews/CyanTriggerVariableEditableTreeView.cs
                "490801d8b39fc3b408ab34226fa372d0",  // Assets/CyanTrigger/Editor/UI/TreeViews/CyanTriggerVariableTreeView.cs
                "a64ce74fcded40e79f0990545b6ed9ff",  // Assets/CyanTrigger/Editor/UI/TreeViews/CyanTriggerVariableValueTreeView.cs
                "22846163833f4e3a9a413710f8affa71",  // Assets/CyanTrigger/Editor/UI/Utils/CyanTriggerEditorGUIUtil.cs
                "e602c230ee2946bfbcdf080fa042a2c0",  // Assets/CyanTrigger/Editor/UI/Utils/CyanTriggerEditorUtils.cs
                "5f6308bba4e34316864cc097d30572c5",  // Assets/CyanTrigger/Editor/UI/Utils/CyanTriggerImageResources.cs
                "ca565aa4fe324a944bc3738df6896f12",  // Assets/CyanTrigger/Editor/UI/Utils/CyanTriggerSerializedPropertyUtils.cs
                "0ce3afccaed64ad08cecd9824690ee8d",  // Assets/CyanTrigger/Editor/Utils/CyanTriggerSessionState.cs
                "62b48230700446649919cb32761d300a",  // Assets/CyanTrigger/Editor/Utils/CyanTriggerUtil.cs
                "53d99b8e4bc6f2f46bee6db8a4a6c60e",  // Assets/CyanTrigger/Examples/Animations/Spin.anim
                "c0506d5cdd9ec5246854fbf22c58eedc",  // Assets/CyanTrigger/Examples/Animations/SpinController.controller
                "d8ae79dd5bc068c4da9ca55ec37087a5",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/CombatSystemV2.asset
                "eef59faa4b189da4182870b53ca120c5",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/DownloadImage.asset
                "a4c131361cd915445bb5ac72adf249d6",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/DownloadString.asset
                "3e40dea233bf0824fbf6b27fecc65ddc",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/LocomotionSettingsV2.asset
                "6af4275727df0d347aa8b5159e4666c6",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/MultiJumpSystem.asset
                "f305577e9922e7c4eba3f59db4f81dba",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/PlayerAudioSettingsV2.asset
                "872fdb6ace8c8844abc85681046d3fac",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/PlayerBaseTrackerV2.asset
                "1bee5724ccee0124886b32e1cb6444ae",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/PlayerBoneTrackerV2.asset
                "34dd1fe0a648c27459296d0b0e45efad",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/PlayerHaptics.asset
                "27a882491d8d109468aa40ef0fa84299",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/PlayerTrackerV2.asset
                "5592758ce57e41145a5aac45ebcb85b0",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/Combat/GunBase.asset
                "bdfeefc340e37c04fb5e01646b756590",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/Combat/GunHandlerBase.asset
                "ec659cb9d1cca5f428f66a2a5dc96b69",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/Combat/GunParticleHandler.asset
                "4513d81634d184a46b428b11d456b272",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/Combat/GunRaycastHandler.asset
                "77ff92c18f2ffea408dad0817358d058",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/Combat/HealthSystem.asset
                "fdd304a62b616c443b952b6f4dbf261d",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/Combat/HealthSystemActions.asset
                "1d2a419471f557b4d8b865fe3f17a005",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/Combat/RendererColorFlash.asset
                "70a4bf912f3a40c459a03be14efbc738",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/FadeController/FadeControllerActions.asset
                "17f1f7cd232e450458f1a455e6753df9",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/FadeController/FadeControllerCalls.asset
                "3116d471382e0bb4aa97364745cf2660",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/FadeController/FadeControllerV2.asset
                "18345d1902b09b5438ba85fd20b7eee0",  // Assets/CyanTrigger/Examples/CyanTriggerPrograms/SceneExamples/[SpawnedObject] Pickup.asset
                "7670e691e97b25b4282bf9c194b21b38",  // Assets/CyanTrigger/Examples/Materials/FakeMirror.mat
                "46ae6414b09aebf42ab122c226c41825",  // Assets/CyanTrigger/Examples/Materials/ImageMaterialCT.mat
                "6bcdbad1f1fd6fb4599c7ddf75df7ee3",  // Assets/CyanTrigger/Examples/Materials/WhiteOverlay.mat
                "1f76a5e71d6aa6545b628ceb66d907c0",  // Assets/CyanTrigger/Examples/Materials/Colors/Black.mat
                "02417c1fddc513b45a833885b863472f",  // Assets/CyanTrigger/Examples/Materials/Colors/Blue.mat
                "1ecb914f77a803347ba8b570bef9f60f",  // Assets/CyanTrigger/Examples/Materials/Colors/Cyan.mat
                "439e2414b74264d4bbf5d838b652bd1e",  // Assets/CyanTrigger/Examples/Materials/Colors/Gray 0.25.mat
                "7c646d1ef17cae040979e4344bfd851a",  // Assets/CyanTrigger/Examples/Materials/Colors/Gray 0.30.mat
                "ba6285bf43e6df547bfa879728df0a5e",  // Assets/CyanTrigger/Examples/Materials/Colors/Gray 0.50.mat
                "af82c8247b5c6ca46b69dfefc7a9e364",  // Assets/CyanTrigger/Examples/Materials/Colors/Gray 0.75.mat
                "bee376e351ed71340a174c583a7aa1dc",  // Assets/CyanTrigger/Examples/Materials/Colors/Gray 0.80.mat
                "3dbd2c3b208506341843174ceac9003c",  // Assets/CyanTrigger/Examples/Materials/Colors/Green.mat
                "812742149eb095f49810ddee4c46e857",  // Assets/CyanTrigger/Examples/Materials/Colors/Magenta.mat
                "38a0f6eebb239744c952b410c03b89af",  // Assets/CyanTrigger/Examples/Materials/Colors/Orange.mat
                "fe4fba6f914dca943a998dec2467eb1f",  // Assets/CyanTrigger/Examples/Materials/Colors/Purple.mat
                "6e48f857c8e5f094cae38c993a2cce59",  // Assets/CyanTrigger/Examples/Materials/Colors/Red.mat
                "3032d9b195bb67748830b89e3085bfa1",  // Assets/CyanTrigger/Examples/Materials/Colors/White.mat
                "67f6a6e90c5f17340a1351b71cfd77f3",  // Assets/CyanTrigger/Examples/Materials/Colors/Yellow.mat
                "b9d431ac9b8df77418d0bc20c48803c9",  // Assets/CyanTrigger/Examples/OldExamples/CombatExample.unity
                "9a95a4668b84c324d8b7c4c2d79309e8",  // Assets/CyanTrigger/Examples/OldExamples/Example.unity
                "3d2239a96d27548409fe7252d0de576b",  // Assets/CyanTrigger/Examples/OldExamples/Prefabs/CombatSetupOld.prefab
                "eb2a7f1638c61414292867040fd6f05c",  // Assets/CyanTrigger/Examples/OldExamples/Prefabs/DoubleJumpSystemOld.prefab
                "85859eb39801b1645baa8791b84ce701",  // Assets/CyanTrigger/Examples/OldExamples/Prefabs/LocomotionSettingsOld.prefab
                "3423a2b24ba3788478651861ee349bb1",  // Assets/CyanTrigger/Examples/OldExamples/Prefabs/PlayerBaseTrackerOld.prefab
                "72c1f3e19f57ba84881ea8a6138d4e08",  // Assets/CyanTrigger/Examples/OldExamples/Prefabs/PlayerBoneTrackerOld.prefab
                "b5ef09c2fcac92949926b9636939e944",  // Assets/CyanTrigger/Examples/OldExamples/Prefabs/PlayerFadeControllerOld.prefab
                "247fe84fa3afb2c41a0094c7eacc97b1",  // Assets/CyanTrigger/Examples/OldExamples/Prefabs/PlayerTrackerOld.prefab
                "94a5630c25c46ec4491d44e4b02d6638",  // Assets/CyanTrigger/Examples/OldExamples/Prefabs/WorldPlayerAudioSettingsOld.prefab
                "fe323a3a9f81e9143920fa3d2269bfda",  // Assets/CyanTrigger/Examples/OldExamples/Prefabs/Combat/SwordOld.prefab
                "44b02172e5f0b674181abf2b91fc4afa",  // Assets/CyanTrigger/Examples/Prefabs/CombatSetupV2.prefab
                "e476501f926aff94c933f46bb493c62c",  // Assets/CyanTrigger/Examples/Prefabs/DownloadImageCT.prefab
                "dcc9e273e0792224284ea1c9b63304aa",  // Assets/CyanTrigger/Examples/Prefabs/DownloadStringCT.prefab
                "2521024c9d223a34a9172ccadaf8cbba",  // Assets/CyanTrigger/Examples/Prefabs/LocomotionSettingsV2.prefab
                "e7f48e52a6b03154088323bfd7167011",  // Assets/CyanTrigger/Examples/Prefabs/MultiJumpSystem.prefab
                "71a537965c6b4354083fc3191ef5040d",  // Assets/CyanTrigger/Examples/Prefabs/PlayerAudioSettingsV2.prefab
                "f2b447d84e4aaef488ce264445e3d45b",  // Assets/CyanTrigger/Examples/Prefabs/PlayerBaseTrackerV2.prefab
                "bbc8f59f50d4f8c4ba5d219fc5a56cc5",  // Assets/CyanTrigger/Examples/Prefabs/PlayerBoneTrackerV2.prefab
                "0176d41702e7db546a6f83f32a0eb2c1",  // Assets/CyanTrigger/Examples/Prefabs/PlayerFadeControllerV2.prefab
                "b4b86a6de75997f4da7d561c108faeea",  // Assets/CyanTrigger/Examples/Prefabs/PlayerHaptics.prefab
                "bf4946afcb743e14a8828113616b7c40",  // Assets/CyanTrigger/Examples/Prefabs/PlayerTrackerV2.prefab
                "f9d22b322057ed24297b7d6c34acc530",  // Assets/CyanTrigger/Examples/Prefabs/UIMenuExample.prefab
                "ceb110819be5f294b976ebdf56779859",  // Assets/CyanTrigger/Examples/Prefabs/Combat/GunBase.prefab
                "222d5853be8a1134380480664f128250",  // Assets/CyanTrigger/Examples/Prefabs/Combat/GunParticle.prefab
                "a23087c5485b5314197a8c23cef16033",  // Assets/CyanTrigger/Examples/Prefabs/Combat/GunRaycast.prefab
                "e66f1082de59ace4590abf52a60fd50f",  // Assets/CyanTrigger/Examples/Prefabs/Combat/PVPColliderGroup.prefab
                "579987f781b6a914aa0eb1650510e0fa",  // Assets/CyanTrigger/Examples/Prefabs/Combat/SwordV2.prefab
                "f0f7d14aa6e01d04fb73f8e2883057dd",  // Assets/CyanTrigger/Examples/Prefabs/Combat/TrainingDummy.prefab
                "d495322368a6ffe449162fa74f26c5f1",  // Assets/CyanTrigger/Examples/Prefabs/Combat/TrainingDummyWithHealth.prefab
                "08ca2dad10aea66438346ff9c6823db8",  // Assets/CyanTrigger/Examples/Prefabs/Combat/VisualDamageBlank.prefab
                "cbe83c4dfbc31124ab6b5df17162c566",  // Assets/CyanTrigger/Examples/Prefabs/Pickups/Pickup Synced Broadcast Only.prefab
                "a41f0310f49eb8b46ab20c9c71b13230",  // Assets/CyanTrigger/Examples/Prefabs/Pickups/Pickup Synced Event Replay.prefab
                "7ab8b3d2e5514b842ad514080477c1e2",  // Assets/CyanTrigger/Examples/Prefabs/Pickups/Pickup Synced Variables.prefab
                "e2d6f7f7331a56843a5f4ddce10d773c",  // Assets/CyanTrigger/Examples/Prefabs/Pickups/Pickup Unsynced.prefab
                "0aff2e5b4688b4c4792774b261b5e43c",  // Assets/CyanTrigger/Examples/Prefabs/Pickups/[PooledObject] Pickup.prefab
                "664886dbaec50c54bbbbe629d7cf0074",  // Assets/CyanTrigger/Examples/Prefabs/Pickups/[SpawnedObject] Pickup.prefab
                "400097004e4a985489672f6a30faa2b0",  // Assets/CyanTrigger/Examples/Prefabs/SceneItems/ExampleDisplay.prefab
                "07ffd0f601f99d443b5f2332900a2e70",  // Assets/CyanTrigger/Examples/Prefabs/SceneItems/ExampleDisplayHalf.prefab
                "42597e5a0458b1a4481d3d2e6623b793",  // Assets/CyanTrigger/Examples/Scenes/CombatExamples.unity
                "0b1b0395bbc2cae46b5ed3919d6c3b9f",  // Assets/CyanTrigger/Examples/Scenes/DataCacheExamples.unity
                "ac0ce857083957846b370dc4c9b0aed1",  // Assets/CyanTrigger/Examples/Scenes/DataListDataDictionaryExamples.unity
                "b6d6da9b23c6ed141b78fc446debe3e7",  // Assets/CyanTrigger/Examples/Scenes/DownloaderExamples.unity
                "cebfa21a38dc0924a91d481a34d9617a",  // Assets/CyanTrigger/Examples/Scenes/FadeControllerExamples.unity
                "343777aac69ff5f4aa4420cc20dd9cb6",  // Assets/CyanTrigger/Examples/Scenes/GeneralExamples.unity
                "2ed1bac0392cea143a8c426e7e7e199c",  // Assets/CyanTrigger/Examples/Scenes/NavMeshExamples.unity
                "ceb42401632fd324488de00630a79f47",  // Assets/CyanTrigger/Examples/Scenes/PickupsExamples.unity
                "bea9a03f810ddad43b9a42d5a0dc224d",  // Assets/CyanTrigger/Examples/Scenes/PlayerApiExamples.unity
                "8a47c0c2bac46624a9264eb864da6714",  // Assets/CyanTrigger/Examples/Scenes/SyncExamples.unity
                "ea01e323eada9f249a8d86553fbdaa40",  // Assets/CyanTrigger/Examples/Scenes/UIExamples.unity
                "0a01743a579fd104091713b64c427b39",  // Assets/CyanTrigger/Examples/Scenes/VariablesAndLogicExamples.unity
                "707b6eba18bdb75499bcfa5232bda8c4",  // Assets/CyanTrigger/Examples/Scenes/NavMeshExamples/NavMesh.asset
                "1750ebdc375fc7440aaabebc9157801b",  // Assets/CyanTrigger/Examples/Scenes/UIExamples_Profiles/DarknessProfile.asset
                "b8edc5a8753b0134a9e45aa01aa38174",  // Assets/CyanTrigger/Examples/Shaders/UnlitDoubleSidedOverlay.shader
                "d39c3e7bce9b1a74480c0795a0134bf2",  // Assets/CyanTrigger/Examples/Textures/SquareSprite.png
                "f45afd6bc7178584eab425b2245c1ae5",  // Assets/CyanTrigger/Examples/UdonGraph/SetGameObjectActiveGraph.asset
                "63fd9f02bbcf1dc4dab3485b09eaebee",  // Assets/CyanTrigger/Resources/Images/CyanTriggerIcon.png
                "7028f647fdfc3ee49a226dd94cd4d97c",  // Assets/CyanTrigger/Resources/Images/CyanTriggerIconRed.png
                "b4e6255283ead0d479dfb1ae6615d988",  // Assets/CyanTrigger/Resources/Images/CyanTriggerIconYellow.png
                "1bc2baa62c763a64ea9c47561f78c1e7",  // Assets/CyanTrigger/Resources/Images/CyanTriggerLogo.png
                "1db59565dfd53994a86ddfb425c5a611",  // Assets/CyanTrigger/Resources/Prefabs/CyanTriggerResources.prefab
                "1d3e151e0bb296b408306844e78e514b",  // Assets/CyanTrigger/Resources/Settings/CyanTriggerFavorite_Actions.asset
                "c6919c5f2b5fe0e40a757aff57ad14eb",  // Assets/CyanTrigger/Resources/Settings/CyanTriggerFavorite_Events.asset
                "8acb5f80f7501164394db20f6d01f0c7",  // Assets/CyanTrigger/Resources/Settings/CyanTriggerFavorite_SDK2_Actions.asset
                "e91c7b0e8ad4e554795e6c78c4a73fa6",  // Assets/CyanTrigger/Resources/Settings/CyanTriggerFavorite_Variables.asset
                "6c02a27735aa4422bf3b0fea3cf16dbd",  // Assets/CyanTrigger/Runtime/AssemblyInfo.cs
                "b316372cfdd1005439302dbcea1ec180",  // Assets/CyanTrigger/Runtime/Cyan.CyanTrigger.asmdef
                "3dd4a7956009f7d429a09b8371329c82",  // Assets/CyanTrigger/Runtime/CyanTrigger.cs
                "7dbcb0ee0db04e7298f72e639d9e2588",  // Assets/CyanTrigger/Runtime/CyanTriggerAsset.cs
                "4a3d7ddbbcf56a94a801c5636abded4a",  // Assets/CyanTrigger/Runtime/CyanTriggerBehaviour.cs
                "9fbe6b59e3138ee47ae6bc55f0b22f5c",  // Assets/CyanTrigger/Runtime/CyanTriggerDataInstance.cs
                "f767ac69593b8de438a8792c51d62faa",  // Assets/CyanTrigger/Runtime/CyanTriggerEnums.cs
                "ebff42e294604d05a22cf425558dace0",  // Assets/CyanTrigger/Runtime/CyanTriggerEventArgData.cs
                "7713eb9a9c07bb442be0218a9aacbcb1",  // Assets/CyanTrigger/Runtime/CyanTriggerResources.cs
                "f10f55585aaa4862ac89da1965ac2a7d",  // Assets/CyanTrigger/Runtime/CyanTriggerScriptableObject.cs
                "36415435a8a5d6e45b02041b5b4605a5",  // Assets/CyanTrigger/Runtime/ICyanTrigger.cs
                "d657b8910ce728a4bab0dab48d4265e6",  // Assets/CyanTrigger/Runtime/ICyanTriggerCustomType.cs
                "cb62c66e31aa49aa87e6666bbf917be7",  // Assets/CyanTrigger/Runtime/ICyanTriggerProgramAsset.cs
                "9170218726654314e88c384bf963a8e8",  // Assets/CyanTrigger/Runtime/Extensions/CyanTriggerNameHelpers.cs
                "7c24ef78eae446a449d114a8312559e0",  // Assets/CyanTrigger/Runtime/Serializables/CyanTriggerSerializableObject.cs
                "5a44af3af1ab46242aa4a670dae51142",  // Assets/CyanTrigger/Runtime/Serializables/CyanTriggerSerializableType.cs
                "154e4a465cf24d1692d32f41ef3e4584",  // Assets/CyanTrigger/Runtime/Serializables/SerializableContainers/CyanTriggerContainerArray.cs
                "36c680aa7151463593e4f0dd11a79086",  // Assets/CyanTrigger/Runtime/Serializables/SerializableContainers/CyanTriggerPlaneContainer.cs
                "bfe621c716c442e2bb68494a7c3924d5",  // Assets/CyanTrigger/Runtime/Serializables/SerializableContainers/CyanTriggerRayContainer.cs
                "590151661d344685adfd0b9ab5e2d61d",  // Assets/CyanTrigger/Runtime/Serializables/SerializableContainers/CyanTriggerVector2IntContainer.cs
                "70ec4b559a0249ddba24beca89313552",  // Assets/CyanTrigger/Runtime/Serializables/SerializableContainers/CyanTriggerVector3IntContainer.cs
                "cc8434379c8345e9bf02fb7a1e991495",  // Assets/CyanTrigger/Runtime/Serializables/SerializableContainers/ICyanTriggerSerializableContainer.cs
                "6065325ad2dce804fa5beceb88e0a49d",  // Assets/CyanTrigger/Runtime/Utils/CyanTriggerCopyUtil.cs
                "c466ccb60a9e46098c8fc73f843219ff",  // Assets/CyanTrigger/Runtime/Utils/CyanTriggerDocumentationLinks.cs
                "e9683386335d4b11a722453e794bdc0e",  // Assets/CyanTrigger/Runtime/Utils/CyanTriggerTrie.cs
                "11091cc725e197041bc328046126d8f8",  // Assets/CyanTrigger/Settings/CyanTriggerSettings.asset
            };
        }

        string[] GetVitDeckGUIDs()
        {
            return new string[]
            {
                "302dedb10dfafbc4890a8bb3a14666c6",  // Assets/VitDeck/Validator/Runtime/VitDeck.Validator.Runtime.asmdef
                "6ca6388f2eb7b124c9822f0ec18507ee",  // Assets/VitDeck/Validator/Runtime/BoundsIndicators/BoothRangeIndicator.cs
                "61a91fd6b14a4c74d8def021d6ac569b",  // Assets/VitDeck/Validator/Runtime/BoundsIndicators/BoundsRangeOutIndicator.cs
                "087a446a9975ddb46a8258b730da5b4a",  // Assets/VitDeck/Validator/Runtime/BoundsIndicators/ColliderBoundsSource.cs
                "ff83b2b8a00549b44917c8d655d8d986",  // Assets/VitDeck/Validator/Runtime/BoundsIndicators/IBoothBoundsProvider.cs
                "fd2ce3eee33700a41882b61a9f0ab570",  // Assets/VitDeck/Validator/Runtime/BoundsIndicators/IBoundsSource.cs
                "0f7ad65a7e2041b2bb8c4f9c33a5f87b",  // Assets/VitDeck/Validator/Runtime/BoundsIndicators/IMeshLocalBoundsProvider.cs
                "d94df5fc33543984b847883374ecd8f6",  // Assets/VitDeck/Validator/Runtime/BoundsIndicators/LightProbeBoundsSource.cs
                "6bd3fe856f374b3b9eb59192b0cadf0f",  // Assets/VitDeck/Validator/Runtime/BoundsIndicators/MeshFilterLocalBoundProvider.cs
                "934a5f28e95c42347b819e98e4da1030",  // Assets/VitDeck/Validator/Runtime/BoundsIndicators/RectTransformRangeOutIndicator.cs
                "98a472ef355204d4bb8ea4843d56ca71",  // Assets/VitDeck/Validator/Runtime/BoundsIndicators/RendererBoundsSource.cs
                "ed421118c3b307d45a120e16c922b98a",  // Assets/VitDeck/Validator/Runtime/BoundsIndicators/ResetToken.cs
                "1aa83c466a8ea1e4ba2e7f307c221a3f",  // Assets/VitDeck/Validator/Runtime/BoundsIndicators/ResetTokenSource.cs
                "53967ee69b2a4ebb8ac928d4e5c65337",  // Assets/VitDeck/Validator/Runtime/BoundsIndicators/SkinnedMeshRendererLocalBoundsProvider.cs
                "4919afe7ffcff8544bc0a71dc7451fc5",  // Assets/VitDeck/Validator/Runtime/BoundsIndicators/TransformMemory.cs
                "af1597e2617a95e4a9d30a3020a864ee",  // Assets/VitDeck/Validator/Runtime/BoundsIndicators/TransformRangeOutIndicator.cs
            };
        }

        string[] GetExhibitTemplateGUIDs()
        {
            // "Assets/VitDeck/Templates/"以下に素材がある場合はバリデート許可するために追加する
            return new string[]
            {
            };
        }

        string[] GetVketAssetsGUIDs()
        {
            return new string[]
            { 
                "195f5ae85b179f247a0a6c24fbd4de2c",  // Assets/VketAssets/Assets/EssentialResources/Vket.EssentialResources.asmdef
                "de6e4588d200665409133a5b319bc25e",  // Assets/VketAssets/Assets/EssentialResources/Vket.EssentialResources.asset
                "6b7de8c3899fec040a5aa5c00c750b6c",  // Assets/VketAssets/Assets/EssentialResources/Attribute/OverrideRevertAttribute.cs
                "ec0fbca47f8fb6c4c8498c64ea0379f0",  // Assets/VketAssets/Assets/EssentialResources/Attribute/ReadOnlyAttribute.cs
                "d6e099f2ff659194a943acbc5922d1dd",  // Assets/VketAssets/Assets/EssentialResources/Attribute/SceneSingletonAttribute.cs
                "f77d6481894f7e3449f36db439005877",  // Assets/VketAssets/Assets/EssentialResources/Attribute/Vket.EssentialResources.Attribute.asmdef
                "a140c31eb71fbd045ad9ae6b07bd0f19",  // Assets/VketAssets/Assets/EssentialResources/Common/Fonts/License.txt
                "d410c15388ebb13449d51bca1b8724aa",  // Assets/VketAssets/Assets/EssentialResources/Common/Fonts/Mplus1-Regular.ttf
                "798bf62f082a7a64c9d48e6f992ecfaa",  // Assets/VketAssets/Assets/EssentialResources/Common/Materials/DisabledVideoPlayerImage.mat
                "0de3ccc1017c4a045a4fed5f810c2748",  // Assets/VketAssets/Assets/EssentialResources/Common/Materials/UI-Lookat.mat
                "d35acdd70bdcab445bbbd6deac832a3b",  // Assets/VketAssets/Assets/EssentialResources/Common/Shaders/UI-Lookat.shader
                "834f21d704cafe3498f4d2ecc38dc5ea",  // Assets/VketAssets/Assets/EssentialResources/Common/Textures/Background.png
                "86487b9f4f81a774a9c49d53278c76cf",  // Assets/VketAssets/Assets/EssentialResources/Common/Textures/Button.png
                "62f7352a395147043809a2d315af37ae",  // Assets/VketAssets/Assets/EssentialResources/Common/Textures/Change Avatar.png
                "866f25587f50b85479e2cf4d2d197fee",  // Assets/VketAssets/Assets/EssentialResources/Common/Textures/DisabledVideoPlayerImage.png
                "dc9da36508b0d8540a31f60446baf4b1",  // Assets/VketAssets/Assets/EssentialResources/Common/Textures/LoadingVideoPlayerImage.png
                "96c692c63aeba764081c1e04790fd3f1",  // Assets/VketAssets/Assets/EssentialResources/Common/Textures/Sample.png
                "abb75ce26e18f4944b01089401edb9fa",  // Assets/VketAssets/Assets/EssentialResources/ExhibitorBoothManager/ExhibitorBoothManager.prefab
                "d0e55bed1631139489f283284ae3127d",  // Assets/VketAssets/Assets/EssentialResources/ExhibitorBoothManager/RigidbodyManager.prefab
                "ef6b4b05c151b074f8f26aed781f169d",  // Assets/VketAssets/Assets/EssentialResources/ExhibitorBoothManager/Scripts/436f790abf020554584f7b7dc84c3cf5.asset
                "be6f72f5ef1db494c93c87c5352d6fb8",  // Assets/VketAssets/Assets/EssentialResources/ExhibitorBoothManager/Scripts/5faef3c9e78b8db48915c3e217687ebc.asset
                "c20bf0df08ed19548909355e9ca762cb",  // Assets/VketAssets/Assets/EssentialResources/ExhibitorBoothManager/Scripts/e2a4f0202851a304baae62480c082707.asset
                "436f790abf020554584f7b7dc84c3cf5",  // Assets/VketAssets/Assets/EssentialResources/ExhibitorBoothManager/Scripts/ExhibitorBoothActivator.asset
                "cabd73074c0408b4092f8aeda76292ee",  // Assets/VketAssets/Assets/EssentialResources/ExhibitorBoothManager/Scripts/ExhibitorBoothActivator.cs
                "5faef3c9e78b8db48915c3e217687ebc",  // Assets/VketAssets/Assets/EssentialResources/ExhibitorBoothManager/Scripts/ExhibitorBoothManager.asset
                "01b06d5ae8dc4084ba0749c7425258f4",  // Assets/VketAssets/Assets/EssentialResources/ExhibitorBoothManager/Scripts/ExhibitorBoothManager.cs
                "d8838554919271d42b711b7d0d0ddd2e",  // Assets/VketAssets/Assets/EssentialResources/ExhibitorBoothManager/Scripts/ExhibitorBoothManagerSetup.cs
                "e2a4f0202851a304baae62480c082707",  // Assets/VketAssets/Assets/EssentialResources/ExhibitorBoothManager/Scripts/RigidbodyManager.asset
                "395b0eb4df3b90f4eabb7287f5493788",  // Assets/VketAssets/Assets/EssentialResources/ExhibitorBoothManager/Scripts/RigidbodyManager.cs
                "0bccc7c4ef60cdd42a4d6c56881b1789",  // Assets/VketAssets/Assets/EssentialResources/ExhibitorBoothManager/Scripts/RigidbodyManagerSetup.cs
                "e64fcb6375f1f2d4fb9e36fe1f3c1252",  // Assets/VketAssets/Assets/EssentialResources/SetupEditorClasses/IgnoreBuildAttribute.cs
                "6aae90ab8f5d0c44a89ffe407ea78003",  // Assets/VketAssets/Assets/EssentialResources/SetupEditorClasses/IPreSaveExecute.cs
                "4e95883001de5a247a4a3591998880d8",  // Assets/VketAssets/Assets/EssentialResources/SetupEditorClasses/PreSaveExecuteFindAttachComponent.cs
                "4f7bef126bd030745bece564b09e0366",  // Assets/VketAssets/Assets/EssentialResources/SetupEditorClasses/PreSaveMarker.cs
                "ec4c8b9df80398343a908c40e5720347",  // Assets/VketAssets/Assets/EssentialResources/SetupEditorClasses/UdonOverrideReverter.cs
                "1f6e6084d5e313f4c9b309fa6f104850",  // Assets/VketAssets/Assets/EssentialResources/SetupEditorClasses/Editor/ExecuteOnPreSave.cs
                "e722fabc74e4b4a4b96870539bf379e4",  // Assets/VketAssets/Assets/EssentialResources/SetupEditorClasses/Editor/IgnoreComponentOnBuild.cs
                "e125af3ba7da62a41aefb41197b4d195",  // Assets/VketAssets/Assets/EssentialResources/VketTriggerScripts/Editor/AssemblyBuilder.cs
                "672a8dec0156e614f9de64ffecd0ac01",  // Assets/VketAssets/Assets/EssentialResources/VketTriggerScripts/Editor/DictionaryExtension.cs
                "96557f785ce40c0469ee40407efc4ac1",  // Assets/VketAssets/Assets/EssentialResources/VketTriggerScripts/Editor/TriggerParameters.cs
                "1056f83269d9ee04eaf9b0a0db30fbd4",  // Assets/VketAssets/Assets/EssentialResources/VketTriggerScripts/Editor/VketInteractProgramAsset.cs
                "c00c2e468a7ad97489ae83db8c039100",  // Assets/VketAssets/Assets/EssentialResources/VketTriggerScripts/Editor/VketOnBoothProgramAsset.cs
                "9da245cf3d6f1ab4c98ba350e3dc81a9",  // Assets/VketAssets/Assets/EssentialResources/VketTriggerScripts/Editor/VketTriggerProgramAsset.cs
                "ebc8de035ba5d6c419b38c68f06d18b8",  // Assets/VketAssets/Assets/VketPrefabs/LICENSE.txt
                "8ac885989b081be499135bd5e73437ce",  // Assets/VketAssets/Assets/VketPrefabs/version.txt
                "9ade6bede296164498060af8ff1eb032",  // Assets/VketAssets/Assets/VketPrefabs/AudioVolumeSample/vket_booth_samplemusic_-14lufs_-3db.ogg
                "ffb0f4100e356214882c9d0ae8d2c22a",  // Assets/VketAssets/Assets/VketPrefabs/Config/LanguageSettings.asset
                "72f7924d484de814c93b4040b0451409",  // Assets/VketAssets/Assets/VketPrefabs/Editor/Vket.VketPrefabs.Editor.asmdef
                "9da2be9c203472545a1adfb64482afa9",  // Assets/VketAssets/Assets/VketPrefabs/Editor/CustomEditor/VketAvatarPedestalEditor.cs
                "debdb950f48f57a4b96c20b8129c7799",  // Assets/VketAssets/Assets/VketPrefabs/Editor/CustomEditor/VketChairEditor.cs
                "847997774dc22644cb07f0162559de20",  // Assets/VketAssets/Assets/VketPrefabs/Editor/CustomEditor/VketFittingChairEditor.cs
                "da6e50005bc48a9489b7e79da116adc0",  // Assets/VketAssets/Assets/VketPrefabs/Editor/CustomEditor/VketFollowPickupEditor.cs
                "ccec4c1b85a590e43be9629afb9c5bbe",  // Assets/VketAssets/Assets/VketPrefabs/Editor/CustomEditor/VketImageDownloaderEditor.cs
                "11899ba963c270f4fb75ce294f7d700f",  // Assets/VketAssets/Assets/VketPrefabs/Editor/CustomEditor/VketLanguageSwitcherEditor.cs
                "1c972f4201842584ebc0d0a3a3fe476b",  // Assets/VketAssets/Assets/VketPrefabs/Editor/CustomEditor/VketPickupEditor.cs
                "db497477eca9d224793f5a24036ef45f",  // Assets/VketAssets/Assets/VketPrefabs/Editor/CustomEditor/VketPrefabInformationEditor.cs
                "e87907448f5887e4187cb8af21d2efdb",  // Assets/VketAssets/Assets/VketPrefabs/Editor/CustomEditor/VketSoundFadeEditor.cs
                "98cf120a7a4d88a44929333f18d5c8f3",  // Assets/VketAssets/Assets/VketPrefabs/Editor/CustomEditor/VketStringDownloaderEditor.cs
                "ecf98736a3824a94eae0cbbcfb09f0aa",  // Assets/VketAssets/Assets/VketPrefabs/Editor/CustomEditor/VketVideoPlayerEditor.cs
                "71b28fb00454b614cad2159bd4c51fc2",  // Assets/VketAssets/Assets/VketPrefabs/Editor/CustomEditor/VketVideoUrlTriggerEditor.cs
                "b4fd1a16e84a4b648910df602e9d6ba2",  // Assets/VketAssets/Assets/VketPrefabs/Editor/CustomEditor/VketWebPageOpenerEditor.cs
                "da1a89a885dea67429482097f86bae5c",  // Assets/VketAssets/Assets/VketPrefabs/Editor/SettingWindow/VketAvatarPedestalSettingWindow.cs
                "77573840d4eb9a348b2a684a9bc87d45",  // Assets/VketAssets/Assets/VketPrefabs/Editor/SettingWindow/VketChairSettingWindow.cs
                "2d90931f1fde20845ac67a2d7c8159f9",  // Assets/VketAssets/Assets/VketPrefabs/Editor/SettingWindow/VketFittingChairSettingWindow.cs
                "f0110a5eba4caf748b44c50af8c510c2",  // Assets/VketAssets/Assets/VketPrefabs/Editor/SettingWindow/VketFollowPickupSettingWindow.cs
                "33a91ca5f27b3d74c9b84087ce52bafd",  // Assets/VketAssets/Assets/VketPrefabs/Editor/SettingWindow/VketLanguageSwitcherSettingWindow.cs
                "f92ec3abb70194f4d9934ff851931928",  // Assets/VketAssets/Assets/VketPrefabs/Editor/SettingWindow/VketPickupSettingWindow.cs
                "651425bbea80fea4aa5ae007add4b4ed",  // Assets/VketAssets/Assets/VketPrefabs/Editor/SettingWindow/VketSoundFadeSettingWindow.cs
                "b4452f896b4229f4cbf65633c1eebc2e",  // Assets/VketAssets/Assets/VketPrefabs/Editor/SettingWindow/VketVideoPlayerSettingWindow.cs
                "0c36340064f88cf44a572a49b47b7f59",  // Assets/VketAssets/Assets/VketPrefabs/Editor/SettingWindow/VketVideoUrlTriggerSettingWindow.cs
                "21ee774dc28d47b46a5acfc434d2f233",  // Assets/VketAssets/Assets/VketPrefabs/Editor/SettingWindow/VketWebPageOpenerSettingWindow.cs
                "849502f006b52f149987bcef15a79ce4",  // Assets/VketAssets/Assets/VketPrefabs/Editor/SettingWindow/Base/VketPrefabSettingWindow.cs
                "b11626f35398e6840bd2c18ec6c03c17",  // Assets/VketAssets/Assets/VketPrefabs/Editor/SettingWindow/Base/VketPrefabsMainWindow.cs
                "b1f14e1620b75b6459b3bea2a478fbb6",  // Assets/VketAssets/Assets/VketPrefabs/Language/CSVParser.cs
                "90cb9d966dfb4f543adda431221d51f2",  // Assets/VketAssets/Assets/VketPrefabs/Language/English.asset
                "91747f0ba8b4163498faae1eaa894b4a",  // Assets/VketAssets/Assets/VketPrefabs/Language/Japanese.asset
                "94f07a9e629cf31409bdf67fb15b3e2b",  // Assets/VketAssets/Assets/VketPrefabs/Language/LanguageDictionary.cs
                "e098227997bfbf745ac487dd67609c04",  // Assets/VketAssets/Assets/VketPrefabs/Language/LanguageDictionaryEditor.cs
                "4f3117b02d6154b4985964bf3c92a8ca",  // Assets/VketAssets/Assets/VketPrefabs/Language/LanguageDictionaryTreeView.cs
                "683f2bad289e14241b39b3dc10794b01",  // Assets/VketAssets/Assets/VketPrefabs/Language/LanguageLoader.cs
                "d329abe2e66f1c74c9f04b7fe2f3be59",  // Assets/VketAssets/Assets/VketPrefabs/Language/LanguageSettings.cs
                "b5875cea08d1e8c40adfd5f2cf9caea7",  // Assets/VketAssets/Assets/VketPrefabs/Language/LanguageSettingsEditor.cs
                "608f722e6de934948810fe87e79a8392",  // Assets/VketAssets/Assets/VketPrefabs/Language/LocalizedMessage.cs
                "690171d82418a424d9bc0bef30225e8b",  // Assets/VketAssets/Assets/VketPrefabs/Language/Vket.VketPrefabs.Language.asmdef
                "2c70a653a699f5c4b9c51c2160786525",  // Assets/VketAssets/Assets/VketPrefabs/Plugins/CirclePageOpener.dll
                "5a76879e99e51074589a77c5477b7870",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/Vket.VketPrefabs.asmdef
                "bcdcee42feac2ab4db6ac9f7388c5ae6",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/Vket.VketPrefabs.asset
                "32013c443db419448b8c9f83a8654166",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketPrefabInformation.cs
                "da2f193786576d041aa8d2e860314c22",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/VketAvatarPedestal_2D.prefab
                "9fffe84a94533884eaf481963546643d",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/VketAvatarPedestal_3D.prefab
                "1e0f83d3ba1d83045866a6a4dc2e8e83",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/VketAvatarPedestal_Default.prefab
                "0294f3138a383d44188238141e43a0d2",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/Animations/Active_2D.anim
                "8ecf1c1367fda5c45b707eaaf1e6e300",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/Animations/Active_3D.anim
                "826602674183e284685c8212ca89f3ca",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/Animations/AvatarPedestal_2D.controller
                "2f0b20cb1dadf3c498b1272896dd3ba5",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/Animations/AvatarPedestal_3D.controller
                "cc64c7910ebf50249bc5cfdc65ba4729",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/Animations/Deactive_2D.anim
                "68bbad9fbab0708449c847419d62a17e",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/Animations/Deactive_3D.anim
                "6b468349f6cba0248a76b7d33570fbad",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/Animations/Idle_2D.anim
                "8d5e148b91b24cf4dae7e22e010f7603",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/Animations/Idle_3D.anim
                "cf07f0e4c426cd141911a052ae670aee",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/Animations/Open_2D.anim
                "c48a0e2b59d493b4f9f9a865b1471676",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/Animations/Open_3D.anim
                "7fdd6e0e6a66ddb40afafdfe7cbc0e89",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/Scripts/62a7876d06fb1d645ab6cb81d87d0a3a.asset
                "62a7876d06fb1d645ab6cb81d87d0a3a",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/Scripts/VketAvatarPedestal.asset
                "ebe3df2ed38fdfd479e2fce1c5403a74",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketAvatarPedestal/Scripts/VketAvatarPedestal.cs
                "fc94f96f00c165842bddecda68e0e3fe",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketChair/VketChair.prefab
                "d1d36f4319c73e941b11296c909b98dc",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketChair/VketFittingChair.prefab
                "8e8ed867d413bac44b0184b86b91694b",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketChair/Scripts/168ec153ca936bb45960a6d7e7a43aa2.asset
                "a2a2354f3feb8e5488909d09b16541d2",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketChair/Scripts/e9db0ff267d5ebb46abed2e03b9f5b35.asset
                "e9db0ff267d5ebb46abed2e03b9f5b35",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketChair/Scripts/VketChair.asset
                "b981b20127ff3554487b6782b542fc74",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketChair/Scripts/VketChair.cs
                "168ec153ca936bb45960a6d7e7a43aa2",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketChair/Scripts/VketFittingChair.asset
                "85e51283e74194d41b24a5218a24f9ae",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketChair/Scripts/VketFittingChair.cs
                "04f73cff8c985724da36ca6890c417fb",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketImageDownloader/VketImageDownloader.prefab
                "8448694318db126429f93416cc803ccf",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketImageDownloader/Scripts/87e17ac428032da4aae9391eac831375.asset
                "87e17ac428032da4aae9391eac831375",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketImageDownloader/Scripts/VketImageDownloader.asset
                "c5cb4c2f19fde0e47ba2755bf9d84349",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketImageDownloader/Scripts/VketImageDownloader.cs
                "8c011f4ab9cc45c4aaddb76bbd8003c5",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketLanguageSwitcher/VketLanguageSwitcher.prefab
                "3692e13500377f04380bd63d295d7fbc",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketLanguageSwitcher/Scripts/26e911c1e4e64964ea73100994e7c984.asset
                "26e911c1e4e64964ea73100994e7c984",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketLanguageSwitcher/Scripts/VketLanguageSwitcher.asset
                "450cb8826e2f672478ba0b27310dd446",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketLanguageSwitcher/Scripts/VketLanguageSwitcher.cs
                "7f697f0a5403d864a9240a9f0d1e83e4",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketLanguageSwitcher/Textures/SwitchToEnglish.png
                "1e87d7c02aa11184aab67f6998f6a03b",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketLanguageSwitcher/Textures/SwitchToJapanese.png
                "ba410268b82f1d940aedd0d418541c83",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketPickup/VketFollowPickup.prefab
                "6d1e9170d4533ed448e46b707a3ee47a",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketPickup/VketPickup.prefab
                "6893d5acfbed8d544856d8eb7cc11133",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketPickup/Animations/AnimationOverrideEmpty.overrideController
                "3ffeb968a1d1ed244b45c026bb22f30b",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketPickup/Animations/ModeController.controller
                "be21e07eff32d7e4fbbf7babfed27d88",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketPickup/Animations/PickupAnimation.fbx
                "22cb79620122c3046a9d91c51d720baa",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketPickup/Scripts/ab981b08fcfada8458fc2ec950e16e17.asset
                "7683222f972ff444c910f4920ce600dd",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketPickup/Scripts/fc11049e6474c5e47bc42f47d1a8efca.asset
                "ab981b08fcfada8458fc2ec950e16e17",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketPickup/Scripts/VketFollowPickup.asset
                "57498a849a57d5e44bb3fea02cfabbad",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketPickup/Scripts/VketFollowPickup.cs
                "fc11049e6474c5e47bc42f47d1a8efca",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketPickup/Scripts/VketPickup.asset
                "b5d9b5598ab43f64e8aca7422be14f5c",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketPickup/Scripts/VketPickup.cs
                "b2a6c13adeda05d40adb398906d58645",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketSoundFade/VketSoundFade.prefab
                "725962251625c26438771124296958f6",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketSoundFade/Scripts/d6755e37e53268542aae9bd79954a6ab.asset
                "d6755e37e53268542aae9bd79954a6ab",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketSoundFade/Scripts/VketSoundFade.asset
                "2a96b9847fc7b3a4cb515cee9955948c",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketSoundFade/Scripts/VketSoundFade.cs
                "d19ff96a19f6fdb4cb57095e22e5ba37",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketStringDownloader/VketStringDownloader.prefab
                "0bbc261b54b68804c9d84f48b86a1f11",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketStringDownloader/Scripts/d138cc1df68c7c34799e1fef51bfa01e.asset
                "d138cc1df68c7c34799e1fef51bfa01e",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketStringDownloader/Scripts/VketStringDownloader.asset
                "2c916c2ca6c3b6744a276feed6d294b8",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketStringDownloader/Scripts/VketStringDownloader.cs
                "73b0727ab433c3140929fbf088cd8b88",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/VketVideoPlayer.prefab
                "b291170635bff9841bbd09d362a0d170",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/VketVideoUrlTrigger.prefab
                "e1ebd5b1f825bdf499c380c43453e0d6",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/Scripts/5426b85d610dd5a4990a6965e3716f2d.asset
                "6da6d496ffb0b2c438bf36868f1c313d",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/Scripts/c7b58c5d2d42fb643b222ef67f6f4e46.asset
                "5426b85d610dd5a4990a6965e3716f2d",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/Scripts/VketVideoPlayer.asset
                "9e731e574c230934d9ae7df37f8a2603",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/Scripts/VketVideoPlayer.cs
                "c7b58c5d2d42fb643b222ef67f6f4e46",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/Scripts/VketVideoUrlTrigger.asset
                "06f4bcee88c71d048ad06b704324653c",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/Scripts/VketVideoUrlTrigger.cs
                "90fa024f91bf8854a90b6a03ff496d34",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/Textures/AtlasTexture.png
                "02195086c78b86849a556a6dc54fe736",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/Textures/Frame.png
                "42d15b0f44115d348beb2c81dcbe3949",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/Textures/player-pause.png
                "a1692d4b866210d48be96e568853e55e",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/Textures/player-play.png
                "6818807c0a610724588cc04297536f0e",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/Textures/player-skip-back.png
                "6ae4d97d5fd711b478d402d686afdfbf",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/Textures/player-stop.png
                "ce1a6e132e3e68848a1fb26429aed5ac",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/Textures/volume-high.png
                "e459b75489b4af340a7621bc04e785cf",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketVideoPlayer/Textures/volume-no.png
                "5d4f49b1d4a5dca43b04aed3bc01b61f",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/VketCirclePageOpener_2D.prefab
                "829918e636553bf489526e19e7c08a9f",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/VketCirclePageOpener_3D.prefab
                "249a82240095e1a44b9b4aae5f72d41e",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/VketItemPageOpener_2D.prefab
                "8b95eab6f59b5e64d9393292aca982ca",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/VketItemPageOpener_3D.prefab
                "af49ba9be5e5e2149b9c6dfa6decec82",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/Animations/Active_2D.anim
                "f770e16ed5ac5c149b9e34d271b9fea4",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/Animations/Active_3D.anim
                "495ce3af8d25dcd49ab1eb9da5692373",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/Animations/Deactive_2D.anim
                "a41d2fdf7f5e5eb49ab85919cf41e188",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/Animations/Deactive_3D.anim
                "f93cc62348e48c64cbdb3a9b069a1d11",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/Animations/Idle_2D.anim
                "ff7931a8fe75c2142a4e83797c8c21e9",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/Animations/Idle_3D.anim
                "26ac8287216792149b49a2db74fc0309",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/Animations/Open_2D.anim
                "72fddf1509a7339498051122d1da2ce5",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/Animations/Open_3D.anim
                "251fd152628ba744899b986ab59ff7d3",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/Animations/WebPageOpener_2D.controller
                "aac12a50b7cdf694eb2fc4defc7776da",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/Animations/WebPageOpener_3D.controller
                "90a112a9a649fba4d8d442e1d75e7b69",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/Scripts/7704391c33fb5e44a9759bcae27b38a8.asset
                "7704391c33fb5e44a9759bcae27b38a8",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/Scripts/VketWebPageOpener.asset
                "e1e4c1b5ec275be4c832348eed80ecc7",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/Scripts/VketWebPageOpener.cs
                "171b9d597b1e63f4590b8d754491769a",  // Assets/VketAssets/Assets/VketPrefabs/Runtime/VketWebPageOpener/Textures/BUY.png
                "3c0dbec26839f9b4ea24f9606ec62ce4",  // Assets/VketAssets/Assets/VketPrefabs/UITemplate/UI_Button.prefab
                "b4625b5c33c27804d889d16704b81c33",  // Assets/VketAssets/Assets/VketPrefabs/UITemplate/UI_Image.prefab
                "4dc5396d6e370ef4fa9b9e9458c3f735",  // Assets/VketAssets/Assets/VketPrefabs/UITemplate/UI_Text.prefab
                "333992c7f0890014d9a775e3f2303857",  // Assets/VketAssets/Assets/VketPrefabs/UITemplate/UI_TextMeshPro.prefab
                "f22d22ce133301c46b2a5a5458e9c996",  // Assets/VketAssets/Assets/VketPrefabs/UITemplate/FontAssets/Mplus1-Regular Static.asset
                "20cf75c90d5e1dc459b132ea44e65c72",  // Assets/VketAssets/Assets/VketTriggerExamples/VketTriggerExampleScene.unity
                "0be12b7569010d94b9b17f6343a71efa",  // Assets/VketAssets/Assets/VketTriggerExamples/SceneAssets/107c77d0ec82f9d4e83bfb68de193341.asset
                "bb00e746c18043d47ab88e4e4ea0c4fd",  // Assets/VketAssets/Assets/VketTriggerExamples/SceneAssets/2db0d4bdf2e175a45bfea9d196e184f8.asset
                "7c77782274e76654e8cd0aefdfb978ab",  // Assets/VketAssets/Assets/VketTriggerExamples/SceneAssets/AnimationMove.anim
                "c227fd2cc782eef47abc5da50df7eebb",  // Assets/VketAssets/Assets/VketTriggerExamples/SceneAssets/Animator.controller
                "2db0d4bdf2e175a45bfea9d196e184f8",  // Assets/VketAssets/Assets/VketTriggerExamples/SceneAssets/InteractActiveExample.asset
                "107c77d0ec82f9d4e83bfb68de193341",  // Assets/VketAssets/Assets/VketTriggerExamples/SceneAssets/InteractAnimationExample.asset
            };
        }

        string[] GetVketShaderPackGUIDs()
        {
            return new string[]
            {
                "917bb21acbedd3c49b3a66fee0e2a0cd",  // Assets/VketShaderPack/ArxCharacterShaders/Changelog.txt
                "10c126862b1dd7c4b975a244eae01be8",  // Assets/VketShaderPack/ArxCharacterShaders/LICENSE
                "09a5bf6d0f820794eac61eb205435d54",  // Assets/VketShaderPack/ArxCharacterShaders/README.md
                "02a5e10e436c65143afc3f55c8dccfa3",  // Assets/VketShaderPack/ArxCharacterShaders/Editor/AxCommon.cs
                "74c2419ffb2b7fc498b21f8a9a2abbf6",  // Assets/VketShaderPack/ArxCharacterShaders/Editor/AxInspector.cs
                "0571380a9e1852d4f9f5793db83f0b9e",  // Assets/VketShaderPack/ArxCharacterShaders/Editor/AxTips.cs
                "dcc2ee0355c9c784fb78055de8a258b4",  // Assets/VketShaderPack/ArxCharacterShaders/Editor/Generator/AxGenerator.cs
                "18de271c249802b4db0152d713266719",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/ExampleScene.unity
                "79a525cab24559e43ae6b5e827a2a004",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Avatars/ExampleCloth.fbx
                "8576b2a3980cc7d428c9fcf4c773334d",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Avatars/ExampleCloth.mat
                "7edc206d0d21ee9478a881264cc03384",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Avatars/ExampleCloth_Albedo.png
                "29e20095c169a3d488f85915ab9f530a",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Avatars/ExampleCloth_MetallicSmoothness.png
                "4014c6efa5ab58b4ab03473f124eb88a",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Avatars/ExampleCloth_Normal.png
                "54fe456d16edc1045b0384347a9cdcad",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Avatars/exampleSkin.fbx
                "eae4cef569efe05418ebeea9a5470208",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Avatars/ExampleSkin.mat
                "f03a76cde03963e49813cb5bfbb9382d",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Avatars/ExampleSkin.png
                "6dabe3f5ec5f2df4bbb120d41bc6c434",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/ExampleScene/LightingData.asset
                "6848a47ec41a9a941910e7e98833fa8d",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/ExampleScene/Lightmap-0_comp_dir.png
                "38768f86cdcaa364cbf21b423dc1614e",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/ExampleScene/Lightmap-0_comp_light.exr
                "57d7184b37b19d340853ed08767c968c",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/ExampleScene/Lightmap-0_comp_shadowmask.png
                "184e612202758f847ba2977caa5ec274",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/ExampleScene/ReflectionProbe-0.exr
                "0dcae42798d529846add40a686a83a59",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/ExampleScene/ReflectionProbe-1.exr
                "f5eaf837eecc2b54895d58157ba14a8a",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/HalfSphere.fbx
                "236293ca368fe104983dec8869734d4d",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/mannequin.fbx
                "695a55d0643fd6a4099e2c1f906ef260",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/roundedCube.fbx
                "4e16100b62f2359498532c66b47882cd",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials/mat1.mat
                "6109487d6db0bd4449ab735a5af1984e",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials/material.mat
                "f488c7b5e7cf3974197144a0c871825d",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials/violin.mat
                "bc2847aff86b2d74285b7aa6d4121834",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/Cubemap.png
                "e9114b7dbd9d6044a8d3d4029b34db3d",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/FadeTextureExample.png
                "a20890ef2243e6740a27944fef0a8933",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/grdient_horizontal.png
                "f3bb94ff69520514ca588ec857f9f8c5",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/grdient_left.png
                "0993a6e1132b1ad4fa2d52aa2ac048af",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/grdient_top.png
                "294b9de34e762ad4b9b879c455a02428",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/grdient_vertical.png
                "64af6d10c296b2e43ba609d43989fa6d",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/mannequin-knit.mat
                "bf69b0161b0200144bc341c870898c79",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/mannequin-steel.mat
                "cf33e0e0d33f92b4aa05de50fb005b22",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/mannequin_hada_AlbedoTransparency.png
                "8234aa6d2fae5544cacef35fef1df204",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/mannequin_hada_MetallicSmoothness.png
                "a376e4dc1b84cf94d928917debfbfa80",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/mannequin_hada_Normal.png
                "843d938656ee20c43891c4bc0155978d",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/mannequin_knit_albedo.png
                "7142a7f4005cf1e428c040ae10878196",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/mannequin_knit_normal.png
                "45fdda763b21e774b88fe8a7736e4164",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/MaskExample.jpg
                "e5299c3e58f1f4b498f47a9485ceb4a5",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/MaskExample2.jpg
                "e79de89966752634ab702f63917e4b3e",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/Matcap.jpg
                "320667c8a80001d48839fda503844c7d",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/Matcap2.jpg
                "31aa7b785640d264395ce394d7200634",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/Matcap3.png
                "29d082225a76ffd4e8f1ab6c7867d520",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/material 1.mat
                "80930cdb070cfdd4198ba453a8d6928c",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/material 2.mat
                "b6619d1e3ab73d44683abcc8d4d10ade",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/material 3.mat
                "152846edca303b04798828a2a8ed5632",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/material 4.mat
                "4945243b525e98e4da08d6bcbf09e9c9",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/material 5.mat
                "c1dc601b37be9f642a8ba8fa2e297ebc",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/material 6.mat
                "f5555ea0e7eaacd4ebf16bf193191202",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/material.mat
                "1b47fc1779f32b74e84554551a52332b",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialEmissiveFreak.mat
                "6e8733029fd04b74a87a60c54da93bef",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialEmissiveFreakCutout.mat
                "6b15922ce569b954ba8a2ffa74113aa7",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialEmissiveFreakFade.mat
                "c61b7358e6597ea4f9ee82aedc2f7bd8",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialEmissiveFreakFadeRefracted.mat
                "db772d6df514e144cbad8bdaa16ec12a",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialStencilReader.mat
                "608bcb9d4fcab8740a0ce41811326a59",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialStencilReaderFade.mat
                "a4678bc73ce194441a3d163bb1cbd7ae",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialStencilWriter.mat
                "d5e3014f3f488c140a05c07aef01b7f7",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialStencilWriterEmissiveFreak.mat
                "7e7cd7b6a5999d8449acf559d795c8e6",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/roundedCubeAlbedo.png
                "28b7665efe9dab84f85cdb2c8169f6b0",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/roundedCubeNormal.png
                "dccdd3baf6a5eb044b64b4eeb8f4edff",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/ShadeCap-Soft.png
                "5a099e8cfeba411469d2e42ade1bfcd3",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/ShadeCap.png
                "890ffeaa37864ca45be34e58ae5ca0c9",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/starfield.png
                "bccf1a29daed7e347a5365f4ef9714aa",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Cutout.shader
                "9ce3973bae37a5c43a3c22309be11936",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/EmissiveFreak_Cutout.shader
                "9a50e49ef4da46f42869e7bb359e38ae",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/EmissiveFreak_Fade.shader
                "64c7ca6ee0374bb49928cc04ae72e564",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/EmissiveFreak_FadeRefracted.shader
                "f64626cad72e22a4d9c5b9f2edcae765",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/EmissiveFreak_Opaque.shader
                "82b077c6cd0a1bf49924f2e40bbaa11a",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/EmissiveFreak_StencilReader_Cutout.shader
                "2f13f85e17b00034082d64dcb9f7e465",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/EmissiveFreak_StencilReader_DoubleFade.shader
                "efa8f9f56db47404997e88b85819baea",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/EmissiveFreak_StencilReader_Fade.shader
                "745457dca56c6574fba900c741151b79",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/EmissiveFreak_StencilReader_FadeRefracted.shader
                "8f7fc78e18d31314daa105489df1315e",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/EmissiveFreak_StencilWriter_Cutout.shader
                "d6d2255661809bc4e87babd1a8edaff6",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Fade.shader
                "0c89b4121f71eb546abb7748e6c7d25f",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/FadeRefracted.shader
                "acaabbe907212b94c85ab392bb1306a1",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Opaque.shader
                "ed3ee25e40abe154ea5386166422d8fd",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Outline_Cutout.shader
                "844a9d3f6c0fa1044abe14728e14e7e9",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Outline_EmissiveFreak_Cutout.shader
                "ed627574387225c449eaccd6fdbc6d15",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Outline_EmissiveFreak_Opaque.shader
                "f14d4aa117bff4e4e8626947f279fc44",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Outline_EmissiveFreak_StencilReader_Cutout.shader
                "8da7952f673810b41afcb4fb3dbe2673",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Outline_EmissiveFreak_StencilWriter_Cutout.shader
                "87d1c60aa88359946943a8f85f3f67ce",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Outline_Opaque.shader
                "4c3618b923505674184591106c01b13d",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Outline_StencilReader_Cutout.shader
                "49071b1dd6dced84ebbae1ad62c386c5",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Outline_StencilWriter_Cutout.shader
                "034f74b4fdca8cc4a941b0de9ce3616f",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/StencilReader_Cutout.shader
                "e8cd3ae42f616ee48875891991c1ecad",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/StencilReader_DoubleFade.shader
                "727d200179b6d6c469453149d0678cd5",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/StencilReader_Fade.shader
                "b1e4cf9f03cc70a4aa9ec9f8b26c967e",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/StencilReader_FadeRefracted.shader
                "1adb078ef83bc634d91fa5a5e4961844",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/StencilWriter_Cutout.shader
                "396631bc9e5cc9442af1325b1fe9a6c7",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_Cutout.shader
                "20d4c21fc774e6848a7f160f034ac106",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_EmissiveFreak_Cutout.shader
                "f4aa6cae9314eef408b2027d29bd1c12",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_EmissiveFreak_Fade.shader
                "7451a766106f532499d76da495deb5c9",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_EmissiveFreak_FadeRefracted.shader
                "52e2dc1921da94d4192403141eaf1853",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_EmissiveFreak_Opaque.shader
                "474fe9685060c3548a4566e32bc59445",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_EmissiveFreak_StencilReader_Cutout.shader
                "4c44853470ead7a4f9ef4d058ba65f6e",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_EmissiveFreak_StencilReader_DoubleFade.shader
                "5a388d322c36a514082b705f08fa69ee",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_EmissiveFreak_StencilReader_Fade.shader
                "cd68fac637ec6394dac4d76709758f15",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_EmissiveFreak_StencilReader_FadeRefracted.shader
                "6ace59df9308a67468145f2ef8b858cc",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_EmissiveFreak_StencilWriter_Cutout.shader
                "fecc8335cd6c7a6408b9cf376c83e723",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_Fade.shader
                "bc1dfcec858408f458da1831ac3d39d2",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_FadeRefracted.shader
                "64ed6c8a1f3fde64f8bad49eaf1b7be7",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_Opaque.shader
                "35fedec71ddf32340b821c27f1706e5c",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_Outline_Cutout.shader
                "6296778d0d5dcd34dacaec02d0cfdc30",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_Outline_EmissiveFreak_Cutout.shader
                "c64009349dc1273409192851d4c9df12",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_Outline_EmissiveFreak_Opaque.shader
                "bfa9bd51f43c0e044be224260f7b0227",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_Outline_EmissiveFreak_StencilReader_Cutout.shader
                "4be8b350dca07e043a0adf35efc543ad",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_Outline_EmissiveFreak_StencilWriter_Cutout.shader
                "3ab98ad607734274bb8b5d3b8fb87cb6",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_Outline_Opaque.shader
                "c073f6c709cd22a4f8deaa7aa9bbbf33",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_Outline_StencilReader_Cutout.shader
                "fefd38d0c6abc9f479f5be65349140b8",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_Outline_StencilWriter_Cutout.shader
                "047c52475dc26b841bea80ee98a9bff2",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_StencilReader_Cutout.shader
                "36e42f678162c5a41a5a4fcff50b1c2d",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_StencilReader_DoubleFade.shader
                "ef8e5023d3f52724a89e5054a6eec21f",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_StencilReader_Fade.shader
                "72d58515ae944e34d888fffcf0c1cb23",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_StencilReader_FadeRefracted.shader
                "c813a483312d6ab40876410a11b28734",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/Tessellation_StencilWriter_Cutout.shader
                "88114f823868e46499f38529e274c874",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/cginc/arkludeAdd.cginc
                "53197a94cb19cef4f87146e3a60dcbd0",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/cginc/arkludeDecl.cginc
                "6279286c116b40c4b89f4515985a80db",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/cginc/arkludeFadeShadowCaster.cginc
                "bcb34ccca9630de43803b596ab9fb33d",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/cginc/arkludeFrag.cginc
                "6c29e94a661d9564f81f237f71561d0c",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/cginc/arkludeFragOnlyStencilWrite.cginc
                "58d2bd24894859c40bb08f1c498b8773",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/cginc/arkludeOther.cginc
                "e67fd5424f7ffc84d98abffd21c5fd78",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/cginc/arkludeTessellation.cginc
                "347c7b749e91cff4cb7b0751b8f7ec9c",  // Assets/VketShaderPack/ArxCharacterShaders/Shaders/cginc/arkludeVertGeom.cginc
                "df58530a9e453994fa2432bfb44df077",  // Assets/VketShaderPack/ArxCharacterShaders/Textures/HighLightCameraMask1.png
                "df746c19b06154d4c93c8a496644c5e7",  // Assets/VketShaderPack/ArxCharacterShaders/Textures/HighLightCameraMask2.png
                "1f23fd27dcc42c14badf69a05e1b58d6",  // Assets/VketShaderPack/ArxCharacterShaders/Textures/HighLightCameraMask3.png
                "b02421efb41760f40b01282ef78dd393",  // Assets/VketShaderPack/ArxCharacterShaders/Textures/HighLightCameraMask4.png
                "bea93105aa1daac43974972a792f133f",  // Assets/VketShaderPack/ArxCharacterShaders/Textures/Ramps.ai
                "9384d986d3c93e84b896716821787d47",  // Assets/VketShaderPack/ArxCharacterShaders/Textures/Ramp_1.png
                "f848c68db2ea54546905195e13b42731",  // Assets/VketShaderPack/ArxCharacterShaders/Textures/Ramp_2.png
                "a249399272899034eb992ad37ffd0b81",  // Assets/VketShaderPack/ArxCharacterShaders/Textures/Ramp_3.png
                "748abec0e90c15e4983b5d12f2f03917",  // Assets/VketShaderPack/ArxCharacterShaders/Textures/Ramp_4.png
                "c164a5690bb1f3646ae8b26e0d170783",  // Assets/VketShaderPack/ArxCharacterShaders/Textures/Ramp_5.png
                "5ddbcea7f4f05a44b87e3f5b7af74baa",  // Assets/VketShaderPack/ArxCharacterShaders/Textures/Ramp_6.png
                "05c75ecad50759a48afd09f49736b73b",  // Assets/VketShaderPack/ArxCharacterShaders/Textures/Ramp_7.png
                "242351f50fe050d40879c196cfd8b0a8",  // Assets/VketShaderPack/ArxCharacterShaders/Textures/Ramp_Default.png
                "7e3e2734c20f8704986e4b44f7950257",  // Assets/VketShaderPack/Filamented/CrossStandardTest.mat
                "8bb11aba411074a45ba9b48cdabf4447",  // Assets/VketShaderPack/Filamented/dfg-c.exr
                "b6b1f1fa6be1ce54f8bcd5428c160a28",  // Assets/VketShaderPack/Filamented/dfg-ms.exr
                "15666b1e4a43bd44ab4d0fde9f680eb1",  // Assets/VketShaderPack/Filamented/dfg-s.exr
                "68dc287072e9d5a439cd6b82033899d6",  // Assets/VketShaderPack/Filamented/FilamentBRDF.cginc
                "4bd29bb1bce8f064eafa7eb6f8f830eb",  // Assets/VketShaderPack/Filamented/FilamentCommonDithering.cginc
                "2eddc0a27592cca48b1b8f5d14e9c083",  // Assets/VketShaderPack/Filamented/FilamentCommonGraphics.cginc
                "b84d59f7502b982408baa3fa4d9a3081",  // Assets/VketShaderPack/Filamented/FilamentCommonLighting.cginc
                "5060180d12900874c95c28e958268e8d",  // Assets/VketShaderPack/Filamented/FilamentCommonMaterial.cginc
                "5246a13d4fd80814c965c1f0197094c5",  // Assets/VketShaderPack/Filamented/FilamentCommonMath.cginc
                "ee3cbadb87ab9a24bb6e23ffd201081e",  // Assets/VketShaderPack/Filamented/FilamentCommonOcclusion.cginc
                "8a20a50a44d8c5c4c84c86eb379db405",  // Assets/VketShaderPack/Filamented/FilamentCommonShading.cginc
                "88e4dbdb6ade3b14da90a528eaec41f5",  // Assets/VketShaderPack/Filamented/FilamentedTemplate.shader
                "15d843c037dba3d44b1241383a0caf43",  // Assets/VketShaderPack/Filamented/FilamentedTemplateRefraction.shader
                "bba5baa3e3ab1e1408b673307df0108e",  // Assets/VketShaderPack/Filamented/FilamentedTextureBlending.shader
                "044744ced35f660428c5b5f0477a6e1d",  // Assets/VketShaderPack/Filamented/FilamentedTriplanar.shader
                "57bf21b20c08f4f449bbd667549ef91b",  // Assets/VketShaderPack/Filamented/FilamentLightDirectional.cginc
                "7e6d53b497bfe2f46a359456c3dade1a",  // Assets/VketShaderPack/Filamented/FilamentLightIndirect.cginc
                "5ea4f7fc41b41824ba22dfe97495b44b",  // Assets/VketShaderPack/Filamented/FilamentLightLTCGI.cginc
                "394fef5252d2f1347875e9ed94bae290",  // Assets/VketShaderPack/Filamented/FilamentLightPunctual.cginc
                "22e197c1606ed7c4994632b8b210f3e6",  // Assets/VketShaderPack/Filamented/FilamentMaterialInputs.cginc
                "f2290f6a0fe3159419f54d6dace56a9d",  // Assets/VketShaderPack/Filamented/FilamentShadingCloth.cginc
                "521be760d19f10745a6999176e511627",  // Assets/VketShaderPack/Filamented/FilamentShadingLit.cginc
                "ff03188209cc2b94e8c53a67c475e856",  // Assets/VketShaderPack/Filamented/FilamentShadingParameters.cginc
                "b4066f85461bf6e47b85dcf8bd04104c",  // Assets/VketShaderPack/Filamented/FilamentShadingStandard.cginc
                "49eff6157baaf4d4e947236f66994da6",  // Assets/VketShaderPack/Filamented/LICENSE
                "fe7137cdc73277e499772b823ca1974b",  // Assets/VketShaderPack/Filamented/SharedFilteringLib.hlsl
                "5c0e8e86dec25b641897fd7c8211bb90",  // Assets/VketShaderPack/Filamented/SharedNormalShadowLib.hlsl
                "ebaf59039a775d64fbb2ae9cfd1322d3",  // Assets/VketShaderPack/Filamented/SharedParallaxLib.hlsl
                "25cfa7644c2f4bc4c91c60f98b7b63a4",  // Assets/VketShaderPack/Filamented/SharedSamplingLib.hlsl
                "9829e18681954944a8b25de9b080a0b6",  // Assets/VketShaderPack/Filamented/Standard.shader
                "deca4036fd687004ca874acb20f3fd5f",  // Assets/VketShaderPack/Filamented/StandardCloth.shader
                "075dbc3edcecd8747919086245d79325",  // Assets/VketShaderPack/Filamented/StandardRoughness.shader
                "d1e71c1f97670b843a43924d87ceb04b",  // Assets/VketShaderPack/Filamented/StandardSpecular.shader
                "50a0b962e7beb424ca767ae9656c4ed4",  // Assets/VketShaderPack/Filamented/UnityGlobalIllumination.cginc
                "3f43f0fa2318dbc4bb77ee6a480bc42a",  // Assets/VketShaderPack/Filamented/UnityImageBasedLightingMinimal.cginc
                "bf6599aa7f0fc1541ba5f01441e20a46",  // Assets/VketShaderPack/Filamented/UnityLeftovers.cginc
                "7bb28a683afed86479840c9c04e34118",  // Assets/VketShaderPack/Filamented/UnityLightingCommon.cginc
                "5035d8f031d926640b7729712fd7b546",  // Assets/VketShaderPack/Filamented/UnityStandardConfig.cginc
                "a1dd140dcb1e25f499a372f1c7e3ffdd",  // Assets/VketShaderPack/Filamented/UnityStandardCore.cginc
                "8e949ac172157a349a6c5a6b7d4bfe9e",  // Assets/VketShaderPack/Filamented/UnityStandardCoreForward.cginc
                "b82b8515f6fc2424b82b84c40f636d93",  // Assets/VketShaderPack/Filamented/UnityStandardInput.cginc
                "988393ba3605c6d439da49623e84146f",  // Assets/VketShaderPack/Filamented/UnityStandardMeta.cginc
                "a0a28b8f70e8f7d43b7045ffc69e4691",  // Assets/VketShaderPack/Filamented/UnityStandardShadow.cginc
                "6e4b7f491e8ccad4f97d96ae867ed5fa",  // Assets/VketShaderPack/Filamented/UnityStandardUtils.cginc
                "3e0f5f45ae0344f45bbf4779532ef4a9",  // Assets/VketShaderPack/Filamented/Editor/AltGUI.asmdef
                "371fe9b78188bc94182dc330a2b90f25",  // Assets/VketShaderPack/Filamented/Editor/FilamentedStandardShaderGUI.cs
                "3424a6582de421043b7c9fdc83f9e5e4",  // Assets/VketShaderPack/Filamented/Editor/ReplaceShader.cs
                "124c0d2131c2c654f8cde0e74478e4b4",  // Assets/VketShaderPack/Filamented/Editor/UpdateBakerySHMode.cs
                "3e475cdc396583e41b50cc858691ceb7",  // Assets/VketShaderPack/lilToon/CHANGELOG.md
                "d06477b230acf4940b70049030a8d306",  // Assets/VketShaderPack/lilToon/CHANGELOG_JP.md
                "0ad54d988ad680e4e96036b7e5646f00",  // Assets/VketShaderPack/lilToon/LICENSE
                "397d2fa9e93fb5d44a9540d5f01437fc",  // Assets/VketShaderPack/lilToon/package.json
                "eed4a4536acc30b419b91c2bf1ca399e",  // Assets/VketShaderPack/lilToon/README.md
                "50fe6c2708c26874aa2586ef9f6f9cad",  // Assets/VketShaderPack/lilToon/README_JP.md
                "dddcf659c4c0f1a41914529704d2a3f5",  // Assets/VketShaderPack/lilToon/SettingLock.json
                "f206f66109fb1a040a1a9ac212ff8f41",  // Assets/VketShaderPack/lilToon/Third Party Notices.md
                "c9dd6a844225d504fae29b983bf95995",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lilCustomShaderDatas.lilblock
                "3c88ead8db0b5b845ae92ee1910fbf00",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts.lilinternal
                "e54285d89347373438cab8fb9ecf495b",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsl.lilinternal
                "0a32de5c81b815d449152e2e94757c11",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsl_cutout.lilinternal
                "d36cfa740ffabbf4eb218ebec025773f",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsl_cutout_o.lilinternal
                "2ac10282a355aca4f95af2695bca6d38",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsl_o.lilinternal
                "c0a29f5180f108e4d84110f461c8b023",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsl_onetrans.lilinternal
                "ccefaa3102c672f409ee8d2f18d928f8",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsl_onetrans_o.lilinternal
                "4e826208b1b5997408c6bd6966fc57a1",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsl_overlay.lilinternal
                "a1c3ee0332cd460469919db8bc57ab8d",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsl_overlay_one.lilinternal
                "ee409fcd68cebe548a900885d6b04228",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsl_trans.lilinternal
                "9ff130fbfd11749429353d5cd9ad8d8e",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsl_trans_o.lilinternal
                "910bd65957249754188b082cce77e90f",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsl_twotrans.lilinternal
                "1993b568d943fd841a9d1022ae1ba501",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsl_twotrans_o.lilinternal
                "a975dab3b4ab6f346a1bc3119bf74963",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsmulti.lilinternal
                "bf37d9b55aed8a24e810be7498ec970c",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsmulti_fur.lilinternal
                "79abc181b18a2234f9a4d4c0e4489d74",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsmulti_gem.lilinternal
                "d5f896e09b43d144cb7481869ab75b83",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsmulti_o.lilinternal
                "a146987e6182fbd44b99e27a2ee88b0f",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltsmulti_ref.lilinternal
                "834985f94b43cb94c8e0eb8b305255ac",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltspass_baker.lilinternal
                "c3006fa88f758644c964e3a6d0f9cc8e",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltspass_cutout.lilinternal
                "b372f727dd3bf5943829a865f7292a1b",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltspass_lite_cutout.lilinternal
                "bf502fb880aa31c47a7315e9ffbff85a",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltspass_lite_opaque.lilinternal
                "9fa0b606799f40d42be0bd53dd521e89",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltspass_lite_transparent.lilinternal
                "5a12bb22862d779439fb11f240b24a3d",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltspass_opaque.lilinternal
                "247c834c8a61a6342bf6ace853c9e66c",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltspass_proponly.lilinternal
                "4f2776effc2db5447b049cc192367280",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltspass_tess_cutout.lilinternal
                "4fae626e00ffa5a41a14de200b47de1a",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltspass_tess_opaque.lilinternal
                "93d228154ccd81c408cab8f622e72823",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltspass_tess_transparent.lilinternal
                "6b20d895d976f444691f35b5e42cecaf",  // Assets/VketShaderPack/lilToon/BaseShaderResources/ltspass_transparent.lilinternal
                "392fb61d6aba2dd4fb79153e7174da5c",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_cutout.lilinternal
                "4f60e4568bcdd044dbb93448634eea81",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_cutout_o.lilinternal
                "d704b852f3259b5449423441f859cabb",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_cutout_oo.lilinternal
                "f898bd30ebd640a4ba0872d07d80ff75",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_fakeshadow.lilinternal
                "338cdeed8228d524c9b3fafed3c1b84f",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_fur.lilinternal
                "7e9b0eed12308db4ba34f32574c46112",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_furonly.lilinternal
                "d778904dee5051c4b931dce1c80b8220",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_furonly_cutout.lilinternal
                "f2cc57947e10f294cadbabde5caa2c4e",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_furonly_two.lilinternal
                "8fe0d6be8d8e5804295ce446ba35aa35",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_fur_cutout.lilinternal
                "f5fd23346ca8db143b4f4b6c4e568c08",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_fur_two.lilinternal
                "adf2912223933c2418d6850f4f9eb94f",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_gem.lilinternal
                "ab059f869afd76a4389ad759b7261bf8",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_o.lilinternal
                "6ff738bded7cc2c4d8422338aeb6e35c",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_onetrans.lilinternal
                "e912cd31e0b254842a7555108479ed81",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_onetrans_o.lilinternal
                "a35cea713e0c698489216cb7a5df0166",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_oo.lilinternal
                "a7299c31f3470be45a8663933238bb47",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_overlay.lilinternal
                "8333b8dc9a16c7c43845eaf5ac57b317",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_overlay_one.lilinternal
                "466d488d091c32f4d8625a2ddca7c7ff",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_ref.lilinternal
                "f685d4615cde46b4d9d96f5157be4055",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_ref_blur.lilinternal
                "b7a8a885af014a44481c3818f4030d89",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_tess.lilinternal
                "ac4e5e5c11d34a04cabadc67071a64a4",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_tess_cutout.lilinternal
                "070de9f5c8b0a7c438d2c6d27722d0d3",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_tess_cutout_o.lilinternal
                "f9343b99f16b8b9409498d7651d87bd7",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_tess_o.lilinternal
                "4df70d99bb9c0304db730aa2397c08de",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_tess_onetrans.lilinternal
                "c7909e3e79baae841be4e54bdd2c218d",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_tess_onetrans_o.lilinternal
                "bb34dfe1e43a62348b5b5bb52770a7c6",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_tess_trans.lilinternal
                "903ceed4d30b124419a8f723c6c9e813",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_tess_trans_o.lilinternal
                "f67e38458e2d61a429570fc6438b8cc4",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_tess_twotrans.lilinternal
                "eddba9570dbc40e43b0029cec3a1b8fc",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_tess_twotrans_o.lilinternal
                "87550f35c17d44843be41107c3d50296",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_trans.lilinternal
                "afaa68d2de9c8064cafbaa9260fdd978",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_trans_o.lilinternal
                "41759bb4b04a4654696a050e1cca5c44",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_trans_oo.lilinternal
                "6cae238c07a3aa34abcb79c058a52b22",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_twotrans.lilinternal
                "6f9d16c23608d0445945a1f443ebd87f",  // Assets/VketShaderPack/lilToon/BaseShaderResources/lts_twotrans_o.lilinternal
                "6ebd61d6e59969d43af7fccbdec0e863",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/Default.lilblock
                "6c7c6ea0cfb556b41a446160f4dd72e0",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultFakeShadow.lilblock
                "8e7f8a4718705c745a415663d0b39e5d",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultFur.lilblock
                "5eb181e8f5bdd344394711e2d0aa33bb",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultFurTwoPass.lilblock
                "5925c530c913c4240bc95f0a0a490760",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultGem.lilblock
                "9cf3e07844b8997479e0d390f5b2b262",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultLite.lilblock
                "0a0ee5b6c7efca646b758511c4e5c93b",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultLiteTwoSide.lilblock
                "2c93e6dd050037949b2f476bdf063cd9",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultMulti.lilblock
                "16181a2cf01e31c4e9787de436c89bf2",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultMultiFur.lilblock
                "b0bad0f2615172e4982d62c745927949",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultMultiGem.lilblock
                "bfb1b5de7e49a7e4cb01567cbb03a113",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultMultiOutline.lilblock
                "acef6a319b1ed094e8a9e7c5266f4819",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultMultiRefraction.lilblock
                "efa02a893a9c77044b71522deeb0538c",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultRefraction.lilblock
                "0d43882e5e489874a8a7077741713715",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultRefractionBlur.lilblock
                "d814b8829fb05bc4f9aa50543561d7e3",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultTessellation.lilblock
                "da0c0af4b2bc3ea4e9c4c61be252d9a2",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultTessellationTwoSide.lilblock
                "b9708550141f10e43bb3459d5085ce6c",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultTwoSide.lilblock
                "00f5dea61082f9047832df96848c7ae2",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultUsePass.lilblock
                "4f411ebc4b040c649b166d8e6717fc77",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultUsePassFurOnly.lilblock
                "4ae07ee463221304293956bf2f15ded5",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultUsePassFurOnlyTwoPass.lilblock
                "b8d0e7c3bef5f314f83c8e0d63521e51",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultUsePassNoForwardAdd.lilblock
                "620db7f8e34f72140a8f468ffa1cb1bd",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultUsePassNoForwardAddTwo.lilblock
                "428a3fc553730bf4899e4b4ce39959dc",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultUsePassOutline.lilblock
                "bced9aae66b144f438238df93290a8c4",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultUsePassOutlineNoForwardAdd.lilblock
                "2148db6b95d2a5746b4e98f042303551",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultUsePassOutlineNoForwardAddTwo.lilblock
                "64cd17e64423ad54c93b2bc3565b85c3",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultUsePassOutlineOnly.lilblock
                "bdb497c8d186c0245ae1af1bbb1ba7c0",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultUsePassOverlay.lilblock
                "1ecf000c4bfed3b4ea74aba3f1956e16",  // Assets/VketShaderPack/lilToon/CustomShaderResources/BRP/DefaultUsePassOverlayNoForwardAdd.lilblock
                "a64f7da9c81e33a419ca1be23db274fc",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/Default.lilblock
                "5cadb973160f403488e58f5ea9c9d5ef",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultFakeShadow.lilblock
                "be9bfef949d2cf64894907b66c326962",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultFur.lilblock
                "ec3c15b0b24338c41b099770d56cfbf2",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultFurTwoPass.lilblock
                "adc33142a62c77c4988827b484fcc28d",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultGem.lilblock
                "3906b4fe9b2dc1749a6f6b3f73b6e0dd",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultLite.lilblock
                "c23cd36fbb7e4a74eab43cb9d52ba2aa",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultLiteTwoSide.lilblock
                "8403ccf21be7951498107a9c05eb036b",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultMulti.lilblock
                "78edba89c27973c4d872d5acb97ddb49",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultMultiFur.lilblock
                "099f08aefc3f6c74d9ee0ec3df919fd9",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultMultiGem.lilblock
                "b7d19310e8c28d846816d0ee4dd1ef55",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultMultiOutline.lilblock
                "f7e54a173d5111f4f96bee3a041c4db2",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultMultiRefraction.lilblock
                "01ee6f4fd5310594c9e21252bbb3b4c7",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultRefraction.lilblock
                "5dd8756254fb31749a5358c8a016024a",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultRefractionBlur.lilblock
                "30a9295b35a61c54088a3b40502b80ad",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultTessellation.lilblock
                "d0a9c6457c374ec42adf9c3b7572f792",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultTessellationTwoSide.lilblock
                "fe8077ab809b0c949a0bd4254706715b",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultTwoSide.lilblock
                "4ee13c47ed046a44aa56cfe097299517",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultUsePass.lilblock
                "bef72fca1ee8e644784cca859bb5ea81",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultUsePassFurOnly.lilblock
                "2ab592bb32393214a868d09a13bf5063",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultUsePassFurOnlyTwoPass.lilblock
                "480bdca31a882bb40b2d22b7b8d63912",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultUsePassOutline.lilblock
                "6402151328afd96488288abb67d3b620",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultUsePassOutlineOnly.lilblock
                "12a5a658d1e02d44fb80fe9919467f29",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultUsePassOutlineTwoSide.lilblock
                "2a987eb1e65490941b28df3622d18d30",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultUsePassOverlay.lilblock
                "84383628f721b1a45a4ccb8743f62543",  // Assets/VketShaderPack/lilToon/CustomShaderResources/HDRP/DefaultUsePassTwoSide.lilblock
                "6f81c1e4decfeef40bd0373a1c272aae",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/Default.lilblock
                "31e569e8abbf0934180a99b7b1a93fed",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultFakeShadow.lilblock
                "6c0802963fc1f8e44accf44a889d5919",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultFur.lilblock
                "81ac836e59899a04688d2ee5489f49df",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultGem.lilblock
                "8a131adf58d7ebd4f997018082145fb3",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultLite.lilblock
                "30f154ac0ba7232459893a88749cda94",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultMulti.lilblock
                "9143ec0d4677acd43927bb69cd1cda1b",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultMultiFur.lilblock
                "0f103d6b53fb84547b9fa73dfa6d3afd",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultMultiGem.lilblock
                "448812a833587e64986bb97db89a3bdb",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultMultiOutline.lilblock
                "8313281cdfa5e824e943e525c9167d5d",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultMultiRefraction.lilblock
                "4b28ae398a79802498b44d995c17f61e",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultRefraction.lilblock
                "a64c06017b143824aaa49d77ec00c393",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultRefractionBlur.lilblock
                "9373cce6cc2477d49b1ed7235ad83114",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultTessellation.lilblock
                "b3b20e7176d1ee649856ccd9a7c74a2e",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultUsePass.lilblock
                "3e0b26e4465bee845b415c4b985f11b0",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultUsePassFurOnly.lilblock
                "5aff6f07c94ee1e4b8b1a0667f231708",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultUsePassOutline.lilblock
                "cbe3b405688ea014c92e5ea176b0090b",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultUsePassOutlineOnly.lilblock
                "69e4aaf771a269745a72739117621d48",  // Assets/VketShaderPack/lilToon/CustomShaderResources/LWRP/DefaultUsePassOverlay.lilblock
                "d7fa25c78b38bbb4fa3a5c9b1dd4f46f",  // Assets/VketShaderPack/lilToon/CustomShaderResources/Properties/Default.lilblock
                "62ddfb21f2f86464086c1a2d5b53c8da",  // Assets/VketShaderPack/lilToon/CustomShaderResources/Properties/DefaultAll.lilblock
                "c856b023d8a0dd441ba39eb3a28554fb",  // Assets/VketShaderPack/lilToon/CustomShaderResources/Properties/DefaultCutout.lilblock
                "b33476967d36e3143aa915a3d1e9097a",  // Assets/VketShaderPack/lilToon/CustomShaderResources/Properties/DefaultFakeShadow.lilblock
                "dbfd8bcca48bc4a42b819210b3c69b76",  // Assets/VketShaderPack/lilToon/CustomShaderResources/Properties/DefaultFurCutout.lilblock
                "a465e9a7ac56e5a418e010f6c348082b",  // Assets/VketShaderPack/lilToon/CustomShaderResources/Properties/DefaultFurTransparent.lilblock
                "d309f3cf3370f764cbf2a8b331649e89",  // Assets/VketShaderPack/lilToon/CustomShaderResources/Properties/DefaultGem.lilblock
                "2b3e8b14255901b4aa9076d5b3702f3f",  // Assets/VketShaderPack/lilToon/CustomShaderResources/Properties/DefaultLite.lilblock
                "d606e20cb007db44f8a51f967149568c",  // Assets/VketShaderPack/lilToon/CustomShaderResources/Properties/DefaultOpaque.lilblock
                "76b615f827dd4934188b73496792cf34",  // Assets/VketShaderPack/lilToon/CustomShaderResources/Properties/DefaultRefraction.lilblock
                "07d1f74ed36ca5941a3d35c9c51abf19",  // Assets/VketShaderPack/lilToon/CustomShaderResources/Properties/DefaultTransparent.lilblock
                "1fa9fc04ea38cda4795242e8c9241e92",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/Default.lilblock
                "1515ef93c2b80134db785ddc1512bd26",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultFakeShadow.lilblock
                "9bda4ec5bfb8516458df75e1a185716f",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultFur.lilblock
                "314c973dce805b74e96ce9a63de07a56",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultFurTwoPass.lilblock
                "961baf17f871eb54296407f50922b3dc",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultGem.lilblock
                "4fb29da42d1cd9949925db60be31b013",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultLite.lilblock
                "74108b09605a5bf42a5c62a171394b72",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultLiteTwoSide.lilblock
                "f9705638ec77d6e43b2435aa70be6e13",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultMulti.lilblock
                "f38085a4c53e19646a7e9431db5e4c63",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultMultiFur.lilblock
                "18b966d7e027df543837bf5295d8a4d3",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultMultiGem.lilblock
                "8d3b22fef4361a04a9b31d85a7cac0b0",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultMultiOutline.lilblock
                "cefcd13bac037984a88dae574d6d2ca0",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultMultiRefraction.lilblock
                "9553c5eaad8a50c4f951cfe14584ea9a",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultRefraction.lilblock
                "2341159bc9ad4ca48b131d077085c04c",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultRefractionBlur.lilblock
                "b84271f40c7935348a3cd2e6b6ad4241",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultTessellation.lilblock
                "793ed55269c25af4a853288256961839",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultTessellationTwoSide.lilblock
                "6236a75c2c60df7478099584884584e3",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultTwoSide.lilblock
                "95d7eda411290484783f75e6af497e3b",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultUsePass.lilblock
                "5a61d0630d28c694892a19e9947e6fc1",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultUsePassFurOnly.lilblock
                "a9266652633a6c04e9a99dd62fb00edd",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultUsePassFurOnlyTwoPass.lilblock
                "759e1e97cdbff9443beb81b9fb9885de",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultUsePassOutline.lilblock
                "d2e23c2368064ee4dae459416be4b593",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultUsePassOutlineOnly.lilblock
                "e27e9b6f6d46fd349b9b5d153597df2e",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultUsePassOutlineTwoSide.lilblock
                "01465615d40a0e24496ade150efb677d",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultUsePassOverlay.lilblock
                "8e57254ea5f70ba4b89f617df6d09169",  // Assets/VketShaderPack/lilToon/CustomShaderResources/URP/DefaultUsePassTwoSide.lilblock
                "17c2370ca1618cd428c70b9966b5666f",  // Assets/VketShaderPack/lilToon/Editor/csc.rsp
                "142b3aeca72105442a83089b616e92b8",  // Assets/VketShaderPack/lilToon/Editor/CurrentRP.txt
                "bf37c7fc33ca52e4aa3bf227039f0b3b",  // Assets/VketShaderPack/lilToon/Editor/lilConstants.cs
                "16ce355c89524144ba07853555caf3af",  // Assets/VketShaderPack/lilToon/Editor/lilDirectoryManager.cs
                "cb188ab387b43d64fb32f4b171886a16",  // Assets/VketShaderPack/lilToon/Editor/lilEditorGUI.cs
                "602ca744f7854714490591fa69f178e2",  // Assets/VketShaderPack/lilToon/Editor/lilEnumeration.cs
                "aefa51cbc37d602418a38a02c3b9afb9",  // Assets/VketShaderPack/lilToon/Editor/lilInspector.cs
                "1efa0081aac1a464fa60d26881d2d238",  // Assets/VketShaderPack/lilToon/Editor/lilLanguageManager.cs
                "08e11b7a56e70ee448b6abf5aca2e8f0",  // Assets/VketShaderPack/lilToon/Editor/lilMaterialProperty.cs
                "4f8fa439eb71a9340b0ea2cd90947b89",  // Assets/VketShaderPack/lilToon/Editor/lilMaterialUtils.cs
                "ba359247b3512914c963370174c3e0d4",  // Assets/VketShaderPack/lilToon/Editor/lilOptimizer.cs
                "ef60dad7e5cdbfb4cb0c49c788dde24e",  // Assets/VketShaderPack/lilToon/Editor/lilPropertyNameChecker.cs
                "3fcbf3d5c2bc8f942a20ef548c18ea86",  // Assets/VketShaderPack/lilToon/Editor/lilRenderPipelineReader.cs
                "dd55f8a842290cd4193118c389862e57",  // Assets/VketShaderPack/lilToon/Editor/lilSemVerParser.cs
                "64ff19b6b10348547b41f334ec1a1b9e",  // Assets/VketShaderPack/lilToon/Editor/lilShaderAPI.cs
                "3089979ac9fdd004ba564a7e5418ee8d",  // Assets/VketShaderPack/lilToon/Editor/lilShaderContainerImporter.cs
                "cf7f696b10993fb42a637d34deb7de09",  // Assets/VketShaderPack/lilToon/Editor/lilShaderManager.cs
                "285f5fec2e8cdc74db8c86bce70805bd",  // Assets/VketShaderPack/lilToon/Editor/lilStartup.cs
                "e753050b495ae6e4faa372fc78a4b7e8",  // Assets/VketShaderPack/lilToon/Editor/lilTextureUtils.cs
                "e7f0f8dffe955d640bbc76d1d4f4986e",  // Assets/VketShaderPack/lilToon/Editor/lilToon.Editor.asmdef
                "21405dfa11118c942a590c0eaa7f57ff",  // Assets/VketShaderPack/lilToon/Editor/lilToonAssetPostprocessor.cs
                "5849833bec4d7ba46bb9aa428ea3c8e4",  // Assets/VketShaderPack/lilToon/Editor/lilToonEditorUtils.cs
                "330d117753a947947bf5a4f237b9a9f3",  // Assets/VketShaderPack/lilToon/Editor/lilToonPreset.cs
                "e4575f0fa316410499a82e6e3ceb5562",  // Assets/VketShaderPack/lilToon/Editor/lilToonPropertyDrawer.cs
                "99d3fc7e373356646b25831556309409",  // Assets/VketShaderPack/lilToon/Editor/lilToonSetting.cs
                "bb1313c9ea1425b41b74e98fd04bcbc8",  // Assets/VketShaderPack/lilToon/Editor/Resources/gui_box_inner_dark.png
                "a72199a4c9cc3714d8edfbc5d3b13823",  // Assets/VketShaderPack/lilToon/Editor/Resources/gui_box_inner_half_dark.png
                "8343038a4a0cbef4d8af45c073520436",  // Assets/VketShaderPack/lilToon/Editor/Resources/gui_box_inner_half_light.png
                "f18d71f528511e748887f5e246abcc16",  // Assets/VketShaderPack/lilToon/Editor/Resources/gui_box_inner_light.png
                "29f3c01461cd0474eab36bf2e939bb58",  // Assets/VketShaderPack/lilToon/Editor/Resources/gui_box_outer_dark.png
                "16cc103a658d8404894e66dd8f35cb77",  // Assets/VketShaderPack/lilToon/Editor/Resources/gui_box_outer_light.png
                "45dfb1bafd2c7d34ab453c29c0b1f46e",  // Assets/VketShaderPack/lilToon/Editor/Resources/gui_custom_box_dark.png
                "a1ed8756474bfd34f80fa22e6c43b2e5",  // Assets/VketShaderPack/lilToon/Editor/Resources/gui_custom_box_light.png
                "a63ad2f5296744a4bad011de744ba8ba",  // Assets/VketShaderPack/lilToon/Editor/Resources/lang.txt
                "eb9a5de8acbfdac438a1069c8c834dae",  // Assets/VketShaderPack/lilToon/External/Editor/ChilloutVRModule.cs
                "814e85e19a2ab7d4d873e1630ad85654",  // Assets/VketShaderPack/lilToon/External/Editor/lilToon.Editor.External.asmdef
                "70e462e0b8466484d80daad8d29b6f79",  // Assets/VketShaderPack/lilToon/External/Editor/VRChatModule.cs
                "8c80e2aa732631141a1b44d552a96d88",  // Assets/VketShaderPack/lilToon/Prefabs/FurCollider (range=x.xx22).prefab
                "72377412f6a548c459a5e79549f29dff",  // Assets/VketShaderPack/lilToon/Presets/Cloth-Anime.asset
                "193de7d9d533d4841842d8c5ed740259",  // Assets/VketShaderPack/lilToon/Presets/Cloth-Illust.asset
                "5132cf0fbee6ea540831dc73b68c8c25",  // Assets/VketShaderPack/lilToon/Presets/Cloth-Outline.asset
                "3af05fa678d24444199540173d53c648",  // Assets/VketShaderPack/lilToon/Presets/Cloth-Standard.asset
                "13a5da17b9b512c45a20e627ef499e01",  // Assets/VketShaderPack/lilToon/Presets/Hair-Anime.asset
                "b66bf1309c6d60847ae978e0a54ac5fa",  // Assets/VketShaderPack/lilToon/Presets/Hair-Illust.asset
                "2357e878227675d4bade1cc9e4c2f8ca",  // Assets/VketShaderPack/lilToon/Presets/Hair-Outline.asset
                "1c73e759f4a3a5a44ba7bdc8688222d6",  // Assets/VketShaderPack/lilToon/Presets/Hair-OutlineRimLight.asset
                "8e6edeb6e37727e419e0020134094068",  // Assets/VketShaderPack/lilToon/Presets/Hair-Standard.asset
                "3c845c480ba4fcc4ca3814d63d57e84a",  // Assets/VketShaderPack/lilToon/Presets/Inorganic-Glass.asset
                "e229834c4b98edf4694dfbf4007f3927",  // Assets/VketShaderPack/lilToon/Presets/Inorganic-LiteGlass.asset
                "7f510b0fbfb36ad4285444097c033ed3",  // Assets/VketShaderPack/lilToon/Presets/Inorganic-Metal (MatCap).asset
                "e15c3abc25bfc0e45b18e369741eff76",  // Assets/VketShaderPack/lilToon/Presets/Inorganic-Metal.asset
                "c58d8c29115455b439bf6ad346fc558b",  // Assets/VketShaderPack/lilToon/Presets/Nature-Fur.asset
                "322c901472f2b9a4d98da370ea954214",  // Assets/VketShaderPack/lilToon/Presets/Skin-Anime.asset
                "125301c732c00f84091ef099d83833b7",  // Assets/VketShaderPack/lilToon/Presets/Skin-Flat.asset
                "44e146d270da72d4cb21a0a3b8658d1a",  // Assets/VketShaderPack/lilToon/Presets/Skin-Illust.asset
                "7d0193c3ed109f040bafea9b2a6a17b6",  // Assets/VketShaderPack/lilToon/Presets/Skin-Outline.asset
                "dbec582958af3f340988b3ff86a12633",  // Assets/VketShaderPack/lilToon/Presets/Skin-OutlineShadow.asset
                "df12117ecd77c31469c224178886498e",  // Assets/VketShaderPack/lilToon/Shader/lts.shader
                "381af8ba8e1740a41b9768ccfb0416c2",  // Assets/VketShaderPack/lilToon/Shader/ltsl.shader
                "b957dce3d03ff5445ac989f8de643c7f",  // Assets/VketShaderPack/lilToon/Shader/ltsl_cutout.shader
                "8cf5267d397b04846856f6d3d9561da0",  // Assets/VketShaderPack/lilToon/Shader/ltsl_cutout_o.shader
                "583a88005abb81a4ebbce757b4851a0d",  // Assets/VketShaderPack/lilToon/Shader/ltsl_o.shader
                "34c2907eba944ed45a43970e0c11bcfd",  // Assets/VketShaderPack/lilToon/Shader/ltsl_onetrans.shader
                "701268c07d37f5441b25b2cb99fae4b3",  // Assets/VketShaderPack/lilToon/Shader/ltsl_onetrans_o.shader
                "d28e4b78ba8368e49a44f86c0291df58",  // Assets/VketShaderPack/lilToon/Shader/ltsl_overlay.shader
                "dc9ded9f9d6f16c4e92cbb8f4269ae31",  // Assets/VketShaderPack/lilToon/Shader/ltsl_overlay_one.shader
                "0e3ece1bd59542743bccadb21f68318e",  // Assets/VketShaderPack/lilToon/Shader/ltsl_trans.shader
                "1c12a37046f07ac4486881deaf0187ea",  // Assets/VketShaderPack/lilToon/Shader/ltsl_trans_o.shader
                "82226adb1a0b8c4418f574cfdcf523da",  // Assets/VketShaderPack/lilToon/Shader/ltsl_twotrans.shader
                "62df797f407281640a224388953448cc",  // Assets/VketShaderPack/lilToon/Shader/ltsl_twotrans_o.shader
                "9294844b15dca184d914a632279b24e1",  // Assets/VketShaderPack/lilToon/Shader/ltsmulti.shader
                "1e50f1bc4d1b0e34cbf16b82589f6407",  // Assets/VketShaderPack/lilToon/Shader/ltsmulti_fur.shader
                "69f861c14129e724096c0955f8079012",  // Assets/VketShaderPack/lilToon/Shader/ltsmulti_gem.shader
                "51b2dee0ab07bd84d8147601ff89e511",  // Assets/VketShaderPack/lilToon/Shader/ltsmulti_o.shader
                "d7af54cdd86902d41b8c240e06b93009",  // Assets/VketShaderPack/lilToon/Shader/ltsmulti_ref.shader
                "f96a89829ccb1e54b85214550519a8d6",  // Assets/VketShaderPack/lilToon/Shader/ltspass_baker.shader
                "ad219df2a46e841488aee6a013e84e36",  // Assets/VketShaderPack/lilToon/Shader/ltspass_cutout.shader
                "2bde4bd29a2a70a4d9cf98772a6717ac",  // Assets/VketShaderPack/lilToon/Shader/ltspass_dummy.shader
                "8a6ef0489c3ffbf46812460af3d52bb0",  // Assets/VketShaderPack/lilToon/Shader/ltspass_lite_cutout.shader
                "59b5e58e88aae8a4ca42d1a7253e2fb2",  // Assets/VketShaderPack/lilToon/Shader/ltspass_lite_opaque.shader
                "8773c83ab40fff24b800f74360819a6c",  // Assets/VketShaderPack/lilToon/Shader/ltspass_lite_transparent.shader
                "61b4f98a5d78b4a4a9d89180fac793fc",  // Assets/VketShaderPack/lilToon/Shader/ltspass_opaque.shader
                "fd68f52288a6b0243bf6c217bf0930ea",  // Assets/VketShaderPack/lilToon/Shader/ltspass_proponly.shader
                "14006db8206fb304aa86110d57626d40",  // Assets/VketShaderPack/lilToon/Shader/ltspass_tess_cutout.shader
                "7a7ac427f85673a45a3e4190fc10bc28",  // Assets/VketShaderPack/lilToon/Shader/ltspass_tess_opaque.shader
                "bdf24b2e925ce8a4fb0e903889a52e0e",  // Assets/VketShaderPack/lilToon/Shader/ltspass_tess_transparent.shader
                "2683fad669f20ec49b8e9656954a33a8",  // Assets/VketShaderPack/lilToon/Shader/ltspass_transparent.shader
                "85d6126cae43b6847aff4b13f4adb8ec",  // Assets/VketShaderPack/lilToon/Shader/lts_cutout.shader
                "3b4aa19949601f046a20ca8bdaee929f",  // Assets/VketShaderPack/lilToon/Shader/lts_cutout_o.shader
                "3b3957e6c393b114bab6f835b4ed8f5d",  // Assets/VketShaderPack/lilToon/Shader/lts_cutout_oo.shader
                "00795bf598b44dc4e9bd363348e77085",  // Assets/VketShaderPack/lilToon/Shader/lts_fakeshadow.shader
                "55706696b2bdb5d4d8541b89e17085c8",  // Assets/VketShaderPack/lilToon/Shader/lts_fur.shader
                "33aad051c4a3a844a8f9330addb86a97",  // Assets/VketShaderPack/lilToon/Shader/lts_furonly.shader
                "7ec9f85eb7ee04943adfe19c2ba5901f",  // Assets/VketShaderPack/lilToon/Shader/lts_furonly_cutout.shader
                "f8d9dfac6dbfaaf4c9c3aaf4bd8c955f",  // Assets/VketShaderPack/lilToon/Shader/lts_furonly_two.shader
                "544c75f56e9af8048b29a6ace5f52091",  // Assets/VketShaderPack/lilToon/Shader/lts_fur_cutout.shader
                "54bc8b41278802d4a81b27fe402994e2",  // Assets/VketShaderPack/lilToon/Shader/lts_fur_two.shader
                "a8d94439709469942bc7dcc9156ba110",  // Assets/VketShaderPack/lilToon/Shader/lts_gem.shader
                "efa77a80ca0344749b4f19fdd5891cbe",  // Assets/VketShaderPack/lilToon/Shader/lts_o.shader
                "b269573b9937b8340b3e9e191a3ba5a8",  // Assets/VketShaderPack/lilToon/Shader/lts_onetrans.shader
                "7171688840c632447b22ec14e2bdef7e",  // Assets/VketShaderPack/lilToon/Shader/lts_onetrans_o.shader
                "fba17785d6b2c594ab6c0303c834da65",  // Assets/VketShaderPack/lilToon/Shader/lts_oo.shader
                "94274b8ef5d3af842b9427384cba3a8f",  // Assets/VketShaderPack/lilToon/Shader/lts_overlay.shader
                "33e950d038b8dfd4f824f3985c2abfb7",  // Assets/VketShaderPack/lilToon/Shader/lts_overlay_one.shader
                "dce3f3e248acc7b4daeda00daf616b4d",  // Assets/VketShaderPack/lilToon/Shader/lts_ref.shader
                "3fb94a39b2685ee4d9817dcaf6542d99",  // Assets/VketShaderPack/lilToon/Shader/lts_ref_blur.shader
                "3eef4aee6ba0de047b0d40409ea2891c",  // Assets/VketShaderPack/lilToon/Shader/lts_tess.shader
                "bbfffd5515b843c41a85067191cbf687",  // Assets/VketShaderPack/lilToon/Shader/lts_tess_cutout.shader
                "5ba517885727277409feada18effa4a6",  // Assets/VketShaderPack/lilToon/Shader/lts_tess_cutout_o.shader
                "c6d605ee23b18fc46903f38c67db701f",  // Assets/VketShaderPack/lilToon/Shader/lts_tess_o.shader
                "90f83c35b0769a748abba5d0880f36d5",  // Assets/VketShaderPack/lilToon/Shader/lts_tess_onetrans.shader
                "67ed0252d63362a4ab23707a720508b7",  // Assets/VketShaderPack/lilToon/Shader/lts_tess_onetrans_o.shader
                "afa1a194f5a2fd243bda3a17bca1b36e",  // Assets/VketShaderPack/lilToon/Shader/lts_tess_trans.shader
                "9b0c2630b12933248922527d4507cfa9",  // Assets/VketShaderPack/lilToon/Shader/lts_tess_trans_o.shader
                "7e398ea50f9b70045b1774e05b46a39f",  // Assets/VketShaderPack/lilToon/Shader/lts_tess_twotrans.shader
                "7e61dbad981ad4f43a03722155db1c6a",  // Assets/VketShaderPack/lilToon/Shader/lts_tess_twotrans_o.shader
                "165365ab7100a044ca85fc8c33548a62",  // Assets/VketShaderPack/lilToon/Shader/lts_trans.shader
                "3c79b10c7e0b2784aaa4c2f8dd17d55e",  // Assets/VketShaderPack/lilToon/Shader/lts_trans_o.shader
                "0c762f24b85918a49812fc5690619178",  // Assets/VketShaderPack/lilToon/Shader/lts_trans_oo.shader
                "6a77405f7dfdc1447af58854c7f43f39",  // Assets/VketShaderPack/lilToon/Shader/lts_twotrans.shader
                "9cf054060007d784394b8b0bb703e441",  // Assets/VketShaderPack/lilToon/Shader/lts_twotrans_o.shader
                "7d3136ba82c46534ea3f72a408f36322",  // Assets/VketShaderPack/lilToon/Shader/Includes/GTModelDecode.cginc
                "5520e766422958546bbe885a95d5a67e",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_common.hlsl
                "17c02a1ea49f2d542adfe023f45e7810",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_common_appdata.hlsl
                "5c00516dfc33a2341a88d924b2d261ff",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_common_frag.hlsl
                "184b658250bd4db4798f485de4e4b00b",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_common_frag_alpha.hlsl
                "c92aa095988162843aa4bb2835dcc842",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_common_functions.hlsl
                "b7e02b1c541e17e438d1bf1e67bd9282",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_common_functions_thirdparty.hlsl
                "e03a05e7a51ae944280b84199c373536",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_common_input.hlsl
                "8ff7f7d9c86e1154fb3aac5a8a8681bb",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_common_input_base.hlsl
                "571051a232e4af44a98389bda858df27",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_common_input_opt.hlsl
                "7621b8fcab43e25419d64ba197c6c041",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_common_macro.hlsl
                "134d8aa747e57894dbf11aea028c3ed6",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_common_vert.hlsl
                "977387656ea233a498cc7a2d907e429f",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_common_vert_fur.hlsl
                "e3dbe4ae202b9094eab458bbc934c964",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_common_vert_fur_thirdparty.hlsl
                "5f0659d685a03dc4c8bb13bb7ce1666c",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pass_depthnormals.hlsl
                "ddad648396307fe44a229338e4f0ea51",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pass_depthonly.hlsl
                "076cc9ffbcf86c34fb987ced16c938b8",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pass_forward.hlsl
                "50f1d3c5ad5f4534ea4aade1f5ef1833",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pass_forward_fakeshadow.hlsl
                "df484952219ee584393f59fa74d13996",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pass_forward_fur.hlsl
                "d99da3dcbd650a245a828a50cd8ed9af",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pass_forward_gem.hlsl
                "b5126a711fe38d1448b352f5052c8db4",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pass_forward_lite.hlsl
                "5f89df7ffc356d640a8b6062efc51574",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pass_forward_normal.hlsl
                "da2750caf3fa4744e84f953030e161d5",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pass_forward_refblur.hlsl
                "7a04cfa6852c13e4ebe940703cada42e",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pass_meta.hlsl
                "ab6e6b77aa02ecf47af7a84557fdab63",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pass_motionvectors.hlsl
                "b31f555a2a840974585f4d01711f729d",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pass_shadowcaster.hlsl
                "2de8785cf10861045b3e7b24db5cc97b",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pass_universal2d.hlsl
                "d9658f119fdeefe4dbecc2775e26ffac",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pipeline_brp.hlsl
                "572f275e19e0e8b4488be8e64424ee8f",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pipeline_hdrp.hlsl
                "9148fca3e71035044a9cfac5009018ca",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pipeline_lwrp.hlsl
                "92b4b1bf1a11c064087685cbb7ebe082",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_pipeline_urp.hlsl
                "f3b11bccdd87a1e448a88b4f49d65653",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_replace_keywords.hlsl
                "b02682b409c0260449b279dc9d35d9c7",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_tessellation.hlsl
                "c3c0d0056ab91ba4db7516465a6581fe",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_vert_audiolink.hlsl
                "957b6179a395605459b9e1ef1d0cdc0d",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_vert_encryption.hlsl
                "683a6eed396c8044bb0c482c77c997d4",  // Assets/VketShaderPack/lilToon/Shader/Includes/lil_vert_outline.hlsl
                "ace400995eeef5f40ad13c929ee03b13",  // Assets/VketShaderPack/lilToon/Shader/Includes/openlit_core.hlsl
                "f6af462667a53734aa2bdb88cd28773e",  // Assets/VketShaderPack/lilToon/Texture/lil_bayer_16x16.png
                "0fe8c123eabd3d144b64f3ad8e46ca9d",  // Assets/VketShaderPack/lilToon/Texture/lil_bayer_2x2.png
                "5bf614e5cd085334fb250d85ecf4add8",  // Assets/VketShaderPack/lilToon/Texture/lil_bayer_4x4.png
                "7beb7d447aa213a44bbb0146e0604f76",  // Assets/VketShaderPack/lilToon/Texture/lil_bayer_8x8.png
                "dddb8b9e8cea6b745b98704db1437c88",  // Assets/VketShaderPack/lilToon/Texture/lil_emission_rainbow.png
                "5e5f849189d66b1448d63883a5f9195a",  // Assets/VketShaderPack/lilToon/Texture/lil_noise_1d.png
                "3d421fc7bc8924f4985df741bf8d70ed",  // Assets/VketShaderPack/lilToon/Texture/lil_noise_fur.png
                "8a7dbcb2f4a32ec4297486403aee5f74",  // Assets/VketShaderPack/lilToon/Texture/lil_noise_fur_2.png
                "cbd6b66929773a2418b39e21703893b0",  // Assets/VketShaderPack/lilToon/Texture/lil_shape_hex.png
                "7d5796044ffe6f84a89374673b9dfd78",  // Assets/VketShaderPack/lilToon/Texture/lil_shape_snowflakes.png
                "66e9f2f309eba6543886cbfc714d5276",  // Assets/VketShaderPack/lilToon/Texture/lil_shape_star.png
                "8704ec8533a146d4daf20c45658e92ab",  // Assets/VketShaderPack/lilToon/Texture/lil_tangent.png
                "aa232ebfdb3318845b4de050c61dc2c6",  // Assets/VketShaderPack/lilToon/Texture/lil_tangent_circular.png
                "a06ed8ab718f1884fa52fe010de1c3c7",  // Assets/VketShaderPack/lilToon/Texture/matcap_metal_realistic.png
                "82e13d2a938694aedb5dbb01bd3ecf07",  // Assets/VketShaderPack/MMS3/LICENSE
                "8dd7c14dadb834c4e8324f7d08c5674e",  // Assets/VketShaderPack/MMS3/MMS3.shader
                "128f4720891e8914ab7e6673099df0f0",  // Assets/VketShaderPack/MMS3/MMS3_Cutout.shader
                "fbaec084851cef64fbd877b3b15716cb",  // Assets/VketShaderPack/MMS3/MMS3_Outline.shader
                "f889d00a055a0488e9ecbf22c558ae76",  // Assets/VketShaderPack/MMS3/MMS3_Stencil_Reader.shader
                "f55508f2ed8cc477f9574099971bc4eb",  // Assets/VketShaderPack/MMS3/MMS3_Stencil_Writer.shader
                "fda424b70f79d4e5488e1cc3ee100a95",  // Assets/VketShaderPack/MMS3/MMS3_Transparent.shader
                "ece969dbfb97d446ba8f8358a78789b5",  // Assets/VketShaderPack/MMS3/Shade_Matcap1.psd
                "909b3ce927e8cf246b13b1dbdef33f62",  // Assets/VketShaderPack/Mochie/Common/Color.cginc
                "d5468ef40ceedc549a0911e23c0b1568",  // Assets/VketShaderPack/Mochie/Common/Noise.cginc
                "66399fdb22339fe4daa7700e36ae9465",  // Assets/VketShaderPack/Mochie/Common/Sampling.cginc
                "71a928ffb0de3b442ab7e52a33f42d54",  // Assets/VketShaderPack/Mochie/Common/Utilities.cginc
                "1109e9bdc6f65cb44abfa6b4981829dc",  // Assets/VketShaderPack/Mochie/Glass Shader/Glass.shader
                "d08b3ba340457d545ab1eaeb889e7292",  // Assets/VketShaderPack/Mochie/Glass Shader/GlassDefines.cginc
                "af72ee46ae49fb14db30235753971f30",  // Assets/VketShaderPack/Mochie/Glass Shader/GlassFunctions.cginc
                "6cbfd685fc5d0704699d3299e6142ddf",  // Assets/VketShaderPack/Mochie/Glass Shader/GlassKernels.cginc
                "0df4e76791134454695a4aa95ed80814",  // Assets/VketShaderPack/Mochie/LED Shader/LED Screen (Transparent).shader
                "396751a2316820640b493caee7f0c916",  // Assets/VketShaderPack/Mochie/LED Shader/LED Screen.shader
                "85206c946eae72542b5e4c8b3131bae8",  // Assets/VketShaderPack/Mochie/LED Shader/RGBSubPixel.cginc
                "74e814afd5b766447b0de37c532cc8f9",  // Assets/VketShaderPack/Mochie/Particle Shader/Particles.shader
                "d1c93822d1541934c8fa436a39f0351a",  // Assets/VketShaderPack/Mochie/Particle Shader/PSDefines.cginc
                "56a4a30195ab7a4459b604ae189b3ccd",  // Assets/VketShaderPack/Mochie/Particle Shader/PSFunctions.cginc
                "c46a10d001a1d1b4fa634b7e561639ad",  // Assets/VketShaderPack/Mochie/Particle Shader/PSKeyDefines.cginc
                "0d1d977ca72938b4bb8f3ed06b9a8645",  // Assets/VketShaderPack/Mochie/ScreenFX Shader/SFX.shader
                "0622846791c27d3499465434f2f63a0f",  // Assets/VketShaderPack/Mochie/ScreenFX Shader/SFXBlur.cginc
                "e06fb4e15a03e164dae45a93c3ab3591",  // Assets/VketShaderPack/Mochie/ScreenFX Shader/SFXDefines.cginc
                "e51e722628c0c834f841cbca164dc53b",  // Assets/VketShaderPack/Mochie/ScreenFX Shader/SFXFunctions.cginc
                "7cbe4084658fd6b4e8b73782d48a461d",  // Assets/VketShaderPack/Mochie/ScreenFX Shader/SFXKernel.cginc
                "b78f95f931b33c846b19851684cd7cdc",  // Assets/VketShaderPack/Mochie/ScreenFX Shader/SFXKeyDefines.cginc
                "9a10756a86708fc4f840711e05cf723c",  // Assets/VketShaderPack/Mochie/ScreenFX Shader/SFXPass.cginc
                "4bd03e585f1830247a19f1af0893e73f",  // Assets/VketShaderPack/Mochie/ScreenFX Shader/SFXXFeatures.cginc
                "87a52d53f3012e448b23af4d55a79d02",  // Assets/VketShaderPack/Mochie/ScreenFX Shader/SFXXPasses.cginc
                "e1e688f8a6bee854cbf34c599989d152",  // Assets/VketShaderPack/Mochie/Standard Shader/MochieStandardBRDF.cginc
                "3bb158287983274479b31131c42d344b",  // Assets/VketShaderPack/Mochie/Standard Shader/MochieStandardCore.cginc
                "b65452b9b58de00458966db7c742a120",  // Assets/VketShaderPack/Mochie/Standard Shader/MochieStandardCoreForward.cginc
                "dff6ea45e3970e341ae884c826bc4365",  // Assets/VketShaderPack/Mochie/Standard Shader/MochieStandardGlobalIllumination.cginc
                "8fa3c40715f92f34796f3658af91c019",  // Assets/VketShaderPack/Mochie/Standard Shader/MochieStandardInput.cginc
                "4f56d10786cb04040bdd3483e3d62b5a",  // Assets/VketShaderPack/Mochie/Standard Shader/MochieStandardKeyDefines.cginc
                "09863e5f22b3f69408f7a5e290908b56",  // Assets/VketShaderPack/Mochie/Standard Shader/MochieStandardMeta.cginc
                "65625153f0e17f645bbe3a88be64e27d",  // Assets/VketShaderPack/Mochie/Standard Shader/MochieStandardParallax.cginc
                "3d117fcb52ac8ae48adc1dd82c506ab9",  // Assets/VketShaderPack/Mochie/Standard Shader/MochieStandardPBSLighting.cginc
                "033b9196130059a4aa5f7f2e8b8bf015",  // Assets/VketShaderPack/Mochie/Standard Shader/MochieStandardSampling.cginc
                "77a161d297d8ae6469a456c3173c84c3",  // Assets/VketShaderPack/Mochie/Standard Shader/MochieStandardShadow.cginc
                "bbbf9947de0eb8f4aa275f897aa4353b",  // Assets/VketShaderPack/Mochie/Standard Shader/MochieStandardSSR.cginc
                "d0c866a28c8254e4aa872ec4f078184a",  // Assets/VketShaderPack/Mochie/Standard Shader/MochieStandardSSS.cginc
                "cddaa3a02eb956746b502b80b76e92bc",  // Assets/VketShaderPack/Mochie/Standard Shader/Standard.shader
                "b252ff402bce931488cf8ff5152bf2dc",  // Assets/VketShaderPack/Mochie/Uber Shader/Uber (Outline).shader
                "5398f14cd241f2649988529db4480d1c",  // Assets/VketShaderPack/Mochie/Uber Shader/Uber.shader
                "48f81babcf4c5094084ffcaad36cac09",  // Assets/VketShaderPack/Mochie/Uber Shader/USAudioLink.cginc
                "21947c9bef25458429000c46ca32e021",  // Assets/VketShaderPack/Mochie/Uber Shader/USBRDF.cginc
                "6cd01882b763be542be24bd25c155871",  // Assets/VketShaderPack/Mochie/Uber Shader/USDefines.cginc
                "6e016b6a7bd29c24581e80488f391a0e",  // Assets/VketShaderPack/Mochie/Uber Shader/USFunctions.cginc
                "6390189603c02114c9822185832e97fc",  // Assets/VketShaderPack/Mochie/Uber Shader/USKeyDefines.cginc
                "a517223ef2cd6074b9947340447724b9",  // Assets/VketShaderPack/Mochie/Uber Shader/USLighting.cginc
                "b6948e44e1f92fc4891f424daf8e7bfd",  // Assets/VketShaderPack/Mochie/Uber Shader/USPass.cginc
                "4ec15cb7a78843d4fb5c7c8bdf19bd9b",  // Assets/VketShaderPack/Mochie/Uber Shader/USSSR.cginc
                "76eed4008ba5d464199dcfc895daf3b7",  // Assets/VketShaderPack/Mochie/Uber Shader/USXFeatures.cginc
                "1da8bba388ad86741b84e6899d501ca7",  // Assets/VketShaderPack/Mochie/Uber Shader/USXGeom.cginc
                "d9b054af17135c745adff39d435e039d",  // Assets/VketShaderPack/Mochie/Unity/Editor/Foldouts.cs
                "81a008b2ef8f51c4db481536290b78cd",  // Assets/VketShaderPack/Mochie/Unity/Editor/GlassEditor.cs
                "8f9efe1043f26e44ba3d8715a9d32f10",  // Assets/VketShaderPack/Mochie/Unity/Editor/LEDEditor.cs
                "2f59b3e0bf10120419b941583795ef54",  // Assets/VketShaderPack/Mochie/Unity/Editor/MGUI.cs
                "07e2014e25903e548b0e102aebc5851f",  // Assets/VketShaderPack/Mochie/Unity/Editor/MochieStandardGUI.cs
                "fdc00d0c66b6f3f4eb834fd87b6d760c",  // Assets/VketShaderPack/Mochie/Unity/Editor/PSEditor.cs
                "4689d28cb77840b488838b0a89f5dd78",  // Assets/VketShaderPack/Mochie/Unity/Editor/SFXEditor.cs
                "39a7482c0e084db4dbfc294a0bc1ba7f",  // Assets/VketShaderPack/Mochie/Unity/Editor/Tips.cs
                "566cd2268c7d9194087322ca64b68f61",  // Assets/VketShaderPack/Mochie/Unity/Editor/Toggles.cs
                "1ae5f757db301014b927532c1fe301af",  // Assets/VketShaderPack/Mochie/Unity/Editor/UnderwaterEditor.cs
                "eed6a60c5f8da544690d739b516ada01",  // Assets/VketShaderPack/Mochie/Unity/Editor/USEditor.cs
                "1a38ee131925db44ca2a7d1b33ff7aae",  // Assets/VketShaderPack/Mochie/Unity/Editor/WaterEditor.cs
                "9250c120998bbf045a862f7347bad0e1",  // Assets/VketShaderPack/Mochie/Unity/Editor/Tools/GlobalStandardSettings.cs
                "62960ebdac548b14c802d8e85536416b",  // Assets/VketShaderPack/Mochie/Unity/Editor/Tools/PoiData.cs
                "a82ccf23e3977ae4a89a285543e8ed81",  // Assets/VketShaderPack/Mochie/Unity/Editor/Tools/PoiHelpers.cs
                "5be87493be180c94992ab2dcb86c19e7",  // Assets/VketShaderPack/Mochie/Unity/Editor/Tools/TextureChannelPackerEditor.cs
                "3bb643d832d69134f8fbea4efcd0e109",  // Assets/VketShaderPack/Mochie/Unity/Prefabs/Depth Light.prefab
                "cd555b15b892a6342821da231de50d42",  // Assets/VketShaderPack/Mochie/Unity/Prefabs/Screen FX.prefab
                "76deb04414cfdf7448e08cddc6a46b5e",  // Assets/VketShaderPack/Mochie/Unity/Prefabs/Underwater.prefab
                "49a980c8ddef8ec4aadc4d397b5fcccf",  // Assets/VketShaderPack/Mochie/Unity/Prefabs/Water (Clean).prefab
                "85eef5111d5444e4dacd0f0c23a7b776",  // Assets/VketShaderPack/Mochie/Unity/Prefabs/Water (Lava).prefab
                "2631cd44621d2074f9c217a769a297f1",  // Assets/VketShaderPack/Mochie/Unity/Prefabs/Water.prefab
                "497f8485774204244abb7ba6c0865927",  // Assets/VketShaderPack/Mochie/Unity/Prefabs/Materials/Screen FX Mat.mat
                "8d3f0d345ef88514794c6c74d5e23212",  // Assets/VketShaderPack/Mochie/Unity/Prefabs/Materials/Underwater Mat.mat
                "ba42ddfa8ad23a84da2b5bfd20e32077",  // Assets/VketShaderPack/Mochie/Unity/Prefabs/Materials/Water Clean Mat.mat
                "1b7fbc177741b68479f619a0c54f8120",  // Assets/VketShaderPack/Mochie/Unity/Prefabs/Materials/Water Lava Mat.mat
                "5488c79609ff040468ac12e94672fc70",  // Assets/VketShaderPack/Mochie/Unity/Prefabs/Materials/Water Mat.mat
                "3e38383d19b750046a6fa03b1c2f8bac",  // Assets/VketShaderPack/Mochie/Unity/Resources/CollapseIcon.png
                "09c9c066a27ac424da976a9ae8474231",  // Assets/VketShaderPack/Mochie/Unity/Resources/CopyTo1Icon.png
                "124358866068baa4f90186cb87430c24",  // Assets/VketShaderPack/Mochie/Unity/Resources/CopyTo2Icon.png
                "d29b3eb8412f5e64096afc1ab733122d",  // Assets/VketShaderPack/Mochie/Unity/Resources/Header.png
                "29f18c82d04215e4f87185a100e9ff1b",  // Assets/VketShaderPack/Mochie/Unity/Resources/Header_Pro.png
                "a539fd42ec2ab904eb881b0cea9e7727",  // Assets/VketShaderPack/Mochie/Unity/Resources/LinkIcon.png
                "b7e02a3cda029714da12a9fdd0e3b531",  // Assets/VketShaderPack/Mochie/Unity/Resources/LinkIcon_Pro.png
                "3721cd13b85299e4e85d478b0d0b9e8d",  // Assets/VketShaderPack/Mochie/Unity/Resources/MochieLogo.png
                "b6dd740bedbfa974ea8ded908abf8ce1",  // Assets/VketShaderPack/Mochie/Unity/Resources/MochieLogo_Pro.png
                "8f1c2bbd99970c841b096d9447417468",  // Assets/VketShaderPack/Mochie/Unity/Resources/ParticleHeader.png
                "ca6d24562e19aab4e90be114647a98bb",  // Assets/VketShaderPack/Mochie/Unity/Resources/ParticleHeader_Pro.png
                "31fef82c771a5374b904c64a98fde2ac",  // Assets/VketShaderPack/Mochie/Unity/Resources/Patreon_Icon.png
                "75fdb0eaa2906b74783d9f5f17aedb24",  // Assets/VketShaderPack/Mochie/Unity/Resources/PoiTexturePacker.shader
                "050e937beba23b6448504402e7a76737",  // Assets/VketShaderPack/Mochie/Unity/Resources/PoiTextureUnpacker.shader
                "ec636ed50f955cc42a934e1bd42403d0",  // Assets/VketShaderPack/Mochie/Unity/Resources/ResetIcon.png
                "2201e3ff274d60b42ba46809810c7f0e",  // Assets/VketShaderPack/Mochie/Unity/Resources/SFXHeader.png
                "30a883d22a3859443a814b6bba897043",  // Assets/VketShaderPack/Mochie/Unity/Resources/SFXHeader_Pro.png
                "dff4b38eef00de14487e9ee7ee4359b0",  // Assets/VketShaderPack/Mochie/Unity/Resources/StandardIcon.png
                "4f85bf82a4603c147944a2729a150f75",  // Assets/VketShaderPack/Mochie/Unity/Resources/WaterHeader.png
                "7475ad184b37b0942832ce9622ffc5e4",  // Assets/VketShaderPack/Mochie/Unity/Resources/WaterHeader_Pro.png
                "352f401c145570f488487622cd5fdaa6",  // Assets/VketShaderPack/Mochie/Unity/Textures/Blend Noise.png
                "7589d70a1d40b7c47857a6722e4a0aae",  // Assets/VketShaderPack/Mochie/Unity/Textures/Blend.png
                "c0fa48a763b58a64aa5dbafeb6a4c84a",  // Assets/VketShaderPack/Mochie/Unity/Textures/Caustics Array.asset
                "260d71fa929534a49aa9a5efb274f6d3",  // Assets/VketShaderPack/Mochie/Unity/Textures/CausticsTex.png
                "89819f8cb0b9e5d418f6e90ca96ac9c3",  // Assets/VketShaderPack/Mochie/Unity/Textures/Distortion.tif
                "76ae1285472e6ce48a2f01ef7905b8fd",  // Assets/VketShaderPack/Mochie/Unity/Textures/Droplet Mask.tif
                "923a20e8c8f439648bdf7025c790db42",  // Assets/VketShaderPack/Mochie/Unity/Textures/Flow.png
                "6706fbb4d4df9f04eac5dd6ef24e6ecf",  // Assets/VketShaderPack/Mochie/Unity/Textures/Foam 1.jpg
                "6c6c8f6678ed78e4eb543b06dcbc5b8a",  // Assets/VketShaderPack/Mochie/Unity/Textures/Foam 2.jpg
                "df89a63673a32f4438fba4fb13f0f640",  // Assets/VketShaderPack/Mochie/Unity/Textures/Glass_Rain_Texturesheet.png
                "930ac9d4c358e5846af139e693a08bd2",  // Assets/VketShaderPack/Mochie/Unity/Textures/Hair Normal.png
                "2ebd62d4099a9c649b0ac3494c27de29",  // Assets/VketShaderPack/Mochie/Unity/Textures/Molten Lava.png
                "f0f3d4dc7c5a4f74c8417ea2f593fa7b",  // Assets/VketShaderPack/Mochie/Unity/Textures/NoiseTex.png
                "7ad1e725b3c53a34dae68f092ff297cf",  // Assets/VketShaderPack/Mochie/Unity/Textures/Normal 1.png
                "8c0fe4ffb1eaa3748a17395d8c9bd71a",  // Assets/VketShaderPack/Mochie/Unity/Textures/Normal 2.png
                "4229641ad60880a4a8e7c5614adf58a3",  // Assets/VketShaderPack/Mochie/Unity/Textures/Normal 3.png
                "a8c438561072d124da8228cedc01ac9d",  // Assets/VketShaderPack/Mochie/Unity/Textures/Normal Array.asset
                "2059b62900034054f9f93aafbf8293fb",  // Assets/VketShaderPack/Mochie/Unity/Textures/Perlin (Alpha).jpg
                "5874c4b0804010f46907ed3c81294735",  // Assets/VketShaderPack/Mochie/Unity/Textures/Perlin (Threshold).png
                "dfbb7eeed695dc14d82b08d887041406",  // Assets/VketShaderPack/Mochie/Unity/Textures/Perlin.jpg
                "b8d1261e60bcece48b7708cac8798bfc",  // Assets/VketShaderPack/Mochie/Unity/Textures/Shake Noise.png
                "b7359cc7e3e84444b88656ff6c166220",  // Assets/VketShaderPack/Mochie/Unity/Textures/SSR Noise.png
                "3d92577df3d9415419e771531a80f8c2",  // Assets/VketShaderPack/Mochie/Unity/Textures/Toon Water.png
                "9c8ede69ecd0f824aa80b9929c0b1e5c",  // Assets/VketShaderPack/Mochie/Unity/Textures/Transparent 4x4.png
                "f9e27b340e9c5194199078a54772ec10",  // Assets/VketShaderPack/Mochie/Unity/Textures/Vertex Offset Array.asset
                "f276a76437cf84847a5986084b4d11f3",  // Assets/VketShaderPack/Mochie/Unity/Textures/Ramps/DefaultRamp.png
                "9674bc46ecefab84b9f135c13b18ce36",  // Assets/VketShaderPack/Mochie/Unity/Textures/Ramps/RampImporter.cs
                "dc9e9b9306700534388c35a8de4d93c5",  // Assets/VketShaderPack/Mochie/Unity/Textures/Subpixel Patterns/LG OLED.tga
                "1ea3ebe97bde63a489b073b9cdd90671",  // Assets/VketShaderPack/Mochie/Unity/Textures/Subpixel Patterns/Modern LED.tga
                "a1f2b0323128e69439ef3f548afbe1c2",  // Assets/VketShaderPack/Mochie/Unity/Textures/Subpixel Patterns/Old CRT.tga
                "e030df9cacaecc54da87c188931870e2",  // Assets/VketShaderPack/Mochie/Unity/Textures/Subpixel Patterns/Pentile.tga
                "256672b5a74611a4f9a5f4bf99bba845",  // Assets/VketShaderPack/Mochie/Water Shader/Underwater Visuals.shader
                "7e018b47cc1d059439656129107b3445",  // Assets/VketShaderPack/Mochie/Water Shader/Water.shader
                "364b4ed97da1f4f489446f1181cf1a33",  // Assets/VketShaderPack/Mochie/Water Shader/WaterBlurKernels.cginc
                "03e5a32c48507e743879c7c069a4d50e",  // Assets/VketShaderPack/Mochie/Water Shader/WaterDefines.cginc
                "124202f442c30e247939ed195233070a",  // Assets/VketShaderPack/Mochie/Water Shader/WaterFrag.cginc
                "218d7297de6495e4da6e1694d6ef4535",  // Assets/VketShaderPack/Mochie/Water Shader/WaterFunctions.cginc
                "58fb69c72874c12439e5620263ae954f",  // Assets/VketShaderPack/Mochie/Water Shader/WaterSSR.cginc
                "6a57a7088878d9644b88263c513d2b17",  // Assets/VketShaderPack/Mochie/Water Shader/WaterTess.cginc
                "dcd6d4aea594437468c38cfd7c05ac7a",  // Assets/VketShaderPack/Mochie/Water Shader/WaterVert.cginc
                "840c70bb39e48cc43a17ac9279a026d9",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/SCSS Logo.png
                "163fd2e733679b948b23e92b2341f8f6",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/LightRamps/Default MultiGradient.asset
                "80684a860e75c9a4295d27ead38010c7",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/LightRamps/Info.txt
                "a7e8258f4d13af1419c0326602f31748",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/LightRamps/LightRamp Sharp.png
                "70853d21e5cf0a945ba9ef1baa2f37fa",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/LightRamps/LightRamp Skin.png
                "6af41be6e81954543bfe50e9b2131c4d",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/LightRamps/LightRamp Smooth (old).png
                "6584ffcc7e2c6a746afd371ec1d6ad5d",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/LightRamps/LightRamp Smooth.png
                "a8fbd87577f16ea43ac168bbf9ef88f3",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/LightRamps/LightRamp Soft (old).png
                "51b142bdc7b4f7a428477e77eb815bc7",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/LightRamps/LightRamp Soft.png
                "d0d2092a7d8176a419858a5536e205ee",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/LightRamps/LightRamp Toon v2.png
                "7f445efa362f16248af955f190843381",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/LightRamps/LightRamp Toon v3.png
                "ae6fa37de6d2b4e45a6176091e47455c",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/Mnmr/LICENSE.txt
                "63a9cd46c7dd97644b42c0721976f257",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/Mnmr/Readme.txt
                "fb2f01db930474c3fbd62634f03ffe4b",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/Mnmr/Matcap/MMS_Light_Hard.psd
                "4348a2a80916845739da8629005aef03",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/Mnmr/Matcap/MMS_Light_Hard_Hair2.psd
                "5306755cc52e04770bf7169839c6b350",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/Mnmr/Matcap/MMS_Light_Soft.psd
                "1f802a1910910432ca435480b93e70ec",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/Mnmr/Matcap/MMS_Light_Soft_Hair.psd
                "d247459fa9b47465d92f1eb93eba56e9",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/Mnmr/Matcap/MMS_RimLightMatcap1.psd
                "c2dda37b49c0b4bde9e211e894f7344d",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/Mnmr/Matcap/MMS_RimLightMatcap2.psd
                "44d209fdf321840569dd21a5b61e277d",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/Mnmr/Matcap/MMS_ShadowMatcap1.psd
                "ecfa3da8397834305821fe311f1cbf15",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/Mnmr/Matcap/MMS_ShadowMatcap2.psd
                "c68fab11bf4dfb044a2f51d7ddc4d064",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/mofuaki_/mofuaki_-1185084491351515136-img1.png
                "48e16bad9d9551b499dfb08af7ee7e31",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/mofuaki_/mofuaki_-1185084491351515136-img2.png
                "fb8f4c7cfbfa3f743b381bda894c2eb6",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/mofuaki_/mofuaki_-1185084491351515136.txt
                "21e7d43547251ef4bb267a4aa24f04b7",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/mofuaki_/mofuaki_-1185084491351515136.url
                "ebd77ce0e53676d49853f56eb043a827",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LICENSE
                "c855d2d0c1361d14a841af5aa24d6d26",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/README.txt
                "b7b167660549b8e48a231f62d2fcb008",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Glare.png
                "3dd155f12c76e1447bf62608fc1bf572",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Gloss(LargeFlat).png
                "3dfb01f9d1c32f048ac838f6e3fa8810",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Gloss(LargePoint).png
                "9a6b29adf08462e4f98ec4a45cc7c57c",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Gloss(MultiFlat).png
                "bd882f7694bebd04eb144979a8787993",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Gloss(MultiPoint).png
                "c6cbb49dcbadf93489feca05b6652723",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Gloss(SmallFlat).png
                "b3d17e4df2a84274f8810d14ad0d1537",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Gloss(SmallPoint).png
                "5866d4cb591aa9443b2cb4948139b112",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Hair_AngelRing(Blue).png
                "a369076245553a64eb0f96d537272196",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Hair_AngelRing(Green).png
                "a8845002a4d852249b1f5b2d0422279c",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Hair_AngelRing(Purple).png
                "0db92e9598eb9f64c82095d66efe6a1a",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Hair_AngelRing(Red).png
                "2b71b8337c476dc4b875b2836a0e2720",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Hair_AngelRing(Yellow).png
                "86e9818d2a3ca7041b09c97748be153d",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Hair_AngelRing.png
                "70ae0ed388398f14a983b9be841fbf92",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Iridescent(Large).png
                "431e901af18398a40876e71e33afcb9c",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Iridescent(Multi).png
                "d3a73c13da3082b499720fd2e340fd58",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Iridescent(Small).png
                "c3348e9a93cf3f041a47eab885eff5a4",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Skin_Dark(Peach).png
                "81aad2673b3c3564e86273be96df3985",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Skin_Dark(Red).png
                "51458ebc5861843449e59e63cfbe9312",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Skin_Dark(White).png
                "8d80aa31a13e4ca499d7a120c31b5c30",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Skin_Dark(Yellow).png
                "1e1bd6119670c6644b39d585be1c7dd4",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Skin_Pale(Peach).png
                "de1140e088e6df840abef03788989036",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Skin_Pale(Red).png
                "b249fa84e5347e047a14ebd946f5b992",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Skin_Pale(White).png
                "eeb275611fdaf2648b0749546abab7e7",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Skin_Pale(Yellow).png
                "118b92d83e2a11040921f1eb0de754f2",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Tights_HighDensity.png
                "d7043e6bdc77d9a48bdbfc3fb6dc63b8",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/LightCap/lcap_WF_Tights_LowDensity.png
                "21b34471bb21d714695594ed013671e5",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Gloss(BlackEnamel・Blue).png
                "4b7f9eac7f86d804fa4651f78c2f77a2",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Gloss(BlackEnamel・Red).png
                "a86b7309210462d49937588b29ecb453",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Gloss(BlackEnamel・White).png
                "e789df0d8e991dc46bbbcb776c5a3ada",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Gloss(Rim).png
                "cfc0ca869f3f6aa488e442597f757c16",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Gloss(RimBright).png
                "eba422e248e1b7b46a44eca939e97a7c",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Gloss(WhiteEnamel・Rainbow).png
                "d9eed75190795c742b2a4ad01169a572",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_GradientShadow(Blue).png
                "981358a9d84b3254796caa2aed07f755",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_GradientShadow(Red).png
                "cb816f85c59d0594e919df78215b8643",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_GradientShadow(White).png
                "141320f81a2860f45b14a1de5bb46964",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_GradientShadow.png
                "228a97a7223a715439f7b999a17cb9d5",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Hair_AngelRing(Blue).png
                "de0bb15fb581f874b8c33b1cc5ea50e4",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Hair_AngelRing(Green).png
                "4e89ea8f4509c2d479adc1b6eb4a59b8",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Hair_AngelRing(Purple).png
                "bf6207586b81cc14fa15f173c8e3e88d",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Hair_AngelRing(Red).png
                "731335fba7efbcb4c9c77281e6ebf259",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Hair_AngelRing(Yellow).png
                "7237239c7d9b876468e784c193ea453f",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Hair_AngelRing.png
                "140f68527af730a42884e180331e2385",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_MetalGloss(Yellow).png
                "fc5c401b877c1bf4f90c09420e4564cb",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_SimpleShadow(Hemi).png
                "fb8412e5c0ec1864d94ffbc1c6573070",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_SimpleShadow.png
                "63de7b1aca5f56547b4566d5c452971d",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_SimpleShadow・BlueReflection.png
                "cd71180b27fb20b46bd8a67b792511d4",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_SimpleShadow・SideRim.png
                "cde5a8eff47aab14f956da09db682183",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Skin_BrownSlick_GlosslessFace.png
                "6019069d567c03e40b2e57285c9cf05b",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Skin_BrownSlick_GlossyBody.png
                "e2f3f51b74c61cb4fa33d42a0d857e74",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Skin_Brown_GlossStrong.png
                "b3a2e207de951c44db9c4bacaee6010f",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shaded_Skin_Brown_Rim_GlossWeak.png
                "e2e3379d7afc6c548a77840f6fd5f488",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Glare+Gloss(PointSmall).png
                "874b5960d1ad3b442b9f03199627562f",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Glare.png
                "862bb4f308d31a449b8fbfa82451d751",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Gloss(LargeFlat).png
                "fc0cf484220685d4eafa9ed00ef8cc7e",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Gloss(LargePoint).png
                "cd6d2060efad2c846afb263d634f1f70",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Gloss(MultiFlat).png
                "2e2858828b151be4695fd36ca4d19a40",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Gloss(MultiPoint).png
                "5923f328fc4cb4147ab169d1db94b29c",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Gloss(SmallFlat).png
                "a3680a23ee096f44b9d976c690a872dd",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Gloss(SmallPoint).png
                "5ee24b69dfded3240b1e950e8cfea8bb",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Hair_AngelRing(Blue).png
                "aab7586db9bd139439746510e249d68b",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Hair_AngelRing(Green).png
                "551ec63cc2428814e811c7b90822a9ea",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Hair_AngelRing(Purple).png
                "4e03c1e10a8731844ac2136ea96dff4f",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Hair_AngelRing(Red).png
                "4456fab6540937b4f80185dfcdb32acd",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Hair_AngelRing(Yellow).png
                "9c2691948a9baf44e8231ff1ddd00b36",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Hair_AngelRing.png
                "1c368911528c60d429257dc57624517f",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Iridescent(Large).png
                "3d294a5afc79400468719f6af35dabdf",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Iridescent(Multi).png
                "efd84db93a4cca24d93687e84f48fdf8",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Iridescent(Small).png
                "ae7d1c4c107c5794c8c4ec92b3e31630",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Skin_Glare+Gloss(LargePoint).png
                "96cf3bb60ffe3384fbd847ec4857c9dc",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Shadeless_Skin_Glare.png
                "a8ca5b675c614d348ac51cc580b8e5e2",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Skin_Dark(Peach).png
                "8ace27bb9234eff44b5ec8f841b8ddaf",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Skin_Dark(Red).png
                "865d0a3b58a86df45ac20e870fcf6255",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Skin_Dark(White).png
                "bf45007d8a3c0aa4098a23916c68e58d",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Skin_Dark(Yellow).png
                "cf7bfae60977d664b911c3e163bb92a8",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Skin_Pale(Peach).png
                "01a3d35c1bc39f646a22769f40bbbb20",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Skin_Pale(Red).png
                "4fed231081a962e428e196625a7a4475",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Skin_Pale(White).png
                "478dd3812b01a39469a08b45e94ea6fa",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Skin_Pale(Yellow).png
                "4ff7604ea3c143144a465740b84499dc",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Tights_HighDensity.png
                "3d29d801cba569448b6aacb582271a69",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/WhiteFlare/MedianCap/mcap_WF_Tights_LowDensity.png
                "020b20d50dee3b64784514e35d8a53b8",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/HairMat.png
                "a2b389612cf565643b4b0bbd236f3335",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/Ramp1.png
                "8339ae69dbe9dcc439ff088723737cfb",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/Ramp2.png
                "0280c480c48fcbe40ac3bd5b8888b2d7",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/ReadMe.txt
                "f99f096fbf6cf4d40a2f990e8076c1db",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/ShadowMat.png
                "401e17d367f33e849a6fdb867b4ce269",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/ShadowMat_BGR.png
                "10af221457820f04c96c4b2932e290ab",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/SkinMat.png
                "4e1e2c12906898449933f67285cdc085",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/SpecMat.png
                "b0a5e22ce82df9b42a386995879e80d0",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/SpecMat2.png
                "c61dab59da88c87499105c1b046bcf73",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/Sample/Albedo.png
                "5f8a50cc30df039489b8ee08453375d2",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/Sample/Metal.png
                "06d8cbe587a3d2c4c967de17cac6c502",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/Sample/Normal.png
                "721cd36de640a974ca45b613e85cd800",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/Sample/Sample 1.mat
                "4b8608d176dcf934585ec1b6886e05e4",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/Sample/Sample 2.mat
                "d469e28ac045d044fb9cb2226a7c9c72",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/Sample/Sample.mat
                "af8197deebc61ce459480bd679aa6abc",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/Sample/Sample_SCSS.mat
                "a3185396b596ad949854a764984b9171",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Editor/SCSS_Inspector.cs
                "88a031d938963714a8250672469d5214",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Editor/SCSS_InspectorBase.cs
                "4836f67ee2df5b449a778c67c1ead5d2",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Editor/SCSS_InspectorData.English.txt
                "c46ae961491726741b3d5a5d3dcdcc47",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Editor/SCSS_InspectorData.Japanese.txt
                "07e7aaa47f6b9a04caf3a47e59ff4589",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Editor/SCSS_InspectorTools.cs
                "97aa53bd6e8cb564589015cbdc14fa40",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Editor/SCSS_ShaderBaking.cs
                "dd58167f3f5799f4db066008579b778a",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Editor/SCSS_XSGradientEditor.cs
                "0757509132a7ee748b11bc26b6fd10dd",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Editor/SCSS_XSMultiGradient.cs
                "218766231df1c4d4bbd26fe825a2dc8c",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Editor/SilentCelShading.Unity.asmdef
                "a9a812ee108476f4eae9c507264cc297",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Shaders/Crosstone (No Outline).shader
                "932c3f8bb2ba7d04480beb8e4c98b2a8",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Shaders/Crosstone.shader
                "5ef0bfd488b046146adfc1d1744c3cbc",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Shaders/dfg-multiscatter-cloth.exr
                "369d2ecd6fc95bc469360ddecf6b2155",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Shaders/Flat Lit Toon (No Outline).shader
                "a883b384ca4bc054aa10b5f554ae85a3",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Shaders/Flat Lit Toon.shader
                "831a9d552fe111645a13e3b7597596fa",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Shaders/SCSS_Attributes.cginc
                "f929e51157333ed49ab7a8029278babb",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Shaders/SCSS_AudioLink.cginc
                "4006a4b454064fb48bbb26c7a7ce2aaa",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Shaders/SCSS_Config.cginc
                "949047d11aa1be843ab010f80e6e1ad7",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Shaders/SCSS_Core.cginc
                "ac54125faed4a1c4d8641c311f115c9d",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Shaders/SCSS_Forward.cginc
                "48ee46d436066fb45a8719bd24d7250c",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Shaders/SCSS_ForwardVertex.cginc
                "ad30dacf242f54a49b203e540fe72e8a",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Shaders/SCSS_Input.cginc
                "8acce3fdffc81da43bbff56f95bd5e98",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Shaders/SCSS_Shadows.cginc
                "e4f4f1f16f5f7a940a8a91cda2684a75",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Shaders/SCSS_UnityGI.cginc
                "4918d8dc352c4f14095b785dedffaab1",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Shaders/SCSS_Utils.cginc
                "a780591bd355dfb42b8d43171c524127",  // Assets/VketShaderPack/Sunao Shader/LICENSE
                "3e696a6fcbf6c3d48b6b18d391ac27f3",  // Assets/VketShaderPack/Sunao Shader/README.txt
                "52db967a50319a342b8d3e03e2c948c5",  // Assets/VketShaderPack/Sunao Shader/Sunao Shader 解説書.url
                "cc3d17770479d8443bb7437a9ab8b43f",  // Assets/VketShaderPack/Sunao Shader/Editor/SunaoShader.asmdef
                "ac4920ac84fea1840bcc25ab63dd1154",  // Assets/VketShaderPack/Sunao Shader/Editor/SunaoShaderGUI.cs
                "01846cdaa65259e48a71d9812e4e1c22",  // Assets/VketShaderPack/Sunao Shader/Shader/Sunao_Shader_Cutout.shader
                "09296c4f29b71fb4ba42ef8983d8007f",  // Assets/VketShaderPack/Sunao Shader/Shader/Sunao_Shader_Cutout_SO.shader
                "f75c0a6890497514ab95a64c20497f07",  // Assets/VketShaderPack/Sunao Shader/Shader/Sunao_Shader_Fur.shader
                "3701d6a6f5f988b4a9cea92f1426a955",  // Assets/VketShaderPack/Sunao Shader/Shader/Sunao_Shader_Opaque.shader
                "2fb75b0069e4fe147a396141dcf70627",  // Assets/VketShaderPack/Sunao Shader/Shader/Sunao_Shader_Opaque_SO.shader
                "7362334fb65c850469785caac3918093",  // Assets/VketShaderPack/Sunao Shader/Shader/Sunao_Shader_Stencil_R.shader
                "a95ac57a344b931459880f4ca527efc4",  // Assets/VketShaderPack/Sunao Shader/Shader/Sunao_Shader_Stencil_W.shader
                "0b073aeeaec66294aa00c57784f4a0bb",  // Assets/VketShaderPack/Sunao Shader/Shader/Sunao_Shader_Transparent.shader
                "cd2723fb285798b4b801e483a793b3c3",  // Assets/VketShaderPack/Sunao Shader/Shader/Sunao_Shader_Transparent_SO.shader
                "7b79b52b7a89bf543a911ab175f08c9b",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/SunaoShader_Core.cginc
                "aded917ccb12d9847ac59496e8fa57af",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/SunaoShader_FR.cginc
                "a0a8cef7d729dd548bea8c0179114e1a",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/SunaoShader_Frag.cginc
                "7c91ecb7ec33e624aa825469df256c8d",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/SunaoShader_Function.cginc
                "d18cde8fa1759a645a51c13007da9b45",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/SunaoShader_Geom.cginc
                "349b3c656072d0444812de08c663ff40",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/SunaoShader_OL.cginc
                "331fdc83d13aff84cb82da583877f0d7",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/SunaoShader_SC.cginc
                "666562b3b8d23d64fa0f6ee5216239b1",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/SunaoShader_Vert.cginc
                "7c20d1e19286adc4da754ef3357d9140",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/Module/SS_Cutout.cginc
                "bf70302f5acb92f4d8fd3fc91c0e5abf",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/Module/SS_Decal.cginc
                "0f0248fe0a8726845ac8b409ebe743d3",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/Module/SS_Emission.cginc
                "fa6c5f2f9f305c74c814967fb844ed9a",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/Module/SS_FinalColor.cginc
                "bdd9ae549e6d6024fb589b23e54e4f89",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/Module/SS_Fur.cginc
                "48f0de100f9a07c4d91d7129400be58d",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/Module/SS_Lighting.cginc
                "ae9ba0359f672914fa8d51069491115b",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/Module/SS_Main.cginc
                "6faefce83e0d7e44888802f9c7c9d15e",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/Module/SS_Normal.cginc
                "34f643fa1c1ec9d45af6f154b81e3f5f",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/Module/SS_Output.cginc
                "a175e30be26c92e4bb96b1c38878177a",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/Module/SS_Reflection.cginc
                "297205d3e040e824d8650158af3646c6",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/Module/SS_RimLight.cginc
                "6cb3e9c6cc57cff4f9511a47c9036116",  // Assets/VketShaderPack/Sunao Shader/Shader/Cginc/Module/SS_Shading.cginc
                "e30857b716beae5479b313fde1a5efaf",  // Assets/VketShaderPack/Toon/Editor/CopyMaterialParameter.cs
                "cad15f56be91b744aaf8e22339bc598c",  // Assets/VketShaderPack/Toon/Editor/RemoveUnusedMaterialProperties.cs
                "a9775daf5f793f64e98ccd6c4a61bbc8",  // Assets/VketShaderPack/Toon/Editor/RemoveUnusedShaderKeywordsFromUTS2Material.cs
                "e403ef4b1d56fce47b49ec46981d9fcb",  // Assets/VketShaderPack/Toon/Editor/UTS2GUI.cs
                "4c57a42f315f467488f69755e6a7d42c",  // Assets/VketShaderPack/Toon/Shader/README.txt
                "96d4d9f975e6c8849bd1a5c06acfae84",  // Assets/VketShaderPack/Toon/Shader/ToonColor_DoubleShadeWithFeather.shader
                "ccd13b7f8710b264ea8bd3bc4f51f9e4",  // Assets/VketShaderPack/Toon/Shader/ToonColor_DoubleShadeWithFeather_Clipping.shader
                "9c3978743d5db18448a8b945c723a6eb",  // Assets/VketShaderPack/Toon/Shader/ToonColor_DoubleShadeWithFeather_Clipping_StencilMask.shader
                "d7da29588857e774bb0650f1fae494c6",  // Assets/VketShaderPack/Toon/Shader/ToonColor_DoubleShadeWithFeather_Clipping_StencilOut.shader
                "315897103223dab42a0746aa65ec251a",  // Assets/VketShaderPack/Toon/Shader/ToonColor_DoubleShadeWithFeather_StencilMask.shader
                "2e5cc2da6af713844956264245e092e4",  // Assets/VketShaderPack/Toon/Shader/ToonColor_DoubleShadeWithFeather_StencilOut.shader
                "369d674ae1ba36249bb00e2f73b0cd10",  // Assets/VketShaderPack/Toon/Shader/ToonColor_DoubleShadeWithFeather_TransClipping.shader
                "8600b2bec3ae31145afa80084df20c61",  // Assets/VketShaderPack/Toon/Shader/ToonColor_DoubleShadeWithFeather_TransClipping_StencilMask.shader
                "43d0eeb4c46f52841b0941e99ac9b16b",  // Assets/VketShaderPack/Toon/Shader/ToonColor_DoubleShadeWithFeather_TransClipping_StencilOut.shader
                "97b7edb5fc0f5744c9b264c2224a0b1e",  // Assets/VketShaderPack/Toon/Shader/ToonColor_DoubleShadeWithFeather_Transparent.shader
                "3b20fc0febd34f94baf0304bf47841d8",  // Assets/VketShaderPack/Toon/Shader/ToonColor_DoubleShadeWithFeather_Transparent_StencilOut.shader
                "af8454e09b3a41448a4140e792059446",  // Assets/VketShaderPack/Toon/Shader/ToonColor_ShadingGradeMap.shader
                "295fec4a7029edd4eb9522bef07f41ce",  // Assets/VketShaderPack/Toon/Shader/ToonColor_ShadingGradeMap_AngelRing.shader
                "e32270aa38f4b664b90f04cc475fdb81",  // Assets/VketShaderPack/Toon/Shader/ToonColor_ShadingGradeMap_AngelRing_StencilOut.shader
                "29a860a3f3c4cec43ab821338e28eac8",  // Assets/VketShaderPack/Toon/Shader/ToonColor_ShadingGradeMap_AngelRing_TransClipping.shader
                "d5d9c1f4718235248ad37448b0c74c68",  // Assets/VketShaderPack/Toon/Shader/ToonColor_ShadingGradeMap_AngelRing_TransClipping_StencilOut.shader
                "6439813c08a1f8947bb0ca6599499dd7",  // Assets/VketShaderPack/Toon/Shader/ToonColor_ShadingGradeMap_StencilMask.shader
                "b39692f1382224b4cbe21c12ae51c639",  // Assets/VketShaderPack/Toon/Shader/ToonColor_ShadingGradeMap_StencilOut.shader
                "cd7e85b59edbb7740841003baeb510b5",  // Assets/VketShaderPack/Toon/Shader/ToonColor_ShadingGradeMap_TransClipping.shader
                "6b4b6d07944415f44b1fc2f0fc24535f",  // Assets/VketShaderPack/Toon/Shader/ToonColor_ShadingGradeMap_TransClipping_StencilMask.shader
                "31c75b34739dfc64fb57bf49005e942d",  // Assets/VketShaderPack/Toon/Shader/ToonColor_ShadingGradeMap_TransClipping_StencilOut.shader
                "7737ca8c4e3939f4086a6e08f93c2ebd",  // Assets/VketShaderPack/Toon/Shader/ToonColor_ShadingGradeMap_Transparent.shader
                "be27d4be45de7dd4ab2e69c992876edb",  // Assets/VketShaderPack/Toon/Shader/ToonColor_ShadingGradeMap_Transparent_StencilOut.shader
                "9baf30ce95c751649b14d96da3a4b4d5",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather.shader
                "345def18d0906d544b7d12b050937392",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather_Clipping.shader
                "7a735f9b121d96349b6da0a077299424",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather_Clipping_StencilMask.shader
                "ed7fba947f3bccb4cbc78f55d7a56a70",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather_Clipping_StencilOut.shader
                "1d10c7840eb6ba74c889a27f14ba6081",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather_Mobile.shader
                "88791c14394118d42a5e176b433af322",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather_Mobile_Clipping.shader
                "41f4ee183cb66ad40bc74a9f8f944974",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather_Mobile_Clipping_StencilMask.shader
                "dec01cbdbc5b8da4ca8671815cda1557",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather_Mobile_StencilMask.shader
                "55e8b9eeaaff205469365133fe7bc744",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather_Mobile_StencilOut.shader
                "d4c592285a93c3844aafdaafffc07ec7",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather_Mobile_TransClipping.shader
                "100d373b596f44d49ac9bb944d671d32",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather_Mobile_TransClipping_StencilMask.shader
                "036bc90bfe3475b4c9fadb85d0520621",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather_StencilMask.shader
                "0a1e4c9dcc0e9ea4db38ae9cb5059608",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather_StencilOut.shader
                "e8e7d781c3155254b9ea8956c5bd1218",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather_TransClipping.shader
                "79add09e32e5c4541980118f6c4045b6",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather_TransClipping_StencilMask.shader
                "fb47be5a840097b45bac228446468ef3",  // Assets/VketShaderPack/Toon/Shader/Toon_DoubleShadeWithFeather_TransClipping_StencilOut.shader
                "42a47eda2ed77084c9136507eadb8641",  // Assets/VketShaderPack/Toon/Shader/Toon_OutlineObject.shader
                "2e2edd12fbf6bcb4ea1f34c17ee42df5",  // Assets/VketShaderPack/Toon/Shader/Toon_OutlineObject_StencilOut.shader
                "ca035891872022e4f80c952b3916e450",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap.shader
                "9aadc53d7cdc63f4898ea042aa9d853b",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_AngelRing.shader
                "23e399973d807464fb195291a44a614c",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_AngelRing_Mobile.shader
                "8d33e4e4084e5af449f3e762fecce3c9",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_AngelRing_Mobile_StencilOut.shader
                "415f07ab6fd766048ac6f8c2f2b406a9",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_AngelRing_StencilOut.shader
                "b2a70923168ea0c40a3051a013c93a8a",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_AngelRing_TransClipping.shader
                "d1e11a558d143f14c864edf263332764",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_AngelRing_TransClipping_StencilOut.shader
                "f90e11a40dcf4f745ae6b21b857943fa",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_Mobile.shader
                "206c554c8b0c60041a9d242385f543d3",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_Mobile_StencilMask.shader
                "cfc201757f2519c4bb6ef9265a046582",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_Mobile_StencilOut.shader
                "cce1da34c52aff745adf0222f56a356c",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_Mobile_TransClipping.shader
                "e88039bab21b7894e918126e8fce5d1b",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_Mobile_TransClipping_StencilMask.shader
                "aa2e05ed58ca15441bd0989f008da78b",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_StencilMask.shader
                "923058fda1b61544b93d91eeee772086",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_StencilOut.shader
                "aebd33b74ef849a4882b4a8d55f0f0c9",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_TransClipping.shader
                "0a05dd221bacbb448afac3d63e6bd833",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_TransClipping_StencilMask.shader
                "67212ac11ff43b04a833d3986b997a9f",  // Assets/VketShaderPack/Toon/Shader/Toon_ShadingGradeMap_TransClipping_StencilOut.shader
                "80bd7ce6cad775a4e9de24e18eb5e61e",  // Assets/VketShaderPack/Toon/Shader/UCTS_DoubleShadeWithFeather.cginc
                "ec7b5c1d006f6be49b412bcd7a789c78",  // Assets/VketShaderPack/Toon/Shader/UCTS_Outline.cginc
                "eca315d4d2d36194b8be3cf2a6869762",  // Assets/VketShaderPack/Toon/Shader/UCTS_ShadingGradeMap.cginc
                "ae8d06deb98501947846000ba6cd3ab2",  // Assets/VketShaderPack/Toon/Shader/UCTS_ShadowCaster.cginc
                "5b8a1502578ed764c9880a7be65c9672",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_DoubleShadeWithFeather_Clipping_Tess.shader
                "682e6e6cf60a51040ade19437a3f53e2",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_DoubleShadeWithFeather_Clipping_Tess_StencilMask.shader
                "148d1eca2cf299e4eb949d15c4cf95ee",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_DoubleShadeWithFeather_Clipping_Tess_StencilOut.shader
                "e987cf9cca0941042aa68d1dd51ee20f",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_DoubleShadeWithFeather_Tess.shader
                "97df86a7afe06ef41b2a2c242b10593e",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_DoubleShadeWithFeather_Tess_StencilMask.shader
                "b179fb8a87955a347b5f594a18b43475",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_DoubleShadeWithFeather_Tess_StencilOut.shader
                "60fe384b76fb67d40bc7e38411073dd6",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_DoubleShadeWithFeather_TransClipping_Tess.shader
                "4a20b66d106d3f5409f759b5193ecdc2",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_DoubleShadeWithFeather_TransClipping_Tess_StencilMask.shader
                "a7842aa9522c7584cae2169b8e1ddb86",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_DoubleShadeWithFeather_TransClipping_Tess_StencilOut.shader
                "0cb6c9e6216a91e4a9d38cd2acb4ccb6",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_DoubleShadeWithFeather_Transparent_Tess.shader
                "f28bba8b2f259bb40b697d91849c8794",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_DoubleShadeWithFeather_Transparent_Tess_StencilOut.shader
                "4876871966ca2344793e439d7391d7b0",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_ShadingGradeMap_AngelRing_Tess.shader
                "7c48bdc9fed28c14b8ad0748673b1369",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_ShadingGradeMap_AngelRing_Tess_StencilOut.shader
                "d3fb22770ec830b43bdb5ccb973e6f76",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_ShadingGradeMap_AngelRing_Tess_TransClipping.shader
                "11e8f1e181e558a47a387492d3ecdb88",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_ShadingGradeMap_AngelRing_TransClipping_Tess_StencilOut.shader
                "01494e58d87212f44ab51d29caea84e4",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_ShadingGradeMap_Tess.shader
                "24c20b8ed5be113499b40f4e3b6b03e6",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_ShadingGradeMap_Tess_StencilMask.shader
                "9cf7e8eb46e9128438d50adf7a841de6",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_ShadingGradeMap_Tess_StencilOut.shader
                "3c39a77fda28b5043a7a17c7877cf7b2",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_ShadingGradeMap_TransClipping_Tess.shader
                "bf840a439c33c8b4a99d52e6c3d8511f",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_ShadingGradeMap_TransClipping_Tess_StencilMask.shader
                "8eff803eae89c994fae3acf2f686fafa",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_ShadingGradeMap_TransClipping_Tess_StencilOut.shader
                "0959cb8822a344c4da890457e668fdc9",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_ShadingGradeMap_Transparent_Tess.shader
                "6d115cf94d14d1842a56dfff76b57f42",  // Assets/VketShaderPack/Toon/Shader/Tess/ToonColor_ShadingGradeMap_Transparent_Tess_StencilOut.shader
                "f0b2fc9b8a189134da9c7d24f361caf4",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_DoubleShadeWithFeather_Clipping_Tess.shader
                "8c94ee3046ef0574f87f6b658b4e4691",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_DoubleShadeWithFeather_Clipping_Tess_StencilMask.shader
                "c4aed8662ca0f194284f3ab649e66d23",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_DoubleShadeWithFeather_Clipping_Tess_StencilOut.shader
                "1f248db3b28fc5f44aabd7aca618bd1e",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_DoubleShadeWithFeather_Tess.shader
                "a3214384442742648aa664ef0039d397",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_DoubleShadeWithFeather_Tess_Light.shader
                "3073cd2564e4cde45a19c05e0012d22a",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_DoubleShadeWithFeather_Tess_Light_StencilMask.shader
                "7e7690a767a07da4f943439680e70db8",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_DoubleShadeWithFeather_Tess_Light_StencilOut.shader
                "08c65988dc25d9f44b791fcc18fb543a",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_DoubleShadeWithFeather_Tess_StencilMask.shader
                "f937ea4ce96dfbe448afc0fb671198e5",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_DoubleShadeWithFeather_Tess_StencilOut.shader
                "3fb99ac3775edeb4aa9530db5a614c92",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_DoubleShadeWithFeather_TransClipping_Tess.shader
                "9855f226cd8152d4e99085272aceede6",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_DoubleShadeWithFeather_TransClipping_Tess_StencilMask.shader
                "2a0d4af863770404faee6488b86fe3c9",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_DoubleShadeWithFeather_TransClipping_Tess_StencilOut.shader
                "1847c44f729b68e49ba63610abdf9132",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_OutlineObject_Tess.shader
                "06cae78b869a3234bab02eeb52197e1c",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_OutlineObject_Tess_StencilOut.shader
                "3a1af221400a61a4b94bae19aa79da2b",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_ShadingGradeMap_AngelRing_Tess.shader
                "a1449ab672051624ca3160737b630f5e",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_ShadingGradeMap_AngelRing_Tess_Light.shader
                "79d3dc54c32b69b42be17c48d33575f2",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_ShadingGradeMap_AngelRing_Tess_Light_StencilOut.shader
                "18c9172cdf36a344f9aca9bbc0e7002d",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_ShadingGradeMap_AngelRing_Tess_StencilOut.shader
                "54a94f776a43a074c8c2d205bb934005",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_ShadingGradeMap_AngelRing_TransClipping_Tess.shader
                "d496a1c70c797ad43836d5bfff575b5f",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_ShadingGradeMap_AngelRing_TransClipping_Tess_StencilOut.shader
                "183ea557143786346b1bfc862ad22636",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_ShadingGradeMap_Tess.shader
                "356dd5af8f0d40e41b647d3d0a0555c1",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_ShadingGradeMap_Tess_Light.shader
                "ffadecfbd9e31f840ba4109fea0f0436",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_ShadingGradeMap_Tess_Light_StencilMask.shader
                "98ac5d198a471494da681b7b8d1e1727",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_ShadingGradeMap_Tess_Light_StencilOut.shader
                "0d799eb857c0e2c45bbdfb2c033d33e6",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_ShadingGradeMap_Tess_StencilMask.shader
                "e667137c8b6fd3d4390fc364b2e5c70b",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_ShadingGradeMap_Tess_StencilOut.shader
                "feba437d8ff93f745a78828529e9a272",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_ShadingGradeMap_TransClipping_Tess.shader
                "8d1395a9f4bfad44d8fddd0f2af19b1e",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_ShadingGradeMap_TransClipping_Tess_StencilMask.shader
                "08c6bb334aed21c4198cf46b71ebca2d",  // Assets/VketShaderPack/Toon/Shader/Tess/Toon_ShadingGradeMap_TransClipping_Tess_StencilOut.shader
                "6d04fc34e9717d34d9589f39decf8333",  // Assets/VketShaderPack/Toon/Shader/Tess/UCTS_DoubleShadeWithFeather_tess.cginc
                "c139664fde6401f45a09b0f32279484b",  // Assets/VketShaderPack/Toon/Shader/Tess/UCTS_Outline_Tess.cginc
                "ad7807131760d5544843d7424e535b75",  // Assets/VketShaderPack/Toon/Shader/Tess/UCTS_ShadingGradeMap_tess.cginc
                "6261ac20c5dfa024a98d6ce3921bab70",  // Assets/VketShaderPack/Toon/Shader/Tess/UCTS_ShadowCaster_Tess.cginc
                "13aee1e1f6c49d94fa292dca9910126e",  // Assets/VketShaderPack/Toon/Shader/Tess/UCTS_Tess.cginc
                "b8bbbd51c2e41dd4bbcb0da1b7a48808",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/LICENSE
                "4ebc920fe2745624bbed02e79a222e3d",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/README.txt
                "f9bd228ff6fb34948a32cc6fd10d7d5b",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/version.json
                "b47edf5bc98ee4a40914f2217b80f666",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WFEditorSetting.cs
                "2af1dc2d564a2984285ccec8d3613390",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WFEditorSettings.asset
                "4e2b056a1e7bff1489e0ce6676d2ac92",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WFMaterialTemplate.cs
                "68c7e39ce323e8244a12bafca00f6afd",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WFMaterialValidator.cs
                "cab203e574463724c97f1793ab56065b",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WF_AutoMigrationPostprocessor.cs
                "b71e250f3c9f9a54cac228148bc800f7",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WF_Common.cs
                "6b1a45934e0846141979f322772dc3b8",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WF_DebugViewEditor.cs
                "052a5a21704733543a9cbbf6369ca43c",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WF_Dictionary.cs
                "fd719665a609948468efa505fb7931d9",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/wf_icon_menu.png
                "3cfda3489e17b1443aff9df4cca77dde",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WF_MaterialConverter.cs
                "efe907d6d9845294ba3924f3f2a5a9cb",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WF_MaterialEditUtility.cs
                "3ca4c3d3a4488214db9818862a2eff69",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WF_MiscUtil.cs
                "37f805e6e0a61ab4e9b1dd36b7b14d92",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WF_QuestScenePreprocessor.cs
                "4f0275352c196ca4d864b6611897bfd7",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WF_ShaderCustomEditor.cs
                "a623a7a5403c6bb4897c0f52eaa12f5e",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WF_ShaderPreprocessor.cs
                "e3269783b9ab81e4f85d813345bc1a7e",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WF_ShaderToolWindow.cs
                "16f6ecebfe3bedf48922ade8760ef404",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Editor/WF_VersionCheck.cs
                "2a4dc116efeb0db4192f11f17d555b87",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Logo/README.txt
                "c02ebf9b7a5d66c4ead5f94ef99b20c8",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Logo/UnlitWFロゴ_1024.png
                "54ed4f64546b23741987a94ff9769567",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Logo/UnlitWFロゴ_256.png
                "b8e19d3beb8c169458f9b150a00f40ec",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Logo/UnlitWFロゴ_512.png
                "c7e5995223250464cac205689e058693",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_DebugView.shader
                "aab16a18215b5e2488031de410fda510",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_FakeFur_FurOnly_Mix.shader
                "152ebc8c9a60c0e43820c71a0f0dfbb6",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_FakeFur_FurOnly_TransCutout.shader
                "a3777cac8e82d944bba542a87aa5368c",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_FakeFur_FurOnly_Transparent.shader
                "f19f31eb9bfd5154ba2c1a41c54a908b",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_FakeFur_Mix.shader
                "58bb80b63bec29d4384e105c53ca6970",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_FakeFur_TransCutout.shader
                "2210f95a2274e9d4faf8a14dac933fdb",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_FakeFur_Transparent.shader
                "c0f75d3ed420fd144a74722588d3bc21",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_Gem_Opaque.shader
                "21f6eaa1dd1f25c4cb29a42c4ff5d98f",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_Gem_Transparent.shader
                "cda050f4096b1df478fdc6fcc89586c7",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_Grass_TransCutout.shader
                "ec023985bf0f3f44bb6fe6bf962c2c69",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Custom_ClearCoat_Addition.shader
                "2dafe14658b486b4bb44869606d30d67",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Custom_ClearCoat_Opaque.shader
                "46e58649f5c3de74a9f578af4e6d30b5",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Custom_ClearCoat_TransCutout.shader
                "ccf60bd2b8af3e94ebc1a310962dac61",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Custom_ClearCoat_Transparent.shader
                "e4b04fed7c539d84887123c0beeffaca",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Custom_GhostOpaque.shader
                "4ba701b07ccc81e4aae7f053bf332eab",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Custom_GhostTransparent.shader
                "f3f80636c64e389498b3b19e2ee218da",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Custom_LameOnly_Transparent.shader
                "f5d5b7934ab1854418baf5042b0f7453",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Custom_Mirror_Opaque.shader
                "90cac9ec3b2a7524eb99b36ab87f25f1",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Custom_OffsetOutline_Opaque.shader
                "871fd7a51a8ea3e4980c3fe7b8347619",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Custom_PowerCap_Outline_Opaque.shader
                "7240817400475dd41b084e32b1264d6f",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Custom_PowerCap_Outline_TransCutout.shader
                "58ccf9c912b226146a25726b8a1f04db",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Custom_Tess_PowerCap_Opaque.shader
                "bd20e788383cdb943b829f230eb1c5ec",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Custom_Tess_PowerCap_TransCutout.shader
                "28e73745c5d1e464a876038f875501b4",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Custom_Transparent_FrostedGlass.shader
                "45e1aee78764bec499e99106c330f95a",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Custom_Transparent_Refracted.shader
                "6bf04fb5e50a2de4dbf06063f8592ab0",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_DepthOnly.shader
                "af51615040dcdad4cb01c29ea34dbb03",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Hidden.shader
                "4bd76f6599a5b8e4d88d81300fb74c37",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Mobile_Opaque.shader
                "d279a88eda1ae0e4c89e92539639eb16",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Mobile_OutlineOnly_Opaque.shader
                "e0b93fdad2eeedf42baccbc0975cdd1d",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Mobile_OutlineOnly_TransCutout.shader
                "98bb3de5d5444094aa041a65f8a85708",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Mobile_Outline_Opaque.shader
                "3276a740671679f44b1141523bf73e33",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Mobile_Outline_TransCutout.shader
                "af3422dc9372a89449a9f44d409d9714",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Mobile_TransCutout.shader
                "0a7a6cdca16a38548a5d81aca8d4e3ba",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Mobile_Transparent.shader
                "4e4be4aab63a2bd4fbcea2390ae92fdf",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Mobile_TransparentOverlay.shader
                "a3678756e883b9349ac22fce33313139",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Opaque.shader
                "4eef00f52cc21b04e9e34e4caefa6bbf",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_OutlineOnly_Opaque.shader
                "64bf3ca653a7b274fab3e8a87016bfb0",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_OutlineOnly_TransCutout.shader
                "660abd485057f4740ac9050f7ab3237d",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_OutlineOnly_Transparent.shader
                "3c07b964e541eef45bc195a029b878b3",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_OutlineOnly_Transparent_MaskOut.shader
                "a5ae7f40ac53e274ea0bc1262e1f6895",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Outline_Opaque.shader
                "ab4eb87c406a22f46887cf72178e2685",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Outline_TransCutout.shader
                "5523e041d29d259439fa14bd131f5c82",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Outline_Transparent.shader
                "5498b01615002d948bea7542f55e0c07",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Outline_Transparent3Pass.shader
                "9350854c6e88f3f4eb873d2f94ff3328",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Outline_Transparent_MaskOut.shader
                "ad88000744b4fb241835ba6ec106caf4",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Outline_Transparent_MaskOut_Blend.shader
                "0733cfc88032e8d4eafce250263c497c",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_PowerCap_Opaque.shader
                "2cf66b0706c40744baab089297afa895",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_PowerCap_TransCutout.shader
                "747bf283d686334469fb662b2fc4a5c2",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_PowerCap_Transparent.shader
                "d242cb83664caae4f957030870dd801d",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_PowerCap_Transparent3Pass.shader
                "dd3a683002b3a6f43bdb6c97bd0985c1",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Tess_Opaque.shader
                "94ee7f8988740fd4887f8b1ce41f0c1c",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Tess_TransCutout.shader
                "3bde56820d1aece41bd22966876a061c",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Tess_Transparent.shader
                "78d2e3fa0b8eb674aa9cf9e048f79c93",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Tess_Transparent3Pass.shader
                "8c7888a4ac175584f81e0b6e7d4af5a7",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_TransCutout.shader
                "15212414cba0c7a4aac92d94a4ae8750",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Transparent.shader
                "d1e7b0a18e221a1409ad59065ec157e4",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Transparent3Pass.shader
                "2efe527cfcbf0e1408b67463225f552f",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Transparent_Mask.shader
                "0b53cf0bcd0f9db4fa9d1297d255d06d",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Transparent_MaskOut.shader
                "d01a5c313ada49e488b2ef8c6b00f56d",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_Transparent_MaskOut_Blend.shader
                "a220e3e0675cc3f4f817a485688788d6",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_TriShade_Opaque.shader
                "2d294f328149d944eb0899b452ff879c",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_TriShade_TransCutout.shader
                "1435581bcf13e7a47b5bf5636f8d8252",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_TriShade_Transparent.shader
                "e7263331a8ee0a04aa4a271fc1fef104",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_TriShade_Transparent3Pass.shader
                "0299954f2a9b0994f8c9587945948766",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_TriShade_Transparent_Mask.shader
                "06e9294a93df4474cac2f4157b5e1d1d",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_TriShade_Transparent_MaskOut.shader
                "dfb821bc7afadc14591e4338a8ec865f",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_UnToon_TriShade_Transparent_MaskOut_Blend.shader
                "fb639668f92376a4786d635258ae96da",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_Water_Caustics_Addition.shader
                "0c20402a658b66444a95461b76f367b2",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_Water_DepthFog_Fade.shader
                "c6e2e8e77eaf9bb4b82f757fe9b823f0",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_Water_Lamp_Addition.shader
                "17124f35e2c0c5244a712e8654a994f9",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_Water_Sun_Addition.shader
                "ffa6a9ecd57d50a4db625f7ed4fabeeb",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_Water_Surface_Custom_Mirror_Opaque.shader
                "5c1fc3340b9976949b12562190be5db0",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_Water_Surface_Custom_Mirror_TransCutout.shader
                "188bb4cd384ba624892e50b5b35f7297",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_Water_Surface_Custom_Mirror_Transparent.shader
                "5c54a3a3c81772549819db795008c986",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_Water_Surface_Opaque.shader
                "92675e22926880147a67c25086c3bb70",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_Water_Surface_TransCutout.shader
                "a3641cf3b665bec468ac868660864d64",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_Water_Surface_Transparent.shader
                "1332d868874e61e44b48c1f59bf4bd11",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/Unlit_WF_Water_Surface_TransparentRefracted.shader
                "0380b1621ab524c43aeb10eba3346ea6",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_Common.cginc
                "578346e318940304389ae3dda992ac86",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_Common_BuiltinRP.cginc
                "2762fae01792d2745ad5d02376392d52",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_Common_LightweightRP.cginc
                "ef1a901a2feeb0a45859ecc184e2e3e2",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_FakeFur.cginc
                "b892a7ae3359eb0428b2f8aebf24d314",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_FakeFur_Uniform.cginc
                "45af0d16a1af0a947b445e08dd6dead4",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_Gem.cginc
                "34a1cdb7cd82cd045a521aa2db90ba27",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_Gem_Uniform.cginc
                "c97f16136cf45b748a0b5223970f14be",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_Grass.cginc
                "f92b42d23402f9b4dbec6b3f2285b1d9",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_Grass_Uniform.cginc
                "77ee5292cc4f46649a13611c8d76c85b",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_INPUT_FakeFur.cginc
                "e33666b113c868d41bfa058f5bc50d9c",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_INPUT_Gem.cginc
                "740ad847af5985b4a90b5494a44772d0",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_INPUT_Grass.cginc
                "be668f2e5a5e4ef46838001f79babcef",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_INPUT_UnToon.cginc
                "f288c96f64ec292449ffe1bfd631b586",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_INPUT_Water.cginc
                "22546fe6fb0bed84e8db3fc80b0b2302",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_UnToon.cginc
                "3b73cacbca25f4a4baf987d7257ef5fb",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_UnToon_AudioLink.cginc
                "33f7da738e6fb71439ca490558305e3c",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_UnToon_ClearBackground.cginc
                "9534237cfc7d3bd49b9b45a45d0750c5",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_UnToon_ClearCoat.cginc
                "93e68367384c3bd42a3a37868cc25554",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_UnToon_DepthOnly.cginc
                "8e439fa11883d4b429904a7fc398851e",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_UnToon_Function.cginc
                "afa8b2842288b044b9cdccd7508670a7",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_UnToon_LineCanceller.cginc
                "074195645f64a224d9482cb666563c89",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_UnToon_Meta.cginc
                "bf91baf439ae72542bd718eb51378f5a",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_UnToon_PowerCap.cginc
                "ad9922cd501663b4cbfbef594d1b22d0",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_UnToon_ShadowCaster.cginc
                "95ae3c73098e55148862b3125c46785e",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_UnToon_Tessellation.cginc
                "bad784f674c77404f8234c8d284656d2",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_UnToon_Uniform.cginc
                "fbecb1333483eba4b923d7dfa6fbf1dd",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_Water.cginc
                "9a1dcf37b1c3190499c0eb84f1b6759d",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Shaders/WF_Water_Uniform.cginc
                "f54324a974e6e7f43a1125942128ce95",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Template/wf00_Basic_標準.asset
                "672497684d940da4f807c5ae07833ceb",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Template/wf01_Skin_肌.asset
                "3e5122ad422ece24b9a02da53f17c1ab",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Template/wf02_Face_顔.asset
                "614e36c2007867145adf36d0207a0c84",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Template/wf03_Hair_髪.asset
                "517680171b52d3046a9d2e3da261dadb",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Template/wf09_FacialEffect_表情パーツ.asset
                "d2346e13970e83b4691c4e4a55c0555d",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Template/wf21_Metallic_金属.asset
                "29a0a4fb194bcdb4189eb970c6abaad9",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Template/wf22_Rubber_ゴム.asset
                "a42db1a0256afed489f5723f7bcf19b3",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Template/wf23_Fur_ファー.asset
                "488d3c5263b2c6949b3a302e783784a9",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Template/wf24_Gem_宝石.asset
                "7ed92ab47682e2c4ebf707f9b409fd77",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Template/wf25_Ghost_幽霊.asset
                "c3c25fb89b9120f4192dff0c67fbb119",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Template/wf26_GlassLens_ガラスレンズ.asset
                "38f0db87dbdc7b147852b318a4388472",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Template/wf27_FrostedGlass_くもりガラス.asset
                "bb9610632e748424586247724588439e",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Texture/CubeMap/32x32/HDRIHaven_lythwood_lounge_1k_32.hdr
                "80b684ec03e5e1c40943d9eb0e0d32f4",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Texture/CubeMap/32x32/HDRIHaven_lythwood_room_1k_32.hdr
                "aa8a3f966d6b7874fa743d4c67f78068",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Texture/LightCap/lcap_WF_ラバー光沢6.png
                "0089774353388424d8f87a85c5dd84b2",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Texture/MedianCap/mcap_WF_影なし_髪用_エンジェルリング.png
                "8a48db2e83469024789112bf721b2c2e",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Texture/misc/Lamp_Cookie_01.png
                "0c90f262b70f7634ea0fb53f2912f537",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Texture/Noise/noise_ランダム(粗)_1024.png
                "3dc3b246520274646989d6e4fc7089c1",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Texture/Noise/noise_水底(Caustics)_1024.bmp
                "85ffbe629c1736d49b22c059ad134322",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Texture/Noise/noise_水面_512.png
                "ba587353da6c721418c7a2b4bd4cd7bf",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Texture/Noise/normal_凹凸(細)_1024.png
                "6aa39f0cbabc2d34eb842e2881d4f50e",  // Assets/VketShaderPack/Unlit_WF_ShaderSuite/Texture/Noise/normal_水面_512.jpg
                "8020a337ceab108438d088a3482a4b90",  // Assets/VketShaderPack/VRMShaders/CHANGELOG.md
                "00999fd020bde754ab4ae5f8a5205844",  // Assets/VketShaderPack/VRMShaders/LICENSE.md
                "0905ad83e0b774444bcc48ac9a191d51",  // Assets/VketShaderPack/VRMShaders/package.json
                "4918a8517e721c5429d0f8033ae065c7",  // Assets/VketShaderPack/VRMShaders/README.md
                "21fb6bf38127a35498543f81ba8cc2e2",  // Assets/VketShaderPack/VRMShaders/Documentation/VRMShaders.md
                "4c70714358bb2fb4fa96ef08640763fd",  // Assets/VketShaderPack/VRMShaders/GLTF/UniUnlit/Editor/UniUnlitEditor.cs
                "529ce3a240c1a7a4bbbb220bbd59686b",  // Assets/VketShaderPack/VRMShaders/GLTF/UniUnlit/Editor/VRMShaders.GLTF.UniUnlit.Editor.asmdef
                "8c17b56f4bf084c47872edcb95237e4a",  // Assets/VketShaderPack/VRMShaders/GLTF/UniUnlit/Resources/UniGLTF/UniUnlit.shader
                "318c9e903f457f94589b2c5513d7d914",  // Assets/VketShaderPack/VRMShaders/GLTF/UniUnlit/Runtime/UniUnlitUtil.cs
                "60c8346e00a8ddd4cafc5a02eceeec57",  // Assets/VketShaderPack/VRMShaders/GLTF/UniUnlit/Runtime/VRMShaders.GLTF.UniUnlit.Runtime.asmdef
                "486ebb794ada0de41be4f35c56876f82",  // Assets/VketShaderPack/VRMShaders/VRM/VRMShaders.shadervariants
                "50935dd2f9f3fa445a687f30d4dd663b",  // Assets/VketShaderPack/VRMShaders/VRM/IO/Runtime/PreShaderPropExporter.cs
                "279964035c950b24cb745511298855dd",  // Assets/VketShaderPack/VRMShaders/VRM/IO/Runtime/ShaderProps.cs
                "301b251fd9834274c9228e0532f444f7",  // Assets/VketShaderPack/VRMShaders/VRM/IO/Runtime/VRMShaders.VRM.IO.Runtime.asmdef
                "2a5e8a5d481e3574b8274fa7ce4bdc2d",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/LICENSE
                "1021e7e6d453b9f4fb2f46a130425deb",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/README.md
                "a9bc101fb0471f94a8f99fd242fdd934",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/MToon.asmdef
                "24156f9da0724eb5a159f36c69a7d90a",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Editor/EditorEnums.cs
                "531922bb16b74a00b81445116c49b09c",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Editor/EditorUtils.cs
                "dddf8398e965f254cae2d7519d7f67d2",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Editor/MToon.Editor.asmdef
                "8b43baa9f62f04748bb167ad186f1b1a",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Editor/MToonInspector.cs
                "1a97144e4ad27a04aafd70f7b915cedb",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Resources/Shaders/MToon.shader
                "ef6682d138947ed4fbc8fbecfe75cd28",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Resources/Shaders/MToonCore.cginc
                "084281ffd8b1b8e4a8605725d3b0760b",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Resources/Shaders/MToonSM3.cginc
                "17d4e0f990fbc794ab41e4fcc196d559",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Resources/Shaders/MToonSM4.cginc
                "8b731264e8acd0f4b8f56986e5eb2531",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Samples/OutlineWidthModes.unity
                "4f42a26097c877b40a7616aa60580c43",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Samples/Materials/Ex_OutlineWidth_Screen.mat
                "e40a129e14e378c4db040df3fd4a6077",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Samples/Materials/Ex_OutlineWidth_World.mat
                "54da18ba3126f1343924588562df72e0",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Samples/Materials/Ground.mat
                "9639e17dffc656345a70282f7f216672",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Samples/Materials/Toon.mat
                "9a3fb070d7eb4114b5cf387e2cd60391",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Scripts/Enums.cs
                "2849b99d94074fcf9e10c5ca3eab15a8",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Scripts/MToonDefinition.cs
                "9d2012c170a74b3db0002f7ecda53622",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Scripts/Utils.cs
                "6724aa45c8c349fabd5954a531301aa8",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Scripts/UtilsGetter.cs
                "b24a672e82874c9fbfef9c2b2dfdab42",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Scripts/UtilsSetter.cs
                "4702d4b2c1414cc08b4382c3762eebab",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Scripts/UtilsVersion.cs
                "58f01bf8180084642a5f4c17a3c8cd4b",  // Assets/VketShaderPack/VRMShaders/VRM10/VRM10Shaders.shadervariants
                "bce005214fa49654d93927908c15b1f2",  // Assets/VketShaderPack/VRMShaders/VRM10/Format/Runtime/VRMShaders.VRM10.Format.Runtime.asmdef
                "7bbff45dd057a5a4d935d5ee9a64e40a",  // Assets/VketShaderPack/VRMShaders/VRM10/Format/Runtime/MaterialsMToon/Format.g.cs
                "af7abf08bb63495fae1f334377be0b23",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Editor/LabelScope.cs
                "22a9170004064d029bc205fc5b5a1acc",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Editor/MToon10EditorEditMode.cs
                "b8b6f1e6a8a133a4987361f178d9f548",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Editor/MToonInspector.cs
                "e3216dcc79b326b47b307c06b99c7331",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Editor/VRMShaders.VRM10.MToon10.Editor.asmdef
                "e0edbf68d81d1f340ae8b110086b7063",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon.shader
                "82d864eead0e48f79295fe490a6d614f",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_attribute.hlsl
                "cfd10dec96ea4e3f9fad70e2e5dc148e",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_define.hlsl
                "93f88dc204e783a4aaabfc17a087bce8",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_depthnormals_fragment.hlsl
                "b69080781a3778e499a6ae99adb82053",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_depthnormals_vertex.hlsl
                "68d7d7ebb0ec69440b052da859a20d74",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_depthonly_fragment.hlsl
                "6957b39e3ab2cbc43aa0de76d9b4a411",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_depthonly_vertex.hlsl
                "3717321cda361ad48abfc6b4fcb7a320",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_forward_fragment.hlsl
                "26b921255f4e4d62a8c0ec730521831f",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_forward_vertex.hlsl
                "87aa2d8d38194c35a6531b8f5e3da7aa",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_geometry_alpha.hlsl
                "5ddfcfc47ee84eed9365b902bf76ce75",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_geometry_normal.hlsl
                "1381f11f525a4d90a57348ada020ea01",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_geometry_uv.hlsl
                "92e89e208e604f28acdce8a142d1e241",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_geometry_vertex.hlsl
                "c7f37d6d268db6f4da25955d0ca7cb01",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_input.hlsl
                "54d13d2ffd7249259772084874b76659",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_lighting_mtoon.hlsl
                "08491980bb224017bbb8e432761db8c4",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_lighting_unity.hlsl
                "4c9e0c12587dbb5488a3e07dfc9430d3",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_render_pipeline.hlsl
                "de3bc67bd63189846842d2a03b2b13a9",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_shadowcaster_fragment.hlsl
                "06e9bdc3bc909534399f25d6769c8396",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_shadowcaster_vertex.hlsl
                "0781c64b7f2a1604ea6aa7e252080372",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_urp.shader
                "79a73f8bac4e45498341e9b6c1ea841e",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Resources/VRM10/vrmc_materials_mtoon_utility.hlsl
                "1f07ab1da80c4891938ee36c6f2042a2",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/MToon10Context.cs
                "920db45663454ba8b8debaf2aeb7f5c9",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/MToon10Migrator.cs
                "5b6d00cd3f0249a2b4ee7dc86f9a8d63",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/MToonOutlineRenderFeature.cs
                "df162971c1034acdbe28ff1ec94d8601",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/MToonOutlineRenderPass.cs
                "18b52251a22d40ccbfc16c4a89d09c51",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/MToonValidator.cs
                "0aaf403bd13871a44b7127aef2695ff8",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/VRMShaders.VRM10.MToon10.Runtime.asmdef
                "173b50f65d2a44109cc67a84f18bde53",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/Extensions/MaterialExtensions.cs
                "4915ab0041b841b6aa0cdfecae764008",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/MToonDefinedValues/MToon10AlphaMode.cs
                "c3c422f6c0ac416bbcd70ee3203bca6a",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/MToonDefinedValues/MToon10DoubleSidedMode.cs
                "c9f4547527b3411aa87d8322ce9d9132",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/MToonDefinedValues/MToon10EmissiveMapKeyword.cs
                "78ea3fe03cac402083bd53882d186b95",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/MToonDefinedValues/MToon10NormalMapKeyword.cs
                "8aece52be2cb42de9bfde4a072e5a6a0",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/MToonDefinedValues/MToon10OutlineMode.cs
                "94313b3f0f4943d681f40e4cfbd5b600",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/MToonDefinedValues/MToon10OutlineModeKeyword.cs
                "4cf6251408b74e228660a1a43f4de9c9",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/MToonDefinedValues/MToon10ParameterMapKeyword.cs
                "f22a9af9be334674ad2638be0af0c818",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/MToonDefinedValues/MToon10RimMapKeyword.cs
                "d5df515a36d3460891b348872459476f",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/MToonDefinedValues/MToon10TransparentWithZWriteMode.cs
                "4cb4057134654c769422a3618b76bccc",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/Properties/MToon10Meta.cs
                "aea6e3bb9944499e94c1026d60104432",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/Properties/MToon10Prop.cs
                "3c8828cd778c400f81b8a1c93b54ee50",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/Properties/MToon10Properties.cs
                "1e9a4286845a4c998a402e50da51ecc9",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/ShaderLabDefinedValues/UnityAlphaModeKeyword.cs
                "d254dee2e3b3407abcdc3f6f84b3cd59",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/ShaderLabDefinedValues/UnityAlphaToMaskMode.cs
                "03ac85ab52a547019ff7e377f1778225",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/ShaderLabDefinedValues/UnityCullMode.cs
                "df547717467444c29ff3dcec1e555e17",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/ShaderLabDefinedValues/UnityRenderTag.cs
                "4c80d709af9c46f9b57290659de4c1c6",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Runtime/UnityShaderLab/ShaderLabDefinedValues/UnityZWriteMode.cs
                "6530bdc57fd7d354db37f84b93222964",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Tests/MigrationTests.cs
                "a8e4a462662564e48931da8e2a390e60",  // Assets/VketShaderPack/VRMShaders/VRM10/MToon10/Tests/VRMShaders.VRM10.MToon10.Tests.asmdef
            };
        }

        /// <summary>
        /// 公式配布されたアセット及び前提となるアセットのGUID
        /// <list type="bullet">
        /// <item>DynamicBone</item>
        /// <item>TextMeshPro</item>
        /// <item>VRCSDK</item>
        /// <item>Udon</item>
        /// <item>VitDeck</item>
        /// <item>VitDeck Template SharesAssets</item>
        /// <item>VketAssets</item>
        /// <item>VketShaderPack</item>
        /// </list>
        /// </summary>
        public virtual string[] GetGUIDs()
        {
            var guids = new List<string>();
            guids.AddRange(GetDynamicBoneGUIDs());
            guids.AddRange(GetTextMeshProGUIDs());
            guids.AddRange(GetVrcSdkGUIDs());
            guids.AddRange(GetCyanTriggerGUIDs());
            guids.AddRange(GetVitDeckGUIDs());
            guids.AddRange(GetExhibitTemplateGUIDs());
            guids.AddRange(GetVketAssetsGUIDs());
            guids.AddRange(GetVketShaderPackGUIDs());
            guids.AddRange(GetGajouenCollectGUIDs());
            return guids.ToArray();
        }

        public virtual string[] GetMaterialGUIDs()
        {
            return new string[]
            {
                "86c2d2ecf42eb67438b001b3f2d0b12b",  // Assets/VitDeck/Templates/V2023S_03_Cocoon/SharedAssets/BoothFrame/mat_tex/wall_cement_dark.mat
                "d98d77a41aada9a4ba1b50b47b2b4ac6",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/Booth_Ceiling.mat
                "8feacc893f8903f428e62b5d4a274ab9",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/Booth_CeilingBrick 1.mat
                "aa2c619f1a7ef2248b1c29fd76bf6fa1",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/Booth_CeilingBrick 2.mat
                "9e1c7fa8fd460244980920c5148d85e5",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/Booth_CeilingBrick 3.mat
                "75d2ebe26e381554db56ea6cc21dcf0c",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/Booth_CeilingWood.mat
                "2f1a70da51c6ae943a4e092278f63354",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/Booth_Ground.mat
                "40f14ca9ab4400c44b2c194ba501acc0",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/Booth_Ground_Volcano.mat
                "b4e58c9cefb368b4d9ba1d314fe80437",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/Booth_PillarBark.mat
                "a1089c9d41d525a40a3a0bf725299cf7",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/Booth_PillarBrick.mat
                "2d1c0625b7823ed4f88e9c3eb22e04b8",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/Booth_PillarWood.mat
                "daff3d4f409c9a7418ba1a9a1d091ae3",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/Booth_WallBrick.mat
                "44cca76faf4846d479eda3c5db870507",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/leaf_basecolor_1.mat
                "0ec8c6e049d1f1845a8a49e6d49fb999",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/Summer_Wall.mat
                "66bb025986bd66c479f0fe7e007798b0",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/volcano/Booth_CeilingMetal.mat
                "f9d3d0adb0578be45993c6728d3cc2dc",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/volcano/Booth_PillarMetal.mat
                "4a2abd4f67aefc24b9c6cca2d197adc7",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/volcano/Booth_underPillarMetal.mat
                "0acfe541cc046384d9e49c053de97ffb",  // Assets/VitDeck/Templates/V2023S_08_VketPlaza_Quest/SharedAssets/BoothFrame/Materials/volcano/Booth_volcano_wal.mat

                "798bf62f082a7a64c9d48e6f992ecfaa",  // Assets/VketAssets/Assets/EssentialResources/Common/Materials/DisabledVideoPlayerImage.mat
                "0de3ccc1017c4a045a4fed5f810c2748",  // Assets/VketAssets/Assets/EssentialResources/Common/Materials/UI-Lookat.mat

                "8576b2a3980cc7d428c9fcf4c773334d",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Avatars/ExampleCloth.mat
                "eae4cef569efe05418ebeea9a5470208",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Avatars/ExampleSkin.mat
                "4e16100b62f2359498532c66b47882cd",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials/mat1.mat
                "6109487d6db0bd4449ab735a5af1984e",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials/material.mat
                "f488c7b5e7cf3974197144a0c871825d",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials/violin.mat
                "64af6d10c296b2e43ba609d43989fa6d",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/mannequin-knit.mat
                "bf69b0161b0200144bc341c870898c79",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/mannequin-steel.mat
                "29d082225a76ffd4e8f1ab6c7867d520",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/material 1.mat
                "80930cdb070cfdd4198ba453a8d6928c",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/material 2.mat
                "b6619d1e3ab73d44683abcc8d4d10ade",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/material 3.mat
                "152846edca303b04798828a2a8ed5632",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/material 4.mat
                "4945243b525e98e4da08d6bcbf09e9c9",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/material 5.mat
                "c1dc601b37be9f642a8ba8fa2e297ebc",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/material 6.mat
                "f5555ea0e7eaacd4ebf16bf193191202",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/material.mat
                "1b47fc1779f32b74e84554551a52332b",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialEmissiveFreak.mat
                "6e8733029fd04b74a87a60c54da93bef",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialEmissiveFreakCutout.mat
                "6b15922ce569b954ba8a2ffa74113aa7",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialEmissiveFreakFade.mat
                "c61b7358e6597ea4f9ee82aedc2f7bd8",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialEmissiveFreakFadeRefracted.mat
                "db772d6df514e144cbad8bdaa16ec12a",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialStencilReader.mat
                "608bcb9d4fcab8740a0ce41811326a59",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialStencilReaderFade.mat
                "a4678bc73ce194441a3d163bb1cbd7ae",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialStencilWriter.mat
                "d5e3014f3f488c140a05c07aef01b7f7",  // Assets/VketShaderPack/ArxCharacterShaders/Examples/Objects/Materials_Textures/materialStencilWriterEmissiveFreak.mat
                "7e3e2734c20f8704986e4b44f7950257",  // Assets/VketShaderPack/Filamented/CrossStandardTest.mat
                "497f8485774204244abb7ba6c0865927",  // Assets/VketShaderPack/Mochie/Unity/Prefabs/Materials/Screen FX Mat.mat
                "8d3f0d345ef88514794c6c74d5e23212",  // Assets/VketShaderPack/Mochie/Unity/Prefabs/Materials/Underwater Mat.mat
                "5488c79609ff040468ac12e94672fc70",  // Assets/VketShaderPack/Mochie/Unity/Prefabs/Materials/Water Mat.mat
                "721cd36de640a974ca45b613e85cd800",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/Sample/Sample 1.mat
                "4b8608d176dcf934585ec1b6886e05e4",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/Sample/Sample 2.mat
                "d469e28ac045d044fb9cb2226a7c9c72",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/Sample/Sample.mat
                "af8197deebc61ce459480bd679aa6abc",  // Assets/VketShaderPack/Silent's Cel Shading Shader/Assets/YSHT/Sample/Sample_SCSS.mat
                "4f42a26097c877b40a7616aa60580c43",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Samples/Materials/Ex_OutlineWidth_Screen.mat
                "e40a129e14e378c4db040df3fd4a6077",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Samples/Materials/Ex_OutlineWidth_World.mat
                "54da18ba3126f1343924588562df72e0",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Samples/Materials/Ground.mat
                "9639e17dffc656345a70282f7f216672",  // Assets/VketShaderPack/VRMShaders/VRM/MToon/MToon/Samples/Materials/Toon.mat

                "da07ab9b78cb0432e95e11e2cb619ea7",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Materials/BlueprintCam.mat
                "2166f6bbfce69594fad494087eca58e8",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Materials/damageGrey.mat
                "841c3ce718e8b61408005c1cfce6b7de",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Models/Materials/lambert2.mat
                "68be9f0f6e5adbd44a76bf6bf69fda7b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/BrightButton.mat
                "9414e644b0d9d4c4cb1d863093f0284c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/Chair.mat
                "b6099d83d6f02e34ea589e768df4173b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/Green.mat
                "34348aa1b91e32f48bda8333f82f6335",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/GUI_Gradient_Ground.mat
                "4546b0ec54086e840800d63eb723acd2",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/GUI_Zone_Holo.mat
                "c815f7613a04b724089c206857e57c6a",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/MirrorReflection.mat
                "7a2568654af4bef4cad7a3dfa02c31b2",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/Red.mat
                "4a04f8d3981104848915e66f7a02ec72",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Materials/Screen.mat
                "26803b57669325843a97b0ae43031082",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/PanoViewer/Sphere.mat
                "4876fc9dc009bbe4493553020a561611",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_black_grid.mat
                "eae9c11350249284e8400a100179e0b2",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_blue_grid.mat
                "1ab66d94bde8cce46bb35638099bfd31",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_grey_smooth.mat
                "76ff537c8e1a84345868e6aeee938ab3",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_navy_grid.mat
                "1032d41f900276c40a9dd24f55b7d420",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_navy_smooth.mat
                "8c19a618a0bd9844583b91dca0875a34",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_pink_grid.mat
                "fed4e78bda2b3de45954637fee164b8c",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_pink_smooth.mat
                "5aa95b3fa56e28f43a84e301c3d19e08",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_white_grid.mat
                "799167b062f9e2944a302eea855166b4",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_white_smooth.mat
                "82096aab38f01cb40a1cbf8629a810ba",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_yellow_grid.mat
                "6e1d36c4bbd37d54f9ea183e4f5fd656",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Materials/prototype_yellow_smooth.mat
                "4cfb7ae289eb1e546b751d287bc1ee62",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/Materials/NavyGrid.mat
                "22a917a65630c404e8ebe2c26a9c7d5e",  // Packages/com.vrchat.base/Runtime/VRCSDK/Sample Assets/Prototyping/Models/Materials/PinkSmooth.mat

                "ae776b55e18c8b9499ea29e238b86238",  // Packages/com.vrchat.worlds/Samples/AsyncGPUReadbackExampleScene/Test.mat
                "c10d8bf61c50ae74a8ba37e6c69900c1",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/Materials/MiniMap Blitter.mat
                "6792d124852a74b4291d60d0f2787f31",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/Materials/MiniMap.mat
                "4e872ec796d64d540aa9016e6f4528cb",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Floor.mat
                "88e9119b6e679a848b6cc4b812721fbb",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Blue Glow.mat
                "268f44155a3f69e499ecc06c8a037a6c",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Blue.mat
                "f7b973f95bf253b4aa6c2e4985c86c06",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Glow.mat
                "2249d68a0e7bf074286c24e22d34ced8",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Green Glow.mat
                "02d6f7e0ec6eaca4ea8f10743bacfc52",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Green.mat
                "f9b6ae70f8b27454da67e14997bb525a",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Purple Glow.mat
                "f5f9510b4abf7e84e86d402def103b2d",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Purple.mat
                "e913b7f0bf0007b4590919fe35053b71",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Red Glow.mat
                "a86511bcb78f07a419a662d27b8be0b2",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material Red.mat
                "0388ea6ffe023754792ecc3e749e538d",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sample Material.mat
                "a4beea845c9905b459897504b0e5698c",  // Packages/com.vrchat.worlds/Samples/GraphicsBlitExampleScene/SampleSceneAssets/Sky.mat
                "506771de2b6f16f4494d9cad34491466",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/AVProVideoScreen.mat
                "e3769e73b10dfc1498cf1136e66de63a",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/CubeMat1.mat
                "99f7ea0146bcbb64d97a8468253bb347",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/CubeMat2.mat
                "cc54f62d6419422419aacb98b2cbaa66",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/CubeMat3.mat
                "3e749d8edb4501f488bf37401bec19cf",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/Ground.mat
                "74de320d298ce3e498b0401ec1ffcb7f",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/ImageMaterial.mat
                "c706afb4295d44a48a7a860f31d36150",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/LineMaterial.mat
                "8f5d353f21dad544ebeb59af6fe64604",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/PickupCubeMat.mat
                "219b8b6950b888f40b189f45cb13f02a",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/TriggerAreaMat.mat
                "cba30de4550b90f4f8ef7bc7d94faf95",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Materials/UnityVideoPlayerScreen.mat
                "fc18322c8bd152b458ffc49ade697169",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/GridFloor.mat
                "fd20e45036ef323459e8286e9c23c02c",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/GridFloor_udon.mat
                "1c987494452b85f4ab4cac3322415907",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_matte.mat
                "f32dd500294c1d048bf0629cf0c69be5",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_matte_blue.mat
                "a6c1d9564b56ecd47b82dfa7a8f11cbe",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_matte_green.mat
                "3b420fd445c370647be21f178917127d",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_matte_red.mat
                "5bec13570cd015140a051a07a3c55af5",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_mixed.mat
                "278c5fc8b64c3514b98f6554ff2e1328",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_mixed_blue.mat
                "1e2cef468006db345aef0ff70a68e96f",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_mixed_green.mat
                "3fc341313acf6ac48af69958cf612904",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_mixed_red.mat
                "916688f1c2e4c63498d399d9335c9ef7",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_shine.mat
                "5461c3b904b45cb4b932e10263cb3c88",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_shine_blue.mat
                "2d24fc897d87d8d4a80a06e5684c2eb7",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_shine_green.mat
                "d419d3432b8a0a24b986e614c57c2039",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SampleAssets_shine_red.mat
                "21221da753878694b9b9518a540dda85",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Materials/SKY_UdonLab.mat
            };
        }

        public virtual string[] GetPickupObjectSyncPrefabGUIDs()
        {
            return new string[]
            {
                "ba410268b82f1d940aedd0d418541c83",  // Assets/VketAssets/Assets/VketPrefabs/VketPickup/VketFollowPickup.prefab
                "6d1e9170d4533ed448e46b707a3ee47a",  // Assets/VketAssets/Assets/VketPrefabs/VketPickup/VketPickup.prefab
            };
        }


        public virtual string[] GetAvatarPedestalPrefabGUIDs()
        {
            return new string[]
            {
                "da2f193786576d041aa8d2e860314c22",  // Assets/VketAssets/Assets/VketPrefabs/VketAvatarPedestal/VketAvatarPedestal_2D.prefab
                "9fffe84a94533884eaf481963546643d",  // Assets/VketAssets/Assets/VketPrefabs/VketAvatarPedestal/VketAvatarPedestal_3D.prefab
                "1e0f83d3ba1d83045866a6a4dc2e8e83",  // Assets/VketAssets/Assets/VketPrefabs/VketAvatarPedestal/VketAvatarPedestal_Default.prefab
            };
        }

        public virtual string[] GetVideoPlayerPrefabGUIDs()
        {
            return new string[]
            {
                "73b0727ab433c3140929fbf088cd8b88",  // Assets/VketAssets/Assets/VketPrefabs/VketVideoPlayer/VketVideoPlayer.prefab
            };
        }

        public virtual string[] GetImageDownloaderPrefabGUIDs()
        {
            return new string[]
            {
                "04f73cff8c985724da36ca6890c417fb",  // Assets/VketAssets/Assets/VketPrefabs/VketImageDownloader/VketImageDownloader.prefab
            };
        }

        public virtual string[] GetStringDownloaderPrefabGUIDs()
        {
            return new string[]
            {
                "d19ff96a19f6fdb4cb57095e22e5ba37",  // Assets/VketAssets/Assets/VketPrefabs/VketStringDownloader/VketStringDownloader.prefab
            };
        }

        public virtual string[] GetStarshipTreasurePrefabGUIDs()
        {
            return new string[]
            {
                "dc2f05a9ff39c964886c7b5638799687", // Assets/VketStarshipTreasure/VketStarshipTreasure.prefab
            };
        }

        public virtual string[] GetGajouenCollectGUIDs()
        {
            return new string[]
            {
                "6bae13377363d2f40b430eb7f6fa772c",  // Assets/VketGajouenCollect/VketGajouenCollect2D.prefab
                "e35766d0b5ef7554fab3a5d6572542d2",  // Assets/VketGajouenCollect/VketGajouenCollect3D.prefab
                "f07506aea4ce24047ad5b1617e9ce7c4",  // Assets/VketGajouenCollect/Materials/KiraParticle.mat
                "b93504a0265f2434ba89e2b0053811e1",  // Assets/VketGajouenCollect/Models/Kira.fbx
                "7e85f66e7480c8843a53787625f3bf2c",  // Assets/VketGajouenCollect/Scripts/d88a03e9c58b1f743b0281c0cdec71bf.asset
                "100b1304f5822fa4db9126211c0491ba",  // Assets/VketGajouenCollect/Scripts/GajouenCollect2DSetup.cs
                "5d87db7345abd1d4dabb8e0bc3ad9822",  // Assets/VketGajouenCollect/Scripts/GajouenCollect3DSetup.cs
                "d88a03e9c58b1f743b0281c0cdec71bf",  // Assets/VketGajouenCollect/Scripts/VketGajouenCollect.asset
                "09e11c275306a8d4481c8c618e0892b0",  // Assets/VketGajouenCollect/Scripts/VketGajouenCollect.cs
                "34b7d3b27ed7f9c458bf0c1f1c0b01fd",  // Assets/VketGajouenCollect/Textures/vket2023w_icon_hotelgajouen_goyoumei_1.png
                "55e42c2311ed5ee4b8f944a89660ca24",  // Assets/VketGajouenCollect/Textures/vket2023w_icon_hotelgajouen_goyoumei_2.png
            };
        }

        public virtual string[] GetUdonBehaviourPrefabGUIDs()
        {
            var guids = new List<string>();
            guids.AddRange(GetPickupObjectSyncPrefabGUIDs());
            guids.AddRange(GetAvatarPedestalPrefabGUIDs());
            guids.AddRange(GetVideoPlayerPrefabGUIDs());
            guids.AddRange(GetImageDownloaderPrefabGUIDs());
            guids.AddRange(GetStringDownloaderPrefabGUIDs());
            guids.AddRange(new string[]
            {
                "abb75ce26e18f4944b01089401edb9fa",  // Assets/VketAssets/Assets/EssentialResources/ExhibitorBoothManager/ExhibitorBoothManager.prefab
                "d0e55bed1631139489f283284ae3127d",  // Assets/VketAssets/Assets/EssentialResources/ExhibitorBoothManager/RigidbodyManager.prefab
                "3c0dbec26839f9b4ea24f9606ec62ce4",  // Assets/VketAssets/Assets/VketPrefabs/UITemplate/UI_Button.prefab
                "b4625b5c33c27804d889d16704b81c33",  // Assets/VketAssets/Assets/VketPrefabs/UITemplate/UI_Image.prefab
                "4dc5396d6e370ef4fa9b9e9458c3f735",  // Assets/VketAssets/Assets/VketPrefabs/UITemplate/UI_Text.prefab
                "333992c7f0890014d9a775e3f2303857",  // Assets/VketAssets/Assets/VketPrefabs/UITemplate/UI_TextMeshPro.prefab
                "fc94f96f00c165842bddecda68e0e3fe",  // Assets/VketAssets/Assets/VketPrefabs/VketChair/VketChair.prefab
                "d1d36f4319c73e941b11296c909b98dc",  // Assets/VketAssets/Assets/VketPrefabs/VketChair/VketFittingChair.prefab
                "8c011f4ab9cc45c4aaddb76bbd8003c5",  // Assets/VketAssets/Assets/VketPrefabs/VketLanguageSwitcher/VketLanguageSwitcher.prefab
                "b2a6c13adeda05d40adb398906d58645",  // Assets/VketAssets/Assets/VketPrefabs/VketSoundFade/VketSoundFade.prefab
                "b291170635bff9841bbd09d362a0d170",  // Assets/VketAssets/Assets/VketPrefabs/VketVideoPlayer/VketVideoUrlTrigger.prefab
                "5d4f49b1d4a5dca43b04aed3bc01b61f",  // Assets/VketAssets/Assets/VketPrefabs/VketWebPageOpener/VketCirclePageOpener_2D.prefab
                "829918e636553bf489526e19e7c08a9f",  // Assets/VketAssets/Assets/VketPrefabs/VketWebPageOpener/VketCirclePageOpener_3D.prefab
                "249a82240095e1a44b9b4aae5f72d41e",  // Assets/VketAssets/Assets/VketPrefabs/VketWebPageOpener/VketItemPageOpener_2D.prefab
                "8b95eab6f59b5e64d9393292aca982ca",  // Assets/VketAssets/Assets/VketPrefabs/VketWebPageOpener/VketItemPageOpener_3D.prefab
            });
            return guids.ToArray();
        }

        public virtual string[] GetSizeIgnorePrefabGUIDs()
        {
            return new string[]
            {
                "d19ff96a19f6fdb4cb57095e22e5ba37",  // Assets/VketAssets/Assets/VketPrefabs/VketStringDownloader/VketStringDownloader.prefab
                "73b0727ab433c3140929fbf088cd8b88",  // Assets/VketAssets/Assets/VketPrefabs/VketVideoPlayer/VketVideoPlayer.prefab
                "b291170635bff9841bbd09d362a0d170",  // Assets/VketAssets/Assets/VketPrefabs/VketVideoPlayer/VketVideoUrlTrigger.prefab
            };
        }

        public virtual string[] GetUdonBehaviourGlobalLinkGUIDs()
        {
            return GetVketAssetsGUIDs().ToArray();
        }

        public virtual string[] GetAudioSourcePrefabGUIDs()
        {
            return new string[]
            {
            };
        }

        public virtual string[] GetCanvasPrefabGUIDs()
        {
            return new string[]
            {
                "3c0dbec26839f9b4ea24f9606ec62ce4",  // Assets/VketAssets/Assets/VketPrefabs/UITemplate/UI_Button.prefab
                "b4625b5c33c27804d889d16704b81c33",  // Assets/VketAssets/Assets/VketPrefabs/UITemplate/UI_Image.prefab
                "4dc5396d6e370ef4fa9b9e9458c3f735",  // Assets/VketAssets/Assets/VketPrefabs/UITemplate/UI_Text.prefab
                "333992c7f0890014d9a775e3f2303857",  // Assets/VketAssets/Assets/VketPrefabs/UITemplate/UI_TextMeshPro.prefab
            };
        }

        public virtual string[] GetPointLightProbeGUIDs()
        {
            return new string[]
            {
            };
        }

        public virtual string[] GetVRCSDKForbiddenPrefabGUIDs()
        {
            return new string[]
            {

                #region VRCSDK

                "be98467dc5d3c4eb1aeef22952913b0e",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/VRCCam.prefab
                "dce0dda226bd1f147a34f9b4660f5992",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/VRCProjectSettings.prefab
                "b14e1b78a061f8543b2f4120b5c369fa",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/VRCSDKAvatar.prefab
                "fc887d4eeb5a941ed86bca0135b05e2b",  // Packages/com.vrchat.base/Runtime/VRCSDK/Dependencies/VRChat/Resources/VRCSDKWorld.prefab

                "23dafc7d7a578b44f9cb37330ca2a156",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/AvatarPedestal.prefab
                "b1a39f599f0964049b1c3ba10835ddf9",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/SimplePenSystem.prefab
                "d25c8082618057240967336d52b56d3c",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/Udon Variable Sync.prefab
                "70279d83763c0d745a4e513a75053671",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VRCMirror.prefab
                // // VRC Visual Damage は使用許可コンポーネントに入っていない
                "00bd1d0a2cb96d845b0767189f49508d",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VRCPlayerVisualDamage.prefab
                "77a89e097657af54c85573a268691d5f",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VRCPortalMarker.prefab
                "8894fa7e4588a5c4fab98453e558847d",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VRCWorld.prefab
                "0d3d2df115d3d5147a07bd2e971a4443",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VideoPlayers/UdonSyncPlayer (AVPro).prefab
                "f2d01e7f26c5bb04f8c22c15fbf7475b",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VideoPlayers/UdonSyncPlayer (Unity).prefab
                // // 無害なので許可
                // "1dacfe29d81b51c46a85f97842455123",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VRCChair/VRCChair3.prefab
                // "d42d6e607dd21cf44945dc953c8aa1e3",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/Floor.prefab
                // "53370219a5e4a584f9c6395b208dfda3",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_billboard_matte.prefab
                // "c54c44a7b317f1349b5bbf3315981f3d",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_billboard_mixed.prefab
                // "05606a80052633b4c85dca01e934d390",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_billboard_shine.prefab
                // "706a6b0da0fe80a4080ffc5d4e3225e0",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_button_A_matte.prefab
                // "7658a1c7a33fb0f4b9a41f41dd825e3d",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_button_A_mixed.prefab
                // "ebf0301a541f0dd4886bbf3682912046",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_button_A_shine.prefab
                // "3486463dc6f1f3341a3708cd620f4811",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_button_B_matte.prefab
                // "b3dc6a315139c4a44bd3184523b641e5",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_button_B_mixed.prefab
                // "b33b62db28a33a14993177833ee91f41",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_button_B_shine.prefab
                // // 椅子は別のルールで個数制限されるのでPrefabとしては許可
                // "be555230638b05445b7a82c619f0bccf",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_chair_udon.prefab
                // "67fe5764aeb1bed479337d54d189d208",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_chair_udon_mixed.prefab
                // "ad5069971f2a1ea47b4db3525d965c91",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_chair_udon_shine.prefab
                // "382bdcf96025b7440af9c72a7e1b6872",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_cube_A_matte.prefab
                // "5cd93a74517fed64db2b6fce666760a4",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_cube_A_mixed.prefab
                // "fb7e5afc37161ed4ead2fdd070c9a537",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_cube_A_shine.prefab
                // "e6927a60d9835084594485b53371cdce",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_cube_B_matte.prefab
                // "2a41fcd39a5fb094fbdd414730ed7d9c",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_cube_B_mixed.prefab
                // "f70e27dc68a53cc4aa9513aa5e0468e8",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_cube_B_shine.prefab
                // // 名前に反して無害な普通のメッシュなので許可
                // "f3b2536f1de783f4182a88b6bd9e1645",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_pedestal_udon.prefab
                // "aaa719cd8b802744598805bb392fe605",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_pedestal_udon_mixed.prefab
                // "f8e6c0777affc3741b0e7db6ca23a036",  // Packages/com.vrchat.worlds/Samples/UdonExampleScene/SampleAssetsSet/Prefabs/VRC_pedestal_udon_shine.prefab

                #endregion

            };
        }

        public virtual string[] GetDisabledCallbak()
        {
            return new string[]
            {
                "_start",
                "_update",
                "_lateUpdate",
                "_fixedUpdate",
                "_postLateUpdate",
                "_onAnimatorMove",
                "_onCollisionStay",
                "_onRenderObject",
                "_onTriggerStay",
                "_onWillRenderObject",
                "_onPlayerJoined",
                "_onPlayerLeft",
                "_inputJump",
                "_inputUse",
                "_inputGrab",
                "_inputDrop",
                "_inputMoveVertical",
                "_inputMoveHorizontal",
                "_inputLookVertical",
                "_inputLookHorizontal",
                "_onPlayerRespawn",
                "_onPlayerTriggerStay",
                "_onPlayerCollisionStay"
            };
        }

        public virtual Dictionary<string, string> GetDisabledDirectives()
        {
            return new Dictionary<string, string>()
            {
                { "(#[ \t]*if)[ ,\n,\t]*", "#if" },
                { "(#[ \t]*elif)[ ,\n,\t]*", "#elif" },
            };
        }

        public virtual Dictionary<string, string> GetAllowedShaders()
        {
            return new Dictionary<string, string>();
        }

        // 2019.4.31f1 Unityビルトインシェーダー
        public virtual string[] GetDeniedShaderNames()
        {
            return new[]
            {
                "GUI/Text Shader",
                "Hidden/InternalClear",
                "Hidden/Internal-Colored",
                "Hidden/InternalErrorShader",
                "Hidden/FrameDebuggerRenderTargetDisplay",
                "Legacy Shaders/Transparent/Bumped Diffuse",
                "Legacy Shaders/Transparent/Bumped Specular",
                "Legacy Shaders/Transparent/Diffuse",
                "Legacy Shaders/Transparent/Specular",
                "Legacy Shaders/Transparent/Parallax Diffuse",
                "Legacy Shaders/Transparent/Parallax Specular",
                "Legacy Shaders/Transparent/VertexLit",
                "Legacy Shaders/Transparent/Cutout/Bumped Diffuse",
                "Legacy Shaders/Transparent/Cutout/Bumped Specular",
                "Legacy Shaders/Transparent/Cutout/Diffuse",
                "Legacy Shaders/Transparent/Cutout/Specular",
                "Legacy Shaders/Transparent/Cutout/Soft Edge Unlit",
                "Legacy Shaders/Transparent/Cutout/VertexLit",
                "AR/TangoARRender",
                "Autodesk Interactive",
                "Hidden/Compositing",
                "Hidden/CubeBlend",
                "Hidden/CubeBlur",
                "Hidden/CubeBlurOdd",
                "Hidden/CubeCopy",
                "Legacy Shaders/Decal",
                "FX/Flare",
                "Hidden/GIDebug/ShowLightMask",
                "Hidden/GIDebug/TextureUV",
                "Hidden/GIDebug/UV1sAsPositions",
                "Hidden/GIDebug/VertexColors",
                "Legacy Shaders/Self-Illumin/Bumped Diffuse",
                "Legacy Shaders/Self-Illumin/Bumped Specular",
                "Legacy Shaders/Self-Illumin/Diffuse",
                "Legacy Shaders/Self-Illumin/Specular",
                "Legacy Shaders/Self-Illumin/Parallax Diffuse",
                "Legacy Shaders/Self-Illumin/Parallax Specular",
                "Legacy Shaders/Self-Illumin/VertexLit",
                "Hidden/BlitCopy",
                "Hidden/BlitCopyDepth",
                "Hidden/BlitCopyWithDepth",
                "Hidden/BlitToDepth",
                "Hidden/BlitToDepth_MSAA",
                "Hidden/Internal-CombineDepthNormals",
                "Hidden/ConvertTexture",
                "Hidden/Internal-CubemapToEquirect",
                "Hidden/Internal-DeferredReflections",
                "Hidden/Internal-DeferredShading",
                "Hidden/Internal-DepthNormalsTexture",
                "Hidden/Internal-Flare",
                "Hidden/Internal-GUIRoundedRect",
                "Hidden/Internal-GUIRoundedRectWithColorPerBorder",
                "Hidden/Internal-GUITexture",
                "Hidden/Internal-GUITextureBlit",
                "Hidden/Internal-GUITextureClip",
                "Hidden/Internal-GUITextureClipText",
                "Hidden/Internal-Halo",
                "Hidden/Internal-MotionVectors",
                "Hidden/Internal-ODSWorldTexture",
                "Hidden/Internal-PrePassLighting",
                "Hidden/Internal-ScreenSpaceShadows",
                "Hidden/Internal-StencilWrite",
                "Hidden/Internal-UIRAtlasBlitCopy",
                "Hidden/Internal-UIRDefault",
                "Legacy Shaders/Lightmapped/Bumped Diffuse",
                "Legacy Shaders/Lightmapped/Bumped Specular",
                "Legacy Shaders/Lightmapped/Diffuse",
                "Legacy Shaders/Lightmapped/Specular",
                "Legacy Shaders/Lightmapped/VertexLit",
                "Mobile/Bumped Diffuse",
                "Mobile/Bumped Specular (1 Directional Realtime Light)",
                "Mobile/Bumped Specular",
                "Mobile/Diffuse",
                "Mobile/Unlit (Supports Lightmap)",
                "Mobile/Particles/Additive",
                "Mobile/Particles/VertexLit Blended",
                "Mobile/Particles/Alpha Blended",
                "Mobile/Particles/Multiply",
                "Mobile/Skybox",
                "Mobile/VertexLit (Only Directional Lights)",
                "Mobile/VertexLit",
                "Nature/Tree Soft Occlusion Bark",
                "Hidden/Nature/Tree Soft Occlusion Bark Rendertex",
                "Nature/Tree Soft Occlusion Leaves",
                "Hidden/Nature/Tree Soft Occlusion Leaves Rendertex",
                "Nature/SpeedTree",
                "Nature/SpeedTree8",
                "Nature/SpeedTree Billboard",
                "Hidden/Nature/Tree Creator Albedo Rendertex",
                "Nature/Tree Creator Bark",
                "Hidden/Nature/Tree Creator Bark Optimized",
                "Hidden/Nature/Tree Creator Bark Rendertex",
                "Nature/Tree Creator Leaves",
                "Nature/Tree Creator Leaves Fast",
                "Hidden/Nature/Tree Creator Leaves Fast Optimized",
                "Hidden/Nature/Tree Creator Leaves Optimized",
                "Hidden/Nature/Tree Creator Leaves Rendertex",
                "Hidden/Nature/Tree Creator Normal Rendertex",
                "Legacy Shaders/Bumped Diffuse",
                "Legacy Shaders/Bumped Specular",
                "Legacy Shaders/Diffuse",
                "Legacy Shaders/Diffuse Detail",
                "Legacy Shaders/Diffuse Fast",
                "Legacy Shaders/Specular",
                "Legacy Shaders/Parallax Diffuse",
                "Legacy Shaders/Parallax Specular",
                "Legacy Shaders/VertexLit",
                "Legacy Shaders/Particles/Additive",
                "Legacy Shaders/Particles/~Additive-Multiply",
                "Legacy Shaders/Particles/Additive (Soft)",
                "Legacy Shaders/Particles/Alpha Blended",
                "Legacy Shaders/Particles/Anim Alpha Blended",
                "Legacy Shaders/Particles/Blend",
                "Legacy Shaders/Particles/Multiply",
                "Legacy Shaders/Particles/Multiply (Double)",
                "Legacy Shaders/Particles/Alpha Blended Premultiply",
                "Particles/Standard Surface",
                "Particles/Standard Unlit",
                "Legacy Shaders/Particles/VertexLit Blended",
                "Legacy Shaders/Reflective/Bumped Diffuse",
                "Legacy Shaders/Reflective/Bumped Unlit",
                "Legacy Shaders/Reflective/Bumped Specular",
                "Legacy Shaders/Reflective/Bumped VertexLit",
                "Legacy Shaders/Reflective/Diffuse",
                "Legacy Shaders/Reflective/Specular",
                "Legacy Shaders/Reflective/Parallax Diffuse",
                "Legacy Shaders/Reflective/Parallax Specular",
                "Legacy Shaders/Reflective/VertexLit",
                "Skybox/Cubemap",
                "Skybox/Panoramic",
                "Skybox/Procedural",
                "Skybox/6 Sided",
                "Sprites/Default",
                "Sprites/Diffuse",
                "Sprites/Mask",
                "Standard",
                "Standard (Specular setup)",
                "Hidden/TerrainEngine/Details/Vertexlit",
                "Hidden/TerrainEngine/Details/WavingDoublePass",
                "Hidden/TerrainEngine/Details/BillboardWavingDoublePass",
                "Hidden/TerrainEngine/Splatmap/Diffuse-AddPass",
                "Hidden/TerrainEngine/Splatmap/Diffuse-Base",
                "Hidden/TerrainEngine/Splatmap/Diffuse-BaseGen",
                "Nature/Terrain/Diffuse",
                "Hidden/TerrainEngine/Splatmap/Specular-AddPass",
                "Hidden/TerrainEngine/Splatmap/Specular-Base",
                "Nature/Terrain/Specular",
                "Hidden/TerrainEngine/Splatmap/Standard-AddPass",
                "Hidden/TerrainEngine/Splatmap/Standard-Base",
                "Hidden/TerrainEngine/Splatmap/Standard-BaseGen",
                "Nature/Terrain/Standard",
                "Hidden/Nature/Terrain/Utilities",
                "Hidden/TerrainEngine/BillboardTree",
                "Hidden/TerrainEngine/CameraFacingBillboardTree",
                "Hidden/TerrainEngine/BrushPreview",
                "Hidden/TerrainEngine/CrossBlendNeighbors",
                "Hidden/TerrainEngine/GenerateNormalmap",
                "Hidden/TerrainEngine/PaintHeight",
                "Hidden/TerrainEngine/HeightBlitCopy",
                "Hidden/TerrainEngine/TerrainLayerUtils",
                "Hidden/TextCore/Distance Field SSD",
                "Hidden/TextCore/Distance Field",
                "Hidden/UI/CompositeOverdraw",
                "UI/Default",
                "UI/DefaultETC1",
                "UI/Default Font",
                "UI/Lit/Bumped",
                "UI/Lit/Detail",
                "UI/Lit/Refraction",
                "UI/Lit/Refraction Detail",
                "UI/Lit/Transparent",
                "Hidden/UI/Overdraw",
                "UI/Unlit/Detail",
                "UI/Unlit/Text",
                "UI/Unlit/Text Detail",
                "UI/Unlit/Transparent",
                "Unlit/Transparent",
                "Unlit/Transparent Cutout",
                "Unlit/Color",
                "Unlit/Texture",
                "Hidden/VideoComposite",
                "Hidden/VideoDecode",
                "Hidden/VideoDecodeAndroid",
                "Hidden/VideoDecodeML",
                "Hidden/VideoDecodeOSX",
                "Hidden/VR/BlitFromTex2DToTexArraySlice",
                "Hidden/VR/BlitTexArraySlice",
                "Hidden/VR/BlitTexArraySliceToDepth",
                "Hidden/VR/BlitTexArraySliceToDepth_MSAA",
                "Hidden/VR/Internal-VRDistortion",
                "VR/SpatialMapping/Occlusion",
                "VR/SpatialMapping/Wireframe",
            };
        }

        public string[] GUIDs => GetGUIDs();
        public string[] MaterialGUIDs => GetMaterialGUIDs();
        public string[] PickupObjectSyncPrefabGUIDs => GetPickupObjectSyncPrefabGUIDs();
        public string[] AvatarPedestalPrefabGUIDs => GetAvatarPedestalPrefabGUIDs();
        public string[] VideoPlayerPrefabGUIDs => GetVideoPlayerPrefabGUIDs();
        public string[] ImageDownloaderPrefabGUIDs => GetImageDownloaderPrefabGUIDs();
        public string[] StringDownloaderPrefabGUIDs => GetStringDownloaderPrefabGUIDs();
        public string[] StarshipTreasurePrefabGUIDs => GetStarshipTreasurePrefabGUIDs();
        public string[] UdonBehaviourPrefabGUIDs => GetUdonBehaviourPrefabGUIDs();
        public string[] UdonBehaviourGlobalLinkGUIDs => GetUdonBehaviourGlobalLinkGUIDs();
        public string[] SizeIgnorePrefabGUIDs => GetSizeIgnorePrefabGUIDs();
        public string[] AudioSourcePrefabGUIDs => GetAudioSourcePrefabGUIDs();
        public string[] CanvasPrefabGUIDs => GetCanvasPrefabGUIDs();
        public string[] PointLightProbeGUIDs => GetPointLightProbeGUIDs();
        public string[] VRCSDKForbiddenPrefabGUIDs => GetVRCSDKForbiddenPrefabGUIDs();
        public string[] DisabledCallback => GetDisabledCallbak();
        public Dictionary<string, string> DisabledDirectives => GetDisabledDirectives();
        public Dictionary<string, string> AllowedShaders => GetAllowedShaders();
        public string[] DeniedShaderNames => GetDeniedShaderNames();
    }
}