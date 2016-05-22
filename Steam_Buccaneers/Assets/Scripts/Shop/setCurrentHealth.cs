using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetCurrentHealth : MonoBehaviour 
{
	void Start () 
	{
		gameObject.GetComponent<Slider>().value = GameControl.control.health;
	}
}