﻿using UnityEngine;
using System.Collections;

public class CombatAnimationController : MonoBehaviour {

	public GameObject playerCharacterWindow;
	public GameObject enemyCharacterWindow;
	public GameObject marineAnimation;
	public GameObject bossAnimation;
	public GameObject shopKeeperAnimation;
	public GameObject playerAnimation;

	public bool combatBoss = false;


	
	// Update is called once per frame
	void Update () 
	{
		if (GameObject.Find ("SpawnsAI").GetComponent<SpawnAI> ().stopSpawn == true)
		{
			enemyCharacterWindow.SetActive (true);
			playerCharacterWindow.SetActive (true);
		} 
		else if (enemyCharacterWindow.activeSelf == true)
		{
			//After battle remove stuff
			enemyCharacterWindow.SetActive (false);
			playerCharacterWindow.SetActive (false);

			if (combatBoss == true)
			{
				combatBoss = false;
			}
		}

	}
	public void setHappy(string character)
	{
		if (character == "Player")
		{
			if (playerAnimation.GetComponent<Animator> ().GetBool ("isAngryMainCharacter") == true)
			{
				playerAnimation.GetComponent<Animator> ().SetBool ("isAngryMainCharacter", false);
			}
			playerAnimation.GetComponent<Animator> ().SetBool ("isHappyMainCharacter", true);
		} 
		else if (character == "Marine")
		{
			if (marineAnimation.GetComponent<Animator> ().GetBool ("isAngryMarine") == true)
			{
				marineAnimation.GetComponent<Animator> ().SetBool ("isAngryMarine", false);
			}
			marineAnimation.GetComponent<Animator> ().SetBool ("isHappyMarine", true);
		}
		else if (character == "Boss")
		{
			if (bossAnimation.GetComponent<Animator> ().GetBool ("isAngryBoss") == true)
			{
				bossAnimation.GetComponent<Animator> ().SetBool ("isAngryBoss", false);
			}
			bossAnimation.GetComponent<Animator> ().SetBool ("isHappyBoss", true);
		}

	}

	public void setAngry(string character)
	{
		if (character == "Player")
		{
			if (playerAnimation.GetComponent<Animator> ().GetBool ("isHappyMainCharacter") == true)
			{
				playerAnimation.GetComponent<Animator> ().SetBool ("isHappyMainCharacter", false);
			}
			playerAnimation.GetComponent<Animator> ().SetBool ("isAngryMainCharacter", true);
		} 
		else if (character == "Enemy")
		{
			if (combatBoss == false)
			{
				if (marineAnimation.GetComponent<Animator> ().GetBool ("isHappyMarine") == true)
				{
					marineAnimation.GetComponent<Animator> ().SetBool ("isHappyMarine", false);
				}
				marineAnimation.GetComponent<Animator> ().SetBool ("isAngryMarine", true);
			} 
			else
			{
				if (bossAnimation.GetComponent<Animator> ().GetBool ("isHappyBoss") == true)
				{
					bossAnimation.GetComponent<Animator> ().SetBool ("isHappyBoss", false);
				}
				bossAnimation.GetComponent<Animator> ().SetBool ("isAngryBoss", true);
			}
		
		}
	}
}
		