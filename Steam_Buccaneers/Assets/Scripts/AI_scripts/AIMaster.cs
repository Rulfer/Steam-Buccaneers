using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIMaster : MonoBehaviour 
{
	private spawnAI spawn;
	public GameObject scrap;
	public GameObject aiModelObject;
	private GameObject playerPoint;

	public Material mat2;
	public Material mat3;
	private float aiHealthMat2;
	private float aiHealthMat3;

	public static float detectDistance;

	private bool testedFleeing = false;
	public bool detectedPlayer = false;
	public bool isBoss = false;

	public float aiHealth = 20;
	public int arrayIndex;
	private int ranNum;

	// Use this for initialization
	void Start () {
		playerPoint = GameObject.FindGameObjectWithTag ("Player"); //As the player is a prefab, I had to add it to the variable this way
		aiHealthMat2= aiHealth * 0.66f;
		aiHealthMat3 = aiHealth * 0.33f;

		spawn = GameObject.Find("GameControl").GetComponent<spawnAI>();
	}
	
	void Update () {
		detectDistance = Vector3.Distance (playerPoint.transform.position, this.transform.position); //calculates the distance between the AI and the player

		if(detectDistance < 60)
		{
			this.GetComponent<AImove>().maxVelocity.x = 3.5f;
			this.GetComponent<AImove>().maxVelocity.z = 3.5f;
			this.GetComponent<AImove>().force = 200f;
		}

		if(isBoss == false)
		{
			if(detectDistance < 100)
			{
				if(detectedPlayer == false)
				{
					deaktivatePatroling();
					killMarines();
				}
			}
		}

		else
			deaktivatePatroling();

		if(testedFleeing == false)
		{
			if(aiHealth <= (aiHealth*0.2))
			{
				int ranNum = Random.Range(1, 11);
				{
					if(ranNum > 9)
					{
						testedFleeing = true;
						this.GetComponent<AImove>().flee();
					}
				}
			}
		}

		if(aiHealth <= aiHealthMat3)
			aiModelObject.GetComponent<Renderer>().material = new Material(mat3);

		else if(aiHealth <= aiHealthMat2)
			aiModelObject.GetComponent<Renderer>().material = new Material(mat2);

		if(detectDistance > 500)
			killAI();
		
		if(aiHealth <= 0)
			killAI();
	}

	public void deaktivatePatroling()
	{
		detectedPlayer = true;
		spawn.stopFightTimer = true;
		this.GetComponent<AIPatroling>().enabled = false;
		this.GetComponent<AImove>().isPatroling = false;
		this.GetComponent<AImove>().force = 10000f;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Planet")
		{
			Debug.Log("planet pls");
			killAI();
		}
	}

	public void killMarines()
	{
		spawn.stopSpawn = true;
		for(int i = 0; i < 10; i++)
		{
			Debug.Log("We are killing them now,");
			if(i != arrayIndex)
			{
				if(spawn.marineShips[i] != null)
				{
					Destroy(spawn.marineShips[i].GetComponent<AIPatroling>().target);
					Destroy(spawn.marineShips[i]);
					spawn.marineShips[i] = null;
					spawn.availableIndes[i] = true;
					spawn.livingShips--;
				}
			}
		}
	}

	private void killAI()
	{
		int temp = Random.Range(1, 7);
		for(int i = 0; i < temp; i++)
		{
			Instantiate(scrap, this.transform.position, this.transform.rotation);
		}
		spawn.marineShips[arrayIndex] = null;
		spawn.availableIndes[arrayIndex] = true;
		spawn.livingShips--;
		spawn.stopSpawn = false;
		spawn.stopFightTimer = false;
		Destroy(this.GetComponent<AIPatroling>().target);
		Destroy(this.gameObject);

		if (GameObject.Find ("TutorialControl").activeInHierarchy == true)
		{
			GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().nextDialog ();
		}
	}
}
