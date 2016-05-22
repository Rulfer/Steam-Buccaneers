using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Monies : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GetComponent<Text>().text = GameControl.control.money.ToString();
	}
}
