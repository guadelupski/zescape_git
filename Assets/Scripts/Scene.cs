using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Scene : Base
{

	public int size;

	public DataLevel level;

	public Transform cellsRoot;
	public Transform itemsRoot;

	public List<Cell> cells;
	public Cell[,] cellsArray;

	public bool drawDebug = false;

	public void Generate()
	{
		Init();

		List<DataItem> items = new List<DataItem>();
		foreach (var si in level.items)
		{
			for (int i = -1; i < si.possibility; i++)
				items.Add(si.item);
		}

		foreach (var si in items)
		{
			ForEachCell(c =>
			{
				if(!c.HaveItem)
				if (Random.value < level.solidComplexity)
				{
					c.IsWalkable = false;
					c.HaveItem = true;
					var go = Instantiate(si.prefab, c.transform.position, si.GetRotation()) as GameObject;
					go.transform.parent = itemsRoot;
				}
			});
		}

		ForEachCell(c =>
			{
				c.waypoint = c.gameObject.AddComponent<Waypoint>();
				c.waypoint.cell = c;
			});

	}

	void Init()
	{
		if (cellsRoot)
			DestroyImmediate(cellsRoot.gameObject);
		if (itemsRoot)
			DestroyImmediate(itemsRoot.gameObject);

		cellsRoot = new GameObject("Cells").transform;
		cellsRoot.parent = transform;
		itemsRoot = new GameObject("Items").transform;
		itemsRoot.parent = transform;

		cells = new List<Cell>();

		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				var co = new GameObject("" + i + "_" + j);
				co.transform.position = new Vector3(i, 0, j);
				co.transform.parent = cellsRoot;

				var cell = co.AddComponent<Cell>();
				cells.Add(cell);

				cell.i = i;
				cell.j = j;

			}
		}

	}

	void ForEachCell(System.Action<Cell> func)
	{
		foreach (var c in cells)
			func(c);
	}

	void Awake()
	{
		scene = this;
	}

	void Start()
	{
		cellsArray = new Cell[size, size];
		ForEachCell(c =>
			{
				cellsArray[c.i, c.j] = c;
			});
		ForEachCell(c =>
			{
				c.left = GetCellAtIndex(c.i - 1, c.j);
				c.right = GetCellAtIndex(c.i + 1, c.j);
				c.front = GetCellAtIndex(c.i, c.j + 1);
				c.front = GetCellAtIndex(c.i, c.j - 1);

				if (c.left) c.near.Add(c.left);
				if (c.right) c.near.Add(c.right);
				if (c.front) c.near.Add(c.front);
				if (c.back) c.near.Add(c.back);

			});
	}

	public Cell GetCellAtPosition(Vector3 pos)
	{
		int ix = (int)(pos.x + .5f);
		int iz = (int)(pos.z + .5f);

		return GetCellAtIndex(ix, iz);
	}
	
	public Cell GetCellAtIndex(int ix, int iz)
	{
		if (ix < 0 || iz < 0) return null;
		if (ix > size - 1 || iz > size - 1) return null;

		return cellsArray[ix, iz];
	}

	public Cell GetNearestCell(Vector3 pos)
	{
		float minDist = Mathf.Infinity;
		Cell result=null;
		foreach (var c in cells)
		{
			float dist = (c.transform.position - pos).magnitude;
			if (dist < minDist)
			{
				minDist = dist;
				result = c;
			}
		}
		return result;
	}

	void Update()
	{

	}

	void OnDrawGizmos()
	{

		if (!drawDebug)
			return;

		foreach (var c in cells)
		{
			
			if(c.IsWalkable)
				Gizmos.DrawWireCube(c.transform.position, Vector3.one);
			else
				Gizmos.DrawCube(c.transform.position, Vector3.one);

		}
	}

}
