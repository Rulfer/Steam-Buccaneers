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
		Debug.Log ("Enter treasureplanet");
		if (other.tag == "Player" && treasureHasBeenPickedUp == false)
		{
			Debug.Log ("Play animation " + treasureAnimation);
			//spill av animasjon
			GameObject.Find("GameControl").GetComponent<GameButtons>().pause();
			Instantiate(treasureAnimation);
			treasureAnimation.GetComponentInChildren<PlayVideoScript>().playTreasureAnimation ();
			treasureHasBeenPickedUp = true;
			GameControl.control.money += 500;
			GameObject.Find("value_scraps_tab").GetComponent<Text>().text = GameControl.control.money.ToString();
			gameObject.transform.parent.tag = "asteroid";
		}
	}
}