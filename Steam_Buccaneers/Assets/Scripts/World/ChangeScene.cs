using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour 
{
	GameObject player; // the player object
	bool part1Loaded; // bool for testing if the part 1 of the level is loaded
	bool part2Loaded; // bool for testing if part 2 of the level is loaded
	bool part3Loaded; // bool for testing if part 3 of the level is loaded
	public static bool inShop; // checking if game is in shop or not
	

	// Use this for initialization
	void Start () 
	{
		inShop = false; // setting shop to false so we can load the parts of the level
	}
	
	// Update is called once per frame
	void Update () 
	{
		// if the current scene is not tutorial, shop, main menu or the loading screen
		if (SceneManager.GetActiveScene ().name != "Tutorial" && SceneManager.GetActiveScene ().name != "Shop" && SceneManager.GetActiveScene ().name != "main_menu" && SceneManager.GetActiveScene().name != "loading_screen")
		{
			LoadScenes();
			UnloadScenes();

		}

		else // every scene in the world is not loaded
		{
			part1Loaded = false;  
			part2Loaded = false;
			part3Loaded = false;
		}
	}

	void LoadScenes ()
	{

		if (GameObject.Find ("PlayerShip"))
		{
			player = GameObject.Find ("PlayerShip"); // finds the player object so we can measure the distance for when the scenes should be loaded

			//Loads in scene 1
			if (player.transform.position.z <= 4800 && part1Loaded == false) // if the player is close enough and the scene is not already loaded
			{
				SceneManager.LoadScene("worldPt1",LoadSceneMode.Additive); // loads the scene in addition to worldMain scene
				part1Loaded = true; // sets the scene to loaded
			}

			//Loads in scene 2 if the player is close to it, if either part one or three is loaded and it is not
			// or if the player is in the middle of the scene and it is not already loaded
			if (player.transform.position.z >= 3000 && part2Loaded == false && part1Loaded == true || 
				player.transform.position.z <= 12000 && part3Loaded == true && part2Loaded == false ||
				player.transform.position.z >= 3000 && player.transform.position.z < 12000 && part2Loaded == false)
			{
				SceneManager.LoadScene("worldPt2", LoadSceneMode.Additive); // loads the scene in addition to the worldMain scene
				part2Loaded = true; // says that the scene is now loaded 
			}

			//Loads in scene 3 if the player is close enough to it, and it is not already loaded
			if (player.transform.position.z >= 9500 && part3Loaded == false)
			{
				SceneManager.LoadScene("worldPt3", LoadSceneMode.Additive); // loads the scene in addition to the worldMain scene
				part3Loaded = true; // says that the scene is now loaded
			}
		}
	}

	void UnloadScenes()
	{
		if (player)
		{
			//Unloads Scene 1 if the player is far enough from it and it is loaded
			if (player.transform.position.z > 4800 && part1Loaded == true)
			{
				SceneManager.UnloadScene ("worldPt1");
				part1Loaded = false; // says that the scene is not loaded
			}
			//Unloads Scene 2 if the player is far enough from it and it is loaded, and eiter part one or three is also loaded
			if (player.transform.position.z < 3000 && part2Loaded == true && part1Loaded == true ||
			   player.transform.position.z > 12000 && part2Loaded == true && part3Loaded == true)
			{
				SceneManager.UnloadScene ("worldPt2");
				part2Loaded = false; // says that is is not loaded
			}
			//Unloads the third part of the world if the player is far enough from it and it is loaded
			if (player.transform.position.z < 9500 && part3Loaded == true)
			{
				SceneManager.UnloadScene ("worldPt3");
				part3Loaded = false; // says that it is not loaded
			}
		}
	}
}
