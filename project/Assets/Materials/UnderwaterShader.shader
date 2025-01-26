Shader "Custom/UnlitWaveEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        // Caustics
        _CausticsTex ("Caustics Texture", 2D) = "white" {}
        _CausticsStrength ("Caustics Strength", Range(0, 1)) = 0.5

        // Wave effect
        _Brightness ("Brightness", Range(0, 2)) = 1.0
        _Tint ("Tint Color", Color) = (1, 1, 1, 1)
        _WaveSpeed ("Wave Speed", Float) = 1.0
        _CausticSpeed ("Caustics Speed", Float) = 1.0
        _WaveStrength ("Wave Strength", Float) = 0.05
        _WaveFrequency ("Wave Frequency", Float) = 5.0
        _CausticsWaveStrength ("Caustics Wave Strength", Float) = 0.02
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
            sampler2D _CausticsTex;
            float _CausticsStrength;
            float _CausticSpeed;
            float _Brightness;
            float4 _Tint;
            float _WaveSpeed;
            float _WaveStrength;
            float _WaveFrequency;
            float _CausticsWaveStrength;

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
                // Offset UVs for main texture wave effect
                float wave = sin(i.uv.y * _WaveFrequency + _Time.y * _WaveSpeed) * _WaveStrength;
                float2 distortedUV = i.uv + float2(wave, 0.0);

                // Sample main texture with distorted UVs
                fixed4 texColor = tex2D(_MainTex, distortedUV);

                float causticOpacity = sin(_Time.y) * 0.5 + 1.0;

                // Offset UVs for caustics texture (combining scrolling and distortion)
                float causticWave = sin(i.uv.x * _WaveFrequency + _Time.y * _CausticSpeed) * _CausticsWaveStrength;
                float2 scrollUV = i.uv + float2(_Time.y * _CausticSpeed * 0.1, _Time.y * _CausticSpeed * 0.2); // Scrolling effect
                float2 distortedCausticsUV = scrollUV + float2(causticWave, causticWave); // Combine scrolling and distortion

                // Sample caustics texture with combined scrolling and distortion
                fixed4 causticsColor = tex2D(_CausticsTex, distortedCausticsUV) * causticOpacity;

                // Blend caustics with the main texture
                texColor.rgb += causticsColor.rgb * _CausticsStrength;

                // Apply brightness and tint
                texColor.rgb *= _Brightness;
                texColor *= _Tint;

                return texColor;
            }
            ENDCG
        }
    }
}
