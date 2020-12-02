using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Author: Marie Lencer
 * Date: 25.10.2020
 * 
 * Modified by: Kenneth Englisch
 * Date: 26.10.2020
 * 
 * Modified by: Kenneth Englisch
 * Date: 02.11.2020
 * 
 * Version: 1.2
 */

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float speed = 2f;

    [Header("Health and Attack")]
    [Range(0, 100)] [SerializeField] int maxHealthPoints = 100;
    [Range(0, 100)] [SerializeField] int currentHealthPoints = 100;
    [SerializeField] int permaHealthPoints = 0;
    
    [Range(0, 100)] [SerializeField] int maxArmorPoints = 100;
    [Range(0, 100)] [SerializeField] int currentArmorPoints = 0;
    
    [Range(1, 50)] [SerializeField] int attackPoints = 10;
    [SerializeField] int permaAttackPoints = 0;
    [Tooltip("In Seconds")] [Range(1.0f, 10f)] [SerializeField] float fireRate = 1.5f; // The interval the player is able to fire

    [Header("Loading Resources")]
    [SerializeField] Animator animator = null;
    [SerializeField] SpriteRenderer spriteRenderer = null;
    [SerializeField] GameObject rangedAttackPrefab = null;

    [SerializeField] PlayerHealthBar healthBar;
    [SerializeField] PlayerArmorBar armorBar;
    [SerializeField] PlayerStats stats;

    private float horizontalInput;
    private float verticalInput;

    private bool deadScenePlayed = false;

    private float nextFire = 0.0f;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

       // rangedAttackPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/hero-projectile-rounded.prefab", typeof(GameObject));

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        healthBar.SetMaxHealth(maxHealthPoints + permaHealthPoints);
        healthBar.SetHealth(currentHealthPoints + permaHealthPoints);
        
        armorBar.SetMaxArmor(maxArmorPoints);
        armorBar.SetArmor(currentArmorPoints);
        
        stats.SetDmg(attackPoints + permaAttackPoints);
        stats.SetSpeed(speed);
    }

    void Update()
    { 
        if (!IsDead())
        {
            ProcessMovement();
            ProcessAttack();
        }

        if (IsDead() && !deadScenePlayed)
        {
            ProcessDead();
            deadScenePlayed = true;
        }
    }

    private void ProcessMovement()
    {
        if (!IsAttacking() && !GotHit())
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }


        if (IsIdle())
        {
            animator.Play("hero-idle");
        }

        if (horizontalInput > 0 && !IsAttacking() && !GotHit())
        {
            // set player facing to the right
            //transform.localRotation = Quaternion.Euler(0, 0, 0);
            spriteRenderer.flipX = false;
            animator.Play("hero-run");
        }
        if (horizontalInput < 0 && !IsAttacking() && !GotHit())
        {
            // set player facing to the left
            //transform.localRotation = Quaternion.Euler(0, 180, 0);
            // inverting the input, otherwise the player would walk backwards
            //horizontalInput = -horizontalInput;
            spriteRenderer.flipX = true;
            animator.Play("hero-run");
        }
        if (verticalInput != 0 && !IsAttacking() && !GotHit())
        {
            animator.Play("hero-run");
        }

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0.0f);

        if (!IsAttacking() && !GotHit())
            transform.Translate(movement * speed * Time.deltaTime);
    }

    private void ProcessAttack()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            animator.Play("hero-attack1");
            GetComponent<AudioSource>().Play();
        }
        else if (Input.GetButton("Fire2") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            animator.Play("hero-attack2");

            if (rangedAttackPrefab != null){
                ProcessRangedAttack();
                GetComponent<AudioSource>().Play();
                
            }
        }
    }

    private void ProcessRangedAttack()
    {
        float vertical = verticalInput;
        float horizontal = horizontalInput;

        float[] inputs = new float[2] { vertical, horizontal };

        GameObject clone;

        clone = Instantiate(rangedAttackPrefab, gameObject.transform.parent, false);


        clone.SendMessage("GetInputs", inputs);
    }

    private bool IsDead()
    {
        return currentHealthPoints <= 0;
    }

    private void ProcessDead()
    {
        // play deathscreen
        animator.Play("hero-death");
        Invoke("LoadDeathScreen", 1.5f);

    }

    private void LoadDeathScreen()
    {
        Debug.Log("Loading death screen");
        SceneManager.LoadScene(6);
    }

    private void OnTriggerEnter2D(Collider2D col)
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
        currentHealthPoints -= damage;
        healthBar.SetHealth(currentHealthPoints);
        animator.Play("hero-hit");
    }

    private bool GotHit()
    {
        return IsPlaying("hero-hit");
    }

    private bool IsIdle()
    {
        if (horizontalInput == 0 && verticalInput == 0
                                 && !IsPlaying("hero-attack1") && !IsPlaying("hero-attack2")
                                 && !IsPlaying("hero-fall") && !IsPlaying("hero-jump")
                                 && !IsPlaying("hero-death") && !IsPlaying("hero-hit"))
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

    public void AddAttackPoints(int addedPoints)
    {
        attackPoints += addedPoints;
        stats.SetDmg(attackPoints);
        Debug.Log("adding " + addedPoints + " attack points");
    }

    public void AddArmor(int addedPoints)
    {
        if (currentArmorPoints + addedPoints < maxArmorPoints)
        {
            currentArmorPoints += addedPoints;
        } else
        {
            currentArmorPoints = maxArmorPoints;
        }
        
        armorBar.SetArmor(currentArmorPoints);
        Debug.Log("adding " + addedPoints + " armor");
    }

    public void AddHeal(int addedPoints)
    {
        if(currentHealthPoints + addedPoints < maxHealthPoints)
        {
            currentHealthPoints += addedPoints;
        } else
        {
            currentHealthPoints = maxHealthPoints;
        }
        
        healthBar.SetHealth(currentHealthPoints);
        Debug.Log("adding " + addedPoints + " health points");
    }

    public void AddMaxHealth(int addedPoints)
    {
        maxHealthPoints += addedPoints;
        healthBar.SetMaxHealth(maxHealthPoints);
        Debug.Log("adding " + addedPoints + " max health points");
    }

    public void AddPermaHealth(int addedPoints)
    {
        permaHealthPoints += addedPoints;
        maxHealthPoints += addedPoints;
        healthBar.SetMaxHealth(maxHealthPoints);
        Debug.Log("adding " + addedPoints + " perma health points");
    }

    public void AddPermaAttackPoints(int addedPoints)
    {
        permaAttackPoints += addedPoints;
        attackPoints += addedPoints;
        stats.SetDmg(attackPoints);
        Debug.Log("adding " + addedPoints + " perma attack points");
    }

    public void AddSpeed(float addedPoints)
    {
        speed += addedPoints;
        stats.SetSpeed(speed);
        Debug.Log("adding " + addedPoints + " speed");
    }

    public void resetStats()
    {
        maxHealthPoints = 100 + permaHealthPoints;
        currentHealthPoints = maxHealthPoints;
        speed = 2f;
        attackPoints = 10 + permaAttackPoints;
        currentArmorPoints = 0;
        deadScenePlayed = false;
        healthBar.SetHealth(currentHealthPoints);
        healthBar.SetMaxHealth(maxHealthPoints);
        stats.SetSpeed(speed);
        stats.SetDmg(attackPoints);
        armorBar.SetArmor(currentArmorPoints);
        armorBar.SetMaxArmor(maxArmorPoints);

        Debug.Log("reset stats");
    }
}
