using UnityEngine;
using System.Collections;
using EZCameraShake;

public class AIprojectile : MonoBehaviour {
	private CheatCodesScript cheats;

	private float projectileSpeed = 175;
	public int damageOutput;
	private float distance;
	public Rigidbody test;
	public GameObject explotion;

	public AudioClip[] hitSounds;
	private AudioSource source;

	CameraShakeInstance shake;

	private bool musicPlayer = false;

	// Use this for initialization
	void Start () 
	{
		source = this.GetComponent<AudioSource>();
		test.AddForce (this.transform.right * projectileSpeed);
	}
	
	// Update is called once per frame
	void Update () 
	{
		distance = Vector3.Distance(transform.position, GameObject.Find("PlayerShip").transform.position);

		if (distance >= 500)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			if(CheatCodesScript.godMode == false)
			{
				int tempSound = Random.Range(0, 3);
				source.clip = hitSounds[tempSound];
				source.Play();

				GameControl.control.health -= damageOutput;
				CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(1, 5, 0.10f, 0.8f);

				Instantiate(explotion, this.transform.position, this.transform.rotation);
				this.GetComponent<MeshFilter>().mesh = null;
				musicPlayer = true;
				Destroy(this.gameObject, source.clip.length);
			}
		}

		if(other.tag == "aiShip") //The AI hit itself
		{
			int tempSound = Random.Range(0, 3);
			source.clip = hitSounds[tempSound];
			source.Play();

			other.transform.GetComponentInParent<AIMaster>().aiHealth -= damageOutput;
			other.GetComponentInParent<AIMaster>().aiHealth -= damageOutput;

			Instantiate(explotion, this.transform.position, this.transform.rotation);
			this.GetComponent<MeshFilter>().mesh = null;
			musicPlayer = true;
			Destroy(this.gameObject, source.clip.length);
		}

		if(other.tag == "shop" || other.tag == "Planet")
		{
			int tempSound = Random.Range(0, 3);
			source.clip = hitSounds[tempSound];
			source.Play();

			Instantiate(explotion, this.transform.position, this.transform.rotation);
			this.GetComponent<MeshFilter>().mesh = null;
			musicPlayer = true;
			Destroy(this.gameObject, source.clip.length);
		}
	}

	void killProjectile()
	{
		Destroy(this.gameObject);
	}
}
