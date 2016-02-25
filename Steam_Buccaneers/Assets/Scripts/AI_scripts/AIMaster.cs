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
	public bool detectedPlayer = false;

	public float aiHealth = 20;
	public int arrayIndex;
	private int ranNum;

	// Use this for initialization
	void Start () {
		playerPoint = GameObject.FindGameObjectWithTag ("Player"); //As the player is a prefab, I had to add it to the variable this way
		aiHealthMat2= aiHealth * 0.66f;
		aiHealthMat3 = aiHealth * 0.33f;
	}
	
	void Update () {
		detectDistance = Vector3.Distance (playerPoint.transform.position, this.transform.position); //calculates the distance between the AI and the player

		if(detectDistance < 60)
		{
			this.GetComponent<AImove>().maxVelocity.x = 3.5f;
			this.GetComponent<AImove>().maxVelocity.z = 3.5f;
			this.GetComponent<AImove>().force = 200f;
		}

		if(detectDistance < 100)
		{
			if(detectedPlayer == false)
				deaktivatePatroling();
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
						this.GetComponent<AImove>().flee();
					}
				}
			}
		}

		if(aiHealth <= aiHealthMat3)
			aiModelObject.GetComponent<Renderer>().material = new Material(mat3);

		else if(aiHealth <= aiHealthMat2)
			aiModelObject.GetComponent<Renderer>().material =new Material(mat2);

		if(detectDistance > 500)
			killAI();
		
		if(aiHealth <= 0)
			killAI();
	}

	public void deaktivatePatroling()
	{
		detectedPlayer = true;
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
		Destroy(this.GetComponent<AIPatroling>().target);
		Destroy(this.gameObject);
		if (GameObject.Find ("TutorialControl").activeInHierarchy == true)
		{
			GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().nextDialog ();
		}
	}
}
