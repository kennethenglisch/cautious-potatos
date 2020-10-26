using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

/* Author: Bartholomäus Berresheim
 * Version: 1.0
 * Date: 26.10.2020
 */

public class Enemy : MonoBehaviour
{
    //[SerializeField] int attackDamage = 10;
    [SerializeField] int lifePoints;
    [SerializeField] float speed;

    float attackRadius;
    float followRadius;

    SpriteRenderer enemySR;
    Rigidbody2D rb;
    
    Animator enemyAnim;

    UnityEngine.Vector3 Player;
    UnityEngine.Vector2 Playerdirection;

    float Xdif;
    float Ydif;

    void Start()
    {
        enemyAnim = gameObject.GetComponent<Animator>();
        enemySR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        speed = 1f;
        attackRadius = 0.6f;
        followRadius = 8f;
        lifePoints = 20;

        enemyAnim.Play("Enemy1Idle");
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.Find("Player").transform.position;

        Xdif = Player.x - transform.position.x;
        Ydif = Player.y - transform.position.y;

        Playerdirection = new UnityEngine.Vector2(Xdif, Ydif);

        if (lifePoints < 0)
        {
            enemyAnim.Play("Enemy1Death");
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

                enemyAnim.Play("Enemy1AttackA");

                rb.velocity = UnityEngine.Vector3.zero;
                rb.angularVelocity = 0;
            }
            else
            {


                rb.AddForce(Playerdirection.normalized * speed);
                rb.velocity = rb.velocity.normalized * speed;


                //for attack animation
                enemyAnim.Play("Enemy1Walk");
                print(Player.x + " --------------- " + transform.position.x);

                
            }
        }
        else
        {
            enemyAnim.Play("Enemy1Idle");
            rb.velocity = UnityEngine.Vector3.zero;
            rb.angularVelocity = 0;
        }
        //Destroy(GetComponent<PolygonCollider2D>());
        //gameObject.AddComponent<PolygonCollider2D>();
    }

    public bool checkRadius(float radius)
    {
        return (Mathf.Sqrt(Mathf.Pow(Player.x - transform.position.x, 2) + Mathf.Pow(Player.y - transform.position.y, 2)) < radius);
    }
}
