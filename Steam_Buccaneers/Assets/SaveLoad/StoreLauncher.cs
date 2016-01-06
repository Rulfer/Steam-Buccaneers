using UnityEngine;
using System.Collections;

public class StoreLauncher : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		//When player hits store he travles to store
		if (collision.gameObject.tag == "Player") 
		{
			//Updates controller with current data. Here posstions to player and meteor
			GameObject goP = GameObject.FindGameObjectWithTag ("Player");
			GameControl.control.shipPos = goP.transform.position;
			GameObject goM = GameObject.FindGameObjectWithTag ("Meteor");
			GameControl.control.meteorPos = goM.transform.position;
			GameControl.control.storeTag = this.tag;
			//Writes data to file in GameControl.cs
			GameControl.control.Save ();
			//Write whatever scene we want to go to here
			GameControl.control.ChangeScene("Store");
		}
	}
}
