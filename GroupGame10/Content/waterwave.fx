
cbuffer ConstantBuffer : register(b0)
{
    matrix World;
    matrix View;
    matrix Projection;
    float4 vMeshColor;
    float2 param;
}

Texture2D txDiffuse    : register(t0);
Texture2D txAnother    : register(t1);
Texture2D txMask       : register(t2);
SamplerState samLinear : register(s0);


struct VS_INPUT
{
    float4 Pos : POSITION;
    float2 Tex : TEXCOORD0;
};

struct PS_INPUT
{
    float4 Pos : SV_POSITION;
    float2 Tex : TEXCOORD0;
};

PS_INPUT VS(VS_INPUT input)
{
    PS_INPUT output = (PS_INPUT)0;
    output.Pos = mul(input.Pos, World);
    output.Pos = mul(output.Pos, View);
    output.Pos = mul(output.Pos, Projection);    
    output.Tex = input.Tex;
    return output;
}

float4 PS(PS_INPUT input) : SV_Target
{
    float4 another = txAnother.Sample(samLinear, input.Tex);
    float4 alpha = txMask.Sample(samLinear, input.Tex);
    
    // 更新像素点的 uv 偏移，相当于点在做?周??
    // param.x 是以秒??位的??
    float2 bg = input.Tex;
    bg.x += sin(param.x + bg.x * 15) * 0.01;
    bg.y += cos(param.x + 0.003 + bg.y * 15) * 0.01;

    float4 color = txDiffuse.Sample(samLinear, bg) * vMeshColor;
    float4 finanl = color * (1.0f - alpha) + alpha * another;
    float4 blend = saturate(finanl);
    return color;
}

technique Textured
{
	pass Pass0
	{
		VertexShader = compile vs_4_0 VS();
		PixelShader = compile ps_4_0 PS();
	}
}