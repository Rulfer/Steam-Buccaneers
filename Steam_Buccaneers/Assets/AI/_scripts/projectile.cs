using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {

	public float projectileSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * projectileSpeed * Time.deltaTime);
	}
}
