using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AIMaster : MonoBehaviour 
{
	public GameObject scrap;
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
		aiHealth = 1;
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
							if(SpawnAI.spawn.stopSpawn == false && isCargo == false)
							{
								isFighting = true;
								deaktivatePatroling();
							}
							else if(SpawnAI.spawn.stopSpawn == true && isCargo == false)
								thisAIFlee();
						}
						else
						{
							isFighting = true;
							deaktivatePatroling();
						}
					}
				}
				else
				{
					if(detectDistance > aiRadar + 50)
						reactivatePatroling();
				}
			}
			else if(isBoss == true)
			{
				isFighting = true;
				deaktivatePatroling();
			}

			if(detectDistance > 350)//If the distance is greater than this number, delete this AI
				killAbsentAI();
		}
//		else
//		{
//			if(source.isPlaying == true)
//			{
//				sourcePlaying = true;
//			}
//			else
//				sourcePlaying = false;
//		}
	}

	public void reactivatePatroling()
	{	
		detectedPlayer = false;
		testedFleeing = false;
		if(isFighting == true)
		{
			isFighting = false;
			SpawnAI.spawn.stopSpawn = false;
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
		if(SceneManager.GetActiveScene().name != "Tutorial" && isCargo == false)
		{
			SpawnAI.spawn.stopFightTimer = true;
			SpawnAI.spawn.stopSpawn = true;
		}
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
			killAbsentAI();
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
			SpawnAI.spawn.marineShips[arrayIndex].GetComponent<AImove>().flee();
		}
		else if(isCargo == true)
		{
			GameObject temp = GameObject.Find("Cargo(Clone)").gameObject;
			testedFleeing = true;
			temp.GetComponent<AIPatroling>().enabled = false;
			temp.GetComponent<AImove>().isPatroling = false;
			temp.GetComponent<AImove>().flee();
		}
	}

	public void allAIFlee()
	{
		if(SceneManager.GetActiveScene().name != "Tutorial")
		{
			SpawnAI.spawn.stopSpawn = true;
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
			if(isFighting == true)
			{			
				SpawnAI.spawn.stopSpawn = false;
				SpawnAI.spawn.stopFightTimer = false;
			}
			Destroy(this.GetComponent<AIPatroling>().target);
		}

		Instantiate(boom, this.transform.position, this.transform.rotation);
		boom.GetComponent<DeleteParticles>().killDuration = 3;

//		this.GetComponent<DeadAI>().axisOfRotation = this.GetComponent<Rigidbody>().angularVelocity;
//		//this.GetComponent<DeadAI>().axisOfRotation = this.GetComponent<Rigidbody>().velocity.x ;
//		float xSpeed = this.GetComponent<Rigidbody>().velocity.x;
//		float zSpeed = this.GetComponent<Rigidbody>().velocity.z;
//		if(xSpeed < 0)
//			xSpeed *= -1;
//		if(zSpeed < 0)
//			zSpeed *= -1;
//		float speed = xSpeed + zSpeed;
//		this.GetComponent<DeadAI>().angularVelocity = speed;

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

	private void deactivateAI()
	{
		this.GetComponent<AImove>().enabled = false;
		this.GetComponent<AIavoid>().enabled = false;
		this.GetComponent<AIPatroling>().enabled = false;
		//this.GetComponent<AIMaster>().enabled = false;
		this.GetComponent<DeadAI>().enabled = true;
		kill.gameObject.SetActive(false);
	}
}
