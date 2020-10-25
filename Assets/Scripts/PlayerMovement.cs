using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Marie Lencer
 * Version: 1.0
 * Date: 25.10.2020
 */

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    //private Rigidbody2D rB2D;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        //rB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movementRB = new Vector2(horizontalInput, verticalInput);
        
        
        rB2D.AddForce(movementRB * speed);
    }*/

    void Update(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0.0f);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
