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

		if (distance >= 500)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			Debug.Log("We hit the player");
			GameControl.control.health -= damageOutput;
		}

		if(other.tag == "aiShip") //The AI hit itself
		{
			Debug.Log("We hit an ai");
			other.transform.GetComponentInParent<AIMaster>().aiHealth -= damageOutput;
			other.GetComponentInParent<AIMaster>().aiHealth -= damageOutput;
		}

		Destroy(this.gameObject);
	}
}
