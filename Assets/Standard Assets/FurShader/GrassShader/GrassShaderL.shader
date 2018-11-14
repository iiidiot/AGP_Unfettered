Shader "RIOT/Grass/GrassShader (Low)" 
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

                #define _GRASSLAYER 0.033333335
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog
                #include "GrassHelper.cginc"

                ENDCG
            }

            Pass
            {
                CGPROGRAM

                #define _GRASSLAYER 0.06666667
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

                #define _GRASSLAYER 0.13333334
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog
                #include "GrassHelper.cginc"

                ENDCG
            }

            Pass
            {
                CGPROGRAM

                #define _GRASSLAYER 0.16666667
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

                #define _GRASSLAYER 0.23333333
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog
                #include "GrassHelper.cginc"

                ENDCG
            }

            Pass
            {
                CGPROGRAM

                #define _GRASSLAYER 0.26666668
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

                #define _GRASSLAYER 0.33333334
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog
                #include "GrassHelper.cginc"

                ENDCG
            }

            Pass
            {
                CGPROGRAM

                #define _GRASSLAYER 0.36666667
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

                #define _GRASSLAYER 0.43333334
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog
                #include "GrassHelper.cginc"

                ENDCG
            }

            Pass
            {
                CGPROGRAM

                #define _GRASSLAYER 0.46666667
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

                #define _GRASSLAYER 0.53333336
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog
                #include "GrassHelper.cginc"

                ENDCG
            }

            Pass
            {
                CGPROGRAM

                #define _GRASSLAYER 0.56666666
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

                #define _GRASSLAYER 0.6333333
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog
                #include "GrassHelper.cginc"

                ENDCG
            }

            Pass
            {
                CGPROGRAM

                #define _GRASSLAYER 0.6666667
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

                #define _GRASSLAYER 0.73333335
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog
                #include "GrassHelper.cginc"

                ENDCG
            }

            Pass
            {
                CGPROGRAM

                #define _GRASSLAYER 0.76666665
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

                #define _GRASSLAYER 0.8333333
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog
                #include "GrassHelper.cginc"

                ENDCG
            }

            Pass
            {
                CGPROGRAM

                #define _GRASSLAYER 0.8666667
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

                #define _GRASSLAYER 0.93333334
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog
                #include "GrassHelper.cginc"

                ENDCG
            }

            Pass
            {
                CGPROGRAM

                #define _GRASSLAYER 0.96666664
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog
                #include "GrassHelper.cginc"

                ENDCG
            }
        }
    }
}
