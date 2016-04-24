using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossTalking : MonoBehaviour 
{
	public static BossTalking talking;
	private GameObject playerPoint;
	private float detectDistance;
	private Vector3 relativePoint;
	private int turnSpeed;
	public bool faced = false;
	private bool firstTime = true;
	private bool doneTaling = false;

	//All text
	private Text dialogTextBox;
	private Text characterName;
	//Next button
	private GameObject nextButton;
	//Number that keeps track of progress
	public int dialogNumber;
	//Array that hold the tutorial dialog
	private string[] dialogTexts = new string[13];
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
	private gameButtons buttonEvents;
	//Character vinduer
	private GameObject bossCharacterWindow;

	bool turnLeft;
	bool turnRight;

	// Use this for initialization
	void Start () 
	{
		talking = this;
		this.GetComponent<AIMaster>().enabled = false;
		this.GetComponent<AImove>().enabled = false;
		this.GetComponent<AIavoid>().enabled = false;
		this.GetComponent<AIPatroling>().enabled = false;
		playerPoint = GameObject.Find ("PlayerShip"); //As the player is a prefab, I had to add it to the variable this way
		turnSpeed = PlayerMove2.move.turnSpeed-1;
	}

	void findAllDialogElements()
	{
		buttonEvents = GameObject.Find ("GameControl").GetComponent<gameButtons> ();
		buttonEvents.pause ();
		for (int i = 0; i < GameObject.Find("dialogue_elements").transform.childCount; i++)
		{
			GameObject.Find("dialogue_elements").transform.GetChild (i).gameObject.SetActive (true);
		}
		dialogTextBox = GameObject.Find ("dialogue_ingame").GetComponent<Text> ();
		characterName = GameObject.Find ("dialogue_name").GetComponent<Text> ();
		questInfo = GameObject.Find ("quest_info_text").GetComponent<Text> ();
		bossCharacterWindow = GameObject.Find ("Portrett2_boss");
		nextButton = GameObject.Find ("dialogue_next");
		nextButton.GetComponent<Button> ().onClick.AddListener (nextDialogBoss);

		nameLeftPos = new Vector3(115.0f, -25.0f);
		nameRightPos = new Vector3 (600.0f, -25.0f);

		textColorPlayer = "#173E3CFF";
		textColorBoss = "#4F3430FF";

		setDialog ();
		dialogBoss (0);
	}

	void setDialog()
	{
		characters [0] = "Boss";
		characters [1] = "Player";

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
		if(GameControl.control.talkedWithBoss == false)
		{
			if (doneTaling == false)
			{
				
				detectDistance = Vector3.Distance (playerPoint.transform.position, this.transform.position); //calculates the distance between the AI and the player

				if (detectDistance < 100)
				{
					if (faced == false)
						facePlayer ();
				}

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
				}
			}
		}
		else
			activateScripts();
	}

	void facePlayer()
	{
		if(firstTime == true)
		{
			playerPoint.GetComponent<PlayerMove2>().enabled = false;
			playerPoint.GetComponent<Rigidbody>().mass = 5;
			playerPoint.GetComponent<Rigidbody>().angularDrag = 5;
			playerPoint.GetComponent<Rigidbody> ().drag = 5;


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

		relativePoint = transform.InverseTransformPoint(playerPoint.transform.position);
		Debug.Log ("Realtivepoint is : " + relativePoint);
		bool playerInFrontOfAI = isFacingPlayer ();

		if(playerInFrontOfAI == true)
		{
			turnLeft = false;
			turnRight = false;
			faced = true;
		}
		else
		{
			if(relativePoint.x <= 0) //The player is to the left of the boss)
			{
				turnLeft = true;
				turnRight = false;
			}
			else
			{
				turnLeft = false;
				turnRight = true;
			}
			
		}
	}

	private bool isFacingPlayer()
	{
		if(relativePoint.z > 0)
		{
			if(relativePoint.x > -1 && relativePoint.x < 1)
			{
				return true;
			}
			else return false;
		}
		else return false;
	}

	void activateScripts()
	{
		GameControl.control.talkedWithBoss = true;
		this.GetComponent<AImove>().enabled = true;
		this.GetComponent<AIavoid>().enabled = true;
		this.GetComponent<AIMaster>().enabled = true;
		this.GetComponent<AIMaster>().kill.gameObject.SetActive(true);

		playerPoint.GetComponent<PlayerMove2>().enabled = true;
		playerPoint.GetComponent<Rigidbody>().mass = 1;
		playerPoint.GetComponent<Rigidbody>().angularDrag = 0.5f;
		playerPoint.GetComponent<Rigidbody>().drag = 0.5f;

		this.GetComponent<BossTalking>().enabled = false;
	}

	private void dialogBoss(int dialogNumber)
	{		
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

		characterName.color = tempColor;
		dialogTextBox.color = tempColor;

		if (dialogNumber == 6)
		{
			GameObject.Find ("Portrett").GetComponent<Animator> ().SetBool ("isAngryMainCharacter", true);
		} 
		else if (dialogNumber == 2)
		{
			GameObject.Find ("Portrett2_boss").GetComponent<Animator> ().SetBool ("isHappyBoss", true);
		} 
		else if (dialogNumber == 7)
		{
			GameObject.Find ("Portrett2_boss").GetComponent<Animator> ().SetBool ("isAngryBoss", true);
		} 
		else if (dialogNumber == 9)
		{
			GameObject.Find ("Portrett").GetComponent<Animator> ().SetBool ("isAngryMainCharacter", true);
		} 
		else if (dialogNumber == 10)
		{
			GameObject.Find ("Portrett2_boss").GetComponent<Animator> ().SetBool ("isAngryBoss", true);
		}
		else if (dialogNumber == 11)
		{
			activateScripts ();
			doneTaling = true;
			buttonEvents.pause ();

			for (int i = 0; i < GameObject.Find("dialogue_elements").transform.childCount; i++)
			{
				GameObject.Find("dialogue_elements").transform.GetChild (i).gameObject.SetActive (false);
			}
		}

	}

	public void nextDialogBoss ()
	{
		dialogNumber++;
		Debug.Log ("NextDialog " + dialogNumber);
		dialogBoss (dialogNumber);
	}

	public void setDialog (string character, string text)
	{
		characterName.text = character;
		dialogTextBox.text = text;
	}
}
