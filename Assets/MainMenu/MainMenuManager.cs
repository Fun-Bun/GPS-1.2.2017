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

    public void ChangeBGM(GameObject slider)
    {
        foreach(AudioSource source in soundManager.BGM)
        {
            source.volume = slider.GetComponent<Slider>().value;
        }
    }
}
