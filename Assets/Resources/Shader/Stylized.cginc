#ifndef Unity_EANGULEE_STYLIZED
#define Unity_EANGULEE_STYLIZED
// Wireframe shader based on the the following
// http://developer.download.nvidia.com/SDK/10/direct3d/Source/SolidWireframe/Doc/SolidWireframe.pdf

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"

fixed4 _Diffuse;
fixed4 _Specular;
float _Gloss;
sampler2D _RampTex;
float4 _RampTex_ST;
sampler2D _GridTex;
float4 _GridTex_ST;

struct v2f{
	float4 pos : SV_POSITION;
	float3 worldNormal : TEXCOORD0;
	float4 worldPos : TEXCOORD1;
	float2 uv : TEXCOORD2;
	SHADOW_COORDS(3)
};
			
v2f vert(appdata_base v){
	v2f o;
	o.pos = UnityObjectToClipPos(v.vertex);
	o.worldNormal = UnityObjectToWorldNormal(v.normal);
	o.worldPos = mul(unity_ObjectToWorld,v.vertex);
	o.uv = TRANSFORM_TEX(v.texcoord,_RampTex);
	TRANSFER_SHADOW(o);
	return o;
}

float4 frag(v2f i):SV_TARGET0{
	fixed3 worldNormal = normalize(i.worldNormal);
	fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
	fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
	fixed3 halfDir = normalize(worldLightDir + worldViewDir);
	fixed shadow = SHADOW_ATTENUATION(i);

	fixed halfLambert = (0.5 * dot(worldNormal,worldLightDir)) + 0.5;
	//if(shadow < 0.5){
	//	halfLambert = 0.01;
	//}else{
	//	halfLambert *= shadow;
	//}
	
	fixed3 diffuseColor = tex2D(_RampTex,fixed2(halfLambert,halfLambert)).rgb * _Diffuse.rgb * tex2D(_GridTex,i.uv).rgb;
	if(shadow < 0.3){
		diffuseColor *= 0.2;}
	//}else if(shadow < 0.4){
	//	diffuseColor *= 0.2 + (shadow - 0.3) * 0.8;
	//}


	fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
	fixed3 diffuse = _LightColor0.rgb * diffuseColor;
	fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(max(0,dot(worldNormal,halfDir)),_Gloss);
	fixed atten = 1.0;
	return fixed4(ambient + diffuse * atten , 1.0);
			
}
		

float4 fragg(v2f i):SV_TARGET0{
	fixed3 worldNormal = normalize(i.worldNormal);

	#ifdef USING_DIRECTIONAL_LIGHT
		fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
	#else
		fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz - i.worldPos.xyz);
	#endif

	fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
	fixed3 halfDir = normalize(worldLightDir + worldViewDir);
	fixed halfLambert = 0.5 * dot(worldNormal,worldLightDir) + 0.5;
	fixed3 diffuseColor = tex2D(_RampTex,fixed2(halfLambert,halfLambert)).rgb * _Diffuse.rgb;

	fixed3 diffuse = _LightColor0.rgb * diffuseColor;
	fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(worldNormal,halfDir)),_Gloss);

	#ifdef USING_DIRECTIONAL_LIGHT
		float atten = 1.0;
	#else
		float3 lightCoord = mul(unity_WorldToLight,float4(i.worldPos.xyz,1)).xyz;
		fixed atten = tex2D(_LightTexture0,dot(lightCoord,lightCoord).rr).UNITY_ATTEN_CHANNEL;
	#endif

	return fixed4((diffuse + specular) * atten, 1.0);
			
}

#endif