using UnityEngine;
using System.Collections;

public class PlayerRotate : MonoBehaviour
{
	Vector3 rotationVec = new Vector3 (0f, 0f, 1f); // the vector that defines the axis the player should rotate in while turning
	Vector3 uprightRot = new Vector3(0f, 180f, 0f); // the default upright rotation of the player ship
	private float minDeg = 20f; // the minimum degrees of how low the player can rotate
	private float maxDeg = 340f; // the maximum degrees of how far the player can rotate
	private float currentDeg = 0f; // the current degrees of the ship

	// Update is called once per frame
	void Update () 
	{
		currentDeg = this.transform.localEulerAngles.z; // setting the variable currentDeg to the players rotation in the z-axis at all times

		if (PlayerMove.turnLeft == true) // if the player is turning to the left
		{
			// if the current rotation of the player is above max rotation or below the minimum rotation
			if (currentDeg >= maxDeg || currentDeg <= minDeg)
			{
				// the player has a negative rotation in the z-axis, at the speed of 10 per second
				this.transform.Rotate(-rotationVec, 10*Time.deltaTime);
			}
			
		}
			
		else if (PlayerMove.turnRight == true) // if the player is turning to the right
		{
			// if the current rotation of the player is above max rotation or below the minimum rotation
			if (currentDeg >= maxDeg || currentDeg <= minDeg) 
			{
				// the player has a positive rotation in the z-axis, at the speed of 10 per second
				this.transform.Rotate(rotationVec, 10*Time.deltaTime);
			}
		}

		else // if the player in not moving either left nor right
		{
			// if the current rotation of the player is above the max rotation - 20, ie. 0
			if ( currentDeg >= maxDeg-20f)
			{
				//rotate the player back to 0 by 20 per second
				this.transform.Rotate(rotationVec, 20*Time.deltaTime);
			}

			// if the current rotation of the player is below the minimum degrees + 20, ie. 0
			if ( currentDeg <= minDeg+20f)
			{
				// rotate the player back to 0 by 20 per second
				this.transform.Rotate(-rotationVec, 20*Time.deltaTime);
			}
	
		}
		// if the player is not turning to either side
		if ( PlayerMove.turnLeft == false && PlayerMove.turnRight == false)
		{
			// if it is still rotating by a tiny amount, but is close to 0
			if (currentDeg < 0.2f || currentDeg > 359.8f)
			{
				// set the players rotation equal to the upright rotation
				this.transform.localEulerAngles = uprightRot;
			}
		}
	}
}
