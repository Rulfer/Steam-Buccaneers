using UnityEngine;
using System.Collections;

public class AIavoid : MonoBehaviour 
{
	private GameObject player; //A referense to the player object

	public bool hitFront = false; //Tells AIMove if something is in front of it
	public bool hitSide = false; //Tells AIMove if something is to the side of it

	private Vector3 relativePlayerPoint; //Relative position between this enemy and the player
	private Vector3 fwd; //Forward vector

	private int detectDistance = 60; //Length of enemies radar

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () 
	{
		fwd = this.transform.TransformDirection(Vector3.forward); //Casts a ray forwards

		Debug.DrawRay(this.transform.position, fwd * detectDistance, Color.yellow); //Only visible in the editor

		relativePlayerPoint = transform.InverseTransformPoint(player.transform.position); //Used to check if the player is to the left or right of the AI

		sensors(); //Test if the sensor hit something
	}

	private void sensors()
	{
		RaycastHit objectHit;
		if(Physics.Raycast(this.transform.position, fwd, out objectHit, detectDistance))
		{
			if(objectHit.transform.tag == "Planet" || objectHit.transform.tag == "Moon" || objectHit.transform.tag == "asteroid" || objectHit.transform.root.name == "Cargo(Clone)" || objectHit.transform.root.name == "Marine(Clone)" || objectHit.transform.root.name == "Boss(Clone)") //The planet is in front of the AI
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
				hitFront = true;
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
				hitFront = true;
			}

			else //The raycast hit nothing of interest
			{
				this.GetComponent<AImove>().turnLeft = false;
				this.GetComponent<AImove>().turnRight = false;
				hitFront = false;
			}
		}

		else //The raycast hit nothing
		{
			this.GetComponent<AImove>().turnLeft = false;
			this.GetComponent<AImove>().turnRight = false;
			hitFront = false;
		}
	}
}
