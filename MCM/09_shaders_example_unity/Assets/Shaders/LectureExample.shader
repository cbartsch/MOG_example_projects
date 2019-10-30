Shader "Custom/LectureExample"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_TestProperty("TestProperty", Range(0, 1)) = 1
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma geometry geom
				#pragma fragment frag
				// make fog work
				#pragma multi_compile_fog

				#include "UnityCG.cginc"

				float _TestProperty;

            struct appdata
            {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
				//v.vertex.y += sin(v.vertex.x * v.vertex.z / 10);

                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.normal = v.normal;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

			[maxvertexcount(6)]
			void geom(triangle v2f input[3], inout TriangleStream<v2f> OutputStream)
			{
				for (int i = 0; i < 3; i++)
				{
					OutputStream.Append(input[i]);
				}

				OutputStream.RestartStrip();

				for (int i = 0; i < 3; i++)
				{
					v2f newVert = input[i];

					newVert.vertex += float4(newVert.normal * 10, 0);

					OutputStream.Append(newVert);
				}
			}

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
				return col * _TestProperty;
            }
            ENDCG
        }
    }
}
