using UnityEngine;
using System.Collections;

public class MusicControl : MonoBehaviour
{
	public AudioSource mainCamSource;
	private GameObject player;
	private AudioSource thisAudioSource;

	private float sourceDistance;

	void Start()
	{
		this.GetComponent<AudioSource>();
		player = GameObject.Find("PlayerShip");
	}
		
	// Update is called once per frame
	void Update () 
	{
//		sourceDistance = Vector3.Distance (this.transform.position, player.transform.position); //Distance between player and where the boss spawns
//
//		if(sourceDistance < 500)
//		{
//			thisAudioSource.volume = sourceDistance / 500;
//			mainCamSource.volume = mainCamSource.volume - thisAudioSource.volume;
//		}
	}
}
