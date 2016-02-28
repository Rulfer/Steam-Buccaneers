using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnAI : MonoBehaviour
{
	public static spawnAI spawn;
	public GameObject playerPoint; //Player position
	public GameObject origin; //Position of players original startoint in the game
	private GameObject bossSpawn; //Spawnpoint of the boss
	public GameObject[] marineShips = new GameObject[10]; //Array holding all living Marines
	public GameObject AI; //The Marine prefab
	public GameObject Boss; //The Boss prefab

	public static int[] cannonLevel = new int[6];
	public bool[] cannonUpgraded = new bool[6];
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
		spawn = this;
		//Debug.Log(marineShips.Length + " this is aasdasd");
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
		
		if(startFightTimer > 10)
		{
			//Debug.Log("We started a fight!");
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
				marineShips[tempI].GetComponent<AIMaster>().killMarines();
			}
		}
	}
	//After X (now 1) seconds the checkShipStatus function will run
	//This function is used to check weather or not to spawn a new ship
	//every X second.
	void waitBeforeNewSpawn () 
	{
		//Debug.Log("waitBeforeNewSpawn function started");
		Invoke ("checkShipStatus", 1);
	}

	//Checks if we should spawn a new ship or not
	void checkShipStatus ()
	{
		//Debug.Log("We are checking ship status");
		//There are no living ships, therefore we spawn a new one
		if(stopSpawn == false)
		{
			//Debug.Log("We should spawn a ship");
			spawnShip ();
		}
		else
		{
			//Debug.Log("We should wait before we spawn a new ship");
			waitBeforeNewSpawn();
		}
	}

	void setCannonLevel()
	{
		//Debug.Log("We are going to set the level of  the cannons");
		float relativeOriginPosition = Vector3.Distance (playerPoint.transform.position, origin.transform.position); //Distance between player and where the boss spawns
		//Debug.Log("Distance between player and origin is set: " + relativeOriginPosition);
		float temp = Mathf.Floor(relativeOriginPosition * 0.1f); //10% of the distance is the new temp;
		//Debug.Log("the 10% of distance is set: " + temp);
		int upgradedWapons = 0;
		int ranNum;

		for(int i = 0; i < 6; i++)
		{
			//Debug.Log("We are running the loop 6 times");
			ranNum = Random.Range (0, 101); //Generates a number from 0 to 100
			//Debug.Log("Random number between 0 to 100: " + ranNum);
			if(ranNum < temp) //The roll is low enough! We got a lvl 2 gun now
			{
				//Debug.Log("ranNum is smaller than temp");
				ranNum = Random.Range(0, 101);
				//Debug.Log("New random number to check if we should create a lvl3 cannon: " + ranNum);
				if(ranNum < temp*0.5)
				{
					//Debug.Log("Create a lvl 3 cannon");
					cannonLevel[i] = 3;
					//Debug.Log("Success!");
				}
				else
				{
					//Debug.Log("Create a lvl 2 cannon");
					cannonLevel[i] = 2;
					//Debug.Log("Success!");
				}
				upgradedWapons++;
				//Debug.Log("This ship has upgraded a total of " + upgradedWapons + " cannons");
				cannonUpgraded[i] = true;
				//Debug.Log("cannonUpgraded[" + i + "] is now " + cannonUpgraded[i]);
			}
			else
			{
				//Debug.Log("We should not upgrade the cannon");
				cannonLevel[i] = 1;
				//Debug.Log("cannonLevel[" + i + "] is 1");
			}
		}

		//Just add a bunch of tests like these to create definite upgraded guns criterias.
		//By that I mean you can add more tests to guarantee that a minimum ammount of
		//cannons are upgraded, and to what lvl.
		if(relativeOriginPosition > 200 && upgradedWapons < 2) //We need some guaranteed lvl2 weapons
		{
			//Debug.Log("The distance is greater than 200 and it has less than 2 upgraded cannons");
			while(upgradedWapons < 2) //There are too few upgraded cannons
			{
				//Debug.Log("Upgrade a new one");
				ranNum = Random.Range(0, 6); //Create a random number from 0 to 5, as this is the array length
				//Debug.Log("Random number between 0 and 5: " + ranNum);
				if(cannonUpgraded[ranNum] == false) //The cannon is not upgraded
				{
					//Debug.Log("Upgrades an un-upgraded cannon, cannonUpgraded[" + ranNum + "]: " + cannonUpgraded[ranNum]);
					cannonUpgraded[ranNum] = true; //It is now!
					//Debug.Log("Sets the cannon to true: " + cannonUpgraded[ranNum]);
					cannonLevel[ranNum] = 2; //Set it to lvl 2
					//Debug.Log("The cannon is now lvl 2: " + cannonLevel[ranNum]);
					upgradedWapons++; //Update ammount of upgraded weapons
				}
			}
		}
	}

	private Vector3 setPatrolPoint()
	{
		float tempPosX = Random.Range(playerPoint.transform.position.x, playerPoint.transform.position.x + 3000); //Random x position
		//Debug.Log("random x position: " + tempPosX);
		float tempPosZ = Random.Range(playerPoint.transform.position.z, playerPoint.transform.position.z + 3000); //Random z position
		//Debug.Log("random z position: " + tempPosZ);
		float posX;
		float posZ;

		//Creates a random variable from 1 to 10 (the last number is not included, aka 11).
		//Use this number to determine if the variable should be positive or negative, just
		//to create som variation in the spawnpositions of the AI.
		float ranRangeX = Random.Range(1, 11);
		//Debug.Log("random number between 1 to 10(ranRangeX): " + ranRangeX);
		if(ranRangeX > 5)
		{
			//Debug.Log("ranRangeX is greater than 5");
			posX = playerPoint.transform.position.x + tempPosX;
			//Debug.Log("posX = playerPoint.transform.position.x + tempPosX: " + posX);

		}
		else posX = playerPoint.transform.position.x - tempPosX;


		//Does the same to the Z position of the AI as we did with the X position just above this.
		float ranRangeZ = Random.Range(1, 11);
		//Debug.Log("random number between 1 to 10(ranRangeZ): " + ranRangeZ);
		if(ranRangeZ > 5)
		{
			//Debug.Log("ranRangeZ is greater than 5");
			posZ = playerPoint.transform.position.z + tempPosZ;
			//Debug.Log("posZ = playerPoint.transform.position.z + tempPosZ: " + posZ);
		}
		else posZ = playerPoint.transform.position.z - tempPosZ;
		//Debug.Log("return new Vector3(" + posX + ", 0, " + posZ);
		return new Vector3(posX, 0, posZ);

	}


	void spawnShip ()
	{
		setCannonLevel();
		patrolPoint = setPatrolPoint();
		//Debug.Log("patrolPoint: " + patrolPoint);
		//float relativePoint = Vector3.Distance (playerPoint.transform.position, origin.transform.position); //Distance between player and Origin
		float relativeBossPoint = Vector3.Distance (playerPoint.transform.position, bossSpawn.transform.position); //Distance between player and where the boss spawns
		//Debug.Log("rekativeBossPoint is the distance between player and bossSpawn: " + relativeBossPoint);
		if(relativeBossPoint > 100) //We are too far away from the boss, so we spawn a regular AI.
		{
			//Debug.Log("Distance is greater than 100");
			if(livingShips < 10)
			{
				//Debug.Log("There are not 10 living ships");
				livingShips++;
				//Debug.Log("There are, after this, " + livingShips + " alive");

				//Create random numbers between 100 and 200
				float tempPosX = Random.Range(100f, 200f); //Random x position
				//Debug.Log("random number between 100 to 199(tempPosX: " + tempPosX);
				float tempPosZ = Random.Range(100f, 200f); //andom z position
				//Debug.Log("random number between 100 to 199(tempPosZ: " + tempPosZ);
				float posX;
				float posZ;
				//Creates a random variable from 1 to 10 (the last number is not included, aka 11).
				//Use this number to determine if the variable should be positive or negative, just
				//to create som variation in the spawnpositions of the AI.
				float ranRangeX = Random.Range(1, 11);
				//Debug.Log("random number between 1 to 10(ranRangeX): " + ranRangeX);
				if(ranRangeX > 5)
				{
					//Debug.Log("The number is greater than 5");
					posX = tempPosX;
				}
				else posX = -tempPosX;

				//Does the same to the Z position of the AI as we did with the X position just above this.
				float ranRangeZ = Random.Range(1, 11);
				//Debug.Log("random number between 1 go 10(ranRangeZ): " + ranRangeZ);
				if(ranRangeZ > 5)
				{
					//Debug.Log("The number is greater than 5");
					posZ = tempPosZ;
				}
				else posZ = -tempPosZ;

				spawnPosition = new Vector3(posX+playerPoint.transform.position.x, 2, posZ+playerPoint.transform.position.z); //Sets the position of the AI relative to the player position
				//Debug.Log("Creates the spawnPosition of the Marine(spawnPosition): " + spawnPosition);

				for(int i = 0; i < marineShips.Length; i++)
				{
					//Debug.Log("Runs the loop equal to the ammount of living ships we want.");
					//Debug.Log("marineShips.Length: " + marineShips.Length + ", and we are on number " + i + " in the loop");
					if(availableIndes[i] == true)
					{		
						//Debug.Log("The index is available for spawn(availableIndes): " + availableIndes[i]);
						GameObject temp = (Instantiate(AI));
						//Debug.Log("We Instantiate the Marine!: " + temp);
						lastSpawn = i;
						//Debug.Log("Index " + i + " is the latest spawned ship");
						marineShips[i] = temp;
						//Debug.Log("marineships[" + i + "] = " + marineShips[i]);
						availableIndes[i] = false;
						//Debug.Log("Make the index unavailable: " + availableIndes[i]);
						marineShips[i].transform.position = spawnPosition;
						//Debug.Log("Change the position of the AI: " + marineShips[i].transform.position);

						float aiOriginDistance = Vector3.Distance (AI.transform.position, origin.transform.position); //Distance between player and Origin
						//Debug.Log("aiOriginDistance: " + aiOriginDistance);
						marineShips[i].gameObject.GetComponent<AIMaster>().aiHealth = Mathf.Floor(aiOriginDistance * 0.01f); //AI health is equal to the number that is 10% of the distance between it and origin
						//Debug.Log("AIMaster.aiHealth is set by taking Mathf.Floor(aiOriginDistance * 0.01: " + (Mathf.Floor(aiOriginDistance * 0.01f)));
						//Debug.Log("Actual health is: " + marineShips[i].gameObject.GetComponent<AIMaster>().aiHealth);
						if(marineShips[i].gameObject.GetComponent<AIMaster>().aiHealth < 20)
						{
							//Debug.Log("The health is lower than 20, so set it to 20");
							marineShips[i].gameObject.GetComponent<AIMaster>().aiHealth = 20;
							//Debug.Log("Health is now " + marineShips[i].gameObject.GetComponent<AIMaster>().aiHealth);
						}
						marineShips[i].gameObject.GetComponent<AIMaster>().arrayIndex = i;
						//Debug.Log("The index of the Marine is equal to the 'i' in the loop: " + i + " and thus the marine is: " + marineShips[i].gameObject.GetComponent<AIMaster>().arrayIndex);
						waitBeforeNewSpawn();
						return;
					}
				}
			}
		}

		else //We should spawn the boss
		{
			//Debug.Log("We should actually spawn the boss instead");
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

		waitBeforeNewSpawn();
	}
}

