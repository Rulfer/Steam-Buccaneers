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
		rotateVec = new Vector3 (Mathf.RoundToInt(Random.Range(-1,1)),
			Mathf.RoundToInt(Random.Range(-1,1)), Mathf.RoundToInt(Random.Range(-1,1)));
		
		player = GameObject.Find("PlayerShip");

		for(int i = 0; i < 20; i ++)
		{
			randomSpawnVec  = new Vector3 (this.transform.position.x + Random.Range(-20f, 20f), 0f, 
				this.transform.position.z + Random.Range(-20f, 20f));
			tempScrap = Instantiate (scrap[Random.Range(0, 4)]);
			tempScrap.transform.position = randomSpawnVec;
			tempScrap.transform.rotation = this.transform.rotation;
			scrapsLyingAroundShip [i] = tempScrap;
		}
	}

	
	// Update is called once per frame
	void Update () 
	{
		distanceAway = Vector3.Distance(this.transform.position, player.transform.position);

		if(distanceAway <= 30)
			gameObject.tag = "aiShip";

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
