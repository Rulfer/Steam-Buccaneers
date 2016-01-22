//This class is used to move the AI Agent around!
//It detects the balls surrounding the player and drives towards the closest one.
//The AI will follow this object.

using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {
	public static MoveTo move;

	private GameObject[] ball; //Array for the balls around the player
	public Transform agentObject; //We now use the basic Agent object. No detector in front, as the AI is to follow this Agent.
	public GameObject player; //Player
	//public GameObject aiObject; //AI ship
	public GameObject aiPoint; //Detector in front of AI ship

	private float distance = 1000; //An unreasonable large distance used for testing
	private float playerAndBallsDistance = 1000;
	public float minDistanceToPlayerBall = 40; //The minimun distance the AI Ship must have between itself and a player ball

	public static int aiTargetBall;

	private bool isChosen = false;
	public static bool newBall = false;

	private NavMeshAgent agent; //AI Agent

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player");
		//aiObject = GameObject.FindGameObjectWithTag("aiShip");
		aiPoint = GameObject.Find("aiPoint");
		ball = GameObject.FindGameObjectsWithTag("playerBalls"); //Finds all gameobjects with the tag "playerBalls" and put them in the array
	}

	void Update() {

		if(newBall == true)
		{
			isChosen = false;
			stopNextToPlayer();
			newBall = false;
		}
		if(PlayerMove2.goingForward == false) //If its equal to the previous, then the player has stopped moving
		{
			stopNextToPlayer(); //Make the next AI move
		}

		else
		{
			touchBalls (); //Continue to touch the balls
		}
	}

	//This function makes it so that the AI follows the balls surrounding the player instead of the player itself.
	void touchBalls()
	{
		for (int i = 0; i < ball.Length; i++) //Runs the length of the array
		{
			float temp = Vector3.Distance (aiPoint.transform.position, ball [i].transform.position); //Calculates the distance between the aiPoint and the player
			if (temp < distance) { //The new ball is closer than the previous
				distance = temp; //The new ball is the closest one
				agent.destination = ball [i].transform.position; //The new ball is the new destination
			}
		}
		distance = 1000; //Resets the distance so a new test kan be initiated. 
	}
		
	//Makes the Agent choose a ball that is close to the AI Ship.
	//This happens when the player stoppes moving forward.
	public void stopNextToPlayer()
	{
		if(isChosen == false)
		{
			aiTargetBall = studyBalls(aiTargetBall);
			agent.destination = ball [aiTargetBall].transform.position;
			isChosen = true;
		}

		else agent.destination = ball [aiTargetBall].transform.position;
	}

	private int studyBalls(int test) //Drives to the ball closest to the AI detector
	{
		float temp;
		for (int i = 0; i < ball.Length; i++) //Runs equal to the ammount of player balls
		{
			temp = Vector3.Distance (aiPoint.transform.position, ball[i].transform.position); //Distance between AI Detector and the chosen ball
			if(temp >= minDistanceToPlayerBall)
			{
				if(temp < playerAndBallsDistance)
				{
					playerAndBallsDistance = temp;
					test = i;
				}
			}
		}
		isChosen = true;
		return test;
	}
}