using UnityEngine;
using System.Collections;
using EZCameraShake;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AIprojectile : MonoBehaviour {
	private CheatCodesScript cheats; //Creates a reference to the cheatcode script

	public float projectileSpeed = 175; //Speed of the cannonballs
	public int damageOutput; //Damage dealt by the cannonball
	private float distance; //Distance between the cannonball and the player
	private float aliveTimer = 0f; //How long a cannonball has been alive
	private float aliveDuration = 20f; //Max time a cannonball can be alive

	public GameObject explosion; //Explosion particle simulation
	private GameObject player; //Creates a reference to the player object

	public AudioClip[] hitSounds; //Array of audioclips
	private AudioSource source; //The audio source

	CameraShakeInstance shake; //Creates a reference to the camerashake script

	private Vector3 axisOfRotation; //Random rotation for the cannonball
	private float angularVelocity; //Random speed for the rotation

	private CombatAnimationController characterWindows; //Reference to the CombatAnimationController script

	// Use this for initialization
	void Start () 
	{
		characterWindows = GameObject.Find ("dialogue_elements").GetComponent<CombatAnimationController>();
		source = this.GetComponent<AudioSource>();
		player = GameObject.Find("PlayerShip");

		axisOfRotation = Random.onUnitSphere;
		angularVelocity = Random.Range (600, 750);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		distance = Vector3.Distance(transform.position, player.transform.position); //Distance between the cannonball and the player
		this.transform.Rotate(axisOfRotation, angularVelocity * Time.smoothDeltaTime); //Rotates the object
		if (distance >= 500) //The distance is greater than 500
		{
			Destroy(gameObject); //Delete the cannonball because it is no longer visible
		}
		if(aliveTimer < aliveDuration) //The cannonball has not been alive 20 seconds yet
			aliveTimer += Time.deltaTime; //Increase timer
		else //It has been alive too long
			Destroy(gameObject); //Destroy the cannonball
	}

	void OnTriggerEnter(Collider other) //The cannonball hit something
	{
		if (other.tag == "Player") //The cannonball hit the player
		{
			if(CheatCodesScript.godMode == false) //The player has not activated Godmode
			{
				source.clip = hitSounds[Random.Range(0, 3)]; //Play a random audioclip
				source.Play();

				GameControl.control.health -= damageOutput; //Damage the player
				other.GetComponentInChildren<ChangeMaterial> ().checkPlayerHealth(); //See if the player should change material
				if(GameControl.control.health <= 0) //If true, the player is dead
				{
					other.GetComponentInParent<DeadPlayer>().enabled = true; //Activate the DeadPlayer script
					other.GetComponentInParent<DeadPlayer>().killPlayer(); //Kill the player via DeadPlayer.cs
				}
				CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(1, 5, 0.10f, 0.8f); //This actually instantiates the camera shake. Do NOT remove this line of code. 

				Instantiate(explosion, this.transform.position, this.transform.rotation); //Instantiate the explosion simulation at this cannonballs position. 
				this.GetComponent<MeshFilter>().mesh = null; //Remove the mesh of this object to make it invisible
				Destroy(this.gameObject, source.clip.length); //Destroy this object when the audioclip is done playing
			}
		}

		if(other.tag == "aiShip") //The projectile hit an enemy
		{
			if(other.transform.root.name == "Cargo(Clone)") //It his a cargo ship
				other.transform.GetComponentInParent<AIMaster>().thisAIFlee(); //Make the cargo ship flee
			
			if(other.transform.root.name == "Boss(Clone)") //It hit the boss
			{
				if ((other.GetComponentInParent<AIMaster> ().aiHealth - damageOutput) <= 0) //The next cannonball is going to kill the boss
				{
					other.GetComponentInParent<BossTalking> ().enabled = true; //Start the last dialogue
					other.GetComponentInParent<BossTalking> ().findAllDialogElements(); //Opens all GUI elements needed for the dialogue 
					other.GetComponentInParent<BossTalking> ().dialogBoss (12); //Display correct player animation
					other.GetComponentInParent<BossTalking> ().nextButton.GetComponent<Button> ().onClick.AddListener (delegate{GameControl.control.ChangeScene("cog_screen");}); //The next time the "next button" is pressed, load the cog_screen scene
				}
			}

			if(other.GetComponentInParent<AIMaster>().isDead == false) //We make sure the projectile don't hit an already dead ship. 
			{
				if(SceneManager.GetActiveScene().name != "Tutorial") //Make sure this is not the tutorial scene
				{
					if(other.GetComponentInParent<AIMaster>().isBoss == true && GameControl.control.health > 0) //Boss can only loose health if player is alive
						other.GetComponentInParent<AIMaster>().aiHealth -= damageOutput; //Deal damage to the hit ai
					else if(other.GetComponentInParent<AIMaster>().isBoss == false)  //If it did not hit the boss, damage none the less
						other.GetComponentInParent<AIMaster>().aiHealth -= damageOutput; //Deal damage to the hit ai
				}
				else //This is in the tutorial scene
					other.GetComponentInParent<AIMaster>().aiHealth -= damageOutput; //Deal damage to the marine

				if (other.transform.GetComponentInParent<AIMaster> ().aiHealth <= 0) //If the marine has less than 0 health, kill it
					other.transform.GetComponentInParent<AIMaster> ().killAI (); //Kills the marine
				else if (other.GetComponentInParent<AIMaster> ().aiHealth <= other.GetComponentInParent<AIMaster> ().aiHealthMat3) //It should not die, but should change material
				{
					if (other.GetComponentInParent<AIMaster> ().usingMat3 != true) //Has not changed to mat3 yet
						characterWindows.setHappy ("Player"); //The player is happy the marine got damaged
					other.GetComponentInParent<AIMaster> ().changeMat3 (); //Change the material
				} 
				else if (other.GetComponentInParent<AIMaster> ().aiHealth <= other.GetComponentInParent<AIMaster> ().aiHealthMat2) //It should not die, but should change material
				{
					if (other.GetComponentInParent<AIMaster> ().usingMat2 != true) //Has not changed to mat2 yet
						characterWindows.setHappy ("Player"); //The player is happy the marine got damaged
					other.GetComponentInParent<AIMaster> ().changeMat2 (); //Change the material
				}

				if(other.transform.root.name == "Marine(Clone)" && other.GetComponentInParent<AIMaster>().aiHealth > 0) //Hit a marine, and the health of the marine is greater than 0
				{
					if(SceneManager.GetActiveScene().name != "Tutorial") //We are not in the tutorial scene
					{
						if(GameControl.control.isFighting == false) //A fight is not ongoing, so we engage this marine in a fight with the player
							other.transform.GetComponentInParent<AIMaster>().deaktivatePatroling(); //Start the fight
					}
				}

				characterWindows.setAngry ("Enemy"); //Make the enemy angry because it got hit
				Instantiate(explosion, this.transform.position, this.transform.rotation); //Instantiate a particle simulation where the cannonball is at this time
				this.GetComponent<MeshFilter>().mesh = null; //Remove the mesh of the cannonball, making it invisible
				source.clip = hitSounds[Random.Range(0, 3)]; //Play a random audioclip
				source.Play();
				Destroy(this.gameObject, source.clip.length); //Delete this cannonball when the audio is done playing
			}
			else //Hit an enemy that is dead
			{
				if(other.GetComponentInParent<AIMaster>().source.isPlaying == false) //The clip played when an enemy dies is no longer playing
				{
					Instantiate(explosion, this.transform.position, this.transform.rotation); //Instantiate a particle simulation at this position
					this.GetComponent<MeshFilter>().mesh = null; //Remove the mesh of the cannonball, making it invisible
					source.clip = hitSounds[Random.Range(0, 3)]; //Play a random audioclip
					source.Play();
					Destroy(this.gameObject, source.clip.length); //Delete this cannonball when the audio is done playing
				}
				else //The audioclip is still playing, so we don't want to have multiple tiny clips from the cannonballs too
				{
					Instantiate(explosion, this.transform.position, this.transform.rotation); //Instantiate a particle simulation at this position
					Destroy(this.gameObject); //Delete this object
				}
			}
		}

		if(other.tag == "shop" || other.tag == "Planet" || other.tag == "asteroid" || other.tag == "Moon") //The cannonball hit a hindering, not an enemy or player
		{
			source.clip = hitSounds[Random.Range(0, 3)]; //Play a random audioclip
			if(this.gameObject != null) //The cannonball is not deleted yet
				source.Play(); //Play the clip

			Instantiate(explosion, this.transform.position, this.transform.rotation); //Instantiate a particle simulation where the cannonball is at this time
			this.GetComponent<MeshFilter>().mesh = null; //Remove the mesh of the cannonball, making it invisible
			Destroy(this.gameObject, source.clip.length); //Delete this cannonball when the audio is done playing
		}
	}
		
}
