using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AIMaster : MonoBehaviour 
{
	public GameObject[] scrap;
	public GameObject aiModelObject;
	private GameObject playerPoint;
	public GameObject boom;
	public GameObject kill;

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
	private bool isFighting = false;

	public int arrayIndex;
	private int ranNum;

	public AudioClip clip;
	public AudioSource source;
	public bool isDead = false;
	public bool sourcePlaying = false;

	void Start () 
	{
		playerPoint = GameObject.FindGameObjectWithTag ("Player"); //As the player is a prefab, I had to add it to the variable this way
		aiHealthMat2= aiHealth * 0.66f;
		aiHealthMat3 = aiHealth * 0.33f;
		source = this.GetComponent<AudioSource>();

		if(this.transform.name == "Boss(Clone)")
			kill.gameObject.SetActive(false);
	}
	
	void Update () 
	{
		if(isDead == false)
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
						if(SceneManager.GetActiveScene().name != "Tutorial")
						{
							if(GameControl.control.isFighting == false && isCargo == false)
							{
								deaktivatePatroling();
							}
							else if(GameControl.control.isFighting == true && isCargo == false)
								thisAIFlee();
						}
						else
						{
							deaktivatePatroling();
						}
					}
				}
				else
				{
					if(SceneManager.GetActiveScene().name != "Tutorial")
					{
						if(GameControl.control.isFighting == false)
							reactivatePatroling();
						if(detectDistance > aiRadar + 50)
							reactivatePatroling();
					}
				}
			}
			else if(isBoss == true)
			{
				deaktivatePatroling();
			}

			if(detectDistance > 350)//If the distance is greater than this number, delete this AI
				killAbsentAI();
		}
	}

	public void reactivatePatroling()
	{	
		detectedPlayer = false;
		testedFleeing = false;
		if(isFighting == true)
		{
			isFighting = false;
			GameControl.control.isFighting = false;
			SpawnAI.spawn.stopFightTimer = false;
		}
		this.GetComponent<AIPatroling>().enabled = true;
		this.GetComponent<AImove>().isPatroling = true;
		this.GetComponent<AImove>().isFleeing = false;
		this.GetComponent<AImove>().force = PlayerMove2.move.force;
	}

	public void deaktivatePatroling()
	{
		detectedPlayer = true;
		isFighting = true;

		if(SceneManager.GetActiveScene().name != "Tutorial" && isCargo == false)
		{
			SpawnAI.spawn.stopFightTimer = true;
			GameControl.control.isFighting = true;
		}
		this.GetComponent<AIPatroling>().enabled = false;
		this.GetComponent<AImove>().isPatroling = false;
		if(isCargo == false)
			this.GetComponent<AImove>().force = PlayerMove2.move.force + 200;
		else
		{
			testedFleeing = true;
			this.GetComponent<AImove>().isFleeing = true;
			this.GetComponent<AImove>().force = PlayerMove2.move.force - 100;
		}
	}

	public void thisAIFlee()
	{
		detectedPlayer = true;
		testedFleeing = true;

		if(isBoss == false && isCargo == false)
		{
			SpawnAI.spawn.marineShips[arrayIndex].GetComponent<AIPatroling>().enabled = false;
			SpawnAI.spawn.marineShips[arrayIndex].GetComponent<AImove>().isPatroling = false;
			SpawnAI.spawn.marineShips[arrayIndex].GetComponent<AImove>().isFleeing = true;
		}
		else if(isCargo == true)
		{
			GameObject temp = GameObject.Find("Cargo(Clone)").gameObject;
			testedFleeing = true;
			temp.GetComponent<AIPatroling>().enabled = false;
			temp.GetComponent<AImove>().isPatroling = false;
			temp.GetComponent<AImove>().isFleeing = true;
		}
	}

	public void allAIFlee()
	{
		if(SceneManager.GetActiveScene().name != "Tutorial")
		{
			GameControl.control.isFighting = true;
			SpawnAI.spawn.stopFightTimer = true;
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
							SpawnAI.spawn.marineShips[arrayIndex].GetComponent<AImove>().isFleeing = true;
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
					temp.GetComponent<AImove>().isFleeing = true;
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
							SpawnAI.spawn.marineShips[i].GetComponent<AImove>().isFleeing = true;
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
						SpawnAI.spawn.marineShips[i].GetComponent<AImove>().isFleeing = true;
					}
					i++;
				}
				if(GameObject.Find("Cargo(Clone)") == true)
				{
					GameObject temp = GameObject.Find("Cargo(Clone)").gameObject;
					temp.GetComponent<AIPatroling>().enabled = false;
					temp.GetComponent<AImove>().isPatroling = false;
					temp.GetComponent<AIMaster>().testedFleeing = true;
					temp.GetComponent<AImove>().isFleeing = true;
				}
			}
		}
	}

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
		{
			Instantiate(scrap[Random.Range(0,4)], this.transform.position, this.transform.rotation);
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
			if(isFighting == true)
			{			
				GameControl.control.isFighting = false;
				SpawnAI.spawn.stopFightTimer = false;
			}
			Destroy(this.GetComponent<AIPatroling>().target);
		}

		Instantiate(boom, this.transform.position, this.transform.rotation);
		boom.GetComponent<DeleteParticles>().killDuration = 3;

		this.GetComponent<DeadAI>().enabled = true;
		deactivateAI();

		source.clip = clip;
		source.Play();
		isDead = true;

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
			this.GetComponent<AImove>().force = PlayerMove2.move.force - 100;
		}
	}	

	public void changeMat2()
	{
		if(usingMat2 == false)
		{
			aiModelObject.GetComponent<Renderer>().material = new Material(mat2);
			this.GetComponent<DamagedAI>().startSmoke();
			usingMat2 = true;
			this.GetComponent<AImove>().force = PlayerMove2.move.force - 100;
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
					this.GetComponent<AImove>().isFleeing = true;
				}
				testedFleeing = true;
			}
		}
	}

	private void deactivateAI()
	{
		this.GetComponent<AImove>().enabled = false;
		this.GetComponent<AIavoid>().enabled = false;
		this.GetComponent<AIPatroling>().enabled = false;
		//this.GetComponent<AIMaster>().enabled = false;
		this.GetComponent<DeadAI>().enabled = true;
		kill.gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Planet" || other.tag == "Moon")
		{
			if(isFighting == true)
				killAI();
			else
				killAbsentAI();
		}
	}
}
