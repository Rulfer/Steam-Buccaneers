using UnityEngine;
using System.Collections;

public class DeadAI : MonoBehaviour {

	private Rigidbody rb;
	public Vector3 forcePos;
	public float force = 5;
	// Use this for initialization
	void Start () 
	{
		rb = this.GetComponent<Rigidbody>();
	}
	
	public void createForce()
	{
		rb.constraints = RigidbodyConstraints.None;
		rb.constraints = RigidbodyConstraints.FreezePositionY;
		rb.AddExplosionForce(force, forcePos, 2);
	}
}
