using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUItrigger : MonoBehaviour
{
	public bool showText = false;
	public GameObject Button;

	public GameObject ShopWindow;

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

	void PopOutUi ()
    {
		if (Input.GetKeyDown (KeyCode.E) && showText == true)
		{
			SoundManagerScript.Instance.StopLoopingSFX(AudioClipID.SFX_WALKING);
			//GameObject.Find ("EventSystem").SetActive (false);
			ShopWindow.SetActive(true);
            player.DisableControls();
		}
	}

	public void CloseShop()
	{
		ShopWindow.SetActive (false);
		player.EnableControls();
	}

	void Update()
	{
		Button.SetActive (showText && !ShopWindow.activeSelf);
		PopOutUi();
	}

}
	