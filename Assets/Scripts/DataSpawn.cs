using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataSpawn : ScriptableObject
{
	[System.Serializable]
	public class Item
	{
		public DataItem[] items;
		public float delay;
	}

	public List<Item> items;

}
