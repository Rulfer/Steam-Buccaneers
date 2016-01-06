using UnityEngine;
using System.Collections;

public class StoreLauncher : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		//When player hits store he travles to store
		if (collision.gameObject.tag == "Player") 
		{
			//Writes data to file in GameControl.cs
			GameControl.control.Save ();
			//Write whatever scene we want to go to here
			GameControl.control.ChangeScene("Store");
		}
	}
}
