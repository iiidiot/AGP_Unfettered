Shader "RIOT/Grass/GrassShader (Medium)" 
{
	Properties 
	{
		[Header(Textures)]
		_MainTex("Fur Texture (RGB)", 2D) = "white" {}
		[NoScaleOffset]
		_DirtTex("Dirt Texture (RGB)", 2D) = "white" {}
		[NoScaleOffset]
		_HeightMap("Height Map (Gray) ", 2D) = "white" {}

		[Header(Grass Color Properties)]
		_GrassColor("Grass Color", Color) = (0.27, 0.949, 0.121, 1)
		_GrassBrightness("Grass Brightness", Range(0, 1)) = 0.150
		_HeightMapBrightness("Height Map Brightness", Range(0, 1)) = 1.00
		_GrassTransparency("Grass Transparency", Range(0, 1.0)) = 1.0

		[Header(Dirt Color Properties)]
		[Toggle]_EnableDirt("Enable Dirt Layer", Float) = 0
		_DirtColor("Dirt Color", Color) = (1, 1, 1, 1)
		_DirtBrightness("Dirt Brightness", Range(0, 1)) = 0.25
		_DirtTransparency("Dirt Transparency", Range(0, 1.0)) = 1.0
		
		[Header(Grass Properties)]
		_GrassLength("Grass Length", Range(0, 1.0)) = 0.075
		_GrassStiff("Grass Stiffness", Range(0, 1.0)) = 0.1
		_Gravity("Gravity Direction", Vector) = (0.0, 0.25, 0.0, 0.0)

		[Header(Depth Shadow Properties)]
		[Toggle]_Shadows("Depth Shadows", Float) = 0
		_ShadowStrength("Depth Shadow Strength", Range(0.0, 1.0)) = .5
		
		[Header(Randomized Wind Properties)]
		_WindSpeed("Wind Speed", Range(0, 1.0)) = 0.245
		_WindStrength("Wind Strength", Range(0, 1)) = 0.150	
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

			    #define _GRASSLAYER 0.0
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.02
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.04
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.06
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.08
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.1
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.12
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.14
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.16
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.18
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.2
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.22
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.24
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.26
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.28
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.3
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.32
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.34
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.36
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.38
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.4
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.42
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.44
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.46
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.48
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.5
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.52
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.54
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.56
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.58
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.6
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.62
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.64
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.66
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.68
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.7
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.72
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.74
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.76
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.78
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.8
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.82
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.84
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.86
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.88
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.9
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.92
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.94
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.96
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}

			Pass
			{
			    CGPROGRAM

			    #define _GRASSLAYER 0.98
			    #pragma vertex vert
			    #pragma fragment frag
			    #pragma multi_compile_fog
			    #include "GrassHelper.cginc"

			    ENDCG
			}
		}
	}
}
