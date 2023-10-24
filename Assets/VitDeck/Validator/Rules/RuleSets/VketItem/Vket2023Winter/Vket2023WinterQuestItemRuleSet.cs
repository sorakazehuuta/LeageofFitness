#if VRC_SDK_VRCSDK3
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VitDeck.Language;

namespace VitDeck.Validator.RuleSets
{
    public class Vket2023WinterQuestItemRuleSet : VketItemRuleSetBase
    {
        public Vket2023WinterQuestItemRuleSet() : base(new VketItemOfficialAssetData())
        {
        }
        public override string RuleSetName => "Vket2023Winter - Item - Quest";
        
        protected override long FolderSizeLimit => 20 * MegaByte;

        protected override Vector3 BoothSizeLimit => new Vector3(1, 2, 1);

        protected override int MaterialUsesLimit => 0;

        public override IRule[] GetRules()
        {
            var rules = base.GetRules().ToList();
            rules.Add(new ShaderWhitelistRule_3(LocalizedMessage.Get("Booth.ShaderWhiteListRule.Title"), GetAllowedShaders2(), GetAllowedShaders()));
            return rules.ToArray();
        }
        
        public string[] GetAllowedShaders()
            => new string[] { "VRChat/Mobile/", "VRChat/Panosphere", "VRChat/Sprites/Default", "TextMeshPro/Mobile/Distance Field", "Noriben/DepthWater", "Noriben/noribenQuestWaterCubemap" };
        
