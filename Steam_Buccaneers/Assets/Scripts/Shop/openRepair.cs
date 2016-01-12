using UnityEngine;
using System.Collections;

public class openRepair : MonoBehaviour {

	GameObject repairMenu;

	void start()
	{
		repairMenu = GameObject.Find("Root");
	}

	public void showRepairMenu()
	{
		repairMenu.GetComponentsInChildren( typeof(Transform), true );
	}
}
