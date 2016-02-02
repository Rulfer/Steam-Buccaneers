using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class setCurrentHealth : MonoBehaviour {
	
		void Start () 
		{
				gameObject.GetComponent<Slider>().value = GameControl.control.health;
			}
	}