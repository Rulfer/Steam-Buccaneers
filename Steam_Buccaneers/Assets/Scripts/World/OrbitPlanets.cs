using UnityEngine;
using System.Collections;

public class OrbitPlanets : MonoBehaviour 
{
	//bool rayHit (Ray ray, out RaycastHit donger, float maxDistance = Mathf.Infinity);
	float distance;
	Ray ray;
	RaycastHit hit;
	Vector3 donger = new Vector3 (0f, -1f, 0f);
	float rotationSpeed;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{/*
		Vector3 bleugh = new Vector3 (this.transform.position.x, this.transform.position.y - 3000f, this.transform.position.z);
		Debug.DrawLine(this.transform.position, bleugh, Color.cyan,Mathf.Infinity,false);*/

		//Debug.Log (hit.transform.tag);
		Vector3 down = this.transform.TransformDirection(Vector3.down);
		Vector3 currentPos = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);
		distance = Vector3.Distance(this.transform.position, GameObject.Find("sun").transform.position);
		//i steden for å dele på masse burde jeg heller dele på avstanden mellom planetene og sola*masse
		// rb.mass
		// Just rotates the planets around the sun based on the distance between the sun and the planet, divided by 50, cuz that seemed like a good number to start with.
		transform.RotateAround (this.transform.parent.position, transform.up, rotationSpeed /(distance));
		//Debug.Log(this.transform.parent.rotation);

		if (this.transform.position.x > .6f)
		{
			//this.transform.localPosition = new Vector3(-currentPos.x, currentPos.y, currentPos.z);
		}
			
	}

	void FixedUpdate()
	{
		//Vector3 bleugh = new Vector3 (this.transform.position.x, this.transform.position.y - 3000f, this.transform.position.z);
		//Debug.DrawLine(this.transform.position, bleugh, Color.cyan,Mathf.Infinity,false);
		Vector3 currentPos = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);

		if (Physics.Raycast(this.transform.position, donger, out hit, Mathf.Infinity))
		{
			if (hit.transform.tag == ("endOfWorld"))
			{
				rotationSpeed = 10f;
				//Debug.Log ("dongeriSchlonger");
			}
			//Debug.Log (hit.transform.tag);

		}

		else
		{		
			rotationSpeed = 5000f;
			//Debug.Log ("dongeriSchlonger");
			//this.transform.localPosition = new Vector3(-currentPos.x, currentPos.y, currentPos.z);
		}
	}

}
