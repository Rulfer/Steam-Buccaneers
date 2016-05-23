using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrapRandomDirection : MonoBehaviour 
{
	private float randomDirectionX; //Random X direction
	private float randomDirectionZ; //Random Z direction
	private float angularVelocity; //Speed of the rotation

	private Vector3 axisOfRotation; //Random rotation axis
	private Rigidbody scrapRigid; //The rigidbody of the scrap

	private int speed = 1500000; //Speed of which the srap moves
	public int value; //How much money the player get from pickin up this scrap
	public int killTimer = 2; //Changed in unity editor. Used to destroy this object after a given time


	// Use this for initialization
	void Start () {
		randomDirectionX = Random.onUnitSphere.x; //Random X direction to move
		randomDirectionZ = Random.onUnitSphere.z; //Random Z direction to move
		axisOfRotation = Random.onUnitSphere; //Random rotation to the model
		angularVelocity = Random.Range (20, 40); //Random rotation speed
		scrapRigid = GetComponent<Rigidbody>();

		scrapRigid.AddForce(new Vector3(randomDirectionX, 0, randomDirectionZ) * speed * Time.deltaTime); //Adds a force for the beginning
		killAfterXSeconds ( killTimer); //Kill the scrap after 20 seconds (changed in the inspector)
	}

	public void setValue(int one, int two, int three) //The value of the scrap is equal to each of the cannon levels of the ship that spawned the scrap
	{
		value = 1 * one + 2 * two + 3 * three; //Generates value
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(axisOfRotation, angularVelocity * Time.smoothDeltaTime); //Rotates the object
	}

	void killAfterXSeconds(int timer)
	{
		Invoke("kill", timer); //Kills this object after "timer" seconds
	}

	//If the object hits the player it means that the player picked it up.
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player") //Hit the player
		{
			GameControl.control.money += value; //Pay the player
			GameObject.Find("value_scraps_tab").GetComponent<Text>().text = GameControl.control.money.ToString(); //Update GUI information

			Destroy(this.gameObject); //Destroy this object
			if (GameObject.Find("TutorialControl") != null) //If this is the tutorial scene
			{
				GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().countingDownScrap (); //Cound how many scraps has been picked up
			}
		}
	}

	void kill()
	{
		Destroy(this.gameObject); //Destroy this object
		if (GameObject.Find("TutorialControl") != null) //If this is the tutorial scene
		{
			GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().countingDownScrap (); //Cound how many scraps has been picked up (destroyd)
		}
	}
}
