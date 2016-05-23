﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TreasurePlanet : MonoBehaviour 
{
	bool treasureHasBeenPickedUp; // bool for checking if the player has picked up the treasure
	public GameObject treasureAnimation; // the animation of the player picking up the scrap



	// Use this for initialization
	void Start ()
	{
		if (this.transform.name == "TreasurePlanet")
		{
			if (GameControl.control.treasureplanetsfound [0] == true)
				treasureHasBeenPickedUp = true;
			else
				treasureHasBeenPickedUp = false;
		} 
		else
		{
			if (GameControl.control.treasureplanetsfound [1] == true)
				treasureHasBeenPickedUp = true;
			else
				treasureHasBeenPickedUp = false;
		}
			
	}

	void OnTriggerEnter (Collider other) // collider for checking if player is to pick up the scrap
	{

		Debug.Log ("Enter treasureplanet");
		if (other.tag == "Player" && treasureHasBeenPickedUp == false ) // if the player hits the collider and has not yet picked up the treasure
		{
			if (this.transform.name == "TreasurePlanet") // if the collider belongs to treasure planet
			{
				if (GameControl.control.treasureplanetsfound [0] == true) 
					return;
			} 
			else
			{
				if (GameControl.control.treasureplanetsfound [1] == true)
					return;
			}
				
			Debug.Log ("Play animation " + treasureAnimation);
			// pauses the game while animation is playing
			GameObject.Find("GameControl").GetComponent<GameButtons>().pause();
			// creates the treasure animation
			Instantiate(treasureAnimation);
			// plays the animation of the player picking up the treasure
			treasureAnimation.GetComponentInChildren<PlayVideoScript>().playTreasureAnimation ();

			treasureHasBeenPickedUp = true; // setting that the player has picked up the treasure
			if (this.transform.name == "TreasurePlanet")
			{
				GameControl.control.treasureplanetsfound [0] = true;
			} 
			else
			{
				GameControl.control.treasureplanetsfound [1] = true;
			}
			GameControl.control.money += 500; // gives the player the treasure
			GameObject.Find("value_scraps_tab").GetComponent<Text>().text = GameControl.control.money.ToString(); // updates players total scrap
			gameObject.transform.parent.tag = "asteroid"; // changes the tag of the treasure planet so it the player will no longer trigger the function of picking up treasure
		}
	}
}