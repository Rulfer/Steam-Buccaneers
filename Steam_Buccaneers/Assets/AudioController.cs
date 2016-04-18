using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour 
{
	public AudioClip[] clips;

	private AudioSource backgroundSource;
	private AudioSource combatSource;

	private float counter = 0;
	private float volumeCounter = 0;

	// Use this for initialization
	void Start ()
	{
		backgroundSource = GameObject.Find("CameraChild").GetComponent<AudioSource>();
		combatSource = GameObject.Find("CombatMusic").GetComponent<AudioSource>();
		combatSource.volume = 0;
		backgroundSource.volume = 1;
	}

	void Update()
	{
		if(SpawnAI.spawn.stopSpawn == true)
		{
			if (combatSource.volume < 0.99f)
			{
				if(!combatSource.isPlaying)
					combatSource.Play();
				volumeCounter += Time.deltaTime;
				combatSource.volume = volumeCounter;
				backgroundSource.volume = 1 - combatSource.volume;
				if(volumeCounter >= 0.99f)
				{
					counter = 0;
					volumeCounter = 0;
					backgroundSource.volume = 0;
					backgroundSource.Stop();
				}
			}
		}
		else
		{
			if(combatSource.isPlaying)
			{
				counter += Time.deltaTime;
				if(counter >= 1)
				{
					if(!backgroundSource.isPlaying)
						backgroundSource.Play();
					volumeCounter += Time.deltaTime;
					backgroundSource.volume = volumeCounter;
					combatSource.volume = 1 - backgroundSource.volume;
					if(volumeCounter >= 0.99f)
					{
						counter = 0;
						volumeCounter = 0;
						combatSource.volume = 0;
						combatSource.Stop();
					}
				}
			}
		}
	}
}
