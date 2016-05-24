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
	public bool[] availableIndes; //Bool used to check the availability in the marineShips array
	public bool stopFightTimer = false; //Bool used to deactivate the timer that starts auromatic fights
	public bool livingCargo = false; //True is a cargo ship is alive
	public bool trespassingWorldBorder = false; //True is the player is trespassing the world border

	public static Vector3 spawnPosition; //Where the AI should spawn

	public int livingShips = 0; //Number of living ships
	public float startFightTimer = 0; //If this reach a given number, start a fight

	// Use this for initialization
	void Start ()
	{
		spawn = this;

		marineShips = new GameObject[maxMarines]; //Sets the array length equal to the maximum ammount of marines we want in the game
		availableIndes = new bool[maxMarines]; //Does the same with this array

		for(int i = 0; i < marineShips.Length; i++) //Cleans out the arrays
		{
			marineShips[i] = null;
			availableIndes[i] = true;
		}
		waitBeforeNewSpawn(); //Start the marine spawn timer
		waitBeforeCargoSpawn(); //Start the cargo spawn timer
	}

	void Update()
	{
		startFightTimer += Time.deltaTime; //Update timer

		if(stopFightTimer == true) //A fight is not to start automatically
			startFightTimer = 0; //reset the timer
		
		if(startFightTimer > 45) //There are 45 seconds since last fight. Starte one. 
		{
			startFightTimer = 0; //Reset the timer
			float temp = 10000; //Create a value to test with, it needs to be large
			float aiPlayerDistance; //Distance between player and enemies
			int tempI = 100; //Holds the index of the enemy to fight
			for(int i = 0; i < marineShips.Length; i++) //Run through the entire array of marines
			{
				if(marineShips[i] != null) //Make sure a marine is alive in the current index
				{
					aiPlayerDistance = Vector3.Distance (playerPoint.transform.position, marineShips[i].transform.position); //Distance between player and marine
					if(aiPlayerDistance < temp) //The distance is lower than temp, i.e. the new closest enemy. 
					{
						temp = aiPlayerDistance; //Sett temp to the new value
						tempI = i; //Save the array index
					}
				}
			}
			if(tempI != 100) //An enemy was alive and is the one to fight the player
			{
				marineShips[tempI].GetComponent<AIMaster>().deaktivatePatroling(); //Deactivate that enemies patroling and engage the player
			}
		}
	}
	//After X (now 1) seconds the checkShipStatus function will run
	//This function is used to check weather or not to spawn a new ship
	//every X second.
	void waitBeforeNewSpawn () 
	{
		InvokeRepeating ("spawnShip", 1, 2); //Check if a new marine should spawn every second seconds
	}

	void waitBeforeCargoSpawn()
	{
		InvokeRepeating("spawnCargo", 1, 2); //Check if a cargo ship should spawn every second seconds
	}

	void setCannonLevel(bool cargo) //Generate cannonlevel
	{
		float relativeOriginPosition = Vector3.Distance (playerPoint.transform.position, origin.transform.position); //Distance between player and where the boss spawns
		float temp = Mathf.Floor(relativeOriginPosition * 0.02f); //2% of the distance is the new temp;
		int upgradedWapons = 0; //create variable holding number of upgraded weapons
		int ranNum;
		if(cargo == false) //This is a marine
		{
			for(int i = 0; i < 6; i++) //Check every cannon
			{
				ranNum = Random.Range (0, 101); //Generates a number from 0 to 100
				if(ranNum < temp) //The roll is low enough! We got a lvl 2 gun now
				{
					ranNum = Random.Range(0, 101); //Generate random number that can be anything from 0 to 100
					if(ranNum < temp*0.5) //If ranNum is lower than half of temp, we get a level 3 gun. This is just to make it harder to get a level 3 cannon
					{
						cannonLevel[i] = 3; //Cannon [i] is level 3
					}
					else
					{
						cannonLevel[i] = 2; //Cannon [i] is level 2
					}
					upgradedWapons++; //Increase amount of upgraded weapoons
				}
				else //Failed to upgrade
				{
					cannonLevel[i] = 1; //Cannon [i] is level 1
				}
			}
		}

		else //Upgrading cannons for the cargo ship
		{
			for(int i = 0; i < 2; i++) //Check both cannons
			{
				ranNum = Random.Range (0, 101); //Generates a number from 0 to 100
				if(ranNum < temp) //The roll is low enough! We got a lvl 2 gun now
				{
					ranNum = Random.Range(0, 101); //Generate random number that can be anything from 0 to 100
					if(ranNum < temp*0.5) //If ranNum is lower than half of temp, we get a level 3 gun. This is just to make it harder to get a level 3 cannon
					{
						cannonLevel[i] = 3; //Cannon [i] is level 3
					}
					else
					{
						cannonLevel[i] = 2; //Cannon [i] is level 2
					}
					upgradedWapons++; //Increase amount of upgraded weapoons
				}
				else //Failed to upgrade
				{
					cannonLevel[i] = 1; //Cannon [i] is level 1
				}
			}
		}
	}

	private Vector3 setPatrolPoint() //Generate a point in the world which the AI will drive towards
	{
		float tempPosX = Random.Range(playerPoint.transform.position.x, playerPoint.transform.position.x + 3000); //Random x position
		float tempPosZ = Random.Range(playerPoint.transform.position.z, playerPoint.transform.position.z + 3000); //Random z position
		float posX;
		float posZ;

		//Creates a random variable from 1 to 10 (the last number is not included).
		//Use this number to determine if the variable should be positive or negative, just
		//to create som variation in the spawnpositions of the AI.
		float ranRangeX = Random.Range(1, 11);
		if(ranRangeX > 5) //Position is positive
		{
			posX = playerPoint.transform.position.x + tempPosX;

		}
		else posX = playerPoint.transform.position.x - tempPosX; //Position is negative


		//Does the same to the Z position of the AI as we did with the X position just above this.
		float ranRangeZ = Random.Range(1, 11);
		if(ranRangeZ > 5)  //Position is positive
		{
			posZ = playerPoint.transform.position.z + tempPosZ;
		}
		else posZ = playerPoint.transform.position.z - tempPosZ;  //Position is negative
		return new Vector3(posX, 0, posZ); //Return the position

	}

	private Vector3 marineSpawnpoint() //Generate a position for the marine to spawn
	{
		bool foundSpawn = false; //True when a valid position is found
		Vector3 testerSpawnPos = new Vector3(0, 0, 0); //The vector we are going to return

		while(foundSpawn == false) //Has not found a spawn yet
		{
			//Create random numbers between 100 and 200
			float tempPosX = Random.Range(200f, 250f); //Random x position
			float tempPosZ = Random.Range(200f, 250f); //andom z position
			float posX;
			float posZ;
			//Creates a random variable from 1 to 10 (the last number is not included, aka 11).
			//Use this number to determine if the variable should be positive or negative, just
			//to create som variation in the spawnpositions of the AI.
			float ranRangeX = Random.Range(1, 11);
			if(ranRangeX > 5) //Position is positive
			{
				posX = tempPosX;
			}
			else posX = -tempPosX;  //Position is negative

			//Does the same to the Z position of the AI as we did with the X position just above this.
			float ranRangeZ = Random.Range(1, 11);
			if(ranRangeZ > 5) //Position is positive
			{
				posZ = tempPosZ;
			}
			else posZ = -tempPosZ;  //Position is negative

			testerSpawnPos = new Vector3 (posX+playerPoint.transform.position.x, 2, posZ+playerPoint.transform.position.z); //Save the position

			Collider[] colliders = Physics.OverlapSphere(testerSpawnPos, 10); //Test if any other objects are within a 10 meter radious around the newly created spawnpoint
			if(colliders.Length == 0) //There are not any other objects there!
				foundSpawn = true; //Sets the position of the Marine relative to the player position
		}

		return testerSpawnPos; //Return the position
	}

	private Vector3 cargoSpawnpoint() //Generate a position for the cargo to spawn
	{
		Vector3 playerPosition = playerPoint.transform.position; //Position of the player
		Vector3 playerDirection = playerPoint.transform.forward; //The players forward vector
		float spawnDistance = 200; //Spawn 200 meters in front of the player
		Vector3 spawnPos = playerPosition + playerDirection * spawnDistance; //Make a position directly in front of the player

		Collider[] colliders = Physics.OverlapSphere(spawnPos, 10); //Test if any other objects are within a 10 meter radious around the newly created spawnpoint
		if(colliders.Length == 0) //There are not any other objects there!
			return spawnPos; //Return the position
		else //There was something else there!
			return marineSpawnpoint(); //Generate a random point, just as with the marine
	}


	void spawnCargo() //Instantiate a cargo ship
	{
		if(livingCargo == false && GameControl.control.isFighting == false && trespassingWorldBorder == false && GameObject.Find("Boss(Clone)") == null) //Multiple tests to see if it is ok to spawn the cargo ship
		{
			livingCargo = true; //Set the variable to true to prevent multiple cargo ships at the same time
			setCannonLevel(true); //Generate cannon levels for the cargo ship

			Instantiate(Cargo, cargoSpawnpoint(), playerPoint.transform.rotation); //Instantiate the cannon at a generated position
			Cargo.GetComponent<AIMaster>().isCargo = true; //Tell the code that this is a cargo
			Cargo.GetComponent<AIPatroling>().target = setPatrolPoint(); //Generate patrol point
			float aiOriginDistance = Vector3.Distance (playerPoint.transform.position, origin.transform.position); //Distance between player and Origin
			Cargo.GetComponent<AIMaster>().aiHealth = Mathf.Floor(aiOriginDistance * 0.01f); //AI health is equal to the number that is 10% of the distance between it and origin
			if(Cargo.GetComponent<AIMaster>().aiHealth < 20) //If the health is under 20
			{
				Cargo.GetComponent<AIMaster>().aiHealth = 20; //Set the health to 20
			}
		}
	}


	void spawnShip () //Instantiate a marine or the boss
	{
		float relativeBossPoint = Vector3.Distance (playerPoint.transform.position, bossSpawn.transform.position); //Distance between player and where the boss spawns
		if(trespassingWorldBorder == false && GameControl.control.isFighting == false && GameObject.Find("Boss(Clone)") == null) //Multiple tests to see if it is ok to spawn a marine
		{
			if(relativeBossPoint > 150) //We are too far away from the boss, so we spawn a regular AI.
			{
				if(livingShips < maxMarines) //There are not too many marines alive
				{
					livingShips++; //Increase amount of living marines
					setCannonLevel(false); //Generate cannonlevels

					for(int i = 0; i < marineShips.Length; i++) //Search for available position in array
					{
						if(availableIndes[i] == true) //Found available index
						{		
							GameObject temp = (Instantiate(Marine)); //Instantiate marine
							temp.transform.position = marineSpawnpoint(); //Generate spawnpoint
							marineShips[i] = temp; //Send the marine into the array
							availableIndes[i] = false; //Make the index taken
							marineShips[i].GetComponent<AIPatroling>().target = setPatrolPoint(); //Generate patrol point
							float aiOriginDistance = Vector3.Distance (playerPoint.transform.position, origin.transform.position); //Distance between player and Origin
							marineShips[i].gameObject.GetComponent<AIMaster>().aiHealth = Mathf.Floor(aiOriginDistance * 0.01f); //AI health is equal to the number that is 10% of the distance between it and origin
							if(marineShips[i].gameObject.GetComponent<AIMaster>().aiHealth < 20) //Health is under 20
							{
								marineShips[i].gameObject.GetComponent<AIMaster>().aiHealth = 20; //Set health to 20
							}
							marineShips[i].gameObject.GetComponent<AIMaster>().arrayIndex = i; //Save this marines index in the array
							return; //End the loop
						}
					}
				}
			}
		}

		if(relativeBossPoint < 200 && GameObject.Find("Boss(Clone)") == null)//We should spawn the boss
		{
			Instantiate(Boss, bossSpawn.transform.position, bossSpawn.transform.rotation); //Instantiate the boss at the boss's spawn position
			Boss.GetComponent<AIMaster>().isBoss = true; //Tell the code that this is the boss
			Boss.transform.GetComponent<AIMaster>().aiHealth = 100; //Sets the health
			stopFightTimer = true; //Stop automatic fighting
			GameControl.control.isFighting = true; //Tell the code globally that this ship is fighting
		}
	}
}

