using UnityEngine;
using System.Collections;

public class gameButtons : MonoBehaviour {
	private bool escMenuStatus = false;
	public GameObject escMenu;

	void Update()
	{
	if (Input.GetKeyDown (KeyCode.Escape))
		{
		setDifferent();
		escMenu.SetActive(escMenuStatus);
		pause();
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

		Debug.Log (Time.timeScale);
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
