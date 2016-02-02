using UnityEngine;
using System.Collections;

public class AImove : MonoBehaviour {
	public static AImove move;
//	public static int turnSpeed = 20;
//	public static int swingSpeed = 50;
	private int targetPlanet;

	public static Rigidbody aiRigid;

	public static float force = 200.0f;
	public static int turnSpeed = 50;

//	public static float forwardSpeed = 20;
//	public float rotationPerSecond = 15f;
//	public float rotationMax = 45f;
	private float distanceToPlayer;
	public float minDist = 20f;
	public float maxDist = 40f;

	private bool turnLeft = false;
	private bool turnRight = false;
	private bool playerInFrontOfAI;
	private bool isFleeing = false;

	private GameObject player;
	private Vector3 relativePoint;
	Vector3 maxVelocity = new Vector3 (3.5f, 0.0f, 3.5f);


	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		aiRigid = GetComponent<Rigidbody>();
	}
		
    void FixedUpdate () 
	{
		if(isFleeing == false)
		{
			checkPlayerPosition ();
		}
		else{
			flee();
		}


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

	//Uses the data recieved from isFacingAgent
	//to determine weather or not to turn left,
	//right or continue driving forward
	void checkPlayerPosition()
	{
		playerInFrontOfAI = isFacingAgent ();
		distanceToPlayer = Vector3.Distance (this.transform.position, player.transform.position); //distance between AI and player
		Debug.Log("distancetoplayer er " + distanceToPlayer);
		Vector3 tempPos = this.transform.position - player.transform.position;

		if(distanceToPlayer > maxDist)
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

	//Runs a test and returns the result in a float variable.
	//Used to determine if an object is to the left, right
	//or front of This object
	private Vector3 Transformation(GameObject test)
	{
		relativePoint = transform.InverseTransformPoint(test.transform.position);
		Debug.Log("Relativepoint " + relativePoint);
		return relativePoint;
	}

	//Checks if the AI Ship is facing the Agent object or not
	private bool isFacingAgent()
	{
		if(relativePoint.x > -0.01 && relativePoint.x < 0.01){return true;}
		else return false;
	}

	//Turns the AI Ship so that the canons on either side
	//is facing straight for the player.
	//This is so that if the player is stationary, the
	//AI will still shoot on it.
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
			turnLeft = false;
			turnRight = false;
		}
	}

	void avoidPlayer()
	{
		relativePoint = Transformation(player);
		if (relativePoint.x <= 0)
		{
			if(relativePoint.z > -0.2)
			{
				turnLeft = false;
				turnRight = true;
			}
		}
		else if (relativePoint.x >= 0) {
			if(relativePoint.z > -0.2)
			{	
				turnRight = false;
				turnLeft = true;
			}
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
}