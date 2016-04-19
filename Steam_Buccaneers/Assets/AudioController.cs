using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour 
{
	private AudioSource backgroundSource;
	private AudioSource combatSource;
	public AudioClip[] clips;

	private float counter = 0;
	private float volumeCounter = 0;
	private float resetCounter;

	bool one = false;
	bool two = false;
	bool three = false;

	// Use this for initialization
	void Start ()
	{
		one = true;
		backgroundSource = GameObject.Find("CameraChild").GetComponent<AudioSource>();
		backgroundSource.clip = clips[0];
		backgroundSource.Play();
		combatSource = GameObject.Find("CombatMusic").GetComponent<AudioSource>();
		combatSource.volume = 0;
		backgroundSource.volume = 1;
	}

	void Update()
	{
		if(GameObject.Find("PlayerShip").transform.position.z < 4000)
		{
			songOne();
		}
		if(GameObject.Find("PlayerShip").transform.position.z > 4200 && GameObject.Find("PlayerShip").transform.position.z < 11350)
			songTwo();
		if(GameObject.Find("PlayerShip").transform.position.z > 12000)
			songThree();
		if(SceneManager.GetActiveScene().name != "Tutorial")
		{
			if(SpawnAI.spawn.stopSpawn == true) //The spawning has topped, so a combat is ongoing
			{
				if (combatSource.volume < 0.99f) //The volume of the combat song is not 1 yet. 
				{
					if(!combatSource.isPlaying) //If it's not playing, start playing the combat song
						combatSource.Play();
					volumeCounter += Time.deltaTime; //Increase the volume. After 1 second the volume will be 1. 
					combatSource.volume = volumeCounter; //Sets the volum
					backgroundSource.volume = 1 - combatSource.volume; //Decrese background song based on combat song volume
					if(volumeCounter >= 1) //The combat volume has reached 1
					{
						counter = 0; //Reset the counter
						volumeCounter = 0; //Reset the conter
						backgroundSource.volume = 0; //Set the volume of background source to 0
						backgroundSource.Stop(); //Stop the background source
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
		}
	}

	private void songOne()
	{
		if(!one)
		{
			backgroundSource.volume -= Time.deltaTime * 2;
			if(backgroundSource.volume <= 0)
			{
				backgroundSource.clip = clips[0];
				backgroundSource.Play();

				one = true;
				two = false;
				three = false;
			}
		}
		else
		{
			if(backgroundSource.volume < 1)
			{
				backgroundSource.volume += Time.deltaTime * 2;
			}
			else
			{
				backgroundSource.volume = 1;
			}
		}
	}

	private void songTwo()
	{
		if(!two)
		{
			backgroundSource.volume -= Time.deltaTime * 2;
			if(backgroundSource.volume <= 0)
			{
				backgroundSource.clip = clips[1];
				backgroundSource.Play();
				two = true;
				one = false;
				three = false;
			}
		}
		else
		{
			if(backgroundSource.volume < 1)
			{
				backgroundSource.volume += Time.deltaTime * 2;
			}
			else
			{
				backgroundSource.volume = 1;
			}
		}
	}

	private void songThree()
	{
		if(!three)
		{
			backgroundSource.volume -= Time.deltaTime * 2;
			if(backgroundSource.volume <= 0)
			{
				backgroundSource.clip = clips[2];
				backgroundSource.Play();

				three = true;
				one = false;
				two = false;
			}
		}
		else
		{
			if(backgroundSource.volume < 1)
			{
				backgroundSource.volume += Time.deltaTime * 2;
			}
			else
			{
				backgroundSource.volume = 1;
			}
		}
	}
}
