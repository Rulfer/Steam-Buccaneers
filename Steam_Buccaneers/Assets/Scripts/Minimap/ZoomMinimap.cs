using UnityEngine;
using System.Collections;

public class ZoomMinimap : MonoBehaviour
{
	public Camera minimapCamera; //Reference to the minimap camera object

	public void zoomOut() //Move the camera farther away from the player
	{
		if(minimapCamera.orthographicSize < 500) //Camera is not too far away
			minimapCamera.orthographicSize += 50; //Zoom out minimap
	}

	public void zoomIn() //Move the camera closer to the player
	{
		if(minimapCamera.orthographicSize > 100) //Camera is not too close
			minimapCamera.orthographicSize -= 50; //Zoom in minimap
	}
}
