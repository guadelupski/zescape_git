using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Damage : Base
{
	private static List<Damage> damages = new List<Damage>();

	public float StartHP;
	public bool isPlayer = false;
	public bool displayLedLife = true;
	public float delayAfterDeath = 5;

	LedLife ledLife;

	[System.NonSerialized]
	public float HP;

	[System.NonSerialized]
	public bool isDead = false;

	public System.Action OnDamageCallback;
	public System.Action OnDeathCallback;
	public System.Action OnDestroyCallback;

	void Awake()
	{
		damages.Add(this);
	}

	public static List<Damage> GetAliveList()
	{
		damages.RemoveAll(d => d == null || d.isDead);
		return damages;
	}

	void Start ()
	{
		HP = StartHP;
		if (isPlayer)
			ui.SetPlayerHPValues(HP, HP);
	}

	public void Hit(float v)
	{
		if (isDead)
			return;

		HP -= v;

		if (displayLedLife)
		{
			if (ledLife == null)
			{
				ledLife = LedLife.Create();
				ledLife.Init(this);
				ledLife.SetAnchor(transform);
			}
		}

		if (isPlayer)
			ui.ChangePlayerHP(HP);

		if (HP > 0)
		{
			if (OnDamageCallback != null)
				OnDamageCallback();
		}
		else
		{
			if (OnDeathCallback != null)
				OnDeathCallback();
			
			isDead = true;

			if(!isPlayer)
				StartCoroutine(WaitAndDestroy());
		}
	}

	IEnumerator WaitAndDestroy()
	{
		yield return new WaitForSeconds(delayAfterDeath);

		if (OnDestroyCallback != null)
			OnDestroyCallback();

		Destroy(gameObject);
	}

}
