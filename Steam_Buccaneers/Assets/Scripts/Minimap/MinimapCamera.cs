using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MinimapCamera : MonoBehaviour 
{
	public static MinimapCamera miniCam;
	public float yPos;
	public bool isMinimap = true;
	private float ortSize;
	public RenderTexture minimapTexture; //minimapTexture.renderTexture
	public RenderTexture bigmapTexture; //bigmapTexture.renderTexture
	public GameObject extraBackground; //map_world_bg (1)
	public GameObject minimapCanvas; //MiniMap in Canvas_Ingame
	public GameObject minimapBackground; //_GUIManager
	public GameObject animationCanvas; //dialogue_elements in Canvas_Ingame
	public GameObject renderPlane; //map_world_bg
	public GameObject renderPlaneBackground; //Plane
	public GameObject compas; //Canvas with compas and it's choices

	public GameObject[] shops;
	public GameObject[] diamonds;
	public GameObject[] moonIcons;
	private GameObject[] moons;
	private GameObject player;
	private GameObject boss;

	// Use this for initialization
	void Start () 
	{
		miniCam = this;
		yPos = 500;
		StartCoroutine(PauseCoroutine());
		player = GameObject.Find("map_icon_player");
		boss = GameObject.Find("map_icon_boss");
	}

	IEnumerator PauseCoroutine()
	{	
		if(SceneManager.GetActiveScene().name != "Tutorial")
		{
			while(true)
			{
				if(Input.GetKeyDown(KeyCode.M))
				{
					if (!isMinimap)
						deactivateBigMap ();
					else
					{
						if(Time.timeScale != 0)
						activateBigMap ();
					}
				}
				yield return null;
			}
		}
	}

	void activateBigMap()
	{
		isMinimap = false;
		Time.timeScale = 0;
		ortSize = this.GetComponent<Camera>().orthographicSize;
		this.transform.position = new Vector3(0, 700, 7220);
		this.transform.rotation = Quaternion.Euler(90, -90, 0);
		this.GetComponent<Camera>().orthographicSize = 8000;
		this.GetComponent<Camera>().targetTexture = bigmapTexture;
		this.GetComponent<Camera>().cullingMask = 1 << 10 | 1 << 11;
		minimapCanvas.SetActive(false);
		renderPlane.SetActive(true);
		extraBackground.SetActive(true);
		renderPlaneBackground.SetActive(true);
		minimapBackground.SetActive(false);
		animationCanvas.SetActive(false);
		compas.SetActive(false);
		boss.transform.localScale = new Vector3(21, 21, 21);
		player.transform.localScale = new Vector3(1000, 1000, 1000);
		foreach(GameObject go in shops)
			go.transform.localScale = new Vector3 (20, 20, 20);
		foreach(GameObject go in diamonds)
			go.transform.localScale = new Vector3 (20, 20, 20);

		foreach(GameObject go in moonIcons)
			go.name = go.name.Replace("_icon", "");

		moons = GameObject.FindGameObjectsWithTag("Moon");
		foreach(GameObject icon in moonIcons)
		{
			foreach(GameObject moon in moons)
			{
				if(icon.name == moon.name)
					icon.transform.position = moon.transform.position;
			}
		}
	}

	void deactivateBigMap()
	{
		isMinimap = true;
		Time.timeScale = 1;
		minimapCanvas.SetActive(true);
		renderPlane.SetActive(false);
		extraBackground.SetActive(false);
		renderPlaneBackground.SetActive(false);
		minimapBackground.SetActive(true);
		animationCanvas.SetActive(true);
		compas.SetActive(true);
		this.transform.rotation = Quaternion.Euler(90, 0, 0);
		this.GetComponent<Camera>().orthographicSize = ortSize;
		this.GetComponent<Camera>().targetTexture = minimapTexture;
		this.GetComponent<Camera>().cullingMask = 1 << 8 | 1 << 9 | 1 << 10;
		boss.transform.localScale = new Vector3(4.1f, 4.1f, 4.1f);
		player.transform.localScale = new Vector3(99.2f, 99.2f, 99.2f);
		foreach(GameObject go in shops)
			go.transform.localScale = new Vector3 (10.55f, 10.55f, 10.55f);
		foreach(GameObject go in diamonds)
			go.transform.localScale = new Vector3 (14.4f, 14.4f, 14.4f);
		foreach(GameObject go in moonIcons)
			go.transform.name = go.transform.name + "_icon";
	}

	// Update is called once per frame
	void Update ()
	{
		if(isMinimap)
			this.transform.position = new Vector3(player.transform.position.x, yPos, player.transform.position.z);
	}
}
