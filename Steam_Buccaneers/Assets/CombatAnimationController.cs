using UnityEngine;
using System.Collections;

public class CombatAnimationController : MonoBehaviour {

	public GameObject characterWindow;
	public GameObject marineAnimation;
	public GameObject bossAnimation;
	public GameObject shopKeeperAnimation;
	public GameObject playerAnimation;

	public bool playerGotHit = false;
	public bool playerHitEnemy = false;

	public bool combatBoss = false;

	private string angry = "isAngry";
	private string happy = "isHappy";

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GameObject.Find ("SpawnsAI").GetComponent<SpawnAI> ().stopSpawn == true)
		{
			characterWindow.SetActive (true);
		} 
		else if (characterWindow.activeSelf == true)
		{
			characterWindow.SetActive (false);
		}
			

		if (playerGotHit == true)
		{
			playCharacterWindowAnimations (angry, happy);
		}

		if (playerHitEnemy == true)
		{
			playCharacterWindowAnimations (happy, angry);
		}

	}
	//WantedBool er player sitt humør, mens otherBool er motstander sitt humør
	void playCharacterWindowAnimations(string wantedBool, string otherBool)
	{
		if (playerAnimation.GetComponent<Animator> ().GetBool (otherBool+"MainCharacter") == true)
		{
			playerAnimation.GetComponent<Animator> ().SetBool (otherBool+"MainCharacter", false);
		}
		playerAnimation.GetComponent<Animator> ().SetBool (wantedBool+"MainCharacter", true);

		if (combatBoss == true)
		{
			if (marineAnimation.GetComponent<Animator> ().GetBool (wantedBool+"Marine") == true)
			{
				marineAnimation.GetComponent<Animator> ().SetBool (wantedBool+"Marine", false);
			}
			bossAnimation.GetComponent<Animator> ().SetBool (otherBool+"Boss", true);
		} 
		else
		{
			if (marineAnimation.GetComponent<Animator> ().GetBool (wantedBool+"Marine") == true)
			{
				marineAnimation.GetComponent<Animator> ().SetBool (wantedBool+"Marine", false);
			}
			marineAnimation.GetComponent<Animator> ().SetBool (otherBool+"Marine", true);
		}
	}
}
