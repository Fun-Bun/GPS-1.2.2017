using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioSource> BGM;
    public List<AudioSource> SoundFX;

    void Start()
    {
        TrackAudio();
    }

    void TrackAudio()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("BGM"))
        {
            BGM.Add(go.GetComponent<AudioSource>());
        }
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("SoundFX"))
        {
            SoundFX.Add(go.GetComponent<AudioSource>());
        }
    }
}
