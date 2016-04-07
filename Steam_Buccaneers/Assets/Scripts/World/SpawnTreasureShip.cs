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
		nextSpawnIn = Random.Range (300, 600);
	}
		
	// Spawns the treasure ship at a random position around the player
	void SpawnTreasure ()
	{
		xPos = player.transform.position.x + Random.Range(-500,500);
		zPos = player.transform.position.z + Random.Range(-500,500);
		spawnPos = new Vector3 (xPos, 0, zPos);
		spawnDistance = Vector3.Distance (spawnPos, player.transform.position);

		if (spawnDistance < 300)
		{
			SpawnTreasure();
		}

		else
		{
		Instantiate(treasureShip, spawnPos, player.transform.rotation);
		GetTime();
		}
	}
}
