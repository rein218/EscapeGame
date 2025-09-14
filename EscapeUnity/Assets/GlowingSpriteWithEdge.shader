Shader "Custom/GlowingSpriteWithAlphaEdge"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _GlowColor ("Glow Color", Color) = (1,1,1,1)
        _GlowIntensity ("Glow Intensity", Range(0, 5)) = 1
        _GlowPower ("Glow Power", Range(0.1, 10)) = 2
        _EdgeColor ("Edge Color", Color) = (1,1,1,1)
        _EdgeIntensity ("Edge Intensity", Range(0, 5)) = 1
        _EdgeWidth ("Edge Width", Range(0, 0.1)) = 0.01
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
        }
        
        LOD 100
        ZWrite Off
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
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed4 _GlowColor;
            float _GlowIntensity;
            float _GlowPower;
            fixed4 _EdgeColor;
            float _EdgeIntensity;
            float _EdgeWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Основной цвет текстуры
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                
                // Эффект свечения (основан на яркости текстуры)
                float brightness = max(col.r, max(col.g, col.b));
                float glow = pow(brightness, _GlowPower) * _GlowIntensity;
                
                // Обнаружение краев по альфа-каналу
                float alpha = col.a;
                float2 uv = i.uv;
                
                // Проверяем соседние пиксели для обнаружения краев
                float alphaRight = tex2D(_MainTex, uv + float2(_EdgeWidth, 0)).a;
                float alphaLeft = tex2D(_MainTex, uv - float2(_EdgeWidth, 0)).a;
                float alphaUp = tex2D(_MainTex, uv + float2(0, _EdgeWidth)).a;
                float alphaDown = tex2D(_MainTex, uv - float2(0, _EdgeWidth)).a;
                
                // Вычисляем градиент альфа-канала
                float edge = abs(alphaRight - alpha) + abs(alphaLeft - alpha) + 
                            abs(alphaUp - alpha) + abs(alphaDown - alpha);
                edge = saturate(edge * 10); // Усиливаем эффект
                
                // Эффект свечения по краям
                float edgeGlow = edge * _EdgeIntensity;
                
                // Комбинируем основной цвет с свечением и краевым свечением
                col.rgb += _GlowColor.rgb * glow + _EdgeColor.rgb * edgeGlow;
                
                return col;
            }
            ENDCG
        }
    }
}
