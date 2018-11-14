Shader "Unfettered/PostInkEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_CameraDepthNormalsTexture ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _CameraDepthNormalsTexture;

			float rgb2gray(fixed3 col) {
				float gray = 0.2125 * col.r + 0.7154 * col.g + 0.0721 * col.b;
				return gray;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				return fixed4(0,0,0,0);
				//float2 texel = 0;//????
				////判断是否是边缘
				//fixed3 col0 = tex2D(_CameraDepthNormalsTexture,i.uv + _EdgeWidth * texel*float2(1,1)).xyz;
				//fixed3 col1 = tex2D(_CameraDepthNormalsTexture,i.uv + _EdgeWidth * texel*float2(1,-1)).xyz;
				//fixed3 col2 = tex2D(_CameraDepthNormalsTexture,i.uv + _EdgeWidth * texel*float2(-1,1)).xyz;
				//fixed3 col3 = tex2D(_CameraDepthNormalsTexture,i.uv + _EdgeWidth * texel*float2(-1,-1)).xyz;
				//float edge = rgb2gray(pow(col0 - col3,2) + pow(col1 - col2,2));
		
				//edge = pow(edge, 0.2);
				//if (edge < _Sensitive) 
				//	edge = 0;
				//return fixed4(edge, edge, edge, 1.0);

				// https://blog.csdn.net/nannan0811666/article/details/79452197
				
			}
			ENDCG
		}
	}
}
