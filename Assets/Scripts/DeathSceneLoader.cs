using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSceneLoader : MonoBehaviour
{
        private SceneLoader sceneLoader;
        private void Start()
        {
        
            sceneLoader = FindObjectOfType<SceneLoader>();
        
            Invoke("LoadFirstLevel", 3f);
        }
    

        private void LoadFirstLevel()
        {
            sceneLoader.SendMessage("PlayerHasDied");
        }
    
}
