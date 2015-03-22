using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Unit : Base
{

	public bool isPlayer = false;

	public Animator animator;

	Direction dir = Direction.front;

	Quaternion destRotation = Quaternion.identity;

	[System.NonSerialized]
	public Cell cell;
	
	[System.NonSerialized]
	public Cell prevCell;

	[System.NonSerialized]
	public Damage damage;

	void Start ()
	{
		damage = GetComponent<Damage>();
		transform.rotation = RotationFromDir(dir);
		destRotation = transform.rotation;
		cell = scene.GetCellAtPosition(transform.position);
		prevCell = cell;
		cell.unit = this;

		transform.position = cell.transform.position;

		if (isPlayer)
			StartCoroutine(Collect());

	}

	IEnumerator Collect()
	{
		while (true)
		{
			yield return new WaitForSeconds(.2f);

			var nearest = Collectable.GetNear(this, .5f);
			if (nearest)
			{
				nearest.Collect();
			}
		}
	}

	public void Move()
	{
		if (!moving)
			StartCoroutine(MoveRoutine());
		else
			movingQueued = true;
	}

	bool moving = false;
	bool movingQueued = false;

	IEnumerator MoveRoutine()
	{

		Cell nc = null;
		
		if(!BeginMove(out nc))
			yield break;

		
		moving = true;
		animator.SetTrigger("Run");

		while (true)
		{
			var vFullDir = nc.transform.position - transform.position;
			var vdir = vFullDir.normalized * Time.deltaTime * 4;
			var nextPos = transform.position + vdir;

			if (vFullDir.magnitude < .01f || Vector3.Dot(nc.transform.position - transform.position, nc.transform.position - nextPos) <= 0)
			{
				cell = nc;

				if (isPlayer)
					OnPlayerComeToCell();

				if (movingQueued)
				{
					movingQueued = false;
					if(BeginMove(out nc))
						continue;
					else
						transform.position = cell.transform.position;
				}
				else
				{
					transform.position = cell.transform.position;
				}
				break;
			}
			else
				transform.position = nextPos;

			yield return null;
		}
		
		animator.SetTrigger("Idle");
		moving = false;
	}

	void OnPlayerComeToCell()
	{
		const float minDist = 2;
		const float maxDist = 3;

		scene.optimalForAttackCells.Clear();
		List<Cell> near = new List<Cell>();

		foreach (var c in scene.cells)
		{
			if (c == cell)
				continue;

			float dist = c.DistanceTo(cell);

			if (dist < maxDist)
			{
				near.Add(c);
				if (dist < minDist)
					c.optimalForAttack = false;
				else
					c.optimalForAttack = true;
			}
			else
			{
				c.optimalForAttack = false;
			}
		}

		foreach (var c in near)
		{
			if (c.optimalForAttack && scene.Linecast(near, cell, c))
				c.optimalForAttack = false;
			else
				if(c.optimalForAttack)
					scene.optimalForAttackCells.Add(c);
		}

	}

	bool BeginMove(out Cell nc)
	{
		nc = cell.GetNear(dir);
		return nc != null && nc.IsWalkable;
	}

	public void TurnTo(Direction toDir)
	{
		if (dir == toDir)
			return;

		dir = toDir;
		destRotation = RotationFromDir(dir);
	}

	
	void Update ()
	{
		transform.rotation = Quaternion.Lerp(transform.rotation, destRotation, Time.deltaTime * 16);
		if (prevCell != cell)
		{
			prevCell.unit = null;
			cell.unit = this;
			prevCell = cell;
		}
	}
}
