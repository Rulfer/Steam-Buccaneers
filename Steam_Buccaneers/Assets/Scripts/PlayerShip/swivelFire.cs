using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class swivelFire : MonoBehaviour 
{

	public GameObject cannonball;
	public GameObject aimedCursor;
	public float fireRate;
	public float fireDelay;
	//public static int shotSpeed = 30;
	public AudioSource source;
	public AudioClip[] clips;
	bool fired = false;
	//float loadTimer = 0;
	//float loadDuration;

	void Start () 
	{
		source = GetComponent<AudioSource> ();
		//loadDuration = (fireRate + fireDelay) - 0.6f;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton("Fire2"))
		{
			Cursor.visible = false;
			aimedCursor.GetComponent<RawImage>().enabled = true;
			//Debug.Log ("fukku shittu");
			if (Input.GetButtonUp ("Fire1") && Time.time > fireDelay && GameControl.control.specialAmmo > 0 && GameControl.control.health > 0)
			{
				fireDelay = Time.time + fireRate;
				Instantiate (cannonball, transform.position , transform.rotation);
				source.clip = clips[0];
				source.Play();
				GameControl.control.specialAmmo -= 1;
				GameObject.Find("value_ammo_tab").GetComponent<Text>().text = GameControl.control.specialAmmo.ToString();
				fired = true;

				if (GameObject.Find ("TutorialControl") != null && GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().mouse1Check == false && GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().dialogNumber == 18)
				{
					GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().mouse1Check = true;
					GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().nextButton.SetActive (true);
				}
			}
		}

		else
		{
			Cursor.visible = true;
			aimedCursor.GetComponent<RawImage>().enabled = false;
		}
		if(Input.GetButtonDown("Fire2"))
		{
			source.clip = clips[1];
			source.Play();
		}

		if(Time.time > fireDelay && fired == true)
		{
			source.clip = clips[2];
			source.Play();
			fired = false;
		}
			
	}
}
