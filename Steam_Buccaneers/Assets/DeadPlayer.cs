using UnityEngine;
using System.Collections;

public class DeadPlayer : MonoBehaviour 
{
	public GameObject boom;
	public Vector3 axisOfRotation;
	public float angularVelocity;

	private float rotateTimer = 0;
	private float rotateDuration = 5;

	// Use this for initialization
	void Start () 
	{
		axisOfRotation = Random.onUnitSphere;
		angularVelocity = Random.Range (20, 40);
		this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		Instantiate(boom, this.transform.position, this.transform.rotation);

	}

	void Update()
	{
		rotateDuration -= Time.deltaTime;
		if(rotateDuration > rotateTimer)
			this.transform.Rotate(axisOfRotation, angularVelocity * Time.smoothDeltaTime * rotateDuration * 0.5f); //Rotates the object
	}
}
