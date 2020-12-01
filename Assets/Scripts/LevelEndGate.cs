using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndGate : MonoBehaviour
{
    private bool hasLoadedNextScene = false;
    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(!hasLoadedNextScene){
            hasLoadedNextScene = true;
            sceneLoader.SendMessage("LoadNextScene");
        }
    }
    
}
