using UnityEngine;
using System.Collections;

public class AIMaster : MonoBehaviour {
	
	public static float detectDistance;
	private GameObject playerPoint;
	public Transform aiPoint;
	public static int aiHealth;
	private float killtimer = 0;
	// Use this for initialization
	void Start () {
		if(this.gameObject.name == "AI_LVL1")
		{
			aiHealth = 10;
		}
		if(this.gameObject.name == "AI_LVL2")
		{
			aiHealth = 15;
		}
		playerPoint = GameObject.FindGameObjectWithTag ("Player"); //As the player is a prefab, I had to add it to the variable this way
	}
	
	void Update () {
		detectDistance = Vector3.Distance (playerPoint.transform.position, aiPoint.transform.position); //calculates the distance between the AI and the player
		 
		if(aiHealth <= 0)
		{
			killAI();
		}

		if(detectDistance >= 60)
		{
			killtimer+= Time.deltaTime;
		}

		if(detectDistance < 60)
		{
			killtimer = 0;
		}

		if(killtimer > 5)
		{
			killAI();
		}
			
		//We dont want to render the AI if its to far away from the player,
		//so we delete it when the distance is equal or greater than 100 (we can change this number at any time).
//		if (detectDistance >= 100) {
//			Destroy(this.gameObject);
//			spawnAI.livingShip = false;
//		}

		//This is commented out, but is usefull when the AI is patrolling.
		//When its not patrolling, and only always going to move against the player,
		//we dont need this code at all.
//		if (detectDistance <= 40) 
//		{
//			GetComponent<AIPatroling>().enabled = false;
//			GetComponent<MoveTo> ().enabled = true;
//		}
//
//		if (detectDistance >= 50)
//		{
//			GetComponent<AIPatroling> ().enabled = true;
//			GetComponent<MoveTo> ().enabled = false;
//		}
	}

//	void OnTriggerEnter(Collider other)
//	{
//		if(this.tag == "detectRight" || this.tag == "detectLeft")
//		{
//		}
//		if(other.tag == "playerBullet")
//		{
//			Destroy(other.gameObject);
//			aiHealth --;
//			Debug.Log("aiHealth = " + aiHealth);
//			if(aiHealth <= 0)
//			{
//				Destroy(this.transform.parent.gameObject);
//			}
//		}
//	}

	private void killAI()
	{
		spawnAI.livingShip = false;
		Destroy(this.transform.parent.gameObject);
	}
}
