using UnityEngine;
using System.Collections;

public class AnimationPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {

		this.GetComponent<Animator> ().SetTrigger ("Idle");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
