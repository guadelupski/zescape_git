using UnityEngine;
using System.Collections;

public class Cell : Base
{

	public bool IsWalkable = true;
	public bool HaveItem = false;
	public int i;
	public int j;

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

	

}
