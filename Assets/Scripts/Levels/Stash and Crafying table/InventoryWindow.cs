using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : MonoBehaviour
{
	public ItemSpriteStorage itemSpriteStorage;

	public PlayerInventory inventory;
	public GameObject content;
	// Use this for initialization
	void Start ()
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inventory;
	}
	
	// Update is called once per frame
	void Update ()
	{
		RefreshContents();
	}

	void RefreshContents()
	{
		for(int i = 0; i < inventory.items.Capacity; i++)
		{
			GameObject slot = content.transform.GetChild(i).gameObject;
			Item item = inventory.items[i];

			slot.SetActive(item.amount > 0);
			slot.GetComponentsInChildren<Image>()[1].sprite = itemSpriteStorage.spriteList[(int)item.type];
			slot.GetComponentInChildren<Text>().text = item.amount.ToString();
		}
	}
}
