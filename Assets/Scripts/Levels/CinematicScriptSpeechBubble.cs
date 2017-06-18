using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicScriptSpeechBubble : MonoBehaviour
{
    public GameObject speechBubble;
    public GameObject tutorialText;

    public int currentScene;
    public float currentSceneTime;
    public List<float> sceneTime;

	// Use this for initialization
	void Start ()
    {
        currentScene = 0;
        currentSceneTime = 0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(currentScene >= sceneTime.Capacity)
        {
            tutorialText.SetActive(true);
            speechBubble.SetActive(false);
            this.gameObject.SetActive(false);
            return;
        }

        currentSceneTime += Time.deltaTime;
        if(currentSceneTime >= sceneTime[currentScene])
        {
            currentSceneTime = 0f;
            currentScene++;
        }

        switch(currentScene)
        {
            case 0:
                speechBubble.SetActive(true);
                break;
        }
	}
}
