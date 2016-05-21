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
	private int antallScrapIgjen = 20;

	float distanceAway;
	int xRot;
	int yRot;
	int zRot;
	// Use this for initialization
	void Start () 
	{
//		xRot = Random.Range(-1,1);
//		Mathf.RoundToInt(xRot);
//		yRot = Random.Range(-1,1);
//		Mathf.RoundToInt(yRot);
//		zRot = Random.Range(-1,1);
//		Mathf.RoundToInt(zRot);
//		
//		rotateVec = new Vector3 (xRot, yRot, zRot);
		rotateVec = new Vector3 (Mathf.RoundToInt(Random.Range(-1,1)),
			Mathf.RoundToInt(Random.Range(-1,1)), Mathf.RoundToInt(Random.Range(-1,1)));
		
		player = GameObject.Find("PlayerShip");

		for(int i = 0; i < 20; i ++)
		{
			randomSpawnVec  = new Vector3 (this.transform.position.x + Random.Range(-20f, 20f), 0f, 
				this.transform.position.z + Random.Range(-20f, 20f));
			tempScrap = Instantiate (scrap[Random.Range(0, 4)]);
			tempScrap.GetComponent<ScrapRandomDirection>().despawn = false;
			tempScrap.transform.position = randomSpawnVec;
			tempScrap.transform.rotation = this.transform.rotation;
			scrapsLyingAroundShip [i] = tempScrap;
		}
	}

	
	// Update is called once per frame
	void Update () 
	{
		distanceAway = Vector3.Distance(this.transform.position, player.transform.position);

		//Debug.Log (distanceAway + " Ableboelb")
		for (int i = 0; i < scrapsLyingAroundShip.Length; i++)
		{
			if (scrapsLyingAroundShip [i] == null)
			{
				antallScrapIgjen--;
			}
		}

		if (antallScrapIgjen == 0)
		{


			for (int j = 0; j < scrapsLyingAroundShip.Length; j++)
				Destroy (scrapsLyingAroundShip[j]);

			gameObject.tag = "Untagged";
		}

		if (distanceAway >= 750)
			Destroy(gameObject);

		//scrap = GameObject.Find("scrap");

		//Just rotates the ship around a little bit
		this.transform.Rotate (rotateVec, 0.1f);
		//randomly generated number for where the treasure is to spawn around the treasure ship
		/*randomSpawnVec  = new Vector3 (this.transform.position.x + Random.Range(-20f, 20f), 0f, 
			this.transform.position.z + Random.Range(-20f, 20f));
		Instantiate (scrap, randomSpawnVec, this.transform.rotation);*/
		antallScrapIgjen = 20;
	}
}
