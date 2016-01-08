using UnityEngine;
using System.Collections;

public class simpleMoveWTurning : MonoBehaviour {

	public float speed;
	Rigidbody rb;

	void Start () {
		//Get the rigidbody to the object
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		//W and S addsforce in front and back, while A and D turns.
		if (Input.GetKey(KeyCode.W)) {
			rb.transform.Translate (Vector3.back);
		}

		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (0.0f, -5.0f, 0.0f);
		}

		if (Input.GetKey (KeyCode.S)) {
			rb.transform.Translate (Vector3.forward);
		}

		if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (0.0f, 5.0f, 0.0f);
		}
	}
}
