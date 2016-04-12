using UnityEngine;
using System.Collections;

public class DeadAI : MonoBehaviour {

	GameObject playerPos;
	float distance;
	public Vector3 axisOfRotation;
	public float angularVelocity;

	private float rotateTimer = 0;
	private float rotateDuration = 5;

	// Use this for initialization
	void Start () 
	{
		playerPos = GameObject.Find("PlayerShip");
		axisOfRotation = Random.onUnitSphere;
		angularVelocity = Random.Range (20, 40);

	}

	void Update()
	{
		distance = Vector3.Distance(transform.position, playerPos.transform.position);
		if(distance >= 250)
		{
			Destroy(gameObject);
		}
		rotateDuration -= Time.deltaTime;
		if(rotateDuration > rotateTimer)
			this.transform.Rotate(axisOfRotation, angularVelocity * Time.smoothDeltaTime * rotateDuration * 0.5f); //Rotates the object

	}
}
