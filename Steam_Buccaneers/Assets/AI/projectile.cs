using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {

	public float projectileSpeed;
	Rigidbody rb;

	// Use this for initialization
	void Start () {

		rb = this.GetComponent<Rigidbody> ();
		rb.AddForce (this.transform.right * projectileSpeed);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
