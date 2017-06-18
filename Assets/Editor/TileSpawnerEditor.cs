using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileSpawner))]
public class TileSpawnerEditor : Editor
{
	public TileSpawner script;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		script = (TileSpawner)target;
		if(GUILayout.Button("Apply"))
		{
			script.DeleteTiles();
			script.CreateTiles();
		}
		if(GUILayout.Button("Remove All"))
		{
			script.DeleteTiles();
		}
		if(GUILayout.Button("Create (Without Removing)"))
		{
			script.CreateTiles();
		}
	}
}
