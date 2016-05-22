using UnityEngine;
using System.Collections;

public class AIavoid : MonoBehaviour {
	private GameObject player;

	public bool hitFront = false;
	public bool hitSide = false;

	private Vector3 relativePlayerPoint;
	private Vector3 fwd;

	private float hitTimer;
	private int detectDistance = 60;

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

			else 
			{
				this.GetComponent<AImove>().turnLeft = false;
				this.GetComponent<AImove>().turnRight = false;
				hitFront = false;
			}
		}

		else
		{
			this.GetComponent<AImove>().turnLeft = false;
			this.GetComponent<AImove>().turnRight = false;
			hitFront = false;
		}
	}
}
