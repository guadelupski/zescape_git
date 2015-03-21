using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

public static class ScriptableObjectUtility
{
#if UNITY_EDITOR
	
	/// <summary>
	//	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public static void CreateAsset<T>() where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T>();

		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		if (path == "")
		{
			path = "Assets";
		}
		else if (Path.GetExtension(path) != "")
		{
			path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
		}

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}

	[MenuItem("Assets/Create/DataItem")]
	public static void CreateDataItem()
	{
		ScriptableObjectUtility.CreateAsset<DataItem>();
	}
	[MenuItem("Assets/Create/DataSpawn")]
	public static void CreateDataSpawn()
	{
		ScriptableObjectUtility.CreateAsset<DataSpawn>();
	}
	[MenuItem("Assets/Create/DataLevel")]
	public static void CreateDataLevel()
	{
		ScriptableObjectUtility.CreateAsset<DataLevel>();
	}
	[MenuItem("Assets/Create/DataOptions")]
	public static void CreateDataOptions()
	{
		ScriptableObjectUtility.CreateAsset<DataOptions>();
	}
#endif
}
