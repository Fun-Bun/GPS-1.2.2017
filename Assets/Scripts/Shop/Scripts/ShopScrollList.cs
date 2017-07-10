using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Item1
{
	public string itemName;
	public Sprite icon;
	public float price = 1;
}

public class ShopScrollList : MonoBehaviour 
{

	public List<Item1> itemList;
	public Transform contentPanel;
	public ShopScrollList otherShop;
	public Text myGoldDisplay;
	public SimpleObjectPool buttonObjectPool;

	public float gold;


	// Use this for initialization
	void Start () 
	{
		RefreshDisplay ();
	}

	void RefreshDisplay()
	{
		myGoldDisplay.text = "You Have : " + gold + " G".ToString ();
		RemoveButtons ();
		AddButtons ();
	}

	private void RemoveButtons()
	{
		while (contentPanel.childCount > 0) 
		{
			GameObject toRemove = transform.GetChild(0).gameObject;
			buttonObjectPool.ReturnObject(toRemove);
		}
	}

	private void AddButtons()
	{
		for (int i = 0; i < itemList.Count; i++) 
		{
			Item1 item1 = itemList[i];
			GameObject newButton = buttonObjectPool.GetObject();
			newButton.transform.SetParent(contentPanel, false);

			SampleButton sampleButton = newButton.GetComponent<SampleButton>();
			sampleButton.Setup(item1, this);
		}
	}

	public void TryTransferItemToOtherShop(Item1 item1)
	{
		if (otherShop.gold >= item1.price) 
		{
			gold += item1.price;
			otherShop.gold -= item1.price;

			AddItem(item1, otherShop);
			RemoveItem(item1, this);

			RefreshDisplay();
			otherShop.RefreshDisplay();
			Debug.Log ("enough gold");

		}

		Debug.Log ("attempted");
	}

	void AddItem(Item1 itemToAdd, ShopScrollList shopList)
	{
		shopList.itemList.Add (itemToAdd);
	}

	private void RemoveItem(Item1 itemToRemove, ShopScrollList shopList)
	{
		for (int i = shopList.itemList.Count - 1; i >= 0; i--) 
		{
			if (shopList.itemList[i] == itemToRemove)
			{
				shopList.itemList.RemoveAt(i);
			}
		}
	}
}