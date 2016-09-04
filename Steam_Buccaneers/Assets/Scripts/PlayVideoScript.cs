using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayVideoScript : MonoBehaviour
{
	private MovieTexture movie;
	float timer;
	float forrigeTid;
	GameObject guiManager;

	void Start()
	{
		//Getting time
		forrigeTid = Time.realtimeSinceStartup;
		//finding minimap and hiding it so it doesnt cover animation
		guiManager = GameObject.Find("_GUIManager");
		guiManager.SetActive (false);
	}

	// Use this for initialization
	void Update () 
	{
		//counting time
		timer += Time.realtimeSinceStartup - forrigeTid;
		//If it has gone more than 6 sec animation is done and object will be destroyed
		//Wanted to code this without hardcoding seconds, but could not find another solution
		if (timer >= 6)
		{
			//Unpause game
			GameObject.Find ("GameControl").GetComponent<GameButtons> ().pause ();
			//Show minimap
			guiManager.SetActive(true);
			Destroy (this.gameObject);
		}
		forrigeTid = Time.realtimeSinceStartup;
	}

	public void playTreasureAnimation () 
	{
		//show movie
		this.enabled = true;
		movie = this.GetComponent<RawImage>().mainTexture as MovieTexture;
		movie.Play();
	}
}