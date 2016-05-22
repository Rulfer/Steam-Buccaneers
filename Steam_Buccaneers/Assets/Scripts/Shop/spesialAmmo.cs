using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpesialAmmo : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GetComponent<Text>().text = GameControl.control.specialAmmo.ToString();
	}
}
