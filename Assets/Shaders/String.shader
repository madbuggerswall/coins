// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Unlit shader. Simplest possible colored shader.
// - no lighting
// - no lightmap support
// - no texture

Shader "Unlit/String" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_OutlineColor ("Outline Color", Color) = (1,1,1,1)
		
		_ColorMul ("Color Mul", Range(0,2)) = 0.4
		_Amplitude ("Wave Size", Range(0,2)) = 0.4
		_Frequency ("Wave Freqency", Range(1, 24)) = 2
		_AnimationSpeed ("Animation Speed", Range(0,48)) = 1
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	struct appdata_t {
		float4 vertex : POSITION;

	};

	struct v2f {
		float4 vertex : SV_POSITION;
		float4 color : COLOR;
	};

	fixed4 _Color;
	uniform float4 _OutlineColor;

	float _Amplitude;
	float _Frequency;
	float _AnimationSpeed;
	float _ColorMul;

	v2f vert (appdata_t data)	{
		v2f o;
		
		float4 modifiedPos = data.vertex;
		modifiedPos.x += sin(data.vertex.y * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;
		modifiedPos.x += sin(-data.vertex.y * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;
		
		data.vertex = modifiedPos;
		float3 baseWorldPos = unity_ObjectToWorld._m03_m13_m23;

		o.vertex = UnityObjectToClipPos(data.vertex);

		// Shading
		o.color = 1 - 8 * _ColorMul * abs(data.vertex.y * data.vertex.y) ;
		o.color -= 16 * _ColorMul * data.vertex.z - baseWorldPos.y;
		return o;
	}
	ENDCG

	SubShader {
		Tags { "Queue"="Transparent" }

		Pass {
			Name "OUTLINE"
			Cull Off
			ZWrite Off
			ZTest Always
			ColorMask RGB // alpha not used
			
			// you can choose what kind of blending mode you want for the outline
			Blend SrcAlpha OneMinusSrcAlpha // Normal
			//Blend One One // Additive
			//Blend One OneMinusDstColor // Soft Additive
			//Blend DstColor Zero // Multiplicative
			//Blend DstColor SrcColor // 2x Multiplicative
			
			CGPROGRAM
			#pragma vertex outlineVert
			#pragma fragment frag
			
			v2f outlineVert (appdata_t data)	{
				v2f o;
				
				float4 modifiedPos = data.vertex;
				modifiedPos.x += sin(data.vertex.y * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;
				modifiedPos.x += sin(-data.vertex.y * _Frequency + _Time.y * _AnimationSpeed) * _Amplitude;
				
				data.vertex = modifiedPos;
				float3 baseWorldPos = unity_ObjectToWorld._m03_m13_m23;

				o.vertex = UnityObjectToClipPos(data.vertex);

				// Shading
				o.color  = _OutlineColor;
				return o;
			}
			
			half4 frag(v2f i) :COLOR {
				return i.color;
			}
			ENDCG
		}

		Pass {
			Name "BASE"
			ZWrite On
			ZTest LEqual
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0

			fixed4 frag (v2f i) : COLOR	{
				fixed4 col = _Color * i.color;
				return col;
			}
			ENDCG
		}
	}

}
