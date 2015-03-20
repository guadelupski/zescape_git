using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Game : MonoBehaviour
{

	public Unit player;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		float vaxis = Input.GetAxis("Vertical");
		float haxis = Input.GetAxis("Horizontal");

		if (vaxis > 0)
			player.TurnTo(Base.Direction.front);
		if (vaxis < 0)
			player.TurnTo(Base.Direction.back);
		if (haxis > 0)
			player.TurnTo(Base.Direction.right);
		if (haxis < 0)
			player.TurnTo(Base.Direction.left);

		if (Input.GetButtonDown("Jump"))
			player.Move();

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

	}

	bool wasDrag = false;

	public void OnDrag(BaseEventData be)
	{

		var e = be as PointerEventData;

		Vector2 delta = e.delta;

		float hlen = e.delta.x;
		float vlen = e.delta.y;

		float hlena = Mathf.Abs(hlen);
		float vlena = Mathf.Abs(vlen);

		if (hlena < 5 && vlena < 5)
			return;

		wasDrag = true;

		if (hlena > vlena)
		{
			if (hlen < 0)
				player.TurnTo(Base.Direction.left);
			else
				player.TurnTo(Base.Direction.right);
		}
		else if (hlena < vlena)
		{
			if (vlen < 0)
				player.TurnTo(Base.Direction.back);
			else
				player.TurnTo(Base.Direction.front);
		}
	}

	public void OnTap()
	{
		if(!wasDrag)
			player.Move();

		wasDrag = false;
	}

}
