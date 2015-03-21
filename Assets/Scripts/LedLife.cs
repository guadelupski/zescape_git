using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LedLife : Base
{
	
	public Image bar;

	SmoothValue v;
	Damage _damage;

	public void Init(Damage damage)
	{
		v = new SmoothValue(damage.StartHP, damage.StartHP);
		v.nv = damage.HP;
		bar.fillAmount = v.AsRange01();
		_damage = damage;
	}

	public void SetAnchor(Transform target)
	{
		var folow = GetComponent<UIFolow3D>();
		folow.BeginFolow(target);
	}

	void Update ()
	{
		if (!_damage)
		{
			Destroy(gameObject);
			return;
		}

		v.nv = _damage.HP;
		v.Update();
		bar.fillAmount = v.AsRange01();
	}

	public static LedLife Create()
	{
		var go = Instantiate(game.options.ledPrefab) as GameObject;
		go.transform.parent = ui.transform;
		return go.GetComponent<LedLife>();
	}

}
