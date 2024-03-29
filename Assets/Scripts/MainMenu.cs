﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Author: Marie Lencer
 * Date: 29.10.2020
 * 
 * Version: 1.0
 */

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        Time.timeScale = 0f;
    }

    public void playGame()
    {
        Time.timeScale = 1f;
        SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.SendMessage("LoadNextScene");
    }

    public void quitGame(){
        Application.Quit();
    }

    
}
