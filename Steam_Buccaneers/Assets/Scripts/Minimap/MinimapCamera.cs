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
	public GameObject mapCanvas;
	public LayerMask bigMapLayer;
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
		//this.transform.position = new Vector3(0, 700, 7220);
		//this.transform.rotation = Quaternion.Euler(90, -90, 0);
		//this.GetComponent<Camera>().orthographicSize = 4000;
		this.GetComponent<Camera>().targetTexture = null;
		this.GetComponent<Camera>().cullingMask = ~(1 >> 10); //This turned out to be a happy little accident
	//	this.GetComponent<Camera>().cullingMask = bigMapLayer;
	//	ingameCanvas.SetActive(false);
		mapCanvas.SetActive(true);
	//	minimap.SetActive(false);
	}

	void deactivateBigMap()
	{
		isMinimap = true;
		Time.timeScale = 1;
		ingameCanvas.SetActive(true);
		minimap.SetActive(true);
		mapCanvas.SetActive(false);
		this.transform.rotation = Quaternion.Euler(90, 0, 0);
		this.GetComponent<Camera>().orthographicSize = ortSize;
		this.GetComponent<Camera>().targetTexture = minimapTexture;
		this.GetComponent<Camera>().cullingMask = 1 << 8 | 1 << 9 | 1 << 10;

	}

	// Update is called once per frame
	void Update ()
	{
		if(isMinimap)
			this.transform.position = new Vector3(player.transform.position.x, yPos, player.transform.position.z);
	}
}
