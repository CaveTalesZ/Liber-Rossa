Shader "Custom/Square Outline Shader"
{
    Properties
    {
		_MainTexture ("Texture", 2D) = "" {}
		_Color("Color", Color) = (1,0,0,1)
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Thickness("Thickness", Range(0,10)) = 2
	}

	SubShader
	{
		Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{

			CGPROGRAM

			#pragma vertex vertexFunc
			#pragma fragment fragmentFunc

			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 position: SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			fixed4 _Color;
			sampler2D _MainTexture;
			float _Thickness;

			v2f vertexFunc(appdata IN)
			{
				v2f OUT;
				// Slightly enlarge our quad, so we have a margin around it to draw the outline.
				float expand = 1.0f;
				IN.vertex.xyz *= expand;
				OUT.position = UnityObjectToClipPos(IN.vertex);
				// If we want to get fancy, we could compute the expansion 
				// dynamically based on line thickness & view angle, but I'm lazy)

				// Expand the texture coordinate space by the same margin, symmetrically.
				OUT.uv = (IN.uv - 0.5f) * expand + 0.5f;
				return OUT;
			}

			fixed4 fragmentFunc(v2f IN) : SV_Target
			{
				// Texcoord distance from the center of the quad.
				float2 fromCenter = abs(IN.uv - 0.5f);
				// Signed distance from the horizontal & vertical edges.
				float2 fromEdge = fromCenter - 0.5f;

				// Use screenspace derivatives to convert to pixel distances.
				fromEdge.x /= length(float2(ddx(IN.uv.x), ddy(IN.uv.x)));
				fromEdge.y /= length(float2(ddx(IN.uv.y), ddy(IN.uv.y)));

				// Compute a nicely rounded distance from the edge.
				float distance = abs(min(max(fromEdge.x, fromEdge.y), 0.0f) + length(max(fromEdge, 0.0f)));

				// Sample our texture for the interior.
				fixed4 pixelColor = float4(0,0,0,0);
				// Clip out the part of the texture outside our original 0...1 UV space.
				pixelColor.a *= step(max(fromCenter.x, fromCenter.y), 0.5f);


				// Blend in our outline within a controllable thickness of the edge.
				pixelColor = lerp(pixelColor, _Color, saturate(_Thickness - distance));

				return pixelColor;
			}

			ENDCG
		}
	}
}
