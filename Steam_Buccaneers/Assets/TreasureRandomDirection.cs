using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TreasureRandomDirection : MonoBehaviour {

	private float randomDirectionX; //Random X direction
	private float randomDirectionZ; //Random Z direction
	private float angularVelocity; //Speed of the rotation

	private Vector3 axisOfRotation; //Random rotation axis
	private Rigidbody scrapRigid;

	private int speed = 1500000;
	public int value; //How much money the player get from pickin up this scrap


	// Use this for initialization
	void Start () {
		randomDirectionX = Random.onUnitSphere.x;
		randomDirectionZ = Random.onUnitSphere.z;
		axisOfRotation = Random.onUnitSphere;
		angularVelocity = Random.Range (20, 40);
		scrapRigid = GetComponent<Rigidbody>();
		value = Random.Range(2, 5);
		scrapRigid.AddForce(new Vector3(randomDirectionX, 0, randomDirectionZ) * speed * Time.deltaTime); //Adds a force for the beginning
	}

	// Update is called once per frame
	void Update () {
		this.transform.Rotate(axisOfRotation, angularVelocity * Time.smoothDeltaTime); //Rotates the object
	}

	//If the object hits the player it means that the player picked it up.
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player")
		{
			GameControl.control.money += value; //Pay the player
			GameObject.Find("value_scraps_tab").GetComponent<Text>().text = GameControl.control.money.ToString();

			Destroy(this.gameObject); //Destroy this object
		}
	}

	public void kill()
	{
		Destroy(this.gameObject); //Destroy this object
	}
}
