// MoveTo.cs
using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {


	public Transform[] ball;
	public Transform aiPoint;

	private float distance = 1000;
	public float timeLeft = 2;
	private int tempI;

	private NavMeshAgent agent;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		touchBalls ();
	}

	void Update() {
			touchBalls ();
	}

	void touchBalls()
	{
		for (int i = 0; i < ball.Length; i++)
		{
			float temp = Vector3.Distance (aiPoint.transform.position, ball [i].transform.position);
			if (temp < distance) {
				distance = temp;
				agent.destination = ball [i].position;
//				tempI = i;


//				if (i == 5) {
//					agent.destination = ball [0].position;
//					tempI = 0;
//				} else {
//					agent.destination = ball [i++].position;
//					tempI = i++;
//				} 
			}
		}
	}
}