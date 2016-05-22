using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopButtons : MonoBehaviour {

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
	private Slider[] sliders;

	private GameObject hull;
	private GameObject thruster;
	private Slider healthSlider;
	private GameObject[] canons = new GameObject[6];

	public AudioSource source;

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

		sliders =repairMenu.GetComponentsInChildren<Slider> ();
		sliders [0].maxValue = 100 + (50 * (GameControl.control.hullUpgrade-1));
		sliders[1].maxValue = 100 + (50 * (GameControl.control.hullUpgrade-1));
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

	void Update()
	{
		if (GameObject.Find ("Slider_refill"))
		{
			if (GameControl.control.health + GameControl.control.money < (int)GameObject.Find ("Slider_refill").GetComponent<Slider> ().value)
			{
				GameObject.Find ("Slider_refill").GetComponent<Slider> ().value = GameControl.control.health + GameControl.control.money;
			}
		}
	}

	public void closeRepair () 
	{
		repairMenu.SetActive(false);
	}

	public void openRepair()
	{
		repairMenu.SetActive(true);
		if (GameObject.Find ("TutorialControl") != null && GameObject.Find ("TutorialControl").GetComponent<Tutorial>().dialogNumber == 34)
		{
			GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().nextDialog ();
		}
	}

	public void buyHealth()
	{
		if (GameObject.Find ("TutorialControl") == null)
		{
			if (GameControl.control.health < (int)GameObject.Find ("Slider_refill").GetComponent<Slider> ().value && (int)GameObject.Find ("value_cost_hp").GetComponent<UpdatePayment> ().payment <= GameControl.control.money)
			{
				
				GameControl.control.health = (int)GameObject.Find ("Slider_refill").GetComponent<Slider> ().value;
				GameObject.Find ("Slider_current_hp").GetComponent<Slider> ().value = GameControl.control.health;
				GameControl.control.money -= (int)GameObject.Find ("value_cost_hp").GetComponent<UpdatePayment> ().payment;
				GameObject.Find ("value_scraps").GetComponent<Text> ().text = GameControl.control.money.ToString ();
			}
		} 
		else if (GameControl.control.health != 100)
		{
			if (GameControl.control.health <= (int)GameObject.Find ("Slider_refill").GetComponent<Slider> ().value)
			{
				source.Play ();
				GameControl.control.health = (int)GameObject.Find ("Slider_refill").GetComponent<Slider> ().value;
				GameObject.Find ("Slider_current_hp").GetComponent<Slider> ().value = GameControl.control.health;

				if (GameControl.control.health == 100)
				{
					GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().nextDialog ();
				}
			}
		} 
		else
		{
			source.Play ();
			GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().nextDialog ();
		}
	}

	public void buyUpgrade()
	{
		int maxUpgrade = 3;
		string upgradeName = this.gameObject.GetComponent<ShopText>().currentUpgradeName;
		if (this.gameObject.GetComponent<ShopText>().currentPrice <= GameControl.control.money)
		{
			Debug.Log(upgradeName);
			switch (upgradeName)
			{
			case "hull":
				if (GameControl.control.hullUpgrade < maxUpgrade)
				{
					GameControl.control.hullUpgrade ++;
					changeIcon(upgradeName, GameObject.Find(upgradeName), GameControl.control.hullUpgrade);
					sliders [0].maxValue = 100 + (50 * (GameControl.control.hullUpgrade-1));
					sliders[1].maxValue = 100 + (50 * (GameControl.control.hullUpgrade-1));
					source.Play();
				}
				break;

			case "special":
				GameControl.control.specialAmmo ++;
				source.Play();
				break;

			case "cannonT1":
				if (GameControl.control.canonUpgrades[0] < maxUpgrade)
				{
					GameControl.control.canonUpgrades[0] ++;
					changeIcon(upgradeName, GameObject.Find(upgradeName), GameControl.control.canonUpgrades[0]);
					source.Play();
				}
				break;

			case "cannonT2":
				if (GameControl.control.canonUpgrades[1] < maxUpgrade)
				{
					GameControl.control.canonUpgrades[1] ++;
					changeIcon(upgradeName, GameObject.Find(upgradeName), GameControl.control.canonUpgrades[1]);
					source.Play();
				}
				break;

			case "cannonT3":
				if (GameControl.control.canonUpgrades[2] < maxUpgrade)
				{
					GameControl.control.canonUpgrades[2] ++;
					changeIcon(upgradeName, GameObject.Find(upgradeName), GameControl.control.canonUpgrades[2]);
					source.Play();
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
					source.Play();
				}
				break;

			case "cannonB3":
				if (GameControl.control.canonUpgrades[5] < maxUpgrade)
				{
					GameControl.control.canonUpgrades[5] ++;
					changeIcon(upgradeName, GameObject.Find(upgradeName), GameControl.control.canonUpgrades[5]);
					source.Play();
				}
				break;

			case "thruster":
				if (GameControl.control.thrusterUpgrade < maxUpgrade)
				{
					GameControl.control.thrusterUpgrade ++;
					changeIcon(upgradeName, GameObject.Find(upgradeName), GameControl.control.thrusterUpgrade);
					source.Play();
				}
				break;
			default:

				break;
			}

			if (this.gameObject.GetComponent<ShopText>().noMoreUpgrade == false)
			{
				if (GameObject.Find ("TutorialControl") == null)
				{
					GameControl.control.money -= this.gameObject.GetComponent<ShopText> ().currentPrice;
					GameObject.Find ("value_scraps").GetComponent<Text> ().text = GameControl.control.money.ToString ();
				} 
				else
				{
					if (GameControl.control.specialAmmo > 20)
					{
						GameControl.control.money -= this.gameObject.GetComponent<ShopText> ().currentPrice;
						GameObject.Find ("value_scraps").GetComponent<Text> ().text = GameControl.control.money.ToString ();
					} else if (GameControl.control.specialAmmo == 20)
					{
						GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().nextDialog ();
					}
				}
				this.gameObject.GetComponent<ShopText>().updateText(this.gameObject.GetComponent<ShopText>().currentUpgradeName);
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
