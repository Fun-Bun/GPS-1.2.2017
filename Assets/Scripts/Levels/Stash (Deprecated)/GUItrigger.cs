using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUItrigger : MonoBehaviour {


	public bool showText = false;
	public GameObject Button;

	public GameObject craftingWindow;

	public Player player;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			showText = true;

		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			showText = false;
		}
	}

	void PopOutUi (){
		if (Input.GetKeyDown (KeyCode.E) && showText == true)
		{
			//GameObject.Find ("EventSystem").SetActive (false);
			craftingWindow.SetActive(true);
			player.controls.enabled = false;
			player.animator.SetFloat("Speed", 0f);
		}
	}

	void Update()
	{
		Button.SetActive (showText);
		PopOutUi();

	}

}
	