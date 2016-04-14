using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
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
	private string[] dialogTexts = new string[37];
	//Holds quest information
	private Text questInfo;
	//Character names
	private string[] character = new string[3];
	private Vector2 nameLeftPos;
	private Vector2 nameRightPos;
	private string textColorPlayer;
	private string textColorShopkeeper;
	private string textColorMarine;
	private Color tempColor;
	//getting ahold of button functions
	private gameButtons buttonEvents;
	//AIship used to battle
	public GameObject AI;
	//Character vinduer
	public GameObject shopKeeperCharacterWindow;
	public GameObject marineCharacterWindow; 
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
		if (SceneManager.GetActiveScene().name == "Shop")
		{
			loadShop ();
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

		nextDialog ();
		Debug.Log ("Store Scene");
		GameObject.Find ("dialogue_next_shop").GetComponent<Button> ().onClick.AddListener (nextDialog);
		nextButton = GameObject.Find ("dialogue_next_shop");
	}

	void Start ()
	{
		//marineCharacterWindow.SetActive (false);
		//Initialize functions
		dialogTextBox = GameObject.Find ("dialogue_ingame").GetComponent<Text> ();
		characterName = GameObject.Find ("dialogue_name").GetComponent<Text> ();
		questInfo = GameObject.Find ("quest_info_text").GetComponent<Text> ();
		buttonEvents = GameObject.Find ("GameControl").GetComponent<gameButtons> ();
		compass = GameObject.Find ("compass_needle").GetComponent<PointTowards> ();

		nameLeftPos = new Vector3(115.0f, -25.0f);
		nameRightPos = new Vector3 (525.0f, -25.0f);

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
		if (dialogNumber >= 31)
		{
			for ( int i = 0; i < blinkingButtons.Length; i ++)
			{
				if (blinkingButtons [i] != null)
				{
					cb = blinkingButtons [i].colors;
					if (blinkingButtons [i].colors.normalColor.a >= 1)
					{
						goingDown = true;
					} else if (blinkingButtons [i].colors.normalColor.a <= 0.25)
					{
						goingDown = false;
					}

					if (goingDown == true)
					{
						cb.normalColor = new Color (cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, cb.normalColor.a - blinkingButtonSpeed );
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
		}
	}

	private void dialogInArray()
	{
		character[0] = "Shopkeeper";
		character[1] = "Player";
		character[2] = "Marine";

		dialogTexts[0] = "Hey, congratulations on acquiring this beauty from yours truly.";//Shopkeeper
		dialogTexts [1] = "What now, I already bought the ship, what more could you possibly want from me?";//Player
		dialogTexts [2] = "Well, I have a the feeling you haven’t really flown one of these things before, am I right? "; //Shopkeeper
		dialogTexts[3] = "So you have two options, either figuring out the controls for yourself, or let me help you."; 
		dialogTexts[4] = "And at the moment, I can see one of them space cop types coming towards here on the radar. " +
			"So I guess you don’t want to get shot into bits in an instant here."; //Shopkeeper
		dialogTexts[5] = "Why would they bother me?  But alright, just tell me the basics " +
			"and let me be on my way.";//Player
		dialogTexts [6] = "Well, that beautiful hunk of metal you’ve just bought ain’t exactly acquired legally, " +
		"you see, and the previous captain, who incidentally is “missing”, wasn’t the nicest of people in " +
		"this here space. ";//Shopkeeper
		dialogTexts [7] = "Also you’re human, aren’t you? So add them together and you know them cops aren’t " +
			"going to give you an easy time.";//Shopkeeper
		dialogTexts [8] = "But anyways, the controls are simple, just press “W” to move your ship forward, " +
			"and “A” and “D” turns you around. Just remember you can’t drive backwards in this thing, since it ain’t got no thrusters in the front.";//Shopkeeper
		dialogTexts [9] = "Just try that for a bit now, you got time before " +
			"them cops arrive here.";//Shopkeeper
		dialogTexts[10] = "Wow, is that it, and I couldn’t figure this out on myself how? " +
			"Would’ve been my first guess anyway.";//Player
		dialogTexts [11] = "Easy peasy. Now, you see you have some guns on either side of your ship there? " +
			"You’ll fire the left side by pressing “Q” and the right side by pressing “E”. ";//Shopkeeper
		dialogTexts[12] = "Both yourself and bullets can be affected by gravity. So your bullets might not go where you want them to go, if you are to close to a planet." +
			" You’ve got infinite ammo for these, thanks to one of them fancy machines on board.";//Shopkeeper
		dialogTexts[13] = "Just fire a couple of volleys from either side, you got enough ammo for " +
			"them to last a lifetime, so don’t worry about wasting them.";//Shopkeeper
		dialogTexts[14] = "Just remember now - those cannons hanging on the side of the ship, " +
			"those will rotate with the ship while you turn. so you might not hit your targets even " +
			"though they are right next to you.";//Shopkeeper
		dialogTexts[15] = "Gee, Einstein, could’ve figured that out on my own.";//Player
		dialogTexts [16] = "Hey, keep your sassy human slang for your kin. Most won’t take kindly to that " +
		"sort of language. "; //Shopkeeper
		dialogTexts [17] = "Anyways, last but not least. The cannon on your roof right there, this is " +
		"a special one. ";
		dialogTexts[18] = "It is way more powerful than your regular cannons, and you can aim it around " +
			"using your mouse. To fire hold down Mouse2, then click Mouse1. Test it out.";//Shopkeeper
		dialogTexts[19] = "Remember, it is a way more accurate and powerful way to take down your opponents, " +
			"but this doesn’t have a lifetime worth of ammo supply, but don’t worry, just visit a shop " +
			"every once in awhile to stock up.";//Shopkeeper
		dialogTexts[20] = "Thanks for telling after i’ve fired a couple, asshole.";//Player
		dialogTexts[21] = "Hey, I am an entrepreneur aren’t i? Need to make money some way or another," +
			" tell you what, i’ll reimburse you for the ones you’ve fired, all right?";//Shopkeeper
		dialogTexts[22] = "Oh would you look at that, looks like we’re done just in time for them coppers" +
			" to arrive here. Now test out what i’ve just taught you.";//Shopkeeper
		dialogTexts[23] = "Stop right there criminal scum. You are wanted for peddling illegal goods " +
			"and scavenged ships. And you, pirate, stay there and we’ll deal with you later.";//Marine
		dialogTexts[24] = "What do you say, pirate? I’ll fix up your ship for free if you help " +
			"me out here. Seems like you’ve got nothing to lose.";//Shopkeeper
		dialogTexts[25] = "If it gets you off my back, fine.";//Player
		dialogTexts [26] = "Nicely done there, humie. Might make a fine pirate of you one day. " +
		"You see those gears and other bits and bobs that’s left after that marine ship? " +
		"This is what us traders take as payment. ";
		dialogTexts[27] = "Fly over there, and pick it up.";//Shopkeeper
		dialogTexts[28] = " And as promised I’ll fix your damage, just fly closer to me " +
			"and i’ll open up a landing pad for you.";//Shopkeeper
		dialogTexts[29] = "Here is what I, and most other shops around this sun has to offer.";//Shopkeeper
		dialogTexts[30] = "You see all these icons on the blueprint of your ship here?";
		dialogTexts [31] = "These " +
		"are upgradeable parts. I can do upgrades for each of your side cannons, increase the damage your hull can take, or your back " +
		"thruster output. ";
		dialogTexts[32] = "The icon in the middle is ammunition for your special weapon. Feel free to get so that you have 20. " +
			"Confirm your purchase on the bottom of the screen.";//Shopkeeper
		dialogTexts [33] = "To repair your ship, just press the button that says “Repair my vessel!” " +
		"and drag the slider for how much you want to repair.";//Shopkeeper
		dialogTexts[34] = "Then just confirm the amount, and " +
			"i’ll fix it right away. And of course, to the top right you’ll see how much money " +
			"you’ve got.";//Shopkeeper
		dialogTexts[35] = "Just fix up your ship right about now, and be on your way.";//Shopkeeper
		dialogTexts[36] = "I guess that’s about it for what I can tell you, be on your way now. " +
			"And remember; Keep alive and keep flying, a living customer is a paying customer. " +
			"Come back anytime should you need something.";//Shopkeeper

	}

	public void dialog (int stage)
	{
		//Player dialog: 1, 4, 9, 13, 17, 22
		//Marine dialog: 20
		//Dialog runs here
		if (stage == 1 || stage == 5 || stage == 10 || stage == 15 || stage == 20 || stage == 25)
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
			//Sets dialog and character
			setDialog (character [0], dialogTexts [stage]);
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

		//Pause, activate guns and other stuff here
		switch (stage) 
		{
		case(1):
			GameObject.Find ("Portrett2_shopkeeper").GetComponent<Animator> ().SetBool ("isHappyShopkeeper", true);
			Debug.Log ("Change mood");
			break;
		case(2):
			GameObject.Find ("Portrett2_shopkeeper").GetComponent<Animator> ().SetBool ("isHappyShopkeeper", false);
			Debug.Log ("Change mood");
			break;
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
			changeButtonInteractivity (false);
			clearButtons ();
			blinkingButtons [0] = GameObject.Find ("button_repair").GetComponent<Button> ();
			blinkingButtons [0].enabled = true;
			break;
		case(34):
			changeButtonInteractivity (false);
			clearButtons ();
			nextButton.SetActive (true);
			GameObject.Find ("button_v").GetComponent<Button> ().enabled = false;
			GameObject.Find ("button_x").GetComponent<Button> ().enabled = false;
			break;
		case(35):
			blinkingButtons [0] = GameObject.Find ("button_v").GetComponent<Button> ();
			nextButton.SetActive (false);
			GameObject.Find ("button_v").GetComponent<Button> ().enabled = true;
			GameObject.Find ("button_x").GetComponent<Button> ().enabled = true;
			changeButtonInteractivity (true);
			break;
		default:
			break;
		}

	}

	public void nextDialog ()
	{
		Debug.Log ("NextDialog");
		dialogNumber++;
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

	private void makeTutorialEnemy()
	{
		Instantiate (AI, this.transform.position, Quaternion.Euler(new Vector3(0, 90, 0)));

		Debug.Log("New Target = " + AI);
	}

	private void changeCharacterWindow()
	{
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
		Array.Clear (blinkingButtons, 0, blinkingButtons.Length);
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
