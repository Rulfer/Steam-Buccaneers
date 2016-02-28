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
		if(other.gameObject != null)
		{
			if (other.tag == "Player") 
			{
				GameControl.control.health -= damageOutput;
				Destroy (this.gameObject);
			}

			if(other.tag == "aiShip") //The AI hit itself
			{
				other.transform.GetComponentInParent<AIMaster>().aiHealth -= damageOutput;

				other.GetComponentInParent<AIMaster>().aiHealth -= damageOutput;
				Destroy(this.gameObject);
			}
		}
	}
}
