using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shopButtons : MonoBehaviour {

	public GameObject repairMenu;
	public Sprite hullLvl1;
	public Sprite hullLvl2;
	public Sprite hullLvl3;
	public Sprite canonLvl1;
	public Sprite canonLvl2;
	public Sprite canonLvl3;
	public Sprite thrusterLvl1;
	public Sprite thrusterLvl2;
	public Sprite thrusterLvl3;

	private GameObject hull;
	private GameObject thruster;
	private Slider healthSlider;
	private GameObject[] canons = new GameObject[6];

	void Start()
	{
		//Finding objects to change icons on
		hull = GameObject.Find("hull");
		thruster = GameObject.Find("thruster");
		canons[0] = GameObject.Find("cannonT1");
		canons[1] = GameObject.Find("cannonT2");
		canons[2] = GameObject.Find("cannonT3");
		canons[3] = GameObject.Find("cannonB1");
		canons[4] = GameObject.Find("cannonB2");
		canons[5] = GameObject.Find("cannonB3");

		//Set right icons
	if (GameControl.control.hullUpgrade == 1)
		{
			hull.GetComponent<Image>().sprite = hullLvl1;
		}
	else if (GameControl.control.hullUpgrade == 2)
		{
			hull.GetComponent<Image>().sprite = hullLvl2;
		}
		else
		{
			hull.GetComponent<Image>().sprite = hullLvl3;
		}

		for (int i = 0; i <6; i++)
		{
			if (GameControl.control.canonUpgrades[i] == 1)
				{
				canons[i].GetComponent<Image>().sprite = canonLvl1;
				}
			else if (GameControl.control.canonUpgrades[i] == 2)
				{
				canons[i].GetComponent<Image>().sprite = canonLvl2;
				}
			else
				{
				canons[i].GetComponent<Image>().sprite = canonLvl3;
				}
		}

		if (GameControl.control.thrusterUpgrade == 1)
		{
			thruster.GetComponent<Image>().sprite = thrusterLvl1;
		}
		else if (GameControl.control.thrusterUpgrade == 2)
		{
			thruster.GetComponent<Image>().sprite = thrusterLvl2;
		}
		else
		{
			thruster.GetComponent<Image>().sprite = thrusterLvl3;
		}
	}

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
		string upgradeName = this.gameObject.GetComponent<shopText>().currentUpgradeName;
		if (this.gameObject.GetComponent<shopText>().currentPrice <= GameControl.control.money)
		{
			Debug.Log(upgradeName);
			switch (upgradeName)
			{
			case "hull":
				if (GameControl.control.hullUpgrade < maxUpgrade)
				{
					GameControl.control.hullUpgrade ++;
					changeIcon(upgradeName, GameObject.Find(upgradeName), GameControl.control.hullUpgrade);
				}
				break;

			case "special":
				GameControl.control.specialAmmo ++;
				break;

			case "cannonT1":
				if (GameControl.control.canonUpgrades[0] < maxUpgrade)
				{
					GameControl.control.canonUpgrades[0] ++;
					changeIcon(upgradeName, GameObject.Find(upgradeName), GameControl.control.canonUpgrades[0]);
				}
				break;

			case "cannonT2":
				if (GameControl.control.canonUpgrades[1] < maxUpgrade)
				{
				GameControl.control.canonUpgrades[1] ++;
				changeIcon(upgradeName, GameObject.Find(upgradeName), GameControl.control.canonUpgrades[1]);
				}
				break;

			case "cannonT3":
				if (GameControl.control.canonUpgrades[2] < maxUpgrade)
				{
					GameControl.control.canonUpgrades[2] ++;
					changeIcon(upgradeName, GameObject.Find(upgradeName), GameControl.control.canonUpgrades[2]);
				}
				break;

			case "cannonB1":
				if (GameControl.control.canonUpgrades[3] < maxUpgrade)
				{
					GameControl.control.canonUpgrades[3] ++;
					changeIcon(upgradeName, GameObject.Find(upgradeName), GameControl.control.canonUpgrades[3]);
				}
				break;

			case "cannonB2":
				if (GameControl.control.canonUpgrades[4] < maxUpgrade)
				{
					GameControl.control.canonUpgrades[4] ++;
					changeIcon(upgradeName, GameObject.Find(upgradeName), GameControl.control.canonUpgrades[4]);
				}
				break;

			case "cannonB3":
				if (GameControl.control.canonUpgrades[5] < maxUpgrade)
				{
					GameControl.control.canonUpgrades[5] ++;
					changeIcon(upgradeName, GameObject.Find(upgradeName), GameControl.control.canonUpgrades[5]);
				}
				break;

			case "thruster":
				if (GameControl.control.thrusterUpgrade < maxUpgrade)
				{
					GameControl.control.thrusterUpgrade ++;
					changeIcon(upgradeName, GameObject.Find(upgradeName), GameControl.control.thrusterUpgrade);
				}
				break;
			default:

				break;
			}

			if (this.gameObject.GetComponent<shopText>().noMoreUpgrade == false)
			{
				if (GameObject.Find ("TutorialControl") == null)
				{
					GameControl.control.money -= this.gameObject.GetComponent<shopText> ().currentPrice;
					GameObject.Find ("value_scraps").GetComponent<Text> ().text = GameControl.control.money.ToString ();
				} 
				else
				{
					if (GameControl.control.specialAmmo > 20)
					{
						GameControl.control.money -= this.gameObject.GetComponent<shopText> ().currentPrice;
						GameObject.Find ("value_scraps").GetComponent<Text> ().text = GameControl.control.money.ToString ();
					} else if (GameControl.control.specialAmmo == 20)
					{
						GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().nextDialog ();
					}
				}
				this.gameObject.GetComponent<shopText>().updateText(this.gameObject.GetComponent<shopText>().currentUpgradeName);
				Debug.Log("This Works TM");
			}
		}


	}

	private void changeIcon(string iconName, GameObject iconObject, int upgradeLvl)
	{
		iconName = iconName.Remove(iconName.Length-2, 2);
		Debug.Log(iconName + ", " + upgradeLvl + ".");
		switch(iconName)
		{
		case "hull":
			if (upgradeLvl == 1)
			{
				iconObject.GetComponent<Image>().sprite = hullLvl1;
			}
			else if (upgradeLvl == 2)
			{
				iconObject.GetComponent<Image>().sprite = hullLvl2;
			}
			else
			{
				iconObject.GetComponent<Image>().sprite = hullLvl3;
			}
			break;
		case "cannon":
			if (upgradeLvl == 1)
			{
				iconObject.GetComponent<Image>().sprite = canonLvl1;
			}
			else if (upgradeLvl == 2)
			{
				iconObject.GetComponent<Image>().sprite = canonLvl2;
			}
			else
			{
				iconObject.GetComponent<Image>().sprite = canonLvl3;
			}
			break;
		case "thruster":
			if (upgradeLvl == 1)
			{
				iconObject.GetComponent<Image>().sprite = thrusterLvl1;
			}
			else if (upgradeLvl == 2)
			{
				iconObject.GetComponent<Image>().sprite = thrusterLvl2;
			}
			else
			{
				iconObject.GetComponent<Image>().sprite = thrusterLvl3;
			}
			break;
		}
			
	}
}
