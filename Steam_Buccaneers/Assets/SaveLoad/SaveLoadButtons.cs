using UnityEngine;
using System.Collections;

public class SaveLoadButtons : MonoBehaviour {

	//Makes buttons
	void OnGUI()
	{
		//If buttons is pressed this stuff happens. I am making a button and saying what will happen to it in one
		if (GUI.Button (new Rect (10, 100, 100, 30), "Save")) 
		{
			//Updates controller with current data. Here posstions to player and meteor
			GameObject goP = GameObject.FindGameObjectWithTag ("Player");
			GameControl.control.shipPos = goP.transform.position;
			GameObject goM = GameObject.FindGameObjectWithTag ("Meteor");
			GameControl.control.meteorPos = goM.transform.position;
			//Writes data to file in GameControl.cs
			GameControl.control.Save ();
		}
		if (GUI.Button (new Rect (10, 140, 100, 30), "Load")) 
		{
			//Loads data from file in GameControl.cs
			GameControl.control.Load ();
			//Update gameobjects with loaded data
			GameObject goP = GameObject.FindGameObjectWithTag ("Player");
			goP.transform.position = GameControl.control.shipPos;
			GameObject goM = GameObject.FindGameObjectWithTag ("Meteor");
			goM.transform.position = GameControl.control.meteorPos;
		}
	}
}
