using UnityEngine;
using System.Collections;

public class RotateCogRigh : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.root.name != "PlayerShip") //The player ship has different rotation axises, so we need to make a check to see if this is the player or not
			this.transform.Rotate(0, 50 * Time.deltaTime, 0); //Rotate "this" object, meaning this cog
		else //This cog is on the player object
			this.transform.Rotate(50 * Time.deltaTime, 0, 0); //Rotate "this" object, meaning this cog
	}
}
