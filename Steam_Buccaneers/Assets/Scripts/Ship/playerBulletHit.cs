using UnityEngine;
using System.Collections;

public class playerBulletHit : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "aiShip")
		{
			AIMaster.aiHealth --;
			Destroy(this.gameObject);
		}
	}
}
