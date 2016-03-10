using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	private string[] dialogTexts = new string[35];
	//Holds quest information
	private Text questInfo;
	//Character names
	private string[] character = new string[3];
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

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
	}
	void Start ()
	{
		marineCharacterWindow.SetActive (false);
		//Initialize functions
		dialogTextBox = GameObject.Find ("dialogue_ingame").GetComponent<Text> ();
		characterName = GameObject.Find ("dialogue_name").GetComponent<Text> ();
		questInfo = GameObject.Find ("quest_info_text").GetComponent<Text> ();
		buttonEvents = GameObject.Find ("GameControl").GetComponent<gameButtons> ();
		compass = GameObject.Find ("compass_needle").GetComponent<PointTowards> ();


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
	}

	private void dialogInArray()
	{
		character[0] = "Shopkeeper";
		character[1] = "Player";
		character[2] = "Marine";

		dialogTexts[0] = "Hey, congratulations on acquiring this beauty from yours truly.";//Shopkeeper
		dialogTexts [1] = "What now, I already bought the ship, what more could you possibly want from me?";//Player
		dialogTexts [2] = "Well, I have a the feeling you haven’t really flown one of these things before, am I right? "; //Shopkeeper
		dialogTexts[3] = "So you have two options, either figuring out the controls for yourself, or let me help you. And at the moment, I can see one of them space cop types coming towards here on the radar. " +
			"So I guess you don’t want to get shot into bits in an instant here."; //Shopkeeper
		dialogTexts[4] = "Why would they bother me? But alright, just tell me the basics " +
			"and let me be on my way.";//Player
		dialogTexts [5] = "Well, that beautiful hunk of metal you’ve just bought ain’t exactly acquired legally, " +
		"you see, and the previous captain, who incidentally is “missing” wasn’t the nicest of people in " +
		"this here space. ";//Shopkeeper
		dialogTexts [6] = "Also you’re human, aren’t you? So add them together and you know them cops aren’t " +
			"going to give you an easy time.";//Shopkeeper
		dialogTexts [7] = "But anyways, the controls are simple, just press “W” to move your ship forward, " +
			"and “A” and “D” turns you around. Just remember you can’t drive backwards in this thing, since it ain’t got no thrusters in the front.";//Shopkeeper
		dialogTexts [8] = " Just try that for a bit now, you got time before " +
			"them cops arrive here.";//Shopkeeper
		dialogTexts[9] = "Wow, is that it, and I couldn’t figure this out on myself how? " +
			"Would’ve been my first guess anyway.";//Player
		dialogTexts[10] = "Easy peasy. Now, you see you have some guns on either side of your ship there? " +
			"You’ll fire the left side by pressing “Q” and the right side by pressing “E”. " +
			"You’ve got infinite ammo for these, thanks to one of them fancy machines on board.";//Shopkeeper
		dialogTexts[11] = "Just fire a couple of volleys from either side, you got enough ammo for " +
			"them to last a lifetime, so don’t worry about wasting them.";//Shopkeeper
		dialogTexts[12] = "Just remember now - those cannons hanging on the side of the ship, " +
			"those will rotate with the ship while you turn. so you might not hit your targets even " +
			"though they are right next to you.";//Shopkeeper
		dialogTexts[13] = "Gee, Einstein, could’ve figured that out on my own.";//Player
		dialogTexts [14] = "Hey, keep your sassy human slang for your kin, most won’t take kindly to that " +
		"sort of language. "; //Shopkeeper
		dialogTexts[15] = "Anyways, last but not least. The cannon on your roof right there, this is " +
			"a special one. It is way more powerful than your regular cannons, and you can aim it around " +
			"using your mouse, and fire using “mouse 1”. Test it out.";//Shopkeeper
		dialogTexts[16] = "Remember, it is a way more accurate and powerful way to take down your opponents, " +
			"but this doesn’t have a lifetime worth of ammo supply, but don’t worry, just visit a shop " +
			"every once in awhile to stock up.";//Shopkeeper
		dialogTexts[17] = "Thanks for telling after i’ve fired a couple, asshole.";//Player
		dialogTexts[18] = "Hey, I am an entrepreneur aren’t i? Need to make money some way or another," +
			" tell you what, i’ll reimburse you for the ones you’ve fired, all right?";//Shopkeeper
		dialogTexts[19] = "Oh would you look at that, looks like we’re done just in time for them coppers" +
			" to arrive here. Now test out what i’ve just taught you.";//Shopkeeper
		dialogTexts[20] = "Stop right there criminal scum. You are wanted for peddling illegal goods " +
			"and scavenged ships. And you, pirate, stay there and we’ll deal with you later.";//Marine
		dialogTexts[21] = "What do you say, pirate? I’ll fix up your ship for free if you help " +
			"me out here. Seems like you’ve got nothing to lose.";//Shopkeeper
		dialogTexts[22] = "If it gets you off my back, fine.";//Player
		dialogTexts [23] = "Nicely done there, humie. Might make a fine pirate of you one day. " +
		"You see those gears and other bits and bobs that’s left after that marine ship? " +
		"This is what us traders take as payment. ";
		dialogTexts[24] = "Fly over there, and pick it up.";//Shopkeeper
		dialogTexts[25] = " And as promised i’ll fix your damage, just fly closer to me " +
			"and i’ll open up a landing pad for you.";//Shopkeeper
		dialogTexts[26] = "Here is what i, and most other shops around this sun has to offer.";//Shopkeeper
		dialogTexts[27] = "You see all these icons on the blueprint of your ship here?";
		dialogTexts[28] = "These " +
			"are upgradeable parts,I can do upgrades for each of your side cannons, or I could " +
			"increase the damage your hull can take before you’re blown to bits, or your back " +
			"thruster output. The icon in the middle is ammunition for your special weapon. " +
			"Confirm your purchase on the bottom of the screen.";//Shopkeeper
		dialogTexts [29] = "To repair your ship, just press the button that says “Repair my vessel!” " +
		"and drag the slider for how much you want to repair. ";//Shopkeeper
		dialogTexts[30] = "Then just confirm the amount, and " +
			"i’ll fix it right away. And of course, to the top left you’ll see how much money " +
			"you’ve got.";//Shopkeeper
		dialogTexts[31] = "Just fix up your ship right about now, and be on your way.";//Shopkeeper
		dialogTexts[32] = "I guess that’s about it for what I can tell you, be on your way now. " +
			"And remember; Keep alive and keep flying, a living customer is a paying customer. " +
			"Come back anytime should you need something.";//Shopkeeper

		//Added 5 dialogs
	}

	//Need to recieve a parameter to decide which dialog to run
	public void dialog (int stage)
	{
		//Player dialog: 1, 4, 9, 13, 17, 22
		//Marine dialog: 20
		//Dialog runs here
		if (stage == 1 || stage == 4 || stage == 9 || stage == 13 || stage == 17 || stage == 22)
		{
			setDialog (character [1], dialogTexts [stage]);
		} 
		else if (stage == 20)
		{
			setDialog (character [2], dialogTexts [stage]);
		} 
		else
		{
			setDialog (character [0], dialogTexts [stage]);
		}


		//Pause, activate guns and other stuff here
		switch (stage) 
		{
		case(8):
			buttonEvents.pause ();
			pauseText.SetActive (false);
			questInfo.text = "Move using W A D.";
			break;
		case(9):
			buttonEvents.pause ();
			pauseText.SetActive (true);
			questInfo.text = "Talk to Shopkeeper.";
			break;
		case(11):
			buttonEvents.pause ();
			pauseText.SetActive (false);
			shootingAllowed (true, "GameObject");
			questInfo.text = "Fire sidcanons using Q and E.";
			break;
		case(12):
			buttonEvents.pause ();
			pauseText.SetActive (true);
			shootingAllowed (true, "NULL");
			questInfo.text = "Talk to Shopkeeper.";
			break;
		case(15):
			buttonEvents.pause ();
			pauseText.SetActive (false);
			questInfo.text = "Fire special weapon using Mouse1.";
			break;
		case(16):
			buttonEvents.pause ();
			pauseText.SetActive (true);
			questInfo.text = "Talk to Shopkeeper.";
			break;
		case(20):
			makeTutorialEnemy ();
			changeCharacterWindow ();
			break;
		case(21):
			changeCharacterWindow ();
			break;
		case(22):
			changeCharacterWindow ();
			buttonEvents.pause ();
			questInfo.text = "Destroy Marineship.";
			compass.goTarget = GameObject.FindGameObjectWithTag("aiShip");
			pauseText.SetActive (false);
			nextButton.SetActive (false);
			break;
		case(23):
			changeCharacterWindow ();
			scrapHolder = (GameObject.FindGameObjectsWithTag ("scrap"));
			scrapCount = scrapHolder.Length;
			Debug.Log ("Scraps left to pick up: " + scrapCount);
			buttonEvents.pause ();
			pauseText.SetActive (true);
			nextButton.SetActive (true);
			break;
		case(24):
			buttonEvents.pause ();
			questInfo.text = "Pickup loot.";
			pauseText.SetActive (false);
			nextButton.SetActive (false);
			break;
		case(25):
			questInfo.text = "Enter store.";
			enterStore = true;
			break;
		default:
			break;
		}

	}

	public void nextDialog ()
	{
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
		Debug.Log ("Scraps left to pick up: " + scrapCount);
		scrapCount--;

		if (scrapCount <= 0)
		{
			nextDialog ();
		}
	}
			
}
