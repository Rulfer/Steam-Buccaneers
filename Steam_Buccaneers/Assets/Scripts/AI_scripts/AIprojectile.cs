using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {

	public float projectileSpeed;
	public static float damageOutput;
	private float timer = 10;

	// Use this for initialization
	void Start () {
		if (this.tag == "ball1") 
		{
			damageOutput = 1;
		}
		if (this.tag == "ball2") 
		{
			damageOutput = 2;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * projectileSpeed * Time.deltaTime);
		timer -= Time.deltaTime;
		if (timer <= 0) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			Debug.Log ("We hit the player!");
			Debug.Log ("Damage delt is " + damageOutput);
			Destroy (this.gameObject);

		}
	}
}
