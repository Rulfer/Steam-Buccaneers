using UnityEngine;
using System.Collections;

public class TreasurePlanet : MonoBehaviour 
{
	public GameObject player;
	public GameObject treasureChest;
	Vector3 enterPos;
	Vector3 treasurePos;
	bool treasureHasBeenPickedUp;
	float travelLenght;
	float speed = 1f;
	float startTime;


	// Use this for initialization
	void Start ()
	{
		treasureHasBeenPickedUp = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//OnTriggerEnter();
		//Debug.Log (enterPos + " EnterPosition");
		

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" && treasureHasBeenPickedUp == false)
		{
			Debug.Log ("DO THIS PLEASE");
			startTime = Time.time;
			treasurePos = treasureChest.transform.position;
			enterPos = player.transform.position;
			travelLenght = Vector3.Distance(enterPos,treasurePos);
			PickupTreasure();
		}
			
		//Destroy(player);
	}

	void PickupTreasure ()
	{
		Debug.Log ("DO THIS PLEASE");
		float distCovered = (Time.time -startTime)*speed;
		float fracJourney = distCovered /travelLenght;
		player.transform.position = Vector3.Lerp(enterPos,treasurePos,fracJourney);
	}
}
