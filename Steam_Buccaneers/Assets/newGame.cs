using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class newGame : MonoBehaviour {

	public void starNewGame()
	{
		GameControl.control.ChangeScene ("Tutorial");
	}
}
