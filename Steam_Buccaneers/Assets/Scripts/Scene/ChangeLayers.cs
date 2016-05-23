using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeLayers : MonoBehaviour {


	
	// Update is called once per frame
	void LateUpdate () 
	{
		if (Vector3.Distance (Vector3.zero, this.transform.position) < 17 && SceneManager.GetSceneByName ("Layer0").isLoaded == false) 
		{
			SceneManager.LoadSceneAsync ("Layer0", LoadSceneMode.Additive);
		} 
		else if (Vector3.Distance (Vector3.zero, this.transform.position) > 10 && SceneManager.GetSceneByName ("Layer1").isLoaded == false)
		{
			SceneManager.LoadSceneAsync ("Layer1", LoadSceneMode.Additive);
		} 
		else if (Vector3.Distance (Vector3.zero, this.transform.position) > 17 && SceneManager.GetSceneByName ("Layer0").isLoaded == true) 
		{
			SceneManager.UnloadScene ("Layer0");
		} 
		else if (Vector3.Distance (Vector3.zero, this.transform.position) < 10 && SceneManager.GetSceneByName ("Layer1").isLoaded == true) 
		{
			SceneManager.UnloadScene ("Layer1");
		}
	}
}
