using UnityEngine;
using System.Collections;

public class detectShip : MonoBehaviour {

	//These are simple hit-boxes on the sides of the ship. 
	//If the player is insode a box, the ship will start shooting.
	//When the player goes out of the box, the AI will stopp shooting.
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
