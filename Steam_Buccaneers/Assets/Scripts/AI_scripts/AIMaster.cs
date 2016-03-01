using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AIMaster : MonoBehaviour 
{
	//private spawnAI.spawnAI spawnAI.spawn;
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

		//spawnAI.spawn = GameObject.Find("spawnAI.spawnsAI").GetComponent<spawnAI.spawnAI>();
	}
	
	void Update () {
		detectDistance = Vector3.Distance (playerPoint.transform.position, this.transform.position); //calculates the distance between the AI and the player

		if(detectDistance < 60)
		{
//			this.GetComponent<AImove>().maxVelocity.x = 35f;
//			this.GetComponent<AImove>().maxVelocity.z = 35f;
			this.GetComponent<AImove>().force = 650f;
		}

		if(isBoss == false)
		{
			if(detectDistance < 150)
			{
				if(detectedPlayer == false)
				{
					Debug.Log("We must kill all other marines!");
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
			aiModelObject.GetComponent<Renderer>().material = new Material(mat3);

		else if(aiHealth <= aiHealthMat2) //It's not low enough, lets check if its low enough for mat2 then
			aiModelObject.GetComponent<Renderer>().material = new Material(mat2);

		if(detectDistance > 300)//If the distance is greater than this number, delete this AI
			killAI();
		
		if(aiHealth <= 0) //If the health if this AI is 0, delete this AI
			killAI();
	}

	public void deaktivatePatroling()
	{
		detectedPlayer = true;
		if(SceneManager.GetActiveScene().name != "Tutorial")
			spawnAI.spawn.stopFightTimer = true;
		this.GetComponent<AIPatroling>().enabled = false;
		this.GetComponent<AImove>().isPatroling = false;
		this.GetComponent<AImove>().force = 1000f;
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
		if(SceneManager.GetActiveScene().name != "Tutorial")
		{
			spawnAI.spawn.stopSpawn = true;
			for(int i = 0; i < spawnAI.spawn.marineShips.Length; i++)
			{
				Debug.Log("We are killing them now,");
				if(i != arrayIndex)
				{
					Debug.Log("Its not this object");
					if(spawnAI.spawn.marineShips[i] != null)
					{
						Debug.Log("Another one died.");
						Destroy(spawnAI.spawn.marineShips[i].GetComponent<AIPatroling>().target);
						Destroy(spawnAI.spawn.marineShips[i]);
						spawnAI.spawn.marineShips[i] = null;
						spawnAI.spawn.availableIndes[i] = true;
						spawnAI.spawn.livingShips--;
					}
				}
			}
		}
	}

	private void killAI()
	{
		Debug.Log("We are killing this ai");
		int temp = Random.Range(1, 7);
		for(int i = 0; i < temp; i++)
		{
			Instantiate(scrap, this.transform.position, this.transform.rotation);
			Debug.Log("We are spawning scrap!");
		}
		if(SceneManager.GetActiveScene().name != "Tutorial")
		{
			spawnAI.spawn.marineShips[arrayIndex] = null;
			Debug.Log("Deleted ship from array");
			spawnAI.spawn.availableIndes[arrayIndex] = true;
			Debug.Log("Made index available");
			spawnAI.spawn.livingShips--;
			spawnAI.spawn.stopSpawn = false;
			spawnAI.spawn.stopFightTimer = false;
			Destroy(this.GetComponent<AIPatroling>().target);
		}

		Destroy(this.gameObject);

		if (GameObject.Find ("TutorialControl").GetComponent<Tutorial>().isActiveAndEnabled == true)
		{
			GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().nextDialog ();
		}
	}
}
