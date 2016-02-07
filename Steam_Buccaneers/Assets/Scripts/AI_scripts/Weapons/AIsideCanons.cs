using UnityEngine;
using System.Collections;

public class AIsideCanons : MonoBehaviour {

	public GameObject cannonball;
	public GameObject[] leftCannons;
	public GameObject[] rightCannons;
	public float fireRate;
	public float fireDelayLeft;
	public float fireDelayRight;
	public GameObject cannonL1;
	public GameObject cannonL2;
	public GameObject cannonL3;
	public GameObject cannonR1;
	public GameObject cannonR2;
	public GameObject cannonR3;

	private Vector3 right;
	private Vector3 left;

	public static bool fireLeft = false;
	public static bool fireRight = false;

	private int detectDistance = 50;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		right = this.transform.TransformDirection(Vector3.right);
		left = this.transform.TransformDirection(Vector3.left);

		Debug.DrawRay(this.transform.position, right * detectDistance, Color.green);
		Debug.DrawRay(this.transform.position, left * detectDistance, Color.blue);

		checkGunPosition();


		if (fireLeft == true && Time.time > fireDelayLeft) { // && Inventory.mainAmmo > 0
			fireDelayLeft = Time.time + fireRate;
			Debug.Log("fire left");

			Instantiate (cannonball, cannonL1.transform.position, cannonL1.transform.rotation);
			Instantiate (cannonball, cannonL2.transform.position, cannonL2.transform.rotation);
			Instantiate (cannonball, cannonL3.transform.position, cannonL3.transform.rotation);
			//AudioSource pangPang = GetComponent<AudioSource> ();
			//transform.Translate (Vector3.up/forwardSpeed);
		}

		if (fireRight == true && Time.time > fireDelayRight) {				
			fireDelayRight = Time.time + fireRate;
			Debug.Log("fire right");

			Instantiate (cannonball, cannonR1.transform.position, transform.rotation);
			Instantiate (cannonball, cannonR2.transform.position, transform.rotation);
			Instantiate (cannonball, cannonR3.transform.position, transform.rotation);
		}
	}

	private void checkGunPosition()
	{
		RaycastHit objectHit;

		if(Physics.Raycast(this.transform.position, left, out objectHit, detectDistance)) //Raycast hit something
		{
			if(objectHit.transform.tag == "Player")//Hit the player
			{
				fireLeft = true;//The AI can now fire
			}
			else //Hit something else than the player
			{
				fireLeft = false; //The AI cant fire anyway
			}
		}
		else //Hit nothing
		{
			fireLeft = false; //The AI cant fire
		}

		if(Physics.Raycast(this.transform.position, right, out objectHit, detectDistance))//Raycast hit something
		{
			if(objectHit.transform.tag == "Player")//Hit the player
			{
				fireRight = true;//The AI can now fire
			}

			else //Hit something else than the player
			{
				fireRight = false; //The AI cant fire anyway
			}
		}
		else//Hit nothing
		{
			fireRight = false;//The AI cant fire
		}
	}
}