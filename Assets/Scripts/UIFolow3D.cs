using UnityEngine;
using System.Collections;

public class UIFolow3D : Base
{

	public Transform target;
	public Vector3 pos;

	RectTransform rect;

	void Start ()
	{
		rect = GetComponent<RectTransform>();
	}

	public void BeginFolow(Transform target_)
	{
		target = target_;
		if (target)
			pos = target.position;
	}

	void Update ()
	{
		if (target)
			pos = target.position;

		var pos2d = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);

		rect.anchoredPosition = pos2d-ui.rect.sizeDelta / 2f;
	}
}
