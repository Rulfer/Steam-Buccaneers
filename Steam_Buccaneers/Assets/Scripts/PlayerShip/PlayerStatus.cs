using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour 
{
	public static int swivelAmmo;
	public static int mainAmmo;
	public static int playerMaxHealth;
	public static int playerCurrentHealth;
	public static float healthPercentageLeft;


	// Use this for initialization
	void Start () 
	{
		swivelAmmo = 500;
		playerMaxHealth = 20;
		playerCurrentHealth = 20;

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (playerCurrentHealth + "playerhealth, u no dead now");
		healthPercentageLeft = (playerCurrentHealth / playerMaxHealth)*100;

		if (healthPercentageLeft <= 100)
		{
			//Debug.Log("Health full");
			//Debug.Log(healthPercentageLeft);
			// Normal texture
		}

		if (healthPercentageLeft <= 75)
		{
			//Debug.Log("Health 75%");
			//Debug.Log(healthPercentageLeft);
			// wounded texture
		}


		if (healthPercentageLeft <= 50)
		{
			//Debug.Log("Health 50%");
			//Debug.Log(healthPercentageLeft);
			// shitty texture
		}


		if (healthPercentageLeft <= 25)
		{
			//Debug.Log("Health Critical, u gon die son");
			//Debug.Log(healthPercentageLeft);
			// jesus im coming home texture
		}

		if (playerCurrentHealth <= 0)
		{
			//Debug.Log("u ded");
		}					
	}
}
