using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public ItemType type;

    public Item(ItemType type)
    {
        this.type = type;
    }

    public static int GetWeight(ItemType type)
    {
        switch(type)
        {
            case ItemType.Gun:
                return 5;
            case ItemType.Ammo:
                return 1;
            case ItemType.Scrap:
                return 1;
            case ItemType.Food:
                return 2;
            default:
                return 0;
        }
    }

    public string GetName()
    {
        switch(type)
        {
            case ItemType.Gun:
                return "Gun";
            case ItemType.Ammo:
                return "Ammo";
            case ItemType.Scrap:
                return "Scrap";
            case ItemType.Food:
                return "Food";
            default:
                return "Unknown";
        }
    }

    public string GetDesc()
    {
        switch(type)
        {
            case ItemType.Gun:
                return "Primary weapon to defend yourself.";
            case ItemType.Ammo:
                return "No gun works without the magazine.";
            case ItemType.Scrap:
                return "Might be useful for crafting.";
            case ItemType.Food:
                return "Fills your stomach. Yummy!";
            default:
                return "No Description.";
        }
    }
}

public enum ItemType
{
	Gun = 0,
	Ammo,
	Scrap,
	Food,
	TotalItems
};

public class DroppedItem : MonoBehaviour
{
    public ItemType type;
    public int amount;

	//Extra: Use Start() to add texture according to the itemType
}
