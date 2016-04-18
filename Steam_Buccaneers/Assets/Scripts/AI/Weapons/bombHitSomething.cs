using UnityEngine;
using System.Collections;
using EZCameraShake;
using UnityEngine.SceneManagement;


public class BombHitSomething : MonoBehaviour {
	private float radius = 20F;
	private float force = 10.0f;

	CameraShakeInstance shake;
	public GameObject explosion;

	public AudioClip[] clips;
	private AudioSource source;

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
		if(other.tag == "aiShip") //It hit the AI
		{
			if(other.transform.root.name == "Cargo(Clone)")
				other.transform.GetComponentInParent<AIMaster>().thisAIFlee();
			
			if(other.transform.root.name == "Boss(Clone)" && (other.GetComponentInParent<AIMaster>().aiHealth - 10) <= 0)
			{
				SceneManager.LoadScene("cog_screen");
			}
			other.transform.GetComponentInParent<AIMaster>().aiHealth -= 10; //Remove 10 health from the AI
			if(other.transform.GetComponentInParent<AIMaster>().aiHealth <= 0)
				other.transform.GetComponentInParent<AIMaster>().killAI();
			else if(other.GetComponentInParent<AIMaster>().aiHealth <= other.GetComponentInParent<AIMaster>().aiHealthMat3)
			{
				other.GetComponentInParent<AIMaster>().changeMat3();
				other.GetComponentInParent<AIMaster>().testFleeing();
			}
			else if(other.GetComponentInParent<AIMaster>().aiHealth <= other.GetComponentInParent<AIMaster>().aiHealthMat2)
				other.GetComponentInParent<AIMaster>().changeMat2();
		}
		if(other.tag == "canonball") //A ball hit this object
		{
			Destroy(other.gameObject); //destroyd the canonball
		}
		createExplotion(); //Initiate the explotion
	}

	void createExplotion()
	{
		Vector3 explotionPos = this.transform.position; //The position of this object. The origin of the explotion

		Collider[] colliders = Physics.OverlapSphere(explotionPos, radius);//An array containing every object that has hit the explotion

		foreach(Collider hit in colliders) //We will do the same check for every object in the array
		{
			Rigidbody rb = hit.GetComponent<Rigidbody>(); //rb holds the Rigidbody data for every object in the array
			if(hit.tag == "Player") //If we hit the player
			{
				PlayerMove2.hitBomb = true; //Disable movement
			}
			if(hit.tag == "aiShip") //If we hit the aiShip
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
					rb.AddExplosionForce(force, explotionPos, radius, 0, ForceMode.Impulse); //Adds explotions to the root object
					if(hit.tag == "aiShip" || hit.transform.root.name == "PlayerShip")
					{
						rb.mass = 5;
						rb.drag = 5;
						rb.angularDrag = 5;
					}
				}
			}

			else //The object has the rigidbody component (usually meaning this object and canonballs)
			{
				rb.AddExplosionForce(force, explotionPos, radius, 0, ForceMode.Impulse); //Adds explotions to the rigidbody
			}
		}

		Instantiate(explosion, this.transform.position, this.transform.rotation);
		source.clip = clips[Random.Range(0, 5)];
		source.Play();
		this.GetComponent<MeshCollider>().enabled = false;
		this.GetComponent<MeshRenderer>().enabled = false;
		Destroy(this.gameObject, source.clip.length); //Destroys this object
	}
}
