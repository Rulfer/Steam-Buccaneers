using UnityEngine;
using System.Collections;

public class EngineFlames : MonoBehaviour {

	private Rigidbody rigi; //This objects rigid body
	private float speed; //Speed of the parent ship
	private float xSpeed; //Speed in x-direction
	private float zSpeed; //Speed in z-direction

	private bool goingDown = false; //Used to see if we should make the flame smaler or bigger
	private float scalingspeed; //How fast the scaling is
	private float maxZ; //Max allowed Z scale
	private float lowerZ; //Max allowed lower Z scale

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
		if(this.gameObject.transform.localScale.z >= maxZ) //The scale of the flame is greater than or equal to the maxZ variable
		{
			goingDown = true; //Scale it down
			setNewLowerScale(); //Generate new lower scale
		}

		else if(this.gameObject.transform.localScale.z <= lowerZ) //The scale of the flame is lower than or equal to the lowerZ variable
		{
			goingDown = false; //Scale it up
			setNewUpperScale(); //Generate new upper scale
		}

		if(goingDown == false) //Scaling up
		{
			this.gameObject.transform.localScale += new Vector3(0, 0, scalingspeed); //Increase scale based on scalingspeed
		}
		else //Scale the flame down
		{
			this.gameObject.transform.localScale -= new Vector3(0, 0, scalingspeed); //Decrease scale based on scalingspeed
		}
	}

	private void setNewLowerScale() //Generate new lower value
	{
		goingDown = true; //Scale down the flame

		lowerZ = maxZ * 0.9f; //Decrease to 90% of current scale
		if(lowerZ < 0) //Ship is at a standstill
			lowerZ = 0;
	}

	private void setNewUpperScale() //Generate new upper value
	{
		goingDown = false; //Scale up the flame

		xSpeed = rigi.velocity.x; //The velocity in x-direction
		zSpeed = rigi.velocity.z; //The velocity in z-direction
		if(xSpeed < 0) //Driving to the left
			xSpeed *= -1; //Make the number positive
		if(zSpeed < 0) //Driving downwards
			zSpeed *= -1; //Make the number positive
		speed = xSpeed + zSpeed; //Total speed
		maxZ = speed * 0.2f * Time.deltaTime; //Generate new max z scale
		if(maxZ < 0) //Ship is standing still
			maxZ = 0;
	}
}
