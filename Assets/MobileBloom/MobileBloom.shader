Shader "SupGames/Mobile/Bloom"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "" {}
	}

	CGINCLUDE
	#include "UnityCG.cginc"
	struct appdata {
		fixed4 pos : POSITION;
		fixed2 uv : TEXCOORD0;
	};

	struct v2fb {
		fixed4 pos : POSITION;
		fixed4 uv : TEXCOORD0;
	};

	struct v2f {
		fixed4 pos : POSITION;
		fixed2 uv : TEXCOORD0;
	};

	sampler2D _MainTex;
	sampler2D _BloomTex;
	fixed4 _MainTex_TexelSize;
	fixed _BlurAmount;
	fixed4 _BloomColor;
	fixed _BloomAmount;
	fixed _Threshold;

	v2fb vertBlur(appdata i)
	{
		v2fb o;
		o.pos = UnityObjectToClipPos(i.pos);
		fixed2 offset = _MainTex_TexelSize.xy * _BlurAmount;
		o.uv = fixed4(i.uv - offset, i.uv + offset);
		return o;
	}

	v2f vert(appdata i)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(i.pos);
		o.uv = i.uv;
		return o;
	}

	fixed4 fragBloom(v2fb i) : COLOR
	{
		fixed4 result = tex2D(_MainTex, i.uv.xy);
		result += tex2D(_MainTex, i.uv.xw);
		result += tex2D(_MainTex, i.uv.zy);
		result += tex2D(_MainTex, i.uv.zw);
		return max(result * 0.25h - _Threshold, 0.0h);
	}

	fixed4 fragBlur(v2fb i) : COLOR
	{
		fixed4 result = tex2D(_MainTex, i.uv.xy);
		result += tex2D(_MainTex, i.uv.xw);
		result += tex2D(_MainTex, i.uv.zy);
		result += tex2D(_MainTex, i.uv.zw);
		return result * 0.25h;
	}

	fixed4 frag(v2f i) : COLOR
	{
		fixed4 c = tex2D(_MainTex, i.uv);
		fixed4 b = tex2D(_BloomTex, i.uv) * _BloomColor * _BloomAmount;
		return (c + b)*0.5h;
	}
	ENDCG 
		
	Subshader 
	{
		Pass //0
		{
		  ZTest Always Cull Off ZWrite Off
		  Fog { Mode off }
		  CGPROGRAM
		  #pragma vertex vertBlur
		  #pragma fragment fragBloom
		  #pragma fragmentoption ARB_precision_hint_fastest
		  ENDCG
		}

		Pass //1
		{
		  ZTest Always Cull Off ZWrite Off
		  Fog { Mode off }
		  CGPROGRAM
		  #pragma vertex vertBlur
		  #pragma fragment fragBlur
		  #pragma fragmentoption ARB_precision_hint_fastest
		  ENDCG
		}
		
		Pass //2
		{
		  ZTest Always Cull Off ZWrite Off
		  Fog { Mode off }
		  CGPROGRAM
		  #pragma vertex vert
		  #pragma fragment frag
		  #pragma fragmentoption ARB_precision_hint_fastest
		  ENDCG
		}
	}
	Fallback off
}