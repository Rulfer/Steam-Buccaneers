using UnityEngine;
using System.Collections;

public class AImove : MonoBehaviour {
	public static int turnSpeed = 20;
	public static int swingSpeed = 50;

	private int targetPlanet;

	public static float forwardSpeed = 20;

	public float rotationPerSecond = 15f;
	public float rotationMax = 45f;

	public static bool turnLeft = false;
	public static bool turnRight = false;
	public static bool stopMoving = false;

	private bool agentInFrontOfPlayer;
	private bool startTurning = true;
	private bool planetTrouble = false;

	private GameObject agent;
	private GameObject player;
	private GameObject[] planets;

	private Vector3 relativePoint;

	void Start ()
	{
		agent = GameObject.FindGameObjectWithTag("aiAgent");
		player = GameObject.FindGameObjectWithTag("Player");
		planets = GameObject.FindGameObjectsWithTag("Planet");
	}

	// Update is called once per frame
	/// <summary>
    /// 
    /// </summary>
    void Update () 
	{
//		relativePoint = transform.InverseTransformPoint(player.transform.position);
//		Debug.Log(relativePoint);
		checkAIPosition ();
		planetTrouble = spotPlanets();

		if(planetTrouble == true)
		{

		}

		else
		{
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
					isFacingPlayer(player);
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

	private bool spotPlanets()
	{
		float temp;

		for(int i = 0; i < planets.Length; i++)
		{
			temp = Vector3.Distance (this.transform.position, planets[i].transform.position); //Distance between AI Ship and the chosen ball
			if(temp <= 150)
			{
				return true;
			}
		}

		return false;
	}

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

	private void isFacingPlayer(GameObject test)
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

	void OnTriggerEnter(Collider other)
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