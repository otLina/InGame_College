Shader "Grenge/PJ_colorful/Transparent"
{
	Properties
	{
		_MainTexture ("MainTexture", 2D) = "" {}
//		[HideInInspector]_MaskTex ("Mask", 2D) = "white" {}
		_tileID_Mask ("tileID_Mask", int) = 0

		[Space(15)]
		_Brightness ("Brightness", Range(0.0, 1.0)) = 1.0
	}

	SubShader
	{
		Tags
		{
			"RenderType"="Transparent"
			"Queue"="Transparent"
			"IgnoreProjector"="True"
		}

		Pass
		{
			Name "AlphaMask"
			ZWrite Off
			Blend One OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "ToonCommon.cginc"

			sampler2D _MainTex;

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert (appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos (v.vertex);
				o.uv = v.texcoord;
				return o;
			}

			half4 frag (v2f i) : COLOR
			{
				half4 base = tex2D(_MainTexture, TRANSFORM_TEX(GetTileUV2x2(_tileID_Mask, i.uv), _MainTexture));
                base.rgb = lerp(0.0.xxx, base.rgb, _Brightness * base.a);
				return base;
			}
			ENDCG
		}
	}
	FallBack Off
}
