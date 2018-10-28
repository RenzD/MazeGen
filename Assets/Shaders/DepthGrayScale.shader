// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/DepthGrayScale" {
	SubShader{
		Tags{ "RenderType" = "Opaque" }

		Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		sampler2D _CameraDepthTexture;

	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
		float4 scrPos:TEXCOORD1;
	};

	//Vertex Shader
	v2f vert(appdata_base v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.scrPos = ComputeScreenPos(o.pos);
		//for some reason, the y position of the depth texture comes out inverted
		//o.scrPos.y = 1 - o.scrPos.y;
		o.uv = v.texcoord;

		return o;
	}

	//Fragment Shader
	half4 frag(v2f i) : COLOR{
		float4 c = tex2D(_MainTex, i.uv);

		float depthValue = Linear01Depth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos)).r) /0.01;
	half4 depth;

	depth.r = depthValue;
	depth.g = depthValue;
	depth.b = depthValue;

	depth.a = 1;
	float4 result = c;

	//float depth = Linear01Depth(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD(i.projPos)).r);
	//bw = float3(i.depth, i.depth, i.depth);
	result.rgb = lerp(c, depth,0);
	return result;
	//return depth;
	}
		ENDCG
	}
	}
		FallBack "Diffuse"
}