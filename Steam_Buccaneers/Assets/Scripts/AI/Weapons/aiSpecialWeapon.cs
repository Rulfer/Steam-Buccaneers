using UnityEngine;
using System.Collections;

public class AISpecialWeapon : MonoBehaviour {

	public GameObject bomb; //Bomb to be spawned
	public GameObject bombSpawner; //Empty gameobject where the bombs spawn
	public GameObject teleportParticle; //Particlesimulatiom
	public Animation bombAnim; //Animation for the bomb spawner

	private int waitBeforeAnimationStart = 2; //2 seconds delay after animation starts before the bomb is teleported
	private float timer = 0; //Timer
	private float animTimer = 0; //Animation timer
	private bool placedBomb = false; //See if a bomb has spawned

	void Update()
	{
		if(!bombAnim.isPlaying) //the animation is not playing
		{
			timer += Time.deltaTime; //Add time to the timer
			if(placedBomb == false) //If a bomb is not placed
			{
				placeBomb(); //Place a bomb
			}
		}
		else //The animation is playing
		{
			animTimer += Time.deltaTime; //Add time to the timer
			if(animTimer >= 2.1f) //If the timer is equal or greater than 2.1 seconds, do this
			{
				Instantiate(teleportParticle, bombAnim.transform.position, bombAnim.transform.rotation); //Instantiates a bomb at the animation object
				animTimer = 0; //Reset the timer
			}
		}
		if(timer > waitBeforeAnimationStart) //We wait before we spawn a new bomb
		{
			bombAnim.Play(); //Play the animation
			placedBomb = false; //A new bomb will be placed
			timer = 0; //Reset the timer
			animTimer = 0; //Reset the timer
		}
	}
	
	void placeBomb()
	{
		Instantiate(bomb, this.transform.position, this.transform.rotation); //Instantiate a bomb at the bombspawners position
		Instantiate(teleportParticle, this.transform.position, this.transform.rotation); //Play a particle simulation as well
		placedBomb = true; //A bomb has been placed
	}
}
