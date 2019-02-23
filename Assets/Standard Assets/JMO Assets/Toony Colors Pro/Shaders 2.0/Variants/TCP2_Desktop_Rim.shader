		{
				#if UNITY_SPECCUBE_BOX_PROJECTION
				half3 worldNormal1 = BoxProjectedCubemapDirection (worldNormal, IN.worldPos, unity_SpecCube1_ProbePosition, unity_SpecCube1_BoxMin, unity_SpecCube1_BoxMax);
				#else
				half3 worldNormal1 = worldNormal;
				#endif
				
				half3 env1 = Unity_GlossyEnvironment (UNITY_PASS_TEXCUBE_SAMPLER(unity_SpecCube1,unity_SpecCube0), unity_SpecCube1_HDR, worldNormal1, 1-oneMinusRoughness);
				reflColor = lerp(env1, env0, blendLerp);
			}
			else
			{
				reflColor = env0;
			}
			#else
			reflColor = env0;
			#endif
			reflColor *= 0.5;
		#else
			#if TCP2_BUMP
			half3 worldRefl = WorldReflectionVector(IN, o.Normal);
			#else
			half3 worldRefl = IN.worldRefl;
			#endif
			fixed4 reflColor = texCUBE(_Cube, worldRefl);
		#endif
		#if TCP2_REFLECTION_MASKED
			reflColor.rgb *= c.a;
		#endif
			reflColor.rgb *= _ReflectColor.rgb * _ReflectColor.a;
			o.Emission += reflColor.rgb;
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


Shader "Hidden/Toony Colors Pro 2/Variants/Desktop Rim"
{
	Properties
	{
		//TOONY COLORS
		_Color ("Color", Color) = (1,1,1,1)
		_HColor ("Highlight Color", Color) = (0.785,0.785,0.785,1.0)
		_SColor ("Shadow Color", Color) = (0.195,0.195,0.195,1.0)
		
		//DIFFUSE
		_MainTex ("Main Texture (RGB) Spec/Refl Mask (A) ", 2D) = "white" {}
		
		//TOONY COLORS RAMP
		[TCP2Gradient] _Ramp ("#RAMPT# Toon Ramp (RGB)", 2D) = "gray" {}
		_RampThreshold ("#RAMPF# Ramp Threshold", Range(0,1)) = 0.5
		_RampSmooth ("#RAMPF# Ramp Smoothing", Range(0.01,1)) = 0.1
		
		//BUMP
		_BumpMap ("#NORM# Normal map (RGB)", 2D) = "bump" {}
		
		//RIM LIGHT
		_RimColor ("#RIM# Rim Color", Color) = (0.8,0.8,0.8,0.6)
		_RimMin ("#RIM# Rim Min", Range(0,1)) = 0.5
		_RimMax ("#RIM# Rim Max", Range(0,1)) = 1.0
		
		//RIM DIRECTION
		_RimDir ("#RIMDIR# Rim Direction", Vector) = (0.0,0.0,1.0,0.0)
		
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		
		#include "../Include/TCP2_Include.cginc"
		
		#pragma surface surf ToonyColors nodirlightmap vertex:vert
		#pragma target 3.0
		
		#pragma shader_feature TCP2_DISABLE_WRAPPED_LIGHT
		#pragma shader_feature TCP2_RAMPTEXT
		#pragma shader_feature TCP2_BUMP
		#pragma shader_feature TCP2_RIMDIR
		
		//================================================================
		// VARIABLES
		
		fixed4 _Color;
		sampler2D _MainTex;
		
	#if TCP2_BUMP
		sampler2D _BumpMap;
	#endif
		fixed4 _RimColor;
		f