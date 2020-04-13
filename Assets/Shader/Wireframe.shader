Shader "Custom/Wireframe"
{
	Properties
	{
		_LineColor("Line Color",Color) = (1.0,1.0,1.0,1.0)
		_WireWidth("Wire Width",Range(0,1)) = 0.1
		_Diffuse("DIFFUSE", Color) = (1,1,1,1)
		_Specular("Specular", Color) = (1,1,1,1)
		_Gloss("Gloss",Range(8.0,256)) = 20
		_RampTex("Ramp Tex",2D) = "white"{}
	}

	SubShader
	{

		Pass{
			Tags{"LightMode" = "ShadowCaster"}
			Cull Back

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_shadowcaster
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 pos:SV_POSITION;
			};

			v2f vert(appdata_full v) 
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
				return o;
			}

			float4 frag(v2f o) :SV_Target
			{
				SHADOW_CASTER_FRAGMENT(o)
			}

			ENDCG
		
		}

		Pass {
			Tags{"LightMode" = "ForwardBase"}
			Cull off
			CGPROGRAM

			#pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"
			#include "Stylized.cginc"
			#pragma vertex vert
			#pragma fragment frag


            ENDCG
        }

		//Tags { "RenderType"="Transparent" "Queue"="Transparent" }

		Pass 
		{
			Blend SrcAlpha OneMinusSrcAlpha
			LOD 200
			Cull Front
			zWrite off

			CGPROGRAM
			#pragma target 4.0
			#include "UnityCG.cginc"
			#include "Wireframe Function.cginc"
			#pragma vertex vert
			#pragma fragment frag
			
			ENDCG
		}

		Pass 
		{
			Blend SrcAlpha OneMinusSrcAlpha
			LOD 200
			Cull Back
			zWrite off

			CGPROGRAM

			#pragma target 4.0
			#include "UnityCG.cginc"
			#include "Wireframe Function.cginc"
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
			}

		//Pass
		//{
		//	Blend SrcAlpha OneMinusSrcAlpha 
		//	ZWrite Off
		//	Cull Back
		//	LOD 200
			
		//	CGPROGRAM
		//	#pragma target 4.0
		//	#pragma multi_compile __ ENABLE_DRAWQUAD
		//	#include "UnityCG.cginc"
		//	#include "Wireframe Function.cginc"
		//	#pragma vertex vert
		//	#pragma geometry geom
		//	#pragma fragment frag
			
		//	ENDCG
		//}


	}

	FallBack "VertexLit"
}