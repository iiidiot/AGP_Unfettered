Shader "Unfettered/InkPaintingLit" {
	
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Main Texture", 2D) = "white" { }
		_RampThreshold("Ramp Threshold", Float) = 1.19
		_RampSmooth("Ramp Smooth", Float) = 1.19

	}

		SubShader
	{
		Tags { "RenderType" = "Opaque" }

		CGPROGRAM

		#pragma surface surf Toon addshadow fullforwardshadows exclude_path:deferred exclude_path:prepass
		#pragma target 3.0

		fixed4 _Color;
		sampler2D _MainTex;
		half _RampThreshold;
		half _RampSmooth;


		struct Input
		{
			float2 uv_MainTex;
			float3 viewDir;
		};

		inline fixed4 LightingToon(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
		{
			half3 normalDir = normalize(s.Normal);
			float ndl = max(0, dot(normalDir, lightDir));

			fixed3 lightColor = _LightColor0.rgb;

			fixed3 ramp = smoothstep(_RampThreshold - _RampSmooth * 0.5, _RampThreshold + _RampSmooth * 0.5, ndl);
			ramp *= atten;
			
			fixed3 diffuse = s.Albedo * lightColor * ramp;

			fixed4 color;
			//fixed3 diffuse = s.Albedo * lightColor * ndl * atten;

			color.rgb = diffuse;
			color.a = s.Alpha;
			return color;
		}

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = mainTex.rgb * _Color.rgb;

			o.Alpha = mainTex.a * _Color.a;
		}

		ENDCG
	}

	FallBack "Diffuse"
}
