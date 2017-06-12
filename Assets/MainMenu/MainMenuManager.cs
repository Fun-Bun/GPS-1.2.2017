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
    public List<GameObject> menuWindows;

    public SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("Singleton").GetComponent<GameManager>().soundManager;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Saferoom");
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
        slider.GetComponent<Slider>().value = soundManager.BGMVolume;
    }

    public void SetupSoundFX(GameObject slider)
    {
        slider.GetComponent<Slider>().value = soundManager.SoundFXVolume;
    }

    public void ChangeBGM(GameObject slider)
    {
        soundManager.BGMVolume = slider.GetComponent<Slider>().value;
        soundManager.SetVolume();
    }
}
