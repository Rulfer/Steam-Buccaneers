using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class changeSliderValue : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		this.GetComponent<Slider>().value = GameControl.control.health;
	}

}
