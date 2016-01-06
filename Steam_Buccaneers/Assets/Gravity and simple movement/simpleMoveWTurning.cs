using UnityEngine;
using System.Collections;

public class simpleMoveWTurning : MonoBehaviour {

	public float speed;
	Rigidbody rb;

	void Start () {
		//Henter ut rigidbody
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		//W og S legger til force frem og bak, mens A og D svinger.
		if (Input.GetKey(KeyCode.W)) {
			rb.AddForce (-this.transform.forward);
		}

		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (0.0f, -5.0f, 0.0f);
		}

		if (Input.GetKey (KeyCode.S)) {
			rb.AddForce (this.transform.forward);
		}

		if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (0.0f, 5.0f, 0.0f);
		}
	}
}
