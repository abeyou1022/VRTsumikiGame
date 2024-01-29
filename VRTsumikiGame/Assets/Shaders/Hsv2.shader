Shader "Custom/Hsv2"
{
    Properties
    {
        _Speed("Speed", Range(1,2)) = 1
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
            float2 uv_MainTex;
        };

		float _Hue;

		//HSB→RGB
		fixed3 hsv2rgb(fixed3 c)
		{
			fixed4 K = fixed4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
			fixed3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
			return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
		}

		float _Speed;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float _Hue = (_Time*_Speed);
			o.Albedo = hsv2rgb(fixed3(_Hue%1,1,1));
        }

        ENDCG
    }
    FallBack "Diffuse"
}
