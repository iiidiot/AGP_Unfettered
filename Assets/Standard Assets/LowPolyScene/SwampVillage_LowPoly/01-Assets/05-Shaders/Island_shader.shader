// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Island_shader"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Color0("Color 0", Color) = (0,0,0,0)
		_Color1("Color 1", Color) = (0,0,0,0)
		_Metal("Metal", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 texcoord_0;
		};

		uniform float4 _Color1;
		uniform float4 _Color0;
		uniform float _Metal;
		uniform float _Smoothness;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.texcoord_0.xy = v.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 lerpResult2 = lerp( _Color1 , _Color0 , length( (float2( 0,0 ) + (i.texcoord_0 - float2( 0,0 )) * (float2( 1,0 ) - float2( 0,0 )) / (float2( 1,0 ) - float2( 0,0 ))) ));
			o.Albedo = lerpResult2.rgb;
			o.Metallic = _Metal;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13101
7;29;1906;1004;1448.397;707.1255;1.248649;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-956.3818,136.2869;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.TFHCRemap;11;-569.3486,171.9237;Float;True;5;0;FLOAT2;0.0;False;1;FLOAT2;0,0;False;2;FLOAT2;1,0;False;3;FLOAT2;0,0;False;4;FLOAT2;1,0;False;1;FLOAT2
Node;AmplifyShaderEditor.ColorNode;4;-571.5,-412;Float;False;Property;_Color1;Color 1;1;0;0,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;3;-571.4946,-238.2271;Float;False;Property;_Color0;Color 0;0;0;0,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LengthOpNode;12;-260.9319,173.1721;Float;True;1;0;FLOAT2;0,0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;10;126.1492,-26.61127;Float;False;Property;_Metal;Metal;2;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;2;-139.6245,-161.2647;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0.0;False;2;FLOAT;0.0;False;1;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;9;124.9006,59.54559;Float;False;Property;_Smoothness;Smoothness;3;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;556,-123;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Island_shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;11;0;8;0
WireConnection;12;0;11;0
WireConnection;2;0;4;0
WireConnection;2;1;3;0
WireConnection;2;2;12;0
WireConnection;0;0;2;0
WireConnection;0;3;10;0
WireConnection;0;4;9;0
ASEEND*/
//CHKSM=D4B331B32AF543212DFA2FE91860B0F3A269036D