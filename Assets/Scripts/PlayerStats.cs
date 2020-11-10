using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Text stats;

    private float playerSpeed;

    private int attackDmg;

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
