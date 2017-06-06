using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	Gun = 0,
	Ammo,
	Scrap,
	Food,
	TotalItems
};

public class ItemScript : MonoBehaviour
{
	public ItemType itemType;

	//Extra: Use Start() to add texture according to the itemType
}
