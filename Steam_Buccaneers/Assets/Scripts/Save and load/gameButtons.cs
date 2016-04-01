using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameButtons : MonoBehaviour {
	private bool escMenuStatus = false;
	public GameObject escMenu;
	private int i = 0;

	void Update()
	{
		if (i == 1)
		{
			escMenu = GameObject.Find ("menu");
			GameObject.Find ("menu").SetActive (false);
			Debug.Log (escMenu + "Is alive!");
			i++;
		}
		//Debug.Log (escMenu);
	if (Input.GetKeyDown (KeyCode.Escape))
		{
		setDifferent ();
		escMenu.SetActive(escMenuStatus);
			if (SceneManager.GetActiveScene ().name != "Tutorial")
			{
				pause ();
			}
		}
		i++;
	}

	public void pause()
	{
		if(Time.timeScale == 0)
		{
			Time.timeScale = 1;
		}
		else
		{
			Time.timeScale = 0;
		}
	}

	public void resume()
	{
		
		escMenu.SetActive(!escMenuStatus);
		setDifferent();
		pause();
	}

	private void setDifferent()
	{
		escMenuStatus = !escMenuStatus;
	}

	public void closeApplication()
	{
		Application.Quit();
	}

}
