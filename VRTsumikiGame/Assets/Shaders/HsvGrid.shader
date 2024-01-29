Shader "Custom/HsvGrid"
{
	Properties
	{
		_Interval("interval", float) = 1
		_Range("range", float) = 20
		_Speed("Speed", Range(1,2)) = 1
		_Bloom("x", float) = 7
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        struct Input
        {
            float3 worldPos;
        };

		float _Interval;
		float _Range;
		float _Speed;
		float _Bloom;
		
		//HSB→RGB
		fixed3 hsv2rgb(fixed3 c)
		{
			fixed4 K = fixed4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
			fixed3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
			return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float _Hue = (_Time*_Speed);
			_Interval *= (distance(fixed3(0, 0, 0), IN.worldPos)*0.05);
			float initialRange = _Interval / 2;
			float inter = _Range + _Interval;
			fixed3 color = (-initialRange < IN.worldPos.x&&IN.worldPos.x<initialRange || IN.worldPos.x%inter>_Range || IN.worldPos.x%inter < -_Range
				|| -initialRange < IN.worldPos.z&&IN.worldPos.z<initialRange || IN.worldPos.z % inter>_Range || IN.worldPos.z % inter < -_Range) ? hsv2rgb(fixed3(_Hue % 1, 1, 1))*_Bloom : fixed3(0, 0, 0);

			o.Albedo = color;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
