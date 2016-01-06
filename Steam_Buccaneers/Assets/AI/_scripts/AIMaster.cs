using UnityEngine;
using System.Collections;

public class AIMaster : MonoBehaviour {
	
	public static float detectDistance;
	public Transform playerPoint;
	public Transform aiPoint;

	// Use this for initialization
	void Start () {
	}
	
	void Update () {
		detectDistance = Vector3.Distance (playerPoint.transform.position, aiPoint.transform.position);

//		if (detectDistance <= 40) 
//		{
//			GetComponent<AIPatroling>().enabled = false;
//			GetComponent<MoveTo> ().enabled = true;
//		}
//
//		if (detectDistance >= 50)
//		{
//			GetComponent<AIPatroling> ().enabled = true;
//			GetComponent<MoveTo> ().enabled = false;
//		}

		Debug.DrawLine(aiPoint.position, playerPoint.position, Color.red);
		Debug.Log (detectDistance);
	}
}
