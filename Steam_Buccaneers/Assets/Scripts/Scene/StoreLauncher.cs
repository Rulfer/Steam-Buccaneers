using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StoreLauncher : MonoBehaviour {

	//Picks up all AI and shops in arrays and deletes them. Also bossSpawn
	private GameObject[] AI;
	private GameObject[] shops;
	public GameObject bossSpawn;

	private bool entered = false;

	void OnTriggerEnter(Collider collision)
	{
		//If player enter shop trigger
		if (collision.gameObject.tag == "Player")
		{
			//And it is tutorial
			if (GameObject.Find ("TutorialControl") != null)
			{
				//And the tutorial says that player can enter the store
				if (GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().enterStore == true)
				{
					//He will enter
					//Saves the store name
					GameControl.control.storeName = this.name;
					//Writes data to file in GameControl.cs
					GameControl.control.Save ("null");
					//Write whatever scene we want to go to here
					GameControl.control.ChangeScene("Shop");
					ChangeScene.inShop = true;
				}
					
			} 
			//And it is not tutorial
			else
			{
				//He will enter	
				//Saves the store name
				GameControl.control.storeName = this.name;
				//Writes data to file in GameControl.cs
				Debug.Log(GameObject.Find(this.name));
				GameControl.control.Save (this.name);

				GameControl.control.isFighting = false;

				//Write whatever scene we want to go to here
				GameControl.control.ChangeScene ("Shop");
				ChangeScene.inShop = true;
			}
			entered = true;
		}
	}

	//Sometimes the player is inside the shop before the OnTriggerEnter notices it. 
	//This check is to combat that issue.
	void OnTriggerStay(Collider collision) 
	{
		if(collision.gameObject.tag == "Player" && entered == false)
		{
			entered = true;
			if (GameObject.Find ("TutorialControl") != null)
			{
				if (GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().enterStore == true)
				{
					//Saves the store name
					GameControl.control.storeName = this.name;
					//Writes data to file in GameControl.cs
					GameControl.control.Save ("null");
					//Write whatever scene we want to go to here
					GameControl.control.ChangeScene("Shop");
					ChangeScene.inShop = true;
				}

			} 
			else
			{

				//Saves the store name
				GameControl.control.storeName = this.name;
				//Writes data to file in GameControl.cs
				Debug.Log(GameObject.Find(this.name));
				GameControl.control.Save (this.name);

				GameControl.control.isFighting = false;

				//Write whatever scene we want to go to here
				GameControl.control.ChangeScene ("Shop");
				ChangeScene.inShop = true;
			}
		}
	}

}
