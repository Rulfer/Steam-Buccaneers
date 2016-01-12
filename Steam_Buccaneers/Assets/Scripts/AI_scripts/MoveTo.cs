using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

	private GameObject[] ball; //Array for the balls around the player
	public Transform aiPoint; //The AI has a ball in front of it, a detector. This is that detector used to detect the balls around the player

	private float distance = 1000; //An unreasonable large distance used for testing

	private NavMeshAgent agent; //AI Agent

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		ball = GameObject.FindGameObjectsWithTag("playerBalls"); //Finds all gameobjects with the tag "playerBalls" and put them in the array
	}

	void Update() {
		touchBalls ();
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

	void swapBalls()
	{
		for (int i = 0; i < ball.Length; i++)
		{
			NavMesh.GetAreaCost (ball[i]);
		}

	}
}