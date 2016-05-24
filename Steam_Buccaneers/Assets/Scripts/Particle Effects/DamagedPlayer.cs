using UnityEngine;
using System.Collections;

public class DamagedPlayer : MonoBehaviour 
{
	public GameObject[] smokeParticles; //Array holding all smoke simulations
	public GameObject[] fireParticles; //Array holding all fire simulations
	public bool isSmoking = true; //Test if the smoke simulation has already started
	public bool isBurning = true; //Test if the fire simulation has already started
	// Use this for initialization
	void Start () {
		isSmoking = true; //Needs to be true for checkPlayerHealth in ChangeMaterial.cs to work
		isBurning = true; //Needs to be true for checkPlayerHealth in ChangeMaterial.cs to work
	}

	public void startSmoke() //Start all smoke particles in the smokeParticles array
	{
		isSmoking = true; //Smokes are playing
		foreach(GameObject go in smokeParticles) //Activate all particles
			go.SetActive(true);
	}

	public void startFire() //Start all fire particles in the fireParticles array
	{
		isBurning = true; //Fires are playing
		foreach(GameObject go in fireParticles) //Activate all particles
			go.SetActive(true);
	}

	public void removeSmoke() //Stop all smoke particles
	{
		isSmoking = false; //No longer smoking
		foreach(GameObject go in smokeParticles) //Deactivate all particles
			go.SetActive(false);
	}

	public void removeFire() //Stop all fire particles
	{
		isBurning = false; //No longer burning
		foreach(GameObject go in fireParticles) //Deactivate all particles
			go.SetActive(false);
	}
}
