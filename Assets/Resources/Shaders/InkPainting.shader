Shader "Unfettered/InkPainting" {

	Properties
	{
		_MainTex("main tex",2D) = ""{}
		_BrushFeel("brush feel",2D) = ""{}
	
		_PlainWhiteColor("Plain Color", Color) = (1,1,1,1)
		_EdgeThred("Edge Thred", float) = 0.15
		_Tooniness("Tooniness", int) = 3

		_Brightness("Brightness", Float) = 0   //调整亮度
		_Saturation("Saturation", Float) = 0    //调整饱和度
		_Contrast("Contrast", Float) = 1.19        //调整对比度
			_WhiteBlend("White Blend", Float) = 1
	}
	
	SubShader
	{
		Pass
		{
			Cull Back // 2d效果？？
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"


			fixed4 _PlainWhiteColor;
			float _EdgeThred;
			sampler2D _MainTex;
			sampler2D _BrushFeel;
			int _Tooniness;
			half _Brightness;
			half _Saturation;
			half _Contrast;
			half _WhiteBlend;

			struct a2v {
				float4 vert : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float3 worldNormal : TEXCOORD1;
				float4 worldPos:TEXCOORD2;
				float4 uv:TEXCOORD0;
			};

		
			v2f vert(a2v v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vert);
				o.worldNormal = mul((float3x3)unity_ObjectToWorld, v.normal);
				o.worldPos = mul(unity_ObjectToWorld, v.vert);
				o.uv = v.texcoord;
				return o;
			}


			half4 SimplifyColor(half4 texColor)
			{
				half4 simplifiedColor = floor(texColor * _Tooniness) / _Tooniness;
				return simplifiedColor;
			}


			half4 BrightnessSaturationContrast(half4 renderTex) // 一通xjb操作
			{
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

			fixed4 frag(v2f i) : SV_Target
			{
				fixed3 worldNormal = normalize(i.worldNormal);
				fixed3 worldCamDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
				float edge = dot(worldNormal, worldCamDir);
				float edgeOpt = (edge > _EdgeThred) ? 1 : edge* edge;
				half4 texColor = tex2D(_MainTex, i.uv);

				half4 brushEffect = tex2D(_BrushFeel, i.uv);

				half4 color = SimplifyColor(texColor);
				half4 color2 = BrightnessSaturationContrast(color);

				

				return color * edgeOpt;
				//return i.color;
			}
	ENDCG
	}


			
	}


		
}