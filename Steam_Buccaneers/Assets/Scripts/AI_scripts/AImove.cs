using UnityEngine;
using System.Collections;

public class AImove : MonoBehaviour {
	public static AImove move;
	private int targetPlanet;

	public static Rigidbody aiRigid;

	public static float force = 10000f;
	public static int turnSpeed = 50;

//	public static float forwardSpeed = 20;
//	public float rotationPerSecond = 15f;
//	public float rotationMax = 45f;
	private float distanceToPlayer;
	public float minDist = 20f;
	public float maxDist = 40f;

	public static bool turnLeft = false;
	public static bool turnRight = false;
	private bool playerInFrontOfAI;
	private bool isFleeing = false;

	private GameObject player;
	private Vector3 relativePoint;
	/// <summary>
	/// Is now changed via AIMaster.cs.
	/// We want the AI to move extra fast once spawned, and slower
	/// when it has reached the player.
	/// </summary>
	public static Vector3 maxVelocity = new Vector3 (3.5f, 0.0f, 3.5f);


	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		aiRigid = GetComponent<Rigidbody>();
	}
		
    void FixedUpdate () 
	{
		if(avoidPlanet.hitPlanet == false)
		{
			if(isFleeing == false)
			{
				checkPlayerPosition ();
			}
			else{
				flee();
			}
		}

		relativePoint = transform.InverseTransformPoint(player.transform.position);

		aiRigid.AddForce(transform.forward * force*Time.deltaTime);
		// Series of if tests
		if (aiRigid.velocity.x >= maxVelocity.x) //|| -aiRigid.velocity.x >= -maxVelocity.x)
		{
			// one type of fix, but it is far from correct, speed stays around the max velocity, but it also makes it a lot harder to accelerate
			// in the z-axis, although it does in fact accelerate.
			aiRigid.velocity = new Vector3 (maxVelocity.x, 0.0f, aiRigid.velocity.z);
		}

		if (aiRigid.velocity.x <= -maxVelocity.x)
		{
			aiRigid.velocity = new Vector3 (-maxVelocity.x, 0.0f, aiRigid.velocity.z);
		}

		if (aiRigid.velocity.z >= maxVelocity.z)
		{
			aiRigid.velocity = new Vector3 (aiRigid.velocity.x, 0.0f, maxVelocity.z);
		}

		if (aiRigid.velocity.z <= -maxVelocity.z)
		{
			aiRigid.velocity = new Vector3 (aiRigid.velocity.x, 0.0f, -maxVelocity.z);
		}

		if (turnLeft == true) 
		{
			transform.Rotate (Vector3.down, turnSpeed * Time.deltaTime);
		}

		if (turnRight == true) 
		{
			transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
		}
	}
		
	//Uses the data recieved from isFacingAgent
	//to determine weather or not to turn left,
	//right or continue driving forward
	void checkPlayerPosition()
	{
		playerInFrontOfAI = isFacingAgent ();
		distanceToPlayer = Vector3.Distance (this.transform.position, player.transform.position); //distance between AI and player

		if(distanceToPlayer > maxDist) //AI too far away from the player
		{
			if(playerInFrontOfAI == true)
			{
				turnLeft = false;
				turnRight = false;
			}

			else
			{
				turnTowardsPlayer ();
			}
		}

		else if(distanceToPlayer < minDist)
		{
			avoidPlayer();
		}

		else if(distanceToPlayer < maxDist && distanceToPlayer > minDist)
		{
			canonsFacingPlayer(player);
		}
	}

	//Turns the AI Ship so that the canons on either side
	//is facing straight for the player.
	private void canonsFacingPlayer(GameObject test)
	{
		relativePoint = Transformation(test);

		if(relativePoint.z < 0)
		{
			if(relativePoint.x <= 0)
			{
				turnLeft = true;
				turnRight = false;
			}

			else if(relativePoint.x >= 0)
			{
				turnLeft = false;
				turnRight = true;
			}
		}

		else
		{
			if(relativePoint.x <= 0)
			{
				turnLeft = false;
				turnRight = true;
			}

			else if(relativePoint.x >= 0)
			{
				turnLeft = true;
				turnRight = false;
			}
		}
	}

	void avoidPlayer()
	{
		relativePoint = Transformation(player);
		if (relativePoint.x <= 0)
		{
			turnLeft = false;
			turnRight = true;
		}
		else if (relativePoint.x >= 0) 
		{
			turnRight = false;
			turnLeft = true;
		}
	}

	//Makes the AI Ship turn towards the agent
	void turnTowardsPlayer()
	{
		relativePoint = Transformation(player);
		if (relativePoint.x <= 0)
		{
			turnLeft = true;
			turnRight = false;
		}
		else if (relativePoint.x >= 0) {
			turnRight = true;
			turnLeft = false;
		}
	}

	public void flee()
	{
		relativePoint = Transformation(player);

		if(relativePoint.x >-0.1 && relativePoint.x <0.1)
		{
			if(relativePoint.z <= 0)
			{
				turnLeft = false;
				turnRight = false;
			}
			else if(relativePoint.z >= 0)
			{
				if(PlayerMove2.turnLeft == true)
				{
					turnRight = true;
					turnLeft = false;
				}

				else if(PlayerMove2.turnRight == true)
				{
					turnLeft = true;
					turnRight = false;
				}

				else
				{
					turnLeft = true;
					turnRight = false;
				}
			}
		}
		if (relativePoint.x <= 0)
		{
			turnRight = true;
			turnLeft = false;
		}
		else if (relativePoint.x >= 0) 
		{
			turnRight = false;
			turnLeft = true;
		}
		isFleeing = true;
	}


	//Runs a test and returns the result in a float variable.
	//Used to determine if an object is to the left, right
	//or front of This object
	private Vector3 Transformation(GameObject test)
	{
		relativePoint = transform.InverseTransformPoint(test.transform.position);
		return relativePoint;
	}

	//Checks if the AI Ship is facing the Agent object or not
	private bool isFacingAgent()
	{
		if(relativePoint.z > 0)
		{
			if(relativePoint.x > -0.01 && relativePoint.x < 0.01)
			{
				return true;
			}
			else return false;
		}
		else return false;
	}
}