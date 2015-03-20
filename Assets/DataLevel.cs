using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataLevel : ScriptableObject
{
	[System.Serializable]
	public class SceneItem
	{
		public DataItem item;
		public float possibility;
	}

	public List<SceneItem> items;

}
