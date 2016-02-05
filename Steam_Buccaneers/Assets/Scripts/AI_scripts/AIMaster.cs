﻿using UnityEngine;
using System.Collections;

public class AIMaster : MonoBehaviour {
	
	public GameObject scrap;
	private GameObject playerPoint;

	public Transform aiPoint;

	public static float detectDistance;
	private float killtimer = 0;

	private bool testedFleeing = false;
	private bool newSpawn = true;

	public static int aiHealth;
	private int ranNum;

	// Use this for initialization
	void Start () {
		if(this.gameObject.name == "AI_LVL1(Clone)")
		{
			aiHealth = 1;
		}
		if(this.gameObject.name == "AI_LVL2(Clone)")
		{
			aiHealth = 20;
		}
		playerPoint = GameObject.FindGameObjectWithTag ("Player"); //As the player is a prefab, I had to add it to the variable this way
	}
	
	void Update () {
		detectDistance = Vector3.Distance (playerPoint.transform.position, aiPoint.transform.position); //calculates the distance between the AI and the player

		if(detectDistance < 40)
		{
			AImove.maxVelocity.x = 3.5f;
			AImove.maxVelocity.z = 3.5f;
			AImove.force = 200f;
			newSpawn = false;
		}

		if(aiHealth <= 0)
		{
			killAI();
		}

		if(testedFleeing == false)
		{
			if(aiHealth <= (aiHealth*0.2))
			{
				int ranNum = Random.Range(1, 11);
				{
					if(ranNum > 9)
					{
						testedFleeing = true;
						AImove.move.flee();
					}
				}
			}
		}


		if(detectDistance >= 60)
		{
			killtimer+= Time.deltaTime;
		}

		if(detectDistance < 60 || newSpawn == true)
		{
			killtimer = 0;
		}

		if(killtimer > 10)
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

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Planet")
		{
			Debug.Log("planet pls");
			killAI();
		}

	}

	private void killAI()
	{
		int temp = Random.Range(1, 7);
		for(int i = 0; i < temp; i++)
		{
			Instantiate(scrap);
			scrap.transform.position = this.transform.position;
		}
		spawnAI.livingShip = false;
		Debug.Log("pls dont kill");
		Destroy(this.gameObject);
	}
}
