using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackgroundSongsController : MonoBehaviour 
{
	public static BackgroundSongsController audControl;
	private AudioSource backgroundSource; //Audio source for the background music
	private AudioSource combatSource; //Audio source for the combat music
	public AudioClip deathClip; //Plays when the player dies
	public AudioClip[] backgroundClips; //Array holding all backgound tracks
	public AudioClip[] combatClips; //Array holding all combat tracks

	private float counter = 0; //Cooldown before combat music fades out
	private float volumeCounter = 0; //Volume of the combat song

	bool one = false; //Playing the first track
	bool two = false; //Playing the second track
	bool three = false; //Playing the third track
	bool isDead = false; //Checks if the player is dead
	public bool fightingBoss = false; //Checks if the player is fighting the boss

	// Use this for initialization
	void Start ()
	{
		audControl = this;
		backgroundSource = GameObject.Find("CameraChild").GetComponent<AudioSource>();
		combatSource = GameObject.Find("CombatMusic").GetComponent<AudioSource>();
		combatSource.volume = 0;
	}

	void Update()
	{
		if(GameControl.control.isFighting == false && isDead == false) //Player is not fighting and isn't dead
		{
			if(GameObject.Find("PlayerShip").transform.position.z < 4000) //Should play the first track
				songOne(); //Start the first track
			
			if(GameObject.Find("PlayerShip").transform.position.z > 4200 && GameObject.Find("PlayerShip").transform.position.z < 11350) //Should play the second track
				songTwo(); //Start the second track
			
			if(GameObject.Find("PlayerShip").transform.position.z > 12000) //Should play the third track
				songThree(); //Start the third track
		}
		if(GameControl.control.isFighting == true && isDead == false) //The spawning has topped, so a combat is ongoing
		{
			if(fightingBoss == false) //Is fighting a marine
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
			else //Is fighting the boss
			{
				if(combatSource.volume < 0.99f) //Increase the volume until it's 1
				{
					volumeCounter += Time.deltaTime * 5; //Fast increase in volume
					combatSource.volume = volumeCounter; //Sets the volume
					if(volumeCounter >= 1) //Volume has reached 1
					{
						counter = 0; //Reset the counter
						volumeCounter = 0; //Reset the counter
					}
				}
			}
		}
		else //The player is not fighting, so reduve the combat song volume
		{
			if(combatSource.volume > 0.01f) //Volume still to high
			{
				if(counter < 1) //Has not waited one second yet
					counter += Time.deltaTime; //Has waited some more
				if(counter >= 1) //There has been 1 second since the last battle
				{
					counter += Time.smoothDeltaTime; //Increase timer to reduce volume
					combatSource.volume = 2-counter; //Sets the volume
					if(counter >= 2) //Volume is 0
					{
						counter = 0; //Reset the counter
						volumeCounter = 0; //Reset the counter
						combatSource.volume = 0; //Sets volume to 0
					}
				}
			}
		}
	}

	public void playDeadSong() //Player is dead, play correct song
	{
		isDead = true; //Sets the variable to trye
		backgroundSource.clip = deathClip; //Play the dead song
		backgroundSource.volume = 1; //Set the volume
		backgroundSource.Play(); //Play the clip
		backgroundSource.loop = false; //Don't loop the song
		combatSource.Stop(); //Stop the combat song
		combatSource.volume = 0; //Set the volume of the combat song to 0
		one = false; //Not playing track 1 anymore
		two = false; //Not playing track 2 anymore
		three = false; //Not playing track 3 anymore
	}

	public void stopDeadSong() //Player has respawned
	{
		isDead = false; //No longer dead 
		backgroundSource.loop = true; //Loop the background song
	}

	public void bossCombat() //Fighting the boss
	{
		one = false; //Not playing track 1 anymore
		two = false; //Not playing track 2 anymore
		three = false; //Not playing track 3 anymore
		fightingBoss = true; //Fighting the boss
		combatSource.volume = 1;
		combatSource.clip = combatClips[3]; //Play the unique boss combat song
		combatSource.Play(); //Start the song
		backgroundSource.volume = 0; //Remove background audio
		backgroundSource.Stop(); //Stop the background song
		volumeCounter = 0; //Reset the counter
	}

	private void songOne() //Play the first song
	{
		if(!one) //Song not started playing yet
		{
			backgroundSource.volume -= Time.deltaTime * 2; //Fade out the current playing track
			if(backgroundSource.volume <= 0) //Current playing track is muted
			{
				backgroundSource.clip = backgroundClips[0]; //Change the track to be this song
				combatSource.clip = combatClips[0]; //Change to the correct combat song
				backgroundSource.Play(); //Start the background song
				combatSource.Play(); //Start the combat song

				one = true; //Playing the first track
				two = false; //Not playing the second
				three = false; //Not playing the this
			}
		}
		else //The first track is playing
		{
			if(backgroundSource.volume < 1) //Volume is under 1
			{
				backgroundSource.volume += Time.deltaTime * 2; //Increase volume
			}
			else //Volume is 1
			{
				backgroundSource.volume = 1; //Set it to 1
			}
		}
	}

	private void songTwo() //Play the second song
	{
		if(!two) //Song not started playing yet
		{
			backgroundSource.volume -= Time.deltaTime * 2; //Reduce the volume of the current playing track
			if(backgroundSource.volume <= 0) //Track muted
			{
				backgroundSource.clip = backgroundClips[1]; //Change to play song number 2
				combatSource.clip = combatClips[1]; //Change to the correct combat song

				backgroundSource.Play(); //Start the background song
				combatSource.Play(); //Start the combat song

				two = true; //Playing the second track
				one = false; //Not playing the first track
				three = false; //Not playing the third track
			}
		}
		else //The second track is playing
		{
			if(backgroundSource.volume < 1) //Volume is under 1
			{
				backgroundSource.volume += Time.deltaTime * 2; //Increase volume
			}
			else //Volume is 1
			{
				backgroundSource.volume = 1; //Set it to 1
			}
		}
	}

	private void songThree() //Play the third song
	{
		if(!three) //Third track not yet activated
		{
			backgroundSource.volume -= Time.deltaTime * 2; //Reduce the volume of the current playing track
			if(backgroundSource.volume <= 0) //Track muted
			{
				backgroundSource.clip = backgroundClips[2]; //Change to play song number 3
				combatSource.clip = combatClips[2]; //Change to the correct combat song 
				backgroundSource.Play(); //Start the background song
				combatSource.Play(); //Start the combat song

				three = true; //Playing the third track
				one = false; //Not playing the first track
				two = false; //Not playing the second track
			}
		}
		else //Third track is playing
		{
			if(backgroundSource.volume < 1) //Volume is under 1
			{
				backgroundSource.volume += Time.deltaTime * 2; //Increase volume
			}
			else //Volume is 1
			{
				backgroundSource.volume = 1; //Set it to 1
			}
		}
	}
}
