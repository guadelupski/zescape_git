using UnityEngine;
using System.Collections;

public class AlertIcon : MonoBehaviour
{

	Vector3 baseScale;
	float scale = .01f;

	void Awake()
	{
		baseScale = transform.localScale;
		transform.localScale = baseScale * scale;
	}

	IEnumerator Start ()
	{
		
		yield return new WaitForSeconds(Random.value * .5f);

		while (true)
		{
			scale += Time.deltaTime * 8;
			if (scale >= 1)
			{
				scale = 1;
				transform.localScale = baseScale * scale;
				break;
			}
			transform.localScale = baseScale * scale;
			yield return null;
		}

		Destroy(this);
	}
	
}
