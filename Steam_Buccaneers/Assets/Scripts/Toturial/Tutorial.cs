using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
	//All text
	private Text dialogTextBox;
	private Text characterName;
	//Next button
	public GameObject nextButton;
	//Number that keeps track of progress
	public int dialogNumber;
	//Holds gameobjects that holds scripts that is making shooting possible
	public GameObject[] shootyThings;
	//Text that says pause
	public GameObject pauseText;
	//Array that hold the tutorial dialog
	private string[] dialogTexts = new string[45];
	//Holds quest information
	private Text questInfo;
	//Character names
	private string[] character = new string[3];
	private Vector2 nameLeftPos;
	private Vector2 nameRightPos;
	private Vector2 nameShop;
	private string textColorPlayer;
	private string textColorShopkeeper;
	private string textColorMarine;
	private Color tempColor;
	//getting ahold of button functions
	private GameButtons buttonEvents;
	//AIship used to battle
	public GameObject AI;
	//Character vinduer
	public GameObject shopKeeperCharacterWindow;
	public GameObject marineCharacterWindow; 
	private GameObject bossCharacterWindow;
	//dialog deler
	private GameObject talkBubble;
	private GameObject avatarWindow;
	private GameObject avatarWindow2;
	//Controls that outside actions are over
	public bool enterStore = false;
	//Compass
	private PointTowards compass;
	//Array that holds all scrap
	public GameObject[] scrapHolder;
	private int scrapCount;
	//Array that checks if controllbuttons are pressed
	public bool[] wadCheck = new bool[3];
	//Array that checks if fire-buttons are pressed
	public bool[] qeCheck = new bool[2];
	//Array taht checks if mouse1 is clicked
	public bool mouse1Check;

	//Everything with blinkingbuttons v
	//Array that holds shopbuttons that shall blink
	private Button[] blinkingButtons = new Button[8];
	private ColorBlock cb;
	private bool goingDown = true;
	private float blinkingButtonSpeed;

	private GameObject[] shopButtons = new GameObject[13];

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
	}

	void OnLevelWasLoaded(int level)
	{
		if (SceneManager.GetActiveScene ().name == "Shop")
		{
			loadShop ();
		} 
		else if (SceneManager.GetActiveScene ().name == "WorldMaster")
		{
			loadWorldMaster ();
		}
	}

	private void loadShop()
	{
		//Reassign textboxes in shop
		dialogTextBox = GameObject.Find ("dialogue_ingame_shop").GetComponent<Text> ();
		characterName = GameObject.Find ("dialogue_name_shop").GetComponent<Text> ();

		blinkingButtons [0] = GameObject.Find ("thruster").GetComponent<Button> ();
		blinkingButtons [1] = GameObject.Find ("cannonT1").GetComponent<Button> ();
		blinkingButtons [2] = GameObject.Find ("cannonT2").GetComponent<Button> ();
		blinkingButtons [3] = GameObject.Find ("cannonT3").GetComponent<Button> ();
		blinkingButtons [4] = GameObject.Find ("cannonB1").GetComponent<Button> ();
		blinkingButtons [5] = GameObject.Find ("cannonB2").GetComponent<Button> ();
		blinkingButtons [6] = GameObject.Find ("cannonB3").GetComponent<Button> ();
		blinkingButtons [7] = GameObject.Find ("hull").GetComponent<Button> ();

		//Turning off button scripts in shop
		shopButtons = GameObject.FindGameObjectsWithTag ("button");
		changeButtonInteractivity (false);


		Debug.Log ("Store Scene");
		GameObject.Find ("dialogue_next_shop").GetComponent<Button> ().onClick.AddListener (nextDialog);
		nextButton = GameObject.Find ("dialogue_next_shop");
		nextDialog ();
	}

	private void loadWorldMaster()
	{
		dialogTextBox = GameObject.Find ("dialogue_ingame").GetComponent<Text> ();
		characterName = GameObject.Find ("dialogue_name").GetComponent<Text> ();
		questInfo = GameObject.Find ("quest_info_text").GetComponent<Text> ();
		pauseText = GameObject.Find ("pause");
		buttonEvents = GameObject.Find ("GameControl").GetComponent<GameButtons> ();

		marineCharacterWindow = GameObject.Find ("Portrett2_marine");
		shopKeeperCharacterWindow = GameObject.Find ("Portrett2_shopkeeper");
		bossCharacterWindow = GameObject.Find ("Portrett2_boss");

		nextButton = GameObject.Find ("dialogue_next");
		nextButton.GetComponent<Button> ().onClick.AddListener (nextDialog);

		talkBubble = GameObject.Find ("bubble_ingame");
		avatarWindow = GameObject.Find ("avatar1");
		avatarWindow2 = GameObject.Find ("avatar2");

		buttonEvents.pause ();
	}

	void Start ()
	{
		Debug.Log ("Start tutorial");
		//marineCharacterWindow.SetActive (false);
		//Initialize functions
		dialogTextBox = GameObject.Find ("dialogue_ingame").GetComponent<Text> ();
		characterName = GameObject.Find ("dialogue_name").GetComponent<Text> ();
		questInfo = GameObject.Find ("quest_info_text").GetComponent<Text> ();
		buttonEvents = GameObject.Find ("GameControl").GetComponent<GameButtons> ();
		compass = GameObject.Find ("compass_needle").GetComponent<PointTowards> ();

		nameLeftPos = new Vector3(217.0f, -25.0f);
		nameRightPos = new Vector3 (612.0f, -25.0f);

		nameShop = new Vector3 (215.4f, 73.2f);

		textColorPlayer = "#173E3CFF";
		textColorShopkeeper = "#631911FF";
		textColorMarine = "#323A46FF";

		wadCheck [0] = false;
		wadCheck [1] = false;
		wadCheck [2] = false;

		qeCheck [0] = false;
		qeCheck [1] = false;

		//Pause at startup
		buttonEvents.pause ();
		//Turn off shooting
		shootingAllowed (false, "NULL");
		//Fill arrays with text
		dialogInArray ();
		//set quest info
		questInfo.text = "Talk to Shopkeeper.";
		//Trigger dialog here
		dialog (0);
		//The speed the buttons blink at
		blinkingButtonSpeed = 0.025f;
	}

	void Update()
	{
		Debug.Log (dialogNumber);

		if (dialogNumber >= 31 && dialogNumber <= 36)
		{
			for (int i = 0; i < blinkingButtons.Length; i++)
			{
				Debug.Log (blinkingButtons[i]);
				if (blinkingButtons [i] != null)
				{
					cb = blinkingButtons [i].colors;
					Debug.Log (blinkingButtons [i].colors.normalColor.a);
					if (blinkingButtons [i].colors.normalColor.a >= 1)
					{
						goingDown = true;
					} 
					else if (blinkingButtons [i].colors.normalColor.a <= 0.25)
					{
						goingDown = false;
					}

					if (goingDown == true)
					{
						cb.normalColor = new Color (cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, cb.normalColor.a - blinkingButtonSpeed);
					} 
					else
					{
						cb.normalColor = new Color (cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, cb.normalColor.a + blinkingButtonSpeed);
					}
					cb.pressedColor = cb.normalColor;
					cb.highlightedColor = cb.normalColor;
					blinkingButtons [i].colors = cb;
				}
			}
			if (SceneManager.GetActiveScene ().name == "WorldMaster")
			{
				talkBubble.SetActive (true);
				avatarWindow.SetActive (true);
				avatarWindow2.SetActive (true);
				nextDialog ();
			}
		} 
	}

	private void dialogInArray()
	{
		character[0] = "Shopkeeper";
		character[1] = "Player";
		character[2] = "Marine";

		dialogTexts[0] = "Hey, congratulations on acquiring this beauty from yours truly.";//Shopkeeper
		dialogTexts [1] = "What now, I’ve already bought the ship, what more could you possibly want from me?";//Player
		dialogTexts [2] = "Well, I have the feeling you haven’t really flown one of these things before, am I right? "; //Shopkeeper
		dialogTexts[3] = "So you have two options, either figuring out the controls for yourself, or let me help you."; 
		dialogTexts[4] = "And at the moment, I can see one of them space cop types coming towards us on the radar. " +
			"So I guess you don’t want to get shot into bits in an instant here."; //Shopkeeper
		dialogTexts[5] = "Why would they bother me? But alright, just tell me the basics and let me be on my way.";//Player
		dialogTexts [6] = "Well, that beautiful hunk of metal you’ve just bought ain’t exactly acquired legally, you see, and the previous" +
			" captain who, incidentally, is “missing”, wasn’t the nicest of creatures in this here space. ";//Shopkeeper
		dialogTexts [7] = "Also you’re human, aren’t you? So add them together and you know them cops aren’t going to give you an easy time.";//Shopkeeper
		dialogTexts [8] = "But anyways. The controls are simple, just press “W” to move your ship forward. “A” and “D” turns you around. " +
			"Just remember you can’t drive backwards in this thing, since it ain’t got no thrusters in the front.";//Shopkeeper
		dialogTexts [9] = "You can push the engine into overdrive by pressing “Shift.” While in combat, your're using a lot of power, " +
			"so you can’t boost continously. Out of combat you should be fine. This deals more ramming damage to enemies. Try the controls now.";//Shopkeeper
		dialogTexts[10] = "Wow, is that it? And I couldn’t figure this out by myself how? Would’ve been my first guess anyway.";//Player
		dialogTexts [11] = "Easy peasy. Now, you see you have some guns on both sides of your ship there? You can fire the left side by pressing “Q” and the right side by pressing “E”.";//Shopkeeper
		dialogTexts[12] = "Both yourself and bullets can be affected by gravity. So your bullets might not go where you want them to go, if you are to close to a planet." +
			" You’ve got infinite ammo for these, thanks to one of them fancy machines on board.";//Shopkeeper
		dialogTexts[13] = "Just fire a couple of volleys from either side. You got enough ammo for them to last a lifetime, " +
			"so don’t worry about wasting them.";//Shopkeeper
		dialogTexts[14] = "Just remember now - those cannons hanging on the side of the ship will rotate with the ship while you turn. So you might not hit your targets even though they are right next to you.\n";//Shopkeeper
		dialogTexts[15] = "Gee, Einstein, I could’ve figured that out on my own.";//Player
		dialogTexts [16] = "Hey, keep your sassy human slang for your kin, most won’t take kindly to that sort of language! "; //Shopkeeper
		dialogTexts [17] = "Anyways, last but not least; the cannon on your roof right there is a special one. ";
		dialogTexts[18] = "It is way more powerful than your regular cannons, and you can aim it around using your mouse." +
			" You fire by holding the right mouse button down and clicking the left mouse button. Test it out.";//Shopkeeper
		dialogTexts[19] = "Remember, it is a way more accurate and powerful way to take down your opponents, " +
			"but this doesn’t have a lifetime worth of ammo supply. But don’t worry, just visit a shop every " +
			"once in awhile to stock up.";//Shopkeeper
		dialogTexts[20] = "Thanks for telling after I’ve fired a couple, asshole!";//Player
		dialogTexts[21] = "Hey, I am an entrepreneur aren’t I? Need to make money some way or another. " +
			"Tell you what, I’ll reimburse you for the ones you’ve fired, alright?";//Shopkeeper
		dialogTexts[22] = "Oh would you look at that, looks like we’re done just in time for " +
			"them coppers to arrive. Now test out what I’ve just taught you.";//Shopkeeper
		dialogTexts[23] = "Stop right there criminal scum! You are wanted for peddling illegal" +
			" goods and scavenged ships. And you, pirate! Stay there and we’ll deal with you later!";//Marine
		dialogTexts[24] = "What do you say, pirate? I’ll fix up your ship for free if you help me " +
			"out here. Seems like you’ve got nothing to lose.";//Shopkeeper
		dialogTexts[25] = "If it gets you off my back, fine.";//Player
		dialogTexts [26] = "Nicely done there, humie. Might make a fine pirate of you one day. " +
			"You see those gears and other bits and bobs that’s left after that marine ship? " +
			"This is what us traders take as payment.";
		dialogTexts[27] = "Fly over there, and pick it up.";//Shopkeeper
		dialogTexts[28] = "  And as promised I’ll fix your damage, just fly over that landing pad marked with the big red X.";//Shopkeeper
		dialogTexts[29] = "Here is what I, and most other shops around this star has to offer.";//Shopkeeper
		dialogTexts[30] = "You see all these icons on the blueprint of your ship here?";
		dialogTexts [31] = "These are upgradeable parts. I can do upgrades for each of your side cannons, " +
			"or I could increase the damage your hull can take before you’re blown to bits, " +
			"or your back thruster output. ";
		dialogTexts[32] = "The icon in the middle is ammunition for your special weapon." +
			"Confirm your purchase on the bottom of the screen. Click untill you got 20.";//Shopkeeper
		dialogTexts [33] = "As you've might have noticed if you got hit during your fight, there is no form " +
			"of health bar displayed on you HUD. You’ll see when your ship starts to smoke and burn, then it’s time to get to a repair.";
		dialogTexts [34] = "To repair your ship, just press the button that says “Repair my vessel!”" +
			" and drag the slider for how much you want to repair.";//Shopkeeper
		dialogTexts[35] = "Then just confirm the amount, and I’ll fix it right away. And of course, " +
			"to the top left you’ll see how much scraps you’ve got.";//Shopkeeper
		dialogTexts[36] = "I guess that’s about it for what I can tell you. Be on your way now, and remember; " +
			"Keep alive and keep flying! A living customer is a paying customer. " +
			"Exit by pressing the X in the top right corner.";//Shopkeeper

		dialogTexts [37] = "Oh, and one more thing. You are never far from one of my shops, just follow " +
			"the funky music and you will get there!";//Shopkeeper
		dialogTexts [38] = "...Right. By the way, where is the first pirate lord? " +
			"I want to get this done with!";//Player
		dialogTexts [39] = "The coordinates are already installed, just follow the compass to your left. You can also press “M” to see the map.";
		dialogTexts[40] ="You know, you could lose that attitude, I don't think Sir Pincushion will appreciate it. " +
			"He is quite posh from what I've heard.";//Shopkeeper
		dialogTexts [41] = "Sir Pincushion? Seriously?!?!?";//Player
		dialogTexts [42] = "Yes, yes, he is a pirate lord and will be your first challenge. " +
			"Now shut it and get going!";//Shopkeeper
		dialogTexts [43] = "...";//Player

	}



	public void dialog (int stage)
	{
		//Player dialog: 1, 5, 10, 15, 20, 25, 38, 40, 42
		//Marine dialog: 20
		//Dialog runs here
		if (stage == 1 || stage == 5 || stage == 10 || stage == 15 || stage == 20 || stage == 25|| stage == 38 || stage == 41 || stage == 43)
		{
			//Sets dialog and character
			setDialog (character [1], dialogTexts [stage]);
			//Moves name closer to portrait
			if (characterName.gameObject.GetComponent<RectTransform>().anchoredPosition != nameLeftPos)
			{
				characterName.gameObject.GetComponent<RectTransform>().anchoredPosition = nameLeftPos;
			}
			//Changes color on text which is closer to character
			ColorUtility.TryParseHtmlString (textColorPlayer, out tempColor);
		} 
		else if (stage == 23)
		{
			//Sets dialog and character
			setDialog (character [2], dialogTexts [stage]);
			//Moves name closer to portrait
			if (characterName.gameObject.GetComponent<RectTransform>().anchoredPosition != nameRightPos)
			{
				characterName.gameObject.GetComponent<RectTransform>().anchoredPosition = nameRightPos;
			}
			//Changes color on text which is closer to character
			ColorUtility.TryParseHtmlString (textColorMarine, out tempColor);
		} 
		else
		{
			if (stage == 33 && GameControl.control.health == 100)
			{
				setDialog (character [0], "It seems like you havent got a scratch! Good for you! Next time you come back with broken ship you can repair your ship here. Click “V” to continue.");
			}
			else
			{
				//Sets dialog and character
				setDialog (character [0], dialogTexts [stage]);
			}

			//Moves name closer to portrait
			if (SceneManager.GetActiveScene ().name != "Shop")
			{
				if (characterName.gameObject.GetComponent<RectTransform> ().anchoredPosition != nameRightPos)
				{
					characterName.gameObject.GetComponent<RectTransform> ().anchoredPosition = nameRightPos;
				}
			} 
			else
			{
				if (characterName.gameObject.GetComponent<RectTransform> ().anchoredPosition != nameShop)
				{
					characterName.gameObject.GetComponent<RectTransform> ().anchoredPosition = nameShop;
				}
			}
			//Changes color on text which is closer to character
			ColorUtility.TryParseHtmlString (textColorShopkeeper, out tempColor);
		}

		characterName.color = tempColor;
		dialogTextBox.color = tempColor;

		//Pause, activate guns and other stuff here
		switch (stage) 
		{

		case(9):
			buttonEvents.pause ();
			pauseText.SetActive (false);
			questInfo.text = "Move using W A D.";
			nextButton.SetActive (false);
			break;
		case(10):
			buttonEvents.pause ();
			pauseText.SetActive (true);
			questInfo.text = "Talk to Shopkeeper.";
			break;
		case(13):
			buttonEvents.pause ();
			pauseText.SetActive (false);
			nextButton.SetActive (false);
			shootingAllowed (true, "GameObject");
			questInfo.text = "Fire sidcanons using Q and E.";
			break;
		case(14):
			buttonEvents.pause ();
			pauseText.SetActive (true);
			shootingAllowed (true, "NULL");
			questInfo.text = "Talk to Shopkeeper.";
			break;
		case(18):
			buttonEvents.pause ();
			pauseText.SetActive (false);
			questInfo.text = "Fire special weapon using Mouse2, then Mouse1.";
			nextButton.SetActive (false);
			break;
		case(19):
			buttonEvents.pause ();
			pauseText.SetActive (true);
			questInfo.text = "Talk to Shopkeeper.";
			break;
		case(20):
			GameObject.Find ("Portrett").GetComponent<Animator> ().SetBool ("isAngryMainCharacter", true);
			Debug.Log ("Change mood");
			break;
		case(21):
			GameObject.Find ("Portrett2_shopkeeper").GetComponent<Animator> ().SetBool ("isHappyShopkeeper", true);
			Debug.Log ("Change mood");
			break;
		case(23):
			makeTutorialEnemy ();
			changeCharacterWindow ();
			break;
		case(24):
			changeCharacterWindow ();
			break;
		case(25):
			changeCharacterWindow ();
			buttonEvents.pause ();
			questInfo.text = "Destroy Marineship.";
			compass.goTarget = GameObject.FindGameObjectWithTag("aiShip");
			pauseText.SetActive (false);
			nextButton.SetActive (false);
			break;
		case(26):
			changeCharacterWindow ();
			scrapHolder = (GameObject.FindGameObjectsWithTag ("scrap"));
			scrapCount = scrapHolder.Length;
			Debug.Log ("Scraps left to pick up: " + scrapCount);
			buttonEvents.pause ();
			pauseText.SetActive (true);
			nextButton.SetActive (true);
			break;
		case(27):
			buttonEvents.pause ();
			questInfo.text = "Pickup loot.";
			pauseText.SetActive (false);
			nextButton.SetActive (false);
			break;
		case(28):
			compass.goTarget = GameObject.Find ("shop1");
			questInfo.text = "Enter store.";
			enterStore = true;
			break;
		case(32):
			clearButtons ();
			blinkingButtons [0] = GameObject.Find ("special").GetComponent<Button> ();
			blinkingButtons [0].enabled = true;
			GameObject.Find ("button_yes").GetComponent<Button> ().enabled = true;
			nextButton.SetActive (false);
			break;
		case(33):
			clearButtons ();
			changeButtonInteractivity (false);
			nextButton.SetActive (true);
			break;
		case(34):
			nextButton.SetActive (false);
			blinkingButtons [0] = GameObject.Find ("button_repair").GetComponent<Button> ();
			blinkingButtons [0].enabled = true;
			break;
		case(35):
			clearButtons ();
			blinkingButtons [0] = GameObject.Find ("button_v").GetComponent<Button> ();
			nextButton.SetActive (false);
			GameObject.Find ("button_v").GetComponent<Button> ().enabled = true;
			GameObject.Find ("button_x").GetComponent<Button> ().enabled = true;
			changeButtonInteractivity (true);
			GameObject.Find ("ExitButton").GetComponent<Button> ().enabled = false;
			break;
		case(36):
			clearButtons ();
			GameObject.Find ("button_v").GetComponent<Button> ().enabled = false;
			GameObject.Find ("ExitButton").GetComponent<Button> ().enabled = true;
			blinkingButtons [0] = GameObject.Find ("ExitButton").GetComponent<Button> ();
			break;
		case(37):
			changeCharacterWindow ();
			bossCharacterWindow.SetActive (false);
			questInfo.text = "Talk to shopkeeper.";
			pauseText.SetActive (true);
			break;
		case(44):
			questInfo.text = "Find ancient cog!";
			talkBubble.SetActive (false);
			avatarWindow.SetActive (false);
			avatarWindow2.SetActive (false);
			pauseText.SetActive (false);
			buttonEvents.pause ();
			Destroy (this.gameObject);
			break;
		default:
			break;
		}

	}

	public void nextDialog ()
	{
		dialogNumber++;
		Debug.Log ("NextDialog " + dialogNumber);
		dialog (dialogNumber);
	}

	public void setDialog (string character, string text)
	{
		characterName.text = character;
		dialogTextBox.text = text;
	}

	private void shootingAllowed (bool status, string exception)
	{
		for (int i = 0; i < shootyThings.Length; i++) 
		{
			if (shootyThings [i].name != exception)
			{
				shootyThings [i].SetActive (status);
			}
		}
	}

	private Vector3 marineSpawnpoint()
	{
		bool foundSpawn = false;
		Vector3 testerSpawnPos = new Vector3(0, 0, 0);

		while(foundSpawn == false)
		{
			//Create random numbers between 100 and 200
			float tempPosX = Random.Range(50, 100); //Random x position
			float tempPosZ = Random.Range(50f, 100); //andom z position
			float posX;
			float posZ;
			//Creates a random variable from 1 to 10 (the last number is not included, aka 11).
			//Use this number to determine if the variable should be positive or negative, just
			//to create som variation in the spawnpositions of the AI.
			float ranRangeX = Random.Range(1, 11);
			if(ranRangeX > 5)
			{
				posX = tempPosX;
			}
			else posX = -tempPosX;

			//Does the same to the Z position of the AI as we did with the X position just above this.
			float ranRangeZ = Random.Range(1, 11);
			if(ranRangeZ > 5)
			{
				posZ = tempPosZ;
			}
			else posZ = -tempPosZ;

			testerSpawnPos = new Vector3 (posX+shootyThings[1].transform.position.x, 2, posZ+shootyThings[1].transform.position.z);

			Collider[] colliders = Physics.OverlapSphere(testerSpawnPos, 10);
			if(colliders.Length == 0)
				foundSpawn = true; //Sets the position of the Marine relative to the player position
		}

		return testerSpawnPos;
	}

	private void makeTutorialEnemy()
	{
		GameObject temp = Instantiate (AI);
		temp.transform.position = marineSpawnpoint();
		GameControl.control.isFighting = true;
		Debug.Log("New Target = " + AI);
	}

	private void changeCharacterWindow()
	{
		Debug.Log (marineCharacterWindow + " marine");
		Debug.Log (shopKeeperCharacterWindow + " shopkeeper");
		if (marineCharacterWindow.activeInHierarchy == false)
		{
			marineCharacterWindow.SetActive (true);
			shopKeeperCharacterWindow.SetActive (false);
		} 
		else
		{
			shopKeeperCharacterWindow.SetActive (true);
			marineCharacterWindow.SetActive (false);
		}
	}

	public void countingDownScrap()
	{

		scrapCount--;
		Debug.Log ("Scraps left to pick up: " + scrapCount);

		if (scrapCount <= 0)
		{
			nextDialog ();
		}
	}

	private void clearButtons()
	{
		for (int i = 0; i < blinkingButtons.Length; i++)
		{
			if (blinkingButtons [i] != null)
			{
				cb.normalColor = Color.white;
				blinkingButtons [i].colors = cb;
			}
		}
		System.Array.Clear (blinkingButtons, 0, blinkingButtons.Length);
	}

	private void changeButtonInteractivity(bool possibleInteraction)
	{
		foreach (GameObject i in shopButtons)
		{
			i.GetComponent<Button> ().enabled = possibleInteraction;
		}
	}

	public void checkArray(bool[] array)
	{

		int k = 0;

		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == true)
			{
				k++;
			}
		}

		if (k == array.Length)
		{
			nextButton.SetActive (true);
		}
	}

}
