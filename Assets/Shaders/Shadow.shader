Shader "Custom/Shadow"
{
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" { }
    }
    SubShader {
		Tags { "Queue" = "Geometry-50" }
        Pass
		{
			ZWrite Off
			ZTest Always
            Material {
                Diffuse [_Color]
				Ambient [_Color]
				Emission [_Color]
            }
            Lighting Off
			Blend SrcAlpha OneMinusSrcAlpha
            SetTexture [_MainTex]
			{
                constantColor [_Color]
                Combine texture * primary DOUBLE, texture * constant
            }
        }
    }
}
