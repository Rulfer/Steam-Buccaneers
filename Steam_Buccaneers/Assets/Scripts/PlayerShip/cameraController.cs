using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public float distanceAway; // variable for the distance away the camera is from the player in the y-axis
	private float boostDistance = 0; //variable used for calculating camera zoom during boosting
	private Vector3 PlayerPOS; // variable for the position of our player
	private GameObject player; //the player object
	private float cameraAddPOS; //variable used for zooming in and out

	// Use this for initialization
	void Start () 
	{
		cameraAddPOS = distanceAway; // setting the variable we use to zoom equal to the starting position of the camera
		player = GameObject.Find("PlayerShip");	// finding the player we need to follow
	}
	
	// Update is called once per frame
	void Update () 
	{
		PlayerPOS = player.transform.transform.position; // getting the position of the player for the camera to follow

		// setting the camera to follow the player, as well setting the 
		// diffrent distance variables used to calculate how far away the camera should be the player
		this.transform.position = new Vector3(PlayerPOS.x, (PlayerPOS.y)+distanceAway+boostDistance, (PlayerPOS.z)); 


		if(MinimapCamera.miniCam.isMinimap)
		{
			// Camera zooming
			float scrollDistance = Input.GetAxisRaw("Mouse ScrollWheel"); // float for measuring which direction player scrolls

			if (scrollDistance > 0f)// if player scrolls up, we will subtract from the cameras current position
			{
				if (cameraAddPOS > 50)// if the camera is above 50, which is the lowest distance the camera can be away, it will subtract
				{
					cameraAddPOS -= 5; // we subtract by 5 for each scroll to the distance we are to reach by scrolling
				}
			}

			if (scrollDistance < 0f)// if the player scrolls down, we will add to the cameras current position
			{
				if (cameraAddPOS < 200)// if the camera is below 200, which is the highest distance the camera can be away, we will add
				{
					cameraAddPOS += 5; // we add by 5 for each scroll to the distance we are to reach by scrolling
				}
			}
				

			if (distanceAway > cameraAddPOS)// if the current distance of the camera is greater than the position the camera should be in

			{
				if (distanceAway >= 50)// as long as the distance is higher than 50
				{
					distanceAway --;// we will zoom the camera in
				}
			}

			else if (distanceAway < cameraAddPOS)// if the current distance of the camera is lower than the position the camera should be in
			{
				if (distanceAway <= 200) // as long as the distance is lower than 200
				{
					distanceAway ++; // we will zoom the camera out
				}
			}

			if (PlayerMove.isBoosting) // if the player is boosting, we will zoom the camera out 50 from its current position
			{
				if (boostDistance <= 49)
				{
					boostDistance ++; // we will zoom the camera out
				}
			}

			else
			{
				if (boostDistance >= 1) // if we are not zooming, and the camera is over 0, we will zoom it back in after boosting
				{
					boostDistance --; // we will zoom the camera in
				}
			}
		}

		else
		{
			this.transform.position = new Vector3(PlayerPOS.x, (PlayerPOS.y)+210, (PlayerPOS.z));
		}
	}
}
