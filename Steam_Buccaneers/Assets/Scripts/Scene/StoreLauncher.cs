using UnityEngine;
using System.Collections;

public class StoreLauncher : MonoBehaviour {
//	private spawnAI spawn;

	void OnTriggerEnter(Collider collision)
	{
		//When player hits store he travles to store
		if (collision.gameObject.tag == "Player") 
		{
//			killShips();

			//Saves the store name
			GameControl.control.storeName = this.name;
			//Writes data to file in GameControl.cs
			GameControl.control.Save (this.name);
			//Write whatever scene we want to go to here
			GameControl.control.ChangeScene("Shop");
//			GameObject.Find ("GameControl").GetComponent<spawnAI> ().stopSpawn = true;
//			GameObject.Find ("GameControl").GetComponent<spawnAI> ().enabled = false;
		}

		Debug.Log("Triggered!");
	}

//	void killShips()
//	{
//		spawn = GameObject.Find ("GameControl").GetComponent<spawnAI> ();
//		for(int i = 0; i <spawn.marineShips.Length; i++)
//		{
//			//Destroy(spawn.marineShips[i].GetComponent<AIPatroling>().target);
//			Destroy(spawn.marineShips[i]);
//			spawn.marineShips[i] = null;
//			spawn.availableIndes[i] = true;
//			spawn.livingShips--;
//		}
//	}
}
