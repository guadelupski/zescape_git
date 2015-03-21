using UnityEngine;
using System.Collections;

public class FreeCellAfterDeath : Base
{

	Damage damage;

	void Start ()
	{
		damage = GetComponent<Damage>();
		if (damage)
			damage.OnDestroyCallback += OnDeath;
	}

	void OnDeath()
	{
		var cell = scene.GetCellAtPosition(transform.position);
		if (cell)
			cell.IsWalkable = true;
	}

}
