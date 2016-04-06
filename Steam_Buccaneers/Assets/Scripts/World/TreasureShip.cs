using UnityEngine;
using System.Collections;

public class TreasureShip : MonoBehaviour 
{
	Vector3 rotateVec = new Vector3 (0f,1f,1f);
	Vector3 randomSpawnVec;
	public GameObject scrap;
	public GameObject player;
	float deleteTimer;
	float distanceAway;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("PlayerShip");

		for(int i = 0; i < 20; i ++)
		{
			randomSpawnVec  = new Vector3 (this.transform.position.x + Random.Range(-20f, 20f), 0f, 
				this.transform.position.z + Random.Range(-20f, 20f));
			Instantiate (scrap, randomSpawnVec, this.transform.rotation);
			deleteTimer = 60;

			//Spawns the scrap around the treasure ship
			//Instantiate (scrap, randomSpawnVec, this.transform.rotation);

		}

	
	}
	/*
	void Timer ()
	{
		//timeLeft = nextSpawnIn;
		//timeLeft -= Time.deltaTime;
		deleteTimer -= Time.deltaTime;

		if (deleteTimer <= 0f)
		{
			Destroy (gameObject);
		}

	}*/
	
	// Update is called once per frame
	void Update () 
	{
		distanceAway = Vector3.Distance(this.transform.position, player.transform.position);

		Debug.Log (distanceAway + " Ableboelb");
		if (distanceAway >= 750)
		{
			Destroy(gameObject);
		}
		//scrap = GameObject.Find("scrap");

		//Just rotates the ship around a little bit
		this.transform.Rotate (rotateVec, 0.1f);
		//randomly generated number for where the treasure is to spawn around the treasure ship
		/*randomSpawnVec  = new Vector3 (this.transform.position.x + Random.Range(-20f, 20f), 0f, 
			this.transform.position.z + Random.Range(-20f, 20f));
		Instantiate (scrap, randomSpawnVec, this.transform.rotation);*/
	}
}
