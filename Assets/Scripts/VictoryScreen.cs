using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    private SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerCanvas = GameObject.Find("PlayerCanvas");
        playerCanvas.SetActive(false);

        sceneLoader = FindObjectOfType<SceneLoader>();

        Invoke("LoadFirstLevel", 3f);
    }

    private void LoadFirstLevel()
    {
        sceneLoader.SendMessage("PlayerHasDied");
    }
}
