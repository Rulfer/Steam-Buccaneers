﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenCloseObjectives : MonoBehaviour {

	public GameObject objectMenu;
	public Sprite pluss;
	public Sprite minus;
	
	public void openCloseObjectivemenu()
	{
		//For toggle button next to compass. Open and closes objectivemenu
		objectMenu.SetActive (!objectMenu.activeSelf);
		if (objectMenu.activeSelf == false)
		{
			this.gameObject.GetComponent<Image> ().sprite = pluss;
		}
		else 
		{
			this.gameObject.GetComponent<Image>().sprite = minus;
		}
	}

}
