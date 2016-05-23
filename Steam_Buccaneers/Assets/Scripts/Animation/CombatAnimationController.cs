using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CombatAnimationController : MonoBehaviour {
	//This script holds control over combatanimations. Which animation is going to be shown, when it is going to be shown and if it is goign to be shown.
	public GameObject playerCharacterWindow;
	public GameObject enemyCharacterWindow;

	public GameObject marineAnimation;
	public GameObject bossAnimation;
	public GameObject shopKeeperAnimation;
	public GameObject playerAnimation;

	public bool combatBoss = false;


	
	// Update is called once per frame
	//Using fixedupdate so Time.timescale = 0 doesnt stop animation 
	void FixedUpdate () 
	{
		//Check if it is tutorial before playing combat animaitons
		if (SceneManager.GetActiveScene ().name != "Tutorial" && GameObject.Find("TutorialControl") == null)
		{
			//Animation appear in combat
			if (GameControl.control.isFighting == true)
			{
				//If boss exist it is bossbattle
				if (GameObject.Find ("Boss(Clone)"))
				{
					combatBoss = true;
				}

				//Starting boss or marine animation
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

				//Starting player animation. Removing shopkeeperanimation just in case it was used last time.
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
	//Use this funksjon for starting happyanimation
	public void setHappy(string character)
	{
		//Checking which character animation that is going to be played.
		if (character == "Player")
		{
			//Checking that the animation is activ before we try to chenge the bool in the animator
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
			//Checking if it is bossbattle
			if (combatBoss == false)
			{
				//Checking that the animation is activ before we try to chenge the bool in the animator
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
					//Checking that the animation is activ before we try to chenge the bool in the animator
					if (bossAnimation.GetComponent<Animator> ().GetBool ("isAngryBoss") == true)
					{
						bossAnimation.GetComponent<Animator> ().SetBool ("isAngryBoss", false);
					}
					bossAnimation.GetComponent<Animator> ().SetBool ("isHappyBoss", true);
				}
			}

		}

	}
	//Use this funksjon for starting angryanimation
	public void setAngry(string character)
	{
		//Checking which character animation that is going to be played.
		if (character == "Player")
		{
			//Checking that the animation is activ before we try to chenge the bool in the animator
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
				//Checking that the animation is activ before we try to chenge the bool in the animator
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
					//Checking that the animation is activ before we try to chenge the bool in the animator
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
		