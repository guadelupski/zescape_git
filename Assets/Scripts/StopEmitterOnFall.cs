using UnityEngine;
using System.Collections;

public class StopEmitterOnFall : MonoBehaviour
{
	public void OnFall()
	{
		var e = GetComponent<ParticleSystem>();
		e.Stop();
	}
}
