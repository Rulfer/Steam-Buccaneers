using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

	public void starNewGame()
	{
		//Goes into tutorial. Savefile is overwritten when player enter shop in tutorial
		GameControl.control.ChangeScene ("Tutorial");
	}
}
