using UnityEngine;
using System.Collections;

public class AIMaster : MonoBehaviour {
	
	public GameObject scrap;
	public GameObject aiModelObject;
	private GameObject playerPoint;

	public Material mat2;
	public Material mat3;
	private float aiHealthMat2;
	private float aiHealthMat3;

	public static float detectDistance;
	private float killtimer = 0;

	private bool testedFleeing = false;
	private bool newSpawn = true;

	public static float aiHealth;
	private int ranNum;

	// Use this for initialization
	void Start () {
		playerPoint = GameObject.FindGameObjectWithTag ("Player"); //As the player is a prefab, I had to add it to the variable this way
		Debug.Log("aiHealth");
		Debug.Log(aiHealth);
		Debug.Log(aiHealth*0.33f);
		Debug.Log(aiHealth*0.66f);
		aiHealthMat2= aiHealth * 0.66f;
		aiHealthMat3 = aiHealth * 0.33f;
	}
	
	void Update () {
		detectDistance = Vector3.Distance (playerPoint.transform.position, this.transform.position); //calculates the distance between the AI and the player

		if(detectDistance < 40)
		{
			AImove.maxVelocity.x = 3.5f;
			AImove.maxVelocity.z = 3.5f;
			AImove.force = 200f;
			newSpawn = false;
		}

		if(aiHealth <= aiHealthMat3)
		{
			aiModelObject.GetComponent<Renderer>().material = new Material(mat3);
			Debug.Log("We changed to mat3");
		}

		else if(aiHealth <= aiHealthMat2)
		{
			aiModelObject.GetComponent<Renderer>().material =new Material(mat2);
			Debug.Log("We changed to mat2");
		}


		if(aiHealth <= 0)
		{
			killAI();
		}

		if(testedFleeing == false)
		{
			if(aiHealth <= (aiHealth*0.2))
			{
				int ranNum = Random.Range(1, 11);
				{
					if(ranNum > 9)
					{
						testedFleeing = true;
						this.gameObject.GetComponent<AImove>().flee();
					}
				}
			}
		}


		if(detectDistance >= 60)
		{
			killtimer+= Time.deltaTime;
		}

		if(detectDistance < 60 || newSpawn == true)
		{
			killtimer = 0;
		}

		if(killtimer > 10)
		{
			killAI();
		}
			
		//We dont want to render the AI if its to far away from the player,
		//so we delete it when the distance is equal or greater than 100 (we can change this number at any time).
//		if (detectDistance >= 100) {
//			Destroy(this.gameObject);
//			spawnAI.livingShip = false;
//		}

		//This is commented out, but is usefull when the AI is patrolling.
		//When its not patrolling, and only always going to move against the player,
		//we dont need this code at all.
//		if (detectDistance <= 40) 
//		{
//			GetComponent<AIPatroling>().enabled = false;
//			GetComponent<MoveTo> ().enabled = true;
//		}
//
//		if (detectDistance >= 50)
//		{
//			GetComponent<AIPatroling> ().enabled = true;
//			GetComponent<MoveTo> ().enabled = false;
//		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Planet")
		{
			Debug.Log("planet pls");
			killAI();
		}

	}

	private void killAI()
	{
		int temp = Random.Range(1, 7);
		for(int i = 0; i < temp; i++)
		{
			Instantiate(scrap);
			scrap.transform.position = this.transform.position;
		}
		spawnAI.livingShip = false;
		Destroy(this.gameObject);
	}
}
