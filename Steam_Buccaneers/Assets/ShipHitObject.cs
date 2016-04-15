using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShipHitObject : MonoBehaviour 
{
	private Vector3 currentVel;
	private Vector3 newVel;
	public GameObject sparkSimulation;

	void Update()
	{
		currentVel = this.GetComponent<Rigidbody>().velocity; //Constantly update to see the players current velocity
	}

	// Use this for initialization
	void OnCollisionEnter(Collision col)
	{
		newVel = this.GetComponent<Rigidbody>().velocity; //The velocity after the ship hit something

		//We check if the player is driving to the left or down. 
		//These directions will return a negative velocity value
		if(currentVel.x < 0)
			currentVel.x *= -1;
		if(currentVel.z < 0)
			currentVel.z *= -1;
		if(newVel.x < 0)
			newVel.x *= -1;
		if(newVel.z < 0)
			newVel.z *= -1;

		//This ship is to deal damage to the other ship based on the difference in velocity
		//this ship had before and after impact. 
		float lostHealthX = currentVel.x - newVel.x; //The velocity difference in x-axis
		if(lostHealthX < 0) 
			lostHealthX *= -1;
		float lostHealthZ = currentVel.z - newVel.z; //The velocity difference in z-axis
		if(lostHealthZ < 0)
			lostHealthZ *= -1;
		int healthLost = (int)Mathf.Round((lostHealthX + lostHealthZ) / 10); //Rounds the lost health to the closest integer. We also takes /10 to make the numbers lower, decreasing health lost on impact. 

		if(healthLost < 0)
			healthLost *= -1;
		if(healthLost > 1) //If the damage dealt is greater than 1, deal the damage. 
		{
			ContactPoint contact = col.contacts[0];
			Instantiate(sparkSimulation, new Vector3(contact.point.x, contact.point.y + 20, contact.point.z), this.transform.rotation); //Create spark effects on the impact point. 
			if(col.transform.name == "PlayerShip")
				GameControl.control.health -= healthLost;
			if(col.transform.tag == "Boss(Clone)" || col.transform.tag == "Marine(Clone)" || col.transform.tag == "Cargo(Clone)" )
			{
				if(col.transform.name == "Boss(Clone)" && (col.transform.GetComponent<AIMaster>().aiHealth - healthLost) <= 0)
				{
					SceneManager.LoadScene("cog_screen");
				}
				if(col.transform.GetComponent<AIMaster>().isDead == false) //We make sure the projectile don't hit an already dead ship. 
				{
					col.transform.GetComponent<AIMaster>().aiHealth -= healthLost;
					if(col.transform.GetComponent<AIMaster>().aiHealth <= 0)
					{
						col.transform.GetComponent<AIMaster>().killAI();
					}
					else if(col.transform.GetComponent<AIMaster>().aiHealth <= col.transform.GetComponent<AIMaster>().aiHealthMat3)
					{
						col.transform.GetComponent<AIMaster>().changeMat3();
						col.transform.GetComponent<AIMaster>().testFleeing();
					}
					else if(col.transform.GetComponent<AIMaster>().aiHealth <= col.transform.GetComponent<AIMaster>().aiHealthMat2)
						col.transform.GetComponent<AIMaster>().changeMat2();
				}
			}
		}
	}
}
