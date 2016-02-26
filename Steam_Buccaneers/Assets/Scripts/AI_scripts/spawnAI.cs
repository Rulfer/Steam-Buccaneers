using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnAI : MonoBehaviour
{
	public static spawnAI spawn;

	private GameObject playerPoint; //Player position
	private GameObject origin; //Position of players original startoint in the game
	private GameObject bossSpawn; //Spawnpoint of the boss
	public GameObject[] marineShips = new GameObject[10]; //Array holding all living Marines
	public GameObject AI; //The Marine prefab
	public GameObject Boss; //The Boss prefab

	public static int[] cannonLevel = new int[6];
	private bool[] cannonUpgraded = new bool[6];
	public bool[] availableIndes = new bool[10]; //Bool used to check the availability in the marineShips array
	public bool stopSpawn = false; //Stops the spawning when a combat is going on
	public bool stopFightTimer = false;

	public static Vector3 spawnPosition; //Where the AI should spawn
	public static Vector3 patrolPoint;

	public int livingShips = 0;
	private int lastSpawn; //The last spawned Marine
	public float startFightTimer = 0;

	// Use this for initialization
	void Start ()
	{
		Debug.Log(marineShips.Length + " this is aasdasd");
		for(int i = 0; i < marineShips.Length; i++)
		{
			marineShips[i] = null;
			availableIndes[i] = true;
		}
		playerPoint = GameObject.FindGameObjectWithTag("Player");
		origin = GameObject.Find("GameOrigin");
		bossSpawn = GameObject.Find("BossSpawn");
		spawnShip ();
	}

	void Update()
	{
		startFightTimer += Time.deltaTime;

		if(stopFightTimer == true)
			startFightTimer = 0;
		
		if(startFightTimer > 45)
		{
			Debug.Log("We started a fight!");
			startFightTimer = 0;
			marineShips[lastSpawn].GetComponent<AIMaster>().deaktivatePatroling();
			marineShips[lastSpawn].GetComponent<AIMaster>().killMarines();
		}
	}
	//After X (now 5) seconds the checkShipStatus function will run
	//This function is used to check weather or not to spawn a new ship
	//every X second.
	void waitBeforeNewSpawn () 
	{
		Invoke ("checkShipStatus", 1);
	}

	//Checks if we should spawn a new ship or not
	void checkShipStatus ()
	{
		//There are no living ships, therefore we spawn a new one
		if(stopSpawn == false)
			spawnShip ();
		else
			waitBeforeNewSpawn();
	}

	void setCannonLevel()
	{
		float relativeOriginPosition = Vector3.Distance (playerPoint.transform.position, origin.transform.position); //Distance between player and where the boss spawns
		float temp = Mathf.Floor(relativeOriginPosition * 0.1f); //10% of the distance is the new temp;
		int upgradedWapons = 0;
		int ranNum;

		for(int i = 0; i < 6; i++)
		{
			ranNum = Random.Range (0, 101); //Generates a number from 0 to 100
			if(ranNum < temp) //The roll is low enough! We got a lvl 2 gun now
			{
				ranNum = Random.Range(0, 101);
				if(ranNum < temp*0.5)
				{
					cannonLevel[i] = 3;
				}
				else
				{
					cannonLevel[i] = 2;
				}
				upgradedWapons++;
				cannonUpgraded[i] = true;
			}
			else
			{
				cannonLevel[i] = 1;
			}
		}

		//Just add a bunch of tests like these to create definite upgraded guns criterias.
		//By that I mean you can add more tests to guarantee that a minimum ammount of
		//cannons are upgraded, and to what lvl.
		if(relativeOriginPosition > 200 && upgradedWapons < 2) //We need some guaranteed lvl2 weapons
		{
			while(upgradedWapons < 2) //There are too few upgraded cannons
			{
				ranNum = Random.Range(0, 6); //Create a random number from 0 to 5, as this is the array length
				if(cannonUpgraded[ranNum] == false) //The cannon is not upgraded
				{
					cannonUpgraded[ranNum] = true; //It is now!
					cannonLevel[ranNum] = 2; //Set it to lvl 2
					upgradedWapons++; //Update ammount of upgraded weapons
				}
			}
		}
	}

	private Vector3 setPatrolPoint()
	{
		float tempPosX = Random.Range(playerPoint.transform.position.x, playerPoint.transform.position.x + 3000); //Random x position
		float tempPosZ = Random.Range(playerPoint.transform.position.z, playerPoint.transform.position.z + 3000); //Random z position
		float posX;
		float posZ;

		//Creates a random variable from 1 to 10 (the last number is not included, aka 11).
		//Use this number to determine if the variable should be positive or negative, just
		//to create som variation in the spawnpositions of the AI.
		float ranRangeX = Random.Range(1, 11);
		if(ranRangeX > 5)
		{
			posX = playerPoint.transform.position.x + tempPosX;

		}
		else posX = playerPoint.transform.position.x - tempPosX;

		//Does the same to the Z position of the AI as we did with the X position just above this.
		float ranRangeZ = Random.Range(1, 11);
		if(ranRangeZ > 5)
		{
			posZ = playerPoint.transform.position.z + tempPosZ;
		}
		else posZ = playerPoint.transform.position.z - tempPosZ;

		return new Vector3(posX, 1946, posZ);

	}


	void spawnShip ()
	{
		livingShips++;

		setCannonLevel();
		patrolPoint = setPatrolPoint();
		//float relativePoint = Vector3.Distance (playerPoint.transform.position, origin.transform.position); //Distance between player and Origin
		float relativeBossPoint = Vector3.Distance (playerPoint.transform.position, bossSpawn.transform.position); //Distance between player and where the boss spawns
		Debug.Log("relativeBossPoint" + relativeBossPoint);
		if(relativeBossPoint > 100) //We are too far away from the boss, so we spawn a regular AI.
		{
			if(livingShips < 10)
			{
				//Create random numbers between 60 and 120
				float tempPosX = Random.Range(100f, 200f); //Random x position
				float tempPosZ = Random.Range(100f, 200f); //Random z position
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

				spawnPosition = new Vector3(posX+playerPoint.transform.position.x, 0, posZ+playerPoint.transform.position.z); //Sets the position of the AI relative to the player position

				Debug.Log("Hi");
				for(int i = 0; i < marineShips.Length; i++)
				{
					Debug.Log("Hi2");

					if(availableIndes[i] == true)
					{			Debug.Log("Hi3");
						
						GameObject temp = (Instantiate(AI));
						lastSpawn = i;
						marineShips[i] = temp;
						availableIndes[i] = false;

						marineShips[i].transform.position = spawnPosition;

						float aiOriginDistance = Vector3.Distance (AI.transform.position, origin.transform.position); //Distance between player and Origin
						marineShips[i].gameObject.GetComponent<AIMaster>().aiHealth = Mathf.Floor(aiOriginDistance * 0.01f); //AI health is equal to the number that is 10% of the distance between it and origin
						if(marineShips[i].gameObject.GetComponent<AIMaster>().aiHealth < 20)
						{
							marineShips[i].gameObject.GetComponent<AIMaster>().aiHealth = 20;
						}
						marineShips[i].gameObject.GetComponent<AIMaster>().arrayIndex = i;
						waitBeforeNewSpawn();
						return;
					}
				}
			}
		}

		else //We should spawn the boss
		{
			for(int i = 0; i < marineShips.Length; i++)
			{
				if(marineShips[i] != null)
				{
					marineShips[i].GetComponent<AImove>().isPatroling = false;
					marineShips[i].GetComponent<AImove>().isFleeing = true;
				}
			}
			Instantiate(Boss);
			Boss.GetComponent<AIMaster>().isBoss = true;
			Boss.transform.position = new Vector3(bossSpawn.transform.position.x, 0, bossSpawn.transform.position.z); //Spawn the boss at the boss's spawn point
			Boss.transform.GetComponent<AIMaster>().aiHealth = 100; //Sets the health
			stopFightTimer = true;
			stopSpawn = true;
		}

		waitBeforeNewSpawn();
	}
}

