using UnityEngine;
using System.Collections;

public class scrapRandomDirection : MonoBehaviour {

	private float randomDirectionX; //Random X direction
	private float randomDirectionZ; //Random Z direction
	private Vector3 axisOfRotation; //Random rotation axis

	private Rigidbody scrapRigid;

	private int speed = 1500;
	private int value; //How much money the player get from pickin up this scrap
	public int killTimer = 2; //Changed in unity editor. Used to destroy this object after a given time

	private float angularVelocity; //Speed of the rotation

	// Use this for initialization
	void Start () {
		randomDirectionX = Random.onUnitSphere.x;
		randomDirectionZ = Random.onUnitSphere.z;
		axisOfRotation = Random.onUnitSphere;
		angularVelocity = Random.Range (20, 40);
		value = Random.Range(1, 5);
		scrapRigid = GetComponent<Rigidbody>();

		scrapRigid.AddForce(new Vector3(randomDirectionX, 0, randomDirectionZ) * speed * Time.deltaTime); //Adds a force for the beginning
		killAfterXSeconds ( killTimer);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(axisOfRotation, angularVelocity * Time.smoothDeltaTime); //Rotates the object
	}

	void killAfterXSeconds(int timer)
	{
		Invoke("kill", timer);
	}

	//If the object hits the player it means that the player picked it up.
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			GameControl.control.money += value; //Pay the player
			Debug.Log("Player scrap = " + GameControl.control.money);
			kill();
		}
	}

	void kill()
	{
		if (GameObject.Find ("TutorialControl").activeInHierarchy == true)
		{
			GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().nextDialog ();
		}
		Destroy(this.gameObject); //Destroy this object
	}
}
