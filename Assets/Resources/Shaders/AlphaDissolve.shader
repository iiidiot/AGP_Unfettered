Shader "Unfettered/AlphaDissolve"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DissolveTex("DissolveTex", 2D) = "white" {}
		_Threshold("Threshold", Range(0,1)) = 0
	}
	SubShader
	{
		Blend SrcAlpha OneMinusSrcAlpha
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

		

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};


			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _DissolveTex;
			float4 _DissolveTex_ST;
			fixed _Threshold;
			

			v2f vert (appdata_base  v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
		
			fixed4 frag (v2f i) : SV_Target
			{
				
				fixed4 col = tex2D(_MainTex, i.uv);

				float2 uv = TRANSFORM_TEX(i.uv, _DissolveTex);
				fixed noize = tex2D(_DissolveTex, uv).r;
				if (noize < _Threshold) {
					discard;
				}
				
				// just invert the colors
				
				return fixed4(0,0,0,1);
			}
			ENDCG
		}
	}
}
