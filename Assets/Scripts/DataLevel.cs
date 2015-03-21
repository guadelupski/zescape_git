using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataLevel : ScriptableObject
{
	[System.Serializable]
	public class SceneItem
	{
		public DataItem item;
		public int possibility;
	}

	public Range delayBeforeSpawns;
	public Range delayBetwenSpawns;

	public float solidComplexity = .1f;

	public List<SceneItem> items;
	public List<SceneItem> spawns;

}
