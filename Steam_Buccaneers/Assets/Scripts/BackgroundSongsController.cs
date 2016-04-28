using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackgroundSongsController : MonoBehaviour 
{
	public static BackgroundSongsController audControl;
	private AudioSource backgroundSource;
	private AudioSource combatSource;
	public AudioClip deathClip;
	public AudioClip[] backgroundClips;
	public AudioClip[] combatClips;

	private float counter = 0;
	private float volumeCounter = 0;
	private float resetCounter;

	bool one = false;
	bool two = false;
	bool three = false;
	bool isDead = false;
	public bool fightingBoss = false;

	// Use this for initialization
	void Start ()
	{
		audControl = this;
		one = true;
		backgroundSource = GameObject.Find("CameraChild").GetComponent<AudioSource>();
		backgroundSource.clip = backgroundClips[0];
		backgroundSource.Play();
		combatSource = GameObject.Find("CombatMusic").GetComponent<AudioSource>();
		combatSource.volume = 0;
		backgroundSource.volume = 1;
	}

	void Update()
	{
		if(GameControl.control.isFighting == false && isDead == false)
		{
			if(GameObject.Find("PlayerShip").transform.position.z < 4000)
			{
				songOne();
			}
			if(GameObject.Find("PlayerShip").transform.position.z > 4200 && GameObject.Find("PlayerShip").transform.position.z < 11350)
				songTwo();
			if(GameObject.Find("PlayerShip").transform.position.z > 12000)
				songThree();
		}
		if(GameControl.control.isFighting == true && isDead == false) //The spawning has topped, so a combat is ongoing
		{
			if(fightingBoss == false)
			{
				if (combatSource.volume < 0.99f) //The volume of the combat song is not 1 yet. 
				{
					volumeCounter += Time.deltaTime; //Increase the volume. After 1 second the volume will be 1. 
					combatSource.volume = volumeCounter; //Sets the volum
					if(volumeCounter >= 1) //The combat volume has reached 1
					{
						counter = 0; //Reset the counter
						volumeCounter = 0; //Reset the conter
					}
				}
			}
			else
			{
				if(combatSource.volume < 0.99f)
				{
					volumeCounter += Time.deltaTime * 5;
					combatSource.volume = volumeCounter;
					if(volumeCounter >= 1)
					{
						counter = 0;
						volumeCounter = 0;
					}
				}
			}
		}
		else
		{
			if(combatSource.volume > 0.01f)
			{
				if(counter < 1)
					counter += Time.deltaTime;
				if(counter >= 1)
				{
					counter += Time.smoothDeltaTime;
					combatSource.volume = 2-counter;
					if(counter >= 2)
					{
						counter = 0;
						volumeCounter = 0;
						combatSource.volume = 0;
					}
				}
			}
		}
	}

	public void playDeadSong()
	{
		isDead = true;
		backgroundSource.clip = deathClip;
		backgroundSource.volume = 1;
		backgroundSource.Play();
		backgroundSource.loop = false;
		combatSource.Stop();
		combatSource.volume = 0;
	}

	public void stopDeadSong()
	{
		isDead = false;
		backgroundSource.loop = true;
	}

	public void bossCombat()
	{
		one = false;
		two = false;
		three = false;
		fightingBoss = true;
		combatSource.clip = combatClips[3];
		combatSource.Play();
		backgroundSource.volume = 0;
		backgroundSource.Stop();
		volumeCounter = 0;
	}

	private void songOne()
	{
		if(!one)
		{
			backgroundSource.volume -= Time.deltaTime * 2;
			if(backgroundSource.volume <= 0)
			{
				backgroundSource.clip = backgroundClips[0];
				combatSource.clip = combatClips[0];
				backgroundSource.Play();
				combatSource.Play();

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
				backgroundSource.clip = backgroundClips[1];
				combatSource.clip = combatClips[1];

				backgroundSource.Play();
				combatSource.Play();

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
				backgroundSource.clip = backgroundClips[2];
				combatSource.clip = combatClips[2];
				backgroundSource.Play();
				combatSource.Play();

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
