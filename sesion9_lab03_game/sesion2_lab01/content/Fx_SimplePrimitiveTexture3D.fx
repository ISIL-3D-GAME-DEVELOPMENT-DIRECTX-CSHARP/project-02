cbuffer Fx_MiscInputBuffer : register(b0) {
	uniform float4x4 worldViewProj;
};

struct VS_IN {
	float3 pos : POSITION;
	float4 color : COLOR;
};

struct PS_IN {
	float4 pos : SV_POSITION;
	float4 color : COLOR;
};

PS_IN VS( VS_IN input ) {
	PS_IN output = (PS_IN)0;

	float4 position = float4(input.pos.xyz, 1.0);

	output.pos = mul(position, worldViewProj);
	output.color = input.color;

	return output;
}

float4 PS(PS_IN input) : SV_Target {
	return input.color;
}
