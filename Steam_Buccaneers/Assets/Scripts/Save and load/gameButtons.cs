using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameButtons : MonoBehaviour 
{
	//Bool to see if esc menu is up or closed
	private bool escMenuStatus = false;
	public GameObject escMenu;
	//Counter
	private int i = 0;

	void OnLevelWasLoaded(int level)
	{
		i = 0;
		//Adds listeners to esc menu buttons
		//Not when it is in not game scenes
		if (SceneManager.GetActiveScene ().name != "Shop" && SceneManager.GetActiveScene ().name != "main_menu" && SceneManager.GetActiveScene ().name != "cog_screen")
		{
			GameObject.Find ("resume").GetComponent<Button> ().onClick.AddListener (resume);
			GameObject.Find ("load").GetComponent<Button> ().onClick.AddListener (GameControl.control.Load);
			GameObject.Find ("exit").GetComponent<Button> ().onClick.AddListener (closeApplication);
		}
	}

	void Update()
	{
		//Ugly code, but have to do this to be able to find esc menu. If I put in start() or onlevelloaded() the referece becomes null. So checking in frame 2
		if (i == 1)
		{
			Debug.Log (SceneManager.GetActiveScene ().name);
			if (SceneManager.GetActiveScene ().name != "main_menu" && SceneManager.GetActiveScene ().name != "Shop")
			{
				if (GameObject.Find ("menu"))
				{
					escMenu = GameObject.Find ("menu");
					GameObject.Find ("menu").SetActive (false);
					Debug.Log (escMenu + "Is alive!");
				}
			}

		}
		if (i == 10)
		{
			//Removes loadingscreen 10 frames after level is loaded
			//Unity loades textures last after level is tecnically loaded.
			//Letting it go 10 frames from starts makes sure all textures are loaded
			if (GameControl.control.loadingCanvas != null)
			{
				if (GameControl.control.loadingCanvas.activeSelf == true)
				{
					GameControl.control.loadingCanvas.SetActive (false);
				}
			}
		}

		//Open escmenu
		if (Input.GetKeyDown (KeyCode.Escape) && SceneManager.GetActiveScene().name != "main_menu" && Time.timeScale != 0)
		{
			setDifferent ();
			escMenu.SetActive(escMenuStatus);
			//pause game when esc menu is open. Not in Tutorial as it will mess with a already paused game during dialog
			if (SceneManager.GetActiveScene ().name != "Tutorial")
			{
				pause ();
			}
		}
		i++;
	}

	//Basic pause function
	public void pause()
	{
		Debug.Log ("Pause game");
		if(Time.timeScale == 0)
		{
			Time.timeScale = 1;
		}
		else
		{
			Time.timeScale = 0;
		}
	}

	//Closes menu, changes bool and unpauses game
	public void resume()
	{
		escMenu.SetActive(!escMenuStatus);
		setDifferent();
		pause();
	}
	//Changes bool
	private void setDifferent()
	{
		escMenuStatus = !escMenuStatus;
	}
	//Close game
	public void closeApplication()
	{
		Application.Quit();
	}

}
