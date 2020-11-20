using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Marie Lencer
 * Date: 19.11.2020
 * 
 * Version: 2.0
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
                    collider.gameObject.SendMessage("AddArmor", addedPoints);
                    break;
                case "Damage":
                    Debug.Log("damage item");
                    collider.gameObject.SendMessage("AddAttackPoints", addedPoints);
                    break;
                case "Heal":
                    Debug.Log("heal item");
                    collider.gameObject.SendMessage("AddHeal", addedPoints);
                    break;
                case "Health":
                    Debug.Log("health item");
                    collider.gameObject.SendMessage("AddMaxHealth", addedPoints);
                    break;
                case "PermaDamage":
                    Debug.Log("permaDamage item");
                    collider.gameObject.SendMessage("AddPermaAttackPoints", addedPoints);
                    break;
                case "PermaHealth":
                    Debug.Log("permaHealth item");
                    collider.gameObject.SendMessage("AddPermaHealth", addedPoints);
                    break;
                case "Speed":
                    Debug.Log("speed item");
                    collider.gameObject.SendMessage("AddSpeed", addedPoints);
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
