using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerArmorBar : MonoBehaviour
{

	[SerializeField] private Image armorBar;
	private float maxArmor;

	void Start()
	{
		armorBar = gameObject.GetComponent<Image>();
	}
	public void SetMaxArmor(int armor)
	{
		maxArmor = armor;
	}

    public void SetArmor(int armor)
	{
		print("omg u (armor) didnt ");
		armorBar.fillAmount = armor/maxArmor;
	}

}
