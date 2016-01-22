using UnityEngine;
using System.Collections;

public class AImove : MonoBehaviour {
	public static int turnSpeed = 20;
	public static int swingSpeed = 50;
	private int targetPlanet;

	public static float forwardSpeed = 20;
	public float rotationPerSecond = 15f;
	public float rotationMax = 45f;

	private bool turnLeft = false;
	private bool turnRight = false;
	public static bool stopMoving = false;
	private bool agentInFrontOfPlayer;
	private bool startTurning = true;

	private GameObject agent;
	private GameObject player;

	private Vector3 relativePoint;

	void Start ()
	{
		agent = GameObject.FindGameObjectWithTag("aiAgent");
		player = GameObject.FindGameObjectWithTag("Player");
	}
		
    void Update () 
	{
//		relativePoint = transform.InverseTransformPoint(player.transform.position);
//		Debug.Log(relativePoint);
		checkAIPosition ();

		if(PlayerMove2.goingForward == false)
		{
			if(AIsideCanons.fireLeft == true || AIsideCanons.fireRight == true)
			{
				stopMoving = true;
				startTurning = true;
			}
		}

		else stopMoving = false;

		if(stopMoving == true)
		{
			if(startTurning == true)
			{
				canonsFacingPlayer(player);
			}
		}

		if(PlayerMove2.goingForward == false && startTurning == false && stopMoving == true)
		{
			if(AIsideCanons.fireLeft == false && AIsideCanons.fireRight == false)
			{
				startTurning = true;
			}
		}

		else transform.Translate (Vector3.forward/forwardSpeed);


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
	void checkAIPosition()
	{
		agentInFrontOfPlayer = isFacingAgent ();

		if (agentInFrontOfPlayer == true) 
		{
			turnLeft = false;
			turnRight = false;
		} 
		else 
		{
			turnTowardsAgent ();
		}
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
		relativePoint = Transformation(agent);
		if(relativePoint.x == 0){return true;}
		else return false;
	}

	//Turns the AI Ship so that the canons on either side
	//is facing straight for the player.
	//This is so that if the player is stationary, the
	//AI will still shoot on it.
	private void canonsFacingPlayer(GameObject test)
	{
		relativePoint = Transformation(test);

		if(relativePoint.z != 0)
		{
			if(relativePoint.x < 0)
			{
				if(relativePoint.z < 0)
				{
					startTurning = true;
					turnLeft = true;
					turnRight = false;
				}
				if(relativePoint.z > 0)
				{
					startTurning = true;
					turnLeft = false;
					turnRight = true;
				}
			}

			if(relativePoint.x >= 0)
			{
				if(relativePoint.z < 0)
				{
					turnLeft = false;
					turnRight = true;
				}
				if(relativePoint.z > 0)
				{
					turnLeft = true;
					turnRight = false;
				}
			}
		}

		else
		{
			turnLeft = false;
			turnRight = false;
			startTurning = false;
		}
	}

	//Makes the AI Ship turn towards the agent
	void turnTowardsAgent()
	{
		relativePoint = Transformation(agent);
		if (relativePoint.x < 0)
		{
			turnLeft = true;
			turnRight = false;
		}
		if (relativePoint.x > 0) {
			turnRight = true;
			turnLeft = false;
		}
	}

	//Stops the AI Ship when it collides with the Agent
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "aiAgent")
		{
			stopMoving = true;
		}
	}

	//Makes the AI move again
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "aiAgent")
		{
			stopMoving = false;
		}
	}
}