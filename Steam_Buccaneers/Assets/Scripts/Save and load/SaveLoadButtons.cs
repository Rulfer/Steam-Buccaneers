using UnityEngine;
using System.Collections;

public class SaveLoadButtons : MonoBehaviour {

	//Makes buttons
	void OnGUI()
	{
		//If buttons is pressed this stuff happens. I am making a button and saying what will happen to it in one
		if (GUI.Button (new Rect (10, 160, 100, 30), "Save")) 
		{
			GameControl.control.Save ("null");
		}
		if (GUI.Button (new Rect (10, 200, 100, 30), "Load")) 
		{
			//Loads data from file in GameControl.cs
			GameControl.control.Load ();
		}
	}
}
