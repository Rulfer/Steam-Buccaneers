using UnityEngine;
using System.Collections;

public class spawnAI : MonoBehaviour
{
	private GameObject playerPoint; //Player position
	private GameObject origin; //Position of players original startoint in the game
	private GameObject bossSpawn;
	public GameObject AI;
	public GameObject Boss;

	private Vector3 spawnPosition; //Where the AI should spawn

	public static bool livingShip = false; //Public bool to check if there is a living AI or not

	// Use this for initialization
	void Start ()
	{
		playerPoint = GameObject.FindGameObjectWithTag("Player");
		origin = GameObject.Find("GameOrigin");
		bossSpawn = GameObject.Find("BossSpawn");
		spawnShip ();
	}

	//After X (now 5) seconds the checkShipStatus function will run
	//This function is used to check weather or not to spawn a new ship
	//every X second.
	void waitBeforeNewSpawn () 
	{
		Invoke ("checkShipStatus", 5);
	}

	//Checks if we should spawn a new ship or not
	void checkShipStatus ()
	{
		if (livingShip == false) { //There are no living ships, therefore we spawn a new one
			spawnShip ();
		}

		else //There's a living AI. Restart the timer.
		{
			waitBeforeNewSpawn ();
		} 
	}

	void spawnShip ()
	{
		livingShip = true;

		//float relativePoint = Vector3.Distance (playerPoint.transform.position, origin.transform.position); //Distance between player and Origin
		float relativeBossPoint = Vector3.Distance (playerPoint.transform.position, bossSpawn.transform.position); //Distance between player and where the boss spawns

		if(relativeBossPoint > 100)
		{
			//Create random numbers between -60 and -20
			float tempPosX = Random.Range(60f, 120f);
			float tempPosZ = Random.Range(60f, 120f);
			float posX;
			float posZ;
			float ranRangeX = Random.Range(1, 11);
			if(ranRangeX > 5)
			{
				posX = tempPosX;
			}
			else posX = -tempPosX;

			float ranRangeZ = Random.Range(1, 11);
			if(ranRangeZ > 5)
			{
				posZ = tempPosZ;
			}
			else posZ = -tempPosZ;

			spawnPosition = new Vector3(posX, -44, posZ);

			//Here I should actually check distance to Origin, and spawn an AI
			//with better gear depending on the distance.
			//We don't really have the code for that yet, so for now I just spawn the regular AI.
			Instantiate(AI);
			AI.transform.position = spawnPosition;

			float aiOriginDistance = Vector3.Distance (AI.transform.position, origin.transform.position); //Distance between player and Origin
			AIMaster.aiHealth = Mathf.Floor(aiOriginDistance * 0.1f); //AI health is equal to the number that is 10% of the distance between it and origin
		}

		else
		{
			Instantiate(Boss);
			Boss.transform.position = new Vector3(bossSpawn.transform.position.x, -44, bossSpawn.transform.position.z);
			AIMaster.aiHealth = 50;
		}

		waitBeforeNewSpawn();
	}
}

