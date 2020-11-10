using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Text stats;

    private float playerSpeed;

    private int attackDmg;

    // Update is called once per frame
    void SetText()
    {
        stats.text = ("Dmg: " + attackDmg + "\nSpeed: " + playerSpeed );
    }

    public void SetDmg(int dmg)
    {
        attackDmg = dmg;
        SetText();
    }

    public void SetSpeed(float speed)
    {
        playerSpeed = speed;
        SetText();
    }
}
