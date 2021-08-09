Shader "Unlit/Tree"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Amplitude ("Wave Size", Range(0,2)) = 0.4
		_AnimationSpeed ("Animation Speed", Range(0,4)) = 0.8
		_Mul ("Pivot Mul", Range(-4,4)) = .42
		_ZMul ("Z Mul", Range(0,2)) = 1

	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			float _Amplitude;
			float _AnimationSpeed;
			float _Mul;
			float _ZMul;

			v2f vert (appdata data)
			{
				v2f o;
				
				float4 modifiedPos = data.vertex;
				float3 baseWorldPos = unity_ObjectToWorld._m03_m13_m23;

				modifiedPos.x += (data.vertex.y+_Mul*baseWorldPos.y) * sin(_ZMul* data.vertex.z + _Time.y * _AnimationSpeed) * _Amplitude;
				
				data.vertex = modifiedPos;
				o.vertex = UnityObjectToClipPos(data.vertex);
				o.uv = TRANSFORM_TEX(data.uv, _MainTex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}
