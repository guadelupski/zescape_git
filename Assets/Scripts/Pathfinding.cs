using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Pathfinding : Base
{
	public List<Waypoint> waypoints = new List<Waypoint>();

	public void Prepare()
	{
		var wps = Object.FindObjectsOfType<Waypoint>();
		waypoints = wps.ToList();

		foreach (Waypoint w in waypoints)
			w.position = w.transform.position;

		foreach (Waypoint w in waypoints)
		{
			foreach (Waypoint ww in waypoints)
			{
				if (w == ww) continue;

				if (w.DistanceTo(ww) < 1.1f)
					w.AddLink(ww);
			}
		}


	}

	public void ResetAStar()
	{
		foreach (Waypoint w in waypoints)
		{

			w.calculated = false;
			w.fullPath = Mathf.Infinity;

			w.prev = null;
			w.opened = false;
			w.closed = false;
			w.g = Mathf.Infinity;
			w.h = Mathf.Infinity;
			w.f = Mathf.Infinity;

		}

	}

	void OnDrawGizmos_()
	{
		foreach (Waypoint w in waypoints)
		{
			foreach (Waypoint.WaypointLink wl in w.near)
			{
				Gizmos.DrawLine(w.position, wl.target.position);
				Gizmos.DrawWireCube(w.position, Vector3.one * .1f);
			}
		}
	}

	public Waypoint GetNearestWaypoint(Vector3 pos)
	{
		float minDist = Mathf.Infinity;
		Waypoint res = null;
		foreach (Waypoint w in waypoints)
		{
			float dist = (pos - w.position).magnitude;
			if (dist < minDist)
			{
				minDist = dist;
				res = w;
			}
		}
		return res;
	}

	void Awake()
	{
		pathfinding = this;
	}

	List<Waypoint> opened = new List<Waypoint>();

	public Waypoint AStarWhereToGo(Waypoint start, Waypoint finish)
	{

		Waypoint res = null;

		ResetAStar();

		opened.Clear();

		start.g = 0;
		start.h = (start.position - finish.position).magnitude;
		start.f = start.h;

		opened.Add(start);

		start.opened = true;

		Waypoint cur = null;

		while (opened.Count > 0)
		{

			float dist = Mathf.Infinity;

			for (int i = 0; i < opened.Count; i++)
			{
				Waypoint w = opened[i];
				if (w.f < dist)
				{
					cur = w;
					dist = w.f;
				}
			}

			if (cur == finish)
			{

				while (cur != null)
				{
					if (cur.prev != null) res = cur;
					cur = cur.prev;

				}

				if (res == null) return finish;

				return res;
			}

			opened.Remove(cur);

			cur.closed = true;
			cur.opened = false;

			foreach (Waypoint.WaypointLink l in cur.near)
			{

				if (!l.target.closed)
				{

					float tentative_g_score = cur.g + l.GetLen();
					bool tentative_is_better = false;

					if (l.target.opened == false)
					{

						opened.Add(l.target);

						l.target.opened = true;
						l.target.closed = false;
						tentative_is_better = true;
					}
					else
					{

						if (tentative_g_score < l.target.g)
						{
							tentative_is_better = true;
						}

					}

					if (tentative_is_better)
					{

						l.target.prev = cur;
						l.target.g = tentative_g_score;
						l.target.h = (l.target.position - finish.position).magnitude;
						l.target.f = l.target.h + l.target.g;

					}

				}

			}

		}

		cur = finish;
		while (cur != null)
		{

			if (cur.prev != null) res = cur;
			cur = cur.prev;

		}

		if (res == null) return finish;

		return res;
	}

}
