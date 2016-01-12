using UnityEngine;
using System.Collections;

public class AIprojectile : MonoBehaviour {

	public float projectileSpeed;
	public static float damageOutput;
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
	//	transform.Translate (Vector3.right * projectileSpeed * Time.deltaTime);


		distance = Vector3.Distance(transform.position, GameObject.Find("PlayerShip").transform.position);

		if (distance >= 40)
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Debug.Log ("We hit the player!");
			Debug.Log ("Damage delt is " + damageOutput);
			Destroy (this.gameObject);
		}
	}
}
