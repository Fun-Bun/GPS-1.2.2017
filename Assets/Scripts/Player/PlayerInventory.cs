using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropReason
{
    Intentionally = 0,
    FullInventory
}

public class PlayerInventory : MonoBehaviour
{
    [System.NonSerialized]
	public Player self;

    public List<Item> items;
    public Resource occupiedSpace;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
    }

    public void AddItem(ItemType type, int amount)
    {
        for(int i = amount; i >= 0; i--)
        {
            if(occupiedSpace.Extend(Item.GetWeight(type)) > 0)
            {
                DropItem(type, i, DropReason.FullInventory);
                break;
            }

            items.Add(new Item(type));
        }
    }

    public void DropItem(ItemType type, int amount, DropReason reason = DropReason.Intentionally)
    {
        for(int i = amount; i >= 0; i--)
        {
            Item sample = new Item(type);
            if(!items.Contains(sample)) break;

            if(reason == DropReason.Intentionally)
            {
                occupiedSpace.Reduce(Item.GetWeight(type));
            }
            else if(reason == DropReason.FullInventory)
            {
                //Drop Item
            }
            
            items.Remove(sample);
        }
    }
}
