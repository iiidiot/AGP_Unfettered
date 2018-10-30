// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

#include "UnityCG.cginc"
#include "UnityLightingCommon.cginc"


struct appdata
{
	float4 vertex	: POSITION;
	float2 uv		: TEXCOORD0;
	float4 normal	: NORMAL;
};

struct v2f
{
	float4 vertex        : SV_POSITION;
	float2 uv		     : TEXCOORD0;
	float4 normal        : TEXCOORD1;
	float4 diff          : COLOR0;
	UNITY_FOG_COORDS(3)
};

sampler2D _MainTex;
sampler2D _SkinTex;
sampler2D _HeightMap;
float4 _MainTex_ST;

float4 _FurColor;
float4 _SkinColor;
float _FurTransparency;
float _SkinTransparency;
float _Brightness;
float _SkinBrightness;
float _HeightMapBrightness;

float4 _Gravity;
float4 _Velocity;

float _WindStrength;
float _WindSpeed;

float _FurLength;
float _FurStiff;
float _EnableSkin;

float _Shadows;
float _InvertShadows;
float _ShadowStrength;

float _CullVelocity;
float _CullAngle;

v2f vert(appdata v)
{
	v2f o;
	float furlength = _FURLAYER * (20 * _FurLength);

	//WIND
	float3 wind = float3(0, 0, 0);
	wind.x = _WindStrength * sin(_Time.y * (10 * _WindSpeed) + v.vertex.x);
	wind.y = _WindStrength * cos(_Time.y * (10 * _WindSpeed) * 0.25 + v.vertex.y);
	wind.z = _WindStrength * sin(_Time.y * (10 * _WindSpeed) * 0.45 + v.vertex.y);

	//GRAVITY - Add gravity to wind direction and subtract velocity
	_Gravity = mul(unity_WorldToObject, _Gravity);
	float3 totaldisplacement = wind + _Gravity.xyz - _Velocity.xyz;

	//FURLENGTH, WEIGHT, AND SPACING BETWEEN LAYERS - Displacement
	float4 disNormal = v.normal;
	float displacementFactor = pow(furlength, (5 * (1 - _FurStiff)));
	disNormal.xyz += totaldisplacement * displacementFactor;
	float4 displacement = normalize(disNormal) * furlength * 0.25;

	//SET VERTEX POSITION BY ADDING DISPLACEMENT POSITION
	//OUTPUT VERTEX VIEWPORT POSITION
	float4 wpos = float4(v.vertex.xyz + displacement.xyz, 1.0);
	o.vertex = UnityObjectToClipPos(wpos);

	//OUTPUT TEXTURE COORDINATES
	o.uv = TRANSFORM_TEX(v.uv, _MainTex);
	o.normal = mul(unity_ObjectToWorld, v.normal);

	//LIGHTING - Provided by Unity's shader tutorials (http://docs.unity3d.com/Manual/SL-VertexFragmentShaderExamples.html)
	float3 worldNormal = mul(v.normal, unity_WorldToObject); //This Caused errors for PS4 : UnityObjectToWorldNormal(v.normal); 
	float dotNormal = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
	o.diff = dotNormal * _LightColor0;
	o.diff.rgb += ShadeSH9(half4(worldNormal, 1));

	//FOG
	UNITY_TRANSFER_FOG(o, o.vertex);

	return o;
}

float4 frag(v2f i) : SV_Target
{
	//VELOCITY CULLING
	if (_CullVelocity)
	{
		float4 normal = mul(unity_WorldToObject, i.normal);
		if (dot(normalize(normal), normalize(-_Velocity)) > _CullAngle) discard;
	}

	//SKIN LAYER
	if (_FURLAYER == 0.0 && _EnableSkin)
	{
		//Skin Color
		float4 skinColor = tex2D(_SkinTex, i.uv);
		float4 colorChange = i.diff * _SkinColor;
		skinColor *= colorChange;

		//Skin Brightness
		skinColor.rgb *= (5 * _SkinBrightness);

		//Skin Transparency
		skinColor.a = 1.0;
		skinColor.a *= max(_SkinTransparency, _SkinTransparency * _FURLAYER);

		//Skin Fog
		UNITY_APPLY_FOG(i.fogCoord, skinColor);

		return skinColor;
	}
	//FUR LAYER
	else
	{
		//Fur Color
		float4 furColor = tex2D(_MainTex, i.uv);
		float4 colorChange = i.diff * _FurColor;
		furColor *= colorChange;

		//Depth Shadows - This darkens lower layers of the fur shader to give apperance of shadows underneath
		//Normal shadows
		if (_Shadows == 1)
		{
			float shadow = pow(_FURLAYER, (2 * _ShadowStrength));
			furColor.rgb = furColor.rgb * shadow;
		}
		//Invert shadows
		else if (_Shadows == 2)
		{
			float shadow = pow(_FURLAYER, 2 - (2 * _ShadowStrength));
			furColor.rgb = furColor.rgb * (1 - shadow);
		}

		//Height map
		float2 height = tex2D(_HeightMap, i.uv);
		float hmBright = (2 * _HeightMapBrightness);
		height.xy *= hmBright;

		//Discard pixels if heightmap is black 
		if (height.x <= 0.0 || height.y < _FURLAYER) discard;

		//Fur transparency, lower layers are more visable
		furColor.a = 1.0 - _FURLAYER;

		//Fur brightness
		furColor.rgb *= (5 * _Brightness);

		//User set fur transparency
		furColor.a *= max(_FurTransparency, _FurTransparency * _FURLAYER);

		//Fur Fog
		UNITY_APPLY_FOG(i.fogCoord, furColor);

		return furColor;
	}
}
