using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Respawn : MonoBehaviour 
{
	float temp = 100000;
	int tempI;
	private float distance;
	public GameObject[] shops;
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
			PlayerMove2.turnLeft = false;
			PlayerMove2.turnRight = false;
			showDeathScreen = true;
			//Debug.Log("You are dead, press space to respawn at closest shop");
			deathScreen.SetActive (showDeathScreen);
			if (Input.GetKey(KeyCode.Space))
			{
				RespawnPlayer();
				showDeathScreen = false;
				deathScreen.SetActive (showDeathScreen);
				player.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			}
		
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
		Vector3 spawnCoord = new Vector3 (shops[tempI].transform.position.x,shops[tempI].transform.position.y,shops[tempI].transform.position.z - 100);
		player.transform.localEulerAngles = new Vector3(0,0,0);
		player.transform.position = spawnCoord;
		GameControl.control.money -= (GameControl.control.money*10)/100;
		GameControl.control.health = 100;

		if (GameControl.control.firstDeath == false)
		{
			GameObject.Find ("GameControl").GetComponent<gameButtons> ().pause();
			dialogGui.SetActive (true);
			portrett1.SetActive (true);
			portrett2.SetActive (true);
			setDeathData ();
			teachDeath (0);

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
		GameObject.Find ("Portrett2_marine").SetActive (false);
		if (GameObject.Find ("Portrett2_boss"))
		{
			GameObject.Find ("Portrett2_boss").SetActive (false);
		}

		dialogTextBox = GameObject.Find ("dialogue_ingame").GetComponent<Text> ();
		characterName = GameObject.Find ("dialogue_name").GetComponent<Text> ();
		GameObject.Find ("dialogue_next").GetComponent<Button> ().onClick.AddListener (nextDialogDeath);
		nextButton = GameObject.Find ("dialogue_next_shop");
		nameLeftPos = new Vector3(115.0f, -25.0f);
		nameRightPos = new Vector3 (525.0f, -25.0f);

		textColorPlayer = "#173E3CFF";
		textColorShopkeeper = "#631911FF";

		characters [0] = "Shopkeeper";
		characters [1] = "Player";

		deathDialog [0] = "So, you decided to get yourself killed? Luckily I was there to help you out! I've fixed the ship for you, so you can be on your way!";
		deathDialog [1] = "Oh thank you, that's actually really nice! Wait, will I have to pay for this!";
		deathDialog [2] = "Of cause! It's already taken care of, no worries!";
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
			GameObject.Find ("GameControl").GetComponent<gameButtons> ().pause();
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
