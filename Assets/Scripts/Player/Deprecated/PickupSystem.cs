using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    /*
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Item")
		{
			ItemScript itemScript = other.gameObject.GetComponent<ItemScript>();
			switch(itemScript.itemType)
			{
				case ItemType.Food:
					GetComponent<HealthManager>().CurrentHunger = GetComponent<HealthManager>().MaxHunger;
					GetComponent<HealthManager>().CurrentHealth = GetComponent<HealthManager>().MaxHealth;
				break;
				default:
					InventorySystem invSys = GetComponent<InventorySystem>();
					invSys.itemCount[(int)itemScript.itemType] += 1;
				break;
			}
			Destroy(other.gameObject);
		}
	}*/
}
