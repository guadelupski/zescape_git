using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Range
{
	public float min;
	public float max;

	public float Random()
	{
		return UnityEngine.Random.Range(min, max);
	}

	public bool IsIn(float v)
	{
		return v > min && v < max;
	}

}
