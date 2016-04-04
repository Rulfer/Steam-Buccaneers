using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class updatePayment : MonoBehaviour 
{
	float sliderValue;
	public float payment;

	// Use this for initialization
	void Start () 
	{
		gameObject.GetComponent<Text>().text = GameControl.control.health.ToString() + "HP";
	}

	void Update () 
	{
		payment = sliderValue - GameControl.control.health;
		sliderValue = GameObject.Find("Slider_refill").GetComponent<Slider>().value;
		if (sliderValue > GameControl.control.health)
		{
			gameObject.GetComponent<Text>().text = payment.ToString() + ",-";
		}
		else
		{
			gameObject.GetComponent<Text>().text = "0,-";
		}
	}
}