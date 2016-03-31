using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class swivelFire : MonoBehaviour 
{

	public GameObject cannonball;
	public float fireRate;
	public float fireDelay;
	//public static int shotSpeed = 30;
	public AudioSource pewPew;
	//public Vector3 position;
	//public Quaternion rotation;
	//public GameObject shotSpawn;
	// Use this for initialization
	void Start () 
	{
		//AudioSource pewPew = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log ("fukku shittu");
		if (Input.GetButtonUp ("Fire1") && Time.time > fireDelay && GameControl.control.specialAmmo > 0 && GameControl.control.health > 0)
		{
			AudioSource pewPew = GetComponent<AudioSource> ();
			fireDelay = Time.time + fireRate;
			Instantiate (cannonball, transform.position , transform.rotation);
			pewPew.Play();
			GameControl.control.specialAmmo -= 1;
			GameObject.Find("value_ammo_tab").GetComponent<Text>().text = GameControl.control.specialAmmo.ToString();

			if (GameObject.Find ("TutorialControl") != null && GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().mouse1Check == false && GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().dialogNumber == 18)
			{
				GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().mouse1Check = true;
				GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().nextButton.SetActive (true);
			}
		}
	}
}
