using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public float projectileSpeed = 1;

	void Start () 
	{
		//Rigidbody.velocity = transform.forward * projectileSpeed;
	
	}

	void Update ()
	{
		//Siden swivel er bugga, så har denne blitt endra på
		//transform.Translate (Vector3.up* projectileSpeed* Time.deltaTime);
		transform.Translate (Vector3.right* projectileSpeed* Time.deltaTime);
	}

}
