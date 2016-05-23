using UnityEngine;
using System.Collections;

public class TreasureShip : MonoBehaviour 
{

	Vector3 rotateVec; //= new Vector3 (1f,1f,1f);
	Vector3 randomSpawnVec;
	public GameObject[] scrap;
	private GameObject[] scrapsLyingAroundShip = new GameObject[20];
	public GameObject player;
	private GameObject tempScrap;
	private float distanceAway;

	// Use this for initialization
	void Start () 
	{
		// creating a random vector for the rotation of the ship
		rotateVec = new Vector3 (Mathf.RoundToInt(Random.Range(-1,1)),
			Mathf.RoundToInt(Random.Range(-1,1)), Mathf.RoundToInt(Random.Range(-1,1)));
		
		player = GameObject.Find("PlayerShip"); // declaring the player as this variable

		for(int i = 0; i < 20; i ++) // for every scrap there is to spawn
		{
			// spawn the scrap at the ships position, plus a number between -20 and 20 in x and z-axis
			randomSpawnVec  = new Vector3 (this.transform.position.x + Random.Range(-20f, 20f), 0f, 
				this.transform.position.z + Random.Range(-20f, 20f));
			// create a scrap with a random value
			tempScrap = Instantiate (scrap[Random.Range(0, 4)]);
			// spawns the scrap with the vector created
			tempScrap.transform.position = randomSpawnVec;
			// give the scrap the rotation same as the ship
			tempScrap.transform.rotation = this.transform.rotation;
			// how do i formulate this
			scrapsLyingAroundShip [i] = tempScrap;
		}
	}

	
	// Update is called once per frame
	void Update () 
	{
		// calculates the distance between the player and the treasure ship
		distanceAway = Vector3.Distance(this.transform.position, player.transform.position);

		// if the player is close enough to it, change its tag so it will no longer be shown as a treasure on the compass
		if(distanceAway <= 30)
			gameObject.tag = "aiShip";

		// if the player is too far from the ship, destroy its scrap and the ship itself
		if (distanceAway >= 750)
		{
			foreach(GameObject go in scrapsLyingAroundShip)
			{
				Destroy(go.gameObject);
			}
			Destroy(this.gameObject);
		}

		//Just rotates the ship around a little bit
		this.transform.Rotate (rotateVec, 0.1f);
	}
}
