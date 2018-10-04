Shader "DarkEffect"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_ItemPos("Item Pos", Vector) = (0,0,0)
		_LightRadius("Light Radius", float) = 300
	}
		SubShader
		{
			Tags {"Queue" = "Overlay" "RenderType" = "Transparent" }
			LOD 100
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

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
					float3 worldPos: TEXCOORD7;
				};

				sampler2D _MainTex;

				float3 _ItemPos;
				float _LightRadius;

				float4 _MainTex_ST;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.worldPos = mul(unity_ObjectToWorld, v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					return o;
				}

				fixed alphaMaskHelper(float3 pointWorldPos, float3 itemPos, float r)
				{
					float d = distance(pointWorldPos, itemPos);
					float factor = 1;
					if (d < r)
					{
						factor = (r-d) / r * 0.8;
						factor = 1-factor*factor*factor;
					}
					return factor;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv);
					col.a = col.a * alphaMaskHelper(i.worldPos, _ItemPos, _LightRadius);
					return col;
				}
				ENDCG
			}
		}
}
