using UnityEngine;
using System.Collections;

public class GoBack : MonoBehaviour {

	public void clickButton()
	{
		GameControl.control.ChangeScene ("WorldMaster");
		ChangeScene.inShop = false;
		if (GameObject.Find ("TutorialControl") != null)
		{
			Destroy (GameObject.Find ("TutorialControl").gameObject);
		}
	}
		

}
