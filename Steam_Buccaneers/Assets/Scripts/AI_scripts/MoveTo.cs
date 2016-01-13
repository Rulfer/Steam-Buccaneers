using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

	private Vector3 playerPrevPos;
	private Vector3 playerNewPos;

	private GameObject[] ball; //Array for the balls around the player
	//public Transform aiPoint; //The AI has a ball in front of it, a detector. This is that detector used to detect the balls around the player
	public Transform aiPoint; //We now use the basic Agent object. No detector in front, as the AI is to follow this Agent.
	public Transform player;
	public Transform aiObject;

	private float distance = 1000; //An unreasonable large distance used for testing
	private float playerAndBallsDistance = 1000;

	public static int aiTargetBall;

	private bool isChosen = false;
	public static bool playerStopped = false;

	private NavMeshAgent agent; //AI Agent

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		ball = GameObject.FindGameObjectsWithTag("playerBalls"); //Finds all gameobjects with the tag "playerBalls" and put them in the array
	}

	void Update() {
		playerNewPos = player.position; //The new position of the player

		if(playerNewPos == playerPrevPos) //If its equal to the previous, then the player has stopped moving
		{
			playerStopped = true;
			stopNextToPlayer(); //Make the next AI move
		}

		if(AImove.turnLeft == true || AImove.turnRight == true)
		{
			playerIsTurning();
		}

		else
		{
			isChosen = false;
			playerStopped = false;
			touchBalls (); //Continue to touch the balls
		}
		playerPrevPos = playerNewPos; //Save the new position as the previous one to conduct new tests
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

	void playerIsTurning()
	{
		aiTargetBall = studyBalls(aiTargetBall);
		agent.destination = ball [aiTargetBall].transform.position;
	}

	void stopNextToPlayer()
	{
		if(isChosen == false)
		{
			aiTargetBall = studyBalls(aiTargetBall);
			agent.destination = ball [aiTargetBall].transform.position;
		}

		else agent.destination = ball [aiTargetBall].transform.position;
	}

	private int studyBalls(int test)
	{
		float temp;
		for (int i = 0; i < ball.Length; i++)
		{
			temp = Vector3.Distance (aiObject.transform.position, ball[i].transform.position);
			if(temp < playerAndBallsDistance)
			{
				playerAndBallsDistance = temp;
				test = i;
			}
		}
		isChosen = true;
		return test;
	}
}