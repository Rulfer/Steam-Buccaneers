using UnityEngine;
using System.Collections;

public class DeadAI : MonoBehaviour {

	GameObject playerPos; //Creates a reference to the player object
	public GameObject newMesh; //Mesh used for dead enemies
	public GameObject currentMesh; //Mesh used while the enemy was alive
	float distance; //Distance between this enemy and the player
	public Vector3 axisOfRotation; //Random rotation direction
	public float angularVelocity; //Random rotation speed

	private float rotateTimer = 0; //Timer
	private float rotateDuration = 5; //How long the ship has left to rotate

	// Use this for initialization
	void Start () 
	{
		playerPos = GameObject.Find("PlayerShip");
		axisOfRotation = Random.onUnitSphere; //Creates a random rotation axis
		angularVelocity = Random.Range (20, 40); //Generates a random speed for the rotation
		this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; //Remove all constraints on the rigidbody, allowing other ships to push this ship around
		currentMesh.GetComponent<MeshCollider>().enabled = false; //Disables current mesh and it's collider
		newMesh.GetComponent<MeshCollider>().enabled = true; //Enables the new mesh and it's collider
	}

	void Update()
	{
		distance = Vector3.Distance(transform.position, playerPos.transform.position); //Checks the distance between the player and this enemy
		if(distance >= 250) //If the distance is big enough
		{
			Destroy(gameObject); //destroy this object
		}
		rotateDuration -= Time.deltaTime; //Decrease remaining rotation time 
		if(rotateDuration > rotateTimer) //Still rotating
			this.transform.Rotate(axisOfRotation, angularVelocity * Time.smoothDeltaTime * rotateDuration * 0.5f); //Rotates the object

	}
}
