using UnityEngine;
using System.Collections;

public class DataItem : ScriptableObject
{

	public enum RotationType
	{
		none,
		RandomY,
		RandomY90,
		RandomXYZ
	}

	public GameObject prefab;
	public RotationType rotationType;

	public int score;

	public Quaternion GetRotation()
	{
		switch (rotationType)
		{
			case RotationType.RandomXYZ:
				return Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
			case RotationType.RandomY:
				return Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
			case RotationType.RandomY90:
				return Quaternion.AngleAxis(Random.Range(0, 4) * 90, Vector3.up);
		}
		return Quaternion.identity;
	}

}
