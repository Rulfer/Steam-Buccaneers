using UnityEngine;
using System.Collections;

public class GoBack : MonoBehaviour {

	public void clickButton()
	{
		GameControl.control.Save ("exit_store");
		GameControl.control.ChangeScene ("WorldMaster");
		ChangeScene.inShop = false;
	}
		

}
