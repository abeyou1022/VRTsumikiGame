Shader "Custom/VerWave"
{
    Properties
    {
		_Val("Val",Range(0,1)) = 0.99
		_Speed("Speed", Range(1,10)) = 3.5
		_Bloom("Bloom", float) = 10
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

		float _Val;
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
			float val = abs(sin(IN.worldPos.y-_Time*100));
            o.Albedo = (val>_Val)? hsv2rgb(fixed3(_Hue % 1, 1, 1))*_Bloom : fixed4(0,0,0,1);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
