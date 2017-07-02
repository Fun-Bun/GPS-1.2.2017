using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AudioClipID
{
    BGM_MAIN_MENU = 0,
    BGM_GAMEPLAY = 1,
    BGM_OPTIONAL = 2,
    BGM_OPTIONAL1 = 3,
    BGM_OPTIONAL2 = 4,

    SFX_ATTACK = 100,
    SFX_WALK = 101,
    SFX_ENEMY= 102,
    SFX_LASER = 103,

}

[System.Serializable]
public class AudioClipInfo
{
    public AudioClipID audioClipID;
    public AudioClip audioClip;
}

public class SoundsBox : MonoBehaviour
{
    private static SoundsBox mInstance;

    public static SoundsBox Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject tempObject = GameObject.FindWithTag("SoundManager");
                if (tempObject == null)
                {
                    tempObject = tempObject = Instantiate(PrefabManager.Instance.SoundManagerPrefab, Vector3.zero, Quaternion.identity);
                }
                mInstance = tempObject.GetComponent<SoundsBox>();
                DontDestroyOnLoad(mInstance.gameObject);
            }
            return mInstance;
        }
    }
    public static bool CheckInstanceExist()
    {
        return mInstance;
    }

    public float bgmVolume = 1.0f;
    public float sfxVolume = 1.0f;


    public List<AudioClipInfo> audioClipInfoList = new List<AudioClipInfo>();

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;

    public List<AudioSource> sfxAudioSourceList = new List<AudioSource>();
    public List<AudioSource> bgmAudioSourceList = new List<AudioSource>();

    // Use this for initialization
    void Awake()
    {
        if (SoundsBox.CheckInstanceExist())
        {
            Destroy(this.gameObject);
        }
        AudioSource[] audioSourceList = this.GetComponentsInChildren<AudioSource>();

        if (audioSourceList[0].gameObject.name == "BGMAudioSource")
        {
            bgmAudioSource = audioSourceList[0];
            sfxAudioSource = audioSourceList[1];
        }
        else
        {
            bgmAudioSource = audioSourceList[1];
            sfxAudioSource = audioSourceList[0];
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    AudioClip FindAudioClip(AudioClipID audioClipID)
    {
        for (int i = 0; i < audioClipInfoList.Count; i++)
        {
            if (audioClipInfoList[i].audioClipID == audioClipID)
            {
                return audioClipInfoList[i].audioClip;
            }
        }

        Debug.LogError("Cannot Find Audio Clip : " + audioClipID);

        return null;
    }

    //! BACKGROUND MUSIC (BGM)
    public void PlayBGM(AudioClipID audioClipID)
    {
        bgmAudioSource.clip = FindAudioClip(audioClipID);
        Debug.Log(audioClipID);
        bgmAudioSource.volume = bgmVolume;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    public void PauseBGM(AudioClipID audioClipID)
    {
        if (bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Pause();
        }
    }

    public void StopBGM(AudioClipID audioClipID)
    {
        if (bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Stop();
        }
    }


    //! SOUND EFFECTS (SFX)
    public void PlaySFX(AudioClipID audioClipID)
    {
        sfxAudioSource.PlayOneShot(FindAudioClip(audioClipID), sfxVolume);
    }

    public void PlayLoopingSFX(AudioClipID audioClipID)
    {
        AudioClip clipToPlay = FindAudioClip(audioClipID);

        for (int i = 0; i < sfxAudioSourceList.Count; i++)
        {
            if (sfxAudioSourceList[i].clip == clipToPlay)
            {
                if (sfxAudioSourceList[i].isPlaying)
                {
                    return;
                }

                sfxAudioSourceList[i].volume = sfxVolume;
                sfxAudioSourceList[i].Play();
                return;
            }
        }

        AudioSource newInstance = gameObject.AddComponent<AudioSource>();
        newInstance.clip = clipToPlay;
        newInstance.volume = sfxVolume;
        newInstance.loop = true;
        newInstance.Play();
        sfxAudioSourceList.Add(newInstance);
    }

    public void PauseLoopingSFX(AudioClipID audioClipID)
    {
        AudioClip clipToPause = FindAudioClip(audioClipID);

        for (int i = 0; i < sfxAudioSourceList.Count; i++)
        {
            if (sfxAudioSourceList[i].clip == clipToPause)
            {
                sfxAudioSourceList[i].Pause();
                return;
            }
        }
    }

    public void StopLoopingSFX(AudioClipID audioClipID)
    {
        AudioClip clipToStop = FindAudioClip(audioClipID);

        for (int i = 0; i < sfxAudioSourceList.Count; i++)
        {
            if (sfxAudioSourceList[i].clip == clipToStop)
            {
                sfxAudioSourceList[i].Stop();
                return;
            }
        }
    }

    public void ChangePitchLoopingSFX(AudioClipID audioClipID, float value)
    {
        AudioClip clipToStop = FindAudioClip(audioClipID);

        for (int i = 0; i < sfxAudioSourceList.Count; i++)
        {
            if (sfxAudioSourceList[i].clip == clipToStop)
            {
                sfxAudioSourceList[i].pitch = value;
                return;
            }
        }
    }

    public void SetBGMVolume(float value)
    {
        bgmVolume = value;
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
    }
}