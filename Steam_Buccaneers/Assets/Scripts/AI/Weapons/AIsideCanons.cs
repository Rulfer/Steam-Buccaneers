using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AIsideCanons : MonoBehaviour {

	public static AIsideCanons canons; //Reference to this script
	public GameObject cannonball1; //Level 1 cannonball
	public GameObject cannonball2; //Level 2 cannonball
	public GameObject cannonball3; //Level 3 cannonball 
	public GameObject[] leftCannons; //Array containing all cannons on the left side of the ship
	public GameObject[] rightCannons; //Array containing all cannons on the right side of the ship
	public GameObject[] allCannons; //All the sidecannons on a ship
	private int[] cannonLevel = new int[6]; //Stores each cannons level

	public float fireRate; //How often the sidecannons can shoot
	public float fireDelayLeft; //How long the left sidecannons has to wait before they can shoot again
	public float fireDelayRight; //How long the right sidecannons has to wait before they can shoot again

	public Mesh mesh1; //Mesh for the level 1 cannons
	public Mesh mesh2; //Mesh for the level 2 cannons
	public Mesh mesh3; //Mesh for the level 3 cannons

	private Vector3 right; //Raycast for the right cannons. If they see the player the code will try to fire the right sidecannons
	private Vector3 left; //Raycast for the left cannons. If they see the player the code will try to fire the left sidecannons

	public bool fireLeft = false; //Used to activate the code that initiates the cannonballs
	public bool fireRight = false; //Used to activate the code that initiates the cannonballs

	private int detectDistance = 100; //Length of the raycasts

	public AudioClip[] cannonFireSounds; //Array containing multiple audioclips
	public AudioSource sourceLeft; //Audiosource for the left cannons
	public AudioSource sourceRight; //Audiosource for the right cannons

	// Use this for initialization
	void Start () {
		canons = this;
		if(SceneManager.GetActiveScene().name != "Tutorial") //We are not in the tutorial scene
		{
			if(this.transform.root.name == "Marine(Clone)") //This is a marine
			{
				for(int i = 0; i < 6; i++) //Loop 6 times, one for each cannon
				{
					if(SpawnAI.cannonLevel[i] == 1) //Check if the index in cannonLevel[i] is level 1
					{
						allCannons[i].GetComponent<MeshFilter>().mesh = mesh1; //Change the mesh
						cannonLevel[i] = 1; //Save the level
						this.GetComponentInParent<AIMaster>().cannonLevelOne++; //Increase the counter to use it later for scrap value
					}
					else if(SpawnAI.cannonLevel[i] == 2) //Check if the index in cannonLevel[i] is level 2
					{
						allCannons[i].GetComponent<MeshFilter>().mesh = mesh2; //Change the mesh to look like a upgraded cannon
						cannonLevel[i] = 2; //Save the level
						this.GetComponentInParent<AIMaster>().cannonLevelTwo++; //Increase the counter to use it later for scrap value
					}
					else if(SpawnAI.cannonLevel[i] == 3) //Check if the index in cannonLevel[i] is level 3
					{
						allCannons[i].GetComponent<MeshFilter>().mesh = mesh3; //Change the mesh to look like a upgraded cannon
						cannonLevel[i] = 3; //Save the level
						this.GetComponentInParent<AIMaster>().cannonLevelThree++; //Increase the counter to use it later for scrap value
					}
				}
			}

			else if(this.transform.root.name == "Cargo(Clone)") //This is the cargo ship
			{
				for(int i = 0; i < 2; i++) //Run the loop 2 times, one for each cannon
				{
					if(SpawnAI.cannonLevel[i] == 1)
					{
						allCannons[i].GetComponent<MeshFilter>().mesh = mesh1; //Change the mesh
						cannonLevel[i] = 1; //Save the level
						this.GetComponentInParent<AIMaster>().cannonLevelOne++; //Increase the counter to use it later for scrap value
					}
					else if(SpawnAI.cannonLevel[i] == 2)
					{
						allCannons[i].GetComponent<MeshFilter>().mesh = mesh2; //Change the mesh to look like a upgraded cannon
						cannonLevel[i] = 2; //Save the level
						this.GetComponentInParent<AIMaster>().cannonLevelTwo++; //Increase the counter to use it later for scrap value
					}
					else if(SpawnAI.cannonLevel[i] == 3)
					{
						allCannons[i].GetComponent<MeshFilter>().mesh = mesh3; //Change the mesh to look like a upgraded cannon
						cannonLevel[i] = 3; //Save the level
						this.GetComponentInParent<AIMaster>().cannonLevelThree++; //Increase the counter to use it later for scrap value
					}
				}
			}
		}
		else //This is the tutorial, so we want all cannons to be level 1
		{
			for(int i = 0; i < 6; i++) //Loop 6 times, one for each cannon
			{
				allCannons[i].GetComponent<MeshFilter>().mesh = mesh1; //Change the mesh
				cannonLevel[i] = 1; //Save the level
				this.GetComponentInParent<AIMaster>().cannonLevelOne++; //Increase the counter to use it later for scrap value
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		right = this.transform.TransformDirection(Vector3.right); //Used for right raycasting
		left = this.transform.TransformDirection(Vector3.left); //Used for left raycasting

		Debug.DrawRay(this.transform.position, right * detectDistance, Color.green);
		Debug.DrawRay(this.transform.position, left * detectDistance, Color.blue);

		checkGunPosition(); //Updates raycasts and updates fireLeft and fireRight bools

		if (fireLeft == true && Time.time > fireDelayLeft) //The left raycast saw something, and the cannons have waited long enough to be able to shoot again
		{
			fireDelayLeft = Time.time + fireRate; //Update fireDelay with time and firerate to generate new cooldown time

			sourceLeft.clip = cannonFireSounds[Random.Range(0, 3)]; //Plays a random audioclip


			if(this.transform.root.name == "Boss(Clone)") //This is the boss object
			{
				for(int i = 0; i < 6; i++) //Fire every cannon on the left side
				{
					GameObject test = Instantiate (cannonball3, leftCannons[i].transform.position, leftCannons[i].transform.rotation) as GameObject; //Instantiate a cannonball at the cannons position and rotation
					test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * -this.transform.right)); //Add force to the cannonball
				}
				sourceLeft.Play();
			}

			else if(this.transform.root.name == "Marine(Clone)") //This is a marine                                                                                                                                                  
			{
				for(int i = 0; i < leftCannons.Length; i++) //Fire all three cannons on the left side
				{
					if(cannonLevel[i] == 1) //If the cannon in the current index is level 1
					{
						GameObject test = Instantiate (cannonball1, leftCannons[i].transform.position, leftCannons[i].transform.rotation) as GameObject; //Instantiate a level 1 cannon at the cannons position and rotation
						test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * -this.transform.right)); //Add force to the cannonball
					}
					else if(cannonLevel[i] == 2) //If the cannon in the current index is level 2
					{
						GameObject test = Instantiate (cannonball2, leftCannons[i].transform.position, leftCannons[i].transform.rotation) as GameObject; //Instantiate a level 2 cannon at the cannons position and rotation
						test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * -this.transform.right)); //Add force to the cannonball
					}
					else if(cannonLevel[i] == 3) //If the cannon in the current index is level 3
					{
						GameObject test = Instantiate (cannonball3, leftCannons[i].transform.position, leftCannons[i].transform.rotation) as GameObject; //Instantiate a level 3 cannon at the cannons position and rotation
						test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * -this.transform.right)); //Add force to the cannonball
					}
				}
				sourceLeft.Play();
			}
			else if(this.transform.root.name == "Cargo(Clone)") //This is a cargo ship
			{
				if(this.GetComponentInParent<AImove>().isFleeing == true) //We only want the cargo ship to be able to shoot while it is fleeing from the player
				{
					if(cannonLevel[1] == 1) //If the left cannon is level 1
					{
						GameObject test = Instantiate (cannonball1, leftCannons[0].transform.position, transform.rotation) as GameObject; //Instantiate a level 1 cannon at the cannons position and rotation
						test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * -this.transform.right)); //Add force to the cannonball
					}
					else if(cannonLevel[1] == 2) //If the left cannon is level 2
					{
						GameObject test = Instantiate (cannonball2, leftCannons[0].transform.position, transform.rotation) as GameObject; //Instantiate a level 2 cannon at the cannons position and rotation
						test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * -this.transform.right)); //Add force to the cannonball
					}
					else if(cannonLevel[1] == 3) //If the left cannon is level 3
					{
						GameObject test = Instantiate (cannonball3, leftCannons[0].transform.position, transform.rotation) as GameObject; //Instantiate a level 3 cannon at the cannons position and rotation
						test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * -this.transform.right)); //Add force to the cannonball
					}
					sourceLeft.Play();
				}
			}
		}

		if (fireRight == true && Time.time > fireDelayRight) //The right raycast saw something, and the cannons no longer has a cooldown
		{				
			fireDelayRight = Time.time + fireRate; //Update fireDelay with time and firerate to generate new cooldown time

			sourceRight.clip = cannonFireSounds[Random.Range(0, 3)]; //Play a random audioclip

			if(this.transform.root.name == "Boss(Clone)") //This is the boss
			{
				for(int i = 0; i < 6; i++) //Fire all cannons on the right side
				{
					GameObject test = Instantiate (cannonball3, rightCannons[i].transform.position, transform.rotation) as GameObject; //Instantiate a level 3 cannon at the cannons position and rotation
					test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * this.transform.right)); //Add force to the cannonball
				}
				sourceRight.Play();
			}

			else if(this.transform.root.name == "Marine(Clone)") //This is a marine
			{
				for(int i = 0; i < rightCannons.Length; i++) //Fire all cannons on the right side
				{
					if(cannonLevel[i+3] == 1) //If the left cannon is level 1
					{
						GameObject test = Instantiate (cannonball1, rightCannons[i].transform.position, transform.rotation) as GameObject; //Instantiate a level 1 cannon at the cannons position and rotation
						test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * this.transform.right)); //Add force to the cannonball
					}
					else if(cannonLevel[i+3] == 2) //If the left cannon is level 2
					{
						GameObject test = Instantiate (cannonball2, rightCannons[i].transform.position, transform.rotation) as GameObject; //Instantiate a level 2 cannon at the cannons position and rotation
						test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * this.transform.right)); //Add force to the cannonball
					}
					else if(cannonLevel[i+3] == 3) //If the left cannon is level 3
					{
						GameObject test = Instantiate (cannonball3, rightCannons[i].transform.position, transform.rotation) as GameObject; //Instantiate a level 3 cannon at the cannons position and rotation
						test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * this.transform.right)); //Add force to the cannonball
					}
				}
				sourceRight.Play();
			}
			else if(this.transform.root.name == "Cargo(Clone)") //This is a cargo ship
			{
				if(this.GetComponentInParent<AImove>().isFleeing == true) //We know the Cargo ship is fleeing
				{
					if(cannonLevel[1] == 1) //If the left cannon is level 1
					{
						GameObject test = Instantiate (cannonball1, rightCannons[0].transform.position, transform.rotation) as GameObject; //Instantiate a level 1 cannon at the cannons position and rotation
						test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * this.transform.right)); //Add force to the cannonball
					}
					else if(cannonLevel[1] == 2) //If the left cannon is level 2
					{
						GameObject test = Instantiate (cannonball2, rightCannons[0].transform.position, transform.rotation) as GameObject; //Instantiate a level 2 cannon at the cannons position and rotation
						test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * this.transform.right)); //Add force to the cannonball
					}
					else if(cannonLevel[1] == 3) //If the left cannon is level 3
					{
						GameObject test = Instantiate (cannonball3, rightCannons[0].transform.position, transform.rotation) as GameObject; //Instantiate a level 3 cannon at the cannons position and rotation
						test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * this.transform.right)); //Add force to the cannonball
					}
					sourceRight.Play();
				}
			}
		}
	}

	private void checkGunPosition()
	{
		if(this.transform.root.name != "Boss(Clone)")
		{
			RaycastHit objectHit;

			if(Physics.Raycast(this.transform.position, left, out objectHit, detectDistance)) //Raycast hit something
			{
				if(objectHit.transform.root.name == "PlayerShip")//Hit the player
				{
					fireLeft = true;//The AI can now fire
				}
				else //Hit something else than the player
				{
					fireLeft = false; //The AI cant fire anyway
				}
			}
			else //Hit nothing
			{
				fireLeft = false; //The AI cant fire
			}

			if(Physics.Raycast(this.transform.position, right, out objectHit, detectDistance))//Raycast hit something
			{
				if(objectHit.transform.root.name == "PlayerShip")//Hit the player
				{
					fireRight = true;//The AI can now fire
				}

				else //Hit something else than the player
				{
					fireRight = false; //The AI cant fire anyway
				}
			}
			else//Hit nothing
			{
				fireRight = false;//The AI cant fire
			}
		}
	}
}