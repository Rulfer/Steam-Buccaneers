using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopkeeperDialog : MonoBehaviour {

	private string[] shopkeeperDialogTexts = new string[3];

	// Use this for initialization
	void Start () 
	{
		if (GameObject.Find("TutorialControl").activeInHierarchy == false)
		{
			shopkeeperDialogTexts [0] = "Welcome back! What can I do you for?";
			shopkeeperDialogTexts [1] = "Good to SEA you again!";
			shopkeeperDialogTexts [2] = "Shopkeeper shop! Best prices in all of known space!";
	
			int temp = Random.Range (0, shopkeeperDialogTexts.Length);

			this.GetComponent<Text> ().text = shopkeeperDialogTexts [temp];
		}
	}
}
