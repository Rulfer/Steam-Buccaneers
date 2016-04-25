using UnityEngine;
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
	{
		this.GetComponent<Slider>().value = PlayerMove2.boostCooldownTimer;
	}
}
