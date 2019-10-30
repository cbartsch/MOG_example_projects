// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/TestGeometryShader" {

	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Distance("Distance", Range(0, 100)) = 2
		_Count("Count", Int) = 1
		_Fade("Fade", Range(0, 1)) = 1
	}
	SubShader
	{
		Tags { 
			"Queue" = "Transparent"
			"RenderType" = "Opaque" 
		}
		LOD 100
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma geometry geom

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 worldPosition : TEXCOORD1;
				float4 color : COLOR;
			};

			int _Count;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Distance;
			float _Fade;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.normal = v.normal;
				o.worldPosition = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.color = float4(1, 1, 1, 1);
				return o;
			}

			[maxvertexcount(15)]
			void geom(triangle v2f input[3], inout TriangleStream<v2f> OutputStream)
			{
				for (int i = 0; i < 3; i++)
				{
					OutputStream.Append(input[i]);
				} 

				for (int n = 1; n <= _Count; n++) 
				{
					OutputStream.RestartStrip();

					for (int j = 0; j < 3; j++)
					{
						v2f generated = input[j];
						float3 move = generated.normal * _Distance * n;

						generated.worldPosition += move;
						generated.vertex += mul(UNITY_MATRIX_VP, float4(move, 0));
						generated.color.a = 1.0 - _Fade * (float(n) / (_Count + 1));
						OutputStream.Append(generated);
					}
				}
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				col.a *= i.color.a;

				float3 lightDir = _WorldSpaceLightPos0.xyz;
				float ndotl = dot(i.normal, normalize(lightDir));

				return col * (clamp(ndotl, 0, 1) * 0.7 + 0.3);
			}
			ENDCG
		}
	}
}
