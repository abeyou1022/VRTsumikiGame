Shader "Custom/HsvGrid2"
{
	Properties{
		_GridThickness("Grid Thickness", Float) = 0.1
		_GridSpacing("Grid Spacing", Float) = 9.0
		_Freq("frequency", Range(1,2)) = 1

		_Range("Renge", Float) = 0.05
		_Center("Center", Vector) = (0,0,0,0)
		_Emission("Emission", float) = 7
	}
	SubShader{
		Tags{"Render"="Opaque"}
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		struct Input {
			float3 worldPos;
		};

		//HSB→RGB
		fixed3 hsv2rgb(fixed3 c)
		{
			fixed4 K = fixed4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
			fixed3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
			return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
		}

		float _GridThickness;
		float _GridSpacing;
		float _Freq;
		float _Range;
		float4 _Center;
		float _Emission;

		void surf(Input IN, inout SurfaceOutputStandard o) {
			float _Hue = _Time * _Freq; //色相
			float depth = (distance(fixed3(_Center.x, 0, _Center.z), IN.worldPos)*_Range); //はてブではリテラルにしておく

			//グリッド
			o.Albedo = (frac(IN.worldPos.x / _GridSpacing) < _GridThickness+depth
				|| frac(IN.worldPos.z / _GridSpacing) < _GridThickness+depth) ? fixed3(1,1,1) : fixed3(0, 0, 0); //_Camが正しく取得できてないっぽい
			o.Emission = (frac(IN.worldPos.x / _GridSpacing) < _GridThickness + depth
				|| frac(IN.worldPos.z / _GridSpacing) < _GridThickness + depth) ? hsv2rgb(fixed3(_Hue % 1, 1, 1))*_Emission : fixed3(0, 0, 0);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
