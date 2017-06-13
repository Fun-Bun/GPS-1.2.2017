using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[ExecuteInEditMode]
public class TileSpawner : MonoBehaviour
{
	public GameObject copy;
	public int halfSizeX;
    public int halfSizeY;
	public float distance;

	// Use this for initialization
	public void CreateTiles ()
	{
        for(int i = -halfSizeY; i <= halfSizeY; i++)
		{
			GameObject currentParent = new GameObject();
			currentParent.transform.SetParent(gameObject.transform);
            currentParent.name = "Y_" + (i + halfSizeY).ToString();

            for(int j = -halfSizeX; j <= halfSizeX; j++)
			{
				GameObject go = Instantiate(copy, ((Vector3.up * i) + (Vector3.right * j)) * distance, Quaternion.identity);
				go.transform.SetParent(currentParent.transform);
                go.name = "X_" + (j + halfSizeX).ToString();
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
