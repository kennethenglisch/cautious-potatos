using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSceneLoader : MonoBehaviour
{
        private GameObject sceneLoader;

        private bool hasLoadedNextScene = false;
        private void Start()
        {
        
            sceneLoader = GameObject.FindGameObjectWithTag("SceneLoader");
        
            if(!hasLoadedNextScene){
                sceneLoader.SendMessage("PlayerHasDied");
                hasLoadedNextScene = true;
            }
        }
    

        private void LoadFirstLevel()
        {
        
            sceneLoader.SendMessage("PlayerDied");
        }
    
}
