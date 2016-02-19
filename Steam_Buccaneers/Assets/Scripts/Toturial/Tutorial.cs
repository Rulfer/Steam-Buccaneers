using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

	private Text dialogText;
	private Text characterName;
	public static int dialogNumber;
	public GameObject[] shootyThings;

	void Start ()
	{
		//Initialize functions
		dialogText = GameObject.Find ("dialogue_ingame").GetComponent<Text> ();
		characterName = GameObject.Find ("dialogue_name").GetComponent<Text> ();
		//Turn of functuallity
		GameObject.Find ("GameControl").GetComponent<gameButtons> ().pause ();
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
			setDialog ("shopkeeper", "Instructions. Click button to continue");
			GameObject.Find ("GameControl").GetComponent<gameButtons> ().pause ();
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
