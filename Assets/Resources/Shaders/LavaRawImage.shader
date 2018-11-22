Shader "Unfettered/LavaRawImage"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)

		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255

		_ColorMask("Color Mask", Float) = 15

		[HDR]_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_LavaTex("Lava Texture", 2D) = "white" {}
		_DistortTex("Distort Texture", 2D) = "white" {}
		_Speed("Distort Speed", Float) = 1
		_Scale("Distort Scale", Float) = 1

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
	}
		SubShader{
		Tags 
				{
					"Queue" = "Transparent"
					"IgnoreProjector" = "True"
					"RenderType" = "Transparent"
					"PreviewType" = "Plane"
					"CanUseSpriteAtlas" = "True"
				}

				Stencil
				{
					Ref[_Stencil]
					Comp[_StencilComp]
					Pass[_StencilOp]
					ReadMask[_StencilReadMask]
					WriteMask[_StencilWriteMask]
				}

				Cull Off
				Lighting Off
				ZWrite Off
				ZTest[unity_GUIZTestMode]
				Blend SrcAlpha OneMinusSrcAlpha
				ColorMask[_ColorMask]

		LOD 200

		CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Lambert fullforwardshadows

			// Use shader model 3.0 target, to get nicer looking lighting
			//#pragma target 3.0

			sampler2D _LavaTex;
			sampler2D _DistortTex;
			half _Glossiness;
			half _Metallic;
			half4 _TintColor;
			float _Speed;
			float _Scale;

			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex;
				float2 uv_LavaTex;
				float2 uv_DistortTex;
			};

			void surf(Input IN, inout SurfaceOutput o) {
				float4 distort = tex2D(_DistortTex, IN.uv_DistortTex) * 2 - 1;

				float4 mainTexColor = tex2D(_MainTex, IN.uv_MainTex);
				if (mainTexColor.a == 0)
					discard;

				float4 tex = tex2D(_LavaTex, IN.uv_MainTex + distort / 10 * _Scale + _Speed * _Time.xx);
				float4 tex2 = tex2D(_LavaTex, IN.uv_MainTex - distort / 7 * _Scale - _Speed * _Time.xx * 1.4 + float2(0.4, 0.6));
				tex.rgba *= tex2.rgba;
				o.Emission = _TintColor * tex;
				o.Albedo = o.Emission;
			}
			ENDCG
		}
			FallBack "Diffuse"
		//SubShader
		//{
		//	Tags
		//	{
		//		"Queue" = "Transparent"
		//		"IgnoreProjector" = "True"
		//		"RenderType" = "Transparent"
		//		"PreviewType" = "Plane"
		//		"CanUseSpriteAtlas" = "True"
		//	}

		//	Stencil
		//	{
		//		Ref[_Stencil]
		//		Comp[_StencilComp]
		//		Pass[_StencilOp]
		//		ReadMask[_StencilReadMask]
		//		WriteMask[_StencilWriteMask]
		//	}

		//	Cull Off
		//	Lighting Off
		//	ZWrite Off
		//	ZTest[unity_GUIZTestMode]
		//	Blend SrcAlpha OneMinusSrcAlpha
		//	ColorMask[_ColorMask]

		//	Pass
		//	{
		//		Name "Default"
		//	CGPROGRAM
		//		#pragma vertex vert
		//		#pragma fragment frag
		//		#pragma target 2.0

		//		#include "UnityCG.cginc"
		//		#include "UnityUI.cginc"

		//		#pragma multi_compile __ UNITY_UI_CLIP_RECT
		//		#pragma multi_compile __ UNITY_UI_ALPHACLIP

		//		struct appdata_t
		//		{
		//			float4 vertex   : POSITION;
		//			float4 color    : COLOR;
		//			float2 texcoord : TEXCOORD0;
		//			UNITY_VERTEX_INPUT_INSTANCE_ID
		//		};

		//		struct v2f
		//		{
		//			float4 vertex   : SV_POSITION;
		//			fixed4 color : COLOR;
		//			float2 texcoord  : TEXCOORD0;
		//			float4 worldPosition : TEXCOORD1;
		//			UNITY_VERTEX_OUTPUT_STEREO
		//		};

		//		sampler2D _MainTex;
		//		fixed4 _Color;
		//		fixed4 _TextureSampleAdd;
		//		float4 _ClipRect;
		//		float4 _MainTex_ST;

		//		v2f vert(appdata_t v)
		//		{
		//			v2f OUT;
		//			UNITY_SETUP_INSTANCE_ID(v);
		//			UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
		//			OUT.worldPosition = v.vertex;
		//			OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

		//			OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

		//			OUT.color = v.color * _Color;
		//			return OUT;
		//		}

		//		fixed4 frag(v2f IN) : SV_Target
		//		{
		//			half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

		//			#ifdef UNITY_UI_CLIP_RECT
		//			color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
		//			#endif

		//			#ifdef UNITY_UI_ALPHACLIP
		//			clip(color.a - 0.001);
		//			#endif

		//			half4 lavaColor;

		//			if (color.a != 0) // to draw
		//			{

		//			}

		//			return lavaColor;
		//		}
		//	ENDCG
		//	}
		//}
}
