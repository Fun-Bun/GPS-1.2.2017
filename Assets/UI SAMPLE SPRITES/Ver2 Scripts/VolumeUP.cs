using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeUP : MonoBehaviour {

    public AudioSource BGM;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        BGM.GetComponent<BGM>().volume += 0.1f;
    }
}
