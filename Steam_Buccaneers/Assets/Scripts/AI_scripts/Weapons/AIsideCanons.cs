using UnityEngine;
using System.Collections;

public class AIsideCanons : MonoBehaviour {

	public GameObject cannonball;
	public GameObject[] leftCannons;
	public GameObject[] rightCannons;
	private GameObject[] allCannons;

	public float fireRate;
	public float fireDelayLeft;
	public float fireDelayRight;

	public Mesh mesh1;
	public Mesh mesh2;
	public Mesh mesh3;

	private Mesh initialMesh;

	private Vector3 right;
	private Vector3 left;

	public static bool fireLeft = false;
	public static bool fireRight = false;

	private int detectDistance = 50;

	// Use this for initialization
	void Start () {
		allCannons = GameObject.FindGameObjectsWithTag("sideCannons");
		if(this.transform.root.name == "AI_LVL1(Clone)")
		{
			for(int i = 0; i < 6; i++)
			{
				if(spawnAI.cannonLevel[i] == 1)
				{
					allCannons[i].GetComponent<MeshFilter>().mesh = mesh1;
				}
				else if(spawnAI.cannonLevel[i] == 2)
				{
					allCannons[i].GetComponent<MeshFilter>().mesh = mesh2;
				}
				else if(spawnAI.cannonLevel[i] == 3)
				{
					allCannons[i].GetComponent<MeshFilter>().mesh = mesh3;
				}
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		right = this.transform.TransformDirection(Vector3.right);
		left = this.transform.TransformDirection(Vector3.left);

		Debug.DrawRay(this.transform.position, right * detectDistance, Color.green);
		Debug.DrawRay(this.transform.position, left * detectDistance, Color.blue);

		checkGunPosition();


		if (fireLeft == true && Time.time > fireDelayLeft) { // && Inventory.mainAmmo > 0
			fireDelayLeft = Time.time + fireRate;

			for(int i = 0; i <= 2; i++)
			{
				Instantiate (cannonball, leftCannons[i].transform.position, leftCannons[i].transform.rotation);
				if(cannonball.transform.root.name == "AI_LVL1(Clone)")
				{
					if(spawnAI.cannonLevel[i] == 1)
					{
						cannonball.GetComponent<AIprojectile>().damageOutput = 1;
					}
					else if(spawnAI.cannonLevel[i] == 2)
					{
						cannonball.GetComponent<AIprojectile>().damageOutput = 3;
					}
					else if(spawnAI.cannonLevel[i] == 3)
					{
						cannonball.GetComponent<AIprojectile>().damageOutput = 5;
					}
				}
			}
		}

		if (fireRight == true && Time.time > fireDelayRight) {				
			fireDelayRight = Time.time + fireRate;

			for(int i = 0; i <= 2; i++)
			{
				Instantiate (cannonball, rightCannons[i].transform.position, rightCannons[i].transform.rotation);
				if(cannonball.transform.root.name == "AI_LVL1(Clone)")
				{
					if(spawnAI.cannonLevel[i+3] == 1)
					{
						cannonball.GetComponent<AIprojectile>().damageOutput = 1;
					}
					else if(spawnAI.cannonLevel[i+3] == 2)
					{
						cannonball.GetComponent<AIprojectile>().damageOutput = 3;
					}
					else if(spawnAI.cannonLevel[i+3] == 3)
					{
						cannonball.GetComponent<AIprojectile>().damageOutput = 5;
					}
				}
			}
		}
	}

	private void checkGunPosition()
	{
		RaycastHit objectHit;

		if(Physics.Raycast(this.transform.position, left, out objectHit, detectDistance)) //Raycast hit something
		{
			if(objectHit.transform.tag == "Player")//Hit the player
			{
				fireLeft = true;//The AI can now fire
			}
			else //Hit something else than the player
			{
				fireLeft = false; //The AI cant fire anyway
			}
		}
		else //Hit nothing
		{
			fireLeft = false; //The AI cant fire
		}

		if(Physics.Raycast(this.transform.position, right, out objectHit, detectDistance))//Raycast hit something
		{
			if(objectHit.transform.tag == "Player")//Hit the player
			{
				fireRight = true;//The AI can now fire
			}

			else //Hit something else than the player
			{
				fireRight = false; //The AI cant fire anyway
			}
		}
		else//Hit nothing
		{
			fireRight = false;//The AI cant fire
		}
	}
}