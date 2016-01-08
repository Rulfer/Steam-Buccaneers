using UnityEngine;
using System.Collections;

public class RotateShip : MonoBehaviour 
{


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.A)) 
		{
			transform.Rotate (-Vector3.back, (PlayerMove.turnSpeed*2) * Time.deltaTime);
		}
		
		if (Input.GetKey (KeyCode.D)) 
		{
			transform.Rotate (Vector3.back, (PlayerMove.turnSpeed*2) * Time.deltaTime);
		}
	
	}
}
