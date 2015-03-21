using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cell : Base
{

	public bool IsWalkable = true;
	public bool HaveItem = false;
	public int i;
	public int j;

	public Waypoint waypoint;

	[System.NonSerialized]
	public Cell left;
	[System.NonSerialized]
	public Cell right;
	[System.NonSerialized]
	public Cell front;
	[System.NonSerialized]
	public Cell back;

	[System.NonSerialized]
	public List<Cell> near=new List<Cell>();

	public Cell GetNear(Direction dir)
	{
		switch (dir)
		{
			case Direction.front:
				return scene.GetCellAtIndex(i, j + 1);
			case Direction.back:
				return scene.GetCellAtIndex(i, j - 1);
			case Direction.left:
				return scene.GetCellAtIndex(i -1, j);
			case Direction.right:
				return scene.GetCellAtIndex(i + 1, j);
		}
		return null;
	}

	public Cell GetFarWalkable(Vector3 dir, int distance = 3)
	{

		if (distance <= 0)
			return null;

		float maxD = Mathf.Infinity;
		Cell result = null;

		foreach (var c in near)
		{
			if (c.IsWalkable)
			{
				float d = Vector3.Dot(transform.position - c.transform.position, dir);
				if (d < maxD)
				{
					result = c;
					maxD = d;
				}
			}
		}

		if (result)
		{
			var next = result.GetFarWalkable(dir, distance - 1);
			if (next != null)
				result = next;
		}

		return result;

	}

}
