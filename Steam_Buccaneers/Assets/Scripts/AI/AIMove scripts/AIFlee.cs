using UnityEngine;
using System.Collections;

public class AIFlee : MonoBehaviour 
{
	private Vector3 target; //Position in world coordinates the enemy drives towards while fleeing
	private GameObject player; //Reference to the player object
	private float fleeTimer; //Used to update flee position
	private float fleeDuration = 3; //Duration the codes wait before updating flee position
	private bool first = true; //First time the code runs
	private Vector3 relativePoint; //Relative point between enemy and flee position

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("PlayerShip");
	}

	//Generates a new position for the enemy to drive towards.
	//This point is generated every 3 seconds, and also resets the timer.
	private Vector3 fleeTargetPosition() 
	{
		fleeTimer = 0; //Resets the timer
		relativePoint = transform.InverseTransformPoint(player.transform.position); //Calculates relative relative postion

		//We want a position at the oposite side to where the player is,
		//so we makes the result oposite of what it actually is, and multiplies
		//it with 10 to make sure the position is far enough away from the enemy
		//so that it will generate a new point before it reaches it's current.
		float posX = - relativePoint.x * 10;
		float posZ = - relativePoint.z * 10;

		Vector3 temp = transform.TransformPoint(new Vector3(posX, 0, posZ)); //Save the new position in a variable

		return temp; //Return the position
	}

	//This function is called from AIMove.cs's Update() function
	public void flee()
	{
		if(first) //First time the enemy calculates flee position
		{
			target = fleeTargetPosition(); //Set the targets position
			first = false; //It is no longer the first time for the test
		}
		fleeTimer += Time.deltaTime; //We have waited some more
		if(fleeTimer >= fleeDuration) //Timer reached required duration, generate new position
			target = fleeTargetPosition();

		relativePoint = transform.InverseTransformPoint(target); //Update the relative position between enemy and flee position
		if(relativePoint.x >-0.1 && relativePoint.x < 0.1) //The target position is directly in front or behind the enemy
		{
			if(relativePoint.z >= 0) //It's to the front, do nothing
			{
				this.GetComponent<AImove>().turnLeft = false;
				this.GetComponent<AImove>().turnRight = false;
			}
			else //It's behind. Turn to the right to get there. We should check where the player is and turn based on that result. 
			{
				this.GetComponent<AImove>().turnLeft = true;
				this.GetComponent<AImove>().turnRight = false;
			}
		}

		else if(relativePoint.x >= 0) //Target position to the right of the enemy
		{
			this.GetComponent<AImove>().turnLeft = false;
			this.GetComponent<AImove>().turnRight = true;
		}

		else if(relativePoint.z <= 0) //Target position to the left of the enemy
		{
			this.GetComponent<AImove>().turnLeft = true;
			this.GetComponent<AImove>().turnRight = false;
		}
	}
}
