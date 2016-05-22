using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

	public void starNewGame()
	{
		GameControl.control.ChangeScene ("Tutorial");
	}
}
