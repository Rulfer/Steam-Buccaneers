using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MinimapCamera : MonoBehaviour 
{
	public static MinimapCamera miniCam;
	public float yPos; //The y-position of this camera
	public bool isMinimap = true; //Currently displaying the minimap
	private float ortSize; //Size of camera
	public RenderTexture minimapTexture; //minimapTexture.renderTexture
	public RenderTexture bigmapTexture; //bigmapTexture.renderTexture
	public GameObject extraBackground; //map_world_bg (1)
	public GameObject minimapCanvas; //MiniMap in Canvas_Ingame
	public GameObject minimapBackground; //_GUIManager
	public GameObject animationCanvas; //dialogue_elements in Canvas_Ingame
	public GameObject renderPlane; //map_world_bg
	public GameObject renderPlaneBackground; //Plane
	public GameObject compas; //Canvas with compas and it's choices

	public GameObject[] shops; //Array holding all shop icons
	public GameObject[] diamonds; //Array holding all treasureplanets icons
	public GameObject[] moonIcons; //Array holding all moon icons
	private GameObject[] moons; //Array holding all moons
	private GameObject player; //Player icon
	private GameObject boss; //Boss spawn icon

	// Use this for initialization
	void Start () 
	{
		miniCam = this;
		yPos = 500; //Position of this camera
		StartCoroutine(PauseCoroutine()); //Makes it so that the player can open/close worldmap even though the game is paused
		player = GameObject.Find("map_icon_player");
		boss = GameObject.Find("map_icon_boss");
	}

	IEnumerator PauseCoroutine()
	{	
		if(SceneManager.GetActiveScene().name != "Tutorial") //This is not the tutorial scene
		{
			while(true)
			{
				if(Input.GetKeyDown(KeyCode.M)) //Player pressed M
				{
					if (!isMinimap) //Its the worldmap
						deactivateBigMap (); //Close worldmap and open minimap
					else //Worldmap isclosed
					{
						if(Time.timeScale != 0) //Pause the game
						activateBigMap (); //Open worldmap
					}
				}
				yield return null;
			}
		}
	}

	void activateBigMap() //Open worldmap
	{
		isMinimap = false; //Close the minimap
		Time.timeScale = 0; //Pause the game
		ortSize = this.GetComponent<Camera>().orthographicSize;
		this.transform.position = new Vector3(0, 700, 7220); //Move to this position
		this.transform.rotation = Quaternion.Euler(90, -90, 0); //Rotate the camera to show the world sideways
		this.GetComponent<Camera>().orthographicSize = 8000; //Size of the camera
		this.GetComponent<Camera>().targetTexture = bigmapTexture; //Change render texture
		this.GetComponent<Camera>().cullingMask = 1 << 10 | 1 << 11; //Change culling mask
		minimapCanvas.SetActive(false); //Deactivate minimap elements
		renderPlane.SetActive(true); //Activate the worldmap render texture
		extraBackground.SetActive(true); //Activate the background map
		renderPlaneBackground.SetActive(true); //Activate the background for the worldmap to render
		minimapBackground.SetActive(false); //Deactivate minimap renderer
		animationCanvas.SetActive(false); //Deactivate animation windows
		compas.SetActive(false); //Deactivate compas and quest elements
		boss.transform.localScale = new Vector3(21, 21, 21); //Scale the boss icon
		player.transform.localScale = new Vector3(1000, 1000, 1000); //Scale the player icon
		foreach(GameObject go in shops) //Scale all shop icons
			go.transform.localScale = new Vector3 (20, 20, 20);
		foreach(GameObject go in diamonds) //Scale all diamond icons
			go.transform.localScale = new Vector3 (20, 20, 20);

		foreach(GameObject go in moonIcons) //Rename all moon icons to just moon_ID
			go.name = go.name.Replace("_icon", "");

		moons = GameObject.FindGameObjectsWithTag("Moon"); //Push all moons into this array
		foreach(GameObject icon in moonIcons) //Modifies position of all moon icons
		{
			foreach(GameObject moon in moons)
			{
				if(icon.name == moon.name) //If an icon is == to the moon name
					icon.transform.position = moon.transform.position; //Set it to the same position
			}
		}
	}

	void deactivateBigMap() //Open minimap and close worldmap
	{
		isMinimap = true; //Minimap is open
		Time.timeScale = 1; //Unpause game
		minimapCanvas.SetActive(true); //Minimap elements turned on
		renderPlane.SetActive(false); //Worldmap background turned off
		extraBackground.SetActive(false); //Extra background turned off
		renderPlaneBackground.SetActive(false); //Worldmap render texture turned off
		minimapBackground.SetActive(true); //Minimap render texture turned on
		animationCanvas.SetActive(true); //Animation windows activated
		compas.SetActive(true); //Compas and quest info elements turned on
		this.transform.rotation = Quaternion.Euler(90, 0, 0); //Rotate this camera to be correct for the minimap
		this.GetComponent<Camera>().orthographicSize = ortSize;
		this.GetComponent<Camera>().targetTexture = minimapTexture; //Change render texture
		this.GetComponent<Camera>().cullingMask = 1 << 8 | 1 << 9 | 1 << 10; //Change culling mask
		boss.transform.localScale = new Vector3(4.1f, 4.1f, 4.1f); //Reset boss icon scale
		player.transform.localScale = new Vector3(99.2f, 99.2f, 99.2f); //Reset player icon scale
		foreach(GameObject go in shops) //reset all shop icons scale
			go.transform.localScale = new Vector3 (10.55f, 10.55f, 10.55f);
		foreach(GameObject go in diamonds) //Reset all treasure planet icon scale
			go.transform.localScale = new Vector3 (14.4f, 14.4f, 14.4f);
		foreach(GameObject go in moonIcons) //rename all moon icons
			go.transform.name = go.transform.name + "_icon";
	}

	// Update is called once per frame
	void Update ()
	{
		if(isMinimap) //This is a minimap
			this.transform.position = new Vector3(player.transform.position.x, yPos, player.transform.position.z); //Camera's position is equal to that of the player
	}
}
