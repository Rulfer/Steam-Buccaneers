using UnityEngine;
using System.Collections;

public class PlayerMove2 : MonoBehaviour 
{
	public static PlayerMove2 move;
	public static Rigidbody player;
	public Vector3 stopRotatingShitface = new Vector3 (90f,180f,0f);
	public float force = 200.0f;
	private float boostingForce = 500f;
	public int turnSpeed = 50;
	public float playerTurn = 0.50f;
	public Vector3 maxVelocity = new Vector3 (500f, 0.0f, 500f);
	private Vector3 maxBoostVelocity = new Vector3 (1000f, 0f, 1000f);
	public static bool turnLeft = false;
	public static bool turnRight = false;
	public static bool goingForward = false;
	public bool isBoosting = false;
	private float boostCooldownTimer = 3f;
	private bool boostCooledDown;

	public static bool steerShip = true;

	public static bool hitBomb = false;
	private float bombTimer = 0;

	private GameObject TutorialControl;

	private AudioSource source;
	public AudioClip[] clips;
	private int clip = 2;

	// Use this for initialization
	void Start () 
	{
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
		source = this.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{

		if (boostCooldownTimer < 3f || isBoosting == false)
		{
			boostCooldownTimer +=Time.deltaTime;
			if (boostCooldownTimer >= 3f)
			{
				boostCooldownTimer = 3f;
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
			if (Input.GetKey (KeyCode.W)) 
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
		//}


		if(goingForward == true || turnLeft == true || turnRight == true)
		{
			if(source.isPlaying == false && clip == 0)
				playLoopSound();
			else if(source.isPlaying == false && clip != 0)
				playStartSound();
		}
		else if(goingForward == false && turnLeft == false && turnRight == false)
		{
			if(clip != 2)
				playEndSound();
		}
		//}
	}

	private void playStartSound()
	{
		source.clip = clips[0];
		source.loop = false;
		source.Stop();
		source.Play();
		clip = 0;
	}

	private void playLoopSound()
	{
		source.clip = clips[1];
		source.loop = true;
		source.Stop();
		source.Play();
		clip = 1;
	}

	private void playEndSound()
	{
		source.clip = clips[2];
		source.loop = false;
		source.Stop();
		source.Play();	
		clip = 2;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Planet")
		{
			GameControl.control.health = 0;
		}
	}

	void Boost()
	{
		//Setting the players maximum velocity to a higher amount
		maxVelocity = maxBoostVelocity;

		//if the player is not in combat, boost is active as long as the player uses it
		while (SpawnAI.spawn.stopSpawn != true)
		{
			isBoosting = true;
			Debug.Log ("beleoelge");

			//maxVelocity = maxBoostVelocity;
			//propelling the player forward at double the speed
			player.AddForce (transform.forward * force*2 * Time.deltaTime);

			// Testing if the player is in combat
			break;
		}

		while (SpawnAI.spawn.stopSpawn == true)
		{
			isBoosting = true;
			while (boostCooldownTimer >=0)
			{
				
				player.AddForce (transform.forward * force*2 * Time.deltaTime);
				boostCooldownTimer -= Time.deltaTime;
				//return boostCooldownTimer;

			}
			break;
			//bool boostCooledDown
		}

	}


}
