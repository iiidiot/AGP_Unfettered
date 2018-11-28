// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/MeshColor"
{
	Properties
	{
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Cull Off
		
		//Blend One One, DstColor Zero
		Blend SrcAlpha OneMinusSrcAlpha
		BlendOp Add

		//ZWrite Off
		//ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 pos : TEXCOORD0;
				float4 color : COLOR;
			};





			float deal(float a)
			{
				float b;
				if (a <= 0)
				{
					b = -a;
				}
				else
				{
					b = 1 - a;
				}
				return b;

			}
			v2f vert (appdata v)
			{
				v2f o;
				float4 vertex = v.vertex;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.pos = mul(unity_ObjectToWorld, v.vertex);
				float r = v.vertex.x + 0.5;
				float b = v.vertex.z + 0.5;
				float len = length(v.vertex.xz * 2);
				//r = r * 2 * (0.5 - len) + 1 * len;
				//b = b * 2 * (0.5 - len) + len;
				o.color.r = r;
				o.color.g = pow(1 - len, 1);
				o.color.b = b;
				//o.color.rb = normalize(o.color.rb);
				o.color.a = 1;
				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
				float4 c = i.color;
				return c;
			}
			ENDCG
		}
	}
}
