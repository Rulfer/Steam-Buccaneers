using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CombatAnimationController : MonoBehaviour {

	public GameObject playerCharacterWindow;
	public GameObject enemyCharacterWindow;
	public GameObject marineAnimation;
	public GameObject bossAnimation;
	public GameObject shopKeeperAnimation;
	public GameObject playerAnimation;

	public bool combatBoss = false;


	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (SceneManager.GetActiveScene ().name != "Tutorial" && GameObject.Find("TutorialControl") == null)
		{
			if (GameControl.control.isFighting == true)
			{
				if (GameObject.Find ("Boss(Clone)"))
				{
					combatBoss = true;
				}

				if (combatBoss == true)
				{
					bossAnimation.SetActive (true);
					marineAnimation.SetActive (false);
				} 
				else
				{
					bossAnimation.SetActive (false);
					marineAnimation.SetActive (true);
				}

				enemyCharacterWindow.SetActive (true);
				playerCharacterWindow.SetActive (true);
				shopKeeperAnimation.SetActive (false);
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

	}
	public void setHappy(string character)
	{
		if (character == "Player")
		{
			if (playerAnimation.activeInHierarchy)
			{
				if (playerAnimation.GetComponent<Animator> ().GetBool ("isAngryMainCharacter") == true)
				{
					playerAnimation.GetComponent<Animator> ().SetBool ("isAngryMainCharacter", false);
				}
				playerAnimation.GetComponent<Animator> ().SetBool ("isHappyMainCharacter", true);
			}
		} 
		else if (character == "Enemy")
		{
			if (combatBoss == false)
			{
				if (marineAnimation.activeInHierarchy)
				{
					if (marineAnimation.GetComponent<Animator> ().GetBool ("isAngryMarine") == true)
					{
						marineAnimation.GetComponent<Animator> ().SetBool ("isAngryMarine", false);
					}
					marineAnimation.GetComponent<Animator> ().SetBool ("isHappyMarine", true);
				}
			} 
			else
			{
				if (bossAnimation.activeInHierarchy)
				{
					if (bossAnimation.GetComponent<Animator> ().GetBool ("isAngryBoss") == true)
					{
						bossAnimation.GetComponent<Animator> ().SetBool ("isAngryBoss", false);
					}
					bossAnimation.GetComponent<Animator> ().SetBool ("isHappyBoss", true);
				}
			}

		}

	}

	public void setAngry(string character)
	{
		if (character == "Player")
		{
			if (playerAnimation.activeInHierarchy)
			{
				if (playerAnimation.GetComponent<Animator> ().GetBool ("isHappyMainCharacter") == true)
				{
					playerAnimation.GetComponent<Animator> ().SetBool ("isHappyMainCharacter", false);
				}
				playerAnimation.GetComponent<Animator> ().SetBool ("isAngryMainCharacter", true);
			}
		} 
		else if (character == "Enemy")
		{
			if (combatBoss == false)
			{
				if (marineAnimation.activeInHierarchy)
				{
					if (marineAnimation.GetComponent<Animator> ().GetBool ("isHappyMarine") == true)
					{
						marineAnimation.GetComponent<Animator> ().SetBool ("isHappyMarine", false);
					}
					marineAnimation.GetComponent<Animator> ().SetBool ("isAngryMarine", true);
				}
			} 
			else
			{
				if (bossAnimation.activeInHierarchy)
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
}
		