        public virtual Dictionary<string, string> GetAllowedShaders2()
            => new Dictionary<string, string>() {
{"MMS3/Mnmrshader3","8dd7c14dadb834c4e8324f7d08c5674e"},
{"MMS3/Mnmrshader3_Cutout","128f4720891e8914ab7e6673099df0f0"},
{"MMS3/Mnmrshader3_Outline","fbaec084851cef64fbd877b3b15716cb"},
{"MMS3/Stencil/MMS3_Reader ","f889d00a055a0488e9ecbf22c558ae76"},
{"MMS3/Stencil/MMS3_Writer","f55508f2ed8cc477f9574099971bc4eb"},
{"MMS3/Mnmrshader3_Transparent","fda424b70f79d4e5488e1cc3ee100a95"},
{"UnityChanToonShader/Mobile/Toon_DoubleShadeWithFeather","1d10c7840eb6ba74c889a27f14ba6081"},
{"UnityChanToonShader/Mobile/Toon_DoubleShadeWithFeather_Clipping","88791c14394118d42a5e176b433af322"},
{"UnityChanToonShader/Mobile/Toon_DoubleShadeWithFeather_Clipping_StencilMask","41f4ee183cb66ad40bc74a9f8f944974"},
{"UnityChanToonShader/Mobile/Toon_DoubleShadeWithFeather_StencilMask","dec01cbdbc5b8da4ca8671815cda1557"},
{"UnityChanToonShader/Mobile/Toon_DoubleShadeWithFeather_StencilOut","55e8b9eeaaff205469365133fe7bc744"},
{"UnityChanToonShader/Mobile/Toon_DoubleShadeWithFeather_TransClipping","d4c592285a93c3844aafdaafffc07ec7"},
{"UnityChanToonShader/Mobile/Toon_DoubleShadeWithFeather_TransClipping_StencilMask","100d373b596f44d49ac9bb944d671d32"},
{"UnityChanToonShader/Mobile/AngelRing/Toon_ShadingGradeMap","23e399973d807464fb195291a44a614c"},
{"UnityChanToonShader/Mobile/AngelRing/Toon_ShadingGradeMap_StencilOut","8d33e4e4084e5af449f3e762fecce3c9"},
{"UnityChanToonShader/Mobile/Toon_ShadingGradeMap","f90e11a40dcf4f745ae6b21b857943fa"},
{"UnityChanToonShader/Mobile/Toon_ShadingGradeMap_StencilMask","206c554c8b0c60041a9d242385f543d3"},
{"UnityChanToonShader/Mobile/Toon_ShadingGradeMap_StencilOut","cfc201757f2519c4bb6ef9265a046582"},
{"UnityChanToonShader/Mobile/Toon_ShadingGradeMap_TransClipping","cce1da34c52aff745adf0222f56a356c"},
{"UnityChanToonShader/Mobile/Toon_ShadingGradeMap_TransClipping_StencilMask","e88039bab21b7894e918126e8fce5d1b"},
{"UnlitWF/Debug/WF_DebugView","c7e5995223250464cac205689e058693"},
{"UnlitWF/WF_FakeFur_FurOnly_Mix","aab16a18215b5e2488031de410fda510"},
{"UnlitWF/WF_FakeFur_FurOnly_TransCutout","152ebc8c9a60c0e43820c71a0f0dfbb6"},
{"UnlitWF/WF_FakeFur_FurOnly_Transparent","a3777cac8e82d944bba542a87aa5368c"},
{"UnlitWF/WF_FakeFur_Mix","f19f31eb9bfd5154ba2c1a41c54a908b"},
{"UnlitWF/WF_FakeFur_TransCutout","58bb80b63bec29d4384e105c53ca6970"},
{"UnlitWF/WF_FakeFur_Transparent","2210f95a2274e9d4faf8a14dac933fdb"},
{"UnlitWF/WF_Gem_Opaque","c0f75d3ed420fd144a74722588d3bc21"},
{"UnlitWF/WF_Gem_Transparent","21f6eaa1dd1f25c4cb29a42c4ff5d98f"},
{"UnlitWF/Custom/WF_UnToon_Custom_ClearCoat_Addition","ec023985bf0f3f44bb6fe6bf962c2c69"},
{"UnlitWF/Custom/WF_UnToon_Custom_ClearCoat_Opaque","2dafe14658b486b4bb44869606d30d67"},
{"UnlitWF/Custom/WF_UnToon_Custom_ClearCoat_TransCutout","46e58649f5c3de74a9f578af4e6d30b5"},
{"UnlitWF/Custom/WF_UnToon_Custom_ClearCoat_Transparent","ccf60bd2b8af3e94ebc1a310962dac61"},
{"UnlitWF/Custom/WF_UnToon_Custom_GhostOpaque","e4b04fed7c539d84887123c0beeffaca"},
{"UnlitWF/Custom/WF_UnToon_Custom_GhostTransparent","4ba701b07ccc81e4aae7f053bf332eab"},
{"UnlitWF/Custom/WF_UnToon_Custom_LameOnly_Transparent","f3f80636c64e389498b3b19e2ee218da"},
{"UnlitWF/Custom/WF_UnToon_Custom_MirrorControl_Opaque","f5d5b7934ab1854418baf5042b0f7453"},
{"UnlitWF/Custom/WF_UnToon_Custom_OffsetOutline_Opaque","90cac9ec3b2a7524eb99b36ab87f25f1"},
{"UnlitWF/Custom/WF_UnToon_Custom_PowerCap_Outline_Opaque","871fd7a51a8ea3e4980c3fe7b8347619"},
{"UnlitWF/Custom/WF_UnToon_Custom_Tess_PowerCap_Opaque","58ccf9c912b226146a25726b8a1f04db"},
{"UnlitWF/Custom/WF_UnToon_Custom_Transparent_FrostedGlass","28e73745c5d1e464a876038f875501b4"},
{"UnlitWF/Custom/WF_UnToon_Custom_Transparent_Refracted","45e1aee78764bec499e99106c330f95a"},
{"Hidden/UnlitWF/WF_UnToon_Hidden","af51615040dcdad4cb01c29ea34dbb03"},
{"UnlitWF/UnToon_Mobile/WF_UnToon_Mobile_Opaque","4bd76f6599a5b8e4d88d81300fb74c37"},
{"UnlitWF/UnToon_Mobile/WF_UnToon_Mobile_OutlineOnly_Opaque","d279a88eda1ae0e4c89e92539639eb16"},
{"UnlitWF/UnToon_Mobile/WF_UnToon_Mobile_OutlineOnly_TransCutout","e0b93fdad2eeedf42baccbc0975cdd1d"},
{"UnlitWF/UnToon_Mobile/WF_UnToon_Mobile_Outline_Opaque","98bb3de5d5444094aa041a65f8a85708"},
{"UnlitWF/UnToon_Mobile/WF_UnToon_Mobile_Outline_TransCutout","3276a740671679f44b1141523bf73e33"},
{"UnlitWF/UnToon_Mobile/WF_UnToon_Mobile_TransCutout","af3422dc9372a89449a9f44d409d9714"},
{"UnlitWF/UnToon_Mobile/WF_UnToon_Mobile_Transparent","0a7a6cdca16a38548a5d81aca8d4e3ba"},
{"UnlitWF/UnToon_Mobile/WF_UnToon_Mobile_TransparentOverlay","4e4be4aab63a2bd4fbcea2390ae92fdf"},
{"UnlitWF/WF_UnToon_Opaque","a3678756e883b9349ac22fce33313139"},
{"UnlitWF/UnToon_Outline/WF_UnToon_OutlineOnly_Opaque","4eef00f52cc21b04e9e34e4caefa6bbf"},
{"UnlitWF/UnToon_Outline/WF_UnToon_OutlineOnly_TransCutout","64bf3ca653a7b274fab3e8a87016bfb0"},
{"UnlitWF/UnToon_Outline/WF_UnToon_OutlineOnly_Transparent","660abd485057f4740ac9050f7ab3237d"},
{"UnlitWF/UnToon_Outline/WF_UnToon_OutlineOnly_Transparent_MaskOut","3c07b964e541eef45bc195a029b878b3"},
{"UnlitWF/UnToon_Outline/WF_UnToon_Outline_Opaque","a5ae7f40ac53e274ea0bc1262e1f6895"},
{"UnlitWF/UnToon_Outline/WF_UnToon_Outline_TransCutout","ab4eb87c406a22f46887cf72178e2685"},
{"UnlitWF/UnToon_Outline/WF_UnToon_Outline_Transparent","5523e041d29d259439fa14bd131f5c82"},
{"UnlitWF/UnToon_Outline/WF_UnToon_Outline_Transparent3Pass","5498b01615002d948bea7542f55e0c07"},
{"UnlitWF/UnToon_Outline/WF_UnToon_Outline_Transparent_MaskOut","9350854c6e88f3f4eb873d2f94ff3328"},
{"UnlitWF/UnToon_Outline/WF_UnToon_Outline_Transparent_MaskOut_Blend","ad88000744b4fb241835ba6ec106caf4"},
{"UnlitWF/UnToon_PowerCap/WF_UnToon_PowerCap_Opaque","0733cfc88032e8d4eafce250263c497c"},
{"UnlitWF/UnToon_PowerCap/WF_UnToon_PowerCap_TransCutout","2cf66b0706c40744baab089297afa895"},
{"UnlitWF/UnToon_PowerCap/WF_UnToon_PowerCap_Transparent","747bf283d686334469fb662b2fc4a5c2"},
{"UnlitWF/UnToon_PowerCap/WF_UnToon_PowerCap_Transparent3Pass","d242cb83664caae4f957030870dd801d"},
{"UnlitWF/UnToon_Tessellation/WF_UnToon_Tess_Opaque","dd3a683002b3a6f43bdb6c97bd0985c1"},
{"UnlitWF/UnToon_Tessellation/WF_UnToon_Tess_TransCutout","94ee7f8988740fd4887f8b1ce41f0c1c"},
{"UnlitWF/UnToon_Tessellation/WF_UnToon_Tess_Transparent","3bde56820d1aece41bd22966876a061c"},
{"UnlitWF/UnToon_Tessellation/WF_UnToon_Tess_Transparent3Pass","78d2e3fa0b8eb674aa9cf9e048f79c93"},
{"UnlitWF/WF_UnToon_TransCutout","8c7888a4ac175584f81e0b6e7d4af5a7"},
{"UnlitWF/WF_UnToon_Transparent","15212414cba0c7a4aac92d94a4ae8750"},
{"UnlitWF/WF_UnToon_Transparent3Pass","d1e7b0a18e221a1409ad59065ec157e4"},
{"UnlitWF/WF_UnToon_Transparent_Mask","2efe527cfcbf0e1408b67463225f552f"},
{"UnlitWF/WF_UnToon_Transparent_MaskOut","0b53cf0bcd0f9db4fa9d1297d255d06d"},
{"UnlitWF/WF_UnToon_Transparent_MaskOut_Blend","d01a5c313ada49e488b2ef8c6b00f56d"},
{"UnlitWF/UnToon_TriShade/WF_UnToon_TriShade_Opaque","a220e3e0675cc3f4f817a485688788d6"},
{"UnlitWF/UnToon_TriShade/WF_UnToon_TriShade_TransCutout","2d294f328149d944eb0899b452ff879c"},
{"UnlitWF/UnToon_TriShade/WF_UnToon_TriShade_Transparent","1435581bcf13e7a47b5bf5636f8d8252"},
{"UnlitWF/UnToon_TriShade/WF_UnToon_TriShade_Transparent3Pass","e7263331a8ee0a04aa4a271fc1fef104"},
{"UnlitWF/UnToon_TriShade/WF_UnToon_TriShade_Transparent_Mask","0299954f2a9b0994f8c9587945948766"},
{"UnlitWF/UnToon_TriShade/WF_UnToon_TriShade_Transparent_MaskOut","06e9294a93df4474cac2f4157b5e1d1d"},
{"UnlitWF/UnToon_TriShade/WF_UnToon_TriShade_Transparent_MaskOut_Blend","dfb821bc7afadc14591e4338a8ec865f"},
{"UniGLTF/UniUnlit","8c17b56f4bf084c47872edcb95237e4a"},
{"VRM/MToon","1a97144e4ad27a04aafd70f7b915cedb"},
            };
    }
}
#endif