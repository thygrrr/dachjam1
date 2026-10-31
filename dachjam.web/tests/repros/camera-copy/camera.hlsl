struct Result {
    float4 position;
};

struct Camera_2_ {
    row_major float4x4 projection;
    row_major float3x4 view;
    float3 light;
    float fog;
};

struct Scene_1_ {
    Camera_2_ camera;
};

struct Camera_1_ {
    row_major float4x4 projection;
    row_major float3x4 view;
    float3 light;
    float fog;
};

RWByteAddressBuffer result : register(u1);
cbuffer scene : register(b0) { Scene_1_ scene; }

float4 v_1_(inout Camera_1_ camera)
{
    float4x4 view = (float4x4)0;

    float4 v_2_ = camera.view[0];
    float4 v_3_ = camera.view[1];
    float4 v_4_ = camera.view[2];
    view = transpose(float4x4(float4(v_2_.x, v_2_.y, v_2_.z, v_2_.w), float4(v_3_.x, v_3_.y, v_3_.z, v_3_.w), float4(v_4_.x, v_4_.y, v_4_.z, v_4_.w), float4(0.0, 0.0, 0.0, 1.0)));
    float4x4 _e34 = camera.projection;
    float4x4 _e35 = view;
    return mul(float4(3.0, 2.0, 1.0, 1.0), mul(_e35, _e34));
}

[numthreads(1, 1, 1)]
void main()
{
    Camera_1_ param = (Camera_1_)0;

    Camera_2_ v = scene.camera;
    param.projection = v.projection;
    param.view = v.view;
    param.light = v.light;
    param.fog = v.fog;
    const float4 _e14 = v_1_(param);
    result.Store4(0, asuint(_e14));
    return;
}
