Shader "Unfettered/OilPaintCombine"
{
	Properties
	{
		_MainTex("Main Texture",2D) = "white"{}			// 绘制完物体面积后纹理
		_SceneTex("Scene Texture",2D) = "white"{}			// 原场景纹理
		
	}
		SubShader
		{
			Tags { "Queue" = "Transparent" }
			Pass
			{
				ZTest Always Cull Off ZWrite Off

				CGPROGRAM

				sampler2D _MainTex;
				sampler2D _SceneTex;	

				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				struct v2f
				{
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				v2f vert(appdata_base v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = v.texcoord.xy;
					return o;
				}

				half4 frag(v2f i) : COLOR
				{
					//return half4(1,1,1,1);
				//	// 如果该像素有颜色（原来所占面积），或者该像素不在外边范围内，直接渲染原场景。否则就渲染为外边颜色。
					if (tex2D(_MainTex,i.uv).r > 0 || tex2D(_MainTex,i.uv).g > 0 || tex2D(_MainTex,i.uv).b > 0)
						return tex2D(_MainTex, i.uv);
					else
						return tex2D(_SceneTex, i.uv);
				//
				}
				ENDCG
			}

		}
}
