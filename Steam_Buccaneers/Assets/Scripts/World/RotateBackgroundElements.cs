using UnityEngine;
using System.Collections;

//This class rotates galaxies in the background
public class RotateBackgroundElements : MonoBehaviour 
{
	Vector3 rotateVec = new Vector3(0f,0f,1f);// vector for rotating the object in the z-axis
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		this.transform.Rotate(rotateVec, 0.001f); // rotates it in the vector declared at the start of the script, in the speed of 0.01
	}
}
