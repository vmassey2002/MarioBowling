Shader "Custom/DoubleSided" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
        };

        void vert(inout appdata_full v) {
            v.normal = -v.normal; // Flip normals
        }

        void surf(Input IN, inout SurfaceOutput o) {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
        }
        ENDCG
    } 
    FallBack "Diffuse"
}
