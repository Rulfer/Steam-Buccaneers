using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetCurrentHealth : MonoBehaviour 
{
	void Start () 
	{
		//Sets slider value to player health
		gameObject.GetComponent<Slider>().value = GameControl.control.health;
	}
}