using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AimCursorPosition : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		this.GetComponent<RawImage>().enabled = false; //Disables the aimed cursor
	}
	
	// Update is called once per frame
	void Update () {
		//Constantly update the position of the aimed cursor to be that of the main cursor. 
		//This is to prevent the player from seeing the cursor being visible and take a jump from
		//yhe previous position to the new position, and instead give them a smoother experience
		//with the cursor appearing instantly at the correct position. 
		this.transform.position = Input.mousePosition; 
	}
}
