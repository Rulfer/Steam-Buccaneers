using UnityEngine;
using System.Collections;

public class ExtraCannonSensors : MonoBehaviour {

	private Vector3 right; //Vector facing the right direction
	private Vector3 left; //Vector facing the left direction
	private int detectDistance = 100; //Length of the vectors

	public GameObject bossGunners; //Cannonball spawnpoints on the boss

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		right = this.transform.TransformDirection(Vector3.right); 
		left = this.transform.TransformDirection(Vector3.left);

		Debug.DrawRay(this.transform.position, right * detectDistance, Color.green);
		Debug.DrawRay(this.transform.position, left * detectDistance, Color.blue);

		checkGunRadar();
	}

	private void checkGunRadar()
	{
		RaycastHit objectHit;

		if(Physics.Raycast(this.transform.position, left, out objectHit, detectDistance)) //Raycast hit something
		{
			if(objectHit.transform.root.name == "PlayerShip" || objectHit.transform.tag == "aiShip")//Hit the player or another enemy
			{
				bossGunners.GetComponent<AIsideCanons>().fireRight = true;//The enemy can now fire
			}
			else //Hit something else than the player
			{
				bossGunners.GetComponent<AIsideCanons>().fireRight = false; //The enemy cant fire anyway
			}
		}
		else //Hit nothing
		{
			bossGunners.GetComponent<AIsideCanons>().fireRight = false; //The enemy cant fire
		}

		if(Physics.Raycast(this.transform.position, right, out objectHit, detectDistance))//Raycast hit something
		{
			if(objectHit.transform.root.name == "PlayerShip" || objectHit.transform.tag == "aiShip")//Hit the player or another enemy
			{
				bossGunners.GetComponent<AIsideCanons>().fireLeft = true;//The enemy can now fire
			}

			else //Hit something else than the player
			{
				bossGunners.GetComponent<AIsideCanons>().fireLeft = false; //The enemy cant fire anyway
			}
		}
		else//Hit nothing
		{
			bossGunners.GetComponent<AIsideCanons>().fireLeft = false;//The enemy cant fire
		}
	}
}
