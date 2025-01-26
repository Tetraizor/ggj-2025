Shader "Sprites/WobbleShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _WobbleSpeed ("Wobble Speed", Float) = 1.0
        _WobbleStrength ("Wobble Strength", Float) = 0.1
        _Speed ("Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            Lighting Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _WobbleSpeed;
            float _WobbleStrength;
            float _Speed;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID // For batching
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID // For batching
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v); // For batching

                float time = _Time.y * _WobbleSpeed * _Speed;

                // Add wobble effect to vertices
                float wobble = sin(v.vertex.y * 10.0 + time) * _WobbleStrength * _Speed;
                v.vertex.x += wobble;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture
                fixed4 color = tex2D(_MainTex, i.uv);

                // Return the color
                return color;
            }
            ENDCG
        }
    }
}
