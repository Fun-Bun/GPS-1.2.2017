using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Item")
		{
			ItemScript itemScript = other.gameObject.GetComponent<ItemScript>();
			InventorySystem invSys = GetComponent<InventorySystem>();
			invSys.itemCount[(int)itemScript.itemType] += 1;
			Destroy(other.gameObject);
		}
	}
}
