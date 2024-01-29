Shader "Custom/Collision"
{
    Properties
    {
        [MaterialToggle] _Toggle("CollisionToggle", float) = 0
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

		float _Toggle;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed4 color = (_Toggle<1) ? fixed4(0,1,0,1) : fixed4(1, 0, 0, 1);
			o.Albedo = color;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
