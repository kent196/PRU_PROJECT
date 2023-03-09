using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private Scene scene;
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Start")
        {
            PlayMusic("LobbyBGM");
        }
        else PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.volume = 0.5f;
            musicSource.Play();
        }
    }

    public void PauseMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Pause();
        }
    }


    public void PlaySFX(string name, float vol)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound SFX not found");
        }
        else
        {
            sfxSource.volume = vol;
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void MusicVolume(float vol)
    {
        musicSource.volume = vol;
    }

    public void SFXVolume(float vol)
    {
        sfxSource.volume = vol;
    }


}
