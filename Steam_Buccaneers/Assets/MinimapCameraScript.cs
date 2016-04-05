using UnityEngine;
using System.Collections;

public class MinimapCameraScript : MonoBehaviour
{
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("PlayerShip");
	}
	
	void LateUpdate() //Handled after all other updates
	{
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
	}
}
