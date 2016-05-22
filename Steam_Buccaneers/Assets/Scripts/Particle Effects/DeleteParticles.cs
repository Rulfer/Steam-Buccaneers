using UnityEngine;
using System.Collections;

public class DeleteParticles : MonoBehaviour {
	private float killTimer;
	public float killDuration;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		killTimer += Time.deltaTime;
		if(killTimer >= killDuration)
			Destroy(this.gameObject);
	}
}
