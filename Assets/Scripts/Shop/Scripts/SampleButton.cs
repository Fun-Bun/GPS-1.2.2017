using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour 
{

	public Button buttonComponent;
	public Text nameLabel;
	public Image iconImage;
	public Text priceText;


	private Item1 item1;
	private ShopScrollList scrollList;

	// Use this for initialization
	void Start () 
	{
		buttonComponent.onClick.AddListener (HandleClick);//0127173797
	}

	public void Setup(Item1 currentItem, ShopScrollList currentScrollList)
	{
		item1 = currentItem;
		nameLabel.text = item1.itemName;
		iconImage.sprite = item1.icon;
		priceText.text = item1.price.ToString ();
		scrollList = currentScrollList;

	}

	public void HandleClick()
	{
		scrollList.TryTransferItemToOtherShop (item1);
	}
}