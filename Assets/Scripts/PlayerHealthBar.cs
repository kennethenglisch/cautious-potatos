using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

	private Image healthbar;
	private float maxHealth;

	void Start()
	{
		healthbar = gameObject.GetComponent<Image>();
	}
	public void SetMaxHealth(int health)
	{
		maxHealth = health;
	}

    public void SetHealth(int health)
	{
		print("omg u didnt ");
		healthbar.fillAmount = health/maxHealth;
	}

}
