using UnityEngine;
using System.Collections;

public class PlayerMove2 : MonoBehaviour 
{
	public static Rigidbody donger;
	GameObject schlonger;
	GameObject sun;
	public Vector3 stopRotatingShitface = new Vector3 (90f,180f,0f);
	public static float force = 2000.0f;
	public static int turnSpeed = 10;
	public static float dongerTurn = 0.50f;
	Vector3 maxVelocity = new Vector3 (1000f, 0.0f, 1000f);
	public static bool turnLeft = false;
	public static bool turnRight = false;
	public static bool goingForward = false;
	public static bool hitBomb = false;
	bool steerShip;
	Ray ray;
	RaycastHit hit;
	Vector3 dongerSchlonger = new Vector3 (0f, -1f, 0f);
	LayerMask layer = 1 << 8;


	// Use this for initialization
	void Start () 
	{
		sun = GameObject.Find ("sun");
		donger = GetComponent<Rigidbody>();
		schlonger = GameObject.Find ("PlayerShip");

	}
		
		

	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (steerShip);
		//Debug.Log (turnLeft);
		//Debug.Log (Small_Ship.transform.position);
		//Debug.Log ("x: " + donger.velocity.x);
		//Debug.Log ("z: " + donger.velocity.z);
		//Debug.Log (maxVelocity.x);
		if(hitBomb == false)
		{
			if(steerShip == true)
			{
				if (Input.GetKey(KeyCode.W))
				{
					goingForward = true;
					donger.AddForce(transform.forward * force*Time.deltaTime);
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
					//donger.velocity = donger.velocity * 0.90f;
					//goingForward = false;
				}


				if (Input.GetKey (KeyCode.A)) 
				{
					transform.Rotate (Vector3.down, turnSpeed*Time.deltaTime);
					turnLeft = true;
				}

				else
				{
					turnLeft = false;
				}

				if (Input.GetKey (KeyCode.D)) 
				{
					transform.Rotate (Vector3.up, turnSpeed*Time.deltaTime);
					turnRight = true;
				}
				// ALT DETTE ER NYTT
				else 
				{
					turnRight = false;
				}
			}
				
			// Just a test to see if how fast we can drive without problems, also for testing the possibilities for increasing max speed in the shop upgrade menus.
			if (Input.GetKeyDown (KeyCode.X))
			{
				maxVelocity += new Vector3 (0.5f, 0.0f, 0.5f);
				Debug.Log (maxVelocity);
			}

			OutOfBounds();
		}
	}

	void OutOfBounds ()
	{
		if (Physics.Raycast(this.transform.position, dongerSchlonger, out hit, Mathf.Infinity, layer))
		{
			if (hit.transform.tag == ("playerBorder"))
			{
				steerShip = true;
			}
		}

		else
		{
			steerShip = false;

			if (donger.transform.position.x > 0)
			{
				if (donger.transform.localEulerAngles.y <= 0f || donger.transform.localEulerAngles.y >= 270f)
				{
					turnLeft = true;
					transform.Rotate(Vector3.down, turnSpeed*Time.deltaTime);
					if (donger.transform.localEulerAngles.y <= 270f)
					{
						turnLeft = false;
					}
				}
				//donger.AddForce(transform.forward * force*Time.deltaTime);
			}	

			if (donger.transform.position.x < 0)
			{
				if (donger.transform.localEulerAngles.y <= 0f || donger.transform.localEulerAngles.y >= 90f)
				{
					turnRight = true;
					transform.Rotate(Vector3.up, turnSpeed*Time.deltaTime);
					if (donger.transform.localEulerAngles.y <= 90f)
					{
						turnRight = false;
					}
				}
			}

			if (turnRight == false && turnLeft == false)
			{
				donger.AddForce(transform.forward * force*Time.deltaTime);
			}
				
		}
	}
}

