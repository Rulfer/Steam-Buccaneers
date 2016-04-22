using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AIsideCanons : MonoBehaviour {

	public static AIsideCanons canons;
	public GameObject cannonball1;
	public GameObject cannonball2;
	public GameObject cannonball3;
	public GameObject[] leftCannons;
	public GameObject[] rightCannons;
	public GameObject[] allCannons;
	private int[] cannonLevel = new int[6];

	public float fireRate;
	public float fireDelayLeft;
	public float fireDelayRight;

	public static int damageOutput;

	public Mesh mesh1;
	public Mesh mesh2;
	public Mesh mesh3;

	private Mesh initialMesh;

	private Vector3 right;
	private Vector3 left;

	public bool fireLeft = false;
	public bool fireRight = false;

	private int detectDistance = 100;

	public AudioClip[] cannonFireSounds;
	public AudioSource sourceLeft;
	public AudioSource sourceRight;

	// Use this for initialization
	void Start () {
		canons = this;
		if(SceneManager.GetActiveScene().name != "Tutorial")
		{
			if(this.transform.root.name == "Marine(Clone)")
			{
				for(int i = 0; i < 6; i++)
				{
					if(SpawnAI.cannonLevel[i] == 1)
					{
						allCannons[i].GetComponent<MeshFilter>().mesh = mesh1;
						cannonLevel[i] = 1;
					}
					else if(SpawnAI.cannonLevel[i] == 2)
					{
						allCannons[i].GetComponent<MeshFilter>().mesh = mesh2;
						cannonLevel[i] = 2;
					}
					else if(SpawnAI.cannonLevel[i] == 3)
					{
						allCannons[i].GetComponent<MeshFilter>().mesh = mesh3;
						cannonLevel[i] = 3;
					}
				}
			}
			else if(this.transform.root.name == "Boss(Clone)")
			{
				for(int i = 0; i < 6; i++)
				{
					SpawnAI.spawn.cannonUpgraded[i] = false;
				}
			}
			else
			{
				for(int i = 0; i < 2; i++)
				{
					if(SpawnAI.cannonLevel[i] == 1)
					{
						allCannons[i].GetComponent<MeshFilter>().mesh = mesh1;
						cannonLevel[i] = 1;
					}
					else if(SpawnAI.cannonLevel[i] == 2)
					{
						allCannons[i].GetComponent<MeshFilter>().mesh = mesh2;
						cannonLevel[i] = 2;
					}
					else if(SpawnAI.cannonLevel[i] == 3)
					{
						allCannons[i].GetComponent<MeshFilter>().mesh = mesh3;
						cannonLevel[i] = 3;
					}
				}
			}
		}
		else
		{
			for(int i = 0; i < 6; i++)
			{
				allCannons[i].GetComponent<MeshFilter>().mesh = mesh1;
				cannonLevel[i] = 1;
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

		if (fireLeft == true && Time.time > fireDelayLeft) 
		{
			fireDelayLeft = Time.time + fireRate;

			int tempSound = Random.Range(0, 3);
			sourceLeft.clip = cannonFireSounds[tempSound];
			sourceLeft.Play();

			if(this.transform.root.name == "Boss(Clone)")
			{
				for(int i = 0; i < 6; i++)
				{
					Instantiate (cannonball3, leftCannons[i].transform.position, leftCannons[i].transform.rotation);
				}
			}

			else
			{
				for(int i = 0; i < leftCannons.Length; i++)
				{
					if(cannonLevel[i] == 1)
					{
						Instantiate (cannonball1, leftCannons[i].transform.position, leftCannons[i].transform.rotation);
					}
					else if(cannonLevel[i] == 2)
					{
						Instantiate (cannonball2, leftCannons[i].transform.position, leftCannons[i].transform.rotation);
					}
					else if(cannonLevel[i] == 3)
					{
						Instantiate (cannonball3, leftCannons[i].transform.position, leftCannons[i].transform.rotation);
					}
				}
			}
		}

		if (fireRight == true && Time.time > fireDelayRight) 
		{				
			fireDelayRight = Time.time + fireRate;

			int tempSound = Random.Range(0, 3);
			sourceRight.clip = cannonFireSounds[tempSound];
			sourceRight.Play();

			if(this.transform.root.name == "Boss(Clone)")
			{
				for(int i = 0; i < 6; i++)
				{
					Instantiate (cannonball3, rightCannons[i].transform.position, transform.rotation);
				}
			}

			else if(this.transform.root.name == "Marine(Clone)")
			{
				for(int i = 0; i < rightCannons.Length; i++)
				{
					if(cannonLevel[i+3] == 1)
					{
						Instantiate (cannonball1, rightCannons[i].transform.position, transform.rotation);
					}
					else if(cannonLevel[i+3] == 2)
					{
						Instantiate (cannonball2, rightCannons[i].transform.position, transform.rotation);
					}
					else if(cannonLevel[i+3] == 3)
					{
						Instantiate (cannonball3, rightCannons[i].transform.position, transform.rotation);
					}
				}
			}
			else if(this.transform.root.name == "Cargo(Clone)")
			{
				if(cannonLevel[1] == 1)
				{
					Instantiate (cannonball1, rightCannons[0].transform.position, transform.rotation);
				}
				else if(cannonLevel[1] == 2)
				{
					Instantiate (cannonball2, rightCannons[0].transform.position, transform.rotation);
				}
				else if(cannonLevel[1] == 3)
				{
					Instantiate (cannonball3, rightCannons[0].transform.position, transform.rotation);
				}
			}
		}
	}

	private void checkGunPosition()
	{
		if(this.transform.root.name != "Boss(Clone)")
		{
			RaycastHit objectHit;

			if(Physics.Raycast(this.transform.position, left, out objectHit, detectDistance)) //Raycast hit something
			{
				if(objectHit.transform.root.name == "PlayerShip")//Hit the player
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
				if(objectHit.transform.root.name == "PlayerShip")//Hit the player
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
}