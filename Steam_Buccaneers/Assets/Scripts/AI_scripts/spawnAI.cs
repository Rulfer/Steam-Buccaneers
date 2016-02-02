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
			Debug.Log("Hello");
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

		//Create random numbers between -5 and 5
		float tempX = Random.Range(-5.0f, 5.0f);
		float tempZ = Random.Range(-5.0f, 5.0f);

		//Based on tempX and tempZ, we decide where the AI will spawn.
		//Use "Random.Range" to give he AI a random x and z position based on tempX and tempZ.
		//These can and should be tweeked when player control is finished. 
		if(tempX < 0 && tempZ < 0)
		{
			spawnPosition = new Vector3(Random.Range(playerPoint.transform.position.x -40.0f, playerPoint.transform.position.x -10.0f), -50, 
				Random.Range(playerPoint.transform.position.z - 40.0f, playerPoint.transform.position.z -10.0f));
		}

		else if(tempX < 0 && tempZ >= 0)
		{
			spawnPosition = new Vector3(Random.Range(playerPoint.transform.position.x -40.0f, playerPoint.transform.position.x -10.0f), -50, 
				Random.Range(playerPoint.transform.position.z + 10.0f, playerPoint.transform.position.z + 40.0f));
		}

		else if(tempX >= 0 && tempZ < 0)
		{
			spawnPosition = new Vector3(Random.Range(playerPoint.transform.position.x + 10.0f, playerPoint.transform.position.x + 40.0f), -50, 
				Random.Range(playerPoint.transform.position.z - 40.0f, playerPoint.transform.position.z -10.0f));
		}

		else if(tempX >= 0 && tempZ >= 0)
		{
			spawnPosition = new Vector3(Random.Range(playerPoint.transform.position.x + 10.0f, playerPoint.transform.position.x + 40.0f), -50, 
				Random.Range(playerPoint.transform.position.z + 10.0f, playerPoint.transform.position.z + 40.0f));
		}

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

