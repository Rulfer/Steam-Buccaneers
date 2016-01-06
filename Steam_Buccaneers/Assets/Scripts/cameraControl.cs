using UnityEngine;
using System.Collections;

public class cameraControl : MonoBehaviour 
{
	public float distanceAway;
	private float amountScrolled;
	public float scrollBy;

	// Use this for initialization
	void Start () 
	{
		//scrollBy = 0.5;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// låser kamera til player
		//Vector3 PlayerPOS = GameObject.Find("space donger 5 million").transform.transform.position;
		Vector3 PlayerPOS = GameObject.Find("PlayerShip").transform.transform.position;
		GameObject.Find("MainCamera").transform.position = new Vector3(PlayerPOS.x, (PlayerPOS.y)- distanceAway, (PlayerPOS.z)); 

		// Kamera zoom
		float scrollDistance = Input.GetAxisRaw("Mouse ScrollWheel");

		if (scrollDistance > 0f)
		{
			// scroll in
			if (distanceAway <= -1)
			{
				//Debug.Log (scrollDistance);
				distanceAway += scrollBy;
				//Debug.Log (distanceAway);
			}
		}
		else if (scrollDistance < 0f)
		{
			// scroll out
			if (distanceAway >= -4)
			{
				//Debug.Log (scrollDistance);
				distanceAway -= scrollBy;
				//Debug.Log (distanceAway);
			}
		}
	}
}
