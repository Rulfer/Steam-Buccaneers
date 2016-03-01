using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	//public Vector3 test = PlayerMove2.donger.velocity;
	private float projectileSpeed = 125;
	private float distance;
	public Rigidbody test;


	void Start () 
	{
		test.AddForce (this.transform.right * projectileSpeed);
	}

	void Update ()
	{
		// Calculates the distance between the traveling cannonballs and the player ship.
		distance = Vector3.Distance(transform.position, GameObject.Find("PlayerShip").transform.position);
		//Debug.Log (distance);

		// if the distance between the player ship and the traveling cannonball is greater or equal 40, this will get destroyed.
		if (distance >= 100)
		{
			Destroy(gameObject);
		}
	}

}
