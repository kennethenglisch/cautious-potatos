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
    [SerializeField] int addedAttackPoints = 10;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            Debug.Log("item pick up");
            collider.gameObject.SendMessage("AddAttackPoints", addedAttackPoints);
            Destroy(gameObject);
        }
    }
}
