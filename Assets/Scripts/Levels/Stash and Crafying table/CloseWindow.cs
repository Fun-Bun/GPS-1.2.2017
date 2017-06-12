using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : MonoBehaviour {

	public GameObject craftingWindow;
	public Player player;

	public void Close ()
	{
		craftingWindow.SetActive(false);
		player.controls.enabled = true;
	}
}
