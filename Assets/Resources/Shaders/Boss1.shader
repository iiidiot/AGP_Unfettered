Shader "Unfettered/Boss1" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		[HDR]_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_LavaTex("Lava Texture", 2D) = "white" {}
		_DistortTex("Distort Texture", 2D) = "white" {}

		_EmissionTex("Emission Texture", 2D) = "white" {}

		_Speed("Distort Speed", Float) = 1
		_Scale("Distort Scale", Float) = 1

		_Threshold("Dark Red Threshold", Range(0,1)) = 0.0

		_BumpMap("Bumpmap", 2D) = "bump" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _DistortTex;
		sampler2D _LavaTex;
		half4 _TintColor;
		float _Speed;
		float _Scale;

		struct Input {
			float2 uv_MainTex;
			float2 uv_LavaTex;
			float2 uv_DistortTex;
			float2 uv_BumpMap;
			float2 uv_EmissionTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _Threshold;
		sampler2D _BumpMap;
		sampler2D _EmissionTex;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 e = tex2D(_EmissionTex, IN.uv_EmissionTex) * _Color;
			fixed3 diff = c.rgb - fixed3(82.0/256.0, 16.0/256.0, 41.0/256.0);
		
			if (e.a > 0 || abs(diff.r) + abs(diff.g) + abs(diff.b) < _Threshold)
			{
				float4 distort = tex2D(_DistortTex, IN.uv_DistortTex) * 2 - 1;
				float4 tex = tex2D(_LavaTex, IN.uv_LavaTex + distort / 10 * _Scale + _Speed * _Time.xx);
				float4 tex2 = tex2D(_LavaTex, IN.uv_LavaTex - distort / 7 * _Scale - _Speed * _Time.xx * 1.4 + float2(0.4, 0.6));
				tex.rgba *= tex2.rgba;
				o.Emission = _TintColor * tex;
				o.Albedo = o.Emission;
			}
			else {
				o.Albedo = c.rgb;
				// Metallic and smoothness come from slider variables
				//o.Metallic = _Metallic;
				//o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));


		}
		ENDCG
	}
	FallBack "Diffuse"
}
