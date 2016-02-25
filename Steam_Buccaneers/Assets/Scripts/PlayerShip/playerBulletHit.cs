using UnityEngine;
using System.Collections;

public class playerBulletHit : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "aiShip")
		{
			other.GetComponent<AIMaster>().aiHealth --;
			Destroy(this.gameObject);
		}
		if(other.tag == "Player") //Hit itself in the confusion!
		{
			GameControl.control.health --;
			Destroy(this.gameObject);
		}
	}
}
