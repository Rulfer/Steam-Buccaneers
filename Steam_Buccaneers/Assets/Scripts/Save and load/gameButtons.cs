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
		}
	}

	public void resume()
	{
		escMenu.SetActive(!escMenuStatus);
		setDifferent();
	}

	private void setDifferent()
	{
		escMenuStatus = !escMenuStatus;
	}

}
