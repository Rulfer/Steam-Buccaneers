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

	public float aiHealthMat2;
	public float aiHealthMat3;
	public float aiHealth = 20;
	public float detectDistance;
	public float aiRadar = 200;

	public bool testedFleeing = false;
	public bool detectedPlayer = false;
	public bool isBoss = false;
	public bool isCargo = false;
	public bool usingMat2 = false;
	public bool usingMat3 = false;

	public int arrayIndex;
	private int ranNum;

	void Start () 
	{
		playerPoint = GameObject.FindGameObjectWithTag ("Player"); //As the player is a prefab, I had to add it to the variable this way
		aiHealthMat2= aiHealth * 0.66f;
		aiHealthMat3 = aiHealth * 0.33f;
	}
	
	void Update () 
	{
		detectDistance = Vector3.Distance (playerPoint.transform.position, this.transform.position); //calculates the distance between the AI and the player

		if(detectDistance < aiRadar - 100)
		{
			if(isCargo == false)
				this.GetComponent<AImove>().force = PlayerMove2.move.force;
		}

		if(isBoss == false)
		{
			if(detectedPlayer == false)
			{
				if(detectDistance < aiRadar)
				{
					deaktivatePatroling();
					allAIFlee();
				}
			}
		}
		else 
			deaktivatePatroling();

		if(detectDistance > 350)//If the distance is greater than this number, delete this AI
			killAbsentAI();
	}

	public void deaktivatePatroling()
	{
		detectedPlayer = true;
		if(SceneManager.GetActiveScene().name != "Tutorial")
			SpawnAI.spawn.stopFightTimer = true;
		this.GetComponent<AIPatroling>().enabled = false;
		this.GetComponent<AImove>().isPatroling = false;
		if(isCargo == false)
			this.GetComponent<AImove>().force = PlayerMove2.move.force + 200;
		else
		{
			testedFleeing = true;
			this.GetComponent<AImove>().flee();
			this.GetComponent<AImove>().force = PlayerMove2.move.force - 100;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Planet")
		{
			killAI();
		}
	}

	public void allAIFlee()
	{
		if(SceneManager.GetActiveScene().name != "Tutorial")
		{
			SpawnAI.spawn.stopSpawn = true;
			if(isBoss == false && isCargo == false)
			{
				int i = 0;
				while(i < SpawnAI.spawn.maxMarines)
				{
					if(i != arrayIndex)
					{
						if(SpawnAI.spawn.marineShips[i] != null)
						{
							SpawnAI.spawn.marineShips[i].GetComponent<AIPatroling>().enabled = false;
							SpawnAI.spawn.marineShips[i].GetComponent<AImove>().isPatroling = false;
							SpawnAI.spawn.marineShips[i].GetComponent<AIMaster>().testedFleeing = true;
							SpawnAI.spawn.marineShips[i].GetComponent<AImove>().flee();
							Debug.Log("We are doing this!" + i);
						}
						Debug.Log("i" + i);
					}
					i++;
				}
				if(GameObject.Find("Cargo(Clone)") == true)
				{
					GameObject temp = GameObject.Find("Cargo(Clone)").gameObject;
					temp.GetComponent<AIPatroling>().enabled = false;
					temp.GetComponent<AImove>().isPatroling = false;
					temp.GetComponent<AIMaster>().testedFleeing = true;
					temp.GetComponent<AImove>().flee();
				}
			}

			else if(isCargo == true)
			{
				if(testedFleeing == false)
				{
					int i = 0;

					while(i < SpawnAI.spawn.maxMarines)
					{
						if(SpawnAI.spawn.marineShips[i] != null)
						{
							SpawnAI.spawn.marineShips[i].GetComponent<AIPatroling>().enabled = false;
							SpawnAI.spawn.marineShips[i].GetComponent<AImove>().isPatroling = false;
							SpawnAI.spawn.marineShips[i].GetComponent<AIMaster>().testedFleeing = true;
							SpawnAI.spawn.marineShips[i].GetComponent<AImove>().flee();
						}
						i++;
					}
				}
			}

			else if(isBoss == true)
			{
				int i = 0;
				while(i < SpawnAI.spawn.maxMarines)
				{
					if(SpawnAI.spawn.marineShips[i] != null)
					{
						SpawnAI.spawn.marineShips[i].GetComponent<AIPatroling>().enabled = false;
						SpawnAI.spawn.marineShips[i].GetComponent<AImove>().isPatroling = false;
						SpawnAI.spawn.marineShips[i].GetComponent<AIMaster>().testedFleeing = true;
						SpawnAI.spawn.marineShips[i].GetComponent<AImove>().flee();
					}
					i++;
				}
				if(GameObject.Find("Cargo(Clone)") == true)
				{
					GameObject temp = GameObject.Find("Cargo(Clone)").gameObject;
					temp.GetComponent<AIPatroling>().enabled = false;
					temp.GetComponent<AImove>().isPatroling = false;
					temp.GetComponent<AIMaster>().testedFleeing = true;
					temp.GetComponent<AImove>().flee();
				}
			}
		}
	}

//	public void killMarines()
//	{
//		if(SceneManager.GetActiveScene().name != "Tutorial")
//		{
//			SpawnAI.spawn.stopSpawn = true;
//			for(int i = 0; i < SpawnAI.spawn.marineShips.Length; i++)
//			{
//				if(isBoss == false && isCargo == false)
//				{
//					if(i != arrayIndex)
//					{
//						if(SpawnAI.spawn.marineShips[i] != null)
//						{
//							Destroy(SpawnAI.spawn.marineShips[i].GetComponent<AIPatroling>().target);
//							Destroy(SpawnAI.spawn.marineShips[i]);
//							SpawnAI.spawn.marineShips[i] = null;
//							SpawnAI.spawn.availableIndes[i] = true;
//							SpawnAI.spawn.livingShips--;
//						}
//					}
//				}
//				else
//				{
//					if(SpawnAI.spawn.marineShips[i] != null)
//					{
//						Destroy(SpawnAI.spawn.marineShips[i].GetComponent<AIPatroling>().target);
//						Destroy(SpawnAI.spawn.marineShips[i]);
//						SpawnAI.spawn.marineShips[i] = null;
//						SpawnAI.spawn.availableIndes[i] = true;
//						SpawnAI.spawn.livingShips--;
//					}
//					if(GameObject.Find("Cargo(Clone)") == true && isBoss == true)
//					{
//						Destroy(GameObject.Find("Cargo(Clone)").gameObject);
//					}
//				}
//
//			}
//		}
//	}

	private void killAbsentAI()
	{
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
			Destroy(this.GetComponent<AIPatroling>().target);
		}
		Destroy(this.gameObject);
	}


	public void killAI()
	{
		int temp;
		if(isCargo == false)
			temp = Random.Range(1, 7);
		else
			temp = Random.Range(7, 15);
		
		for(int i = 0; i < temp; i++)
			Instantiate(scrap, this.transform.position, this.transform.rotation);

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

	public void changeMat3()
	{
		if(usingMat3 == false)
		{
			aiModelObject.GetComponent<Renderer>().material = new Material(mat3);
			this.GetComponent<DamagedAI>().startFire();
			usingMat3 = true;
		}
	}	

	public void changeMat2()
	{
		if(usingMat2 = false)
		{
			aiModelObject.GetComponent<Renderer>().material = new Material(mat2);
			this.GetComponent<DamagedAI>().startSmoke();
			usingMat2 = true;
		}
	}

	public void testFleeing()
	{
		if(testedFleeing == false && isBoss == false)
		{
			int ranNum = Random.Range(1, 11);
			{
				if(ranNum > 9)
				{
					this.GetComponent<AImove>().flee();
				}
				testedFleeing = true;
			}
		}
	}
}
