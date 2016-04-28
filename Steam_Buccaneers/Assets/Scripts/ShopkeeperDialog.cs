﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopkeeperDialog : MonoBehaviour {

	private string[] shopkeeperDialogTexts = new string[4];

	// Use this for initialization
	void Start () 
	{
		if (GameObject.Find ("TutorialControl"))
		{
			shopkeeperDialogTexts [0] = "Welcome back! What can I do you for?";
			shopkeeperDialogTexts [1] = "Good to SEA you again!";
			shopkeeperDialogTexts [2] = "Welcome to my shop! Best prices in all of known space!";
			shopkeeperDialogTexts [3] = "Welcome! Buy my stuff!";
			int temp = Random.Range (0, shopkeeperDialogTexts.Length);
			this.GetComponent<Text> ().text = shopkeeperDialogTexts [temp];
		} 
		else
		{
			GameObject.Find ("dialogue_next_shop").SetActive (false);
		}
	}
}
