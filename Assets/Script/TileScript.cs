using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileScript : MonoBehaviour
{
	public bool isSolid;

	void Update()
	{
		GetComponent<Collider2D>().isTrigger = !isSolid;
	}
}
