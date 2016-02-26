using UnityEngine;
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
		if (Input.GetButtonUp ("Fire1") && Time.time > fireDelay && PlayerStatus.swivelAmmo > 0)
		{
			AudioSource pewPew = GetComponent<AudioSource> ();
			Debug.Log ("pew");
			fireDelay = Time.time + fireRate;
			Instantiate (cannonball, transform.position , transform.rotation);
			pewPew.Play();
			PlayerStatus.swivelAmmo -= 1;
			Debug.Log (PlayerStatus.swivelAmmo);
			//transform.TransformDirection(Vector3(0,0,shotSpeed));
		}
	}
}
