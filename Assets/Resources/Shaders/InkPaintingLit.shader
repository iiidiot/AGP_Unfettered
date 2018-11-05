Shader "Unfettered/InkPaintingLit" {
	
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		_SColor("SColor", Color) = (1, 1, 1, 1)
		_HColor("HColor", Color) = (1, 1, 1, 1)
		_MainTex("Main Texture", 2D) = "white" { }
		_RampThreshold("Ramp Threshold", Float) =0.5
		_RampSmooth("Ramp Smooth", Float) = 0.0
		_DiffuseRamp("Diffuse Ramp", 2D) = "white" {}
		_BrushRamp("Brush Ramp", 2D) = "white" {}
		_EdgeThred("Edge Thred", float) = 0.15
		_Tooniness("Tooniness", int) = 3

	}

		SubShader
		{
			
			Tags { "RenderType" = "Opaque" }
			Cull Back
			CGPROGRAM

			#pragma surface surf Toon finalcolor:mycolor
			//addshadow fullforwardshadows exclude_path:deferred exclude_path:prepass
			//#pragma target 3.0

			fixed4 _Color;
			fixed4 _SColor;
			fixed4 _HColor;
			sampler2D _MainTex;
			half _RampThreshold;
			half _RampSmooth;
			sampler2D _DiffuseRamp;
			sampler2D _BrushRamp;

			float _EdgeThred;

			int _Tooniness;

			struct Input
			{
				float2 uv_MainTex;
				float3 viewDir;
				float3 worldNormal;
				float3 worldPos;
			};

			half4 SimplifyColor(half4 texColor)
			{
				half4 simplifiedColor = floor(texColor * _Tooniness) / _Tooniness;
				return simplifiedColor;
			}

			inline fixed4 LightingToon(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half NdotL = dot(s.Normal, lightDir);
				
				// not used
				half diff = NdotL * 0.5 + 0.5;
				half3 ramp2 = tex2D(_DiffuseRamp, float2(diff, diff)).rgb;
				
				fixed3 rampNdotL = smoothstep(_RampThreshold - _RampSmooth * 0.5, _RampThreshold + _RampSmooth * 0.5, NdotL);
				half3 rampColor = lerp(_SColor.rgb, _HColor.rgb, rampNdotL * atten);


				float edge = dot(s.Normal, viewDir);
				float edgeOpt = (edge > _EdgeThred) ? 1 : edge * edge;


				/*float rim = (1.0 - ndv) * ndl;
				rim *= atten;
				rim = smoothstep(_RimThreshold - _RimSmooth * 0.5, _RimThreshold + _RimSmooth * 0.5, rim);


				half3 h = normalize(lightDir + viewDir);

				half diff = max(0, dot(s.Normal, lightDir));

				float nh = max(0, dot(s.Normal, h));
				float spec = pow(nh, 48.0);*/

				
				
				half4 c;
				c.rgb = s.Albedo * _LightColor0.rgb * ramp2;
	//			c.rgb = s.Albedo * _LightColor0.rgb * rampColor ;
				c.a = s.Alpha;
				return  c;


			}

			void surf(Input IN, inout SurfaceOutput o)
			{
				fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = mainTex.rgb;

				/*o.Albedo = mainTex.rgb * _Color.rgb;

				o.Alpha = mainTex.a * _Color.a;*/
			}




			void mycolor(Input IN, SurfaceOutput o, inout fixed4 color)
			{
				/*fixed3 worldNormal = normalize(IN.worldNormal);
				fixed3 worldCamDir = normalize(_WorldSpaceCameraPos.xyz - IN.worldPos.xyz);
				float edge = dot(worldNormal, worldCamDir);*/
				float edge = dot(normalize(IN.viewDir), IN.worldNormal);  // 越接近0越接近90度越是边缘

				float edgeOpt = (edge > _EdgeThred) ? 1 : edge * edge;
				half diff = edgeOpt * 0.5 + 0.5;
				half4 rampColor = (1,1,1,1);
				//rampColor.rgb = tex2D(_BrushRamp, float2(diff, diff)).rgb;

				if (edgeOpt < 1)
					rampColor.rgb = tex2D(_BrushRamp, float2(diff, diff)).rgb;



				color = SimplifyColor(color);

				color *= rampColor;
				
				

			}


			ENDCG
		}
		

	FallBack "Diffuse"
}
