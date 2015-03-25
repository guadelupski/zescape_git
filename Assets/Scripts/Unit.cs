using UnityEngine;
using System.Collections;

public class Unit : Base
{

	public bool isPlayer = false;

	public Animator animator;

	Direction dir = Direction.front;

	Quaternion destRotation = Quaternion.identity;

	[System.NonSerialized]
	public Cell cell;

	void Start ()
	{
		transform.rotation = RotationFromDir(dir);
		destRotation = transform.rotation;
		cell = scene.GetCellAtPosition(transform.position);
		transform.position = cell.transform.position;
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

	// Update is called once per frame
	void Update ()
	{
		transform.rotation = Quaternion.Lerp(transform.rotation, destRotation, Time.deltaTime * 16);
	}
}
