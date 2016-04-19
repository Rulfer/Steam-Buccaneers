using UnityEngine;
using System.Collections;

public class MusicControl : MonoBehaviour
{
	private AudioSource mainCamSource;
	private GameObject player;
	private AudioSource thisAudioSource;

	private float sourceDistance;

	bool startSource = false;

	void Start()
	{
		thisAudioSource = this.GetComponent<AudioSource>();
		mainCamSource = GameObject.Find("CameraChild").GetComponent<AudioSource>();
		player = GameObject.Find("PlayerShip");
	}
		
	// Update is called once per frame
	void Update () 
	{
		sourceDistance = Vector3.Distance (this.transform.position, player.transform.position); //Distance between player and where the boss spawns
		if(SpawnAI.spawn.stopSpawn == true) //A fight is ongoing, so we dont want to play the shop-song
			thisAudioSource.volume = 0;
		
		else if(sourceDistance < 500 && SpawnAI.spawn.stopSpawn == false)
		{
			if(startSource == true)
			{
				startSource = false;
				thisAudioSource.loop = true;
				thisAudioSource.Play();
			}
			thisAudioSource.volume = 1 - sourceDistance / 500;
			if(thisAudioSource.volume < 0)
				thisAudioSource.volume = 0;
			mainCamSource.volume = 1 - (thisAudioSource.volume * 1.5f);
		}
		else
		{
			thisAudioSource.loop = false;
			thisAudioSource.Stop();
			startSource = true;
		}
	}
}
