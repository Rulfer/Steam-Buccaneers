using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StoreLauncher : MonoBehaviour {

	private GameObject[] AI;
	private GameObject[] shops;
	public GameObject bossSpawn;

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

//				AI = GameObject.FindGameObjectsWithTag("aiShip");
//				shops = GameObject.FindGameObjectsWithTag("shop");
//				bossSpawn = GameObject.Find("BossSpawn");
//				foreach(GameObject go in AI)
//					Destroy(go);
//				foreach(GameObject go in shops)
//					Destroy(go);
//				Destroy(bossSpawn.gameObject);

				GameControl.control.isFighting = false;

				//Write whatever scene we want to go to here
				GameControl.control.ChangeScene ("Shop");
				ChangeScene.inShop = true;
			}
		}
	}

}
