using UnityEngine;
using System.Collections;

public class spawnAI : MonoBehaviour
{
	public Transform playerPoint; //Player position
	public Transform AI1; //AI_LVL1 prefab
	public Transform AI2; //AI_LVL2 prefab
	public Transform[] spawnPoints; //Array containing all spawnpoints

	public static bool livingShip = false; //Public bool to check if there is a living AI or not

	private int tempI; //Used to determine what lvl of AI that is to spawn

	// Use this for initialization
	void Start ()
	{
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
		float distance = 1000; //Unnessesary large variable
		
		for (int i = 0; i < spawnPoints.Length; i++) //Runs equal to the ammount of spawnpoints
		{
			float temp = Vector3.Distance (playerPoint.transform.position, spawnPoints [i].transform.position); //Distance between spawnpoint [i] and player
			if (temp < distance) //This spawnpoint is closer to the player than the others
			{
				distance = temp; //Sets distance = temp to be able to perform new tests
				tempI = i; //Saves what position in the array the spawnpoint is
			}
		}

		if (tempI <= 2) //Will spawn AI_LVL1 ship
		{ 
			Instantiate (AI1); //Spawns the prefab
			AI1.position = spawnPoints [tempI].position; //Sets the AI position equal to the spawnpoint position
		} 
		else //Will spawn AI_LVL2 ship
		{ 
			Instantiate (AI2); //Spawns the prefab
			AI2.position = spawnPoints [tempI].position; //Sets the AI position equal to the spawnpoint position
		}

		livingShip = true; //There is now a living AI
		waitBeforeNewSpawn (); //Restarts the whole prosess
	}
}

