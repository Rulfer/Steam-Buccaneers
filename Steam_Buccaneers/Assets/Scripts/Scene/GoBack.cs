using UnityEngine;
using System.Collections;

public class GoBack : MonoBehaviour {

	//Function for exitbutton in shop
	public void clickButton()
	{
		//Saves game first
		GameControl.control.Save ("exit_store");
		//Then change scene
		GameControl.control.ChangeScene ("WorldMaster");
		//Telling Changescene that it can start loading world parts
		ChangeScene.inShop = false;
		//Telling GameControl that player is not fighting anything, because it just left shop
		GameControl.control.isFighting = false;
	}
		

}
