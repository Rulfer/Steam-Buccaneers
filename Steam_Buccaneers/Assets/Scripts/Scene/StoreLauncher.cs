using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StoreLauncher : MonoBehaviour {

	void OnTriggerEnter(Collider collision)
	{

		if (collision.gameObject.tag == "Player")
		{
			if (GameObject.Find ("TutorialControl") != null)
			{
				if (GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().enterStore == true)
				{
					//Saves the store name
					GameControl.control.storeName = this.name;
					//Writes data to file in GameControl.cs
					GameControl.control.Save (this.name);
					//Write whatever scene we want to go to here
					GameControl.control.ChangeScene("Shop");
				}
					
			} 
			else
			{
				//Saves the store name
				GameControl.control.storeName = this.name;
				//Writes data to file in GameControl.cs
				GameControl.control.Save (this.name);
				//Write whatever scene we want to go to here
				GameControl.control.ChangeScene ("Shop");
			}
		}
	}

}
