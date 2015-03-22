using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Game : Base
{

	public DataOptions options;
	public Unit player;

	public int score;

	void Awake()
	{
		game = this;
	}

	void Start ()
	{
		StartCoroutine(Spawn());
	}
	
	IEnumerator Spawn()
	{
		yield return new WaitForSeconds(scene.level.delayBeforeSpawns.Random());

		List<DataItem> spawns = new List<DataItem>();

		foreach (var s in scene.level.spawns)
		{
			for (int i = -1; i < s.possibility; i++)
				spawns.Add(s.item);
		}

		while (true)
		{
			Vector3 pos = player.cell.transform.position;
			pos.x += Random.Range(-2, 2);
			pos.z += Random.Range(-2, 2); 

			var spawnItem = spawns[Random.Range(0, spawns.Count)];

			var go = Instantiate(spawnItem.prefab, pos, spawnItem.GetRotation()) as GameObject;
			go.SetActive(true);

			yield return new WaitForSeconds(scene.level.delayBetwenSpawns.Random());
		}
	}



	void Update ()
	{
		float vaxis = Input.GetAxis("Vertical");
		float haxis = Input.GetAxis("Horizontal");

		if (vaxis > 0)
			{ player.TurnTo(Base.Direction.front); player.Move(); }
		if (vaxis < 0)
			{ player.TurnTo(Base.Direction.back); player.Move(); }
		if (haxis > 0)
			{ player.TurnTo(Base.Direction.right); player.Move(); }
		if (haxis < 0)
			{ player.TurnTo(Base.Direction.left); player.Move(); }

		if (Input.GetButtonDown("Jump"))
			player.Move();

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

	}

	bool wasDrag = false;

	public void OnDrag(BaseEventData be)
	{

		var e = be as PointerEventData;

		if (e.delta.magnitude < 5)
			return;

		float hd = Vector2.Dot(e.delta.normalized, new Vector2(.5f, .5f).normalized);
		float vd = Vector2.Dot(e.delta.normalized, new Vector2(-.5f, .5f).normalized);

		wasDrag = true;

		if (hd < -.5f)
			player.TurnTo(Base.Direction.left);
		else if (hd > .5f)
			player.TurnTo(Base.Direction.right);
		else if (vd > .5f)
			player.TurnTo(Base.Direction.front);
		else if (vd < -.5f)
			player.TurnTo(Base.Direction.back);
	}

	public void OnTap()
	{
		if(!wasDrag)
			player.Move();

		wasDrag = false;
	}

}
