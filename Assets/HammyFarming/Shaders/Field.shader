Shader "Hammy/Field"
{
	Properties
	{
		_UntilledTexture("Untilled Texture", 2D) = "white" {}
		_TilledTexture ("Tilled Texture", 2D) = "white" {}
		_DeformTex("Deformation Texture", 2D) = "white" {}
		_BorderTex("Border Texture", 2D) = "black" {}
		_WetColor ("Wet Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_DryColor("Dry Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_HeightScale("Height Scale", Float) = 1
		_Resolution("Resolution", float) = 10.0
	}
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert
        #pragma target 3.0
		#include "UnityCG.cginc"
		#include "AutoLight.cginc"

        sampler2D _UntilledTexture;
		sampler2D _TilledTexture;
		sampler2D _DeformTex;
		sampler2D _BorderTex;
        struct Input
        {
            float2 uv_UntilledTexture;
			float2 uv_TilledTexture;
			float2 uv_DeformTex;
			float2 uv_BorderTex;
        };
		

        half _Glossiness;
        half _Metallic;

		float _HeightScale;

		float4 _WetColor;
		float4 _DryColor;

		float _Resolution;

		void vert(inout appdata_full v, out Input o) {
			
			float y = tex2Dlod(_DeformTex, float4(v.vertex.xz, 0.0, 0.0)).r * tex2Dlod(_BorderTex, float4(v.vertex.xz, 0.0, 0.0)) * _HeightScale;
			v.vertex.y = y;
			//Estimate neighbor positions
			float3 vertNX = float3(v.vertex.x - 1.0/_Resolution, v.vertex.yz);
			vertNX.y = tex2Dlod(_DeformTex, float4(vertNX.xz, 0.0, 0.0)).r * tex2Dlod(_BorderTex, float4(vertNX.xz, 0.0, 0.0)) * _HeightScale;
			float3 vertNZ = float3(v.vertex.xy, v.vertex.z - 1.0/_Resolution);
			vertNZ.y = tex2Dlod(_DeformTex, float4(vertNZ.xz, 0.0, 0.0)).r * tex2Dlod(_BorderTex, float4(vertNZ.xz, 0.0, 0.0)) * _HeightScale;
			float3 vertPX = float3(v.vertex.x + 1.0/_Resolution, v.vertex.yz);
			vertPX.y = tex2Dlod(_DeformTex, float4(vertPX.xz, 0.0, 0.0)).r * tex2Dlod(_BorderTex, float4(vertPX.xz, 0.0, 0.0)) * _HeightScale;
			float3 vertPZ = float3(v.vertex.xy, v.vertex.z + 1.0/_Resolution);
			vertPZ.y = tex2Dlod(_DeformTex, float4(vertPZ.xz, 0.0, 0.0)).r * tex2Dlod(_BorderTex, float4(vertPZ.xz, 0.0, 0.0)) * _HeightScale;

			float3 U = vertNX - v.vertex.xyz;
			float3 V = vertNZ - v.vertex.xyz;
			float3 X = vertPX - v.vertex.xyz;
			float3 W = vertPZ - v.vertex.xyz;
			float3 norm = float3(0.0, 0.0, 0.0);//ac + ab;//cross(ac, ab);// * (b - a);

			norm = cross(U, V) + cross(V, X) + cross(X, W) + cross(W, U);
			norm = norm / length(norm);
			
			//o.color = float4(v.vertex.x - 1.0/_Resolution, v.vertex.x, 0.0, 0.0);
			v.normal = float4(-norm.xyz, 0.0);

			UNITY_INITIALIZE_OUTPUT(Input,o);
			
			// o.color = norm;//length(norm);
			// TRANSFER_SHADOW(v);
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            float4 tilledColor = tex2D (_UntilledTexture, IN.uv_UntilledTexture);
			float4 untilledColor = tex2D(_TilledTexture, IN.uv_TilledTexture);
			float4 deformColor = tex2D(_DeformTex, IN.uv_DeformTex);
			
            o.Albedo = lerp(untilledColor, tilledColor, 1 - deformColor.g) * lerp(_WetColor, _DryColor, 1- deformColor.b);
            // Metallic and smoothness come from slider variables
			o.Smoothness = 0;
			o.Metallic = -0;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
