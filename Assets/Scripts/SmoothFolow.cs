using UnityEngine;
using System.Collections;

public class SmoothFolow : MonoBehaviour
{

	public Transform target;
	public bool pointOnStart = true;

	void Start()
	{
		if (pointOnStart)
			transform.position = target.position;
	}

	void Update ()
	{
		transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 8);
	}
}
