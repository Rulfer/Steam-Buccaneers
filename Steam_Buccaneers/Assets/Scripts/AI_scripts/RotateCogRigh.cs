using UnityEngine;
using System.Collections;

public class RotateCogRigh : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(0, 50 * Time.deltaTime, 0);

	}
}
