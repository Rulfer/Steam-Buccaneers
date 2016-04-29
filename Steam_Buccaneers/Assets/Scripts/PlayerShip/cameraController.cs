using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour 
{
	public float distanceAway;
	private float amountScrolled;
	public float scrollBy;
	private float boostDistance = 0;
/*	Vector3 maxDistance;
	Vector3 maxDistanceBoosting;
	Vector3 minDistance;
	float tempDist;
	Vector3 cameraNow;
	float startTime;
	float travelLength;
	float speed = 100f;*/

	private Vector3 PlayerPOS;
	private GameObject player;
	private GameObject gameCamera;

	// Use this for initialization
	void Start () 
	{
		//tempDist = (PlayerPOS.y)+distanceAway;
		player = GameObject.Find("PlayerShip");	
		//gameCamera = GameObject.Find("MaingameCamera");
	}
	
	// Update is called once per frame
	void Update () 
	{/*
		if (Input.GetKeyDown (KeyCode.LeftShift))
		{
			tempDist = (PlayerPOS.y)+distanceAway;
			startTime = Time.time;
		}

		if (Input.GetKeyUp (KeyCode.LeftShift) || PlayerMove2.boostCooldownTimer <= 0f)
		{
			startTime = Time.time;
		}

		Debug.Log (tempDist);
		*/
		// låser kamera til player
		//Vector3 PlayerPOS = GameObject.Find("space donger 5 million").transform.transform.position;
		PlayerPOS = player.transform.transform.position;
		/*
		maxDistance = new Vector3 (PlayerPOS.x, PlayerPOS.y +200f, PlayerPOS.z);
		maxDistanceBoosting = new Vector3 (PlayerPOS.x, PlayerPOS.y + 250f, PlayerPOS.z);
		minDistance = new Vector3 (PlayerPOS.x, PlayerPOS.y +50f, PlayerPOS.z);
		*/
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
					boostDistance ++;
				}
			}

			else
			{
				if (boostDistance >= 1)
				{
					boostDistance --;
				}
			}
			/*
			if (PlayerMove2.isBoosting && this.transform.position != maxDistanceBoosting)
			{
				travelLength = Vector3.Distance(cameraNow,maxDistanceBoosting);
				float distCovered = (Time.time -startTime)*speed;
				float fracJourney = distCovered /travelLength;
				cameraNow = new Vector3(PlayerPOS.x, tempDist, PlayerPOS.z);
				this.transform.position = Vector3.Lerp(cameraNow, maxDistanceBoosting, fracJourney);
			}

			else
			{
				travelLength = Vector3.Distance(cameraNow,maxDistanceBoosting);
				float distCovered = (Time.time -startTime)*speed;
				float fracJourney = distCovered /travelLength;
				cameraNow = new Vector3(PlayerPOS.x, tempDist, PlayerPOS.z);
				this.transform.position = Vector3.Lerp(maxDistanceBoosting, cameraNow, fracJourney);
			}*/
		}
		else
		{
			this.transform.position = new Vector3(PlayerPOS.x, (PlayerPOS.y)+210, (PlayerPOS.z)); 
		}
	}
}
