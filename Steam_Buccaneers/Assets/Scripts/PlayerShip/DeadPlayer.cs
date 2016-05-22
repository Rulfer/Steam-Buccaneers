using UnityEngine;
using System.Collections;

public class DeadPlayer : MonoBehaviour 
{
	public GameObject boom; //Reference to the explosion simulation
	public Vector3 axisOfRotation; //Random rotation axis
	public float angularVelocity; //Random velocity for the rotation

	private float rotateTimer = 0; //Used to stop the timer
	private float rotateDuration = 5; //How long it takes for the ship to reach a standstill

	private AudioSource source; //Audiosource
	public AudioClip[] clips; //Array of audio

	public void killPlayer()
	{
		axisOfRotation = Random.onUnitSphere; //Random rotation direction
		angularVelocity = Random.Range (20, 40); //Random velocity
		this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; //Remove constraints to allow other ships to push it
		Instantiate(boom, this.transform.position, this.transform.rotation); //Instantiate the particle simulation at this position
		source = this.GetComponent<AudioSource>(); //Get the audio source
		source.clip = clips[Random.Range(0, 5)]; //Play a random audio clip
		source.volume = 1; //Set the volume to 1
		source.Play(); //Play the clip
	}

	void Update()
	{
		rotateDuration -= Time.deltaTime; //Time has passed
		if(rotateDuration > rotateTimer) //Still not rotated enough
			this.transform.Rotate(axisOfRotation, angularVelocity * Time.smoothDeltaTime * rotateDuration * 0.5f); //Rotates the object
	}
}
