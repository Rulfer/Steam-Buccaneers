using UnityEngine;
using System.Collections;

public class MinimapCamera : MonoBehaviour {
	private GameObject player;
	public float yPos;
	private bool isMap = false;
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

//	void makeMap()
//	{
//		isMap = !isMap;
//		if(isMap == true)
//		{
//			this.GetComponent<Camera>().orthographic = true;
//			this.GetComponent<Camera>().farClipPlane = 8800;
//			this.GetComponent<Camera>().fieldOfView = 90;
//			this.GetComponent<Camera>().cullingMask = "Minimap";
//			this.transform.position = new Vector3 (0, 5585, 6765);
//			this.transform.rotation = Quaternion.Euler(90, -90, 0);
//		}
//	}
}
