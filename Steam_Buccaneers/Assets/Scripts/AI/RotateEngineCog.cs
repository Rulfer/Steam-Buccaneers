using UnityEngine;
using System.Collections;

public class RotateEngineCog : MonoBehaviour {
	private float speed; //Speed of rotation
	private float xspeed; //The players speed in x-direction
	private float zspeed; //The players speed in z-direction
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		xspeed = PlayerMove.player.velocity.x; //Set xspeed equal to players velocity in x-direction
		if(xspeed < 0) //If the speed is negative, the player is driving downwards
			xspeed *= -1; //Multiply the number with -1 to get a positive value

		zspeed = PlayerMove.player.velocity.z; //Set zspeed equal to players velocity in z-direction
		if(zspeed < 0) //If the speed is negative, the player is driving to the left
			zspeed *= -1; //Multiply the number with -1 to get a positive value
		
		speed = xspeed + zspeed; //Calculates the speed value
		this.transform.Rotate(0, 5*Time.deltaTime * speed, 0); //Rotates the cog based on the speed variabel. This means that the rotation is based on the players velocity

	}
}
