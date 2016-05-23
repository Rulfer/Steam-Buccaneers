using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TreasurePlanet : MonoBehaviour 
{
	bool treasureHasBeenPickedUp;
	public GameObject treasureAnimation;



	// Use this for initialization
	void Start ()
	{
		treasureHasBeenPickedUp = false;
	}

	void OnTriggerEnter (Collider other)
	{
		//Debug.Log ("Enter treasureplanet");
		// if the player enters the collider and the treasure has not yet been picked up
		if (other.tag == "Player" && treasureHasBeenPickedUp == false)
			
		{
			// sets the GUI elements to inactive
			if(GameObject.Find("_GUIManager"))
				GameObject.Find("_GUIManager").SetActive(false);
			Debug.Log ("Play animation " + treasureAnimation);
			// pauses the game while animation is playing
			GameObject.Find("GameControl").GetComponent<GameButtons>().pause();
			// creates the treasure animation
			Instantiate(treasureAnimation);
			// plays the animation of the player picking up the treasure
			treasureAnimation.GetComponentInChildren<PlayVideoScript>().playTreasureAnimation ();
			treasureHasBeenPickedUp = true; // sets the treasure to have been picked up
			GameControl.control.money += 500; // gives the player the scrap
			GameObject.Find("value_scraps_tab").GetComponent<Text>().text = GameControl.control.money.ToString(); //updates scrap number
			gameObject.transform.parent.tag = "asteroid"; // gives the planet another tag
		}
	}
}