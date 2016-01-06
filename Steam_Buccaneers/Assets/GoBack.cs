using UnityEngine;
using System.Collections;

public class GoBack : MonoBehaviour {

	private Rigidbody playerRB;

	void OnGUI()
	{
		if (GUI.Button (new Rect (10, 100, 100, 30), "Go back")) 
		{
			GameControl.control.ChangeScene ("Scene1");
		}
	}
		

}
