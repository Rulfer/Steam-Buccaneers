using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AIMaster : MonoBehaviour 
{
	public GameObject[] scrap; //Array holding all scraps in the world
	public GameObject aiModelObject; //Mesh renderer of the object
	public GameObject boom; //The bomb to the boss
	public GameObject kill; //A parent who's children are to be disabled when an enemy dies
	private GameObject[] bombs; //Array used to find all bombs placed by the boss
	private GameObject playerPoint; //A reference to the player object

	public Material mat2; //The material for a damaged enemy
	public Material mat3; //The material for a very damaged enemy

	public float aiHealthMat2; //Float used to see if mat2 should be activated
	public float aiHealthMat3; //Float used to see if mat3 should be activated
	public float aiHealth = 20; //Set the health to 20. This prevent the enemy to die at spawn due to having 0 health
	public float detectDistance; //Stores the distance between the player and the enemy
	public float aiRadar = 200; //Detects the player when detectDistance is lower than aiRadar

	public bool testedFleeing = false; //Used to test if the enemy should flee
	public bool detectedPlayer = false; //Tells the code if the enemy has detected the player or not
	public bool isBoss = false; //True if this enemy is the boss
	public bool isCargo = false; //True is this enemy is a cargo ship
	public bool usingMat2 = false; //True is mat2 has already been applied once
	public bool usingMat3 = false; //True is mat3 has already been applied once
	public bool isFighting = false; //True if this enemy is engaged in combat with the player

	public int arrayIndex; //Store this enemies index in the "marineShips" array in SpawnAI.cs
	public int cannonLevelOne = 0; //Amount of level 1 cannons
	public int cannonLevelTwo = 0; //Amount of level 2 cannons
	public int cannonLevelThree = 0; //Amount of level 3 cannons
	private int ranNum; //Stores a random number

	public AudioClip clip; //Audio clip played when the enemy dies
	public AudioSource source; //The audio source of this object
	public bool isDead = false; //Is true when the enemy dies

	void Start () 
	{
		playerPoint = GameObject.FindGameObjectWithTag ("Player"); //As the player is a prefab, I had to add it to the variable this way
		aiHealthMat2= aiHealth * 0.66f; //Change to mat2 when health is down to 66%
		aiHealthMat3 = aiHealth * 0.33f; //Change to mat3 when health is down to 33%
		source = this.GetComponent<AudioSource>();
	}
	
	void Update () 
	{
		if(isDead == false) //The enemy is alive
		{
			detectDistance = Vector3.Distance (playerPoint.transform.position, this.transform.position); //calculates the distance between the AI and the player

			//When a Marine or the Boss detects the player they gain increased movement speed to be able
			//to catch up with the player. When the distance between them is low enough
			//the speed of the enemy is set to be equal to that of the player.
			if(detectDistance < aiRadar - 100) 
			{
				if(isCargo == false) //Makes sure that this is not a cargo ship
					this.GetComponent<AImove>().force = PlayerMove.move.force;
			}

			if(isBoss == false) //This is not the boss
			{
				if(detectedPlayer == false) //Not detected the player yet
				{
					if(detectDistance < aiRadar) //The enemy is closer than the minimum required distance
					{
						if(SceneManager.GetActiveScene().name != "Tutorial") //This is not the Tutorial scene
						{
							if(GameControl.control.isFighting == false && isCargo == false) //No enemy in combat at this moment, and this is not a cargo ship
							{
								deaktivatePatroling(); //Deactivate the patroling and engage in combat
							}
							else if(GameControl.control.isFighting == true) //An enemy is fighting the player, so this enemy should flee from the combat
								thisAIFlee(); //Flee
						}
						else //We are in the tutorial scene, so start the combat
						{
							deaktivatePatroling(); //Deactivate the patroling and engage in combat
						}
					}
				}
				else //The enemy already spotted the player
				{
					if(SceneManager.GetActiveScene().name != "Tutorial") //We are not in the tutorial scene
					{
						if(GameControl.control.isFighting == false && isCargo == false) //There is currently not a fight, and this is not the cargo ship
							reactivatePatroling(); //Deactivate the chase, reactivate patroling
						if(detectDistance > aiRadar + 50) //If the distance between this enemy and the player is big enough, restart patroling
							reactivatePatroling(); //Deactivate the chase, reactivate patroling
					}
				}
			}
			else if(isBoss == true && GameControl.control.talkedWithBoss == true) //This is the boss, and the player has talked with the boss
			{
				deaktivatePatroling(); //Attack the player
			}
				
			if(detectDistance > 350)//If the distance is greater than this number, delete this enemy
				killAbsentAI();
		}
	}

	public void reactivatePatroling() //Used to reactivate patroling
	{	
		detectedPlayer = false; //The enemy has no longer seen the player
		testedFleeing = false; //The enemy can try to flee again later
		if(isFighting == true) //If this enemy was fighting, we should deactivate this bool
		{
			isFighting = false; //This enemy is no longer in combat
			GameControl.control.isFighting = false; //There is no longer a combat
			SpawnAI.spawn.stopFightTimer = false; //Enable the timer that starts a fight automatically after 45 seconds
		}
		this.GetComponent<AIPatroling>().enabled = true; //Reactivate patroling
		this.GetComponent<AImove>().isPatroling = true; //Tell the code that it is indeed patroling
		this.GetComponent<AImove>().isFleeing = false; //Tell the code that this enemy is no longer fleeing
		this.GetComponent<AImove>().force = PlayerMove.move.force; //Sets the force equal to that of the player
	}

	public void deaktivatePatroling() //Used to engage the enemy into combat with the player
	{
		detectedPlayer = true; //The enemy has detected the player
		isFighting = true; //The enemy is fighting

		if(SceneManager.GetActiveScene().name != "Tutorial" && isCargo == false) //If this is not the tutorial scene, and this is not a cargo
		{
			SpawnAI.spawn.stopFightTimer = true; //Stop the timer that automatically starts a fight
			GameControl.control.isFighting = true; //Globally tell the program that a fight is ongoing
		}
		this.GetComponent<AIPatroling>().enabled = false; //Deactivate patroling
		this.GetComponent<AImove>().isPatroling = false; //Deactivate patroling 
		if(isCargo == false) //If this is not a cargo ship
			this.GetComponent<AImove>().force = PlayerMove.move.force + 200; //Increase speed to allow this enemy to catch up with the player
		else //This IS a cargo ship
		{
			testedFleeing = true; //The enemy is going to flee
			this.GetComponent<AImove>().isFleeing = true; //Activate fleeing
			this.GetComponent<AImove>().force = PlayerMove.move.force - 100; //Decrease speed to allow the player to easily catch up with the enemy
		}
	}

	public void thisAIFlee() //Activates the flee functionalities
	{
		detectedPlayer = true; //Has detected the player
		testedFleeing = true; //Has tested for fleeing

		if(isBoss == false && isCargo == false) //This is a marine enemy
		{
			SpawnAI.spawn.marineShips[arrayIndex].GetComponent<AIFlee>().enabled = true; //Activate fleeing script
			SpawnAI.spawn.marineShips[arrayIndex].GetComponent<AIPatroling>().enabled = false; //Disables patroling
			SpawnAI.spawn.marineShips[arrayIndex].GetComponent<AImove>().isPatroling = false; //Tells the script it is no longer patroling
			SpawnAI.spawn.marineShips[arrayIndex].GetComponent<AImove>().isFleeing = true; //Tells the script it is fleeing
		}
		else if(isCargo == true) //This is a cargo ship
		{
			this.GetComponent<AIFlee>().enabled = true; //Activate fleeing script
			this.GetComponent<AImove>().isPatroling = false; //Tells the script it is no longer patroling 
			this.GetComponent<AImove>().isFleeing = true; //Tells the script it is fleeing
			this.GetComponent<AIPatroling>().enabled = false; //Disables the patroling script
		}
	}

	private void killAbsentAI() //Used to kill enemies that dies due to being too far away from the player
	{
		if(SceneManager.GetActiveScene().name != "Tutorial") //We don't want the tutorial enemy to despawn due to distance
		{
			if(isCargo == false && isBoss == false) //This is a marine
			{
				SpawnAI.spawn.marineShips[arrayIndex] = null; //Removes this marine from the array of all marines
				SpawnAI.spawn.availableIndes[arrayIndex] = true; //Sets the index of this marine to be available
				SpawnAI.spawn.livingShips--; //Reduces the amount of living ships
			}
			else if(isCargo == true) //This is a cargo ship
				SpawnAI.spawn.livingCargo = false; //Tells the script there are no living cargo ships, so that a new one can spawn
			if(isBoss == true) //This is the boss
			{
				isFighting = false; //The boss is no longer fighting
				GameControl.control.isFighting = false; //Tells the program globally that the player is not in combat
				SpawnAI.spawn.stopFightTimer = false; //Reactivates the timer that automatically starts a fight
				bombs = GameObject.FindGameObjectsWithTag("bomb"); //Find all the bombs that the boss placed
				foreach(GameObject go in bombs) //Remove the bombs from the scene
					Destroy(go);
			}

			Instantiate(boom, this.transform.position, this.transform.rotation); //Spawn a particle simulation (explosion) at the position of this object
			boom.GetComponent<DeleteParticles>().killDuration = 3; //Delete the simulation after 3 seconds

			this.GetComponent<DeadAI>().enabled = true; //Enable the "DeadAI" script
			deactivateAI(); //Deactivate this enemy to make a dummy in the scene

			source.clip = clip; //Activate correct audio clip
			source.Play(); //Play the audio clip
			isDead = true; //Tells the code that his enemy is dead
		}
	}


	public void killAI() //Kill this enemy
	{
		int temp; //Stores the random value
		if(isCargo == false) //This is not a cargo ship
			temp = Random.Range(1, 7); //Generate a number that can be anything from 1 to 6
		else //This is the cargo ship
			temp = Random.Range(7, 15); //Generate a number that can be anything from 7 to 14

		if (scrap.Length != 0) //We did not forget to add the prefabs to the array
		{
			for (int i = 0; i < temp; i++) //Runs equal to the size of "temp"
			{
				GameObject tempScrap = Instantiate (scrap [Random.Range (0, 4)]); //Instantiate a random scrap from the scrapArray
				tempScrap.transform.position = this.transform.position; //Sets the position of the scrap
				tempScrap.GetComponent<ScrapRandomDirection>().setValue(cannonLevelOne, cannonLevelTwo, cannonLevelThree); //Sends the number of cannonlevels to the scrap to generate a value
			}
		}

		if(SceneManager.GetActiveScene().name != "Tutorial") //Makes sure we are not in the tutorial scene
		{
			if(isCargo == false && isBoss == false) //This is a marine
			{
				SpawnAI.spawn.marineShips[arrayIndex] = null; //Removes this marine from the array of all marines
				SpawnAI.spawn.availableIndes[arrayIndex] = true; //Sets the index of this marine to be available
				SpawnAI.spawn.livingShips--; //Reduces the amount of living ships
			}
			else if(isCargo == true) //This is a cargo ship
				SpawnAI.spawn.livingCargo = false; //Tells the script there are no living cargo ships, so that a new one can spawn
			if(isFighting == true)
			{			
				isFighting = false; //This enemy is no longer fighting
				GameControl.control.isFighting = false; //Tells the program globally that the player is not in combat
				SpawnAI.spawn.stopFightTimer = false; //Reactivates the timer that automatically starts a fight
			}
		}

		Instantiate(boom, this.transform.position, this.transform.rotation); //Spawn a particle simulation (explosion) at the position of this object
		boom.GetComponent<DeleteParticles>().killDuration = 3; //Delete the simulation after 3 seconds

		this.GetComponent<DeadAI>().enabled = true; //Enable the "DeadAI" script
		deactivateAI(); //Deactivate this enemy to make a dummy in the scene

		source.clip = clip; //Activate correct audio clip
		source.volume = 1;
		source.Play(); //Play the audio clip
		isDead = true; //Tells the code that his enemy is dead


		if (GameObject.Find ("TutorialControl") != null) //We are in the tutorial scene
		{
			GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().nextDialog (); //Continue in the dialogue
		}
	}

	public void changeMat3() //Change to material 3
	{
		if(usingMat3 == false) //Has yet to apply the material
		{
			aiModelObject.GetComponent<Renderer>().material = new Material(mat3); //Changes the material
			this.GetComponent<DamagedAI>().startFire(); //Starts the fire simulations
			usingMat3 = true; //Sets the bool to true
			this.GetComponent<AImove>().force = PlayerMove.move.force - 100; //Decrease the speed to simulate the damage even more
		}
	}	

	public void changeMat2()
	{
		if(usingMat2 == false) //Has yet to apply the material
		{
			aiModelObject.GetComponent<Renderer>().material = new Material(mat2); //Changes the material
			this.GetComponent<DamagedAI>().startSmoke(); //Starts the smoke simulations
			usingMat2 = true; //Sets the bool to true
			this.GetComponent<AImove>().force = PlayerMove.move.force - 100; //Decrease the speed to simulate the damage even more
		}
	}

	public void testFleeing() //Test to see if the enemy should flee from the combat
	{
		if(testedFleeing == false && isBoss == false) //See if it has already tested the fleeing, and that it is not the boss
		{
			int ranNum = Random.Range(1, 11); //Generate a random number from 1 to 10
			{
				if(ranNum > 9) //If the number is greater than 9, meaning 10, the enemy should flee
				{
					this.GetComponent<AImove>().isFleeing = true; //Sets fleeing to true
				}
				testedFleeing = true; //Has tested the fleeing
			}
		}
	}

	private void deactivateAI() //Deactivate this enemy to make a dummy in the scene
	{
		this.GetComponent<AImove>().enabled = false; //Disable the movement script
		this.GetComponent<AIavoid>().enabled = false; //Disable the avoidance script
		this.GetComponent<AIPatroling>().enabled = false; //Disable the patroling script
		this.GetComponent<DeadAI>().enabled = true; //Enables DeadAI.cs
		kill.gameObject.SetActive(false); //Disable irrelevant gameobjects on the enemy object 
	}

	void OnTriggerEnter(Collider other) //Check if the enemy hit somehting
	{
		if(other.tag == "Planet" || other.tag == "Moon") //Hit a planet/moon
		{
			if(isFighting == true) //If this enemy was in a fight, kill it the usual way
				killAI();
			else //If not, the minimalistic way
				killAbsentAI();
		}
	}
}
