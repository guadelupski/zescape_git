using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreLabel : Base
{

	public Text label;

	SmoothValue score;

	void Start ()
	{
		score = new SmoothValue(game.score, game.score);
		score.nv = game.score;
		score.speed = 8;
	}
	
	void Update ()
	{
		score.nv = game.score;
		score.Update();
		label.text = score.AsInt().ToString();
	}
}
