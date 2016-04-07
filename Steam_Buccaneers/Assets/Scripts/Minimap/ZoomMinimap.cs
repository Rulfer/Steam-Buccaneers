using UnityEngine;
using System.Collections;

public class ZoomMinimap : MonoBehaviour
{
	public Camera minimapCamera;
	// Use this for initialization


	public void zoomOut()
	{
		if(minimapCamera.orthographicSize < 500)
			minimapCamera.orthographicSize += 50;
	}

	public void zoomIn()
	{
		if(minimapCamera.orthographicSize > 100)
			minimapCamera.orthographicSize -= 50;
	}
}
