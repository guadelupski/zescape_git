using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Collectable : Base
{
	public static List<Collectable> collectables = new List<Collectable>();

	public int score;
	public int life;

	void Start()
	{
		collectables.Add(this);
	}

	public static Collectable GetNear(MonoBehaviour to, float inRange = 10f)
	{
		collectables.RemoveAll(c => c == null);

		Collectable result = null;

		float min = Mathf.Infinity;
		foreach (var c in collectables)
		{
			float d = c.DistanceTo(to);
			if (d < min && d < inRange)
			{
				min = d;
				result = c;
			}
		}

		return result;
	}

	public void Collect()
	{
		game.score += score;
		game.player.damage.ChangeHP(life);

		Destroy(gameObject);
	}

}
