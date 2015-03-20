using UnityEngine;
using System.Collections;

public class SmoothFolow : MonoBehaviour
{

	public Transform target;

	void Update ()
	{
		transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 8);
	}
}
