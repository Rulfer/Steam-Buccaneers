using UnityEngine;
using System.Collections;

public class OpenCloseObjectives : MonoBehaviour {

	public GameObject objectMenu;
	
	public void openCloseObjectivemenu()
	{
		objectMenu.SetActive (!objectMenu.activeSelf);
	}

}
