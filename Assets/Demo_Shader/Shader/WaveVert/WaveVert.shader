Shader "MyShader/WaveVert"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed("Wave Speed",float) = 10
        _Arange("Wave arange",float) = 10
        _Frequency("Wave Frequency",float) = 10
    }
    SubShader
    {
        // No culling or depth
        // Cull Off ZWrite Off ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

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

            float _Speed;
            float _Arange;
            float _Frequency;

            v2f vert (appdata v)
            {
                float angle = _Time.x * _Speed;
                float wave = _Arange * sin(angle + v.vertex.x * _Frequency);
                v.vertex.y = v.vertex.y + wave;

                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
