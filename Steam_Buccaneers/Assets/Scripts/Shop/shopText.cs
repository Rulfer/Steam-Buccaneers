using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shopText : MonoBehaviour {

	private GameObject header;
	private GameObject infoText;
	private GameObject cost;

	private string[] hullTxt = new string[2];
	private string [] thrusterTxt = new string[2];
	private string [] canonTxt = new string[2];
	private string specialWeapon;

	private int canonNumber;
	public int[] hullCost = new int[2];
	public int [] thrusterCost = new int[2];
	public int [] canonCost = new int[2];
	public int specialAmmoCost;

	private int maxUpgrade;

	public int currentPrice;
	public string currentUpgradeName;
	public bool noMoreUpgrade;

	// Use this for initialization
	void Start () 
	{
		header = GameObject.Find("header");
		infoText = GameObject.Find("description");
		cost = GameObject.Find("value_cost");
		Debug.Log("Start text");

		//Setting tekst
		hullTxt[0] = "Hull level 2 info here.";
		hullTxt[1] = "Hull level 3 info here.";

		thrusterTxt[0] = "Thruster level 2 info here.";
		thrusterTxt[1] = "Thruster level 3 info here.";

		specialWeapon = "Specialweapon info here.";

		canonTxt[0] = "Canon lvl 2 info here.";
		canonTxt[1] = "Canon lvl 3 info here.";

		maxUpgrade = 3;
		noMoreUpgrade = false;
	}


	public void updateText(string buttonName)
	{
		noMoreUpgrade = false;
		Debug.Log("Change text");
		currentUpgradeName = buttonName;
		switch (currentUpgradeName)
		{
		case "hull":
			if (GameControl.control.hullUpgrade < maxUpgrade)
			{
			header.GetComponent<Text>().text = "Upgrade to hull level " + (GameControl.control.hullUpgrade+1);
			infoText.GetComponent<Text>().text = hullTxt[GameControl.control.hullUpgrade-1];
			cost.GetComponent<Text>().text = hullCost[GameControl.control.hullUpgrade-1]+",-";
			currentPrice = hullCost[GameControl.control.hullUpgrade-1];
			}
			else
			{
				noMoreUpgrade = true;
			}
			break;

		case "special":
			header.GetComponent<Text>().text = "Special ammo: " + GameControl.control.specialAmmo;
			infoText.GetComponent<Text>().text = specialWeapon;
			cost.GetComponent<Text>().text = specialAmmoCost+",-";
			currentPrice = specialAmmoCost;
			break;

		
		case "cannonT1":
			canonNumber = 0;
			if (GameControl.control.canonUpgrades[canonNumber] < maxUpgrade)
			{
			header.GetComponent<Text>().text = "Upgrade right top cannon to level " + (GameControl.control.canonUpgrades[canonNumber]+1);
			infoText.GetComponent<Text>().text = canonTxt[GameControl.control.canonUpgrades[canonNumber]-1];
			cost.GetComponent<Text>().text = canonCost[GameControl.control.canonUpgrades[canonNumber]-1]+",-";
			currentPrice = canonCost[GameControl.control.canonUpgrades[canonNumber]-1];
			}
			else
			{
				noMoreUpgrade = true;
			}
			break;


		case "cannonT2":
			canonNumber = 1;
			if (GameControl.control.canonUpgrades[canonNumber] < maxUpgrade)
			{
			header.GetComponent<Text>().text = "Upgrade right middle cannon to level " + (GameControl.control.canonUpgrades[canonNumber]+1);
			infoText.GetComponent<Text>().text = canonTxt[GameControl.control.canonUpgrades[canonNumber]-1];
			cost.GetComponent<Text>().text = canonCost[GameControl.control.canonUpgrades[canonNumber]-1]+",-";
			currentPrice = canonCost[GameControl.control.canonUpgrades[canonNumber]-1];
			}
			else
			{
				noMoreUpgrade = true;
			}
			break;

		case "cannonT3":
			canonNumber = 2;
			if (GameControl.control.canonUpgrades[canonNumber] < maxUpgrade)
			{
			header.GetComponent<Text>().text = "Upgrade right bottom cannon to level " + (GameControl.control.canonUpgrades[canonNumber]+1);
			infoText.GetComponent<Text>().text = canonTxt[GameControl.control.canonUpgrades[canonNumber]-1];
			cost.GetComponent<Text>().text = canonCost[GameControl.control.canonUpgrades[canonNumber]-1]+",-";
			currentPrice = canonCost[GameControl.control.canonUpgrades[canonNumber]-1];
			}
			else
			{
				noMoreUpgrade = true;
			}
			break;

		case "cannonB1":
			canonNumber = 3;
			if (GameControl.control.canonUpgrades[canonNumber] < maxUpgrade)
			{
			header.GetComponent<Text>().text = "Upgrade left top cannon to level " + (GameControl.control.canonUpgrades[canonNumber]+1);
			infoText.GetComponent<Text>().text = canonTxt[GameControl.control.canonUpgrades[canonNumber]-1];
			cost.GetComponent<Text>().text = canonCost[GameControl.control.canonUpgrades[canonNumber]-1]+",-";
			currentPrice = canonCost[GameControl.control.canonUpgrades[canonNumber]-1];
			}
			else
			{
				noMoreUpgrade = true;
			}
			break;

		case "cannonB2":
			canonNumber = 4;
			if (GameControl.control.canonUpgrades[canonNumber] < maxUpgrade)
			{
			header.GetComponent<Text>().text = "Upgrade left middle cannon to level " + (GameControl.control.canonUpgrades[canonNumber]+1);
			infoText.GetComponent<Text>().text = canonTxt[GameControl.control.canonUpgrades[canonNumber]-1];
			cost.GetComponent<Text>().text = canonCost[GameControl.control.canonUpgrades[canonNumber]-1]+",-";
			currentPrice = canonCost[GameControl.control.canonUpgrades[canonNumber]-1];
			}
			else
			{
				noMoreUpgrade = true;
			}
			break;
		case "cannonB3":
			canonNumber = 5;
			if (GameControl.control.canonUpgrades[canonNumber] < maxUpgrade)
			{
			header.GetComponent<Text>().text = "Upgrade left bottom cannon to level " + (GameControl.control.canonUpgrades[canonNumber]+1);
			infoText.GetComponent<Text>().text = canonTxt[GameControl.control.canonUpgrades[canonNumber]-1];
			cost.GetComponent<Text>().text = canonCost[GameControl.control.canonUpgrades[canonNumber]-1]+",-";
			currentPrice = canonCost[GameControl.control.canonUpgrades[canonNumber]-1];
			}
			else
			{
				noMoreUpgrade = true;
			}
			break;

		case "thruster":
			if (GameControl.control.thrusterUpgrade < maxUpgrade)
			{
			header.GetComponent<Text>().text = "Upgrade to thruster level " + (GameControl.control.thrusterUpgrade+1);
			infoText.GetComponent<Text>().text = thrusterTxt[GameControl.control.thrusterUpgrade-1];
			cost.GetComponent<Text>().text = thrusterCost[GameControl.control.thrusterUpgrade-1]+",-";
			currentPrice = thrusterCost[GameControl.control.thrusterUpgrade-1];
			}
			else
			{
				noMoreUpgrade = true;
			}
			break;

		default:
			
			break;
		}
	}
		
}
