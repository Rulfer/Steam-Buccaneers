using UnityEngine;
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
			PickupTreasure();
		}
		if (diglet == true)
		{
			GoBackDown();
		}
		//OnTriggerEnter();
		//Debug.Log (enterPos + " EnterPosition");
		

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" && treasureHasBeenPickedUp == false)
		{
			//Debug.Log ("DO THIS PLEASE");
			startTime = Time.time;
			treasurePos = treasureChest.transform.position;
			enterPos = player.transform.position;
			enterFlyUp = new Vector3 (enterPos.x, enterPos.y + 80, enterPos.z);
			travelLenght = Vector3.Distance(enterPos,enterFlyUp);

			niglet = true;
		}
			
		//Destroy(player);
	}

	void PickupTreasure ()
	{
		//Debug.Log ("DO THIS PLEASE");

		float distCovered = (Time.time -startTime)*speed;
		float fracJourney = distCovered /travelLenght;
		player.transform.position = Vector3.Lerp(enterPos,enterFlyUp,fracJourney);
		if (player.transform.position.y >= 80)
		{
			diglet = true;
			niglet = false;
			treasureHasBeenPickedUp = true;
			startTime = Time.time;
			//treasureChest.GetComponent<Animation>;
			//treasureChest.animation.Play();
		}
			
	}

	void GoBackDown()
	{
		
		float distCovered = (Time.time -startTime)*speed;
		float fracJourney = distCovered /travelLenght;
		player.transform.position = Vector3.Lerp(enterFlyUp,enterPos,fracJourney);
		if (player.transform.position.y <= 0)
		{
			diglet = false;
			//player.transform.position.y = 0F;
		}


	}
}
