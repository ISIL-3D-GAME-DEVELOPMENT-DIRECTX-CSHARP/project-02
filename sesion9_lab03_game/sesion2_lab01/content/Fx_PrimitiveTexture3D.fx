cbuffer Fx_MiscInputBuffer : register(b0) {
	uniform float ambientIntensity;
	uniform float3 lightDirection;
	uniform float4 ambientColor;
	uniform float4 diffuseLighting;
	uniform float4x4 worldViewProj;
};

struct VS_IN {
	float3 pos : POSITION;
	float4 color : COLOR;
	float3 normal : NORMAL;
	float2 textcoord : TEXTCOORD;
};

struct PS_IN {
	float4 pos : SV_POSITION;
	float4 color : COLOR;
	float3 normal : NORMAL0;
	float2 textcoord : TEXTCOORD;
	float4 ambientColor : POSITION0;
	float4 diffuseLighting : POSITION1;
	float3 lightDirection : NORMAL1;
	float ambientIntensity : PSIZE0;
};

Texture2D picture : register(t0);
SamplerState pictureSampler : register(s0);

PS_IN VS( VS_IN input ) {
	PS_IN output = (PS_IN)0;

	float4 position = float4(input.pos.xyz, 1.0);

	output.pos = mul(position, worldViewProj);
	output.normal = normalize(mul(input.normal, worldViewProj));
	output.color = input.color;
	output.textcoord = input.textcoord;
	output.ambientColor = ambientColor;
	output.diffuseLighting = diffuseLighting;
	output.lightDirection = lightDirection;
	output.ambientIntensity = ambientIntensity;

	return output;
}

float4 PS(PS_IN input) : SV_Target {
	float4 pixelResult = input.ambientColor * input.ambientIntensity;
	pixelResult.w = 1.0;

	float3 invLightDir = -input.lightDirection;
	float lightIntensity = saturate(dot(input.normal, invLightDir));

	if(lightIntensity > 0.0) {
		pixelResult += (input.diffuseLighting * lightIntensity);
	}

	pixelResult = saturate(pixelResult);
	pixelResult = pixelResult * picture.Sample(pictureSampler, input.textcoord);

	return pixelResult;

	/*float4 posEffect = (input.ambientColor * input.ambientIntensity) + 
		input.diffuseLighting * saturate(dot(input.lightDirection, input.normal));
	return picture.Sample(pictureSampler, input.textcoord) * posEffect * input.color;*/

	//return picture.Sample(pictureSampler, input.textcoord) * input.ambientColor;
}
