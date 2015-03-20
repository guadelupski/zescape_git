using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour
{

	public static Scene scene;

	public enum Direction
	{
		front,
		back,
		left,
		right
	}

	public float AngleFromDir(Direction dir)
	{
		switch (dir)
		{
			case Direction.front: return 0;
			case Direction.right: return 90;
			case Direction.back: return 180;
			case Direction.left: return 270;
		}
		return 0;
	}

	public Quaternion RotationFromDir(Direction dir)
	{
		return Quaternion.AngleAxis(AngleFromDir(dir), Vector3.up);
	}

	public Vector3 VectorFromDir(Direction dir)
	{
		switch (dir)
		{
			case Direction.front: return Vector3.forward;
			case Direction.right: return Vector3.right;
			case Direction.back: return Vector3.back;
			case Direction.left: return Vector3.left;
		}
		return Vector3.forward;		
	}

	public Direction RandomDir()
	{
		float r = Random.value;
		if (r < .25f)
			return Direction.front;
		else if (r < .5f)
			return Direction.right;
		else if (r < .75f)
			return Direction.back;

		return Direction.left;
	}

}
