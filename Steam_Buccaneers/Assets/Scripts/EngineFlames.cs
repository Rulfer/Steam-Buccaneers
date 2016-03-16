using UnityEngine;
using System.Collections;

public class EngineFlames : MonoBehaviour {

	private Rigidbody rigi;
	private Vector3 oldScale;
	private Vector3 newScale;
	private float tempZ;
	private float speed;
	private float xSpeed;
	private float zSpeed;
	// Use this for initialization
	void Start () {
		
		rigi = this.transform.root.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		xSpeed = rigi.velocity.x;
		zSpeed = rigi.velocity.z;
		if(xSpeed < 0)
			xSpeed *= -1;
		if(zSpeed < 0)
			zSpeed *= -1;
		speed = xSpeed + zSpeed;
		tempZ = speed * Time.deltaTime * 0.5f;//Mathf.PingPong(oldScale, newScale, speed);
		this.transform.localScale = new Vector3(0.03f, 0.03f, tempZ);
	}
}
