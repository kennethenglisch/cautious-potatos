using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Author: Marie Lencer
 * Date: 25.10.2020
 * 
 * Modified by: Kenneth Englisch
 * Date: 26.10.2020
 * 
 * Version: 1.1
 */

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] Animator animator = null;
    [Range(0, 100)][SerializeField] int healthPoints = 100;
    [SerializeField] int attackPoints = 10;

    [SerializeField] private PolygonCollider2D[] runColliders;

    private int currentColliderIndex = 0;

    private float horizontalInput;
    private float verticalInput;
    private bool deadScenePlayed = false;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (!IsDead())
        {
            ProcessMovement();
            ProcessAttack();
        }
      
        if(IsDead() && !deadScenePlayed)
        {
            ProcessDead();
        }
    }

    private void ProcessMovement()
    {
        if (!IsAttacking())
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }


        if (IsIdle())
        {
            animator.Play("hero-idle");
        }

        if (horizontalInput > 0 && !IsAttacking())
        {
            // set player facing to the right
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            animator.Play("hero-run");
        }
        if (horizontalInput < 0 && !IsAttacking())
        {
            // set player facing to the left
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            // inverting the input, otherwise the player would walk backwards
            horizontalInput = -horizontalInput;

            animator.Play("hero-run");
        }
        if (verticalInput != 0 && !IsAttacking())
        {
            animator.Play("hero-run");
        }

         Vector3 movement = new Vector3(horizontalInput, verticalInput, 0.0f);

         if(!IsAttacking())
            transform.Translate(movement * speed * Time.deltaTime);
    }
    
    private void ProcessAttack()
    {
        if(Input.GetButton("Fire1"))
        {
            animator.Play("hero-attack1");
        }
        else if(Input.GetButton("Fire2"))
        {
            animator.Play("hero-attack2");
        }
    }

    private bool IsDead()
    {
        return healthPoints <= 0;
    }

    private void ProcessDead()
    {
        // play deathscreen
        animator.Play("hero-death");
        Invoke("LoadDeathScreen", 1.5f);
        
    }
    private void LoadDeathScreen()
    {
        SceneManager.LoadScene(2);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            if (IsAttacking()) { 
                col.gameObject.SendMessage("ApplyDamage", attackPoints);
                print("fucke you");
            }
        }
    }

    

    public void ApplyDamage(int damage)
    {
        healthPoints -= damage;
    }

    private bool IsIdle()
    {
        if (horizontalInput == 0 && verticalInput == 0
                                 && !IsPlaying("hero-attack1") && !IsPlaying("hero-attack2") 
                                 && !IsPlaying("hero-fall")    && !IsPlaying("hero-jump")    
                                 && !IsPlaying("hero-death")   && !IsPlaying("hero-hit")) 
        {
            return true;
        }
        else
            return false;
    }

    private bool IsAttacking()
    {
        return IsPlaying("hero-attack1") || IsPlaying("hero-attack2");
    }

    private bool IsPlaying(string stateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    public void SetRunColliderForSprite(int spriteNum)
    {
        runColliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteNum;
        runColliders[currentColliderIndex].enabled = true;
    }
}
