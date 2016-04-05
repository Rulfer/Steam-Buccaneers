using UnityEngine;
using System.Collections;

public class TreasureShip : MonoBehaviour 
{
	Vector3 rotateVec = new Vector3 (0f,1f,1f);
	Vector3 randomSpawnVec;
	public GameObject scrap;
	// Use this for initialization
	void Start () 
	{
		for(int i = 0; i < 20; i ++)
		{
			
			randomSpawnVec  = new Vector3 (this.transform.position.x + Random.Range(-20f, 20f), 0f, 
				this.transform.position.z + Random.Range(-20f, 20f));
			Instantiate (scrap, randomSpawnVec, this.transform.rotation);
			//Spawns the scrap around the treasure ship
			//Instantiate (scrap, randomSpawnVec, this.transform.rotation);

		}

	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//scrap = GameObject.Find("scrap");

		//Just rotates the ship around a little bit
		this.transform.Rotate (rotateVec, 0.1f);
		//randomly generated number for where the treasure is to spawn around the treasure ship
		/*randomSpawnVec  = new Vector3 (this.transform.position.x + Random.Range(-20f, 20f), 0f, 
			this.transform.position.z + Random.Range(-20f, 20f));
		Instantiate (scrap, randomSpawnVec, this.transform.rotation);*/
	}
}
