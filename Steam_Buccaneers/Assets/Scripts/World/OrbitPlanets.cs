using UnityEngine;
using System.Collections;

public class OrbitPlanets : MonoBehaviour 
{
	Rigidbody rb;


	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//i steden for å dele på masse burde jeg heller dele på avstanden mellom planetene og sola*masse
		transform.RotateAround (this.transform.parent.position, transform.up, .5f / rb.mass);

	}

}
