using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using EZCameraShake;

public class PlayerMove : MonoBehaviour 
{
	public static PlayerMove move;
	public static Rigidbody player; // the rigidbody of the player ship
	public float force; // how much force the player accelerates at
	public int turnSpeed = 50; // variable for the turning speed of the player
	public Vector3 maxVelocity; // vector for the max velocity the player can drive at
	public static bool turnLeft = false; // bool for testing if the player is turning left
	public static bool turnRight = false; // bool for testing if the player is turning right
	public static bool goingForward = false; // bool for testing if the player is moving forward
	public static bool isBoosting = false; // a bool for testing if the player is boosting or not
	public static float boostCooldownTimer = 3f;// bad name for this variable, this is in fact the pool of available boosting power/time
	private bool boostCooledDown = false; //a bool for testing if the player can boost again
	CameraShakeInstance shake;
	private GameObject boostBar; // the boost bar game object
	private float waitForBoost = 3f; // variable used for the timer of the boost to cool down

	public static bool hitBomb = false; // bool for testing if the player was hit by a bomb
	private float bombTimer = 0;

	private GameObject TutorialControl; //

	// Use this for initialization
	void Start () 
	{
		boostBar = GameObject.Find ("boost_bar"); // finding and declaring the boost bar
		boostBar.SetActive (false); // immediately setting it inactive
		move = this;
		player = GetComponent<Rigidbody>(); // get the players rigidbody
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
		// removing and adding the boosting bar
		// if the player is in combat, and the boost bar is not active, set it active
		if (GameControl.control.isFighting && !boostBar.activeSelf)
		{
			boostBar.SetActive(true);
		}

		// if the player is not in combat, and the boostbar is both active and full, set it inactive
		else if (!GameControl.control.isFighting && boostBar.activeSelf && boostCooledDown)
		{
			boostBar.SetActive(false);
		}

		if(boostCooldownTimer <= 0f) 
		{
			boostCooldownTimer = 0f;
			isBoosting = false;
		}
		else if (boostCooldownTimer < 3f)
		{
			boostCooledDown = false;
		}

		// if the boosting resource is not full, and the player is either not boosting while in combat, OR is not in combat
		if ((boostCooldownTimer < 3f && isBoosting == false && GameControl.control.isFighting == true) || 
			(boostCooldownTimer < 3f && GameControl.control.isFighting == false))
		{
			// if the timer that starts the replenishing of the boosting resource is between -0.1 and 3.1, not pressing down left shift
			// and in combat, then the timer for replenishing the boosting resource should start counting down
			if (waitForBoost > -0.1f && waitForBoost < 3.1f && !Input.GetKey(KeyCode.LeftShift) && GameControl.control.isFighting)
			{
				waitForBoost -= Time.deltaTime;
			}
			// if the player is not in combat, and the timer for waiting for the boost to replenish is above 0, it is set to 0 to start
			// filling up the boost meter immediately
			if (!GameControl.control.isFighting && waitForBoost > 0)
			{
				waitForBoost = 0;
			}

			// if the timer for the boost has reached or is below 0, it should start replenishing the boosting resource
			if (waitForBoost <= 0)
			{
				boostCooldownTimer +=Time.deltaTime/3;// replenishing the boosting resource at the time it takes to complete a frame divided by 3
				if (boostCooldownTimer >= 3f) // if the boost cooldown timer has reached its max number or above
				{
					boostCooledDown = true; // the boost is set to have been cooled down
					boostCooldownTimer = 3f; // the boosting resource is 3
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
			
		if (hitBomb == false && GameControl.control.health > 0) // if the player has not been hit, and is alive
		{
			if (Input.GetKey (KeyCode.LeftShift)) // the key that enables the player to boost
			{
				if (GameControl.control.isFighting && boostCooldownTimer > 0)
				{
					Boost(); // calling the boosting function
				}

				else if (!GameControl.control.isFighting)
				{
					Boost();
				}

			}

			else // if not left shift is down, the player is not boosting
			{
				isBoosting = false;
			}

			// checking only if the player has pressed down the button, not if is actively down
			if (Input.GetKeyDown (KeyCode.LeftShift))
			{
				if (!GameControl.control.isFighting) // if the player is not in combat
				{
					// the camera shakes as you press the boost button
					CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(1, 5, 0.10f, 0.8f);
				}
				// if the player is in combat, and the boost resource is above 0, ie. can boost
				else if (GameControl.control.isFighting && boostCooldownTimer > 0f)
				{
					// the camera shakes as you press the boost button
					CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(1, 5, 0.10f, 0.8f);
				}
			}

			// if the player is pressing the forwards key, and is not boosting
			if (Input.GetKey (KeyCode.W) && isBoosting == false) 
			{
				if (TutorialControl != null && TutorialControl.GetComponent<Tutorial> ().wadCheck [0] == false)
				{
					TutorialControl.GetComponent<Tutorial> ().wadCheck [0] = true;
					TutorialControl.GetComponent<Tutorial> ().checkArray (TutorialControl.GetComponent<Tutorial> ().wadCheck);
				}
			
				goingForward = true; // the player is moving forward
				player.AddForce (transform.forward * force * Time.deltaTime); //adding force to the player so that they accelerate

				// Series of if tests seeing if the player can accelerate in the diffrent directions in the x- and z-axis
				if (player.velocity.x >= maxVelocity.x) 
				{
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
			else // if this is not true, the player is not moving forward
			{
				goingForward = false;
			}

		if (Input.GetKey (KeyCode.A)) // if the player is turning to the left
			{
				if (TutorialControl != null && TutorialControl.GetComponent<Tutorial> ().wadCheck [1] != true)
				{
					TutorialControl.GetComponent<Tutorial> ().wadCheck [1] = true;
				TutorialControl.GetComponent<Tutorial> ().checkArray (TutorialControl.GetComponent<Tutorial> ().wadCheck);
				}
				// we rotate the player to the left using the turn speed * the frames in game in real time
				transform.Rotate (Vector3.down, turnSpeed * Time.deltaTime); 
				turnLeft = true; // the player is turning left
				
			}

		else // if the button press is not true, the player is not turning left
		{
			turnLeft = false;
		}

			if (Input.GetKey (KeyCode.D)) // if the player is turning right
			{
			if (TutorialControl != null && TutorialControl.GetComponent<Tutorial> ().wadCheck [2] != true)
			{
				TutorialControl.GetComponent<Tutorial> ().wadCheck [2] = true;
				TutorialControl.GetComponent<Tutorial> ().checkArray (TutorialControl.GetComponent<Tutorial> ().wadCheck);
			}
				// we rotate the player to the right using the turn speed * the frames in game in real time
				transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime); 
				turnRight = true; // the player is turning right
			}

		else // if the button press is not true, then we are not turning right
			{
				turnRight = false;
			}
		}
	}

	void OnTriggerEnter(Collider other) // if the player is colliding
	{
		if (other.tag == "Planet" || other.tag == "Moon") // if the player is colliding with either a moon or a planet
		{
			GameControl.control.health = 0; // the players health is set to 0 and instantly dies
		}
	}

	void Boost() // function for boosting
	{
		maxVelocity *= 2; // while boosting the player can move at double the speed
		isBoosting = true; // the player is indeed boosting

		// the player accelerates at double the normal speed per second
		player.AddForce (transform.forward * force*2 * Time.deltaTime);


		if (GameControl.control.isFighting)// if the player is in combat, excecute the following things
		{
			boostCooldownTimer -= Time.deltaTime; // the boosting resource is used, and reduced by the time it takes to complete that frame
			if (isBoosting && waitForBoost != 3f) // if the player is boosting and is not yet set to 3, set it to 3
			{
				waitForBoost = 3f; // the timer for the boost to replenish is set to 3 seconds
			}
		}
	}
}
	

