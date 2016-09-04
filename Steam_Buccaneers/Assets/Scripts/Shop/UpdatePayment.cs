using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdatePayment : MonoBehaviour 
{
	float sliderValue;
	public float payment;

	// Use this for initialization
	void Start () 
	{
		//Set health
		gameObject.GetComponent<Text>().text = GameControl.control.health.ToString() + "HP";
	}

	void Update () 
	{
		//set slider healthvalue
		payment = sliderValue - GameControl.control.health;
		sliderValue = GameObject.Find("Slider_refill").GetComponent<Slider>().value;
		//If slider value is bigger than playership health show how much it will cost to repair it
		if (sliderValue > GameControl.control.health)
		{
			gameObject.GetComponent<Text>().text = payment.ToString() + ",-";
		}
		else
		{
			//If the slider value is less than your current health set it too 0
			gameObject.GetComponent<Text>().text = "0,-";
		}
	}
}