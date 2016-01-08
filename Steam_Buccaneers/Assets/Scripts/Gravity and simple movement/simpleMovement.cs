using UnityEngine;
using System.Collections;

public class simpleMovement : MonoBehaviour {

	public float speed;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		//Henter ut rigidbody fra objekt.
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//objekt for lagt til force etter hvilken knapp blir trykket.
		if (Input.GetKey(KeyCode.W)) {
			rb.AddForce (0.0f, 0.0f, -1.0f);
		}

		if (Input.GetKey (KeyCode.A)) {
			rb.AddForce (Vector3.right);
		}

		if (Input.GetKey (KeyCode.S)) {
			rb.AddForce (0.0f, 0.0f, 1.0f);
		}

		if (Input.GetKey (KeyCode.D)) {
			rb.AddForce (Vector3.left);
		}
	}
}
