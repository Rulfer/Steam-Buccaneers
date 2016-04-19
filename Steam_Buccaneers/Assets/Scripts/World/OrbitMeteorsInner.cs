using UnityEngine;
using System.Collections;

public class OrbitMeteorsInner : MonoBehaviour 
{
	Vector3 rotateLocalVec;
	GameObject player;
	int crashdamage;
	Rigidbody asteroidV;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find ("PlayerShip");
		rotateLocalVec = new Vector3 (Mathf.RoundToInt(Random.Range(-1,1)), 
		Mathf.RoundToInt(Random.Range(-1,1)),Mathf.RoundToInt(Random.Range(-1,1)));
	
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player")
		{
			crashdamage = Mathf.RoundToInt (PlayerMove2.donger.velocity.magnitude);
			GameControl.control.health -= crashdamage;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//this.transform.position = this.transform.parent.position;
		this.transform.RotateAround (this.transform.parent.position, Vector3.up, .10f);
		this.transform.Rotate(rotateLocalVec * Random.Range(.5f,1f));
	}


}
