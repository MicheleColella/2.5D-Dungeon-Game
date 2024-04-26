#include <Packages/com.blendernodesgraph.core/Editor/Includes/Importers.hlsl>

void brickwall_float(float3 _POS, float3 _PVS, float3 _PWS, float3 _NOS, float3 _NVS, float3 _NWS, float3 _NTS, float3 _TWS, float3 _BTWS, float3 _UV, float3 _SP, float3 _VVS, float3 _VWS, Texture2D image_222370, Texture2D image_222372, Texture2D image_222376, out float4 Color, out float3 Normal, out float Smoothness, out float4 Emission, out float AmbientOcculusion, out float Metallic, out float4 Specular)
{
	
	float4 _ImageTexture_222376 = node_image_texture(image_222376, _UV, 0);
	float4 _ImageTexture_222370 = node_image_texture(image_222370, _UV, 0);
	float4 _NormalMap_222368; node_normal_map(_ImageTexture_222370, 10, _NormalMap_222368);
	float4 _ImageTexture_222372 = node_image_texture(image_222372, _UV, 0);

	Color = _ImageTexture_222376;
	Normal = _NormalMap_222368;
	Smoothness = _ImageTexture_222372;
	Emission = float4(0.0, 0.0, 0.0, 0.0);
	AmbientOcculusion = 0.0;
	Metallic = 0.0;
	Specular = float4(0.0, 0.0, 0.0, 0.0);
}