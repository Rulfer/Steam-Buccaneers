using UnityEngine;
using System.Collections;

public class gravityCs : MonoBehaviour {

	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//
//	void OnTriggerExit (Collider other) 
//	{ 
//		Debug.Log("Nå forlater vi sola 12111");
//		GameObject.Find ("Cylinder").GetComponent<movement> ().stopMovement ();
//	}
	void OnTriggerStay (Collider other) 
	{ 
		other.attachedRigidbody.GetComponent<ConstantForce> ().enabled = true;
		Vector3 direction = (other.attachedRigidbody.transform.position - transform.position);
		if (other.attachedRigidbody)
		{
			other.attachedRigidbody.GetComponent<ConstantForce>().force = direction;
		}
		
		//Debug.Log(other.attachedRigidbody.GetComponent<ConstantForce>().force);
	}
	
	void OnTriggerExit (Collider other) 
	{ 
		other.attachedRigidbody.GetComponent<ConstantForce>().force = Vector3.zero;
		other.attachedRigidbody.GetComponent<ConstantForce> ().enabled = false;
		other.attachedRigidbody.velocity = Vector3.zero;
		Debug.Log("Nå forlater vi sola");
		Debug.Log(other.attachedRigidbody.GetComponent<ConstantForce>().force);
	} 

}
