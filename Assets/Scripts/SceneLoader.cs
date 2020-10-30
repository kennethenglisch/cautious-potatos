using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        Invoke("LoadFirstLevel", 3f);
    }



    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
}
