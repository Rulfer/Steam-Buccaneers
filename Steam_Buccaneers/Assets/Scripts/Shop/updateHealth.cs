using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateHealth : MonoBehaviour 
{
	float sliderValue;

	// Use this for initialization
	void Start () 
	{
		gameObject.GetComponent<Text>().text = GameControl.control.health.ToString() + "HP";
	}

	void Update () 
	{

		sliderValue = GameObject.Find("Slider_refill").GetComponent<Slider>().value;
		Debug.Log(sliderValue);
		if (sliderValue > GameControl.control.health)
		{
			gameObject.GetComponent<Text>().text = sliderValue.ToString() + "HP";
		}
		else
		{
			gameObject.GetComponent<Text>().text = GameControl.control.health.ToString() + "HP";
		}
	}
}