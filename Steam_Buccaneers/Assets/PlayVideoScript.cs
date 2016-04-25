using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayVideoScript : MonoBehaviour
{
	private MovieTexture movie;

	// Use this for initialization
	void Start () 
	{
		movie = this.GetComponent<RawImage>().mainTexture as MovieTexture;
		movie.Play();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
