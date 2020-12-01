using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int count = 0;
    [SerializeField] int[] order = new int [6];
    [SerializeField] public GameObject playerCanvas;
    private void Awake()
    {
        int numMusicPlayer = FindObjectsOfType<SceneLoader>().Length;
        if (numMusicPlayer > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        RandomSceneOrder();

    }
    

    public void LoadNextScene()
    {
        count++;
        Invoke("LoadTheScene", 0.25f);
        
    }

    void LoadTheScene()
    {
        if(count == 1)
        {
            GameObject player = FindObjectOfType<PlayerController>().gameObject;
            player.SendMessage("resetStats");
            playerCanvas.SetActive(true);
        }
        SceneManager.LoadScene(order[count]);

    }

    public void PlayerHasDied()
    {
        RandomSceneOrder();
        LoadTheScene();
    }

    public void PlayerWon()
    {
        RandomSceneOrder();
        LoadTheScene();
    }

    void RandomSceneOrder()
    {
        count = 0;
        order = new int [6];
        for (int i = 2; i < 5; i++)
        {
            bool fits = true;
            
            int temp = Random.Range(2, 5);
            for (int y = 2; y < 5; y++)
            {
                if (temp == order[y])
                {
                    fits = false;
                }
            }

            if (!fits)
            {
                if(i > 2)
                    i--;
            }
            else
            {
                order[i] = temp;
            }

        }

        order[0] = 0;
        order[1] = 1;
        order[5] = 5;
    }
}
