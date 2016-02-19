using UnityEngine;
using System.Collections;

public class AIprojectile : MonoBehaviour {

	private float projectileSpeed = 120;
	public int damageOutput;
	private float distance;
	public Rigidbody test;

	// Use this for initialization
	void Start () 
	{
		test.AddForce (this.transform.right * projectileSpeed);
		damageOutput = 3;
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
			Debug.Log("Player lost " + damageOutput + " health");
			Destroy (this.gameObject);
		}

		if(other.tag == "aiShip") //The AI hit itself
		{
			AIMaster.aiHealth -= damageOutput;
			Destroy(this.gameObject);
		}
	}
}
