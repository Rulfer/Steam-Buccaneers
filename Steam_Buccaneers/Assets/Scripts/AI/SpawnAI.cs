using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnAI : MonoBehaviour
{
	/**
	 *The max number of living marines at any given time.
	 *Adjust this number to change how many marines that can be alive/in scene at the same time. 
	 */
	public int maxMarines = 5;

	public static SpawnAI spawn;
	public GameObject playerPoint; //Player position
	public GameObject origin; //Position of players original startoint in the game
	public GameObject bossSpawn; //Spawnpoint of the boss
	public GameObject[] marineShips; //Array holding all living Marines
	public GameObject Marine; //The Marine prefab
	public GameObject Boss; //The Boss prefab
	public GameObject Cargo; //The Cargo prefab

	public static int[] cannonLevel = new int[6];
	public bool[] cannonUpgraded = new bool[6];
	public bool[] availableIndes; //Bool used to check the availability in the marineShips array
	public bool stopSpawn = false; //Stops the spawning when a combat is going on
	public bool stopFightTimer = false;
	public bool livingCargo = false;

	public static Vector3 spawnPosition; //Where the AI should spawn

	public int livingShips = 0;
	public float startFightTimer = 0;

	// Use this for initialization
	void Start ()
	{
		spawn = this;

		marineShips = new GameObject[maxMarines];
		availableIndes = new bool[maxMarines];

		for(int i = 0; i < marineShips.Length; i++)
		{
			marineShips[i] = null;
			availableIndes[i] = true;
		}
		waitBeforeNewSpawn();
		waitBeforeCargoSpawn();
	}

	void Update()
	{
		startFightTimer += Time.deltaTime;

		if(stopFightTimer == true)
			startFightTimer = 0;
		
		if(startFightTimer > 45)
		{
			startFightTimer = 0;
			float temp = 10000;
			float aiPlayerDistance;
			int tempI = 100;
			for(int i = 0; i < marineShips.Length; i++)
			{
				if(marineShips[i] != null)
				{
					aiPlayerDistance = Vector3.Distance (playerPoint.transform.position, marineShips[i].transform.position); //Distance between player and where the boss spawns
					if(aiPlayerDistance < temp)
					{
						temp = aiPlayerDistance;
						tempI = i;
					}
				}
			}
			if(tempI != 100)
			{
				marineShips[tempI].GetComponent<AIMaster>().deaktivatePatroling();
				marineShips[tempI].GetComponent<AIMaster>().allAIFlee();
			}
		}
	}
	//After X (now 1) seconds the checkShipStatus function will run
	//This function is used to check weather or not to spawn a new ship
	//every X second.
	void waitBeforeNewSpawn () 
	{
		InvokeRepeating ("checkShipStatus", 1, 1);
	}

	void waitBeforeCargoSpawn()
	{
		InvokeRepeating("spawnCargo", 1, 10);
	}

	//Checks if we should spawn a new ship or not
	void checkShipStatus ()
	{
		//There are no living ships, therefore we spawn a new one
		if(stopSpawn == false && GameObject.Find("Boss(Clone)") == null)
		{
			spawnShip ();
		}
	}

	void setCannonLevel()
	{
		float relativeOriginPosition = Vector3.Distance (playerPoint.transform.position, origin.transform.position); //Distance between player and where the boss spawns
		float temp = Mathf.Floor(relativeOriginPosition * 0.02f); //2% of the distance is the new temp;
		int upgradedWapons = 0;
		int ranNum;
		if(livingCargo == false)
		{
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
		}

		else
		{
			for(int i = 0; i < 2; i++)
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
		return new Vector3(posX, 0, posZ);

	}

	private Vector3 marineSpawnpoint()
	{
		bool foundSpawn = false;
		Vector3 testerSpawnPos = new Vector3(0, 0, 0);

		while(foundSpawn == false)
		{
			//Create random numbers between 100 and 200
			float tempPosX = Random.Range(100f, 250f); //Random x position
			float tempPosZ = Random.Range(100f, 250f); //andom z position
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

			testerSpawnPos = new Vector3 (posX+playerPoint.transform.position.x, 2, posZ+playerPoint.transform.position.z);

			Collider[] colliders = Physics.OverlapSphere(testerSpawnPos, 10);
			if(colliders.Length == 0)
				foundSpawn = true; //Sets the position of the Marine relative to the player position
		}

		return testerSpawnPos;
	}

	private Vector3 cargoSpawnpoint()
	{
		Vector3 playerPosition = playerPoint.transform.position;
		Vector3 playerDirection = playerPoint.transform.forward;
		//Quaternion playerRotation = playerPoint.transform.rotation;
		float spawnDistance = 200;
		Vector3 spawnPos = playerPosition + playerDirection * spawnDistance;

		Collider[] colliders = Physics.OverlapSphere(spawnPos, 10);
		if(colliders.Length == 0)
			return spawnPos;
		else
			return marineSpawnpoint();
	}


	void spawnCargo()
	{
		if(livingCargo == false && stopSpawn == false)
		{
			livingCargo = true;
			setCannonLevel();

			Instantiate(Cargo, cargoSpawnpoint(), playerPoint.transform.rotation);
			//Cargo.transform.position = spawnPos;
			Cargo.GetComponent<AIMaster>().isCargo = true;
			Cargo.GetComponent<AIPatroling>().target = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			Cargo.GetComponent<AIPatroling>().target.transform.position = setPatrolPoint();
			float aiOriginDistance = Vector3.Distance (playerPoint.transform.position, origin.transform.position); //Distance between player and Origin
			Cargo.GetComponent<AIMaster>().aiHealth = Mathf.Floor(aiOriginDistance * 0.01f); //AI health is equal to the number that is 10% of the distance between it and origin
			if(Cargo.GetComponent<AIMaster>().aiHealth < 20)
			{
				Cargo.GetComponent<AIMaster>().aiHealth = 20;
			}
		}
	}


	void spawnShip ()
	{
		Debug.Log("Spaning MARINE");

		//float relativePoint = Vector3.Distance (playerPoint.transform.position, origin.transform.position); //Distance between player and Origin
		float relativeBossPoint = Vector3.Distance (playerPoint.transform.position, bossSpawn.transform.position); //Distance between player and where the boss spawns
		if(relativeBossPoint > 150) //We are too far away from the boss, so we spawn a regular AI.
		{
			if(livingShips < maxMarines)
			{
				livingShips++;
				setCannonLevel();

				for(int i = 0; i < marineShips.Length; i++)
				{
					if(availableIndes[i] == true)
					{		
						GameObject temp = (Instantiate(Marine));
						temp.transform.position = marineSpawnpoint();
						marineShips[i] = temp;
						availableIndes[i] = false;
						//marineShips[i].GetComponent<AIPatroling>().targetPos = patrolPoint;
						marineShips[i].GetComponent<AIPatroling>().target = GameObject.CreatePrimitive(PrimitiveType.Sphere);
						marineShips[i].GetComponent<AIPatroling>().target.transform.position = setPatrolPoint();
						float aiOriginDistance = Vector3.Distance (playerPoint.transform.position, origin.transform.position); //Distance between player and Origin
						marineShips[i].gameObject.GetComponent<AIMaster>().aiHealth = Mathf.Floor(aiOriginDistance * 0.01f); //AI health is equal to the number that is 10% of the distance between it and origin
						Debug.Log("Marine health: " + marineShips[i].gameObject.GetComponent<AIMaster>().aiHealth);
						if(marineShips[i].gameObject.GetComponent<AIMaster>().aiHealth < 20)
						{
							marineShips[i].gameObject.GetComponent<AIMaster>().aiHealth = 20;
						}
						marineShips[i].gameObject.GetComponent<AIMaster>().arrayIndex = i;
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
			Boss.transform.position = new Vector3(bossSpawn.transform.position.x, 2, bossSpawn.transform.position.z); //Spawn the boss at the boss's spawn point
			Boss.transform.GetComponent<AIMaster>().aiHealth = 100; //Sets the health
			stopFightTimer = true;
			stopSpawn = true;
		}
	}
}

