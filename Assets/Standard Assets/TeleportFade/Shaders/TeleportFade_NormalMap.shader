// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "TeleportFade/Standerd (Normal Map)" {
	Properties {
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("Normal Map", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_FadeTex("Fade Texture", 2D) = "white" {}
		_RisePower("Rise Power", Float) = 0.1
		_TwistPower("Twist Power", Float) = 0.0
		_SpreadPower("Spread Power", Float) = 1.0
		_ParticleColor("Particle Color", Color) = (0.0,0.3,0.8,1)
		_FadeRate("Fade Rate", Range(0, 1)) = 0.5
		_ObjectHeight("Object Height", Float) = 2.0
		_ObjectFadeHeight("Object Fade Height", Float) = 0.2
		_ObjectBasePos("Object Base Position", Vector) = (0,0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _FadeTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float4 packedData;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		half _FadeRate;
		half _ObjectFadeHeight;
		half _RisePower;
		half _SpreadPower;
		half _TwistPower;
		fixed4 _ParticleColor;
		half _ObjectHeight;
		float4 _ObjectBasePos;

		void vert(inout appdata_full v, out Input data)
		{
			UNITY_INITIALIZE_OUTPUT(Input, data);
			float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
			float height = lerp(_ObjectHeight, -_ObjectFadeHeight, _FadeRate);
			float warpRate = saturate((worldPos.y - (height + _ObjectBasePos.y)) / _ObjectFadeHeight);
			warpRate *= warpRate;
			worldPos.y += warpRate * _RisePower;
			float2 dir = worldPos.xz - _ObjectBasePos.xz;
			float rot = warpRate * _TwistPower;
			worldPos.x = cos(rot) * dir.x - sin(rot) * dir.y;
			worldPos.z = sin(rot) * dir.x + cos(rot) * dir.y;
			worldPos.xz *= _SpreadPower * warpRate + 1.0;
			worldPos.xz += _ObjectBasePos.xz;
			v.vertex = mul(unity_WorldToObject, worldPos);

			data.packedData.z = warpRate;
			data.packedData.xw = UnityObjectToClipPos(v.vertex).xw;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			half2 uv;
			uv.x = _ScreenParams.x * IN.packedData.x / IN.packedData.w / 128.0;
			uv.y = saturate(IN.packedData.z - 0.01);
			fixed4 fadeColor = tex2D(_FadeTex, uv);
			clip(fadeColor.r - 0.1);
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Emission = _ParticleColor.rgb * _ParticleColor.a * saturate(IN.packedData.z * 2.0);
			o.Alpha = c.a;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		}
		ENDCG
	}
	FallBack "Diffuse"
}
