using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Vector3 Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Makes the camera Player-Centric
        Player = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = new Vector3 (Player.x, Player.y, transform.position.z);;
    }
}
