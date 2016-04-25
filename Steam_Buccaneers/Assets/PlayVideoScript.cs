using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayVideoScript : MonoBehaviour
{
	private MovieTexture movie;

	// Use this for initialization
	void Update () 
	{
		if (this.isActiveAndEnabled)
		{
			//movie
		}
	}
	
	// Update is called once per frame
	public void playTreasureAnimation () 
	{
		this.enabled = true;
		movie = this.GetComponent<RawImage>().mainTexture as MovieTexture;
		movie.Play();
	}
}
