using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour
{

	public Range time;

	IEnumerator Start ()
	{
		yield return null;
		yield return new WaitForSeconds(time.Random());
		Destroy(gameObject);
	}
	
}
