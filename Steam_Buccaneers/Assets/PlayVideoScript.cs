using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayVideoScript : MonoBehaviour
{
	private MovieTexture movie;
	float timer;
	float forrigeTid;

	void Start()
	{
		forrigeTid = Time.realtimeSinceStartup;
	}

	// Use this for initialization
	void Update () 
	{
		timer += Time.realtimeSinceStartup - forrigeTid;
		//Debug.Log (Time.time + "-" + forrigeTid);
		//Debug.Log (timer);
		if (timer >= 6)
		{
			Destroy (this.gameObject);
			GameObject.Find ("GameControl").GetComponent<gameButtons> ().pause ();
		}
		forrigeTid = Time.realtimeSinceStartup;
	}

	public void playTreasureAnimation () 
	{
		this.enabled = true;
		movie = this.GetComponent<RawImage>().mainTexture as MovieTexture;
		movie.Play();

	}
}
