using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {
	Vector3 movement;   
	Rigidbody playerRigidbody;
	public float speed = 6f;  

	// Use this for initialization
	void Start () {
		playerRigidbody = GetComponent <Rigidbody> ();
		transform.localPosition = new Vector3 (-15, 1.5f, -15);
	}

	void FixedUpdate ()
	{
		// Store the input axes.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		// Move the player around the scene.
		Move (h, v);
	}
	
	void Move (float h, float v)
	{
		// Set the movement vector based on the axis input.
		movement.Set (h, 0f, v);

		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;

		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition (transform.position + movement);
	}
}
