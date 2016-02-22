using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

	private Text dialogText;
	private Text characterName;
	public static int dialogNumber;
	public GameObject[] shootyThings;
	private gameButtons buttonEvents;

	void Start ()
	{
		//Initialize functions
		dialogText = GameObject.Find ("dialogue_ingame").GetComponent<Text> ();
		characterName = GameObject.Find ("dialogue_name").GetComponent<Text> ();
		//Turn of functuallity
		buttonEvents = GameObject.Find ("GameControl").GetComponent<gameButtons> ();
		buttonEvents.pause ();
		shootingAllowed (false);
		Debug.Log ("pause");
		//Trigger dialog here
		dialog (0);
	}

	//Might need to recieve a parameter to decide which dialog to run
	public void dialog (int stage)
	{
		//Dialog runs here
		switch (stage) {
		case(0):
			//Dialog here
			setDialog ("shopkeeper", "Very funny dialog");
			break;
		case(1):
			setDialog ("shopkeeper", "Steer ship with WASD. Click button to continue");
			buttonEvents.pause ();
			break;
		case(2):
			setDialog ("ShopKeeper", "GG guy");
			buttonEvents.pause ();
			break;
		case(3):
			setDialog ("ShopKeeper", "Now shoot with Q and E and spess weapon mouse 1. Click button to continue");
			shootingAllowed (true);
			buttonEvents.pause ();
			break;
		case(4):
			setDialog ("ShopKeeper", "Good stuff.");
			buttonEvents.pause ();
			break;
		case(5):
			setDialog ("ShopKeeper", "Look a douchbag!");
			buttonEvents.pause ();
			break;
		case(6):
			break;
		default:
			break;
		}

	}

	public void nextDialog ()
	{
		dialogNumber++;
		dialog (dialogNumber);
	}

	public void setDialog (string character, string text)
	{
		characterName.text = character;
		dialogText.text = text;
	}

	private void shootingAllowed (bool status)
	{
			for (int i = 0; i < shootyThings.Length; i++) 
			{
				shootyThings [i].SetActive (status);
			}
	}
			
}
