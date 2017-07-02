using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundsBox.Instance.PlaySFX(AudioClipID.SFX_ATTACK);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            SoundsBox.Instance.PlayLoopingSFX(AudioClipID.SFX_WALK);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            SoundsBox.Instance.StopLoopingSFX(AudioClipID.SFX_WALK);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            SoundsBox.Instance.PlayBGM(AudioClipID.BGM_MAIN_MENU);
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            SoundsBox.Instance.PauseBGM(AudioClipID.BGM_MAIN_MENU);
        }
    }
}