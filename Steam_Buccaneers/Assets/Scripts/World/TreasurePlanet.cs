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

	void OnTriggerEnter (Collider other)
	{

		Debug.Log ("Enter treasureplanet");
		if (other.tag == "Player" && treasureHasBeenPickedUp == false )
		{
			if (this.transform.name == "TreasurePlanet")
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

			treasureHasBeenPickedUp = true;
			if (this.transform.name == "TreasurePlanet")
			{
				GameControl.control.treasureplanetsfound [0] = true;
			} 
			else
			{
				GameControl.control.treasureplanetsfound [1] = true;
			}
			GameControl.control.money += 500;
			GameObject.Find("value_scraps_tab").GetComponent<Text>().text = GameControl.control.money.ToString();
			gameObject.transform.parent.tag = "asteroid";
		}
	}
}