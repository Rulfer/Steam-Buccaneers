using UnityEngine;
using System.Collections;

public class RotateEngineCog : MonoBehaviour {
	private float speed;
	private float xspeed;
	private float zspeed;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		xspeed =PlayerMove.player.velocity.x;
		if(xspeed < 0)
			xspeed *= -1;

		zspeed = PlayerMove.player.velocity.z;
		if(zspeed < 0)
			zspeed *= -1;
		
		speed = xspeed + zspeed;
		this.transform.Rotate(0, 5*Time.deltaTime * speed, 0);

	}
}
