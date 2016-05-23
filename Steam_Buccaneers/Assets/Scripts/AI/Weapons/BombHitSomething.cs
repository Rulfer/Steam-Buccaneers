using UnityEngine;
using System.Collections;
using EZCameraShake;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BombHitSomething : MonoBehaviour {
	private float radius = 20F; //Explosion radius
	private float force = 10.0f; //Force applied from the explosion

	CameraShakeInstance shake; //Reference to the camera shake script
	public GameObject explosion; //Explosion particle simulation

	public AudioClip[] clips; //An array containing multiple audio clips
	private AudioSource source; //Auio source

	void Start()
	{
		source = this.GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) //The bomb hit something
	{
		if(other.tag == "Player") //It hit the player!
		{
			GameControl.control.health -= 10; //Remove 10 health from the player
			CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(2, 5, 0.10f, 0.8f); //This actually instantiates the camera shake. Do NOT remove this line of code. 
		}
		if(other.tag == "aiShip") //It hit an enemy
		{
			if(other.transform.root.name == "Cargo(Clone)") //The bomb hit a cargo ship
				other.transform.GetComponentInParent<AIMaster>().thisAIFlee(); //It will now flee

			if(other.transform.root.name != "Boss(Clone)") // We hit an enemy, but not the boss
			{
				other.transform.GetComponentInParent<AIMaster>().aiHealth -= 10; //Remove 10 health rom the enemy
				if(other.transform.GetComponentInParent<AIMaster>().aiHealth <= 0) //We are going to kill it if the health is 0 or less
					other.transform.GetComponentInParent<AIMaster>().killAI(); //Kil the enemy that hit the bomb
				else if(other.GetComponentInParent<AIMaster>().aiHealth <= other.GetComponentInParent<AIMaster>().aiHealthMat3) //The enemy did not die, but lost enough health to get a new material
				{
					other.GetComponentInParent<AIMaster>().changeMat3(); //Change the material
				}
				else if(other.GetComponentInParent<AIMaster>().aiHealth <= other.GetComponentInParent<AIMaster>().aiHealthMat2) //The enemy lost enough health to change to the second material
					other.GetComponentInParent<AIMaster>().changeMat2(); //Change the material
			}
		}
		if(other.tag == "canonball") //A ball hit this bomb
		{
			Destroy(other.gameObject); //destroyd the canonball
		}
		createExplotion(); //Initiate the explotion
	}

	void createExplotion()
	{
		Vector3 explosionPos = this.transform.position; //The position of this object. The origin of the explotion

		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);//An array containing every object that has hit the explosion

		foreach(Collider hit in colliders) //We will do the same check for every object in the array
		{
			if(hit.transform.root.name != "Boss(Clone)") //We don't want to apply force to the boss. 
			{
				Rigidbody rb = hit.GetComponent<Rigidbody>(); //rb holds the Rigidbody data for every object in the array
				if(hit.transform.root.name == "PlayerShip") //If we hit the player
				{
					PlayerMove.hitBomb = true; //Disable movement
				}
				if(hit.transform.root.name == "Marine(Clone)" || hit.transform.root.name == "Cargo(Clone)") //If we hit an enemy
				{
					hit.GetComponentInParent<AImove>().hitBomb = true; //Disable movement
				}
				if(rb == null) //The object has no rigidbody. Check if the root has
				{
					Transform test; //Creates a variable to hold the object
					test = hit.transform.root; //Sets the object equal to the objects root (aka the object with the Rigidbody)
					rb = test.GetComponent<Rigidbody>(); //Changes rb to be the rigidbody of the root object
					if(rb != null) //The parent got the rigidbody!
					{
						rb.AddExplosionForce(force, explosionPos, radius, 0, ForceMode.Impulse); //Adds explotions to the root object
						//We increase drag and mass of the player if it get hit by a bomb.
						//This is to decrease the bumpy feeling of the ship, and decrease 
						//the ammount of distance the player can move due to the impact. 
						if(hit.transform.root.name == "PlayerShip")
						{
							rb.mass = 5;
							rb.drag = 5;
							rb.angularDrag = 5;
						}
					}
				}

				else //The object has the rigidbody component (usually meaning this object and canonballs)
				{
					rb.AddExplosionForce(force, explosionPos, radius, 0, ForceMode.Impulse); //Adds explotions to the rigidbody
				}
			}
		}

		Instantiate(explosion, this.transform.position, this.transform.rotation); //Instantiate explosion simulation
		source.clip = clips[Random.Range(0, 5)]; //Play a random audioclip 
		source.Play();
		this.GetComponent<MeshCollider>().enabled = false; //Disable the collider
		this.GetComponent<MeshRenderer>().enabled = false; //Disable the mesh and make the bomb invisible
		Destroy(this.gameObject, source.clip.length); //Destroys this object once the clip is done playing
	}
}
