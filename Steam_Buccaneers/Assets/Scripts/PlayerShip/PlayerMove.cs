﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using EZCameraShake;

public class PlayerMove : MonoBehaviour 
{
	public static PlayerMove move;
	public static Rigidbody player;
	public Vector3 stopRotatingShitface = new Vector3 (90f,180f,0f);
	public float force;
	private float boostingForce = 500f;
	public int turnSpeed = 50;
	public float playerTurn = 0.50f;
	public Vector3 maxVelocity;
	private Vector3 maxBoostVelocity = new Vector3 (1000f, 0f, 1000f);
	public static bool turnLeft = false;
	public static bool turnRight = false;
	public static bool goingForward = false;
	public static bool isBoosting = false;
	// bad name for this variable, this is in fact the pool of available boosting power/time
	public static float boostCooldownTimer = 3f;
	private bool boostCooledDown = false;
	CameraShakeInstance shake;
	private GameObject boostBar;
	private float waitForBoost = 3f;

	public static bool steerShip = true;

	public static bool hitBomb = false;
	private float bombTimer = 0;

	private GameObject TutorialControl;

	// Use this for initialization
	void Start () 
	{
		boostBar = GameObject.Find ("boost_bar");
		boostBar.SetActive (false);
		move = this;
		player = GetComponent<Rigidbody>();
		if (GameObject.Find ("TutorialControl") != null)
		{
			TutorialControl = GameObject.Find ("TutorialControl");
		} 
		else
		{
			Destroy (TutorialControl);
		}
			
		if (GameControl.control.thrusterUpgrade == 2)
		{
			force = 1400;
			maxVelocity = new Vector3 (force * 10, 0, force * 10);
			Debug.Log ("Skift fart " + force);
		} 
		else if (GameControl.control.thrusterUpgrade == 3)
		{
			force = 1900;
			maxVelocity = new Vector3 (force * 10, 0, force * 10);
			Debug.Log ("Skift fart " + force);
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		
		//Debug.Log("boosting timer: " + boostCooldownTimer + " cooled down? " + boostCooledDown + " boosting? " + isBoosting + " waiting for boost? " + waitForBoost);

		/*if (GameControl.control.isFighting == true && isBoosting == true && boostCooledDown == true)
		{
			maxVelocity *= 2;
			player.AddForce (transform.forward * force*2 * Time.deltaTime);
			boostCooldownTimer -= Time.deltaTime;
			//boostCooldownTimer -= Time.deltaTime;
		}*/

		// removing and adding the boosting bar
		if (GameControl.control.isFighting && !boostBar.activeSelf)
		{
			boostBar.SetActive(true);
		}

		else if (!GameControl.control.isFighting && boostBar.activeSelf && boostCooledDown)
		{
			boostBar.SetActive(false);
		}

		if(boostCooldownTimer <= 0f)
		{
			boostCooldownTimer = 0f;
			isBoosting = false;
			//CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(1, 5, 0.10f, 0.8f);
		}
		else if (boostCooldownTimer < 3f)
		{
			boostCooledDown = false;
		}

		if ((boostCooldownTimer < 3f && isBoosting == false && GameControl.control.isFighting == true) || 
			(boostCooldownTimer < 3f && GameControl.control.isFighting == false))
		{
			if (waitForBoost > -0.1f && waitForBoost < 3.1f && !Input.GetKey(KeyCode.LeftShift) && GameControl.control.isFighting)
			{
				waitForBoost -= Time.deltaTime;
			}
			if (!GameControl.control.isFighting && waitForBoost > 0)
			{
				waitForBoost = 0;
			}

			if (waitForBoost <= 0)
			{
				boostCooldownTimer +=Time.deltaTime/3;
				if (boostCooldownTimer >= 3f)
				{
					boostCooledDown = true;
					boostCooldownTimer = 3f;
					//boostCooledDown = true;
				}
			}
		}
			
		if(hitBomb == true)
		{
			bombTimer += Time.deltaTime;
			if(bombTimer >= 1)
			{
				bombTimer = 0;
				hitBomb = false;
				this.transform.root.GetComponent<Rigidbody>().mass = 1;
				this.transform.root.GetComponent<Rigidbody>().drag = 0.5f;
				this.transform.root.GetComponent<Rigidbody>().angularDrag = 0.5f;
			}
		}

		//if (steerShip == true) 
		//{
		if (hitBomb == false && GameControl.control.health > 0) 
		{
			if (Input.GetKey (KeyCode.LeftShift))
			{
				Boost();
			}

			else
			{
				isBoosting = false;
			}
			if (Input.GetKeyDown (KeyCode.LeftShift))
			{
				if (!GameControl.control.isFighting)
				{
					CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(1, 5, 0.10f, 0.8f);
				}

				else if (GameControl.control.isFighting && boostCooldownTimer > 0f)
				{
					//if (boostCooldownTimer <=
					waitForBoost = 3f;
					CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(1, 5, 0.10f, 0.8f);
				}
			}

			if (Input.GetKeyUp (KeyCode.LeftShift))
			{
				if (GameControl.control.isFighting && boostCooldownTimer > 0f)
				{
					waitForBoost = 3f;
				}
			}
				
			if (Input.GetKey (KeyCode.W) && isBoosting == false) 
			{
				if (TutorialControl != null && TutorialControl.GetComponent<Tutorial> ().wadCheck [0] == false)
				{
					TutorialControl.GetComponent<Tutorial> ().wadCheck [0] = true;
					TutorialControl.GetComponent<Tutorial> ().checkArray (TutorialControl.GetComponent<Tutorial> ().wadCheck);
				}
				goingForward = true;
				player.AddForce (transform.forward * force * Time.deltaTime);
				// Series of if tests
				if (player.velocity.x >= maxVelocity.x) 
				{ //|| -player.velocity.x >= -maxVelocity.x)
					// one type of fix, but it is far from correct, speed stays around the max velocity, but it also makes it a lot harder to accelerate
					// in the z-axis, although it does in fact accelerate.
					player.velocity = new Vector3 (maxVelocity.x, 0.0f, player.velocity.z);
				}

				if (player.velocity.x <= -maxVelocity.x) 
				{
					player.velocity = new Vector3 (-maxVelocity.x, 0.0f, player.velocity.z);
				}

				if (player.velocity.z >= maxVelocity.z) 
				{
					player.velocity = new Vector3 (player.velocity.x, 0.0f, maxVelocity.z);
				}

				if (player.velocity.z <= -maxVelocity.z) {
					player.velocity = new Vector3 (player.velocity.x, 0.0f, -maxVelocity.z);
				}
			} 
			else
			{
				goingForward = false;
			}
//		else 
//		{
//				//player.velocity = player.velocity * 0.90f;
//				//goingForward = false;
//			}


		if (Input.GetKey (KeyCode.A)) 
			{
				if (TutorialControl != null && TutorialControl.GetComponent<Tutorial> ().wadCheck [1] != true)
				{
					TutorialControl.GetComponent<Tutorial> ().wadCheck [1] = true;
				TutorialControl.GetComponent<Tutorial> ().checkArray (TutorialControl.GetComponent<Tutorial> ().wadCheck);
				}
				transform.Rotate (Vector3.down, turnSpeed * Time.deltaTime);
				turnLeft = true;
				
			} 
		else 
		{
			turnLeft = false;
		}

			if (Input.GetKey (KeyCode.D)) 
			{
			if (TutorialControl != null && TutorialControl.GetComponent<Tutorial> ().wadCheck [2] != true)
			{
				TutorialControl.GetComponent<Tutorial> ().wadCheck [2] = true;
				TutorialControl.GetComponent<Tutorial> ().checkArray (TutorialControl.GetComponent<Tutorial> ().wadCheck);
			}
				transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
				turnRight = true;
			}
		// ALT DETTE ER NYTT
		else 
			{
				turnRight = false;
			}
				
			// Just a test to see if how fast we can drive without problems, also for testing the possibilities for increasing max speed in the shop upgrade menus.
			if (Input.GetKeyDown (KeyCode.X)) {
				maxVelocity += new Vector3 (0.5f, 0.0f, 0.5f);
				Debug.Log (maxVelocity);
			}

		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Planet" || other.tag == "Moon")
		{
			GameControl.control.health = 0;
		}
	}

	void Boost()
	{
		//if the player is not in combat, boost is active as long as the player uses it
		if (!GameControl.control.isFighting)
		{
			maxVelocity *= 2;
			isBoosting = true;

			//propelling the player forward at double the speed
			player.AddForce (transform.forward * force*2 * Time.deltaTime);
		}

		// Testing if the player is in combat
		if (GameControl.control.isFighting == true && boostCooldownTimer > 0 /*boostCooledDown == true*/)
		{
			maxVelocity *= 2;
			player.AddForce (transform.forward * force*2 * Time.deltaTime);
			boostCooldownTimer -= Time.deltaTime;
			isBoosting = true;		
		}
	}
}
	
