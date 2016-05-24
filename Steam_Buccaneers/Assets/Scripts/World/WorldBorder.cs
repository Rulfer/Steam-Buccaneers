using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldBorder : MonoBehaviour 
{
	private bool isTrespassing = false; //True if the player is inside the world border
	private float killTimer = 0; //Player is dead when this number is reached
	private float killDuration = 20; //Timer ticks down from this value
	public Text theText; //The displayed text
	public Text numberText; //The killTimer displayed as text

	// Update is called once per frame
	void Update () {
		if(isTrespassing == true) //The player is inside the world border
		{
			killDuration -= Time.deltaTime; //Decrease remaining time player has to leave world border
			if(killTimer > killDuration) //Player has stayed outside for too long
			{
				GameControl.control.health = -1; //kill the player
			}
			theText.text = "Return to the playable area!" + "\r\n"; //Display text
			theText.text += "You will die in         " + "seconds."; //Display text
			numberText.text = (Mathf.Round(killDuration * 100f) / 100f).ToString(); //Display remaining time before player is killed
		}
	}

	void OnTriggerEnter(Collider other) //Something hit the world border
	{
		if(other.transform.root.name == "PlayerShip") //It was the player
		{
			isTrespassing = true; //Activate timer
			if(GameObject.Find("SpawnsAI")) //This is not the Tutorial Scene
				SpawnAI.spawn.trespassingWorldBorder = true; //Stop the spawning of enemies

		}
	}

	void OnTriggerExit(Collider other) //Something left the border
	{
		if(other.transform.root.name == "PlayerShip") //It was the player
		{
			if(GameObject.Find("SpawnsAI")) //This is not the Tutorial Scene
				SpawnAI.spawn.trespassingWorldBorder = false; //Enemies can spawn again

			killDuration = 20; //Reset killtiemr
			theText.text = ""; //Clean textfield
			numberText.text = ""; //Clean textfield
			isTrespassing = false; //Player no longer trespassing
		}
	}
}
