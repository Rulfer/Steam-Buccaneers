using UnityEngine;
using System.Collections;

public class shopButtons : MonoBehaviour {

	public GameObject repairMenu;

	public void closeRepair () 
	{
		repairMenu.SetActive(false);
	}

	public void openRepair()
	{
		repairMenu.SetActive(true);
	}
}
