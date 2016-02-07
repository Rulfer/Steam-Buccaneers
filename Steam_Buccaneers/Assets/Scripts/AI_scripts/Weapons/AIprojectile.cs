using UnityEngine;
using System.Collections;

public class AIprojectile : MonoBehaviour {

	private float projectileSpeed = 100;
	public static int damageOutput;
	private float distance;
	public Rigidbody test;

	// Use this for initialization
	void Start () 
	{
		if (this.tag == "ball1") 
		{
			damageOutput = 1;
		}
		if (this.tag == "ball2") 
		{
			damageOutput = 2;
		}

		test.AddForce (this.transform.right * projectileSpeed);
	}
	
	// Update is called once per frame
	void Update () 
	{
		distance = Vector3.Distance(transform.position, GameObject.Find("PlayerShip").transform.position);

		if (distance >= 100)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			GameControl.control.health -= damageOutput;
			Destroy (this.gameObject);
		}

		if(other.tag == "aiShip") //The AI hit itself
		{
			AIMaster.aiHealth -= damageOutput;
			Destroy(this.gameObject);
		}
	}
}
