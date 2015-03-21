using UnityEngine;
using System.Collections;

public class Fall : MonoBehaviour {

	public float speed = 1;
	public float destroyAfter = 0;

	public System.Action OnFallCallback;

	IEnumerator Start ()
	{
		var pos = transform.position;

		while (pos.y > 0)
		{
			pos.y -= speed * Time.deltaTime;
			transform.position = pos;
			yield return null;
		}

		pos.y = 0;
		transform.position = pos;

		yield return null;

		SendMessage("OnFall");
		if (OnFallCallback != null)
			OnFallCallback();

		yield return new WaitForSeconds(destroyAfter);

		Destroy(gameObject);

	}
	
}
