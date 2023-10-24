//--------------------------------------------------------------
//              Sunao Shader Function
//                      Copyright (c) 2022 揚茄子研究所
//--------------------------------------------------------------

//-------------------------------------スケール取得
inline float3 GetScale(float ratio , bool fixscale) {

	float3 Scale;
	       Scale.x  = length(float3(unity_ObjectToWorld[0].x , unity_ObjectToWorld[1].x , unity_ObjectToWorld[2].x));
	       Scale.y  = length(float3(unity_ObjectToWorld[0].y , unity_ObjectToWorld[1].y , unity_ObjectToWorld[2].y));
	       Scale.z  = length(float3(unity_ObjectToWorld[0].z , unity_ObjectToWorld[1].z , unity_ObjectToWorld[2].z));
	       Scale    = 0.01f / Scale;
	       Scale   *= ratio;
	if (fixscale) Scale *= 10.0f;

	return Scale;
}

//-------------------------------------モノクロカラーに変換
float  MonoColor(float3 col) {
	return (0.2126f * col.r) + (0.7152f * col.g) + (0.0722f * col.b); //BT.709
}

//-------------------------------------SHライトの方向を取得
inline float3 SHLightDirection(float len[6]) {
	return normalize(float3(len[1] - len[0] , len[3] - len[2] , len[5] - len[4]));
}

//-------------------------------------明るいSHライトを取得
inline float3 SHLightMax(float3 col[6]) {
	float3 ocol;
	ocol =            col[0];
	ocol = max(ocol , col[1]);
	ocol = max(ocol , col[2]);
	ocol = max(ocol , col[3]);
	ocol = max(ocol , col[4]);
	ocol = max(ocol , col[5]);

	return ocol;
}

//-------------------------------------暗いSHライトを取得
inline float3 SHLightMin(float3 col[6]) {
	float3 ocol;
	ocol  = col[0] + col[1] + col[2] + col[3] + col[4] + col[5];
	ocol *= 0.166667f;	// 0.166667 = 1/6

	float zcol;
	zcol  = MonoColor(abs(col[1] - col[0]) + abs(col[3] - col[2]) + abs(col[5] - col[4]));
	zcol  = saturate(zcol);

	ocol  = lerp(ocol * 0.75f , ocol , zcol);

	return ocol;
}

//-------------------------------------頂点ライトの距離を計算
inline float4 VLightLength(float4 x , float4 y , float4 z) {
	return max((x * x) + (y * y) + (z * z) , 0.000001f);
}

//-------------------------------------頂点ライトの減衰を計算
inline float4 VLightAtten(float4 len) {
	float4 atten;
	atten = 1.0f / (1.0f + len * unity_4LightAtten0);
	atten = pow(atten , max(0.075f / atten , 1.0f));

	return atten;
}

//-------------------------------------トゥーンパラメータの計算
inline float4 Toon(uint toon , float gradient) {
	float4 otoon;
	otoon.x  = max(float(11 - toon) , 2.0f);
	otoon.y  = 1.0f / (otoon.x - 1.0f);
	otoon.z  = 0.5f /  otoon.x;
	otoon.w  = pow(1.0f + (gradient * gradient * gradient) , 10.0f);

	return otoon;
}

//-------------------------------------2つのUVトランスフォームを混合
inline float2 MixingTransformTex(float2 uv , float4 st0 , float4 st1) {
	return (uv * st0.xy * st1.xy) + st0.zw  + st1.zw;
}

//-------------------------------------エミッション時間変化パラメータの計算
inline float2 EmissionWave(uint mode , float blink , float freq , float offset) {
	float wave = 0.0f;

	if (mode == 0) wave = (1.0f - (blink * 0.5f)) + cos((_Time.y * freq + offset) * 6.283185f) * blink * 0.5f; // 6.283185 = 2π
	if (mode == 1) wave = (1.0f - blink) + (frac(_Time.y * freq + offset) * blink);
	if (mode == 2) wave = (1.0f - blink) + (1.0f - frac(_Time.y * freq + offset) * blink);
	if (mode == 3) wave = (1.0f - blink) + (step(0.5f , frac(_Time.y * freq + offset)) * blink);

	return wave;
}

//-------------------------------------ディフューズシェーディングの計算
inline float  DiffuseCalc(float3 normal , float3 ldir , float gradient , float width) {
	float Diffuse;
	Diffuse = ((dot(normal , ldir) - 0.5f) * (gradient + 0.000001f)) + 1.5f - width;

	return saturate(Diffuse);
}

