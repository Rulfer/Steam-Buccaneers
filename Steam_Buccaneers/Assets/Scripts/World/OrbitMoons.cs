using UnityEngine;
using System.Collections;

public class OrbitMoons : MonoBehaviour 
{
	float distance; // variable for the distance between the object and its parent

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(MinimapCamera.miniCam.isMinimap == true)
		{
			// measuring the distance between the moon and its parent planet
			distance = Vector3.Distance(this.transform.position, this.transform.parent.position);
			// rotates the moon around its parent planet, with the speed of 3 divided by its distance to the parent
			transform.RotateAround (this.transform.parent.position, transform.up, 3f / distance);
		}
	}
}
