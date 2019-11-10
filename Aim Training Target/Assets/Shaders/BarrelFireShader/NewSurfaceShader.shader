Shader "Custom/NewSurfaceShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SecondTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_SpeedScroll("Scroll speed", Range(0,100)) = 10
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows 
		


		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _SecondTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SecondTex;
		};

		half _Glossiness;
		half _Metallic;
		half _SpeedScroll;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed2 scrolledUV = IN.uv_SecondTex;
		
			fixed ScrollValue = _Time * _SpeedScroll/2;
			scrolledUV += fixed2(ScrollValue, 0);
			ScrollValue = _Time * _SpeedScroll / 8;
	
			fixed2 scrolledUV2 = IN.uv_SecondTex - fixed2(ScrollValue, 0);
			fixed4 c = (tex2D (_SecondTex,scrolledUV)* tex2D(_SecondTex, scrolledUV2) * tex2D(_MainTex, IN.uv_MainTex) * _Color ) * tex2D(_MainTex, scrolledUV2/2);
			
			o.Albedo = c.rgb;




			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.rgb;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