//-------------------------------------トゥーンシェーディングの計算
inline float  ToonCalc(float diffuse , float4 toon) {

	float Diffuse;
	float Gradient;

	diffuse  = max(diffuse , 0.000001f);
	Gradient = frac((diffuse + toon.z - 0.0000001f) * toon.x) - 0.5f;
	Gradient = saturate(Gradient * toon.w + 0.5f) + 0.5f;
	Gradient = (frac(Gradient) - 0.5f) * toon.y;
	Diffuse  = floor(diffuse * toon.x) * toon.y + Gradient;

	return saturate(Diffuse);
}

//-------------------------------------ライトの計算
inline float3 LightingCalc(float3 light , float diffuse , float3 shadecol , float shademask) {
	float3 ocol;
	ocol = lerp(light * shadecol , light, diffuse  );
	ocol = lerp(light            , ocol  , shademask);

	return ocol;
}

//-------------------------------------スペキュラ反射の計算
inline float3 SpecularCalc(float3 normal , float3 ldir , float3 view , float scale) {
	float3 hv = normalize(ldir  + view);
	float  specular;
	specular = pow(saturate(dot(hv , normal)) , (1.0f / (1.005f - scale))) * (scale * scale * scale + 0.5f);
	specular = saturate(specular * specular * specular);

	return specular;
}

//-------------------------------------環境マッピングの計算
inline float3 ReflectionCalc(float3 wpos , float3 normal , float3 view , float scale) {
	float3 dir   = reflect(-view , normal);
	float3 ocol;
	float3 refl0;
	float3 refl1;

	#if UNITY_SPECCUBE_BOX_PROJECTION
		if (unity_SpecCube0_ProbePosition.w > 0) {
			float3 rbox   = ((dir > 0 ? unity_SpecCube0_BoxMax.xyz : unity_SpecCube0_BoxMin.xyz) - wpos) / dir;
			float  rsize  = min(min(rbox.x, rbox.y), rbox.z);
			       dir    = dir * rsize + (wpos - unity_SpecCube0_ProbePosition);
		}
	#endif

	refl0 = DecodeHDR(UNITY_SAMPLE_TEXCUBE_LOD        (unity_SpecCube0                  , dir, (1.0f - scale) * 7.0f) , unity_SpecCube0_HDR);
	refl1 = DecodeHDR(UNITY_SAMPLE_TEXCUBE_SAMPLER_LOD(unity_SpecCube1, unity_SpecCube0 , dir, (1.0f - scale) * 7.0f) , unity_SpecCube1_HDR);
	ocol  = lerp(refl1 , refl0 , unity_SpecCube0_BoxMin.w);
	
	return ocol;
}

//-------------------------------------リムライトの計算
inline float  RimLightCalc(float3 normal , float3 view , float power , float gradient) {
	float orim;
	orim  = saturate(1.0f - abs(dot(view , normal)));
	orim *= orim;
	orim  = saturate(((orim - 0.5f) * gradient * gradient * gradient) + 0.5f);
	orim  = saturate(orim + ((power * 0.5f) - 0.5f) * 2.0f);

	return orim;
}

#ifdef FUR
//-------------------------------------2頂点の補完
inline VOUT   Fur_Interpolation(VOUT v1 , VOUT v2) {

	VOUT   o             = v1;
	       o.vertex.xyz  = (v1.vertex.xyz + v2.vertex.xyz) * 0.5f;
	       o.normal      = normalize(v1.normal + v2.normal);
	       o.uv          = (v1.uv         + v2.uv        ) * 0.5f;
	       o.pos         = UnityObjectToClipPos(o.vertex);
	       o.wpos        = mul(unity_ObjectToWorld , o.vertex).xyz;

	return o;
}

//-------------------------------------ファーの根本生成
inline VOUT   Fur_GenerateRoot(VOUT v , float uvx) {

	VOUT   o             = v;
	       o.furuv       = float2(uvx , 0.0f);

	return o;
}

//-------------------------------------ファーの生成
inline VOUT   Fur_Generate(VOUT v , float len , float uvx , float roughness , float gravity) {

	VOUT   o             = v;
	       o.furuv       = float2(uvx , 1.0f);

	float3 VertexAdd     = o.normal * MonoColor(tex2Dlod(_FurMask , float4(o.uv.xy , 0.0f , 0.0f)).rgb) * len;
	       VertexAdd    *= lerp(1.0f , frac(dot(o.uv , float2(19.0f , 25.0f))) + 0.5f , roughness);
	       o.vertex.xyz += VertexAdd;
	       o.vertex.xyz  = mul(unity_ObjectToWorld , o.vertex).xyz;
	       o.vertex.y   -= gravity * len;
	       o.vertex.xyz  = mul(unity_WorldToObject , o.vertex).xyz;

	       o.pos         = UnityObjectToClipPos(o.vertex);
	       o.wpos        = mul(unity_ObjectToWorld , o.vertex).xyz;

	return o;
}
#endif
