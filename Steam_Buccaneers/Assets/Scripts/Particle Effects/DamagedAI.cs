using UnityEngine;
using System.Collections;

public class DamagedAI : MonoBehaviour
{
	public GameObject[] smokeParticles;
	public GameObject[] fireParticles;

	// Use this for initialization
	void Start () {
		foreach(GameObject go in smokeParticles)
			go.SetActive(false);
		foreach(GameObject go in fireParticles)
			go.SetActive(false);		
	}

	public void startSmoke()
	{
		foreach(GameObject go in smokeParticles)
			go.SetActive(true);
	}

	public void startFire()
	{
		foreach(GameObject go in fireParticles)
			go.SetActive(true);
	}
}
