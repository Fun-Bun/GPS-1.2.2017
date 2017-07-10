using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string FirstLoad;

    [System.NonSerialized]
    public SoundManager soundManager;

	// Initialization
	void Awake ()
    {
        soundManager = GetComponent<SoundManager>();

        if(soundManager != null) soundManager.mainManager = this;

        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(FirstLoad);
	}
}
