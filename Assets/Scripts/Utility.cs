using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Utility 
{
	public static GameObject GetRandom(this GameObject[] array)
	{
		return array[Random.Range(0, array.Length)];
	}

	public static Vector3 ToVector3(this Color clr)
	{
		return new Vector3(clr.r, clr.g, clr.b);
	}
	
	public static Color ToColor(this Vector3 v)
	{
		return new Color(v.x, v.y, v.z, 1);
	}

	public static float DistanceTo(this MonoBehaviour src, MonoBehaviour target)
	{
		return (src.transform.position - target.transform.position).magnitude;
	}

	public static float DistanceTo(this MonoBehaviour src, Transform target)
	{
		return (src.transform.position - target.position).magnitude;
	}

	public static float DistanceTo(this Transform src, MonoBehaviour target)
	{
		return (src.position - target.transform.position).magnitude;
	}
	
	public static Vector3 VectorTo(this MonoBehaviour src, MonoBehaviour target)
	{
		return target.transform.position - src.transform.position;
	}

	public static Vector3 VectorTo(this MonoBehaviour src, Transform target)
	{
		return target.position - src.transform.position;
	}
	
	public static Vector3 VectorTo(this Transform src, MonoBehaviour target)
	{
		return target.transform.position - src.position;
	}

	public static bool LinearLerp(this Vector3 from, Vector3 to, float speed = 1)
	{
		var dir = to - from;
		var len = dir.magnitude;

		if (len < .001f)
		{
			from = to;
			return true;
		}
		var result = from + dir.normalized * speed;

		if ((from - result).magnitude >= dir.magnitude)
		{
			from = to;
			return true;
		}
		
		from = result;
		return false;
	}

}
