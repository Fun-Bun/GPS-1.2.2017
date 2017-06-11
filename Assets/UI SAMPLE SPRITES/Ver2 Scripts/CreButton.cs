using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreButton : MonoBehaviour {

    public GameObject Menu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        Debug.Log("testing");
        Menu.GetComponent<MainMenu>().Initial = false;
        Menu.GetComponent<MainMenu>().Options = false;
        Menu.GetComponent<MainMenu>().Credits = true;

    }
}
