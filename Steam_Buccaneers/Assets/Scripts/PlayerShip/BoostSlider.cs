﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoostSlider : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{	// sets the slider value equal to the resource pool in the PlayerMove script
		this.GetComponent<Slider>().value = PlayerMove.boostCooldownTimer;
	}
}
