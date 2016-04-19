using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TreasurePlanet : MonoBehaviour 
{
	bool treasureHasBeenPickedUp;




	// Use this for initialization
	void Start ()
	{
		treasureHasBeenPickedUp = false;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" && treasureHasBeenPickedUp == false)
		{
			//spill av animasjon

			treasureHasBeenPickedUp = true;
			GameControl.control.money += 500;
			GameObject.Find("value_scraps_tab").GetComponent<Text>().text = GameControl.control.money.ToString();

		}
	}
}