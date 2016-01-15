using UnityEngine;
using System.Collections;

public class ballsPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 PlayerPOS = GameObject.Find("PlayerShip").transform.transform.position;
		GameObject.Find("Balls").transform.position = new Vector3(PlayerPOS.x, (PlayerPOS.y), (PlayerPOS.z));
	}
}
