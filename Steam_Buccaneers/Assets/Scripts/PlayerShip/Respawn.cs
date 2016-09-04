using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Respawn : MonoBehaviour 
{
	//Set start radius to look for shop
	float shopToPlayerDistance= 100000;
	//tempindex for closest shop
	int tempI;
	//distance shopt to player
	private float distance;
	//Array for all shops in radius givven over
	public GameObject[] shops;
	//Array for bombs from bossbattle that is needed to delete if player looses bossbattle
	private GameObject[] bombs;
	//Reference for playership
	private GameObject player;
	//Plane that shows up når spiller dør
	public GameObject deathScreen;
	//Bool that tells if screen is showing
	private bool showDeathScreen = false;

	//dialog stuff
	//Tells us which dialog we are at
	private int deathTalkNumber = 0;
	//Arrays with characternames and dialog
	private string[] characters = new string[2];
	private string[] deathDialog = new string[5];
	//Button to continue dialog
	private GameObject nextButton;
	//References charactername textbox
	private Text characterName;
	//Refereces dialogtextbox
	private Text dialogTextBox;
	//characters color in hexcode
	private string textColorPlayer;
	private string textColorShopkeeper;	
	//Position for names
	private Vector2 nameLeftPos;
	private Vector2 nameRightPos;
	//variable that holds current textcolor
	private Color tempColor;
	//Parent of all dialog elements
	public GameObject dialogGui;
	//the characterportretts
	public GameObject portrett1;
	public GameObject portrett2;
	//Timer which counts how long after death deathscreen shall appear
	private float timer;
	//Selfexplainatory bools
	private bool isDead = false;
	private bool isPaused = false;


	void Start () 
	{
		//find player 
		player = GameObject.Find("PlayerShip");
		//Deactivate deathscreen
		deathScreen.SetActive (showDeathScreen);
	}

	void Update () 
	{
		//Checking in every frame if player is dead
		if (GameControl.control.health <= 0)
		{
			//If isDead is false it means that player just died
			if (isDead == false)
			{
				//Turn of all motion from player and turn of combat. 
				//Enable deadPlayer script
				PlayerMove.turnLeft = false;
				PlayerMove.turnRight = false;
				GameControl.control.isFighting = false;
				player.GetComponent<DeadPlayer>().enabled = true;
				player.GetComponent<DeadPlayer>().killPlayer();
	
				isDead = true;
			} 
			else if (isDead == true)
			{
				//Three second wait after player dies to show of explosion and flames.
				//Just so player understands what happened.
				if (timer <= 3)
				{
					timer += Time.deltaTime;
				} 
				else
				{
					if (isPaused == false)
					{
						//Pauses game, shows deathscreen and plays deathsong
						GameObject.Find ("GameControl").GetComponent<GameButtons> ().pause ();
						isPaused = true;
						showDeathScreen = true;
						deathScreen.SetActive (showDeathScreen);
						GameObject.Find("CameraChild").GetComponent<BackgroundSongsController>().playDeadSong();
					}
				}
			}
			//Player presses space to respawn
			if (Input.GetKey(KeyCode.Space))
			{
				//If player pressed space before deathscreen is pauses game
				if (isPaused == false)
				{
					GameObject.Find ("GameControl").GetComponent<GameButtons> ().pause ();
					isPaused = true;
				}
				//Spiller repawner og deathscreen forsvinner
				//Sang stanses og vi stopper player
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
		//Finds all shops in game
		shops = GameObject.FindGameObjectsWithTag ("shop"); 
		//Goes through array to find closest
		for (int i = 0; i < shops.Length; i++)
		{
			distance = Vector3.Distance(shops[i].transform.position, player.transform.position);
			if (distance < shopToPlayerDistance)
			{
				shopToPlayerDistance= distance;
				tempI = i;
			}
		}
		//Just to make sure, tells combatanimationcontroller that it is not in combat with boss
		GameObject.Find ("dialogue_elements").GetComponent<CombatAnimationController> ().combatBoss = false;
		//Finds all bombs in games and delets them
		bombs = GameObject.FindGameObjectsWithTag("bomb");
		foreach(GameObject go in bombs)
			Destroy(go);

		//Player coor is the nearest shop -100 i z
		Vector3 spawnCoord = new Vector3 (shops[tempI].transform.position.x,0,shops[tempI].transform.position.z - 100);
		//Rotates ship around to point towards shop
		player.transform.localEulerAngles = new Vector3(0,0,0);
		player.transform.position = spawnCoord;
		//Sets contraints after DeadPlayer script turns them off
		player.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
		//Player looses money after dying
		GameControl.control.money -= (GameControl.control.money*10)/100;
		//Resets health
		GameControl.control.health = 100;
		//changes material to complete
		player.GetComponentInChildren<ChangeMaterial> ().checkPlayerHealth();
		//Updates money GUI
		GameObject.Find ("value_scraps_tab").GetComponent<Text> ().text = GameControl.control.money.ToString();

		//If it first death shopkeeper dialog is showing
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
			//Unpause game and resett bools
			GameObject.Find ("GameControl").GetComponent<GameButtons> ().pause();
			isPaused = false;
			isDead = false;
		}
		
	}

	private void setDeathData ()
	{
		//Player is dead for the first time and shopkeeper is going to teach him what happens
		//For loops shows all dialog elements
		for (int i = 0; i < dialogGui.transform.childCount; i++)
		{
			dialogGui.transform.GetChild (i).gameObject.SetActive (true);
		}

		for (int i = 0; i < portrett2.transform.childCount; i++)
		{
			portrett2.transform.GetChild (i).gameObject.SetActive (true);
		}

		portrett2.transform.GetChild (1).transform.GetChild (2).gameObject.SetActive (true);

		//turns of marine and boss animations if they exist
		if (GameObject.Find ("Portrett2_marine"))
		{
			GameObject.Find ("Portrett2_marine").SetActive (false);
		}
			if (GameObject.Find ("Portrett2_boss"))
		{
			GameObject.Find ("Portrett2_boss").SetActive (false);
		}

		//Finds all dialog elements and give refences values
		dialogTextBox = GameObject.Find ("dialogue_ingame").GetComponent<Text> ();
		characterName = GameObject.Find ("dialogue_name").GetComponent<Text> ();
		//Nextbutton executes nextDialogDeath() when clicked
		GameObject.Find ("dialogue_next").GetComponent<Button> ().onClick.AddListener (nextDialogDeath);
		//Sets nametext pos
		nameLeftPos = new Vector3(215.0f, -25.0f);
		nameRightPos = new Vector3 (615.0f, -25.0f);
		//Sets color on text after which character is tlaking
		textColorPlayer = "#173E3CFF";
		textColorShopkeeper = "#631911FF";
		//Character names
		characters [0] = "Shopkeeper";
		characters [1] = "Player";
		//Dialogs
		deathDialog [0] = "So, you decided to get yourself killed? Luckily I was there to help you out! I've fixed the ship for you, so you can be on your way!";
		deathDialog [1] = "Oh thank you, that's actually really nice! Wait, will I have to pay for this?";
		deathDialog [2] = "Ofcause! It's already taken care of, no worries!";
	}

	private void teachDeath(int dialog)
	{		
		//If it is dialog nr 1 it is player who talks. 
		//Text color and position is set
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
		//if it is not dialog nr 1 it is shopkeeper.
		//text color and pos is set according
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
		//Actually sets text color
		characterName.color = tempColor;
		dialogTextBox.color = tempColor;

		//Stuff that is going to happen at each dialog
		if (dialog == 3)
		{
			//Dialog 3 makes player angry and the dialog is going to be previous dialog
			GameObject.Find ("Portrett").GetComponent<Animator> ().SetBool ("isAngryMainCharacter", true);
			setDialog (characters [0], deathDialog [dialog-1]);
		} 
		else if (dialog == 4)
		{
			//Talk is over. Pack everything down
			//Turns off all dialog element
			for (int i = 0; i < dialogGui.transform.childCount; i++)
			{
				dialogGui.transform.GetChild (i).gameObject.SetActive (false);
			}
			//Unpause game
			GameObject.Find ("GameControl").GetComponent<GameButtons> ().pause();
			//Sets bools according
			isPaused = false;
			isDead = false;
			GameControl.control.firstDeath = true;
		}
	}

	public void setDialog (string character, string text)
	{
		//Function to help set text
		characterName.text = character;
		dialogTextBox.text = text;
	}

	public void nextDialogDeath()
	{
		//fucntion to contiue dialog
		deathTalkNumber++;
		teachDeath (deathTalkNumber);
	}

}
