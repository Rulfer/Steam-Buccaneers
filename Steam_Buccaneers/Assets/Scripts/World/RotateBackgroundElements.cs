using UnityEngine;
using System.Collections;

public class RotateBackgroundElements : MonoBehaviour 
{
	Vector3 rotateVec = new Vector3(0f,0f,1f);

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		this.transform.Rotate(rotateVec, 0.01f);
		

	}
}
