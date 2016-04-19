using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour 
{
	private AudioSource backgroundSource;
	private AudioSource combatSource;

	private float counter = 0;
	private float volumeCounter = 0;
	private float resetCounter;

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
				if(counter < 1)
					counter += Time.deltaTime;
				if(counter >= 1)
				{
					if(!backgroundSource.isPlaying)
					{
						resetCounter = Time.time;
						volumeCounter = 0;
						backgroundSource.Play();
					}
					counter += Time.smoothDeltaTime;

					combatSource.volume = 2-counter;
					backgroundSource.volume = 1 - backgroundSource.volume;
					if(counter >= 2)
					{
						counter = 0;
						volumeCounter = 0;
						combatSource.volume = 0;
						combatSource.Stop();
					}
				}
			}
		}

		Debug.Log("combatSource.volume = " + combatSource.volume);

	}
}
