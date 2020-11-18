using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Author: Marie Lencer
 * Date: 03.11.2020
 * 
 * Version: 1.0
 */


public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool gamePaused = false;

    private SceneLoader sceneLoader;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    private void Start()
    {
        
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void resumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void pauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void returnToMainMenu()
    {
        resumeGame();
        sceneLoader.SendMessage("PlayerHasDied");
       // SceneManager.LoadScene("MainMenu");
    }
}
