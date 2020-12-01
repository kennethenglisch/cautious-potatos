using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

/* Author: Bartholomäus Berresheim
 * Date: 26.10.2020
 * 
 * Modified by: Marie Lencer
 * Date: 20.11.2020
 * 
 * Version: 2.0
 */

public class Enemy : MonoBehaviour
{
    [SerializeField] int attackDamage = 10;
    [SerializeField] int lifePoints = 20;
    
    [SerializeField] float speed = 1f;
    
    [SerializeField] EnemyHealthBar healthBar;

    public GameObject[] loot;

    float attackRadius = 0.6f;
    float followRadius = 8f;

    SpriteRenderer enemySR;
    Rigidbody2D rb;

    float TimerForNextAttack, Cooldown;
    Animator enemyAnim;

    UnityEngine.Vector3 Player;
    UnityEngine.Vector2 Playerdirection;

    float Xdif;
    float Ydif;
    private bool hideHealthBar = false;

    private bool droppedItem = false;

    void Start()
    {
        enemyAnim = gameObject.GetComponent<Animator>();
        enemySR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        enemyAnim.Play("Enemy1Idle");

        Cooldown = 1;
        TimerForNextAttack = Cooldown;
        
        healthBar.SetMaxHealth(lifePoints);
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform.position;

        Xdif = Player.x - transform.position.x;
        Ydif = Player.y - transform.position.y;

        Playerdirection = new UnityEngine.Vector2(Xdif, Ydif);

        if (lifePoints <= 0)
        {
            rb.velocity = UnityEngine.Vector3.zero;
            rb.angularVelocity = 0;
            enemyAnim.Play("Enemy1Death");
            if (!droppedItem)
            {
                DropItem();
                droppedItem = true;
            }

            if(!hideHealthBar){
            healthBar.Dead();
            hideHealthBar = true;
            }
        }
        else if (checkRadius(followRadius))
        {

            if (Player.x < transform.position.x)
            {
                transform.eulerAngles = new UnityEngine.Vector3(0, 180, 0);
                attackRadius = 0.4f;
            }
            else
            {
                transform.eulerAngles = new UnityEngine.Vector3(0, 0, 0);
                attackRadius = 0.8f;
            }

            if (checkRadius(attackRadius))
            {
                if (TimerForNextAttack > 0)
                {
                    TimerForNextAttack -= Time.deltaTime;
                }
                else { 
                enemyAnim.Play("Enemy1AttackA");
                }
                rb.velocity = UnityEngine.Vector3.zero;
                rb.angularVelocity = 0;
            }
            else
            {
                rb.AddForce(Playerdirection.normalized * speed);
                rb.velocity = rb.velocity.normalized * speed;

                //for attack animation
                enemyAnim.Play("Enemy1Walk");
             //   print(Player.x + " --------------- " + transform.position.x);

                
            }
        }
        else
        {
            enemyAnim.Play("Enemy1Idle");
            rb.velocity = UnityEngine.Vector3.zero;
            rb.angularVelocity = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.tag == "Player") {
            print(col);
            if (IsAttacking()) { 
                col.gameObject.SendMessage("ApplyDamage", attackDamage);
                print("fuck me");

                TimerForNextAttack = Cooldown;
            }
        }
    }

    public void ApplyDamage(int damage)
    {
        lifePoints -= damage;
        healthBar.SetHealth(lifePoints);
        enemyAnim.Play("Enemy1Hit");
    }

    public bool checkRadius(float radius)
    {
        return (Mathf.Sqrt(Mathf.Pow(Player.x - transform.position.x, 2) + Mathf.Pow(Player.y - transform.position.y, 2)) < radius);
    }

    private bool IsAttacking()
    {
        return IsPlaying("Enemy1AttackA");
    }

    private bool IsPlaying(string stateName)
    {
        if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
            enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    private void DropItem()
    {
        int random = Random.Range(0, 100);
        UnityEngine.Vector3 pos = transform.position;
        if (random > 94)
        {
            Debug.Log("Dropping PermaHealth");
            Instantiate(loot[4], pos, UnityEngine.Quaternion.identity);
        }
        else if (random <= 94 && random > 89)
        {
            Debug.Log("Dropping PermaDamage");
            Instantiate(loot[5], pos, UnityEngine.Quaternion.identity);
        }
        else if (random <= 89 && random > 78)
        {
            Debug.Log("Dropping Armor");
            Instantiate(loot[0], pos, UnityEngine.Quaternion.identity);
        }
        else if (random <= 78 && random > 66)
        {
            Debug.Log("Dropping Damage");
            Instantiate(loot[1], pos, UnityEngine.Quaternion.identity);
        }
        else if (random <= 66 && random > 54)
        {
            Debug.Log("Dropping Speed");
            Instantiate(loot[6], pos, UnityEngine.Quaternion.identity);
        }
        else if (random <= 54 && random > 39)
        {
            Debug.Log("Dropping Heal");
            Instantiate(loot[2], pos, UnityEngine.Quaternion.identity);
        }
        else if (random <= 39 && random > 24)
        {
            Debug.Log("Dropping Health");
            Instantiate(loot[3], pos, UnityEngine.Quaternion.identity);
        }
        else
        {
            Debug.Log("Nothing to drop");
        }
    }
}
