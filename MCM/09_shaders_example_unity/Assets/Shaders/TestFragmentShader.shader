Shader "Custom/TestFragmentShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Radius("Radius", Range(0, 0.01)) = 0.005
		_Samples("Samples", Int) = 1
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
				// make fog work
		#pragma multi_compile_fog

		#include "UnityCG.cginc"

		struct appdata
		{
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
			float3 normal : NORMAL;
		};

		struct v2f
		{
			float2 uv : TEXCOORD0;
			UNITY_FOG_COORDS(1)
			float4 vertex : SV_POSITION;
		};

		sampler2D _MainTex;
		float4 _MainTex_ST;

		float _Radius;
		int _Samples;

		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			UNITY_TRANSFER_FOG(o,o.vertex);
			return o;
		}

		fixed4 frag(v2f i) : SV_Target 
		{
            fixed4 col = fixed4(0, 0, 0, 0);

			int iterations = _Samples * 2 + 1;

			// compute average of surrounding texture pixels in a square of length iterations (= gaussian blur)
			for (int j = 0; j < iterations * iterations; j++) {
				float x = ((j % iterations) - _Samples) * _Radius;
				float y = ((j / iterations) - _Samples) * _Radius;
				col += tex2D(_MainTex, i.uv + float2(x, y));
			}
			
			col /= iterations * iterations;

			return col;
		}
		ENDCG
		}
	}
}