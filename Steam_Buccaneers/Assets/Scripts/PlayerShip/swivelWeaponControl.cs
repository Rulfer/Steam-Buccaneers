using UnityEngine;
using System.Collections;

public class SwivelWeaponControl : MonoBehaviour 
{
	private Camera mainCam; // the games main camera
	private Vector3 mousePos; // a vector holding the position of the mouse on screen

	// Use this for initialization
	void Start () 
	{
		mainCam = Camera.main; // finds the main game camera
	}
		
	// Update is called once per frame
	void FixedUpdate () 
	{
		//gets the position of the mouse cursor on screen
		mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.y));
		// a debug for drawing a line showing that the cannon is pointing at the tip of the cursor
		Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.back) * 100, Color.green);
		//always rotates and points the cannon towards the position of the cursor on screen
		transform.eulerAngles = new Vector3 (0,-Mathf.Atan2((mousePos.z - transform.position.z), (mousePos.x - transform.position.x))*Mathf.Rad2Deg +90,0);
	}
}
