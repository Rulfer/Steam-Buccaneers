using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitEndScreen : MonoBehaviour 
{
	
	// Update is called once per frame
	void Update () 
	{
		//Exit to mainmenu in cogscreen
		if (Input.GetKey(KeyCode.Space))
		{
			SceneManager.LoadScene("main_menu");
		}
	
	}
}
