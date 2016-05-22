using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopText : MonoBehaviour {

	private GameObject header;
	private GameObject infoText;
	private GameObject cost;

	private string[] hullTxt = new string[2];
	private string [] thrusterTxt = new string[2];
	private string [] canonTxt = new string[2];
	private string specialWeapon;

	private string[] hullName = new string[2];
	private string [] thrusterName = new string[2];
	private string [] canonName = new string[2];
	private string specialWeaponName;

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
		hullTxt[0] = "Keep your ship nice and scratch-free with this upgraded protection.";
		hullTxt[1] = "Military-grade protection for your already clunky ship!";

		thrusterTxt[0] = "Your upgraded thruster. Rocket trough space with this shiny new equipment!";
		thrusterTxt[1] = "Venture on with the harnessed energy of a dying star!";

		specialWeapon = "Ammo for the fancy cannon on your roof.";

		canonTxt[0] = "Upgraded cannon. This will do more damage.";
		canonTxt[1] = "Devastating destruction power! Crush your enemies!";

		hullName[0] = "Shield";
		hullName[1] = "Panser";

		thrusterName[0] = "Rocket";
		thrusterName[1] = "Supernova";

		specialWeaponName = "Basic special ammo";

		canonName[0] = "cannon mk. 2";
		canonName[1] = "cannon mk. 3";


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
				header.GetComponent<Text>().text = hullName[GameControl.control.hullUpgrade-1];
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
			header.GetComponent<Text>().text = specialWeaponName;
			infoText.GetComponent<Text>().text = specialWeapon;
			cost.GetComponent<Text>().text = specialAmmoCost+",-";
			currentPrice = specialAmmoCost;
			break;

		
		case "cannonT1":
			canonNumber = 0;
			if (GameControl.control.canonUpgrades[canonNumber] < maxUpgrade)
			{
				header.GetComponent<Text>().text = canonName[GameControl.control.canonUpgrades[canonNumber]-1];
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
				header.GetComponent<Text>().text = canonName[GameControl.control.canonUpgrades[canonNumber]-1];
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
				header.GetComponent<Text>().text = canonName[GameControl.control.canonUpgrades[canonNumber]-1];
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
				header.GetComponent<Text>().text = canonName[GameControl.control.canonUpgrades[canonNumber]-1];
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
				header.GetComponent<Text>().text = canonName[GameControl.control.canonUpgrades[canonNumber]-1];
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
				header.GetComponent<Text>().text = canonName[GameControl.control.canonUpgrades[canonNumber]-1];
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
				header.GetComponent<Text>().text = thrusterName[GameControl.control.thrusterUpgrade-1];
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

		if (noMoreUpgrade == true)
		{
			header.GetComponent<Text> ().text = "";
			infoText.GetComponent<Text> ().text = "";
			cost.GetComponent<Text> ().text = "";
			currentPrice = 0;
		}
	}
		
}
