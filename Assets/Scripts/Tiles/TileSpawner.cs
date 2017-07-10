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
		this.transform.position = Vector3.zero;

		for(int i = 0; i < sizeY; i++)
		{
			GameObject currentParent = new GameObject();
			currentParent.transform.SetParent(gameObject.transform);
			currentParent.name = "Y_" + i.ToString();

			for(int j = 0; j < sizeX; j++)
			{
				GameObject go = Instantiate(copy, ((Vector3.up * i) + (Vector3.right * j)) * distance, Quaternion.identity);
				go.transform.SetParent(currentParent.transform);
				go.name = "X_" + j.ToString();
			}
		}

		this.transform.position = new Vector3(sizeX - 1, sizeY - 1, 0.0f) * distance / 2 * -1;
	}

	public void DeleteTiles()
	{
		List<Transform> children = transform.Cast<Transform>().ToList();
		foreach(Transform child in children)
		{
			DestroyImmediate(child.gameObject);
		}

		this.transform.position = Vector3.zero;
	}
}
