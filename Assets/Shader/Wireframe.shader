Shader "Custom/Wireframe"
{
	Properties
	{
		_LineColor("Line Color", Color) = (1,1,1,1)
		_Diffuse("DIFFUSE", Color) = (1,1,1,1)
		_Specular("Specular", Color) = (1,1,1,1)
		_Gloss("Gloss",Range(8.0,256)) = 20
		_RampTex("Ramp Tex",2D) = "white"{}
		_WireThickness ("Wire Thickness", RANGE(0, 800)) = 100
		[Toggle(ENABLE_DRAWQUAD)]_DrawQuad("Draw Quad", Float) = 0
	}

	SubShader
	{

		Pass {
			Tags{"LightMode" = "ForwardBase"}

			CGPROGRAM

			#pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
			#include "Stylized.cginc"
			#pragma vertex vert
			#pragma fragment frag

            ENDCG
        }

		//Pass
		//{
		//	Blend SrcAlpha OneMinusSrcAlpha 
		//	ZWrite Off
		//	Cull Front
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

		Tags { "RenderType"="Transparent" "Queue"="Transparent" }


		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha 
			ZWrite Off
			Cull Back
			LOD 200
			
			CGPROGRAM
			#pragma target 4.0
			#pragma multi_compile __ ENABLE_DRAWQUAD
			#include "UnityCG.cginc"
			#include "Wireframe Function.cginc"
			#pragma vertex vert
			#pragma geometry geom
			#pragma fragment frag
			
			ENDCG
		}

	}
}