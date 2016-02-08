using UnityEngine;
using System.Collections;

public class OrbitMoons : MonoBehaviour 
{
	//GameObject planet;



	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.RotateAround (this.transform.parent.position, transform.up, .2f);
	}
}
