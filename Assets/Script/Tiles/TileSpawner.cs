using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[ExecuteInEditMode]
public class TileSpawner : MonoBehaviour
{
	public GameObject copy;
	public int sizeX;
	public int sizeY;
	public float distance;

	// Use this for initialization
	public void CreateTiles ()
	{
		for(int i = -sizeY; i <= sizeY; i++)
		{
			GameObject currentParent = new GameObject();
			currentParent.transform.SetParent(gameObject.transform);
			currentParent.name = "Y_" + (i + sizeY).ToString();

			for(int j = -sizeX; j <= sizeX; j++)
			{
				GameObject go = Instantiate(copy, ((Vector3.up * i) + (Vector3.right * j)) * distance, Quaternion.identity);
				go.transform.SetParent(currentParent.transform);
				go.name = "X_" + (j + sizeX).ToString();
			}
		}
	}

	public void DeleteTiles()
	{
		List<Transform> children = transform.Cast<Transform>().ToList();
		foreach(Transform child in children)
		{
			DestroyImmediate(child.gameObject);
		}
	}
}
