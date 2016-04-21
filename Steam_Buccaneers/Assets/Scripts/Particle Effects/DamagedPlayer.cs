using UnityEngine;
using System.Collections;

public class DamagedPlayer : MonoBehaviour 
{
	public static DamagedPlayer dmPlayer;
	public GameObject[] smokeParticles;
	public GameObject[] fireParticles;
	public bool isSmoking = false;
	public bool isBurning = false;
	// Use this for initialization
	void Start () {
		dmPlayer = this;
		foreach(GameObject go in smokeParticles)
			go.SetActive(false);
		foreach(GameObject go in fireParticles)
			go.SetActive(false);
	}

	public void startSmoke()
	{
		isSmoking = true;
		foreach(GameObject go in smokeParticles)
			go.SetActive(true);
	}

	public void startFire()
	{
		isBurning = true;
		foreach(GameObject go in fireParticles)
			go.SetActive(true);
	}

	public void removeSmoke()
	{
		isSmoking = false;
		foreach(GameObject go in smokeParticles)
			go.SetActive(false);
	}

	public void removeFire()
	{
		isBurning = false;
		foreach(GameObject go in fireParticles)
			go.SetActive(false);
	}
}
