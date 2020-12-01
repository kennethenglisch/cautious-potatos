using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSceneLoader : MonoBehaviour
{
        private SceneLoader sceneLoader;
        private void Start()
        {
            Debug.Log("Start Death Screen");
            GameObject playerCanvas = GameObject.Find("PlayerCanvas");
            playerCanvas.SetActive(false);

            sceneLoader = FindObjectOfType<SceneLoader>();
        
            Invoke("LoadFirstLevel", 3f);
        }
    

        private void LoadFirstLevel()
        {
            sceneLoader.SendMessage("PlayerWon");
        }
    
}
