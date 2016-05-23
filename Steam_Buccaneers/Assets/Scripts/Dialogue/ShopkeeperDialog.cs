using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopkeeperDialog : MonoBehaviour {

	private string[] shopkeeperDialogTexts = new string[4];

	// Use this for initialization
	void Start () 
	{
		//Random shopkeeper qutoes does not happen i tutorial.
		if (!GameObject.Find ("TutorialControl"))
		{
			//Shopkeeper dialog which happens when player enters shop
			shopkeeperDialogTexts [0] = "Welcome back! What can I do you for?";
			shopkeeperDialogTexts [1] = "Good to SEA you again!";
			shopkeeperDialogTexts [2] = "Welcome to my shop! Best prices in all of known space!";
			shopkeeperDialogTexts [3] = "Welcome! Buy my stuff!";

			//Picks random line
			int temp = Random.Range (0, shopkeeperDialogTexts.Length);
			this.GetComponent<Text> ().text = shopkeeperDialogTexts [temp];
			//Turn of nextbutton because only needed in tutorial
			GameObject.Find ("dialogue_next_shop").SetActive (false);
		} 

	}
}
