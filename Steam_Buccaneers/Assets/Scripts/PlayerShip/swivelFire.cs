using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwivelFire : MonoBehaviour 
{

	public GameObject cannonball; // the cannonball the cannon fires
	public GameObject aimedCursor; // the cursor for when the player is aiming
	public float fireRate; // the rate of which the player can fire
	public float fireDelay; // the delay between each shot
	public AudioSource source; // the sound that is to play when firing
	public AudioClip[] clips; // array of sound clips to play
	bool fired = false; // bool checking if the cannon has been fired


	void Start () 
	{
		source = GetComponent<AudioSource> (); // 
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton("Fire2")) // if the player is holding down the right mouse button
		{
			Cursor.visible = false; // the default game cursor is set to false
			aimedCursor.GetComponent<RawImage>().enabled = true; // replacing the cursor with a diffrent cursor
			// if the player is pressing the left mouse button, and the current time is higher than the delay time
			// has ammo for the weapon and is not dead
			if (Input.GetButtonUp ("Fire1") && Time.time > fireDelay && GameControl.control.specialAmmo > 0 && GameControl.control.health > 0)
			{
				// the delay between next shot is set to the current game time + the rate of which the player can fire
				fireDelay = Time.time + fireRate;
				// spawns the cannonball
				GameObject test = Instantiate (cannonball, transform.position , transform.rotation) as GameObject;
				// adds the force of the player ship, as well as the projectile in the direction of the cannonbarrel
				test.GetComponent<Rigidbody> ().AddForce (this.GetComponentInParent<Rigidbody> ().velocity + (test.GetComponent<AIprojectile> ().projectileSpeed * this.transform.right));
				source.clip = clips[0]; // finds the correct firing sound
				source.Play(); // plays firing sound
				GameControl.control.specialAmmo -= 1; // loses ammunition for the cannon
				// updates the number of cannonballs left
				GameObject.Find("value_ammo_tab").GetComponent<Text>().text = GameControl.control.specialAmmo.ToString(); 
				fired = true; // the cannon has fired

				// checking if the player has fired in the tutorial
				if (GameObject.Find ("TutorialControl") != null && GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().mouse1Check == false && GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().dialogNumber == 18)
				{
					GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().mouse1Check = true;
					GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().nextButton.SetActive (true);
				}
			}
		}

		else
		{
			Cursor.visible = true;
			aimedCursor.GetComponent<RawImage>().enabled = false;
		}
		if(Input.GetButtonDown("Fire2"))
		{
			source.clip = clips[1];
			source.Play();
		}

		if(Time.time > fireDelay && fired == true)
		{
			source.clip = clips[2];
			source.Play();
			fired = false;
		}
			
	}
}
