using UnityEngine;
using System.Collections;

public struct SmoothValue
{
	public float v;
	public float max;
	public float nv;
	public float speed;

	public SmoothValue(float V, float MAX)
	{
		v = V;
		max = MAX;
		nv = v;
		speed = 4;
	}
	
	public SmoothValue(float V, float MAX, float NV, float SPEED)
	{
		v = V;
		max = MAX;
		nv = NV;
		speed = SPEED;
	}
	
	public SmoothValue(float V, float MAX, float SPEED)
	{
		v = V;
		max = MAX;
		nv = v;
		speed = SPEED;
	}

	public void Update()
	{
		v = Mathf.Lerp(v, nv, Time.deltaTime * speed);
	}

	public int AsInt()
	{
		return (int)(v + .5f);
	}

	public float AsRange01()
	{
		return Mathf.Clamp01(v / max);
	}

}
