using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

/* Author: Kenneth Englisch
 * Date: 02.11.2020
 * 
 * Version: 1.0
 */

public class ProjectileController : MonoBehaviour
{
    [Header("Renderer")]
    [SerializeField] SpriteRenderer rend = null;
    [SerializeField] SpriteRenderer playerRenderer = null;

    [Header("Attack")]
    [Range(1, 20)][SerializeField] int attackPoints = 5;
    [Tooltip("In Seconds")][Range(1, 20)][SerializeField] int lifeTime = 3;

    private Vector3 positionRelativeToPlayer = new Vector3(0f, 0f, 0f);
    private Vector3 playerPosition = new Vector3(0f, 0f, 0f);

    private bool playerFlippedX = false;

    private float playerVerticalInput = 0f;
    private float playerHorizontalInput = 0f;

    private bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();

        playerRenderer = gameObject.transform.parent.Find("Player Character").GetComponent<SpriteRenderer>();
        playerFlippedX = playerRenderer.flipX;

        if (!playerFlippedX)
            positionRelativeToPlayer = new Vector3(0.579f, 0.162f, 0f);
        else if (playerFlippedX)
            positionRelativeToPlayer = new Vector3(-0.579f, 0.162f, 0f);

        playerPosition = gameObject.transform.parent.Find("Player Character").position;
        transform.position = new Vector3(playerPosition.x + positionRelativeToPlayer.x, playerPosition.y + positionRelativeToPlayer.y, 0);

        Invoke("setCanMove", 0.5f);
    }

    private void setCanMove()
    {
        canMove = true;
        rend.enabled = true;
        Invoke("SelfDestruction", lifeTime);
    }

    private void SelfDestruction()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
            ProcessMovement();
    }

    /*
     * TODO maybe delete to top and down
     */
    private void ProcessMovement()
    {
        if(playerVerticalInput == 0 && playerHorizontalInput == 0) // player not moving
        {
            if(!playerFlippedX)
            {
                transform.position = transform.position + new Vector3(1f, 0f, 0f) * Time.deltaTime;
            }
            else if(playerFlippedX)
            {
                transform.position = transform.position - new Vector3(1f, 0f, 0f) * Time.deltaTime;
            }
        }
        else if(playerVerticalInput == 0 && playerHorizontalInput > 0) // player moving right
        {
            transform.position = transform.position + new Vector3(1f, 0f, 0f) * Time.deltaTime;
        }
        else if(playerVerticalInput < 0 && playerHorizontalInput > 0) // player moving diagonal to bottom right
        {
            transform.position = transform.position + new Vector3(0.5f, -0.5f, 0f) * Time.deltaTime;
        }
        else if (playerVerticalInput < 0 && playerHorizontalInput == 0) // player moving down 
        {
            transform.position = transform.position + new Vector3(0f, -1f, 0f) * Time.deltaTime;
        }
        else if (playerVerticalInput < 0 && playerHorizontalInput < 0) // player moving diagonal to bottom left 
        {
            transform.position = transform.position + new Vector3(-0.5f, -0.5f, 0f) * Time.deltaTime;
        }
        else if (playerVerticalInput == 0 && playerHorizontalInput < 0) // player moving left 
        {
            transform.position = transform.position + new Vector3(-1f, 0f, 0f) * Time.deltaTime;
        }
        else if (playerVerticalInput > 0 && playerHorizontalInput < 0) // player moving diagonal to top left 
        {
            transform.position = transform.position + new Vector3(-0.5f, 0.5f, 0f) * Time.deltaTime;
        }
        else if (playerVerticalInput > 0 && playerHorizontalInput == 0) // player moving up
        {
            transform.position = transform.position + new Vector3(0f, 1f, 0f) * Time.deltaTime;
        }
        else if (playerVerticalInput > 0 && playerHorizontalInput > 0) // player moving diagonal to top right 
        {
            transform.position = transform.position + new Vector3(0.5f, 0.5f, 0f) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            col.gameObject.SendMessage("ApplyDamage", attackPoints);
            print("fuck him");
        }
        
    }

    void GetInputs(float[] inputs)
    {
        playerVerticalInput = inputs[0];
        playerHorizontalInput = inputs[1];

       // print("vert: " + parentVerticalInput + ", hori: " + parentHorizontalInput);
    }
}
