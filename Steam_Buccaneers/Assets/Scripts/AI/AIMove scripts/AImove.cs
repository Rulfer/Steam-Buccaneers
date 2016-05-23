using UnityEngine;
using System.Collections;

public class AImove : MonoBehaviour {
	public Rigidbody aiRigid;

	public float force = 200f; //Force of the enemy. This is what drives the models forwards in the scene. 
	public int turnSpeed = 50; //How fast the enemy can turn. 
	private float bombTimer; //Timer used to disable movement when hitting a bomb. 

	private float distanceToPlayer; //Stores the distance between the enemy and the player
	float minDist = 20f; //Used to check if the player is too close to the enemy
	float maxDist = 80f; //Used to check if the player is too far away from the enemy

	public bool turnLeft = false; //Bool to activate left turning
	public bool turnRight = false; //Bool to activate right turning
	public bool hitBomb = false; //Bool used to activate/deactivate driving
	public bool isPatroling = true; //The enemy is driving towards an objective, not chasing the player
	private bool playerInFrontOfAI; //True if the player is directly in front of the enemy
	public bool isFleeing = false; //Bool to activate/deactivate fleeing

	private GameObject player; //Reference to the player object
	public Vector3 relativePoint; //Stores the relative position between the player and the enemy
	public Vector3 maxVelocity; //Max allowed velocity


	void Start ()
	{
		player = GameObject.Find("PlayerShip");
		aiRigid = this.GetComponent<Rigidbody>();
		force = PlayerMove.move.force; //Sets force equal to that of the player
		maxVelocity = PlayerMove.move.maxVelocity; //Sets max velocity equal to that of the player
	}
		
    void FixedUpdate () 
	{
		if(this.GetComponent<AIavoid>().hitFront == false && this.GetComponent<AIavoid>().hitSide == false) //Nothing is blocking the path
		{
			if(isPatroling == false) //This enemy is not patroling
			{
				if(isFleeing == false) //This enemy is not fleeing, so it must be in combat
				{
					checkPlayerPosition (); //See where the player is 
				}
				else
					this.GetComponent<AIFlee>().flee(); //Continue fleeing
			}
		}

		//If an enemy hits one of the boss's bombs, they are to be stunned for 1 second.
		if(hitBomb ==  true)
		{
			bombTimer += Time.deltaTime; //Increase passed time
			if(bombTimer >= 1) //Timer has reached required duration
			{
				bombTimer = 0; //Reset bomb timer
				hitBomb = false; //No longer affected by the stun

				//Reset Rigidbody values
				this.transform.root.GetComponent<Rigidbody>().mass = 1;
				this.transform.root.GetComponent<Rigidbody>().drag = 0.5f;
				this.transform.root.GetComponent<Rigidbody>().angularDrag = 0.5f;
			}
		}
			
		relativePoint = transform.InverseTransformPoint(player.transform.position); //Check and save the relative position between this enemy and the player

		if(hitBomb == false) //The enemy is not affected by a bomb
		{
			aiRigid.AddForce(transform.forward * force*Time.deltaTime); //Drive forwards

			//Series of tests to see if the velocity in eeither the x og z axis are to high.
			//We need to restrict the velocity like this to prevent any ships from driving
			//too fast. 
			if (aiRigid.velocity.x >= maxVelocity.x)
			{
				aiRigid.velocity = new Vector3 (maxVelocity.x, 0.0f, aiRigid.velocity.z);
			}

			if (aiRigid.velocity.x <= -maxVelocity.x)
			{
				aiRigid.velocity = new Vector3 (-maxVelocity.x, 0.0f, aiRigid.velocity.z);
			}

			if (aiRigid.velocity.z >= maxVelocity.z)
			{
				aiRigid.velocity = new Vector3 (aiRigid.velocity.x, 0.0f, maxVelocity.z);
			}

			if (aiRigid.velocity.z <= -maxVelocity.z)
			{
				aiRigid.velocity = new Vector3 (aiRigid.velocity.x, 0.0f, -maxVelocity.z);
			}

			if (turnLeft == true) //One of the scripts has told this script to activate this part of the code
			{
				this.transform.Rotate (Vector3.down, turnSpeed * Time.deltaTime);
			}

			if (turnRight == true) //One of the scripts has told this script to activate this part of the code
			{
				this.transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
			}
		}
	}
		
