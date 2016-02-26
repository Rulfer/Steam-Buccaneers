﻿using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour 
{
	float temp = 100000;
	int tempI;
	private float distance;
	public GameObject[] shops;
	private GameObject player;
	public GameObject deathScreen;
	private bool showDeathScreen = false;
	//float respawnTime = 5;


	void Start () 
	{
		shops = GameObject.FindGameObjectsWithTag ("shop"); 
		player = GameObject.Find("PlayerShip");
		deathScreen.SetActive (showDeathScreen);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (GameControl.control.health);
		GameControl.control.health --;
		if (GameControl.control.health <= 0)
		{
			showDeathScreen = true;
			//Debug.Log("You are dead, press space to respawn at closest shop");
			deathScreen.SetActive (showDeathScreen);
			if (Input.GetKey(KeyCode.Space))
			{
				RespawnPlayer();
				showDeathScreen = false;
				deathScreen.SetActive (showDeathScreen);
			}
			/*
			respawnTime -= Time.deltaTime;
			if (respawnTime < 0)
			{
				RespawnPlayer();
				respawnTime = 5;
			}*/
		}

	
	}

	void RespawnPlayer()
	{

		for (int i = 0; i < 4; i++)
		{
			distance = Vector3.Distance(shops[i].transform.position, player.transform.position);
			if (distance < temp)
			{
				temp = distance;
				tempI = i;
				//Debug.Log (spawnCoord);
				//player.transform.position = spawnCoord;
				//player.transform.position.z -= 100;
			}

		}
		Vector3 spawnCoord = new Vector3 (shops[tempI].transform.position.x,shops[tempI].transform.position.y,shops[tempI].transform.position.z - 100);
		player.transform.position = spawnCoord;
		GameControl.control.money -= (GameControl.control.money*10)/100;
		GameControl.control.health = 20;
		//Debug.Log (tempI);
		//Debug.Log (shops[tempI].transform.name);
	
		
	}

}