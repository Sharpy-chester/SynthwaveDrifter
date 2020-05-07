Shader "SupGames/Mobile/BloomURP"
{
	Properties
	{
		[HideInInspector]_MainTex("Base (RGB)", 2D) = "white" {}
	}
	HLSLINCLUDE

	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

	TEXTURE2D(_MainTex);
	SAMPLER(sampler_MainTex);
	TEXTURE2D(_BlurTex);
	SAMPLER(sampler_BlurTex);

	half4 _MainTex_TexelSize;
	half _BlurAmount;
	half4 _BloomColor;
	half _BloomAmount;
	half _Threshold;


	struct appdata {
		half4 pos : POSITION;
		half2 uv : TEXCOORD0;
	};

	struct v2fb {
		half4 pos : SV_POSITION;
		half4 uv : TEXCOORD0;
	};

	struct v2f {
		half4 pos : SV_POSITION;
		half2 uv  : TEXCOORD0;
	};

	v2f vert(appdata i)
	{
		v2f o = (v2f)0;
		o.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, half4(i.pos.xyz, 1.0h)));
		o.uv = i.uv;
		return o;
	}

	v2fb vertBlur(appdata i)
	{
		v2fb o = (v2fb)0;
		o.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, half4(i.pos.xyz, 1.0h)));
		half2 offset = _MainTex_TexelSize.xy * _BlurAmount;
		o.uv = half4(i.uv - offset, i.uv + offset);
		return o;
	}

	half4 fragBloom(v2fb i) : SV_Target
	{
		half4 result = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.xy);
		result += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.xw);
		result += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.zy);
		result += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.zw);
		return max(result * 0.25h - _Threshold, 0.0h);
	}

	half4 fragBlur(v2fb i) : COLOR
	{
		half4 result = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.xy);
		result += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.xw);
		result += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.zy);
		result += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.zw);
		return result * 0.25h;
	}

	half4 frag(v2f i) : COLOR
	{
		half4 c = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
		half4 b = SAMPLE_TEXTURE2D(_BlurTex, sampler_BlurTex, i.uv) * _BloomColor * _BloomAmount;
		return (c + b) * 0.5h;
	}

	ENDHLSL

	Subshader
	{
		Pass //0
		{
		  ZTest Always Cull Off ZWrite Off
		  Fog { Mode off }
		  HLSLPROGRAM
		  #pragma vertex vertBlur
		  #pragma fragment fragBloom
		  #pragma fragmentoption ARB_precision_hint_fastest
		  ENDHLSL
		}

		Pass //1
		{
		  ZTest Always Cull Off ZWrite Off
		  Fog { Mode off }
		  HLSLPROGRAM
		  #pragma vertex vertBlur
		  #pragma fragment fragBlur
		  #pragma fragmentoption ARB_precision_hint_fastest
		  ENDHLSL
		}

		Pass //2
		{
		  ZTest Always Cull Off ZWrite Off
		  Fog { Mode off }
		  HLSLPROGRAM
		  #pragma vertex vert
		  #pragma fragment frag
		  #pragma fragmentoption ARB_precision_hint_fastest
		  ENDHLSL
		}
	}
	Fallback off
}