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
	}
	
	// Update is called once per frame
	void Update () 
	{
		distance = Vector3.Distance(transform.position, GameObject.Find("PlayerShip").transform.position);

		if (distance >= 1000000)
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
			Debug.Log ("Stuff" +other.transform.GetComponentInParent<AIMaster>().aiHealth);
			other.transform.GetComponentInParent<AIMaster>().aiHealth -= damageOutput;

			other.GetComponentInParent<AIMaster>().aiHealth -= damageOutput;
			Destroy(this.gameObject);
		}
	}
}
