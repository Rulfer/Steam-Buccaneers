using UnityEngine;
using System.Collections;

public class AIMaster : MonoBehaviour {
	
	public static float detectDistance;
	public Transform playerPoint;
	public Transform aiPoint;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
		detectDistance = Vector3.Distance (playerPoint.transform.position, aiPoint.transform.position);
		if (detectDistance <= 30) 
		{
			GetComponent<AIPatroling>().enabled = false;
			GetComponent<MoveTo> ().enabled = true;
			GetComponent<AIDetect> ().enabled = false;
		}

		if (detectDistance >= 40)
		{
			GetComponent<AIPatroling> ().enabled = true;
			GetComponent<MoveTo> ().enabled = false;
			GetComponent<AIDetect> ().enabled = false;
		}

		if (detectDistance <= 20) 
		{
			GetComponent<AIDetect> ().enabled = true;
			//GetComponent<AIsideCanons> ().enabled = true;
			//GameObject.Find ("Sidecanons").GetComponent<AIsideCanons> ().enabled = true;
		}

		if (AIsideCanons.fireLeft == true || AIsideCanons.fireRight == true) {
			agent.stoppingDistance = 20;
			agent.autoBraking = true;
		} 
		if (AIsideCanons.fireLeft == false && AIsideCanons.fireRight == false){
			agent.stoppingDistance = 0;
			agent.autoBraking = false;
		}


	}
}
