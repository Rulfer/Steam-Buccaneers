using UnityEngine;
using System.Collections;

public class AIPatroling : MonoBehaviour {

	public Vector3 target; //Position the enemy will drive towards
	private Vector3 relativePoint; //The relative position between the enemy and target position

	void Update () 
	{
		if(this.GetComponent<AIavoid>().hitFront == false && this.GetComponent<AIavoid>().hitSide == false) //If nothing is in the way of the enemy, continue with the code
		{
			if(target != null) //There is in fact a target
				goToPoint(); //Drive towards the target
		}
	}


	void goToPoint()
	{
		relativePoint = transform.InverseTransformPoint(target); //Get the relative position
		if(relativePoint.x >-0.1 && relativePoint.x < 0.1) //The target is either directly to the front or rear of the enemy
		{
			if(relativePoint.z >= 0) //The target is directly to the front
			{
				this.GetComponent<AImove>().turnLeft = false;
				this.GetComponent<AImove>().turnRight = false;
			}
			else //The target is directly to the rear. Should check player position before turning
			{
				this.GetComponent<AImove>().turnLeft = true;
				this.GetComponent<AImove>().turnRight = false;
			}
		}

		else if(relativePoint.x >= 0) //The target position is to the right of the enemy
		{
			this.GetComponent<AImove>().turnLeft = false;
			this.GetComponent<AImove>().turnRight = true;
		}

		else if(relativePoint.z <= 0) //The target position is to the left of the enemy
		{
			this.GetComponent<AImove>().turnLeft = true;
			this.GetComponent<AImove>().turnRight = false;
		}
	}
}