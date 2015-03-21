Shader "Custom/Ground" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" { }
    }
    SubShader
	{
		Tags { "Queue" = "Geometry-100" }
        Pass
		{
            Material {
                Diffuse [_Color]
				Ambient [_Color]
            }
            Lighting On
            SetTexture [_MainTex]
			{
                constantColor [_Color]
                Combine texture * primary DOUBLE, texture * constant
            }
        }
    }
}
