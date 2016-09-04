using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossTalking : MonoBehaviour 
{
	//Playership
	private GameObject playerPoint;
	//distance between boss and player
	private float detectDistance;
	//Players relativ posistion to boss
	private Vector3 relativePoint;
	//Speed boss will turn towards player before dialog
	private int turnSpeed;
	//Bool that holds the value if boss is turned towards player
	public bool faced = false;
	//First time meeting boss
	private bool firstTime = true;
	//Save if shopkeeper talk is done
	private bool doneTaling = false;

	//All text
	private Text dialogTextBox;
	private Text characterName;
	//Next button
	public GameObject nextButton;
	//Number that keeps track of progress
	public int dialogNumber;
	//Array that hold the tutorial dialog
	private string[] dialogTexts = new string[20];
	//Holds quest information
	private Text questInfo;
	//Character names
	private string[] characters = new string[2];
	private Vector2 nameLeftPos;
	private Vector2 nameRightPos;
	private string textColorPlayer;
	private string textColorBoss;
	private Color tempColor;
	//getting ahold of button functions
	private GameButtons buttonEvents;
	//Holds which way boss have to turn to face player before dialog
	bool turnLeft;
	bool turnRight;

	// Use this for initialization
	void Start () 
	{
		//Turn of AI script
		this.GetComponent<AIMaster>().enabled = false;
		this.GetComponent<AImove>().enabled = false;
		this.GetComponent<AIavoid>().enabled = false;
		this.GetComponent<AIPatroling>().enabled = false;
		playerPoint = GameObject.Find ("PlayerShip"); //As the player is a prefab, I had to add it to the variable this way
		//Boss turnspeed
		turnSpeed = PlayerMove.move.turnSpeed-1;
	}

	public void findAllDialogElements()
	{
		//Find the button script
		buttonEvents = GameObject.Find ("GameControl").GetComponent<GameButtons> ();
		//Pause the game
		buttonEvents.pause ();
		//Activate all dialog elements
		for (int i = 0; i < GameObject.Find("dialogue_elements").transform.childCount; i++)
		{
			GameObject.Find("dialogue_elements").transform.GetChild (i).gameObject.SetActive (true);
		}
		//Find dialogelements and give them references
		dialogTextBox = GameObject.Find ("dialogue_ingame").GetComponent<Text> ();
		characterName = GameObject.Find ("dialogue_name").GetComponent<Text> ();
		questInfo = GameObject.Find ("quest_info_text").GetComponent<Text> ();
		nextButton = GameObject.Find ("dialogue_next");
		//Placement characetername
		nameLeftPos = new Vector3(215.0f, -25.0f);
		nameRightPos = new Vector3 (570.0f, -25.0f);
		//Character text color. Made to destinct who is talking. Color match character animation color
		textColorPlayer = "#173E3CFF";
		textColorBoss = "#4F3430FF";
		//all dialog is here
		setDialog ();
		//Starts first dialog.
		dialogBoss (0);
	}

	void setDialog()
	{
		//Characters in this conversation
		characters [0] = "Sir Pincushion";
		characters [1] = "Player";
		//All dialog
		dialogTexts [0] = "Who dares enter my domain?"; //Boss
		dialogTexts [1] = "I do! I need your piece of the ancient cog." +
			" Give it up or prepare to fight me!";//Player
		dialogTexts [2] = "Well, hello to you too. I guess I should not " +
			"be baffled, freebooters always try to steal away my cog.";
		
		dialogTexts[3] = "I cannot really blame them, as I am too an immoral pirate" +
		" with a craving for plundering. But I do wish we could handle" +
			" this in a more civilised manner, one pirate to another. ";//Boss
		dialogTexts [4] = "How about we have a game of wits instead? " +
		"Let's see who knows more about beeing a pirate."; //Boss
		dialogTexts[5] = "I am quite knowledgeable, if I do say so myself, " +
		"so don’t be surprised if I beat you with a quiz. What say you, " +
			"bosom buddy?";//Boss
		dialogTexts [6] = "Huh?... I’m not your buddy, so cut the crap! " +
		"You have no idea of how much I had to go through go get here! " +
		"Just fight me already, I have stuff to do!";//Player
		dialogTexts [7] = "Oh your poor peasant, how foolish you are!";
		dialogTexts[8] = "I feel the urge to giggle at your disgraceful behavior, " +
		"but I expect you would try to blow me to smithereens before " +
			"I could open my mouth.";//Boss
		dialogTexts [9] = "Enough talk, let’s fight!";//Player
		dialogTexts [10] = "Certainly!";//Boss
		dialogTexts [12] = "You have bested me. Bollocks!";//Boss
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//Checking if player has already talked to the boss. Happens if player dies and comes back og runs away.
		if (GameControl.control.talkedWithBoss == false)
		{
			//Checks if player is done talking with boss
			if (doneTaling == false)
			{
				detectDistance = Vector3.Distance (playerPoint.transform.position, this.transform.position); //calculates the distance between the AI and the player
				//If player is close boss rotates around to face player.
				if (detectDistance < 100)
				{
					//Checks if boss is already facing player
					if (faced == false)
						facePlayer ();
				}
				//Rotate boss if not facing player
				if (faced == false)
				{
					if (turnLeft == true)
					{
						this.transform.Rotate (Vector3.down, turnSpeed * Time.deltaTime);
					}

					if (turnRight == true)
					{
						this.transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
					}
				} else
				{
					Debug.Log ("Turn successful");

					findAllDialogElements ();
					//Sets nextbutton to execture function nextDialogBoss when clicked
					nextButton.GetComponent<Button> ().onClick.AddListener (nextDialogBoss);
				}
			}
		} 
		else
		{
			//If player has already talked to boss activate all boss scripts
			activateScripts ();
		}
	}

	void facePlayer()
	{
		//checks if it is first time player fights boss
		if(firstTime == true)
		{
			//Makes player heavy and not moveable
			playerPoint.GetComponent<PlayerMove>().enabled = false;
			playerPoint.GetComponent<Rigidbody>().mass = 5;
			playerPoint.GetComponent<Rigidbody>().angularDrag = 5;
			playerPoint.GetComponent<Rigidbody> ().drag = 5;

			//Makes all marineships go away
			foreach(GameObject go in SpawnAI.spawn.marineShips)
			{
				if (go != null)
				{
						GameControl.control.isFighting = true;
					if (go.GetComponent<AIMaster> ().isFighting == true)
						go.GetComponent<AIMaster> ().reactivatePatroling ();
				}
					
			}
		}
		//Calculates player possion relative to boss to see if boss need to turn and which direction
		relativePoint = transform.InverseTransformPoint(playerPoint.transform.position);
		Debug.Log ("Realtivepoint is : " + relativePoint);
		bool playerInFrontOfAI = isFacingPlayer ();

		if(playerInFrontOfAI == true) //Boss if facing player
		{
			turnLeft = false;
			turnRight = false;
			faced = true;
		}
		else //Boss needs to turn
		{
			if(relativePoint.x <= 0) //The player is to the left of the boss)
			{
				turnLeft = true;
				turnRight = false;
			}
			else //The player is to the right of the boss
			{
				turnLeft = false;
				turnRight = true;
			}
			
		}
	}

	private bool isFacingPlayer() //See if the player is directly in front of the boss
	{
		if(relativePoint.z > 0) //Player in front of the boss
		{
			if(relativePoint.x > -1 && relativePoint.x < 1) //Player directly in front of the boss
			{
				return true; //Return true
			}
			else return false; //Player not directly in front of the boss
		}
		else return false; //Player behind the boss
	}

	void activateScripts() //Reactivate boss and player
	{
		//Activate boss scripts
		GameControl.control.talkedWithBoss = true;
		this.GetComponent<AImove>().enabled = true;
		this.GetComponent<AIavoid>().enabled = true;
		this.GetComponent<AIMaster>().enabled = true;
		this.GetComponent<AIMaster>().kill.gameObject.SetActive(true);

		//Activate PlayerMove and reduce mass on RigidBody
		playerPoint.GetComponent<PlayerMove>().enabled = true;
		playerPoint.GetComponent<Rigidbody>().mass = 1;
		playerPoint.GetComponent<Rigidbody>().angularDrag = 0.5f;
		playerPoint.GetComponent<Rigidbody>().drag = 0.5f;

		this.GetComponent<BossTalking>().enabled = false;

		BackgroundSongsController.audControl.bossCombat(); //Play the correct combatsong
	}

	//Function that keeps track of what is going to happen with each dialog
	public void dialogBoss(int dialogNumber)
	{		
		//this is player dialog. Right color and position is applied
		if (dialogNumber == 1 || dialogNumber == 6 || dialogNumber == 9)
		{
			Debug.Log("NextDialog with character: " + characters[1]);
			//Sets dialog and character
			setDialog (characters[1], dialogTexts [dialogNumber]);
			//Moves name closer to portrait
			if (characterName.gameObject.GetComponent<RectTransform>().anchoredPosition != nameLeftPos)
			{
				Debug.Log ("Change character");
				characterName.gameObject.GetComponent<RectTransform>().anchoredPosition = nameLeftPos;
			}
			//Changes color on text which is closer to character
			ColorUtility.TryParseHtmlString (textColorPlayer, out tempColor);
		} 
		//This is boss dialog.
		else
		{
			//Sets dialog and character
			Debug.Log("NextDialog with character: " + characters[0]);
			setDialog (characters [0], dialogTexts [dialogNumber]);
			//Moves name closer to portrait
			if (characterName.gameObject.GetComponent<RectTransform>().anchoredPosition != nameRightPos)
			{
				Debug.Log ("Change character");
				characterName.gameObject.GetComponent<RectTransform>().anchoredPosition = nameRightPos;
			}
			//Changes color on text which is closer to character
			ColorUtility.TryParseHtmlString (textColorBoss, out tempColor);
		}
		//Sets charactername and dialog color after which character that is talking
		characterName.color = tempColor;
		dialogTextBox.color = tempColor;

		//Dialogoptions
		if (dialogNumber == 0)
		{
			//Set quest
			questInfo.text = "Talk to Sir Pincushion";
		} 
		else if (dialogNumber == 2)
		{
			//animate boss
			GameObject.Find ("Portrett2_boss").GetComponent<Animator> ().SetBool ("isHappyBoss", true);
		} 
		else if (dialogNumber == 6)
		{
			//Animate player
			GameObject.Find ("Portrett").GetComponent<Animator> ().SetBool ("isAngryMainCharacter", true);
		} 
		else if (dialogNumber == 7)
		{
			//If boss animation from dialog 2 is still running it must be stopped before next animation is played
			if (GameObject.Find ("Portrett2_boss").GetComponent<Animator> ().GetBool ("isHappyBoss") == true)
			{
				GameObject.Find ("Portrett2_boss").GetComponent<Animator> ().SetBool ("isHappyBoss", false);
			}
			//Play boss animation
			GameObject.Find ("Portrett2_boss").GetComponent<Animator> ().SetBool ("isAngryBoss", true);
		} 
		else if (dialogNumber == 9)
		{
			//Animate player
			GameObject.Find ("Portrett").GetComponent<Animator> ().SetBool ("isAngryMainCharacter", true);
		} 
		else if (dialogNumber == 10)
		{
			//animate boss
			GameObject.Find ("Portrett2_boss").GetComponent<Animator> ().SetBool ("isAngryBoss", true);
		} 
		else if (dialogNumber == 11)
		{
			//Set questinfo and prepare everything for fight with boss.
			questInfo.text = "Defeat Sir Pincushion";
			activateScripts ();
			doneTaling = true;
			buttonEvents.pause ();

			//Turn off dialogelements
			for (int i = 0; i < GameObject.Find ("dialogue_elements").transform.childCount; i++)
			{
				GameObject.Find ("dialogue_elements").transform.GetChild (i).gameObject.SetActive (false);
			}
		} 
		else if (dialogNumber == 12)
		{
			//Happy player when boss is dead. Last dialog before endscreen
			if (GameObject.Find ("Portrett"))
			{
				GameObject.Find ("Portrett").GetComponent<Animator> ().SetBool ("isHappyMainCharacter", true);
			}
		}

	}

	public void nextDialogBoss ()
	{
		//function to go to next dialog
		dialogNumber++;
		Debug.Log ("NextDialog " + dialogNumber);
		dialogBoss (dialogNumber);
	}

	public void setDialog (string character, string text)
	{
		//Sets text in right objects
		characterName.text = character;
		dialogTextBox.text = text;
	}
}
