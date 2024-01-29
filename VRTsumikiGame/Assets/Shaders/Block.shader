Shader "Custom/Block"
{
    Properties
    {
        [HDR] _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Hue("_Hue", float) = 0
		_Bloom("_Bloom", float) = 7
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
		float _Hue;
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
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed3 c = tex2D(_MainTex, IN.uv_MainTex) * hsv2rgb(fixed3(_Hue % 1, 1, 1))*_Bloom;
            o.Albedo = c.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
