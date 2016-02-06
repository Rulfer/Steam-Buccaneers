using UnityEngine;
using System.Collections;

public class avoidPlanet : MonoBehaviour {
	public static avoidPlanet avoid;

	private GameObject player;

	public static bool hitPlanet = false;

	public static float force = 200.0f;
	public static int turnSpeed = 50;
	public static Rigidbody aiRigid;

	private Vector3 relativePlayerPoint;
	private Vector3 fwd;
	private Vector3 right;
	private Vector3 left;

	private float planetTimer;
	private int detectDistance = 30;

    void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		aiRigid = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () 
	{
		fwd = this.transform.TransformDirection(Vector3.forward);
		right = this.transform.TransformDirection(Vector3.right);
		left = this.transform.TransformDirection(Vector3.left);

		Debug.DrawRay(this.transform.position, fwd * detectDistance, Color.yellow);
		Debug.DrawRay(this.transform.position, right * detectDistance, Color.green);
		Debug.DrawRay(this.transform.position, left * detectDistance, Color.blue);

		relativePlayerPoint = transform.InverseTransformPoint(player.transform.position); //Used to check if the player is to the left or right of the AI

		planetSensors();

		planetTimer += Time.deltaTime;

		if(planetTimer >= 1)
		{
			hitPlanet = false;
		}
	}

	private void planetSensors()
	{
		bool forwards = false;
		bool lefty = false;


		RaycastHit objectHit;
		if(Physics.Raycast(this.transform.position, fwd, out objectHit, detectDistance))
		{
			if(objectHit.transform.tag == "Planet") //The planet is in front of the AI
			{
				if(relativePlayerPoint.x > 0) //Player to the right of the AI
				{
					AImove.turnLeft = false;
					AImove.turnRight = true;
				}
				else if(relativePlayerPoint.x <= 0)//Player to the left of the AI
				{
					AImove.turnLeft = true;
					AImove.turnRight = false;
				}
				hitPlanet = true;
				forwards = true;
				planetTimer = 0;
			}
		}

		else
		{
			forwards = false;
			AImove.turnLeft = false;
			AImove.turnRight = false;
		}

		if(Physics.Raycast(this.transform.position, left, out objectHit, detectDistance))
		{
			if(objectHit.transform.tag == "Planet") //The planet is to the left of the AI
			{
				AImove.turnRight = true;
				AImove.turnLeft = false;
				hitPlanet = true;
				planetTimer = 0;

			}

		}
		else
		{
			if(forwards == false)
			{
				AImove.turnRight = false;
				AImove.turnLeft = false;
			}
		}

		if(Physics.Raycast(this.transform.position, right, out objectHit, detectDistance))
		{
			if(objectHit.transform.tag == "Planet") //The planet is to the right of the AI
			{
				AImove.turnLeft = true;
				AImove.turnRight = false;
				hitPlanet = true;
				planetTimer = 0;

			}

		}
		else
		{
			if(forwards == false && lefty == false)
			{
				AImove.turnLeft = false;
				AImove.turnRight = false;
			}
		}
	}
}