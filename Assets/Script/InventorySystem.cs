using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
	public int[] itemCount;

	// Use this for initialization
	void Start ()
	{
		itemCount = new int[(int)ItemType.TotalItems];

		for(int i = 0; i < (int)ItemType.TotalItems; i++)
			itemCount[i] = 0;
	}
}
