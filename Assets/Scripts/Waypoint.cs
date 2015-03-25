using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour
{
	public List<WaypointLink> near = new List<WaypointLink>();

	[System.Serializable]
	public class WaypointLink
	{
		public Waypoint target;
		public float len = 0;

		public float GetLen()
		{
			if(target.cell.IsWalkable)
				return len + target.extraWeight;
			else
				return Mathf.Infinity;
		}

	}

	public Cell cell;

	public Waypoint front;
	public Waypoint back;
	public Waypoint left;
	public Waypoint right;

	public Vector3 position = Vector3.zero;

	public bool calculated = false;
	public float fullPath;

	public Waypoint prev = null;

	public bool opened = false;
	public bool closed = false;
	public float g;
	public float h;
	public float f;
	public float extraWeight = 0;

	public void SetSolid()
	{
		extraWeight = Mathf.Infinity;
	}

	public void SetWalkable()
	{
		extraWeight = 0;
	}

	public void AddLink(Waypoint w)
	{
		if (w == null) return;

		WaypointLink wl = new WaypointLink();
		wl.target = w;
		wl.len = (position - w.position).magnitude;
		near.Add(wl);
	}

}
