using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameManager mainManager;

    public float BGMVolume;
    public float SoundFXVolume;

    public List<AudioSource> BGM;
    public List<AudioSource> SoundFX;

    void Start()
    {
        BGMVolume = 1.0f;
        SoundFXVolume = 1.0f;
        TrackAudio();
    }

    public void TrackAudio()
    {
        BGM.Clear();
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("BGM"))
        {
            BGM.Add(go.GetComponent<AudioSource>());
        }

        SoundFX.Clear();
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("SoundFX"))
        {
            SoundFX.Add(go.GetComponent<AudioSource>());
        }

        SetVolume();
    }

    public void SetVolume()
    {
        foreach(AudioSource source in BGM)
        {
            source.volume = BGMVolume;
        }
        foreach(AudioSource source in SoundFX)
        {
            source.volume = SoundFXVolume;
        }
    }
}
