using UnityEngine;
using System.Collections.Generic;

public class gravityPull : MonoBehaviour
{
	//Puts range of teh gravitationalfield
	public float range = 10f;
	//Makes a rigidbody reference
	Rigidbody ownRb;

	void Start()
	{
		//Stores this objects rigidbody in ownRb
		ownRb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		//Makes an array of colliders who holds all colliders which is inside the gravitationalfield.
		Collider[] cols = Physics.OverlapSphere(transform.position, range);
		//Makes a list which holds all the rigidbodies
		List<Rigidbody> rbs = new List<Rigidbody>();

		//Runs for every collider who is stored in the array "cols"
		foreach (Collider c in cols)
		{
			//Finner rigidbody til objektet som er inne i gravitasjonsfeltet.
			//Finds the rigidbody to the object which is inside the gravitationalfield 
			Rigidbody rb = c.attachedRigidbody;
			//Hvis objektet som er inne i gravitasjonsfeltet har en rigidbody,
			//ikke er rigidbody-en til objektet scriptet er koblet til 
			//eller at rigidboien allerede er lagt til i lista med rigidbodies,
			//så kjøres dette.
			//If the object which is inside the gravitationalfield has a rigidbody, is not the rigidbody to the object which holds this script and the rigidbody is not already added to the list this will run.
			if (rb != null && rb != ownRb && !rbs.Contains(rb))
			{
				//The new rigidbody will be added to the list
				rbs.Add(rb);
				//Regner ut avstanden mellom objektet med gravitasjon og det andre objektet.
				//Calculates the distance between the object with the gravity and the other object.
				Vector3 offset = transform.position - c.transform.position;
				//Regner ut gravitasjonskrefter og dytter objektet mot objektet med gravitasjon.
				//Calculates the gravitationalpowers and pulls that object towards the object with gravity.
				rb.AddForce(offset / offset.sqrMagnitude * ownRb.mass);
			}
		}
	}

	void OnDrawGizmos()
	{
		//Lager en Gizmos som viser gravitasjonsfeltet i scenen.
		//Makes a Gizmo which shows the gravitationalfield in the scene.
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}