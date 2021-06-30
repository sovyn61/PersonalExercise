Shader "MyShader/RotateUV"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed("RotateSpeed",float) =100
        _StencilComp("Stencil Comparison", Float) = 8.000000
        _Stencil("Stencil ID", Float) = 0.000000
        _StencilOp("Stencil Operation", Float) = 0.000000
        _StencilWriteMask("Stencil Write Mask", Float) = 255.000000
        _StencilReadMask("Stencil Read Mask", Float) = 255.000000
        _ColorMask("Color Mask", Float) = 15.000000
    }
    SubShader
    {
          Tags { "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" "PreviewType" = "Plane" "CanUseSpriteAtlas" = "true" }
          ZTest[unity_GUIZTestMode]
          ZWrite Off
          Cull Off
          Stencil {
           Ref[_Stencil]
           ReadMask[_StencilReadMask]
           WriteMask[_StencilWriteMask]
           Comp[_StencilComp]
           Pass[_StencilOp]
          }
          Blend SrcAlpha OneMinusSrcAlpha
          ColorMask[_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _Speed;

            fixed4 frag(v2f i) : SV_Target
            {
                float2 temUV = i.uv;

                //平移到原点
                temUV -= float2(0.5, 0.5);

                if (length(temUV) > 0.5) {
                    return fixed4(0, 0, 0, 0);
                }

                //计算旋转，利用旋转矩阵拆解的分式来计算
                float angle = _Time.x * _Speed;
                float2 finalUV = 0;

                finalUV.x = temUV.x * cos(angle) - temUV.y * sin(angle);
                finalUV.y = temUV.x * sin(angle) + temUV.y * cos(angle);

                //再平移回去
                finalUV += float2(0.5, 0.5);

                fixed4 col = tex2D(_MainTex, finalUV);
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
