Shader "RIOT/Fur/FurShader (High)" 
{
	Properties 
	{
		[Header(Textures)]
		_MainTex ("Fur Texture (RGB)", 2D) = "white" {}
		[NoScaleOffset]
		_SkinTex ("Skin Texture (RGB)", 2D) = "white" {}
		[NoScaleOffset]
		_HeightMap("Height Map (Gray) ", 2D) = "white" {}

		[Header(Fur Color Properties)]
		_FurColor("Fur Color", Color) = (1, 1, 1, 1)
		_Brightness("Fur Brightness", Range(0, 1)) = 0.25
		_HeightMapBrightness("Height Map Brightness", Range(0, 1)) = 0.25
		_FurTransparency("Fur Transparency", Range(0, 1.0)) = 1.0
		
		[Header(Skin Color Properties)]
		[Toggle]_EnableSkin("Enable Skin Layer", Float) = 0
		_SkinColor("Skin Color", Color) = (1, 1, 1, 1)
		_SkinBrightness("Skin Brightness", Range(0, 1)) = 0.25
		_SkinTransparency("Skin Transparency", Range(0, 1.0)) = 1.0
		
		[Header(Fur Properties)]
		_FurLength("Fur Length", Range(0, 1.0)) = 0.25
		_FurStiff("Fur Stiffness", Range(0, 1.0)) = 0.1
		_Gravity("Gravity Direction", Vector) = (0.0, 0.0, 0.0, 0.0)

		[Header(Depth Shadow Properties)]
		[Enum(None,0, Normal, 1, Invert, 2)] _Shadows("Depth Shadows", Float) = 0
		_ShadowStrength("Depth Shadow Strength", Range(0.0, 1.0)) = .5
		
		[Header(Randomized Wind Properties)]
		_WindSpeed("Wind Speed", Range(0, 1.0)) = 0.0
		_WindStrength("Wind Strength", Range(0, 1)) = 0.0
		
		[Header(Velocity Properties)]
		[Toggle] _CullVelocity("Cull Velocity Angle", Float) = 0
		_CullAngle("Cull Angle", Range(-1.0, 1.0)) = 0.0

		[HideInInspector] _Velocity("Velocity", Vector) = (0.0, 0.0, 0.0, 0.0)
		
	}
	
	Category {

		ZWrite on
		Cull off 
		Tags {"Queue" = "Transparent" "RenderType"="Transparent" "LightMode" = "ForwardBase" "DisableBatching" = "True"}
		Blend SrcAlpha OneMinusSrcAlpha
		
		SubShader {
		
			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.0
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.013333334
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.026666667
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.04
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.053333335
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.06666667
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.08
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.093333334
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.10666667
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.12
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.13333334
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.14666666
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.16
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.17333333
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.18666667
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.2
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.21333334
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.22666667
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.24
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.25333333
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.26666668
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.28
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.29333332
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.30666667
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.32
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.33333334
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.34666666
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.36
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.37333333
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.38666666
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.4
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.41333333
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.42666668
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.44
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.45333335
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.46666667
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.48
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.49333334
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.50666666
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.52
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.53333336
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.5466667
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.56
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.5733333
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.58666664
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.6
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.61333334
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.62666667
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.64
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.6533333
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.6666667
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.68
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.6933333
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.70666665
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.72
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.73333335
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.74666667
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.76
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.7733333
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.7866667
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.8
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.81333333
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.82666665
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.84
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.85333335
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.8666667
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.88
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.8933333
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.9066667
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.92
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.93333334
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.94666666
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.96
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.97333336
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

			Pass
			{
				CGPROGRAM

				#define _FURLAYER 0.9866667
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "FurHelper.cginc"

				ENDCG
			}

		}
	}
}
