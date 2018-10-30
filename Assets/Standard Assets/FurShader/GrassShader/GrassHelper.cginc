// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

#include "UnityCG.cginc"
#include "UnityLightingCommon.cginc"


struct appdata
{
	float4 vertex    : POSITION;
	float2 uv		 : TEXCOORD0;
	float4 normal    : NORMAL;
};

struct v2f
{
	float4 vertex    : SV_POSITION;
	float2 uv		 : TEXCOORD0;
	float4 normal    : TEXCOORD1;
	float4 diff      : COLOR0;
	UNITY_FOG_COORDS(3)
};

sampler2D _MainTex;
sampler2D _DirtTex;
sampler2D _HeightMap;
float4 _MainTex_ST;

float4 _GrassColor;
float4 _DirtColor;
float _GrassTransparency;
float _DirtTransparency;
float _GrassBrightness;
float _DirtBrightness;
float _HeightMapBrightness;

float4 _Gravity;

float _WindStrength;
float _WindSpeed;

float _GrassLength;
float _GrassStiff;
float _EnableDirt;

float _Shadows;
float _ShadowStrength;


v2f vert(appdata v)
{
	v2f o;
	float grasslength = _GRASSLAYER * (20 * _GrassLength);

	//WIND
	float3 wind = float3(0, 0, 0);
	wind.x = _WindStrength * sin(_Time.y * (10 *_WindSpeed) + v.vertex.x );
	wind.y = _WindStrength * cos(_Time.y * (10 * _WindSpeed) * 0.25 + v.vertex.y );
	wind.z = _WindStrength * sin(_Time.y * (10 * _WindSpeed) * 0.45 + v.vertex.y );

	//GRAVITY - Add gravity to wind direction and subtract velocity
	_Gravity = mul(unity_WorldToObject, _Gravity);
	float3 totaldisplacement = wind + _Gravity.xyz;

	//GRASSLENGTH, WEIGHT, AND SPACING BETWEEN LAYERS - Displacement
	float4 disNormal = v.normal;
	float displacementFactor = pow(grasslength, (5 * ( 1 - _GrassStiff)));
	disNormal.xyz += totaldisplacement * displacementFactor;
	float4 displacement = normalize(disNormal) * grasslength * 0.25;

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
	//DIRT LAYER
	if (_GRASSLAYER == 0.0 && _EnableDirt)
	{
		//Skin Color
		float4 dirtColor = tex2D(_DirtTex, i.uv);
		float4 colorChange = i.diff * _DirtColor;
		dirtColor *= colorChange;

		//Skin Brightness
		dirtColor.rgb *= (5 * _DirtBrightness);

		//Skin Transparency
		dirtColor.a = 1.0;
		dirtColor.a *= max(_DirtTransparency, _DirtTransparency * _GRASSLAYER);

		//Skin Fog
		UNITY_APPLY_FOG(i.fogCoord, dirtColor);

		return dirtColor;
	}
	else
	{
		//Check HeightMap
		float2 height = tex2D(_HeightMap, i.uv);
		float hmBright = (2 * _HeightMapBrightness);
		height *= hmBright;
		//Discard pixels if heightmap is black 
		if (height.x <= 0.0 || height.y < _GRASSLAYER) discard;

		//Grass Color
		float4 grassColor = tex2D(_MainTex, i.uv);
		float4 colorChange = i.diff * _GrassColor;
		grassColor *= colorChange;

		//Depth Shadows
		if (_Shadows == 1)
		{
			float shadow = pow(_GRASSLAYER, (2 * _ShadowStrength));
			grassColor.rgb = grassColor.rgb * shadow;
		}

		//Grass transparency, lower layers are more visable
		grassColor.a = 1.0 - _GRASSLAYER;

		//Grass brightness
		grassColor.rgb *= (5 * _GrassBrightness);

		//Grass transparency - Overall, set by user
		grassColor.a *= max(_GrassTransparency, _GrassTransparency * _GRASSLAYER);

		//Grass Fog
		UNITY_APPLY_FOG(i.fogCoord, grassColor);

		return grassColor;
	}
	
}
