using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
	Gun = 0,
	Ammo,
	Scrap,
	Food,
	TotalItems,
	None
};

[System.Serializable]
public class Item
{
    public ItemType type;
	public int amount;

	public Item(ItemType type, int amount = 1)
	{
		this.type = type;
		this.amount = amount;
	}
	
	public int GetTotalWeight()
	{
		return GetWeight(type) * amount;
	}

	public string GetName()
	{
		return GetName (type);
	}

	public string GetDesc()
	{
		return GetDesc (type);
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

	public static string GetName(ItemType type)
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

	public static string GetDesc(ItemType type)
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

[System.Serializable]
public class Blueprint
{
	public Item source1;
	public Item source2;

	public Item result;
}
