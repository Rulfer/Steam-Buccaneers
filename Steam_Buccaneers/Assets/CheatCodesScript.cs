using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheatCodesScript : MonoBehaviour {
	private Text theText;
	private bool startTyping = false;
	private bool typed = false;
	private int wordLength = 0;
	private string[] input = new string[10];
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			startTyping = !startTyping;
		}

		if(startTyping == true)
		{
			if(Input.anyKeyDown && wordLength < 10)
			{
				wordLength++;
				input[wordLength] = Input.inputString;
			}
		}
	}
}
