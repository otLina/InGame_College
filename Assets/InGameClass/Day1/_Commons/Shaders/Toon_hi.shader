Shader "Grenge/PJ_colorful/Toon_hi"
{
	Properties
	{
		_ChangeBaseColor ("ChangeBaseColor", Color) = (0, 0, 0, 0)
		_ChangeShadowColor ("ChangeShadowColor", Color) = (0, 0, 0, 0)

		_MainTexture ("MainTexture", 2D) = "white" {}
		_tileID_BaseColor ("tileID_BaseColor", Float) = 2
		_tileID_1stShadow ("tileID_1stShadow", Float) = 3
		_MaskTexture ("MaskTexture", 2D) = "white" {}
		_tileID_Hilight ("tileID_Hilight", Float) = 2
		_tileID_Glow ("tileID_Glow", Float) = 3
		[HideInInspector]_2nd_ShadeColor ("2nd_ShadeColor", Color) = (0.4980392, 0.4980392, 0.4980392, 1)
		[HideInInspector][MaterialToggle] _Is_SystemShadows ("Is_SystemShadows", Float) = 0.5
		[HideInInspector]_Tweak_SystemShadows ("Tweak_SystemShadows", Range(0, 1)) = 1
		[HideInInspector]_Mix_BaseTexture ("Mix_BaseTexture", Range(0.001, 0.999)) = 0.001
		[HideInInspector]_Mix_ShadowTexture ("Mix_ShadowTexture", Range(0.001, 0.999)) = 0.001
		_Shadow1st ("Shadow1st", Range(0, 1)) = 0
		[HideInInspector]_Shadow2nd ("Shadow2nd", Range(0, 1)) = 0
		[MaterialToggle] _Is_Normal ("Is_Normal", Float) = 0
		_NormalMap ("NormalMap", 2D) = "bump" {}
		_OutlineSampler ("OutlineSampler", 2D) = "white" {}
		[HideInInspector][MaterialToggle] _Is_BlendBaseColor ("Is_BlendBaseColor", Float) = 0.4980392
		_Outline_Width ("Outline_Width", Float) = 0
		_Outline_Color ("Outline_Color", Color) = (0.4980392, 0.4980392, 0.4980392, 1)
		[HideInInspector]_Farthest_Distance ("Farthest_Distance", Float) = 50
		[HideInInspector]_Nearest_Distance ("Nearest_Distance", Float) = 0
		[HideInInspector][MaterialToggle] _Is_OutlineGlow ("Is_OutlineGlow", Float) = 0
		[HideInInspector]_OutlineGlow_Color ("OutlineGlow_Color", Color) = (0.5, 0.5, 0.5, 1)
		[MaterialToggle] _Is_Glow ("Is_Glow", Float) = 0
		_Glow_Color ("Glow_Color", Color) = (0.4980392, 0.4980392, 0.4980392, 1)
		[MaterialToggle] _Is_Hilight ("Is_Hilight", Float) = 0
		_Hilight_Color ("Hilight_Color", Color) = (1, 1, 1, 1)
		_Tweak_Hilight("Tweak_Hilight", Range(0.0, 1.0)) = 0.3
		_Stencil ("Stencil ID(Outline)", Float) = 192

		[Space(15)]
		_Brightness ("Brightness", Range(0.0, 1.0)) = 1.0

		[Space(15)]
		_StencilRef ("Stencil Ref", int) = 255
		_StencilReadMask("Stencil Read Mask", int) = 255
		_StencilWriteMask("Stencil Write Mask", int) = 255

		[Enum(UnityEngine.Rendering.CompareFunction)]
		_StencilTest("Stencil Test", int) = 0
		[Enum(UnityEngine.Rendering.StencilOp)]
		_StencilOperatorPass("StencilOperator Pass", int) = 0
		[Enum(UnityEngine.Rendering.StencilOp)]
		_StencilOperatorFail("StencilOperator Fail", int) = 0
		[Enum(UnityEngine.Rendering.StencilOp)]
		_StencilOperatorZFail("StencilOperator ZFail", int) = 0
	}
	SubShader
	{
		Tags
		{
			"Queue"="Geometry-1"
			"RenderType"="Opaque"
		}
		Stencil
		{
			Ref [_Stencil]
			WriteMask 255
			Pass Replace
		}
		Pass
		{
			Name "FORWARD"
			Tags
			{
				"LightMode"="ForwardBase"
			}
			Stencil
			{
				Ref			[_StencilRef]
				ReadMask	[_StencilReadMask]
				WriteMask	[_StencilWriteMask]
				Comp	[_StencilTest]
				Pass	[_StencilOperatorPass]
				Fail	[_StencilOperatorFail]
				ZFail	[_StencilOperatorZFail]
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#define UNITY_PASS_FORWARDBASE
			#define _GLOSSYENV 1
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			#include "UnityStandardBRDF.cginc"
			#pragma multi_compile_fwdbase_fullshadows
			#pragma multi_compile_fog
			#pragma target 3.0
			#include "ToonCommon.cginc"
			struct VertexInput
			{
				float4 vertex		: POSITION;
				float3 normal		: NORMAL;
				float4 tangent		: TANGENT;
				float2 texcoord0	: TEXCOORD0;
			};
			struct VertexOutput
			{
				float4 pos			: SV_POSITION;
				float2 uv0			: TEXCOORD0;
				float4 posWorld		: TEXCOORD1;
				float3 normalDir	: TEXCOORD2;
				float3 tangentDir	: TEXCOORD3;
				float3 bitangentDir	: TEXCOORD4;
				LIGHTING_COORDS(5,6)
				UNITY_FOG_COORDS(7)
			};
			VertexOutput vert (VertexInput v)
			{
				VertexOutput o = (VertexOutput)0;
				o.uv0			= v.texcoord0;
				o.normalDir		= UnityObjectToWorldNormal(v.normal);
				o.tangentDir	= normalize(mul(unity_ObjectToWorld, float4(v.tangent.xyz, 0.0)).xyz);
				o.bitangentDir	= normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
				o.posWorld		= mul(unity_ObjectToWorld, v.vertex);
				o.pos			= UnityObjectToClipPos(v.vertex);
				UNITY_TRANSFER_FOG(o,o.pos);
				TRANSFER_VERTEX_TO_FRAGMENT(o)
				return o;
			}
			float4 frag(VertexOutput i) : COLOR
			{
				float3 NormalMap = UnpackNormal(tex2D(_NormalMap, TRANSFORM_TEX(i.uv0, _NormalMap)));
				float3 N = normalize(mul(NormalMap.xyz, float3x3(i.tangentDir, i.bitangentDir, i.normalDir)));
				float3 E = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				float3 L = normalize(_WorldSpaceLightPos0.xyz);
				float3 H = normalize(E + L);
				float NdotL = 0.5 * dot(lerp(i.normalDir, N, _Is_Normal), L) + 0.5;
				float NdotH = dot(N, H);

				float attenuation = LIGHT_ATTENUATION(i);

				// ベースカラーtex
				float3 set_txBase = tex2D(_MainTexture, TRANSFORM_TEX(GetTileUV2x2(_tileID_BaseColor, i.uv0), _MainTexture)).rgb;
				// 1影カラーtex
				float3 set_tx1stShadow = tex2D(_MainTexture, TRANSFORM_TEX(GetTileUV2x2(_tileID_1stShadow, i.uv0), _MainTexture)).rgb;

				// セルルック光・影
				float3 toon_light_color = CalcAddToonLightColor();
				set_txBase += toon_light_color.rgb;
				set_tx1stShadow *= CalcMultiToonShadowColor(toon_light_color);

				// ハイライトtex
				float3 set_txHilight = tex2D(_MaskTexture, TRANSFORM_TEX(GetTileUV2x2(_tileID_Hilight, i.uv0), _MaskTexture)).rrr;
				// グローtex
				float3 set_txGlow = tex2D(_MaskTexture, TRANSFORM_TEX(GetTileUV2x2(_tileID_Glow, i.uv0), _MaskTexture)).rrr;

				// 色マージ
				float colorMerge = tex2D(_MaskTexture, TRANSFORM_TEX(GetTileUV2x2(_tileID_Hilight, i.uv0), _MaskTexture)).g;
				set_txBase      = lerp(set_txBase,      _ChangeBaseColor.rgb,   colorMerge);
				set_tx1stShadow = lerp(set_tx1stShadow, _ChangeShadowColor.rgb, colorMerge);

				// 光の当たり方
				float set_HitLight	  = (NdotL * lerp(NdotL, (((attenuation * 0.5) + 0.5) * _Tweak_SystemShadows), _Is_SystemShadows));
				float Shadow1x2		   = _Shadow2nd * _Shadow1st;
				float node_3360		   = _Shadow1st - _Mix_BaseTexture;
				float node_6885		   = Shadow1x2	- _Mix_ShadowTexture;
				float set_Step1st	 = saturate(1.0 - ((set_HitLight - node_3360) / (_Shadow1st - node_3360))); // 1影の範囲値
				float set_Step2nd	 = saturate(1.0 - ((set_HitLight - node_6885) / (Shadow1x2	- node_6885))); // 2影の範囲値
				float3 node_443		= lerp(set_txBase, lerp(set_tx1stShadow, (_2nd_ShadeColor.rgb * set_tx1stShadow), set_Step2nd), set_Step1st);

				float3 set_HilightValue = lerp(0.0, _Hilight_Color.rgb, lerp(0.0, set_txHilight, _Is_Hilight)); // ハイライトの値
				float NdotE = dot(N, E);
				float set_HilightRange = step(NdotE, (1-_Tweak_Hilight));	// ハイライトの範囲値

				float3 Hilight = (lerp(saturate((1.0 - (1.0 - node_443) * (1.0 - set_HilightValue))), node_443, set_HilightRange) * CalcLightColor(_LightColor0)); // ライトカラーを乗算

				// グローカラー
				float3 set_GlowColor = _Glow_Color.rgb * 5.0;

				float3 finalColor = lerp(Hilight, lerp(Hilight, set_GlowColor, _Is_Glow), set_txGlow);
				float4 finalRGBA = float4(lerp(0.0.xxx, finalColor, _Brightness), 1.0);
				UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
				return finalRGBA;
			}
			ENDCG
		}
		Pass
		{
			Name "Outline"
			Tags
			{
			}
			Stencil
			{
				Ref			[_StencilRef]
				ReadMask	[_StencilReadMask]
				WriteMask	[_StencilWriteMask]
				Comp	[_StencilTest]
				Pass	[_StencilOperatorPass]
				Fail	[_StencilOperatorFail]
				ZFail	[_StencilOperatorZFail]
			}
			
			Cull Front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#define _GLOSSYENV 1
			#include "UnityCG.cginc"
			#include "UnityPBSLighting.cginc"
			#include "UnityStandardBRDF.cginc"
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma multi_compile_shadowcaster
			#pragma multi_compile_fog
			#pragma target 3.0
			#include "ToonCommon.cginc"
			struct VertexInput
			{
				float4 vertex		: POSITION;
				float3 normal		: NORMAL;
				float2 texcoord0	: TEXCOORD0;
			};
			struct VertexOutput
			{
				float4 pos			: SV_POSITION;
				float2 uv0			: TEXCOORD0;
				float4 posWorld		: TEXCOORD1;
				UNITY_FOG_COORDS(2)
			};
			VertexOutput vert (VertexInput v)
			{
				VertexOutput o = (VertexOutput)0;
				o.uv0			= v.texcoord0;
				o.posWorld		= mul(unity_ObjectToWorld, v.vertex);

				// アウトラインtex
				float3 set_txOutline = tex2Dlod(_MaskTexture, float4(TRANSFORM_TEX(GetTileUV2x2(_tileID_Outline, o.uv0), _MaskTexture), 0.0, 0.0)).rrr;
				// アウトライン
				float CameraDistance = distance(_WorldSpaceCameraPos, mul(unity_ObjectToWorld, v.vertex).xyz);
				float CameraDistance01 = (CameraDistance - _Nearest_Distance) / (_Farthest_Distance - _Nearest_Distance);
				float3 set_Outline = saturate(1.0 - CameraDistance01) * (_Outline_Width * set_txOutline * 0.01);

				o.pos			= UnityObjectToClipPos(float4(v.vertex.xyz + v.normal * set_Outline, 1.0));
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
			float4 frag(VertexOutput i) : COLOR
			{
				float3 set_txBase = tex2D(_MainTexture, TRANSFORM_TEX(GetTileUV2x2(_tileID_BaseColor, i.uv0), _MainTexture)).rgb;
				// アウトラインカラー
				float3 set_OutlineColor = (lerp(_Outline_Color.rgb, ((set_txBase * set_txBase) * _Outline_Color.rgb), _Is_BlendBaseColor) + ((_OutlineGlow_Color.rgb * 5.0) * _Is_OutlineGlow));
				return float4(lerp(0.0.xxx, set_OutlineColor, _Brightness), 1.0);
			}
			ENDCG
		}
	}
	FallBack "Mobile/VertexLit"
//	CustomEditor "ShaderForgeMaterialInspector"
}
