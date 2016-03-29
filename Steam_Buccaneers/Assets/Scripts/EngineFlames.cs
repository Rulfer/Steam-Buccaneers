﻿using UnityEngine;
using System.Collections;

public class EngineFlames : MonoBehaviour {

	private Rigidbody rigi;
	private Vector3 oldScale;
	private Vector3 newScale;
	private float tempZ;
	private float speed;
	private float xSpeed;
	private float zSpeed;

	private bool goingDown = false;
	private float scalingspeed;
	private float maxZ;
	private float lowerZ;
//	private float tempScaleUpperLimit;
//	private float tempScaleLowerLimit;
	// Use this for initialization
	void Start () {
		rigi = this.transform.root.GetComponent<Rigidbody>();
		scalingspeed = 0.005f;
		maxZ = 100;
		setNewLowerScale();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(this.gameObject.transform.localScale.z >= maxZ)
		{
			goingDown = true;
			setNewLowerScale();
		}

		else if(this.gameObject.transform.localScale.z <= lowerZ)
		{
			goingDown = false;
			setNewUpperScale();
		}

		if(goingDown == false)
		{
			Debug.Log("We are herpes");
			this.gameObject.transform.localScale += new Vector3(0, 0, scalingspeed);
		}
		else
		{
			this.gameObject.transform.localScale -= new Vector3(0, 0, scalingspeed);
		}

		Debug.Log(goingDown);
	}

	private void setNewLowerScale()
	{
		goingDown = true;

		Debug.Log("We are going down");
		lowerZ = maxZ * 0.9f;
		if(lowerZ < 0)
			lowerZ = 0;
		Debug.Log("lowerZ: " + lowerZ);
	}

	private void setNewUpperScale()
	{
		Debug.Log("We are going up");
		goingDown = false;

		xSpeed = rigi.velocity.x;
		zSpeed = rigi.velocity.z;
		if(xSpeed < 0)
			xSpeed *= -1;
		if(zSpeed < 0)
			zSpeed *= -1;
		speed = xSpeed + zSpeed;
		maxZ = speed * 0.2f * Time.deltaTime;
		if(maxZ < 0)
			maxZ = 0;
		Debug.Log("maxZ: " + maxZ);

	}
}
