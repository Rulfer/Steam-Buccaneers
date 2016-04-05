using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour 
{
	public float distanceAway;
	private float amountScrolled;
	public float scrollBy;

	private Vector3 PlayerPOS;
	private GameObject player;
	private GameObject camera;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("PlayerShip");	
		camera = GameObject.Find("MainCamera");
	}
	
	// Update is called once per frame
	void Update () 
	{
		// låser kamera til player
		//Vector3 PlayerPOS = GameObject.Find("space donger 5 million").transform.transform.position;
		PlayerPOS = player.transform.transform.position;
		camera.transform.position = new Vector3(PlayerPOS.x, (PlayerPOS.y)+distanceAway, (PlayerPOS.z)); 

		// Kamera zoom
		float scrollDistance = Input.GetAxisRaw("Mouse ScrollWheel");

		if (scrollDistance > 0f)
		{
			if (distanceAway >= 50)
			{
				//Debug.Log (scrollDistance);
				distanceAway -= scrollBy;
				//Debug.Log (distanceAway);
			}
		}
		else if (scrollDistance < 0f)
		{
			// scroll down
			if (distanceAway <= 200)
			{
				//Debug.Log (scrollDistance);
				distanceAway += scrollBy;
				//Debug.Log (distanceAway);
			}
		}
	}
}
