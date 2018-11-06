// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unfettered/TwoPassWithLit" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Ramp("Ramp Texture", 2D) = "white" {}
		//_Ink("Ink Texture", 2D) = "white" {}
		_Tooniness("Tooniness", Range(0.1,20)) = 4
		_OutlineThickness("Detail Outline Thickness", Range(0.01,0.1)) = 0.023
		_EdgeThred("Rough Outline Thickness", Range(0,1)) = 0.3
		_OutLineColor("outline color",Color) = (0,0,0,1)//描边颜色
			
			// rim light
			_RimColor("Rim Color", Color) = (0.8, 0.8, 0.8, 0.6)
			_RimThreshold("Rim Threshold", Range(0, 1)) = 0.5
			_RimSmooth("Rim Smooth", Range(0, 1)) = 0.1

			_HDR("hdr factor", Range(0.01, 10)) = 0.1
			 
	}
		SubShader{
			Tags { "RenderType" = "Transparant" }
			LOD 200

			// outline pass
			Pass {
				Tags { "LightMode" = "ForwardBase" }

				Cull Front
				Lighting Off
				ZWrite On

				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag

				#pragma multi_compile_fwdbase

				#include "UnityCG.cginc"

				float _OutlineThickness;
				half4 _OutLineColor;

				struct a2v
				{
					float4 vertex : POSITION;
					float3 normal : NORMAL;
					float4 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 pos : POSITION;
					float3 color : TEXCOORD1;
				};

				v2f vert(a2v v)
				{
					v2f o;
				
					half4 projSpacePos = UnityObjectToClipPos(v.vertex);
					half4 projSpaceNormal = normalize(UnityObjectToClipPos(half4(v.normal, 0)));

					half4 nv = normalize(v.vertex);
					
					o.color = float3(nv.x * 0.5 + 0.5, (0.5 - nv.x * 0.5), (nv.z * 0.5 + 0.5));

					fixed thicknessFactor = ((nv.x * 0.5 + 0.5) * (0.5 - nv.y * 0.5) * (nv.z * 0.5 + 0.5));
					thicknessFactor = thicknessFactor * thicknessFactor * thicknessFactor;
					thicknessFactor = thicknessFactor / 0.5 + 0.5;

					half4 scaledNormal = _OutlineThickness  * thicknessFactor  * projSpaceNormal; // * projSpacePos.w;

					scaledNormal.z += 0.000001;
					o.pos = projSpacePos + scaledNormal;

					return o;
				
				}

				float4 frag(v2f i) : COLOR
				{
					return _OutLineColor;
				}

				ENDCG
			}

			Pass {
				Tags { "LightMode" = "ForwardBase" }

				Cull Back
				Lighting On

				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag

				#pragma multi_compile_fwdbase

				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "AutoLight.cginc"
				#include "UnityShaderVariables.cginc"


				sampler2D _MainTex;
				sampler2D _Ramp;
				float _EdgeThred;

				float4 _MainTex_ST;
	//			sampler2D _Ink;

				float _Tooniness;

				fixed4 _RimColor;
				fixed _RimThreshold;
				float _RimSmooth;
				float _HDR;

				struct a2v
				{
					float4 vertex : POSITION;
					float3 normal : NORMAL;
					float4 texcoord : TEXCOORD0;
					float4 tangent : TANGENT;
				};

				struct v2f
				{
					float4 pos : POSITION;
					float2 uv : TEXCOORD0;
					float3 normal : TEXCOORD1;
					float4 worldPos:TEXCOORD2;
					LIGHTING_COORDS(2,3)
				};

				v2f vert(a2v v)
				{
					v2f o;

					//Transform the vertex to projection space
					o.pos = UnityObjectToClipPos(v.vertex);
					o.normal = mul((float3x3)unity_ObjectToWorld, SCALED_NORMAL); // world normal
					//Get the UV coordinates
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

					o.worldPos = mul(unity_ObjectToWorld, v.vertex);// world pos

					// pass lighting information to pixel shader
					TRANSFER_VERTEX_TO_FRAGMENT(o);
					return o;
				}

				float4 frag(v2f i) : COLOR
				{
					//Get the color of the pixel from the texture
					float4 c = tex2D(_MainTex, i.uv);
					//Merge the colours
					c.rgb = (floor(c.rgb*_Tooniness) / _Tooniness);

					//Based on the ambient light
					float3 lightColor = UNITY_LIGHTMODEL_AMBIENT.xyz;

					//Work out this distance of the light
					float atten = LIGHT_ATTENUATION(i);
					//Angle to the light
					float ndl = max(0, dot(normalize(i.normal), normalize(_WorldSpaceLightPos0.xyz)));
					float diff = ndl * 0.5 + 0.5;
					//Perform our toon light mapping 
					diff = tex2D(_Ramp, float2(diff, 0.5));
					//Update the colour
					lightColor += _LightColor0.rgb * (diff * atten);
					//Product the final color
					c.rgb = lightColor * c.rgb * 2;


					 //rim prepare
					half3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
					half3 viewDir = normalize(mul((float3x3)unity_CameraToWorld, float3(0, 0, 1)));
					half3 halfDir = normalize(lightDir + viewDir);
					half3 normalDir = normalize(i.normal);
					float ndh = max(0, dot(normalDir, halfDir));
					float ndv = max(0, dot(normalDir, viewDir));
					//rim
					float rim = 1- (1.0 - ndv) * ndl;
					
					rim = smoothstep(_RimThreshold - _RimSmooth * 0.5, _RimThreshold + _RimSmooth * 0.5, rim);
					
					fixed3 rimColor = _RimColor.rgb * lightColor * atten * _RimColor.a * rim;

					c.rgb += rimColor;



					// add non-consistant thickness outline
					fixed3 worldNormal = normalize(i.normal);
					float3 forward = normalize( mul((float3x3)unity_CameraToWorld, float3(0, 0, 1)));
					//fixed3 worldCamDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
					float edge = abs(dot(worldNormal, forward));
					float edgeOpt = (edge > _EdgeThred) ? 1 : 0;

					//float inkA = tex2D(_Ink, float2(i.pos.x, i.pos.y));

					//c.a = inkA;

					c.rgb *= edgeOpt*_HDR;
					c.a = 0.5;
					return c;

				}

				ENDCG
			}

			Pass {
				Tags { "LightMode" = "ForwardAdd" }

				Cull Back
				Lighting On
				Blend One One

				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag

				#pragma multi_compile_fwdadd

				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "AutoLight.cginc"
				#include "UnityShaderVariables.cginc"


				sampler2D _MainTex;
				sampler2D _Ramp;
//				sampler2D _Ink;

				float4 _MainTex_ST;

				float _EdgeThred;
				float _Tooniness;
				float _HDR;

				struct a2v
				{
					float4 vertex : POSITION;
					float3 normal : NORMAL;
					float4 texcoord : TEXCOORD0;
					float4 tangent : TANGENT;
				};

				struct v2f
				{
					float4 pos : POSITION;
					float2 uv : TEXCOORD0;
					float3 normal : TEXCOORD1;
					half3 lightDir : TEXCOORD2;
					LIGHTING_COORDS(3, 4)
					float4 worldPos:TEXCOORD5;
				};

				v2f vert(a2v v)
				{
					v2f o;

					//Transform the vertex to projection space
					o.pos = UnityObjectToClipPos(v.vertex);
					o.normal = mul((float3x3)unity_ObjectToWorld, SCALED_NORMAL);
					o.lightDir = WorldSpaceLightDir(v.vertex);
					//Get the UV coordinates
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

					o.worldPos = mul(unity_ObjectToWorld, v.vertex);// world pos

					// pass lighting information to pixel shader
					TRANSFER_VERTEX_TO_FRAGMENT(o);
					return o;
				}

				float4 frag(v2f i) : COLOR
				{
					//Get the color of the pixel from the texture
					float4 c = tex2D(_MainTex, i.uv);
					//Merge the colours
					c.rgb = (floor(c.rgb*_Tooniness) / _Tooniness);

					//Based on the ambient light
					float3 lightColor = float3(0,0,0);

					//Work out this distance of the light
					float atten = LIGHT_ATTENUATION(i);
					//Angle to the light
					float diff = dot(normalize(i.normal), normalize(i.lightDir));
					diff = diff * 0.5 + 0.5;
					//Perform our toon light mapping 
					diff = tex2D(_Ramp, float2(diff, 0.5));
					//Update the colour
					lightColor += _LightColor0.rgb * (diff * atten);
					//Product the final color
					c.rgb = lightColor * c.rgb * 2;


				
				
					


					// add non-consistant thickness outline
				
					fixed3 worldNormal = normalize(i.normal);
					float3 forward = normalize(mul((float3x3)unity_CameraToWorld, float3(0, 0, 1)));
					fixed3 worldCamDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
					float edge = abs(dot(worldNormal, forward));

				
					float edgeOpt = (edge > _EdgeThred) ? 1 : 0;

				

					// 这个叠法还不如直接p纹理图。。
					/*float2 newuv = normalize(float2(i.pos.x, i.pos.y)) * 0.5 + 0.5;
					float inkA = tex2D(_Ink, float2(0,0));

					c.a = inkA;*/

					c.rgb *= edgeOpt*_HDR;
					c.a = 0.5;
					return c;
				}

				ENDCG
			}
		}
			FallBack "Diffuse"
}