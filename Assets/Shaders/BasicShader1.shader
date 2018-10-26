// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "COMP7051 Shader Demo/PhongWithSpecular" {
	// Adapted from tutorials on Unity website
	Properties{
		_AmbientLightColor("Ambient Light Color", Color) = (1,1,1,1)
		_AmbientLighIntensity("Ambient Light Intensity", Range(0.0, 1.0)) = 1.0

		_DiffuseDirection("Diffuse Light Direction", Vector) = (0.1,0.1,0.1,1)
		_DiffuseColor("Diffuse Light Color", Color) = (0,0,0,1)
		_DiffuseIntensity("Diffuse Light Intensity", Range(0.0, 1.0)) = 1.0

		_SpecularPosition("Specular Light Position", Vector) = (0.1,0.1,0.1,1)
		_SpecularColor("Specular Light Color", Color) = (1,1,1,1)
		_SpecularIntensity("Specular Light Intensity", Range(0.0, 1.0)) = 1.0
		_SpecularShininess("Shininess", Float) = 10
	}
		SubShader
	{
		Pass
	{
		CGPROGRAM
#pragma target 2.0
#pragma vertex vertexShader
#pragma fragment fragmentShader

		float4 _AmbientLightColor;
	float _AmbientLighIntensity;
	float3 _DiffuseDirection;
	float4 _DiffuseColor;
	float _DiffuseIntensity;
	float4 _SpecularPosition;
	float4 _SpecularColor;
	float _SpecularIntensity;
	float _SpecularShininess;


	struct vsIn {
		float4 position : POSITION;
		float3 normal : NORMAL;
	};

	struct vsOut {
		float4 position : SV_POSITION;
		float3 normal : NORMAL;
		float4 posWorld : TEXCOORD0;
		float3 normalDir : TEXCOORD1;
	};

	vsOut vertexShader(vsIn v)
	{
		vsOut o;
		o.position = UnityObjectToClipPos(v.position);
		o.normal = v.normal;
		float4x4 modelMatrix = unity_ObjectToWorld;
		float4x4 modelMatrixInverse = unity_WorldToObject;
		o.posWorld = mul(modelMatrix, v.position);
		o.normalDir = normalize(mul(float4(v.normal, 0.0), modelMatrixInverse).xyz);
		return o;
	}

	float4 fragmentShader(vsOut psIn) : SV_Target
	{
		float4 diffuse = saturate(dot(_DiffuseDirection, psIn.normal));

		float3 specularReflection;
		float3 normalDirection = normalize(psIn.normalDir);

		float3 viewDirection = normalize(_WorldSpaceCameraPos - psIn.posWorld.xyz);
		float3 lightDirection;
		float attenuation;
		if (0.0 == _SpecularPosition.w) // directional light?
		{
			attenuation = 1.0; // no attenuation
			lightDirection = normalize(_SpecularPosition.xyz);
		}
		else // point or spot light
		{
			float3 vertexToLightSource =
				_SpecularPosition.xyz - psIn.posWorld.xyz;
			float distance = length(vertexToLightSource);
			attenuation = 1.0 / distance; // linear attenuation 
			lightDirection = normalize(vertexToLightSource);
		}

		if (dot(normalDirection, lightDirection) < 0.0)
			// light source on the wrong side?
		{
			specularReflection = float3(0.0, 0.0, 0.0);
			// no specular reflection
		}
		else // light source on the right side
		{
			specularReflection = attenuation * _SpecularColor.rgb * pow(max(0.0, dot(
				reflect(-lightDirection, normalDirection),
				viewDirection)), _SpecularShininess);
		}

		return saturate((_AmbientLightColor * _AmbientLighIntensity)
			+ (diffuse * _DiffuseColor * _DiffuseIntensity) + (_SpecularIntensity * float4(specularReflection,1)));
	}

		ENDCG
	}
	}
}