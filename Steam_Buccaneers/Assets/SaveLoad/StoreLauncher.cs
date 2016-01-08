using UnityEngine;
using System.Collections;

public class StoreLauncher : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		//When player hits store he travles to store
		if (collision.gameObject.tag == "Player") 
		{
			//Saves the store name
			GameControl.control.storeName = this.name;
			//Writes data to file in GameControl.cs
			GameControl.control.Save (this.name);
			//Write whatever scene we want to go to here
			GameControl.control.ChangeScene("Store");
		}
	}
}
