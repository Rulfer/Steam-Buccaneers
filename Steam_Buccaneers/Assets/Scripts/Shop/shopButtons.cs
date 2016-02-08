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
	}

	public void buyUpgrade()
	{
		int maxUpgrade = 3;
		if (this.gameObject.GetComponent<shopText>().currentPrice <= GameControl.control.money)
		{

			switch (this.gameObject.GetComponent<shopText>().currentUpgradeName)
			{
			case "hull":
				if (GameControl.control.hullUpgrade < maxUpgrade)
				GameControl.control.hullUpgrade ++;
				break;

			case "special":
				GameControl.control.specialAmmo ++;
				break;

			case "cannonT1":
				if (GameControl.control.canonUpgrades[0] < maxUpgrade)
				GameControl.control.canonUpgrades[0] ++;
				break;


			case "cannonT2":
				if (GameControl.control.canonUpgrades[1] < maxUpgrade)
				GameControl.control.canonUpgrades[1] ++;
				break;

			case "cannonT3":
				if (GameControl.control.canonUpgrades[2] < maxUpgrade)
				GameControl.control.canonUpgrades[2] ++;
				break;

			case "cannonB1":
				if (GameControl.control.canonUpgrades[3] < maxUpgrade)
				GameControl.control.canonUpgrades[3] ++;
				break;

			case "cannonB2":
				if (GameControl.control.canonUpgrades[4] < maxUpgrade)
				GameControl.control.canonUpgrades[4] ++;
				break;

			case "cannonB3":
				if (GameControl.control.canonUpgrades[5] < maxUpgrade)
				GameControl.control.canonUpgrades[5] ++;
				break;

			case "thruster":
				if (GameControl.control.thrusterUpgrade < maxUpgrade)
					GameControl.control.thrusterUpgrade ++;
				break;
			default:

				break;
			}

			if (this.gameObject.GetComponent<shopText>().noMoreUpgrade == false)
			{
				GameControl.control.money -= this.gameObject.GetComponent<shopText>().currentPrice;
				GameObject.Find("value_scraps").GetComponent<Text>().text = GameControl.control.money.ToString();
				this.gameObject.GetComponent<shopText>().updateText(this.gameObject.GetComponent<shopText>().currentUpgradeName);
				Debug.Log("This Works TM");
			}
		}
	}
}
