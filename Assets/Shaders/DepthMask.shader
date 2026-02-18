Shader "Masked/Mask" {
    SubShader {
        Tags {"RenderType"="Opaque" "Queue"="Geometry+10"}
        Stencil {
            Ref 1
            Comp always
            Pass replace
        }
        ColorMask 0
        ZWrite On
        Pass {}
    }
}