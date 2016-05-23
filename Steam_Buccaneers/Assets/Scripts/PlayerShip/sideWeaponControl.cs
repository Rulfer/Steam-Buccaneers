using UnityEngine;
using System.Collections;

public class SideWeaponControl : MonoBehaviour 
{
	//public AudioSource pangPang;
	public GameObject cannonball; // the game object that is the cannonball the player will fire
	public GameObject[] leftCannons; // array holding all the guns on the left side of the ship
	public GameObject[] rightCannons; // array holding all the guns on the right side of the ship
	public float fireRate; // how fast the player can fire
	public float fireDelayLeft; // how long until the player can fire the left side of the ship
	public float fireDelayRight; // how long until the player can fire the right side of the ship

	public GameObject cannonball1; // cannonball for lvl 1 cannon
	public GameObject cannonball2; // cannonball for lvl 2 cannon
	public GameObject cannonball3; // cannonball for lvl 3 cannon

	public AudioClip[] cannonFireSounds; // array holding the firing sounds
	public AudioSource sourceLeft; // where to play the source for the left cannons
	public AudioSource sourceRight; // where to play the source for the right cannons


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		// if the player is pressing the key to fire the left side of the ship, as well as if the time between last shot is greater
		// than the delay to fire, and is not dead
		if (Input.GetKey (KeyCode.Q) && Time.time > fireDelayLeft && GameControl.control.health > 0)
		{
			//the delay for the next time the player can fire is set to this current time, plus the rate of how fast the player can fire
			fireDelayLeft = Time.time + fireRate;
			int tempSound = Random.Range(0, 3); // finding one of three fire sounds
			sourceLeft.clip = cannonFireSounds[tempSound];
			sourceLeft.Play(); // play the sound of the cannon firing

			// firing the cannons on the left side
			for (int i = 0; i <= 2; i++)
			{
				//for the tutorial, checking if the player has fired the cannons in the tutorial
				if (GameObject.Find ("TutorialControl") != null && GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().dialogNumber == 13)
				{
					tutorialCheckIfFire (0);
				}

				//checking if the cannon is upgraded
				// spawns the cannonball, and fires it at the speed of the ship, 
				//in addition to the speed the projectile should have, and to the left side
				if(GameControl.control.canonUpgrades[i] == 1)
				{
					GameObject test = Instantiate (cannonball1, leftCannons[i].transform.position, leftCannons[i].transform.rotation) as GameObject;
					test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * -this.transform.right));
				}
				else if(GameControl.control.canonUpgrades[i] == 2)
				{
					GameObject test = Instantiate (cannonball2, leftCannons[i].transform.position, leftCannons[i].transform.rotation) as GameObject;
					test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * -this.transform.right));
				}
				else if(GameControl.control.canonUpgrades[i] == 3)
				{
					GameObject test = Instantiate (cannonball3, leftCannons[i].transform.position, leftCannons[i].transform.rotation) as GameObject;
					test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * -this.transform.right));
				}
			}
		}
		// if the player is pressing the key to fire the right side of the ship, as well as if the time between last shot is greater
		// than the delay to fire, and is not dead
		if (Input.GetKey (KeyCode.E) && Time.time > fireDelayRight && GameControl.control.health > 0) 
		{
			//the delay for the next time the player can fire is set to this current time, plus the rate of how fast the player can fire
			fireDelayRight = Time.time + fireRate;

			int tempSound = Random.Range(0, 3);
			sourceRight.clip = cannonFireSounds[tempSound];
			sourceRight.Play();

			//for the tutorial, checking if the player has fired the cannons in the tutorial
			if (GameObject.Find ("TutorialControl") != null)
			{
				tutorialCheckIfFire (1);
			}

			//firing the three cannons on the right side of the ship
			for (int i = 0; i <= 2; i++)
			{
				//checking if the cannon is upgraded
				// spawns the cannonball, and fires it at the speed of the ship, 
				//in addition to the speed the projectile should have, and to the right side
				if(GameControl.control.canonUpgrades[i+3] == 1)
				{
					GameObject test = Instantiate (cannonball1, rightCannons[i].transform.position, transform.rotation) as GameObject;
					test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * this.transform.right));
				}
				else if(GameControl.control.canonUpgrades[i+3] == 2)
				{
					GameObject test = Instantiate (cannonball2, rightCannons[i].transform.position, transform.rotation) as GameObject;
					test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * this.transform.right));
				}
				else if(GameControl.control.canonUpgrades[i+3] == 3)
				{
					GameObject test = Instantiate (cannonball3, rightCannons[i].transform.position, transform.rotation) as GameObject;
					test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * this.transform.right));
				}	
			}
		}	
	}

	private void tutorialCheckIfFire(int nr)
	{
		if (GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().qeCheck [nr] != true)
		{
			GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().qeCheck [nr] = true;
			GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().checkArray (GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().qeCheck);
		}
	}
}
