using UnityEngine;
using System.Collections;
using EZCameraShake;

public class AIprojectile : MonoBehaviour {
	private float projectileSpeed = 175;
	public int damageOutput;
	private float distance;
	public Rigidbody test;
	public GameObject explotion;

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
			GameControl.control.health -= damageOutput;
			CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(1, 5, 0.10f, 0.8f);
			Instantiate(explotion);
			explotion.transform.position = this.transform.position;
			Destroy(this.gameObject);
		}

		if(other.tag == "aiShip") //The AI hit itself
		{
			other.transform.GetComponentInParent<AIMaster>().aiHealth -= damageOutput;
			other.GetComponentInParent<AIMaster>().aiHealth -= damageOutput;
			Instantiate(explotion);
			explotion.transform.position = this.transform.position;
			Destroy(this.gameObject);
		}

		if(other.tag == "shop" || other.tag == "Planet")
		{
			Instantiate(explotion);
			explotion.transform.position = this.transform.position;
			Destroy(this.gameObject);
		}
	}
}
