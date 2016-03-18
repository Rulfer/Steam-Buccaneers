using UnityEngine;
using System.Collections;

public class DeleteParticles : MonoBehaviour {
	private float killTimer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		killTimer += Time.deltaTime;
		if(killTimer >= 2)
			Destroy(this.gameObject);
	}
}
