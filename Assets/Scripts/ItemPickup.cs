using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Marie Lencer
 * Date: 08.11.2020
 * 
 * Version: 1.0
 */

public class ItemPickup : MonoBehaviour
{
    [SerializeField] int addedPoints = 10;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            Debug.Log("item pick up");
            switch(gameObject.tag)
            {
                case "Armor":
                    Debug.Log("armor item");
                    break;
                case "Damage":
                    Debug.Log("damage item");
                    break;
                case "Heal":
                    Debug.Log("heal item");
                    break;
                case "Health":
                    Debug.Log("health item");
                    break;
                case "PermaDamage":
                    Debug.Log("permaDamage item");
                    break;
                case "PermaHealth":
                    Debug.Log("permaHealth item");
                    break;
                case "Speed":
                    Debug.Log("speed item");
                    break;
                default:
                    Debug.Log("something is wrong");
                    break;
            }
            //collider.gameObject.SendMessage("AddAttackPoints", addedAttackPoints);
            Destroy(gameObject);
        }
    }
}
