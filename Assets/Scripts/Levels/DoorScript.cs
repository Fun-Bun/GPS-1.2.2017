using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public string sceneName;
    bool isTriggered;
    float animTimer;

	// Use this for initialization
	void Start ()
    {
        isTriggered = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isTriggered)
        {
            animTimer += Time.deltaTime;
            if(animTimer > 2f)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
	}

	void OnTriggerStay2D(Collider2D other)
	{
        if(other.tag == "Player" && !isTriggered)
		{
            isTriggered = true;
			SoundManagerScript.Instance.StopLoopingSFX(AudioClipID.SFX_WALKING);
            other.GetComponent<Player>().DisableControls();
            GetComponent<Animator>().Play("NormalDoor_Opening");
		}
	}
}
