using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingWindow : MonoBehaviour
{
	public ItemSpriteStorage itemSpriteStorage;
	public BlueprintStorage blueprintStorage;

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
		for(int i = 0; i < blueprintStorage.blueprintList.Capacity; i++)
		{
			GameObject slot = content.transform.GetChild(i).gameObject;
			Blueprint blueprint = blueprintStorage.blueprintList[i];

			slot.SetActive(inventory.unlockedBlueprints.Contains(blueprint));

			GameObject source1 = slot.transform.GetChild(0).gameObject;
			GameObject source2 = slot.transform.GetChild(1).gameObject;
			GameObject result = slot.transform.GetChild(3).gameObject;

			source1.GetComponentsInChildren<Image>()[1].sprite = itemSpriteStorage.spriteList[(int)blueprint.source1.type];
			source2.GetComponentsInChildren<Image>()[1].sprite = itemSpriteStorage.spriteList[(int)blueprint.source2.type];
			result.GetComponentsInChildren<Image>()[1].sprite = itemSpriteStorage.spriteList[(int)blueprint.result.type];

			source1.GetComponentInChildren<Text>().text = (blueprint.source1.type != ItemType.None ? blueprint.source1.amount.ToString() : "");
			source2.GetComponentInChildren<Text>().text = (blueprint.source2.type != ItemType.None ? blueprint.source2.amount.ToString() : "");
			result.GetComponentInChildren<Text>().text = (blueprint.result.type != ItemType.None ? blueprint.result.amount.ToString() : "");
		}
	}

	public void Craft(int recipe)
	{
		Blueprint blueprint = blueprintStorage.blueprintList[recipe];

		if
		(
			inventory.HasEnoughItem(blueprint.source1.type, blueprint.source1.amount) &&
			inventory.HasEnoughItem(blueprint.source2.type, blueprint.source2.amount)
		)
		{
			inventory.RemoveItem(blueprint.source1.type, blueprint.source1.amount);
			inventory.RemoveItem(blueprint.source2.type, blueprint.source2.amount);
			inventory.AddItem(blueprint.result.type, blueprint.result.amount);
		}
	}
		
}
