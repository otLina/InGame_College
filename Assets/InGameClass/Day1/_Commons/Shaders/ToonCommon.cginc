uniform sampler2D	_NoiseTexture;
uniform float4		_NoiseTexture_ST;
uniform sampler2D	_MainTexture;
uniform float4		_MainTexture_ST;
uniform sampler2D	_MaskTexture;
uniform float4		_MaskTexture_ST;
uniform sampler2D	_NormalMap;
uniform float4		_NormalMap_ST;

uniform float	_EdgeShineWidth;
uniform float	_EdgeShineSpeed;
uniform float4	_EdgeShineColor;
uniform float	_AttributeChangeRate;
uniform float3	_AttributeChangeColor;
uniform float	_Is_DistortionBG;
uniform float4	_RampColor1;
uniform float4	_RampColor2;
uniform float	_RampColorScale;
uniform float	_CutOff;
uniform float	_DissolveHeight;
uniform float	_DissolveAlpha;
uniform float	_DissolveRange;
uniform float	_DissolveAmount;
uniform vector	_DissolveHeightOffset;
uniform vector	_DissolveScale;
uniform float4	_Outline_Color;
uniform float	_Farthest_Distance;
uniform float	_Nearest_Distance;
uniform float	_Outline_Width;
uniform float4	_BaseColor;
uniform float4	_Hilight_Color;
uniform float4	_2nd_ShadeColor;
uniform float4	_Glow_Color;
uniform float4	_OutlineGlow_Color;
uniform float4	_RimLight_Color;
uniform float	_RimPower;
uniform float	_Shadow1st;
uniform float	_Shadow2nd;
uniform float	_Mix_BaseTexture;
uniform float	_Mix_ShadowTexture;
uniform float	_tileID_BaseColor;
uniform float	_tileID_Outline;
uniform float	_tileID_1stShadow;
uniform float	_tileID_Hilight;
uniform float	_tileID_Glow;
uniform float	_tileID_Mask;
uniform float	_Tweak_SystemShadows;
uniform float	_Tweak_Hilight;
uniform float	_Mix_Hilight;
uniform fixed	_Brightness;
uniform fixed	_Is_BlendBaseColor;
uniform fixed	_Is_SystemShadows;
uniform fixed	_Is_OutlineGlow;
uniform fixed	_Is_Glow;
uniform fixed	_Is_Hilight;
uniform fixed	_Is_Normal;
uniform fixed	_Is_RimLight;
uniform fixed	_Is_EdgeShine;
uniform fixed	_Is_AttributeChange;

uniform float4	_ChangeBaseColor;
uniform float4	_ChangeShadowColor;
uniform float	_LightColorWeight;

uniform float3	_ToonLightColor;
uniform float3	_ToonLightDir;
uniform float	_ToonLightWeight;

float3 ApplyRimLight(float3 source, float3 Eye, float3 Normal)
{
	source += pow(1.0 - abs(dot(Eye, Normal)), _RimPower) * _RimLight_Color * _Is_RimLight;
	return source;
}

float4 ApplyDissolveEdge(float3 source, float2 uv, float2 uvProj, float height)
{
	if (_DissolveAmount <= 0.0)
	{
		return float4(source, 1.0);
	}
	else
	if (_Is_DistortionBG < 0.5)
	{
		discard;
	}

	return float4(float3(0, 0, 0), _DissolveAlpha);
}

float4 ApplyDissolve(float3 source, float2 uv, float3 hOffset ,float height)
{
	if (_DissolveAmount <= 0.0)
	{
		return float4(source, 1.0);
	}

	float2 scale = float2(_NoiseTexture_ST.x*_DissolveScale.x, _NoiseTexture_ST.y*_DissolveScale.y);
	float3 NoiseTextureValue = tex2D(_NoiseTexture, (uv.xy * (_NoiseTexture_ST.xy+scale.xy) + _NoiseTexture_ST.zw)).rgb;

	float HeightOffset = (height / (_DissolveHeight+hOffset.y))*5;
	float NoiseValue = NoiseTextureValue.r - HeightOffset;
	float DissolveValue = NoiseValue+lerp((1.0/_DissolveRange)+(_DissolveHeight+hOffset.y), -(_DissolveHeight*1.5)+hOffset.y, _DissolveAmount);

	clip(DissolveValue);

	DissolveValue = saturate(DissolveValue * _DissolveRange);
	if (DissolveValue > 0.99)
	{
		return float4(source, 1.0);
	}

	float3 RampColor = lerp(_RampColor1.rgb * _RampColorScale, _RampColor2.rgb, DissolveValue * NoiseTextureValue.r);
	return float4(RampColor, 1.0);
}

float ComputeOutlineWidth(float3 PosWorld)
{
	float CameraDistance = distance(_WorldSpaceCameraPos, PosWorld);
	float DistanceRate   = (CameraDistance - _Nearest_Distance) / (_Farthest_Distance - _Nearest_Distance);
	return saturate(1.0 - DistanceRate) * _Outline_Width * 0.01;
}

float2 GetTileUV2x2(float ID, float2 uv)
{
	const float DivU = 2.0; // UV分割数_横
	const float DivV = 2.0; // UV分割数_縦

	float2 DivUVrcp = float2(1.0, 1.0) / float2(DivU, DivV);
	float OffsetV = floor(ID * DivUVrcp.x);
	float OffsetU = ID - DivU * OffsetV;

	return (uv + float2(OffsetU, OffsetV)) * DivUVrcp;
}

float2 GetTileUV2x1(float ID, float2 uv)
{
	const float DivU = 2.0; // UV分割数_横
	const float DivV = 1.0; // UV分割数_縦

	float2 DivUVrcp = float2(1.0, 1.0) / float2(DivU, DivV);
	float OffsetV = floor(ID * DivUVrcp.x);
	float OffsetU = ID - DivU * OffsetV;

	return (uv + float2(OffsetU, OffsetV)) * DivUVrcp;
}

float3 ApplyAttributeChange(float3 OriginalColor, float Height)
{
	if (_Is_AttributeChange < 0.5) return OriginalColor;

	float rate = 1.0 - saturate(abs(Height - _AttributeChangeRate) * 3.0);
	float3 color = lerp(OriginalColor, OriginalColor * 0.5, rate) + (_AttributeChangeColor.rgb * rate);
	return color;
}

float3 CalcLightColor(float3 LightColor)
{
	return lerp(float3(1, 1, 1), LightColor, _LightColorWeight);
}

float3 CalcAddToonLightColor()
{
	// 影響度を加味した演出用ライト色(100%反映にすると色味が強く出過ぎるため、最大50%に抑える)
	return _ToonLightColor * min(_ToonLightWeight, 0.5);
}

float3 CalcMultiToonShadowColor(float3 ToonLightColor)
{
	// 影への乗算カラーの反映率。1に近いほど光による影の濃さが増す
	float shadeDarknessWeight = 0.6;
	// 影への乗算カラー
	return (
		1 - (ToonLightColor.g + ToonLightColor.b) * shadeDarknessWeight,
		1 - (ToonLightColor.r + ToonLightColor.b) * shadeDarknessWeight,
		1 - (ToonLightColor.r + ToonLightColor.g) * shadeDarknessWeight
	);
}