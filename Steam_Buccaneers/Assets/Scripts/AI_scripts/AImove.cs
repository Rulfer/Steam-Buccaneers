using UnityEngine;
using System.Collections;

public class AImove : MonoBehaviour {
	public static float forwardSpeed = 20;
	public static int turnSpeed = 20;
	public static int swingSpeed = 50;
	public float rotationPerSecond = 15f;
	public float rotationMax = 45f;
	public static bool turnLeft = false;
	public static bool turnRight = false;
	public static bool stopMoving = false;
	private bool startTurning = true;

	private GameObject agent;
	private GameObject player;
	private Vector3 relativePoint;

	private bool agentInFrontOfPlayer;

	void Start ()
	{
		agent = GameObject.FindGameObjectWithTag("aiAgent");
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () 
	{
//		relativePoint = transform.InverseTransformPoint(player.position);
//		Debug.Log(relativePoint);
		checkAIPosition ();

		if(PlayerMove.goingForward == false)
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
				isFacingPlayer();
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

	void checkAIPosition()
	{
		agentInFrontOfPlayer = isFacingAgent ();

		if (agentInFrontOfPlayer == true) 
		{
			turnLeft = false;
			turnRight = false;
			return;
		} 
		else 
		{
			turnTowardsAgent ();
		}
	}

	private Vector3 Transformation(GameObject test)
	{
		relativePoint = transform.InverseTransformPoint(test.transform.position);
		return relativePoint;
	}

	private bool isFacingAgent()
	{
		relativePoint = Transformation(agent);
		if(relativePoint.x == 0){return true;}
		else return false;
	}

	void isFacingPlayer()
	{
		relativePoint = Transformation(player);

		if(relativePoint.z != 0)
		{
			if(relativePoint.x < 0)
			{
				if(relativePoint.z <= -0.1)
				{
					startTurning = true;
					turnLeft = true;
					turnRight = false;
				}
				if(relativePoint.z >= 0.1)
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
				if(relativePoint.z >= 0)
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

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "aiAgent")
		{
			stopMoving = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "aiAgent")
		{
			stopMoving = false;
		}
	}
}