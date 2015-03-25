using UnityEngine;
using System.Collections;

public class Walker : Base
{

	public Transform target;

	public Waypoint wTarget;
	public Waypoint wGoto;

	Transform lastTarget = null;
	Unit tUnit;

	Unit unit;
	Damage damage;

	public Range stopDistance;

	Cell cell;
	float moveBackTime = 0;
	Cell moveBackCell;

	IEnumerator Start () 
	{
		unit = GetComponent<Unit>();

		yield return null;
		yield return null;

		cell = scene.GetCellAtPosition(transform.position);
		transform.position = cell.transform.position;

		while (true)
		{
			Rethink();
			yield return new WaitForSeconds(1);
		}

	}

	void Rethink()
	{
		if (target == null)
		{
			wGoto = null;
			return;
		}

		if (target != lastTarget)
		{
			tUnit = target.GetComponent<Unit>();
			lastTarget = target;
		}


		if (moveBackTime > 0)
		{
			wGoto = pathfinding.AStarWhereToGo(unit.cell.waypoint, moveBackCell.waypoint);
		}
		else
		{
			if (tUnit)
			{
				wGoto = pathfinding.AStarWhereToGo(unit.cell.waypoint, tUnit.cell.waypoint);
			}
		}

	}

	void Update()
	{

		if (moveBackTime > 0)
		{
			moveBackTime -= Time.deltaTime;
		}
		else
		{
			var dir = this.VectorTo(target);
			float dist = dir.magnitude;

			if (stopDistance.IsIn(dist))
			{
				return;
			}

			if (dist < stopDistance.min)
			{
				moveBackTime = 1;
				moveBackCell = unit.cell.GetFarWalkable(-dir, 1);
				Rethink();
				return;
			}
		}

		if (wGoto)
		{
			transform.position += this.VectorTo(wGoto).normalized * Time.deltaTime;
			unit.cell = scene.GetCellAtPosition(transform.position);
		}
	}

	void OnDrawGizmos()
	{
		if (moveBackCell)
			Gizmos.DrawWireCube(moveBackCell.transform.position, Vector3.one);
	}

}
