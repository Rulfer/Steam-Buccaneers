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
		forrigeTid = Time.realtimeSinceStartup;
		guiManager = GameObject.Find("_GUIManager");
		guiManager.SetActive (false);
	}

	// Use this for initialization
	void Update () 
	{
		timer += Time.realtimeSinceStartup - forrigeTid;
		//Debug.Log (Time.time + "-" + forrigeTid);
		//Debug.Log (timer);
		if (timer >= 6)
		{
			GameObject.Find ("GameControl").GetComponent<GameButtons> ().pause ();
			guiManager.SetActive(true);
			Destroy (this.gameObject);
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