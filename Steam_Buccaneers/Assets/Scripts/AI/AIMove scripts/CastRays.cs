using UnityEngine;
using System.Collections;

public class CastRays : MonoBehaviour {

	public Vector3 right; //Used to raycast to the right
	public Vector3 left; //Used to raycast to the left

	private int detectDistance = 50; //Length of the raycast

	
	// Update is called once per frame
	void Update () {
		right = this.transform.TransformDirection(Vector3.right); //Raycast
		left = this.transform.TransformDirection(Vector3.left); //Raycast

		Debug.DrawRay(this.transform.position, right * detectDistance, Color.green);
		Debug.DrawRay(this.transform.position, left * detectDistance, Color.blue);

		sensors(); //Update the casts and see if they hit something
	}

	//Check if a hindering is detected to one of the sides of the ship.
	//This is to make it so that the enemies in the game can avoid planets, 
	//moons, asteroids, other enemies and the shops. 
	private void sensors()
	{
		bool hitLeft = false; //See if a raycast hit something to the left
		bool hitRight = false; //See if a raycast hit something to the right
		RaycastHit objectHit;
		if(Physics.Raycast(this.transform.position, right, out objectHit, detectDistance)) //Right raycast hit something
		{
			if(objectHit.transform.tag == "Planet" || objectHit.transform.tag == "Moon" || objectHit.transform.tag == "asteroid" || objectHit.transform.tag == "aiShip" || objectHit.transform.tag == "shopWall") //The planet is in front of the AI
			{
				this.transform.root.GetComponent<AImove>().turnLeft = true; //Turn left to avoid hindering
				this.transform.root.GetComponent<AIavoid>().hitSide = true; //Tell AIAvoid that one of the sides saw something
				hitLeft = true; 
			}
			else //Nothing was to the right
			{
				if(this.transform.root.GetComponent<AIavoid>().hitSide == true) //Resets the values
					this.transform.root.GetComponent<AImove>().turnLeft = false;
				this.transform.root.GetComponent<AIavoid>().hitSide = false;
				hitLeft = false;
			}
		}

		else if(Physics.Raycast(this.transform.position, left, out objectHit, detectDistance)) //Left raycast hit something
		{
			if(objectHit.transform.tag == "Planet" || objectHit.transform.tag == "Moon" || objectHit.transform.tag == "asteroid" || objectHit.transform.tag == "aiShip" || objectHit.transform.tag == "shopWall") //The planet is in front of the AI
			{
				this.transform.root.GetComponent<AImove>().turnRight = true; //Turn right to avoid hindering
				this.transform.root.GetComponent<AIavoid>().hitSide = true; //Tell AIAvoid that one of the sides saw something
				hitRight = true;
			}
			else //Nothing was to the left
			{
				if(this.transform.root.GetComponent<AIavoid>().hitSide == true) //Resets the values
					this.transform.root.GetComponent<AImove>().turnRight = false;
				if(hitLeft == false)
					this.transform.root.GetComponent<AIavoid>().hitSide = false;
			}
		}
		else //Neither of the raycasts saw something
		{
			if(this.transform.root.GetComponent<AIavoid>().hitSide == true) //AIavoid is outdated
			{
				if(hitLeft == true)
					this.transform.root.GetComponent<AImove>().turnLeft = false; //AImove don't need to turn to the left anymore
				if(hitRight == true)
					this.transform.root.GetComponent<AImove>().turnRight = false;
				this.transform.root.GetComponent<AIavoid>().hitSide = false; //AImove don't need to turn to the right anymore
			}
		}
	}
}
