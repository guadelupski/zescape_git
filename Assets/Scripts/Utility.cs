using UnityEngine;
using System.Collections;

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

}
