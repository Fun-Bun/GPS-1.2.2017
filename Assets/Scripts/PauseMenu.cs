using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public GameObject PauseMenuUI;

	public bool paused = false;

	void Start()
	{
		PauseMenuUI.SetActive(false);
	}

	void Update()
	{
		if(Input.GetButtonDown("Pause"))
		{
			paused = !paused;
		}

		if(paused)
		{
			SoundManagerScript.Instance.StopLoopingSFX(AudioClipID.SFX_WALKING);
			PauseMenuUI.SetActive(true);
			Time.timeScale = 0f;
		}

		if(!paused)
		{
			PauseMenuUI.SetActive(false);
			Time.timeScale = 1.0f;
		}
	}

	public void Resume()
	{
		paused = false;
	}
}
