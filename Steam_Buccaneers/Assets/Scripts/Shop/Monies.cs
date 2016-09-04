using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Monies : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		//Sets gui text to player money
		GetComponent<Text>().text = GameControl.control.money.ToString();
	}
}
