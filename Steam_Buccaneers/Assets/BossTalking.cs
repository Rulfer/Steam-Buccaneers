using UnityEngine;
using System.Collections;

public class BossTalking : MonoBehaviour 
{
	public static BossTalking talking;
	private GameObject playerPoint;
	private float detectDistance;
	private Vector3 relativePoint;
	private int turnSpeed;
	public bool faced = false;

	bool turnLeft;
	bool turnRight;

	// Use this for initialization
	void Start () 
	{
		talking = this;
		this.GetComponent<AIMaster>().enabled = false;
		this.GetComponent<AImove>().enabled = false;
		this.GetComponent<AIavoid>().enabled = false;
		this.GetComponent<AIPatroling>().enabled = false;
		playerPoint = GameObject.Find ("PlayerShip"); //As the player is a prefab, I had to add it to the variable this way
		turnSpeed = PlayerMove2.move.turnSpeed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		detectDistance = Vector3.Distance (playerPoint.transform.position, this.transform.position); //calculates the distance between the AI and the player

		if(detectDistance < 150)
		{
			if(faced == false)
				facePlayer();
		}

		if(faced == false)
		{
			if (turnLeft == true) 
			{
				this.transform.Rotate (Vector3.down, turnSpeed * Time.deltaTime);
			}

			if (turnRight == true) 
			{
				this.transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
			}
		}
//		else
//			actiaveScripts();
	}

	void facePlayer()
	{
		playerPoint.GetComponent<PlayerMove2>().enabled = false;
		playerPoint.GetComponent<Rigidbody>().mass = 5;
		playerPoint.GetComponent<Rigidbody>().angularDrag = 5;
		playerPoint.GetComponent<Rigidbody>().drag = 5;

		GameControl.control.isFighting = true;

		relativePoint = transform.InverseTransformPoint(playerPoint.transform.position);

		bool playerInFrontOfAI = isFacingPlayer ();

		if(playerInFrontOfAI == true)
		{
			turnLeft = false;
			turnRight = false;
			faced = true;
		}
		else
		{
			if(relativePoint.x <= 0) //The player is to the left of the boss)
			{
				turnLeft = true;
				turnRight = false;
			}
			else
			{
				turnLeft = false;
				turnRight = true;
			}
			
		}
	}

	private bool isFacingPlayer()
	{
		if(relativePoint.z > 0)
		{
			if(relativePoint.x > -0.1 && relativePoint.x < 0.1)
			{
				return true;
			}
			else return false;
		}
		else return false;
	}

	void actiaveScripts()
	{
		this.GetComponent<AIMaster>().enabled = true;
		this.GetComponent<AImove>().enabled = true;
		this.GetComponent<AIavoid>().enabled = true;
		this.GetComponent<AIPatroling>().enabled = true;

		playerPoint.GetComponent<PlayerMove2>().enabled = true;
		playerPoint.GetComponent<Rigidbody>().mass = 1;
		playerPoint.GetComponent<Rigidbody>().angularDrag = 0.5f;
		playerPoint.GetComponent<Rigidbody>().drag = 0.5f;
	}
}
