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
		Debug.Log("We are waiting");
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

		float relativePoint = Vector3.Distance (playerPoint.transform.position, origin.transform.position);

		float tempX = Random.Range(-5.0f, 5.0f);
		float tempZ = Random.Range(-5.0f, 5.0f);
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

		if(relativePoint < 200)
		{
			Instantiate(AI1); //Spawns the prefab
			AI1.position = spawnPosition;
		}
		else
		{
			Instantiate(AI2); //Spawns the prefab
			AI2.position = spawnPosition;
		}

		livingShip = true;
		waitBeforeNewSpawn();
//		float distance = 1000; //Unnessesary large variable
//		
//		for (int i = 0; i < spawnPoints.Length; i++) //Runs equal to the ammount of spawnpoints
//		{
//			float temp = Vector3.Distance (playerPoint.transform.position, spawnPoints [i].transform.position); //Distance between spawnpoint [i] and player
//			if (temp < distance) //This spawnpoint is closer to the player than the others
//			{
//				distance = temp; //Sets distance = temp to be able to perform new tests
//				tempI = i; //Saves what position in the array the spawnpoint is
//			}
//		}
//
//		if (tempI <= 2) //Will spawn AI_LVL1 ship
//		{ 
//			Instantiate (AI1); //Spawns the prefab
//			AI1.position = spawnPoints [tempI].position; //Sets the AI position equal to the spawnpoint position
//		} 
//		else //Will spawn AI_LVL2 ship
//		{ 
//			Instantiate (AI2); //Spawns the prefab
//			AI2.position = spawnPoints [tempI].position; //Sets the AI position equal to the spawnpoint position
//		}
//
//		livingShip = true; //There is now a living AI
//		waitBeforeNewSpawn (); //Restarts the whole prosess
	}
}

