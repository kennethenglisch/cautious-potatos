using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject player;

    public bool spawned = false;
    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        if (!spawned)
        {
            player.transform.position = gameObject.transform.position;
            spawned = true;
        }

    }
}
