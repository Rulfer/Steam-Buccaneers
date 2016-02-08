using UnityEngine;
using System.Collections;

public class bombHitSomething : MonoBehaviour {
	private float radius = 20F;
	private float force = 10.0f;
	private Rigidbody rigi;

	void Start()
	{
		rigi = GetComponent<Rigidbody>(); //Get the rigidbody of THIS object
	}

	void OnTriggerEnter(Collider other) //The bomb hit something
	{
		if(other.tag == "Player") //It hit the player!
		{
			GameControl.control.health -= 10; //Remove 10 health from the player
		}
		if(other.tag == "aiShip") //It hit the AI
		{
			AIMaster.aiHealth -= 10; //Remove 10 health from the AI
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
				AImove.hitBomb = true; //Disable movement
			}
			if(rb == null) //The object has no rigidbody. Check if the root has
			{
				Transform test; //Creates a variable to hold the object
				test = hit.transform.root; //Sets the object equal to the objects root (aka the object with the Rigidbody)
				rb = test.GetComponent<Rigidbody>(); //Changes rb to be the rigidbody of the root object
				if(rb != null) //The parent got the rigidbody!
				{
					rb.AddExplosionForce(force, explotionPos, radius, 0, ForceMode.Impulse); //Adds explotions to the root object
				}
			}

			else //The object has the rigidbody component (usually meaning this object and canonballs)
			{
				rb.AddExplosionForce(force, explotionPos, radius, 0, ForceMode.Impulse); //Adds explotions to the rigidbody
			}
		}

		Destroy(this.gameObject); //Destroys this object
	}
}
