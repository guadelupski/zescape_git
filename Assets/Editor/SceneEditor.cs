using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Scene))]
public class SceneEditor : Editor
{
	public override void OnInspectorGUI()
	{

		GUILayout.BeginVertical();

		if (GUILayout.Button("Generate"))
			(target as Scene).Generate();

		GUILayout.EndVertical();

		base.OnInspectorGUI();
	}
}
