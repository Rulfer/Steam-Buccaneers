using UnityEngine;
using System.Collections;

public class DamagedAI : MonoBehaviour
{
	public static DamagedAI dmAI;
	public GameObject[] smokeParticles;
	public GameObject[] fireParticles;

	//These 3 bools are set in the Inspector. 
	public bool marine;
	public bool boss;
	public bool cargo;

	// Use this for initialization
	void Start () {
		dmAI = this;
		foreach(GameObject go in smokeParticles)
			go.SetActive(false);
		foreach(GameObject go in fireParticles)
			go.SetActive(false);
	}

	public void startSmoking()
	{
		if(marine == true)
			startMarineSmoke();
	}
	public void startFire()
	{
		if(marine == true)
			startMarineFire();
	}
	
	private void startMarineSmoke()
	{
		foreach(GameObject go in smokeParticles)
			go.SetActive(true);
	}

	private void startMarineFire()
	{
		foreach(GameObject go in fireParticles)
			go.SetActive(true);
	}
}
