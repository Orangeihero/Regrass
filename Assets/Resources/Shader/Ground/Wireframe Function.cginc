#ifndef Unity_EANGULEE_WIREFRAME
#define Unity_EANGULEE_WIREFRAME
// Wireframe shader based on the the following
// http://developer.download.nvidia.com/SDK/10/direct3d/Source/SolidWireframe/Doc/SolidWireframe.pdf

#include "UnityCG.cginc"

//float _WireThickness;
//half4 _LineColor;

//struct appdata
//{
//	float4 vertex : POSITION;
//	UNITY_VERTEX_INPUT_INSTANCE_ID
//};

//struct v2g
//{
//	float4 projectionSpaceVertex : SV_POSITION;
//	float4 vertexPos : TEXCOORD2;
//	UNITY_VERTEX_OUTPUT_STEREO
//};

//struct g2f
//{
//	float4 projectionSpaceVertex : SV_POSITION;
//	float4 dist : TEXCOORD1;
//	int maxIndex : TEXCOORD2;
//	UNITY_VERTEX_OUTPUT_STEREO
//};

//v2g vert (appdata v)
//{
//	v2g o;
//	UNITY_SETUP_INSTANCE_ID(v);
//	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
//	o.projectionSpaceVertex = UnityObjectToClipPos(v.vertex);
//	o.vertexPos = v.vertex;
//	return o;
//}

//[maxvertexcount(3)]
//void geom(triangle v2g i[3], inout TriangleStream<g2f> triangleStream)
//{
//	float2 _p0 = i[0].projectionSpaceVertex.xy / i[0].projectionSpaceVertex.w;
//	float2 _p1 = i[1].projectionSpaceVertex.xy / i[1].projectionSpaceVertex.w;
//	float2 _p2 = i[2].projectionSpaceVertex.xy / i[2].projectionSpaceVertex.w;

//	float3 p0 = i[0].vertexPos;
//	float3 p1 = i[1].vertexPos;
//	float3 p2 = i[2].vertexPos;

//	// 得到边
//	float2 edge0 = _p2 - _p1;
//	float2 edge1 = _p2 - _p0;
//	float2 edge2 = _p1 - _p0;

//	float s0 = length(p2 - p1);
//	float s1 = length(p2 - p0);
//	float s2 = length(p1 - p0);

//	float area = abs(edge1.x * edge2.y - edge1.y * edge2.x);
//	float wireThickness = 800 - _WireThickness;
//	int maxIndex = 0;

//	#if ENABLE_DRAWQUAD
//	if(s1 > s0)
//	{
//		if(s1 > s2)
//			maxIndex = 1;
//		else
//			maxIndex = 2;
//	}
//	else if(s2 > s0)
//	{
//		maxIndex = 2;
//	}
//	#endif
//	g2f o;

//	o.projectionSpaceVertex = i[0].projectionSpaceVertex;
//	o.dist.xyz = float3( (area / length(edge0)), 0.0, 0.0) * wireThickness * o.projectionSpaceVertex.w;
//	o.dist.w = 1.0 / o.projectionSpaceVertex.w;
//	o.maxIndex = maxIndex;
//	UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(i[0], o);
//	triangleStream.Append(o);

//	o.projectionSpaceVertex = i[1].projectionSpaceVertex;
//	o.dist.xyz = float3(0.0, (area / length(edge1)), 0.0) * wireThickness * o.projectionSpaceVertex.w;
//	o.dist.w = 1.0 / o.projectionSpaceVertex.w;
//	o.maxIndex = maxIndex;
//	UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(i[1], o);
//	triangleStream.Append(o);

//	o.projectionSpaceVertex = i[2].projectionSpaceVertex;
//	o.dist.xyz = float3(0.0, 0.0, (area / length(edge2))) * wireThickness * o.projectionSpaceVertex.w;
//	o.dist.w = 1.0 / o.projectionSpaceVertex.w;
//	o.maxIndex = maxIndex;
//	UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(i[2], o);
//	triangleStream.Append(o);
//}

//fixed4 frag (g2f i) : SV_Target
//{				
//	float minDistanceToEdge;
//	#if ENABLE_DRAWQUAD
//	if(i.maxIndex == 0)
//		minDistanceToEdge = min(i.dist.y, i.dist.z);
//	else if(i.maxIndex == 1)
//		minDistanceToEdge = min(i.dist.x, i.dist.z);
//	else 
//		minDistanceToEdge = min(i.dist.x, i.dist.y);
//	#else
//		minDistanceToEdge = min(i.dist.x, min(i.dist.y, i.dist.z)) * i.dist.w;
//	#endif
//	if(minDistanceToEdge > 0.9)
//	{
//		return fixed4(0,0,0,0);
//	}

//	return _LineColor;
//}



struct a2v {
	half4 uv : TEXCOORD0;
	half4 vertex : POSITION;
};

struct v2f {
	half4 pos : SV_POSITION;
	half4 uv : TEXCOORD0;
};

fixed4 _LineColor;
fixed4 _FillColor;
float _WireWidth;

v2f vert(a2v v)
{
	v2f o;
	o.uv = v.uv;
	o.pos = UnityObjectToClipPos(v.vertex);
	return o;
}
			
fixed4 frag(v2f i) : SV_Target
{
	fixed4 col;
	//只要uv的x和y任意一个值位于线框宽度内就是边缘
	// 用step语句代替if-else可以提高计算效率
	float lx = step(_WireWidth, i.uv.x);//左边
	float ly = step(_WireWidth, i.uv.y);//上边
	float hx = step(i.uv.x, 1.0 - _WireWidth);//右边
	float hy = step(i.uv.y, 1.0 - _WireWidth);//下边
	// 通过插值函数来对应线框颜色和填充颜色
	col = lerp(_LineColor, _FillColor, lx*ly*hx*hy);
	return col;
}

#endif