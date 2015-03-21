using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI : Base
{

	public Image playerHP;
	
	SmoothValue playerHPValue;

	public RectTransform rect;

	void Awake()
	{
		ui = this;
		rect = GetComponent<RectTransform>();
	}

	void Start ()
	{
	
	}
	
	void Update ()
	{
		playerHPValue.Update();
		playerHP.fillAmount = playerHPValue.AsRange01();
	}

	public void SetPlayerHPValues(float v, float max)
	{
		playerHPValue = new SmoothValue(v, max);
	}

	public void ChangePlayerHP(float newValue)
	{
		playerHPValue.nv = newValue;
	}

}
