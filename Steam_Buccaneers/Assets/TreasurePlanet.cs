using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TreasurePlanet : MonoBehaviour 
{
	public GameObject player;
	public GameObject treasureChest;
	Vector3 enterPos;
	Vector3 treasurePos;
	Vector3 enterFlyUp;
	bool treasureHasBeenPickedUp;
	float travelLenght;
	float speed = 10f;
	float startTime;
	bool niglet = false;
	bool diglet = false;
	bool pickUpTreasure = false;
	bool pickingUpTreasure = false;


	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find("PlayerShip");
		treasureHasBeenPickedUp = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (niglet == true)
		{
			GoUp();
		}
		if (diglet == true)
		{
			GoBackDown();
		}
		if (pickUpTreasure == true)
		{
			PickupTreasure();
		}
		//OnTriggerEnter();
		//Debug.Log (enterPos + " EnterPosition");
		

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" && treasureHasBeenPickedUp == false && pickingUpTreasure == false)
		{
			//Debug.Log ("DO THIS PLEASE");
			startTime = Time.time;
			treasurePos = treasureChest.transform.position;
			enterPos = player.transform.position;
			enterFlyUp = new Vector3 (enterPos.x, enterPos.y + 80, enterPos.z);
			travelLenght = Vector3.Distance(enterPos,enterFlyUp);
			pickingUpTreasure = true;
			niglet = true;
			PlayerMove2.steerShip = false;
		}
			
		//Destroy(player);
	}

	void GoUp ()
	{
		//Debug.Log ("DO THIS PLEASE");

		float distCovered = (Time.time -startTime)*speed;
		float fracJourney = distCovered /travelLenght;
		player.transform.position = Vector3.Lerp(enterPos,enterFlyUp,fracJourney);
		if (player.transform.position.y >= 80)
		{
			niglet = false;
			pickUpTreasure = true;

			startTime = Time.time;
		}
			
	}

	void PickupTreasure()
	{
		if (treasureHasBeenPickedUp != true)
		{
			float distCovered = (Time.time -startTime)*speed;
			float fracJourney = distCovered /travelLenght;
			player.transform.position = Vector3.Lerp(enterFlyUp,treasurePos,fracJourney);


			if (player.transform.position == treasurePos)
			{
				//pickUpTreasure = false;
				GameControl.control.money += 500;
				treasureHasBeenPickedUp = true;
				startTime = Time.time;
				GameObject.Find("value_scraps_tab").GetComponent<Text>().text = GameControl.control.money.ToString();
			}
		}

		else
		{
			float distCovered = (Time.time -startTime)*speed;
			float fracJourney = distCovered /travelLenght;
			player.transform.position = Vector3.Lerp(treasurePos,enterFlyUp,fracJourney);
			if (player.transform.position == enterFlyUp)
			{
				startTime = Time.time;
				pickUpTreasure = false;
				diglet = true;
			}
		}
	}

	void GoBackDown()
	{
		
		float distCovered = (Time.time -startTime)*speed;
		float fracJourney = distCovered /travelLenght;
		player.transform.position = Vector3.Lerp(enterFlyUp,enterPos,fracJourney);
		if (player.transform.position.y <= 0)
		{
			PlayerMove2.steerShip = true;
			diglet = false;
		}
	}
}
