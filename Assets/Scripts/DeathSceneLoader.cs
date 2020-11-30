using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSceneLoader : MonoBehaviour
{
        private SceneLoader sceneLoader;
        private void Start()
        {
            Debug.Log("Start Death Screen");
            
            sceneLoader = FindObjectOfType<SceneLoader>();
        
            Invoke("LoadFirstLevel", 3f);
        }
    

        private void LoadFirstLevel()
        {
            sceneLoader.SendMessage("PlayerHasDied");
        }
    
}
