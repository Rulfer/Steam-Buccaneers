using UnityEngine;
using System.Collections;

public class OrbitMeteorsInner : MonoBehaviour 
{
	Vector3 rotateLocalVec;

	// Use this for initialization
	void Start () 
	{
		rotateLocalVec = new Vector3 (Mathf.RoundToInt(Random.Range(-1,1)), 
			Mathf.RoundToInt(Random.Range(-1,1)),Mathf.RoundToInt(Random.Range(-1,1)));
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//this.transform.position = this.transform.parent.position;
		this.transform.RotateAround (this.transform.parent.position, Vector3.up, .5f);
		this.transform.Rotate(rotateLocalVec * Random.Range(.5f,1f));
	}
}
