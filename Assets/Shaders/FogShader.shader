// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/FogShader" {
	Properties{

		_MainTex("Base (RGB)", 2D) = "white" {}
		_bwBlend("Black & White blend", Range(0, 1)) = 0.01
		_fogColor("Fog Color", Color) = (1,1,1,1)
	}
		SubShader{
			Pass{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				uniform sampler2D _MainTex;
				sampler2D _CameraDepthTexture; //the depth texture
				uniform float _bwBlend;
				float4 _fogColor;

				struct v2f {
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
					float4 scrPos : TEXCOORD1; //Screen position of pos
				};

				v2f vert(appdata_img v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.scrPos = ComputeScreenPos(o.pos);
					o.uv = v.texcoord;
					return o;
				}
				
				float4 frag(v2f i) : COLOR{
					float4 c = tex2D(_MainTex, i.uv);
					float depthValue = Linear01Depth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos)).r) / _bwBlend;

					float3 bw = _fogColor.xyz;
					float4 result = c;
					result.rgb = lerp(c.rgb, bw, depthValue);
					return result;
				}
			ENDCG
		}
	}
}