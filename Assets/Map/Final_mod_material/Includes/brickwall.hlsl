#include <Packages/com.blendernodesgraph.core/Editor/Includes/Importers.hlsl>

void brickwall_float(float3 _POS, float3 _PVS, float3 _PWS, float3 _NOS, float3 _NVS, float3 _NWS, float3 _NTS, float3 _TWS, float3 _BTWS, float3 _UV, float3 _SP, float3 _VVS, float3 _VWS, Texture2D image_255210, Texture2D image_255212, Texture2D image_255216, out float4 Color, out float3 Normal, out float Smoothness, out float4 Emission, out float AmbientOcculusion, out float Metallic, out float4 Specular)
{
	
	float4 _Mapping_255218 = float4(mapping_point(float4(_UV, 0), float3(0, 0, 0), float3(0, 0, 90), float3(100, 100, 100)), 0);
	float4 _ImageTexture_255216 = node_image_texture(image_255216, _Mapping_255218, 0);
	float4 _ImageTexture_255210 = node_image_texture(image_255210, _Mapping_255218, 0);
	float4 _NormalMap_255208; node_normal_map(_ImageTexture_255210, 10, _NormalMap_255208);
	float4 _ImageTexture_255212 = node_image_texture(image_255212, _Mapping_255218, 0);

	Color = _ImageTexture_255216;
	Normal = _NormalMap_255208;
	Smoothness = _ImageTexture_255212;
	Emission = float4(0.0, 0.0, 0.0, 0.0);
	AmbientOcculusion = 0.0;
	Metallic = 0.0;
	Specular = float4(0.0, 0.0, 0.0, 0.0);
}