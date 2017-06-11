using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject OptionButton;
    public GameObject CreditButton;
    public GameObject StartButton;
    public GameObject OptionMenu;
    public GameObject CreditMenu;

    public GameObject Menu;

    public bool Initial;
    public bool Options;
    public bool Credits;

	// Use this for initialization
	void Start ()
    {
        Initial = true;
        Options = false;
        Credits = false;
	}
	
	// Update is called once per frame
	void Update ()
    { 
        if (Initial == true)
        {
            Options = false;
            Credits = false;
            OptionButton.SetActive(true);
            CreditButton.SetActive(true);
            StartButton.SetActive(true);
            OptionMenu.SetActive(false);
            CreditMenu.SetActive(false);

            Menu.GetComponent<BoxCollider2D>().enabled = false;
        }

        else if (Options == true)
        {
            Initial = false;
            Credits = false;
            OptionButton.SetActive(false);
            CreditButton.SetActive(false);
            StartButton.SetActive(false);
            OptionMenu.SetActive(true);
            CreditMenu.SetActive(false);

            Menu.GetComponent<BoxCollider2D>().enabled = true;
        }

        else if (Credits == true)
        {
            Options = false;
            Initial = false;
            OptionButton.SetActive(false);
            CreditButton.SetActive(false);
            StartButton.SetActive(false);
            OptionMenu.SetActive(false);
            CreditMenu.SetActive(true);

            Menu.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void OnMouseDown()
    {
        if(Options)
        {
            Initial = true;
        }
        else if(Credits)
        {
            Initial = true;
        }

    }
}
