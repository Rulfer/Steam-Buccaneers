using UnityEngine;
using System.Collections;

public class PickupScrap : MonoBehaviour {

	public GameObject[] scrapArray; //Every scrap object on the scene
	private Vector3 temp; //the difference between player and scap position

	private int pickupRange; //The range of the "magnet"

	// Use this for initialization
	void Start () {
		pickupRange = 30;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		scrapArray = GameObject.FindGameObjectsWithTag("scrap"); //Holds all scrap objects in this array
		for(int i = 0; i < scrapArray.Length; i++)
		{
			temp = transform.position - scrapArray[i].transform.position; 
			scrapArray[i].GetComponent<Rigidbody>().AddForce(temp.normalized * (Mathf.Max((pickupRange-temp.magnitude)/2.5f,0)), ForceMode.VelocityChange);
		}
	}
}
