using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public static SoundManager instance;
    public AudioClip[] clip;
    public AudioClip[] Playerclip;
    public float val1;
    public float val2;

    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
        BGSoundVolume(val1);
        SFXVolume(val2);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) //로드할때마다나옴
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(go, clip.length);
    }

    public void Click()
    {
        SFXPlay("Click", clip[0]);
    }
    public void NPC()
    {
        SFXPlay("NPC", clip[1]);
    }
    public void Click2()
    {
        SFXPlay("Click2", clip[2]);
    }

    public void SoundPlay(int num)
    {
        SFXPlay("SFX"+num, clip[num]);
    }

    public void BGSoundVolume(float val)
    {
        val1 = val;
        mixer.SetFloat("BGSound", Mathf.Log10(val) * 20);
    }

    public void SFXVolume(float val)
    {
        val2 = val;
        mixer.SetFloat("SFX", Mathf.Log10(val) * 20);
    }
    public void PlayerSound(int i)
    {
        SFXPlay("Player", Playerclip[i]);
    }
}
