using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour 
{
	GameObject player;
	bool part1Loaded;
	bool part2Loaded;
	bool part3Loaded;
	

	// Use this for initialization
	void Start () 
	{
		SceneManager.LoadScene("WorldMaster");
		//SceneManager.LoadScene("worldPt1", LoadSceneMode.Additive);
		//SceneManager.LoadSceneAsync("worldPt2");
		//SceneManager.LoadSceneAsync("worldPt3", LoadSceneMode.Additive);
		//SceneManager.SetActiveScene(Scene, "WorldMaster");
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		LoadScenes();
		UnloadScenes();


		//Debug.Log (
	}

	void LoadScenes ()
	{
		player = GameObject.Find ("PlayerShip");

		//Loads in scene 1
		if (player.transform.position.z <= 4800 && part1Loaded == false)
		{
			SceneManager.LoadScene("worldPt1",LoadSceneMode.Additive);
			part1Loaded = true;
		}

		//Loads in scene 2
		if (player.transform.position.z >= 3000 && part2Loaded == false && part1Loaded == true || 
			player.transform.position.z <= 12000 && part3Loaded == true && part2Loaded == false)
		{
			Debug.Log("hello, it is you im looking for");
			SceneManager.LoadScene("worldPt2", LoadSceneMode.Additive);
			part2Loaded = true;
		}

		//Loads in scene 3
		if (player.transform.position.z >= 9500 && part3Loaded == false)
		{
			SceneManager.LoadScene("worldPt3", LoadSceneMode.Additive);
			part3Loaded = true;
		}

		Debug.Log(SceneManager.sceneCount);

	}

	void UnloadScenes()
	{
		//Unloads Scene 1
		if(player.transform.position.z > 4800 && part1Loaded == true)
		{
			SceneManager.UnloadScene("worldPt1");
			part1Loaded = false;
		}
		//Unloads Scene 2
		if(player.transform.position.z < 3000 && part2Loaded == true && part1Loaded == true ||
			player.transform.position.z >12000 && part2Loaded == true && part3Loaded == true)
		{
			SceneManager.UnloadScene("worldPt2");
			part2Loaded = false;
		}
		//Unloads Scene 3
		if(player.transform.position.z < 9500 && part3Loaded == true)
		{
			SceneManager.UnloadScene("worldPt3");
			part3Loaded = false;
		}

	}
}