	//Uses the data recieved from isFacingPlayer to determine weather or not to turn left,
	//right or continue driving forward
	void checkPlayerPosition()
	{
		playerInFrontOfAI = isFacingPlayer (); //Returns a bool value
		distanceToPlayer = Vector3.Distance (this.transform.position, player.transform.position); //distance between AI and player

		if(distanceToPlayer > maxDist) //AI too far away from the player
		{
			if(playerInFrontOfAI == true) //Player directly to the front
			{
				turnLeft = false;
				turnRight = false;
			}

			else
			{
				turnTowardsPlayer (); //Player is not to the front. Calculate how to turn
			}
		}

		else if(distanceToPlayer < minDist) //Too close to the player
		{
			avoidPlayer(); //Maneuver away from the player
		}

		else if(distanceToPlayer < maxDist && distanceToPlayer > minDist) //Perfect distance! Aim the cannons directly at the player
		{
			canonsFacingPlayer(player); //Turn so that the canons face the player, rather than turn to chase the player
		}
	}

	//Turns the AI Ship so that the canons on either side
	//is facing straight for the player.
	private void canonsFacingPlayer(GameObject test)
	{
		relativePoint = Transformation(test); //Relative position between this enemy and the player

		//We use the public bools "fireLeft" and "fireRight" from the
		//AIsideCanons.cs script. These change based on Raycast checks.
		//If fireLeft = false, but the ship is to the left, we turn based on that.
		//The same goes for the right side. This results in a much 
		//smoother turning, compared to the previous stuttering one.
		if(relativePoint.x <= 0) //Player to the left
		{
			if(AIsideCanons.canons.fireLeft == false) //The AI cant shoot at the player
			{
				if(relativePoint.z >= 0) //Player to the front-left
				{
					turnRight = true;
					turnLeft = false;
				}
				else if(relativePoint.z <= 0) //Player to  the back-left
				{
					turnLeft = true;
					turnRight = false;
				}
			}
		}

		else if(relativePoint.x >= 0) //Player to the right
		{
			if(AIsideCanons.canons.fireRight == false) //The AI cant shoot at the player
			{
				if(relativePoint.z >= 0) //Player to the front-right
				{
					turnRight = false;
					turnLeft = true;
				}
				else if(relativePoint.z <= 0) //Player to  the back-right
				{
					turnLeft = false;
					turnRight = true;
				}
			}
		}
	}

	//If the enemy gets to close to the player, we want it to avoid collitions
	void avoidPlayer()
	{
		relativePoint = Transformation(player); //Relative position between this enemy and the player 
		if (relativePoint.x <= 0) //Player to the left
		{
			turnLeft = false;
			turnRight = true;
		}
		else if (relativePoint.x >= 0) //Player to the right
		{
			turnRight = false;
			turnLeft = true;
		}
	}

	//Makes the enemy turn towards the player
	void turnTowardsPlayer()
	{
		relativePoint = Transformation(player); //Relative position between this enemy and the player
		if (relativePoint.x <= 0) //Player to the left
		{
			turnLeft = true;
			turnRight = false;
		}
		else if (relativePoint.x >= 0) //Player to the right
		{
			turnRight = true;
			turnLeft = false;
		}
	}


	//Runs a test and returns the result in a vector3 variable.
	//Used to determine if an object is to the left, right
	//or front of This object
	private Vector3 Transformation(GameObject test)
	{
		relativePoint = transform.InverseTransformPoint(test.transform.position); //Relative position between this object (enemy) and the parsed parameter
		return relativePoint; //Return the result
	}

	//Checks if the enemy is facing the player or not
	private bool isFacingPlayer()
	{
		if(relativePoint.z > 0) //The player is in front of the enemy
		{
			if(relativePoint.x > -0.01 && relativePoint.x < 0.01) //The player is directly in front of the enemy
			{
				return true; //Return true
			}
			else return false; //Player is in front of the enemy, but to either of the sides. Return false
		}
		else return false; //Player is somewhere behind the enemy. Return false. 
	}
}