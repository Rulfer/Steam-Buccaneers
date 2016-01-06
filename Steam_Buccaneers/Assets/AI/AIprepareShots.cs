using UnityEngine;
using System.Collections;

public class AIprepareShots : MonoBehaviour {

	public bool hitSomething = false;
	public float traceDistance;

	void Start()
	{
		hitSomething = false;
		Debug.Log (traceDistance);
	}

	void Update()
	{
		Debug.Log ("working?");
		transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * 5, 0.0f, Input.GetAxis("Vertical") * Time.deltaTime * 5, Space.World);
		RaycastHit hit;

		Debug.Log ("y is dis not a ding");

		if (Physics.Raycast (transform.position, Vector3.left, out hit, traceDistance)) 
		{
			if (hit.collider.gameObject.name == "Player") 
			{
				hitSomething = true;
				Debug.Log ("oupsie");
			}
		}

		if (Physics.Raycast (transform.position, Vector3.right, out hit, traceDistance)) 
		{
			if (hit.collider.gameObject.name == "Player") 
			{
				hitSomething = true;
				Debug.Log ("oupsie");
			}
		}

		if (Physics.Raycast (transform.position, Vector3.forward, out hit, traceDistance)) 
		{
			if (hit.collider.gameObject.name == "Player") 
			{
				hitSomething = true;
				Debug.Log ("oupsie");
			}
		}

		if (Physics.Raycast (transform.position, Vector3.back, out hit, traceDistance)) 
		{
			if (hit.collider.gameObject.name == "Player") 
			{
				hitSomething = true;
				Debug.Log ("oupsie");
			}
		}

		if (Physics.Raycast (transform.position, Vector3.up, out hit, traceDistance)) 
		{
			if (hit.collider.gameObject.name == "Player") 
			{
				hitSomething = true;
				Debug.Log ("oupsie");
			}
		}

		if (Physics.Raycast (transform.position, Vector3.down, out hit, traceDistance)) 
		{
			if (hit.collider.gameObject.name == "Player") 
			{
				hitSomething = true;
				Debug.Log ("oupsie");
			}
		}
		Debug.Log ("fant nada");
	}
}
