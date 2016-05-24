using UnityEngine;
using System.Collections;

public class DamagedAI : MonoBehaviour
{
	public GameObject[] smokeParticles; //Holds all smoke particles
	public GameObject[] fireParticles; //Holds all fire particles

	// Use this for initialization
	void Start () {
		foreach(GameObject go in smokeParticles) //Stop all smoke particles when the ai spawns
			go.SetActive(false);
		foreach(GameObject go in fireParticles) //Stop all fire particles when the ai spawns
			go.SetActive(false);		
	}

	public void startSmoke() //Start smoking
	{
		foreach(GameObject go in smokeParticles) //Activate all smoke simulations
			go.SetActive(true);
	}

	public void startFire() //Start burning
	{
		foreach(GameObject go in fireParticles) //Activate all fire simulations
			go.SetActive(true);
	}
}
