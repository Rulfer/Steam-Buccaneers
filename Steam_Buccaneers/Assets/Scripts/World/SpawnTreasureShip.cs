using UnityEngine;
using System.Collections;

public class SpawnTreasureShip : MonoBehaviour 
{
	float nextSpawnIn;
	public GameObject player;
	public GameObject treasureShip;
	float xPos;
	float zPos;
	Vector3 spawnPos;
	float spawnDistance;
	//float timeLeft;

	// Use this for initialization
	void Start () 
	{
		GetTime();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Timer();
	}

	void Timer ()
	{
		//timeLeft = nextSpawnIn;
		//timeLeft -= Time.deltaTime;
		nextSpawnIn -= Time.deltaTime;

		if (nextSpawnIn <= 0f)
		{
			SpawnTreasure();
		}

		//Debug.Log (nextSpawnIn);
		
		
		
	}

	//gets a random time between 5 and 10 minutes, when the next treasure ship spawns
	void GetTime()
	{
		nextSpawnIn = Random.Range (30, 60);
	}
		
	// Spawns the treasure ship at a random position around the player
	void SpawnTreasure ()
	{
		
		spawnPos = treasureSpawnpoint();
		spawnDistance = Vector3.Distance (spawnPos, player.transform.position);

		Instantiate(treasureShip, spawnPos, player.transform.rotation);
		GetTime();
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
