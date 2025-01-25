Shader "Custom/UnlitWaveEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // The RenderTexture
        _Brightness ("Brightness", Range(0, 2)) = 1.0
        _Tint ("Tint Color", Color) = (1, 1, 1, 1)
        _WaveSpeed ("Wave Speed", Float) = 1.0
        _WaveStrength ("Wave Strength", Float) = 0.05
        _WaveFrequency ("Wave Frequency", Float) = 5.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            // Properties
            sampler2D _MainTex;
            float _Brightness;
            float4 _Tint;
            float _WaveSpeed;
            float _WaveStrength;
            float _WaveFrequency;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            // Vertex shader
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Fragment shader
            fixed4 frag (v2f i) : SV_Target
            {
                float time = _Time.y * _WaveSpeed;

                float wave = sin(i.uv.y * _WaveFrequency + time) * _WaveStrength;
                float2 distortedUV = i.uv + float2(wave, 0.0);

                fixed4 texColor = tex2D(_MainTex, distortedUV);

                texColor.rgb *= _Brightness;

                texColor *= _Tint;

                return texColor;
            }
            ENDCG
        }
    }
}
