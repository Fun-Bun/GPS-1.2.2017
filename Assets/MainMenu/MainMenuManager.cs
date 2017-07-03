using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Menu
{
    Main = 0,
    Options,
    Credits
}

public class MainMenuManager : MonoBehaviour
{
    public string startGameScene;
    public List<GameObject> menuWindows;

    //public SoundManager soundManager;

    /*void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("Singleton").GetComponent<GameManager>().soundManager;
    }*/

	void Start()
	{
		SoundManagerScript.Instance.PlayBGM(AudioClipID.BGM_MAIN_MENU);
	}

    public void StartGame()
    {
        SceneManager.LoadScene(startGameScene);
    }

    public void OpenMenu(int menu)
    {
        menuWindows[menu].SetActive(true);
    }

    public void CloseMenu(int menu)
    {
        menuWindows[menu].SetActive(false);
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
