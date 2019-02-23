cBlendOutline ("#BLEND# Blending Source", Float) = 5
		_DstBlendOutline ("#BLEND# Blending Dest", Float) = 10
		
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		
		#include "../Include/TCP2_Include.cginc"
		
		#pragma surface surf ToonyColorsSpec nodirlightmap vertex:vert
		#pragma target 3.0
		
		#pragma shader_feature TCP2_DISABLE_WRAPPED_LIGHT
		#pragma shader_feature TCP2_RAMPTEXT
		#pragma shader_feature TCP2_BUMP
		#pragma shader_feature TCP2_SPEC_TOON
		#pragma shader_feature TCP2_RIMDIR
		
		//================================================================
		// VARIABLES
		
		fixed4 _Color;
		sampler2D _MainTex;
		
	#if TCP2_BUMP
		sampler2D _BumpMap;
	#endif
		fixed _Shininess;
		fixed4 _RimColor;
		fixed _RimMin;
		fixed _RimMax;
		float4 _RimDir;
		
		struct Input
		{
			half2 uv_MainTex : TEXCOORD0;
	#if TCP2_BUMP
			half2 uv_BumpMap : TEXCOORD1;
	#endif
	#if !TCP2_RIMDIR
			float3 viewDir;
	#endif
	#if TCP2_RIMDIR
			float3 bViewDir;
	#endif
		};
		
		//================================================================
		// VERTEX FUNCTION
		
	#if TCP2_RIMDIR
		inline float3 TCP2_ObjSpaceViewDir( in float4 v )
		{
			float3 camPos = _WorldSpaceCameraPos;
			camPos += mul(_RimDir, UNITY_MATRIX_V).xyz;
			float3 objSpaceCameraPos = mul(unity_WorldToObject, float4(camPos, 1)).xyz;
			return objSpaceCameraPos - v.xyz;
		}
	#endif
		
		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			
		#if TCP2_BUMP && TCP2_RIMDIR
			TANGENT_SPACE_ROTATION;
			o.bViewDir = mul(rotation, TCP2_ObjSpaceViewDir(v.vertex));
		#endif
		}
		
		//================================================================
		// SURFACE FUNCTION
		
		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * _Color.rgb;
			o.Alpha = c.a * _Color.a;
			
			//Specular
			o.Gloss = c.a;
			o.Specular = _Shininess;
	#if TCP2_BUMP
			//Normal map
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	#endif
			//Rim Light
		#if TCP2_RIMDIR && TCP2_BUMP
			float3 viewDir = normalize(IN.bViewDir);
		#elif TCP2_RIMDIR
			_RimDir.x += UNITY_MATRIX_MV[0][3] * (1/UNITY_MATRIX_MV[2][3]) * (1-UNITY_MATRIX_P[3][3]);
			_RimDir.y += UNITY_MATRIX_MV[1][3] * (1/UNITY_MATRIX_MV[2][3]) * (1-UNITY_MATRIX_P[3][3]);
			float3 viewDir = normalize(UNITY_MATRIX_V[0].xyz * _RimDir.x + UNITY_MATRIX_V[1].xyz * _RimDir.y + UNITY_MATRIX_V[2].xyz * _RimDir.z);
		#else
			float3 viewDir = normalize(IN.viewDir);
		#endif
			half rim = 1.0f - saturate( dot(viewDir, o.Normal) );
			rim = smoothstep(_RimMin, _RimMax, rim);
			o.Albedo = lerp(o.Albedo.rgb, _RimColor, rim * _RimColor.a);
		}
		
		ENDCG
		
		//Outlines
		Tags { "Queue"="Transparent" "IgnoreProjectors"="True" "RenderType"="Transparent" }
		UsePass "Hidden/Toony Colors Pro 2/Outline Only/OUTLINE_BLENDING"
	}
	
	Fallback "Diffuse"
	CustomEditor "TCP2_MaterialInspector"
}
// Toony Colors Pro+Mobile 2
// (c) 2014-2018 Jean Moreno


Shader "Hidden/Toony Colors Pro 2/Variants/Mobile M