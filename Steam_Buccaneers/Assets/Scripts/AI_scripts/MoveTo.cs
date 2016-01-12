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

	public static bool playerLeftside;

	private NavMeshAgent agent; //AI Agent

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		ball = GameObject.FindGameObjectsWithTag("playerBalls"); //Finds all gameobjects with the tag "playerBalls" and put them in the array
	}

	void Update() {
		playerNewPos = player.position;

		if(playerNewPos == playerPrevPos)
		{
			stopNextToPlayer();
		}

		else
		{
			touchBalls ();
		}
		playerPrevPos = playerNewPos;
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

	void stopNextToPlayer()
	{
		playerLeftside = studyBalls(playerLeftside);
		GameObject testLeft = GameObject.Find("primaryLeft");
		GameObject testRight = GameObject.Find("primaryRight");

		if(playerLeftside == true)
		{
			agent.destination = testLeft.transform.position;
		}

		else
		{
			agent.destination = testRight.transform.position;
		}

	}

	private bool studyBalls(bool test)
	{
		GameObject testLeft = GameObject.Find("primaryLeft");
		GameObject testRight = GameObject.Find("primaryRight");
		float temp = Vector3.Distance (aiObject.transform.position, testLeft.transform.position);

		if(temp < playerAndBallsDistance)
		{
			playerAndBallsDistance = temp;
			test = true;
		}

		temp = Vector3.Distance (aiObject.transform.position, testRight.transform.position);
		if(temp < playerAndBallsDistance)
		{
			test = false;
		}

		return test;
	}
}