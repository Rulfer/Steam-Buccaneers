using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class spesialAmmo : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GetComponent<Text>().text = GameControl.control.spessAmmo.ToString();
	}
}
