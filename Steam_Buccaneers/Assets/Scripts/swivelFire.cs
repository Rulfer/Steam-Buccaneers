﻿using UnityEngine;
using System.Collections;

public class swivelFire : MonoBehaviour 
{

	public GameObject cannonball;
	public float fireRate;
	public float fireDelay;
	public int shotSpeed = 30;
	//public AudioSource pewPew;
	//public Vector3 position;
	//public Quaternion rotation;
	//public GameObject shotSpawn;
	// Use this for initialization
	void Start () 
	{
		//AudioSource pewPew = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton ("Fire1") && Time.time > fireDelay) // && Inventory.swivelAmmo > 0
		{
			//AudioSource pewPew = GetComponent<AudioSource> ();
			//Debug.Log ("pew");
			fireDelay = Time.time + fireRate;
			Instantiate (cannonball, transform.position, transform.rotation);
			//pewPew.Play();
			//Inventory.swivelAmmo -= 1;
			//Debug.Log (Inventory.swivelAmmo);
			//transform.TransformDirection(Vector3(0,0,shotSpeed));

		}
	
	}
}
