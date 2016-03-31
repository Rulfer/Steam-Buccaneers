﻿using UnityEngine;
using System.Collections;

public class changeMaterial : MonoBehaviour {

	public Material[] playerMat = new Material[3];

	private float material2Limit;
	private float material3Limit;
	private float fullHealth;

	// Use this for initialization
	void Start () 
	{
		//Calculates moments of material change
		fullHealth = 100;
		material2Limit = fullHealth * 0.66f;
		material3Limit = fullHealth * 0.33f;
		//Changes material after player health. This happens when game starts or when player goes out of shop.
		checkPlayerHealth ();
	}

	public void checkPlayerHealth()
	{
		//Checks which material playership should have.
		if (GameControl.control.health > material2Limit && GameControl.control.health > material3Limit)
		{
			setNewMaterial (0);
		} 
		else if (GameControl.control.health < material2Limit && GameControl.control.health > material3Limit)
		{
			setNewMaterial (1);
		} 
		else if (GameControl.control.health < material2Limit && GameControl.control.health < material3Limit)
		{
			setNewMaterial (2);
		}
	}

	private void setNewMaterial(int matNr)
	{
		//Changes material that fits health.
		if (GetComponent<Renderer> ().material != playerMat [matNr])
		{
			GetComponent<Renderer> ().material = new Material (playerMat [matNr]);
		}
	}

}