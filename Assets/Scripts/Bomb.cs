using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour 
{

	public GameObject alert;
	public GameObject rocket;
	public GameObject explosion;

	public float alertTime;
	public float radius = 1;
	public float damage = 1;

	public System.Action onRocketFall;

	bool waitingFall = true;

	void Awake()
	{
		alert.SetActive(false);
		rocket.SetActive(false);
		explosion.SetActive(false);

		var fall = rocket.GetComponent<Fall>();
		if (fall)
			fall.OnFallCallback = () => waitingFall = false;
	}

	IEnumerator Start ()
	{
		alert.SetActive(true);
		rocket.SetActive(false);
		explosion.SetActive(false);

		yield return new WaitForSeconds(alertTime);

		rocket.SetActive(true);

		while (waitingFall)
			yield return null;

		alert.SetActive(false);
		explosion.SetActive(true);

		foreach (var d in Damage.GetAliveList())
			if (this.DistanceTo(d) < radius)
				d.Hit(damage);

		yield return new WaitForSeconds(1);

		Destroy(gameObject);

	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, radius);
	}

}
