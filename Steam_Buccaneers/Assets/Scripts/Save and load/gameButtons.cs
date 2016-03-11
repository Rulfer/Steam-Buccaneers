using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameButtons : MonoBehaviour {
	private bool escMenuStatus = false;
	public GameObject escMenu;

	void Update()
	{
	if (Input.GetKeyDown (KeyCode.Escape))
		{
		setDifferent ();
		escMenu.SetActive(escMenuStatus);
			if (SceneManager.GetActiveScene ().name != "Tutorial")
			{
				pause ();
			}
		}
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
