Shader "Custom/Prop" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" { }
    }
    SubShader
	{
		Tags { "Queue" = "Transparent" }
        Pass
		{
			ZWrite Off
			ZTest Always            
			Material {
                Diffuse [_Color]
				Ambient [_Color]
            }
            Lighting On
			Blend SrcAlpha OneMinusSrcAlpha
            SetTexture [_MainTex]
			{
                constantColor [_Color]
                Combine texture * primary DOUBLE, texture * constant
            }
        }
    }
}
