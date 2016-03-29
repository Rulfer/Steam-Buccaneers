using UnityEngine;
using System.Collections;

public class MinimapCamera : MonoBehaviour {
	private GameObject player;
	public float yPos;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("PlayerShip");
		yPos = 500;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(player.transform.position.x, yPos, player.transform.position.z);
	}
}
