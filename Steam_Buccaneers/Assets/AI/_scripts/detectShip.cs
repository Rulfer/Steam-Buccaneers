using UnityEngine;
using System.Collections;

public class detectShip : MonoBehaviour {


	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player" && this.gameObject.tag == "detectRight") {
			AIsideCanons.fireLeft = true;
		}

		if (other.gameObject.tag == "Player" && this.gameObject.tag == "detectLeft") {
			AIsideCanons.fireRight = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			AIsideCanons.fireLeft = false;
			AIsideCanons.fireRight = false;
		}
	}
}
