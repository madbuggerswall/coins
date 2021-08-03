// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Unlit shader. Simplest possible colored shader.
// - no lighting
// - no lightmap support
// - no texture

Shader "Unlit/String" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		
		_Amplitude ("Wave Size", Range(0,2)) = 0.4
		_Frequency ("Wave Freqency", Range(1, 24)) = 2
		_AnimationSpeed ("Animation Speed", Range(0,48)) = 1
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0

			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
			};

			fixed4 _Color;


			float _Amplitude;
			float _Frequency;
			float _AnimationSpeed;

			v2f vert (appdata_t data)
			{
				v2f o;
				
				float4 modifiedPos = data.vertex;
				modifiedPos.x += sin(data.vertex.y * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;
				modifiedPos.x += sin(-data.vertex.y * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;
				
				data.vertex = modifiedPos;
				
				o.vertex = UnityObjectToClipPos(data.vertex);
				return o;
			}

			fixed4 frag (v2f i) : COLOR
			{
				fixed4 col = _Color;
				return col;
			}
			ENDCG
		}
	}

}
