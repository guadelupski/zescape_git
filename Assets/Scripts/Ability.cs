using UnityEngine;
using System.Collections;

public class Ability : Base
{

	public Range attackDistance;

	Walker walker;

	void Start ()
	{
		walker = GetComponent<Walker>();
		walker.stopDistance = attackDistance;
	}
	
	void Update ()
	{
	
	}
}
