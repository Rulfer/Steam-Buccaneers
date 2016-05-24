using UnityEngine;
using System.Collections;

public class PlayClickSound : MonoBehaviour {

	private AudioSource source;
	public AudioClip effect;
	// Use this for initialization

	void Start()
	{
		source = this.GetComponent<AudioSource>();
		source.clip = effect;
	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0)) //When the player clicks while in the shop, play a audio clip
		{
			source.PlayOneShot(effect); //Play the clip
		}
	}
}
