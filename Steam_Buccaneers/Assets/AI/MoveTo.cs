// MoveTo.cs
using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

	public Transform goal;
	public Transform left;
	public Transform right;

	void Start () {
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.position; 
	}

	void FixedUpdate() {
		NavMeshAgent agent = GetComponent<NavMeshAgent>();

//		if (AIDetect.almostLeftSide == true) {
//			agent.destination = left.position;
//		}
//
//		if (AIDetect.almostRightSide == true) {
//			agent.destination = right.position;
//		}
//
//		if (AIDetect.frontSide == true) {
			agent.destination = goal.position;
		//}
	}
}