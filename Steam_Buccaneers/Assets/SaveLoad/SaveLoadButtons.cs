using UnityEngine;
using System.Collections;

public class SaveLoadButtons : MonoBehaviour {


	void OnGUI()
	{
		if (GUI.Button (new Rect (10, 100, 100, 30), "Save")) 
		{
			GameObject goP = GameObject.FindGameObjectWithTag ("Player");
			GameControl.control.shipPos = goP.transform.position;
			GameObject goM = GameObject.FindGameObjectWithTag ("Meteor");
			GameControl.control.meteorPos = goM.transform.position;
			GameControl.control.Save ();
		}
		if (GUI.Button (new Rect (10, 140, 100, 30), "Load")) 
		{
			GameControl.control.Load ();
			GameObject goP = GameObject.FindGameObjectWithTag ("Player");
			goP.transform.position = GameControl.control.shipPos;
			GameObject goM = GameObject.FindGameObjectWithTag ("Meteor");
			goM.transform.position = GameControl.control.meteorPos;
		}
	}
}
