using UnityEngine;
using System.Collections;

public class spawnAI : MonoBehaviour
{
	private GameObject playerPoint; //Player position
	private GameObject origin;
	public Transform AI1; //AI_LVL1 prefab
	public Transform AI2; //AI_LVL2 prefab

	private Vector3 spawnPosition;

	public static bool livingShip = false; //Public bool to check if there is a living AI or not

	private int tempI; //Used to determine what lvl of AI that is to spawn

	// Use this for initialization
	void Start ()
	{
		playerPoint = GameObject.FindGameObjectWithTag("Player");
		origin = GameObject.Find("GameOrigin");
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

		float relativePoint = Vector3.Distance (playerPoint.transform.position, origin.transform.position); //Distance between player and Origin

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

		spawnPosition = new Vector3(posX, -50, posZ);

		if(relativePoint < 200) //Spawns AI_LVL1 if the player is close to Origin
		{
			Instantiate(AI1); //Spawns the prefab
			AI1.position = spawnPosition;
		}
		else //Spawn AI_LVL2
		{
			Instantiate(AI2); //Spawns the prefab
			AI2.position = spawnPosition;
		}

		waitBeforeNewSpawn();
	}
}

