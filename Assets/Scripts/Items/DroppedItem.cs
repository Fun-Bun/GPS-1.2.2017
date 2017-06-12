using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DroppedItem : MonoBehaviour
{
	public ItemSpriteStorage itemSpriteStorage;

	public Item data;

	void Update()
	{
		GetComponent<SpriteRenderer>().sprite = itemSpriteStorage.spriteList[(int)data.type];
	}
}
