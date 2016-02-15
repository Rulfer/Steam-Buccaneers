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

	private bool testedFleeing = false;
	public static bool detectedPlayer = false;

	public float aiHealth = 20;
	public int arrayIndex;
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

		this.transform.position = spawnAI.spawnPosition;
	}
	
	void Update () {
		detectDistance = Vector3.Distance (playerPoint.transform.position, this.transform.position); //calculates the distance between the AI and the player

		if(detectDistance < 40)
		{
			AImove.maxVelocity.x = 3.5f;
			AImove.maxVelocity.z = 3.5f;
			AImove.force = 200f;
		}

		if(detectDistance < 100)
		{
			if(detectedPlayer == false)
			{
				detectedPlayer = false;
				AImove.force = 10000f;
			}
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


		if(detectDistance > 300)
		{
			killAI();
		}
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
		spawnAI.livingShips--;
		spawnAI.availableIndes[arrayIndex]= false;
		Destroy(this.gameObject);
	}
}
