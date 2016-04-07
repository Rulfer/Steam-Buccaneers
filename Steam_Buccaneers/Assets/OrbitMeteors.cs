using UnityEngine;
using System.Collections;

public class OrbitMeteors : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		this.transform.position = this.transform.parent.position;
		this.transform.RotateAround (this.transform.parent.position, Vector3.up, .5f);
	}
}
