using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [System.NonSerialized]
	public Player self;

    public List<Item> items;
	public List<Blueprint> unlockedBlueprints;

	// Use this for initialization
	void Start ()
	{
		for(int i = 0; i < (int)ItemType.TotalItems; i++)
		{
			items.Add(new Item((ItemType)i, 0));
		}

		unlockedBlueprints.Add(self.blueprintStorage.blueprintList[0]);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
    }

	public void AddItem(ItemType type, int amount)
	{
		if(type == ItemType.None) return;

		items[(int)type].amount += amount;
	}

	public void RemoveItem(ItemType type, int amount)
	{
		if(type == ItemType.None) return;

		items[(int)type].amount -= amount;
		if(items[(int)type].amount < 0) items[(int)type].amount = 0;
	}

	public void DropItem(ItemType type, int amount)
	{
		if(type == ItemType.None) return;

		RemoveItem(type, amount);

		//Drop Item
	}

	public bool HasEnoughItem(ItemType type, int amount)
	{
		if(type == ItemType.None) return true;

		return items[(int)type].amount >= amount;
	}
}
