using UnityEngine;
using System.Collections;

public class MinimapCamera : MonoBehaviour {
	private GameObject player;
	public float yPos;
	private bool isMinimap = true;
	private float ortSize;
	public RenderTexture minimapTexture;
	public GameObject ingameCanvas;
	public GameObject minimap;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("PlayerShip");
		yPos = 500;
		StartCoroutine(PauseCoroutine());

	}

	IEnumerator PauseCoroutine()
	{
		while(true)
		{
			if(Input.GetKeyDown(KeyCode.M))
			{
				if(!isMinimap)
					deactivateBigMap();
				else
					activateBigMap();
			}
			yield return null;
		}
	}

	void activateBigMap()
	{
		isMinimap = false;
		Time.timeScale = 0;
		ortSize = this.GetComponent<Camera>().orthographicSize;
		this.transform.position = new Vector3(0, 500, 7220);
		this.transform.rotation = Quaternion.Euler(90, -90, 0);
		this.GetComponent<Camera>().orthographicSize = 4000;
		this.GetComponent<Camera>().targetTexture = null;
		ingameCanvas.SetActive(false);
		minimap.SetActive(false);
	}

	void deactivateBigMap()
	{
		isMinimap = true;
		Time.timeScale = 1;
		this.transform.rotation = Quaternion.Euler(90, 0, 0);
		this.GetComponent<Camera>().orthographicSize = ortSize;
		this.GetComponent<Camera>().targetTexture = minimapTexture;
		ingameCanvas.SetActive(true);
		minimap.SetActive(true);
	}

	// Update is called once per frame
	void Update ()
	{
		if(isMinimap)
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
