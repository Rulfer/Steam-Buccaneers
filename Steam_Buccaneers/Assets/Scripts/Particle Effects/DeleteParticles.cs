using UnityEngine;
using System.Collections;

public class DeleteParticles : MonoBehaviour {
	private float killTimer; //How long the particle simulation has been alive
	public float killDuration; //Kill the simulation after X seconds

	// Update is called once per frame
	void Update () {
		killTimer += Time.deltaTime; //Increase timer
		if(killTimer >= killDuration) //Particle simulation is going to die
			Destroy(this.gameObject); //Kill the simulation and gameobject
	}
}
