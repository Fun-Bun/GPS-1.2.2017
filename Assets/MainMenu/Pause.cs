using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Pause : MonoBehaviour {

    public bool Paused;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!Paused)
            {
                Paused = true;
            }
            else
            {
                Paused = false;
            }
        }
        Pausing();
		
	}


   void Pausing()
    {
        if(Paused == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

}
