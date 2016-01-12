using UnityEngine;
using System.Collections;

public class AImove : MonoBehaviour {
	public static float forwardSpeed = 20;
	public static int turnSpeed = 20;
	public static int swingSpeed = 50;
	public float rotationPerSecond = 15f;
	public float rotationMax = 45f;
	private bool turnLeft = false;
	private bool turnRight = false;
	public static bool stopMoving = false;

	public Transform agent;
	public Transform player;
	private Vector3 relativePoint;

	private bool agentInFrontOfPlayer;

	// Update is called once per frame
	void Update () 
	{
		relativePoint = transform.InverseTransformPoint(player.position);
		Debug.Log(relativePoint);
		checkAIPosition ();

		if(stopMoving == true)
		{
			isFacingPlayer();
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

	private Vector3 Transformation(Transform test)
	{
		relativePoint = transform.InverseTransformPoint(test.position);
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
					turnLeft = true;
					turnRight = false;
				}
				if(relativePoint.z >= 0.1)
				{
					turnLeft = false;
					turnRight = true;
				}
				if(relativePoint.z == 0)
				{
					turnLeft = false;
					turnRight = false;
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
				if(relativePoint.z == 0)
				{
					turnLeft = false;
					turnRight = false;
				}
			}
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