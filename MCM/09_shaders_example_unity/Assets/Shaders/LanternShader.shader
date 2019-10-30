Shader "Unlit/LanternShader"
{
    Properties
    {
		_Color("Color", COLOR) = (0, 0, 0, 1)
		_Angle("Angle", Range(0, 360)) = 0
		_OpenAngle("OpenAngle", Range(0, 180)) = 10
		_PosX("PosX", Range(0, 2000)) = 100
		_PosY("PosY", Range(0, 2000)) = 100
		_Distance("Distance", Range(0, 2000)) = 100
    }
    SubShader
    {
        Tags {
			"Queue" = "Transparent"
			"RenderType"="Transparent"
		}
        LOD 100
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		Lighting Off
		ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

			#define M_PI 3.1415926535897932384626433832795
			fixed4 _Color;
			float _Angle, _OpenAngle;
			float _PosX, _PosY;
			float _Distance;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
            };

            v2f vert (appdata v, out float4 vertex : SV_POSITION)
            {
                v2f o;
                vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i, UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target
            {
				// unity degrees are CCW and 0 = top
				// shader radians are CW and 0 = right
				float _AngleRad = radians(270 - _Angle);

				// vector from lantern position to screen position
				float2 direction = float2(_PosX, _PosY) - screenPos;
				
				// fade alpha from 0 to 1 if direction is longer than _Distance
				float lenAlpha = min(1, (length(direction) - _Distance) / _Distance * 10);

				// calculate and normalize difference of direction angle and lantern angle
				float angle = atan2(direction.y, direction.x);
				float angleDiff = (angle - _AngleRad) / M_PI;
				if (angleDiff > 1)  { angleDiff -= 2; }
				if (angleDiff < -1) { angleDiff += 2; }

				// set alpha to 1 (= show darkness) if the direction angle is > _OpenAngle
				float openAngleFactor = _OpenAngle / 360;
				float angleAlpha = (abs(angleDiff) > openAngleFactor) ? 1 : 0;

				fixed4 col = _Color;
				// use greater alpha for output
				col.a = min(1, max(lenAlpha, angleAlpha));

                return col;
            }
            ENDCG
        }
    }
}
