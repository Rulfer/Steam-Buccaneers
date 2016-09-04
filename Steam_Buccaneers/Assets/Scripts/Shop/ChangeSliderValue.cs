using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeSliderValue : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		//Sets slider value to playership health
		this.GetComponent<Slider>().value = GameControl.control.health;
	}

}
