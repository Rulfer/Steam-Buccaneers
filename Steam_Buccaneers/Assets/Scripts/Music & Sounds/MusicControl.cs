using UnityEngine;
using System.Collections;

public class MusicControl : MonoBehaviour //Used by shops
{
	private AudioSource mainCamSource; //Reference to the main camera's audiosource
	private GameObject player; //Reference to the player
	private AudioSource thisAudioSource; //Reference to the audiosource of this object

	private float sourceDistance; //Distance between this shop and the player

	bool startSource = false; //Start the music

	void Start()
	{
		thisAudioSource = this.GetComponent<AudioSource>();
		mainCamSource = GameObject.Find("CameraChild").GetComponent<AudioSource>();
		player = GameObject.Find("PlayerShip");
	}
		
	// Update is called once per frame
	void Update () 
	{
		sourceDistance = Vector3.Distance (this.transform.position, player.transform.position); //Distance between player and this shop
		if(GameControl.control.isFighting == true) //A fight is ongoing, so we dont want to play the shop-song
			thisAudioSource.volume = 0; //Sets the volume to 0
		
		else if(sourceDistance < 500 && GameControl.control.isFighting == false) //Player is close enough and a fight is not ongoing
		{
			if(startSource == true) //Song not playing, start it
			{
				startSource = false; //Song has started
				thisAudioSource.loop = true; //Loop the song
				thisAudioSource.Play(); //Play the song
			}
			thisAudioSource.volume = 1 - sourceDistance / 500; //The volume of the song is based on the distance between the player and the shop
			if(thisAudioSource.volume < 0) //The player is basically 500 meters away
				thisAudioSource.volume = 0; //Set the volume to 0
			//Reduce the background song faster than the shop song is increasing in volume.
			//This is to be nicer to the players ears and not play two tracks at the same time at the same volume
			mainCamSource.volume = 1 - (thisAudioSource.volume * 1.5f); 
		}
		else //Either in a fight or too far away
		{
			thisAudioSource.loop = false; //Dont loop the song
			thisAudioSource.Stop(); //Stop the song
			startSource = true; //Needs to restart the song
		}
	}
}
