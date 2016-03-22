using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AIMaster : MonoBehaviour 
{
	//private SpawnAI.SpawnAI SpawnAI.spawn;
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
	public bool isCargo = false;

	public float aiHealth = 20;
	public int arrayIndex;
	private int ranNum;

	// Use this for initialization
	void Start () {
		playerPoint = GameObject.FindGameObjectWithTag ("Player"); //As the player is a prefab, I had to add it to the variable this way
		aiHealthMat2= aiHealth * 0.66f;
		aiHealthMat3 = aiHealth * 0.33f;

		//SpawnAI.spawn = GameObject.Find("SpawnAI.spawnsAI").GetComponent<SpawnAI.SpawnAI>();
	}
	
	void Update () {
		detectDistance = Vector3.Distance (playerPoint.transform.position, this.transform.position); //calculates the distance between the AI and the player

		if(detectDistance < 60)
		{
			if(isCargo == false)
				this.GetComponent<AImove>().force = 650f;
		}

		if(isBoss == false)
		{
			if(detectDistance < 150)
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

		if(aiHealth <= aiHealthMat3) //Change the material to mat3 if the health is low enough
		{
			aiModelObject.GetComponent<Renderer>().material = new Material(mat3);
			this.GetComponent<DamagedAI>().startFire();
		}

		else if(aiHealth <= aiHealthMat2) //It's not low enough, lets check if its low enough for mat2 then
		{
			aiModelObject.GetComponent<Renderer>().material = new Material(mat2);
			this.GetComponent<DamagedAI>().startSmoke();
		}
		if(detectDistance > 350)//If the distance is greater than this number, delete this AI
			killAI();
		
		if(aiHealth <= 0) //If the health if this AI is 0, delete this AI
			killAI();
	}

	public void deaktivatePatroling()
	{
		detectedPlayer = true;
		if(SceneManager.GetActiveScene().name != "Tutorial")
			SpawnAI.spawn.stopFightTimer = true;
		this.GetComponent<AIPatroling>().enabled = false;
		this.GetComponent<AImove>().isPatroling = false;
		if(isCargo == false)
			this.GetComponent<AImove>().force = 1000f;
		else
		{
			testedFleeing = true;
			this.GetComponent<AImove>().flee();
			this.GetComponent<AImove>().force = 650;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Planet")
		{
			killAI();
		}
	}

	public void killMarines()
	{
		if(SceneManager.GetActiveScene().name != "Tutorial")
		{
			SpawnAI.spawn.stopSpawn = true;
			for(int i = 0; i < SpawnAI.spawn.marineShips.Length; i++)
			{
				if(isBoss == false && isCargo == false)
				{
					if(i != arrayIndex)
					{
						if(SpawnAI.spawn.marineShips[i] != null)
						{
							Destroy(SpawnAI.spawn.marineShips[i].GetComponent<AIPatroling>().target);
							Destroy(SpawnAI.spawn.marineShips[i]);
							SpawnAI.spawn.marineShips[i] = null;
							SpawnAI.spawn.availableIndes[i] = true;
							SpawnAI.spawn.livingShips--;
						}
					}
				}
				else
				{
					if(SpawnAI.spawn.marineShips[i] != null)
					{
						Destroy(SpawnAI.spawn.marineShips[i].GetComponent<AIPatroling>().target);
						Destroy(SpawnAI.spawn.marineShips[i]);
						SpawnAI.spawn.marineShips[i] = null;
						SpawnAI.spawn.availableIndes[i] = true;
						SpawnAI.spawn.livingShips--;
					}
					if(GameObject.Find("Cargo(Clone)") == true && isBoss == true)
					{
						Destroy(GameObject.Find("Cargo(Clone)").gameObject);
					}
				}

			}
		}
	}

	private void killAI()
	{
		int temp;
		if(isCargo == false)
			temp = Random.Range(1, 7);
		else
			temp = Random.Range(7, 15);
		for(int i = 0; i < temp; i++)
		{
			Instantiate(scrap, this.transform.position, this.transform.rotation);
		}

		if(SceneManager.GetActiveScene().name != "Tutorial")
		{
			if(isCargo == false)
			{
				SpawnAI.spawn.marineShips[arrayIndex] = null;
				SpawnAI.spawn.availableIndes[arrayIndex] = true;
				SpawnAI.spawn.livingShips--;
			}
			else
				SpawnAI.spawn.livingCargo = false;
			SpawnAI.spawn.stopSpawn = false;
			SpawnAI.spawn.stopFightTimer = false;
			Destroy(this.GetComponent<AIPatroling>().target);
		}

		Destroy(this.gameObject);

		if (GameObject.Find ("TutorialControl") != null)
		{
			GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().nextDialog ();
		}
	}
}
