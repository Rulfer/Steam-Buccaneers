using UnityEngine;
using System.Collections;
using EZCameraShake;
using UnityEngine.SceneManagement;

public class AIprojectile : MonoBehaviour {
	private CheatCodesScript cheats;

	public float projectileSpeed = 175;
	public int damageOutput;
	private float distance;
	private float aliveTimer = 0f;
	private float aliveDuration = 20f;
	public Rigidbody test;

	public GameObject explotion;
	private GameObject player;

	public AudioClip[] hitSounds;
	private AudioSource source;

	CameraShakeInstance shake;

	// Use this for initialization
	void Start () 
	{
		source = this.GetComponent<AudioSource>();
		test.AddForce (this.transform.right * projectileSpeed);
		player = GameObject.Find("PlayerShip");
	}
	
	// Update is called once per frame
	void Update () 
	{
		distance = Vector3.Distance(transform.position, player.transform.position);

		if (distance >= 500)
		{
			Destroy(gameObject);
		}
		if(aliveTimer < aliveDuration)
			aliveTimer += Time.deltaTime;
		else
			Destroy(gameObject);
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
				other.GetComponentInChildren<changeMaterial> ().checkPlayerHealth();

				CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(1, 5, 0.10f, 0.8f);

				Instantiate(explotion, this.transform.position, this.transform.rotation);
				this.GetComponent<MeshFilter>().mesh = null;
				Destroy(this.gameObject, source.clip.length);
			}
		}

		if(other.tag == "aiShip") //The AI hit itself
		{
			int tempSound = Random.Range(0, 3);
			source.clip = hitSounds[tempSound];
			source.Play();

			if(other.transform.root.name == "Cargo(Clone)")
				other.transform.GetComponentInParent<AIMaster>().thisAIFlee();
			
			if(other.transform.root.name == "Boss(Clone)" && (other.GetComponentInParent<AIMaster>().aiHealth - damageOutput) <= 0)
			{
				SceneManager.LoadScene("cog_screen");
			}

			other.GetComponentInParent<AIMaster>().aiHealth -= damageOutput;
			if(other.transform.GetComponentInParent<AIMaster>().aiHealth <= 0)
			{
				other.GetComponentInParent<DeadAI>().forcePos = this.transform.position;
				other.transform.GetComponentInParent<AIMaster>().killAI();
				other.GetComponentInParent<DeadAI>().createForce();
			}
			else if(other.GetComponentInParent<AIMaster>().aiHealth <= other.GetComponentInParent<AIMaster>().aiHealthMat3)
			{
				other.GetComponentInParent<AIMaster>().changeMat3();
				other.GetComponentInParent<AIMaster>().testFleeing();
			}
			else if(other.GetComponentInParent<AIMaster>().aiHealth <= other.GetComponentInParent<AIMaster>().aiHealthMat2)
				other.GetComponentInParent<AIMaster>().changeMat2();
			
			Instantiate(explotion, this.transform.position, this.transform.rotation);
			this.GetComponent<MeshFilter>().mesh = null;
			Destroy(this.gameObject, source.clip.length);
		}

		if(other.tag == "shop" || other.tag == "Planet")
		{
			int tempSound = Random.Range(0, 3);
			source.clip = hitSounds[tempSound];
			if(this.gameObject != null)
				source.Play();

			Instantiate(explotion, this.transform.position, this.transform.rotation);
			explotion.GetComponent<DeleteParticles>().killDuration = 2;
			this.GetComponent<MeshFilter>().mesh = null;
			Destroy(this.gameObject, source.clip.length);
		}
	}
}
