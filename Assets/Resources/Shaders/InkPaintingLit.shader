Shader "Unfettered/InkPaintingLit" {
	
	Properties
	{

		_MainTex("Main Texture", 2D) = "white" { }

		// Colors
		_Color("Color", Color) = (1, 1, 1, 1)
		_SColor("SColor", Color) = (1, 1, 1, 1)
		_HColor("HColor", Color) = (1, 1, 1, 1)

		
		_ToonSteps("Steps of Toon", range(1, 9)) = 2
		_RampThreshold("Ramp Threshold", Float) =0.5
		_RampSmooth("Ramp Smooth", Float) = 0.0
		
		_DiffuseRamp("Diffuse Ramp", 2D) = "white" {}
		_BrushRamp("Brush Ramp", 2D) = "white" {}
		_EdgeThred("Edge Thred", float) = 0.15
		_Tooniness("Tooniness", int) = 3


			// specular
	   _SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	   _SpecSmooth("Specular Smooth", Range(0, 1)) = 0.1
	   _Shininess("Shininess", Range(0.001, 10)) = 0.2

			// rim light
			_RimColor("Rim Color", Color) = (0.8, 0.8, 0.8, 0.6)
			_RimThreshold("Rim Threshold", Range(0, 1)) = 0.5
			_RimSmooth("Rim Smooth", Range(0, 1)) = 0.1

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
			//half _RampThreshold;
			//half _RampSmooth;
			sampler2D _DiffuseRamp;
			sampler2D _BrushRamp;

			float _EdgeThred;

			int _Tooniness;



		

			float _RampThreshold;
			float _RampSmooth;
			float _ToonSteps;

			float _SpecSmooth;
			fixed _Shininess;

			fixed4 _RimColor;
			fixed _RimThreshold;
			float _RimSmooth;

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
				
				
				half diff = NdotL * 0.5 + 0.5;
				half3 ramp2 = tex2D(_DiffuseRamp, float2(diff, diff)).rgb;
			

				// not used
				//fixed3 rampNdotL = smoothstep(_RampThreshold - _RampSmooth * 0.5, _RampThreshold + _RampSmooth * 0.5, NdotL);
				//half3 rampColor = lerp(_SColor.rgb, _HColor.rgb, rampNdotL * atten);


			

				/*float rim = (1.0 - ndv) * ndl;
				rim *= atten;
				rim = smoothstep(_RimThreshold - _RimSmooth * 0.5, _RimThreshold + _RimSmooth * 0.5, rim);


				half3 h = normalize(lightDir + viewDir);

				half diff = max(0, dot(s.Normal, lightDir));

				float nh = max(0, dot(s.Normal, h));
				float spec = pow(nh, 48.0);*/

				
				
				half4 c;
				c.rgb = s.Albedo * _LightColor0.rgb * ramp2 * atten;
	//			c.rgb = s.Albedo * _LightColor0.rgb * rampColor ;
				c.a = s.Alpha;
				return  c;


			}

			float linearstep(float min, float max, float t)
			{
				return saturate((t - min) / (max - min));
			}

			inline fixed4 LightingToon2(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 normalDir = normalize(s.Normal);
				half3 halfDir = normalize(lightDir + viewDir);

				float ndl = max(0, dot(normalDir, lightDir));
				float ndh = max(0, dot(normalDir, halfDir));
				float ndv = max(0, dot(normalDir, viewDir));

				// multi steps
				float diff = smoothstep(_RampThreshold - ndl, _RampThreshold + ndl, ndl);
				float interval = 1 / _ToonSteps;
				// float ramp = floor(diff * _ToonSteps) / _ToonSteps;
				float level = round(diff * _ToonSteps) / _ToonSteps;
				float ramp;
				if (_RampSmooth == 1)
				{
					ramp = interval * linearstep(level - _RampSmooth * interval * 0.5, level + _RampSmooth * interval * 0.5, diff) + level - interval;
				}
				else
				{
					ramp = interval * smoothstep(level - _RampSmooth * interval * 0.5, level + _RampSmooth * interval * 0.5, diff) + level - interval;
				}
				ramp = max(0, ramp);
				ramp *= atten;

				_SColor = lerp(_HColor, _SColor, _SColor.a);
				float3 rampColor = lerp(_SColor.rgb, _HColor.rgb, ramp);

				// specular
				float spec = pow(ndh, s.Specular * 128.0) * s.Gloss;
				spec *= atten;
				spec = smoothstep(0.5 - _SpecSmooth * 0.5, 0.5 + _SpecSmooth * 0.5, spec);

				// rim
				float rim = (1.0 - ndv) * ndl;
				rim *= atten;
				rim = smoothstep(_RimThreshold - _RimSmooth * 0.5, _RimThreshold + _RimSmooth * 0.5, rim);

				fixed3 lightColor = _LightColor0.rgb;

				fixed4 color;
				fixed3 diffuse = s.Albedo * lightColor * rampColor;
				fixed3 specular = _SpecColor.rgb * lightColor * spec;
				fixed3 rimColor = _RimColor.rgb * lightColor * _RimColor.a * rim;

				color.rgb = diffuse + specular + rimColor;
				color.a = s.Alpha;
				return color;
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
