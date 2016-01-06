using UnityEngine;
using System.Collections;

public class spawnAI : MonoBehaviour
{
	public Transform playerPoint;
	public Transform AI1;
	public Transform AI2;
	public Transform[] spawnPoints;

	private float distance = 1000;
	private int tempI;

	public bool livingShip = false;

	private AIMaster aiObject;
	GameObject aiHolder;

	// Use this for initialization
	void Start ()
	{
		spawnShip ();
	}

	void waitBeforeNewSpawn ()
	{
		Invoke ("checkShipStatus", 5);
	}

	void checkShipStatus ()
	{
		if (livingShip == false) { //There are no living ships, therefore we spawn a new one
			spawnShip ();
		}

		else //There is a ship alive. Check the distance between the player and the AI to determine
			//wether or not to destroy it
		{
			aiObject = Object.FindObjectOfType<AIMaster> (); //Find the AI
			aiHolder = aiObject.gameObject;

			float temp = Vector3.Distance (playerPoint.transform.position, aiHolder.transform.position); //The distance between the player and AI
			if (temp >= 100)
			{
				Destroy (aiHolder); //The AI is now dead.
				livingShip = false;
			}

			waitBeforeNewSpawn ();
		} 
	}

	void spawnShip ()
	{
		
		for (int i = 0; i < spawnPoints.Length; i++) 
		{
			float temp = Vector3.Distance (playerPoint.transform.position, spawnPoints [i].transform.position);
			if (temp < distance) 
			{
				distance = temp;
				tempI = i;
			}
		}

		if (tempI <= 2) {
			Instantiate (AI1);
			AI1.position = spawnPoints [tempI].position;
		} else {
			Instantiate (AI2);
			AI2.position = spawnPoints [tempI].position;
		}

		livingShip = true;
		waitBeforeNewSpawn ();
	}
}

