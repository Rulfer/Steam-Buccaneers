using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public static float forwardSpeed = 20;
	public static int turnSpeed = 20;
	public static int swingSpeed = 50;
	public float rotationPerSecond = 15f;
	public float rotationMax = 45f;
//	public AudioSource wubWub;

	// Use this for initialization
	void Start () 
	{
//		AudioSource wubWub = GetComponent<AudioSource> ();
//		wubWub.Stop ();
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		//må antakeligvis ha en timer for akkselerasjon/de
		if (Input.GetKey (KeyCode.W)) 
		{
			//AudioSource wubWub = GetComponent<AudioSource> ();
			transform.Translate (Vector3.up/forwardSpeed);
		}
		if (Input.GetKey (KeyCode.A)) 
		{
			transform.Rotate (Vector3.forward, turnSpeed * Time.deltaTime);
			//transform.Rotate (Vector3.up, swingSpeed * Time.deltaTime);
			//endRotation.transform.Rotate(Vector3.left, 360, Space.World);	
			//transform.Translate (Vector3.left/turnSpeed);
			//	Quaternion 
			//transform.rotation = Quaternion.AngleAxis
			//transform.Rotate
		}

		if (Input.GetKey (KeyCode.D)) 
		{
			transform.Rotate (Vector3.back, turnSpeed * Time.deltaTime);
			//transform.Rotate (Vector3.up, PlayerMovement.turnSpeed * Time.deltaTime);
			//transform.Rotate (Vector3.down, swingSpeed * Time.deltaTime);
			//endRotation.transform.Rotate(Vector3.right, 360, Space.World);
			//transform.Translate (Vector3.right/turnSpeed);
			//her blir det z og y +
		}

		//this.transform.rotation = Quaternion.Lerp(this.transform.rotation, endRotation.transform.rotation, Time.deltaTime*turnSpeed);
	
	}

	//public void rotateShip (Vector3 axis, float angle
}
