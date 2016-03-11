using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StoreLauncher : MonoBehaviour {

	void OnTriggerEnter(Collider collision)
	{
		//When player hits store he travles to store
		if (collision.gameObject.tag == "Player" && GameObject.Find("TutorialControl").GetComponent<Tutorial>().enterStore == true) 
		{
			//Saves the store name
			GameControl.control.storeName = this.name;
			if (SceneManager.GetActiveScene ().name == "Tutorial")
			{
				//Reset values
				Debug.Log("Give back health and spessAmmo");
				Debug.Log (GameControl.control.health);
				GameControl.control.health = 100;
				GameControl.control.specialAmmo = 20;
			}
			//Writes data to file in GameControl.cs
			GameControl.control.Save (this.name);
			//Write whatever scene we want to go to here
			GameControl.control.ChangeScene("Shop");
		}
			
	}

}
