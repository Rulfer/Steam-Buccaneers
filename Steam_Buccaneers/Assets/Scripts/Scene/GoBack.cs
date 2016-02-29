using UnityEngine;
using System.Collections;

public class GoBack : MonoBehaviour {

	public void clickButton()
	{
		GameControl.control.ChangeScene ("world");
	}
		

}
