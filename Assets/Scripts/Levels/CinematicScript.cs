using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicScript : MonoBehaviour
{
    public Player player;
    public GameObject canvas;
    public GameObject cam;
    public GameObject speechBubble;
    public GameObject tutorialText;
    public Transform camStart;
    public Transform camEnd;

    public int currentScene;
    public float currentSceneTime;
    public List<float> sceneTime;

	// Use this for initialization
	void Start ()
    {
		//
		SoundManagerScript.Instance.StopBGM();
		SoundManagerScript.Instance.PlayBGM(AudioClipID.BGM_LEVEL1);

        currentScene = 0;
        currentSceneTime = 0f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.DisableControls();
        player.enabled = false;
        canvas.SetActive(false);
        cam.transform.position = camStart.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(currentScene >= sceneTime.Capacity)
        {
            player.animator.Play("Player_Idle");
            player.renderer.flipX = true;
            player.enabled = true;
            player.EnableControls();
            tutorialText.SetActive(true);
            speechBubble.SetActive(false);
            canvas.SetActive(true);
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
                cam.transform.position = Vector3.Lerp(camStart.position, camEnd.position, currentSceneTime / sceneTime[0]);
                player.animator.Play("Player_Lying");
                break;
            case 1:
                cam.transform.position = camEnd.position;
                break;
            case 2:
                player.animator.Play("Player_Awoken");
                break;
            case 3:
                player.animator.Play("Player_Idle");
                speechBubble.SetActive(true);
                break;
        }
	}
}
