Shader "Unfettered/SpriteGreyWhiteBlend"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		_Brightness("Brightness", Float) = 0   //调整亮度
		_Saturation("Saturation", Float) = 0    //调整饱和度
		_Contrast("Contrast", Float) = 1        //调整对比度
			_WhiteBlend("White Blend", Float) = 1        
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
			half _Brightness;
			half _Saturation;
			half _Contrast;
			half _WhiteBlend;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 renderTex = tex2D(_MainTex, i.uv);
				
				
				fixed3 finalColor = renderTex * _Brightness;
				fixed gray = 0.2125 * renderTex.r + 0.7154 * renderTex.g + 0.0721 * renderTex.b;
				gray = (_WhiteBlend * gray > 1) ? 1 : _WhiteBlend * gray;
				fixed3 grayColor = fixed3(gray, gray, gray);
				//根据Saturation在饱和度最低的图像和原图之间差值
				finalColor = lerp(grayColor, finalColor, _Saturation);
				//contrast对比度：首先计算对比度最低的值
				fixed3 avgColor = fixed3(0.5, 0.5, 0.5);
				//根据Contrast在对比度最低的图像和原图之间差值
				finalColor = lerp(avgColor, finalColor, _Contrast);
				
				

				return fixed4(finalColor, renderTex.a);
				

			}
			ENDCG
		}
	}
}
