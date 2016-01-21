using UnityEngine;
using System.Collections;

public class chasePlayer : MonoBehaviour {

	private GameObject player;

	private bool turnRight = false;
	private bool turnLeft = false;
	private bool moveForward = false;

	private float forwardSpeed;
	private float turnSpeed;
	private float relativePoint;
	private float distanceToPlayer;
	private float minDistance = 5;
	private float maxDistance = 20;

	private Vector3 relativePlayerPoint;
	private Vector3 playerRotation;
	private Vector3 aiRotation;
	private Vector3 playerPrevPos;
	private Vector3 playerNewPos;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		turnSpeed = PlayerMove2.turnSpeed;
		forwardSpeed = 20;
	}
	
	// Update is called once per frame
	void Update () {
		relativePoint = Vector3.Distance (this.transform.position, player.transform.position); //Distance between AI and all planets

		playerPrevPos = playerNewPos; //Updates old player position
		playerNewPos = player.transform.position; //Updates new player position

		planNewRoute();

//		if(relativePoint <= minDistance) //AI to close to a planet
//		{
//			GetComponent<avoidPlanet> ().enabled = false;
//			followPlayer();
//		}

		//		if(relativePoint >= aiPlanetDistance) //Out of danger
		//		{
		//			GetComponent<AImove> ().enabled = true;
		//			MoveTo.newBall = true;
		//		}

		if(moveForward == true)
		{
			transform.Translate (Vector3.forward/forwardSpeed);
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

	void planNewRoute()
	{
		relativePlayerPoint = transform.InverseTransformPoint(player.transform.position); //Used to check if the player is to the left or right of the AI
		distanceToPlayer = Vector3.Distance (this.transform.position, player.transform.position); //Distance between AI and player
		playerRotation =  gameObject.transform.localEulerAngles;
		aiRotation = this.gameObject.transform.localEulerAngles;

		if(relativePlayerPoint.x >-0.1 && relativePlayerPoint.x <0.1) //The player is to the front of the AI
		{
			playerToTheFront();
			Debug.Log("Player fremover");
		}

		if(relativePlayerPoint.x < -0.1)//The player is to the left of the AI
		{
			playerToTheLeft();
			Debug.Log("Player til venstre");
		}

		if(relativePlayerPoint.x > 0.1) //The player is to the right of the AI
		{
			playerToTheRight();
			Debug.Log("Player til høyre");
		}
	}

	void playerToTheFront()
	{
		if(aiRotation.y > playerRotation.y)
		{
			if(distanceToPlayer >= minDistance)
			{
				moveForward = true;
				turnLeft = true;
				turnRight = false;
			}

			else
			{
				turnRight = false;
				turnLeft = false;
				moveForward = true;
			}
		}
	}

	void playerToTheLeft()
	{

	}

	void playerToTheRight()
	{

	}
		
}
