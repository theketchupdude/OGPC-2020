// colored vertex lighting
Shader "OGPC/Palette Swap"
{
    // a single color property
    Properties {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Hue ("Hue", Range(0, 360)) = 0
    }
    // define one subshader
    SubShader
    {
        // a single pass in our subshader
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Hue;

            static const float PI = 3.14159265f;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 ret;

                // hue magic

                float U = cos(_Hue*PI/180);
                float W = sin(_Hue*PI/180);

                ret.r = (.299+.701*U+.168*W)*col.r
                    + (.587-.587*U+.330*W)*col.g
                    + (.114-.114*U-.497*W)*col.b;
                ret.g = (.299-.299*U-.328*W)*col.r
                    + (.587+.413*U+.035*W)*col.g
                    + (.114-.114*U+.292*W)*col.b;
                ret.b = (.299-.3*U+1.25*W)*col.r
                    + (.587-.588*U-1.05*W)*col.g
                    + (.114+.886*U-.203*W)*col.b;

                    ret.a = col.a

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return ret;
            }

            ENDCG
        }
    }
}
