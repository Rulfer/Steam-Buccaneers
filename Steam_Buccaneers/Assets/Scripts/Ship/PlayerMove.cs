using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour 
{
	public Rigidbody donger;
	public float force = 0.5f;
	public int turnSpeed = 40;
	Vector3 maxVelocity = new Vector3 (0.5f, 0.0f, 0.5f);
	//Quaternion playerUpright = GameObject.Find("Small_Ship").transform.transform.rotation;
	//private GameObject Small_Ship;
	public static bool turnLeft = false;
	public static bool turnRight = false;
	public static bool goingForward = false;
	//Vector3 prevVelocity = new Vector3 (x,y,z);



	// Use this for initialization
	void Start () 
	{
		donger = GetComponent<Rigidbody>();

	}

	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (Small_Ship.transform.position);
		//Debug.Log ("x: " + donger.velocity.x);
		//Debug.Log ("z: " + donger.velocity.z);
		//Debug.Log (maxVelocity.x);
		if (Input.GetKey(KeyCode.W))
		{
			goingForward = true;
			donger.AddForce(transform.up * force*Time.deltaTime);
			// Series of if tests
			if (donger.velocity.x >= maxVelocity.x) //|| -donger.velocity.x >= -maxVelocity.x)
			{
				// one type of fix, but it is far from correct, speed stays around the max velocity, but it also makes it a lot harder to accelerate
				// in the z-axis, although it does in fact accelerate.
				donger.velocity = new Vector3 (maxVelocity.x, 0.0f, donger.velocity.z);
			}

			if (donger.velocity.x <= -maxVelocity.x)
			{
				donger.velocity = new Vector3 (-maxVelocity.x, 0.0f, donger.velocity.z);
			}

			if (donger.velocity.z >= maxVelocity.z)
			{
				donger.velocity = new Vector3 (donger.velocity.x, 0.0f, maxVelocity.z);
			}

			if (donger.velocity.z <= -maxVelocity.z)
			{
				donger.velocity = new Vector3 (donger.velocity.x, 0.0f, -maxVelocity.z);
			}
		}

		else
		{
			donger.velocity = donger.velocity * 0.90f;
			goingForward = false;
		}

		if (Input.GetKey (KeyCode.A)) 
		{
			transform.Rotate (Vector3.forward, turnSpeed*Time.deltaTime);
			turnLeft = true;
		}

		else
		{
			turnLeft = false;
		}

		if (Input.GetKey (KeyCode.D)) 
		{
			transform.Rotate (Vector3.back, turnSpeed*Time.deltaTime);
			turnRight = true;
		}
		// ALT DETTE ER NYTT
		else 
		{
			turnRight = false;
		}

		// Just a test to see if how fast we can drive without problems, also for testing the possibilities for increasing max speed in the shop upgrade menus.
		if (Input.GetKeyDown (KeyCode.X))
		{
			maxVelocity += new Vector3 (0.5f, 0.0f, 0.5f);
			Debug.Log (maxVelocity);
		}
	}
}