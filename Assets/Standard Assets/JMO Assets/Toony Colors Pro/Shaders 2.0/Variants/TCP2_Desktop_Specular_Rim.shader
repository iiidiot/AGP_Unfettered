=============================
		// VARIABLES
		
		fixed4 _Color;
		sampler2D _MainTex;
		
	#if TCP2_BUMP
		sampler2D _BumpMap;
	#endif
		fixed _Shininess;
	#if TCP2_REFLECTION || TCP2_REFLECTION_MASKED
		#if !TCP2_U5_REFLPROBE
		samplerCUBE _Cube;
		#else
		fixed _ReflSmoothness;
		#endif
		fixed4 _ReflectColor;
	#endif
		fixed4 _RimColor;
		fixed _RimMin;
		fixed _RimMax;
		float4 _RimDir;
		
		struct Input
		{
			half2 uv_MainTex : TEXCOORD0;
	#if TCP2_BUMP
			half2 uv_BumpMap : TEXCOORD1;
	#endif
	#if TCP2_REFLECTION || TCP2_REFLECTION_MASKED
			float3 worldRefl;
		#if TCP2_BUMP
			INTERNAL_DATA
		#endif
		#if TCP2_U5_REFLPROBE
			float3 worldPos;
			float3 worldNormal;
		#endif
	#endif
	#if !TCP2_RIMDIR
			float3 viewDir;
	#endif
	#if TCP2_RIMDIR
			float3 bViewDir;
	#endif
		};
		
		//================================================================
		// VERTEX FUNCTION
		
	#if TCP2_RIMDIR
		inline float3 TCP2_ObjSpaceViewDir( in float4 v )
		{
			float3 camPos = _WorldSpaceCameraPos;
			camPos += mul(_RimDir, UNITY_MATRIX_V).xyz;
			float3 objSpaceCameraPos = mul(unity_WorldToObject, float4(camPos, 1)).xyz;
			return objSpaceCameraPos - v.xyz;
		}
	#endif
		
		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			
		#if TCP2_BUMP && TCP2_RIMDIR
			TANGENT_SPACE_ROTATION;
			o.bViewDir = mul(rotation, TCP2_ObjSpaceViewDir(v.vertex));
		#endif
		}
		
		//================================================================
		// SURFACE FUNCTION
		
		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * _Color.rgb;
			o.Alpha = c.a * _Color.a;
			
			//Specular
			o.Gloss = c.a;
			o.Specular = _Shininess;
	#if TCP2_BUMP
			//Normal map
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	#endif
	#if TCP2_REFLECTION || TCP2_REFLECTION_MASKED
			
		#if TCP2_U5_REFLPROBE
			half3 eyeVec = IN.worldPos.xyz - _WorldSpaceCameraPos.xyz;
			#if TCP2_BUMP
			half3 worldNormal = reflect(eyeVec, WorldNormalVector(IN, o.Normal));
			#else
			half3 worldNormal = reflect(eyeVec, IN.worldNormal);
			#endif
			
			float oneMinusRoughness = _ReflSmoothness;
			fixed3 reflColor = fixed3(0,0,0);
			#if UNITY_SPECCUBE_BOX_PROJECTION
			half3 worldNormal0 = BoxProjectedCubemapDirection (worldNormal, IN.worldPos, unity_SpecCube0_ProbePosition, unity_SpecCube0_BoxMin, unity_SpecCube0_BoxMax);
			#else
			half3 worldNormal0 = worldNormal;
			#endif
			half3 env0 = Unity_GlossyEnvironment (UNITY_PASS_TEXCUBE(unity_SpecCube0), unity_SpecCube0_HDR, worldNormal0, 1-oneMinusRoughness);
			
			#if UNITY_SPECCUBE_BLENDING
			const float kBlendFactor = 0.99999;
			float blendLerp = unity_SpecCube0_BoxMin.w;
			UNITY_BRANCH
			if (blendLerp < kBlendFactor)
			{
				#if UNITY_SPECCUBE_BOX_PROJECTION
				half3 worldNormal1 = BoxProjectedCubemapDirection (worldNormal, IN.worldPos, unity_SpecCube1_ProbePosition, unity_SpecCube1_BoxMin, unity_SpecCube1_BoxMax);
				#else
				half3 worldNormal1 = worldNormal;
				#endif
				
				half3 env1 = Unity_GlossyEnvironment (UNITY_PASS_TEXCUBE_SAMPLER(unity_SpecCube1,unity_SpecCube0), unity_SpecCube1_HDR, worldNormal1, 1-oneMinusRoughness);
				reflColor = lerp(env1, env0, blendLerp);
			}
			else
			{
				reflColor = env0;
			}
			#else
			reflColor = env0;
			#endif
			reflColor *= 0.5;
		#else
			#if TCP2_BUMP
			half3 worldRefl = WorldReflectionVector(IN, o.Normal);
			#else
			half3 worldRefl = IN.worldRefl;
			#endif
			fixed4 reflColor = texCUBE(_Cube, worldRefl);
		#endif
		#if TCP2_REFLECTION_MASKED
			reflColor.rgb *= c.a;
		#endif
			reflColor.rgb *= _ReflectColor.rgb * _ReflectCol