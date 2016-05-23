using UnityEngine;
using System.Collections;

// The script is named poorly, as it is in fact asteroids and not meteors
public class OrbitMeteorsInner : MonoBehaviour 
{
	Vector3 rotateLocalVec; // a vector for the asteroids local rotation

	// Use this for initialization
	void Start () 
	{
		// generates a random vector for the asteroids rotation around the axes
		rotateLocalVec = new Vector3 (Mathf.RoundToInt(Random.Range(-1,1)), 
		Mathf.RoundToInt(Random.Range(-1,1)),Mathf.RoundToInt(Random.Range(-1,1)));
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		// rotates the asteroid around the parent, which is a planet
		this.transform.RotateAround (this.transform.parent.position, Vector3.up, .10f);
		// rotates the asteroid around their randomly generated axes, with a random speed between 0.5 and 1
		this.transform.Rotate(rotateLocalVec * Random.Range(.5f,1f));
	}


}
