Shader "Custom/TestVertexShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Length("Length", Range(0, 0.3)) = 0.12
		_Intensity("Intensity", Range(0, 5)) = 1
		_StartValue("Start value", Range(0, 1)) = 0
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
		float _Length;
		float _Intensity;
		float _StartValue;

		v2f vert(appdata v)
		{
			v.vertex.y += sin(v.vertex.x * v.vertex.z * _Length +
				_StartValue * 2 * 3.14159) * _Intensity;

			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			UNITY_TRANSFER_FOG(o,o.vertex);
			return o;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			// sample the texture
			fixed4 col = tex2D(_MainTex, i.uv);
			// apply fog
			UNITY_APPLY_FOG(i.fogCoord, col);

			return col;
		}
		ENDCG
		}
	}
}