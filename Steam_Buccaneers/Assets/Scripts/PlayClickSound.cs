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
		if(Input.GetMouseButtonDown(0))
		{
			Debug.Log("hello");
			source.PlayOneShot(effect);
		}
	}
}
