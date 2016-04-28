using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour 
{
	public float distanceAway;
	private float amountScrolled;
	public float scrollBy;
	private float boostDistance = 0;

	private Vector3 PlayerPOS;
	private GameObject player;
	private GameObject gameCamera;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("PlayerShip");	
		//gameCamera = GameObject.Find("MaingameCamera");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (boostDistance);

		// låser kamera til player
		//Vector3 PlayerPOS = GameObject.Find("space donger 5 million").transform.transform.position;
		PlayerPOS = player.transform.transform.position;
		this.transform.position = new Vector3(PlayerPOS.x, (PlayerPOS.y)+distanceAway+boostDistance, (PlayerPOS.z)); 

		if(MinimapCamera.miniCam.isMinimap)
		{
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

			if (PlayerMove2.isBoosting)
			{
				if (boostDistance <= 49)
				{
				boostDistance++;
				}
			}

			else
			{
				if (boostDistance >= 1)
				{
					boostDistance --;
				}
			}
		}
		else
		{
			this.transform.position = new Vector3(PlayerPOS.x, (PlayerPOS.y)+210, (PlayerPOS.z)); 
		}
	}
}
