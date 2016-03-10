using UnityEngine;
using System.Collections;
using EZCameraShake;

public class AIprojectile : MonoBehaviour {
	private float projectileSpeed = 175;
	public int damageOutput;
	private float distance;
	public Rigidbody test;

	CameraShakeInstance shake;

	// Use this for initialization
	void Start () 
	{
		test.AddForce (this.transform.right * projectileSpeed);
	}
	
	// Update is called once per frame
	void Update () 
	{
		distance = Vector3.Distance(transform.position, GameObject.Find("PlayerShip").transform.position);

		if (distance >= 500)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			Debug.Log("We hit the player");
			GameControl.control.health -= damageOutput;
			CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(1, 5, 0.10f, 0.8f);
		}

		if(other.tag == "aiShip") //The AI hit itself
		{
			Debug.Log("We hit an ai");
			other.transform.GetComponentInParent<AIMaster>().aiHealth -= damageOutput;
			other.GetComponentInParent<AIMaster>().aiHealth -= damageOutput;
		}

		Destroy(this.gameObject);
	}
}
