using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shopButtons : MonoBehaviour {

	public GameObject repairMenu;

	public void closeRepair () 
	{
		repairMenu.SetActive(false);
	}

	public void openRepair()
	{
		repairMenu.SetActive(true);
	}

	public void buyHealth()
	{
		if (GameControl.control.health < (int)GameObject.Find("Slider_refill").GetComponent<Slider>().value && (int)GameObject.Find("value_cost_hp").GetComponent<updatePayment>().payment <= GameControl.control.money)
		{
			GameControl.control.health = (int)GameObject.Find("Slider_refill").GetComponent<Slider>().value;
			GameObject.Find("Slider_current_hp").GetComponent<Slider>().value = GameControl.control.health;
			GameControl.control.money -= (int)GameObject.Find("value_cost_hp").GetComponent<updatePayment>().payment;
			GameObject.Find("value_scraps").GetComponent<Text>().text = GameControl.control.money.ToString();
		}
//		else if ((int)GameObject.Find("Slider_refill").GetComponent<Slider>().value < GameControl.control.money)
//		{
//			
//		}
	}
}
