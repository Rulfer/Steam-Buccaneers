using UnityEngine;
using System.Collections;

public class DeadAI : MonoBehaviour {

	GameObject playerPos;
	float distance;
	public Vector3 axisOfRotation;
	public Vector3 angularVelocity;

	// Use this for initialization
	void Start () 
	{
		playerPos = GameObject.Find("PlayerShip");
	}

	void Update()
	{
		distance = Vector3.Distance(transform.position, playerPos.transform.position);
		if(distance >= 100)
		{
			Destroy(gameObject);
		}

		this.transform.Rotate(axisOfRotation, angularVelocity * Time.smoothDeltaTime); //Rotates the object

	}
}
