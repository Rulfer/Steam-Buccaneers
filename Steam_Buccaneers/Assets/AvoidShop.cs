using UnityEngine;
using System.Collections;
/// <summary>
/// This script is basically AvoidPlanet, with some minor tweeks. 
/// It's easier to know where to go when we need to change something if
/// we have an entire script dedicated to avoid the shops.
/// </summary>
public class AvoidShop : MonoBehaviour {

	private GameObject player;

	public static bool hitShop = false;

	private Vector3 relativePlayerPoint;
	private Vector3 fwd;
	private Vector3 right;
	private Vector3 left;

	private float shopTimer;
	private int detectDistance = 30;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
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

		shopSensors();

		shopTimer += Time.deltaTime;

		if(shopTimer >= 1)
		{
			hitShop = false;
		}
	}

	private void shopSensors()
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
					this.GetComponent<AImove>().turnLeft = true;
					this.GetComponent<AImove>().turnRight = false;
				}
				else if(relativePlayerPoint.x <= 0)//Player to the left of the AI
				{
					this.GetComponent<AImove>().turnLeft = false;
					this.GetComponent<AImove>().turnRight = true;
				}
				hitShop = true;
				forwards = true;
				shopTimer = 0;
			}
		}

		else
		{
			forwards = false;
			this.GetComponent<AImove>().turnLeft = false;
			this.GetComponent<AImove>().turnRight = false;
		}

		if(Physics.Raycast(this.transform.position, left, out objectHit, detectDistance))
		{
			if(objectHit.transform.tag == "Planet") //The planet is to the left of the AI
			{
				this.GetComponent<AImove>().turnRight = true;
				this.GetComponent<AImove>().turnLeft = false;
				hitShop = true;
				shopTimer = 0;

			}

		}
		else
		{
			if(forwards == false)
			{
				this.GetComponent<AImove>().turnRight = false;
				this.GetComponent<AImove>().turnLeft = false;
			}
		}

		if(Physics.Raycast(this.transform.position, right, out objectHit, detectDistance))
		{
			if(objectHit.transform.tag == "Planet") //The planet is to the right of the AI
			{
				this.GetComponent<AImove>().turnLeft = true;
				this.GetComponent<AImove>().turnRight = false;
				hitShop = true;
				shopTimer = 0;

			}

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
