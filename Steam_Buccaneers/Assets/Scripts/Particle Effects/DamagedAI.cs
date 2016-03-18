using UnityEngine;
using System.Collections;

public class DamagedAI : MonoBehaviour
{
	public static DamagedAI dmAI;
	public GameObject[] smokeParticles;

	//These 3 bools are set in the Inspector. 
	public bool marine;
	public bool boss;
	public bool cargo;

	// Use this for initialization
	void Start () {
		dmAI = this;
		foreach(GameObject go in smokeParticles)
			go.SetActive(false);

//		if(this.gameObject.name == "Marine(Clone)")
//			marine = true;
//		else if(this.gameObject.name == "Boss(Clone)")
//			boss = true;
//		else
//			cargo = true;
	}

	public void startSmoking()
	{
		if(marine == true)
			startMarineSmoke();
	}
	
	public void startMarineSmoke()
	{
		foreach(GameObject go in smokeParticles)
			go.SetActive(true);
	}
}
