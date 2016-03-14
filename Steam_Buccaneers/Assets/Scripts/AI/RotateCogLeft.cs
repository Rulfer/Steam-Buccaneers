using UnityEngine;
using System.Collections;

public class RotateCogLeft : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.root.tag != "Player")
			this.transform.Rotate(0, -50 * Time.deltaTime, 0);
		else
			this.transform.Rotate(-50 * Time.deltaTime, 0, 0);
	}
}
