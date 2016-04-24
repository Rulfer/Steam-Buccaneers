using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour 
{
	GameObject player;
	bool part1Loaded;
	bool part2Loaded;
	bool part3Loaded;
	public static bool inShop;
	

	// Use this for initialization
	void Start () 
	{
		inShop = false;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (SceneManager.GetActiveScene ().name != "Tutorial" && SceneManager.GetActiveScene ().name != "Shop" && SceneManager.GetActiveScene ().name != "main_menu" && SceneManager.GetActiveScene().name != "loading_screen")
		{

			if (inShop != true)
			{
				//Debug.Log ("do i do anything2?");
				LoadScenes ();
				UnloadScenes ();
			} 
			else
			{
				part1Loaded = false;  
				part2Loaded = false;
				part3Loaded = false;
			}
		}

		//Debug.Log (
	}

	void LoadScenes ()
	{

		if (GameObject.Find ("PlayerShip"))
		{
			//Debug.Log ("do i do anything3?");

			player = GameObject.Find ("PlayerShip");

			//Loads in scene 1
			if (player.transform.position.z <= 4800 && part1Loaded == false)
			{
				SceneManager.LoadScene("worldPt1",LoadSceneMode.Additive);
				part1Loaded = true;
			}

			//Loads in scene 2
			if (player.transform.position.z >= 3000 && part2Loaded == false && part1Loaded == true || 
				player.transform.position.z <= 12000 && part3Loaded == true && part2Loaded == false ||
				player.transform.position.z >= 3000 && player.transform.position.z < 12000 && part2Loaded == false)
			{

				//Debug.Log("hello, it is you im looking for");
				SceneManager.LoadScene("worldPt2", LoadSceneMode.Additive);
				part2Loaded = true;
			}

			//Loads in scene 3, this is commented out for our game test
			if (player.transform.position.z >= 9500 && part3Loaded == false)
			{
				SceneManager.LoadScene("worldPt3", LoadSceneMode.Additive);
				part3Loaded = true;
			}

			//Debug.Log(SceneManager.sceneCount);
		}
	}

	void UnloadScenes()
	{
		if (player)
		{
			//Unloads Scene 1
			if (player.transform.position.z > 4800 && part1Loaded == true)
			{
				SceneManager.UnloadScene ("worldPt1");
				part1Loaded = false;
			}
			//Unloads Scene 2
			if (player.transform.position.z < 3000 && part2Loaded == true && part1Loaded == true ||
			   player.transform.position.z > 12000 && part2Loaded == true && part3Loaded == true)
			{
				SceneManager.UnloadScene ("worldPt2");
				part2Loaded = false;
			}
			//Unloads Scene 3, this is commented out for our game test
			if (player.transform.position.z < 9500 && part3Loaded == true)
			{
				SceneManager.UnloadScene ("worldPt3");
				part3Loaded = false;
			}
		}
	}
}
