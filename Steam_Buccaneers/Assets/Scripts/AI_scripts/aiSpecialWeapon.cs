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
//		bomb.transform.parent = this.transform;
//		bomb.transform.position = this.transform.position;
//		this.transform.DetachChildren();

		Invoke("placeBomb", 5);
	}
}
