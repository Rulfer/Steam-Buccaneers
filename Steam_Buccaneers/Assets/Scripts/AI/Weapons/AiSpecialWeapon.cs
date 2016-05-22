using UnityEngine;
using System.Collections;

public class AISpecialWeapon : MonoBehaviour {

	public GameObject bomb;
	public GameObject bombSpawner;
	public GameObject teleportParticle;
	public Animation bombAnim;

	private int waitBeforeAnimationStart = 2;
	private float timer = 0;
	private float animTimer = 0;
	private bool placedBomb = false;

	void Update()
	{
		if(!bombAnim.isPlaying)
		{
			timer += Time.deltaTime;
			if(placedBomb == false)
			{
				placeBomb();
			}
		}
		else
		{
			animTimer += Time.deltaTime;
			if(animTimer >= 2.1f)
			{
				Instantiate(teleportParticle, bombAnim.transform.position, bombAnim.transform.rotation);
				animTimer = 0;
			}
		}
		if(timer > waitBeforeAnimationStart)
		{
			bombAnim.Play();
			placedBomb = false;
			timer = 0;
			animTimer = 0;
		}
	}
	
	void placeBomb()
	{
		Instantiate(bomb, this.transform.position, this.transform.rotation);
		Instantiate(teleportParticle, this.transform.position, this.transform.rotation);
		placedBomb = true;
	}
}
