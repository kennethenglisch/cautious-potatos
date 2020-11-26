using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource[] audioSources = null;
    bool playedStart = false;
    bool playedMainMenu = false;
    bool playedBossStage = false;
    bool playedBackground = false;
    
    bool firstTime = true;
    private void Awake()
    {
        int numMusicPlayer = FindObjectsOfType<MusicPlayer>().Length;
        if (numMusicPlayer > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        audioSources= GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        String sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Equals("LevelStart"))
        {    
            // !played in einer separaten if, weil der da sonst trotzdem reingeht, auch wenn test = false ist
            if (!playedStart)
            {
                if (firstTime)
                {
                    StopAllAudio(audioSources[0]);
                    audioSources[0].Play();
                    playedStart = true;
                    firstTime = false;

                }
                else
                {
                    StopAllAudio(audioSources[1]);
                    audioSources[1].Play();
                    playedStart = true;
                }
            }
            
            playedMainMenu = false;
            playedBossStage = false;
            playedBackground = false;
        }
        else if (sceneName.Equals("MainMenu") || sceneName.Equals("DeathScreen"))
        {
            if (!playedMainMenu){
                StopAllAudio(audioSources[2]);
                audioSources[2].Play();
                playedMainMenu = true;
            
            }
            playedStart = false;
            playedBossStage = false;
            playedBackground = false;

        }
        else if (sceneName.Equals("LevelBossStage"))
        {
            if (!playedBossStage){
                StopAllAudio(audioSources[3]);
                audioSources[3].Play();
                playedBossStage = true;
            
            }
            playedStart = false;
            playedMainMenu = false;
            playedBackground = false;

        }
        else
        {
            if (!playedBackground){
                StopAllAudio(audioSources[4]);
                audioSources[4].Play();
                playedBackground = true;
            
            }
            playedStart = false;
            playedMainMenu = false;
            playedBossStage = false;
        }
    }
    void StopAllAudio(AudioSource aS) {
        foreach(AudioSource audioS in audioSources) {
            if (aS.clip == audioS.clip)
                continue;
            audioS.Stop();
        }
    }
}
