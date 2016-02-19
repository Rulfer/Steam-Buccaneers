using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

	private Text dialogText;
	private Text characterName;

	void Start () 
	{
		//Initialize functions
		dialogText = GameObject.Find("dialogue_ingame").GetComponent<Text>();
		characterName = GameObject.Find("dialogue_name").GetComponent<Text>();
		//Turn of functuallity
		GameObject.Find("GameControl").GetComponent<gameButtons>().pause();
		Debug.Log("pause");
		//Trigger dialog here
		dialog(0);
	}

	//Might need to recieve a parameter to decide which dialog to run
	private void dialog(int stage)
	{
		//Dialog runs here
		switch(stage)
		{
		case(0):
			//Dialog here
			setDialog("shopkeeper", "Very funny dialog");
			//When dialog is finished

			break;
		case(1):
			break;
		case(2):
			break;
		case(3):
			break;
		case(4):
			break;
		case(5):
			break;
		case(6):
			break;
		default:
			break;
		}
		//Unpause game
		GameObject.Find("GameControl").GetComponent<gameButtons>().pause();

	}

	public void setDialog(string character, string text)
	{
		characterName.text = character;
		dialogText.text = text;
	}
			
}
