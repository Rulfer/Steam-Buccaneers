using UnityEngine;
using System.Collections;

public class aiSpecialWeapon : MonoBehaviour {

	public GameObject bomb;

	// Use this for initialization
	void Start () {
		placeBomb();
	}
	
	void placeBomb()
	{
		Instantiate(bomb, this.transform.position, this.transform.rotation);
		Invoke("placeBomb", 5);
	}
}
