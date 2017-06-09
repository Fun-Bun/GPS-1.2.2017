using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUItrigger : MonoBehaviour {


	public bool showText = false;
	public GameObject Button;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			showText = true;

		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			showText = false;
		}
	}

	void changeScene (){
		if (Input.GetKeyDown (KeyCode.E) && showText == true) {
			//GameObject.Find ("EventSystem").SetActive (false);
			SceneManager.LoadScene ("UIScene",LoadSceneMode.Additive);
		}
	}
		
			


	void Update()
	{
		Button.SetActive (showText);
		changeScene();

	}

}
	