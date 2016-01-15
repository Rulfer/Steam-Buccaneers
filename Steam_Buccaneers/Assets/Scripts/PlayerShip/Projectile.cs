using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	//public Vector3 test = PlayerMove2.donger.velocity;
	public static float projectileSpeed = /* PlayerMove2.donger.velocity.z + PlayerMove2.donger.velocity.x +*/ 20.0f;
	private float distance;

	void Start () 
	{
		//Rigidbody.velocity = transform.forward * projectileSpeed;
	}

	void Update ()
	{
		//float projectileSpeed = (test.x + test.z) + 2.0f;
		//Siden swivel er bugga, så har denne blitt endra på
		//transform.Translate (Vector3.up* projectileSpeed* Time.deltaTime);
		transform.Translate (Vector3.right* projectileSpeed* Time.deltaTime);
		// Calculates the distance between the traveling cannonballs and the player ship.
		distance = Vector3.Distance(transform.position, GameObject.Find("PlayerShip").transform.position);
		//Debug.Log (distance);

		// if the distance between the player ship and the traveling cannonball is greater or equal 40, this will get destroyed.
		if (distance >= 40)
		{
			Destroy(gameObject);
		}
	}

}
