using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Respawn : MonoBehaviour 
{
	float temp = 100000;
	int tempI;
	private float distance;
	public GameObject[] shops;
	private GameObject[] bombs;
	private GameObject player;
	public GameObject deathScreen;
	private bool showDeathScreen = false;
	//float respawnTime = 5;

	//dialog stuff
	private int deathTalkNumber = 0;
	private string[] characters = new string[2];
	private string[] deathDialog = new string[5];
	private GameObject nextButton;
	private Text characterName;
	private Text dialogTextBox;
	private string textColorPlayer;
	private string textColorShopkeeper;	
	private Vector2 nameLeftPos;
	private Vector2 nameRightPos;
	private Color tempColor;
	public GameObject dialogGui;
	public GameObject portrett1;
	public GameObject portrett2;

	private float timer;

	private bool isDead = false;
	private bool isPaused = false;


	void Start () 
	{
		player = GameObject.Find("PlayerShip");
		deathScreen.SetActive (showDeathScreen);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (GameControl.control.health);
		//GameControl.control.health --;
		if (GameControl.control.health <= 0)
		{
			if (isDead == false)
			{
				PlayerMove.turnLeft = false;
				PlayerMove.turnRight = false;
				GameControl.control.isFighting = false;
				player.GetComponent<DeadPlayer>().enabled = true;
				player.GetComponent<DeadPlayer>().killPlayer();
	
				isDead = true;
			} 
			else if (isDead == true)
			{
				if (timer <= 3)
				{
					timer += Time.deltaTime;
				} 
				else
				{
					if (isPaused == false)
					{
						GameObject.Find ("GameControl").GetComponent<GameButtons> ().pause ();
						isPaused = true;
						showDeathScreen = true;
						deathScreen.SetActive (showDeathScreen);
						GameObject.Find("CameraChild").GetComponent<BackgroundSongsController>().playDeadSong();
					}
				}
			}
			if (Input.GetKey(KeyCode.Space))
			{
				if (isPaused == false)
				{
					GameObject.Find ("GameControl").GetComponent<GameButtons> ().pause ();
					isPaused = true;
				}
				RespawnPlayer();
				showDeathScreen = false;
				deathScreen.SetActive (showDeathScreen);
				player.GetComponent<Rigidbody> ().velocity = Vector3.zero;
				player.GetComponent<DeadPlayer> ().enabled = false;
				GameObject.Find("CameraChild").GetComponent<BackgroundSongsController>().stopDeadSong();
				timer = 0;
			}
			player.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			Debug.Log ("Is game paused: " + isPaused);
		}
	}

	void RespawnPlayer()
	{
		shops = GameObject.FindGameObjectsWithTag ("shop"); 
		for (int i = 0; i < shops.Length; i++)
		{
			distance = Vector3.Distance(shops[i].transform.position, player.transform.position);
			if (distance < temp)
			{
				temp = distance;
				tempI = i;
			}

		}

		bombs = GameObject.FindGameObjectsWithTag("bomb");
		foreach(GameObject go in bombs)
			Destroy(go);

		Vector3 spawnCoord = new Vector3 (shops[tempI].transform.position.x,shops[tempI].transform.position.y,shops[tempI].transform.position.z - 100);
		player.transform.localEulerAngles = new Vector3(0,0,0);
		player.transform.position = spawnCoord;
		player.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
		GameControl.control.money -= (GameControl.control.money*10)/100;
		GameControl.control.health = 100;
		player.GetComponentInChildren<ChangeMaterial> ().checkPlayerHealth();

		if (GameControl.control.firstDeath == false)
		{
			dialogGui.SetActive (true);
			portrett1.SetActive (true);
			portrett2.SetActive (true);
			setDeathData ();
			teachDeath (0);
		} 
		else
		{
			GameObject.Find ("GameControl").GetComponent<GameButtons> ().pause();
			isPaused = false;
			isDead = false;
		}
		
	}

	private void setDeathData ()
	{

		for (int i = 0; i < dialogGui.transform.childCount; i++)
		{
			dialogGui.transform.GetChild (i).gameObject.SetActive (true);
		}

		for (int i = 0; i < portrett2.transform.childCount; i++)
		{
			portrett2.transform.GetChild (i).gameObject.SetActive (true);
		}

		portrett2.transform.GetChild (1).transform.GetChild (2).gameObject.SetActive (true);
		if (GameObject.Find ("Portrett2_marine"))
		{
			GameObject.Find ("Portrett2_marine").SetActive (false);
		}
			if (GameObject.Find ("Portrett2_boss"))
		{
			GameObject.Find ("Portrett2_boss").SetActive (false);
		}

		dialogTextBox = GameObject.Find ("dialogue_ingame").GetComponent<Text> ();
		characterName = GameObject.Find ("dialogue_name").GetComponent<Text> ();
		GameObject.Find ("dialogue_next").GetComponent<Button> ().onClick.AddListener (nextDialogDeath);
		nameLeftPos = new Vector3(215.0f, -25.0f);
		nameRightPos = new Vector3 (615.0f, -25.0f);

		textColorPlayer = "#173E3CFF";
		textColorShopkeeper = "#631911FF";

		characters [0] = "Shopkeeper";
		characters [1] = "Player";

		deathDialog [0] = "So, you decided to get yourself killed? Luckily I was there to help you out! I've fixed the ship for you, so you can be on your way!";
		deathDialog [1] = "Oh thank you, that's actually really nice! Wait, will I have to pay for this?";
		deathDialog [2] = "Ofcause! It's already taken care of, no worries!";
	}

	private void teachDeath(int dialog)
	{		
		if (dialog == 1 )
		{
			//Sets dialog and character
			setDialog (characters[1], deathDialog [dialog]);
			//Moves name closer to portrait
			if (characterName.gameObject.GetComponent<RectTransform>().anchoredPosition != nameLeftPos)
			{
				characterName.gameObject.GetComponent<RectTransform>().anchoredPosition = nameLeftPos;
			}
			//Changes color on text which is closer to character
			ColorUtility.TryParseHtmlString (textColorPlayer, out tempColor);
		} 
		else
		{
			//Sets dialog and character
			setDialog (characters [0], deathDialog [dialog]);
			//Moves name closer to portrait
			if (characterName.gameObject.GetComponent<RectTransform>().anchoredPosition != nameRightPos)
			{
				characterName.gameObject.GetComponent<RectTransform>().anchoredPosition = nameRightPos;
			}
			//Changes color on text which is closer to character
			ColorUtility.TryParseHtmlString (textColorShopkeeper, out tempColor);
		}

		characterName.color = tempColor;
		dialogTextBox.color = tempColor;

		if (dialog == 3)
		{
			GameObject.Find ("Portrett").GetComponent<Animator> ().SetBool ("isAngryMainCharacter", true);
			setDialog (characters [0], deathDialog [dialog-1]);
		} 
		else if (dialog == 4)
		{
			for (int i = 0; i < dialogGui.transform.childCount; i++)
			{
				dialogGui.transform.GetChild (i).gameObject.SetActive (false);
			}
			GameObject.Find ("GameControl").GetComponent<GameButtons> ().pause();
			isPaused = false;
			isDead = false;
			GameControl.control.firstDeath = true;
		}
	}

	public void setDialog (string character, string text)
	{
		characterName.text = character;
		dialogTextBox.text = text;
	}

	public void nextDialogDeath()
	{
		deathTalkNumber++;
		teachDeath (deathTalkNumber);
	}

}
