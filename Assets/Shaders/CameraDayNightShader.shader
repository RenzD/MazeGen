Shader "Custom/CameraDayNightShader" {
	Properties{

		_MainTex("Base (RGB)", 2D) = "white" {}
		_bwBlend("Day & Night blend", Range(0, 1)) = 0.25
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
						
						float3 dn = float3(0, 0, 0);
						float4 result = c;
						result.rgb = lerp(c.rgb, dn, _bwBlend);
						return result;
				}
			ENDCG
		}
	}
}
