using UnityEngine;
using System.Collections;

public class GoBack : MonoBehaviour {

	public void clickButton()
	{
		GameControl.control.ChangeScene ("world");
		if (GameObject.Find ("TutorialControl") != null)
		{
			Destroy (GameObject.Find ("TutorialControl").gameObject);
		}
	}
		

}
