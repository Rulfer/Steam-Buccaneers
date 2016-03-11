using UnityEngine;
using System.Collections;

public class AIavoid : MonoBehaviour {
	private GameObject player;

	public bool hitObject = false;
	public bool hitLeft = false;
	public bool hitRight = false;

	private Vector3 relativePlayerPoint;
	private Vector3 fwd;

	private float hitTimer;
	private int detectDistance = 30;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () 
	{
		fwd = this.transform.TransformDirection(Vector3.forward);

		Debug.DrawRay(this.transform.position, fwd * detectDistance, Color.yellow);

		relativePlayerPoint = transform.InverseTransformPoint(player.transform.position); //Used to check if the player is to the left or right of the AI

		sensors();

		hitTimer += Time.deltaTime;
		if(hitTimer >= 0.1f)
		{
			hitObject = false;
			hitRight = false;
			hitLeft = false;
		}
	}

	private void sensors()
	{
		bool forwards = false;
		bool lefty = false;


		RaycastHit objectHit;
		if(Physics.Raycast(this.transform.position, fwd, out objectHit, detectDistance))
		{
			if(objectHit.transform.tag == "Planet" || objectHit.transform.tag == "aiShip") //The planet is in front of the AI
			{
				if(relativePlayerPoint.x > 0) //Player to the right of the AI
				{
					this.GetComponent<AImove>().turnLeft = false;
					this.GetComponent<AImove>().turnRight = true;
				}
				else if(relativePlayerPoint.x <= 0)//Player to the left of the AI
				{
					this.GetComponent<AImove>().turnLeft = true;
					this.GetComponent<AImove>().turnRight = false;
				}
				hitObject = true; //We hit something
				forwards = true; //Sets this to true so the rest of the code knows this
				hitTimer = 0; //Restarts the timer
			}

			else if(objectHit.transform.tag == "shopWall") //A shopWall is in front of the AI
			{
				if(relativePlayerPoint.x > 0) //Player to the right of the AI
				{
					this.GetComponent<AImove>().turnLeft = true;
					this.GetComponent<AImove>().turnRight = false;
				}
				else if(relativePlayerPoint.x <= 0)//Player to the left of the AI
				{
					this.GetComponent<AImove>().turnLeft = false;
					this.GetComponent<AImove>().turnRight = true;
				}
				hitObject = true; //We hit something
				forwards = true; //Sets this to true so the rest of the code knows this
				hitTimer = 0; //Restarts the timer
			}
		}

		else
		{
			forwards = false;
			this.GetComponent<AImove>().turnLeft = false;
			this.GetComponent<AImove>().turnRight = false;
		}

		if(hitLeft == true)
		{
			hitLeft = false;
			hitObject = true;
			hitTimer = 0;
			this.GetComponent<AImove>().turnRight = true;
			this.GetComponent<AImove>().turnLeft = false;
		}

		else
		{
			if(forwards == false)
			{
				this.GetComponent<AImove>().turnRight = false;
				this.GetComponent<AImove>().turnLeft = false;
			}
		}

		if(hitRight == true)
		{
			hitObject = true;
			hitTimer = 0;
			this.GetComponent<AImove>().turnLeft = true;
			this.GetComponent<AImove>().turnRight = false;
		}

		else
		{
			if(forwards == false && lefty == false)
			{
				this.GetComponent<AImove>().turnLeft = false;
				this.GetComponent<AImove>().turnRight = false;
			}
		}
	}
}
