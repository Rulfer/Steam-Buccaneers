using UnityEngine;
using System.Collections;

public class SpawnTreasureShip : MonoBehaviour 
{
	float nextSpawnIn; // variable for declaring how long until the next spawn of a treasure ship
	public GameObject player; // the player game object
	public GameObject treasureShip; // the treasure ship game object
	float xPos; // the position on the x-axis the treasure ship is to spawn at
	float zPos; // the position on the z-axis the treasure ship is to spawn at
	Vector3 spawnPos; // vector for holding the spawn coordinates
	float spawnDistance; // 

	// Use this for initialization
	void Start () 
	{
		GetTime(); // initiates the function to get time for a ship to spawn
	}
	
	// Update is called once per frame
	void Update () 
	{
		Timer(); // always runs the timer function
	}

	void Timer ()
	{
		nextSpawnIn -= Time.deltaTime; // count down to when the next ship should spawn

		if (nextSpawnIn <= 0f)
		{
			SpawnTreasure(); // does the function that spawns the treasure
		}
		
	}

	//gets a random time between 30 and 60 seconds, when the next treasure ship spawns
	void GetTime()
	{
		nextSpawnIn = Random.Range (30, 60);
	}
		
	// Spawns the treasure ship at a random position around the player
	void SpawnTreasure ()
	{
		
		spawnPos = treasureSpawnpoint(); // sets the spawn position equal to the spawn point 
		spawnDistance = Vector3.Distance (spawnPos, player.transform.position); // 

		// creates the treasure ship at the spawn position
		Instantiate(treasureShip, spawnPos, player.transform.rotation);
		GetTime(); // gets a new time for the next treasure to spawn
	}

	private Vector3 treasureSpawnpoint()
	{
		bool foundSpawn = false;
		Vector3 testerSpawnPos = new Vector3(0, 0, 0);

		while(foundSpawn == false)
		{
			//Create random numbers between 100 and 200
			float tempPosX = Random.Range(300, 500); //Random x position
			float tempPosZ = Random.Range(300, 500); //andom z position
			float posX;
			float posZ;
			//Creates a random variable from 1 to 10 (the last number is not included, aka 11).
			//Use this number to determine if the variable should be positive or negative, just
			//to create som variation in the spawnpositions of the AI.
			float ranRangeX = Random.Range(1, 11);
			if(ranRangeX > 5)
			{
				posX = tempPosX;
			}
			else posX = -tempPosX;

			//Does the same to the Z position of the AI as we did with the X position just above this.
			float ranRangeZ = Random.Range(1, 11);
			if(ranRangeZ > 5)
			{
				posZ = tempPosZ;
			}
			else posZ = -tempPosZ;

			testerSpawnPos = new Vector3 (posX+player.transform.position.x, 2, posZ+player.transform.position.z);

			Collider[] colliders = Physics.OverlapSphere(testerSpawnPos, 30);
			if(colliders.Length == 0)
				foundSpawn = true; //Sets the position of the Marine relative to the player position
		}

		return testerSpawnPos;
	}
}
