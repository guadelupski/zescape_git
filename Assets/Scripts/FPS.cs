using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPS : MonoBehaviour {

	public Text text;

	int frames = 0;

	IEnumerator Start ()
	{
		while (true)
		{
			yield return new WaitForSeconds(.5f);
			text.text = "FPS: " + (frames * 2).ToString();
			frames = 0;
		}
	}
	
	void Update ()
	{
		frames++;
	}
}
