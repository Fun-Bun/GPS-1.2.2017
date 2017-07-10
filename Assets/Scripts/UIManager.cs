using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour 
{
	void Start()
	{
	}

	public void SetupBGM(GameObject slider)
	{
		slider.GetComponent<Slider>().value = SoundManagerScript.Instance.bgmVolume;
	}

	public void SetupSFX(GameObject slider)
	{
		slider.GetComponent<Slider>().value = SoundManagerScript.Instance.sfxVolume;
	}

	public void ChangeBGM(GameObject slider)
	{
		SoundManagerScript.Instance.SetBGMVolume(slider.GetComponent<Slider>().value);
	}

	public void ChangeSFX(GameObject slider)
	{
		SoundManagerScript.Instance.SetSFXVolume(slider.GetComponent<Slider>().value);
	}
}
