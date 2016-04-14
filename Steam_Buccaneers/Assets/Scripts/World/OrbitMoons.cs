using UnityEngine;
using System.Collections;

public class OrbitMoons : MonoBehaviour 
{
	float distance;
	//GameObject planet;



	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log("Paused pls");
		distance = Vector3.Distance(this.transform.position, this.transform.parent.position);
		transform.RotateAround (this.transform.parent.position, transform.up, 3f / distance);
	}
}
